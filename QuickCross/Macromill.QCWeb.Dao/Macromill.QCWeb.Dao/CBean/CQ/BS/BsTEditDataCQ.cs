
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTEditDataCQ : AbstractBsTEditDataCQ {

        protected TEditDataCIQ _inlineQuery;

        public BsTEditDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TEditDataCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TEditDataCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TEditDataCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TEditDataCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TEditConditionCQ> _dataEditId_ExistsSubQuery_TEditConditionListMap;
        public Map<String, TEditConditionCQ> DataEditId_ExistsSubQuery_TEditConditionList { get { return _dataEditId_ExistsSubQuery_TEditConditionListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TEditConditionListMap == null) { _dataEditId_ExistsSubQuery_TEditConditionListMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TEditConditionListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TEditConditionListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TEditConditionList." + key;
        }

        protected Map<String, TEditTargetItemCQ> _dataEditId_ExistsSubQuery_TEditTargetItemListMap;
        public Map<String, TEditTargetItemCQ> DataEditId_ExistsSubQuery_TEditTargetItemList { get { return _dataEditId_ExistsSubQuery_TEditTargetItemListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TEditTargetItemListMap == null) { _dataEditId_ExistsSubQuery_TEditTargetItemListMap = new LinkedHashMap<String, TEditTargetItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TEditTargetItemListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TEditTargetItemListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TEditTargetItemList." + key;
        }

        protected Map<String, TEditConditionCQ> _dataEditId_NotExistsSubQuery_TEditConditionListMap;
        public Map<String, TEditConditionCQ> DataEditId_NotExistsSubQuery_TEditConditionList { get { return _dataEditId_NotExistsSubQuery_TEditConditionListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TEditConditionListMap == null) { _dataEditId_NotExistsSubQuery_TEditConditionListMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TEditConditionListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TEditConditionListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TEditConditionList." + key;
        }

        protected Map<String, TEditTargetItemCQ> _dataEditId_NotExistsSubQuery_TEditTargetItemListMap;
        public Map<String, TEditTargetItemCQ> DataEditId_NotExistsSubQuery_TEditTargetItemList { get { return _dataEditId_NotExistsSubQuery_TEditTargetItemListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TEditTargetItemListMap == null) { _dataEditId_NotExistsSubQuery_TEditTargetItemListMap = new LinkedHashMap<String, TEditTargetItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TEditTargetItemListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TEditTargetItemListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TEditTargetItemList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_InScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_InScopeSubQuery_TDataEditList { get { return _dataEditId_InScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataEditListMap == null) { _dataEditId_InScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TEditConditionCQ> _dataEditId_InScopeSubQuery_TEditConditionListMap;
        public Map<String, TEditConditionCQ> DataEditId_InScopeSubQuery_TEditConditionList { get { return _dataEditId_InScopeSubQuery_TEditConditionListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TEditConditionListMap == null) { _dataEditId_InScopeSubQuery_TEditConditionListMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TEditConditionListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TEditConditionListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TEditConditionList." + key;
        }

        protected Map<String, TEditTargetItemCQ> _dataEditId_InScopeSubQuery_TEditTargetItemListMap;
        public Map<String, TEditTargetItemCQ> DataEditId_InScopeSubQuery_TEditTargetItemList { get { return _dataEditId_InScopeSubQuery_TEditTargetItemListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TEditTargetItemListMap == null) { _dataEditId_InScopeSubQuery_TEditTargetItemListMap = new LinkedHashMap<String, TEditTargetItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TEditTargetItemListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TEditTargetItemListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TEditTargetItemList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_NotInScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_NotInScopeSubQuery_TDataEditList { get { return _dataEditId_NotInScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataEditListMap == null) { _dataEditId_NotInScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TEditConditionCQ> _dataEditId_NotInScopeSubQuery_TEditConditionListMap;
        public Map<String, TEditConditionCQ> DataEditId_NotInScopeSubQuery_TEditConditionList { get { return _dataEditId_NotInScopeSubQuery_TEditConditionListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TEditConditionList(TEditConditionCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TEditConditionListMap == null) { _dataEditId_NotInScopeSubQuery_TEditConditionListMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TEditConditionListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TEditConditionListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TEditConditionList." + key;
        }

        protected Map<String, TEditTargetItemCQ> _dataEditId_NotInScopeSubQuery_TEditTargetItemListMap;
        public Map<String, TEditTargetItemCQ> DataEditId_NotInScopeSubQuery_TEditTargetItemList { get { return _dataEditId_NotInScopeSubQuery_TEditTargetItemListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TEditTargetItemListMap == null) { _dataEditId_NotInScopeSubQuery_TEditTargetItemListMap = new LinkedHashMap<String, TEditTargetItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TEditTargetItemListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TEditTargetItemListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TEditTargetItemList." + key;
        }

        protected Map<String, TEditConditionCQ> _dataEditId_SpecifyDerivedReferrer_TEditConditionListMap;
        public Map<String, TEditConditionCQ> DataEditId_SpecifyDerivedReferrer_TEditConditionList { get { return _dataEditId_SpecifyDerivedReferrer_TEditConditionListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TEditConditionList(TEditConditionCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TEditConditionListMap == null) { _dataEditId_SpecifyDerivedReferrer_TEditConditionListMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TEditConditionListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TEditConditionListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TEditConditionList." + key;
        }

        protected Map<String, TEditTargetItemCQ> _dataEditId_SpecifyDerivedReferrer_TEditTargetItemListMap;
        public Map<String, TEditTargetItemCQ> DataEditId_SpecifyDerivedReferrer_TEditTargetItemList { get { return _dataEditId_SpecifyDerivedReferrer_TEditTargetItemListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TEditTargetItemListMap == null) { _dataEditId_SpecifyDerivedReferrer_TEditTargetItemListMap = new LinkedHashMap<String, TEditTargetItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TEditTargetItemListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TEditTargetItemListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TEditTargetItemList." + key;
        }

        protected Map<String, TEditConditionCQ> _dataEditId_QueryDerivedReferrer_TEditConditionListMap;
        public Map<String, TEditConditionCQ> DataEditId_QueryDerivedReferrer_TEditConditionList { get { return _dataEditId_QueryDerivedReferrer_TEditConditionListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TEditConditionList(TEditConditionCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TEditConditionListMap == null) { _dataEditId_QueryDerivedReferrer_TEditConditionListMap = new LinkedHashMap<String, TEditConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TEditConditionListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TEditConditionListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TEditConditionList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TEditConditionListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TEditConditionListParameter { get { return _dataEditId_QueryDerivedReferrer_TEditConditionListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TEditConditionListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TEditConditionListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TEditConditionListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TEditConditionListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TEditConditionListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TEditConditionListParameter." + key;
        }

        protected Map<String, TEditTargetItemCQ> _dataEditId_QueryDerivedReferrer_TEditTargetItemListMap;
        public Map<String, TEditTargetItemCQ> DataEditId_QueryDerivedReferrer_TEditTargetItemList { get { return _dataEditId_QueryDerivedReferrer_TEditTargetItemListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TEditTargetItemList(TEditTargetItemCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TEditTargetItemListMap == null) { _dataEditId_QueryDerivedReferrer_TEditTargetItemListMap = new LinkedHashMap<String, TEditTargetItemCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TEditTargetItemListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TEditTargetItemListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TEditTargetItemList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TEditTargetItemListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TEditTargetItemListParameter { get { return _dataEditId_QueryDerivedReferrer_TEditTargetItemListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TEditTargetItemListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TEditTargetItemListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TEditTargetItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TEditTargetItemListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TEditTargetItemListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TEditTargetItemListParameter." + key;
        }

        public BsTEditDataCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTEditDataCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        protected ConditionValue _conditionFlag;
        public ConditionValue ConditionFlag {
            get { if (_conditionFlag == null) { _conditionFlag = new ConditionValue(); } return _conditionFlag; }
        }
        protected override ConditionValue getCValueConditionFlag() { return this.ConditionFlag; }


        public BsTEditDataCQ AddOrderBy_ConditionFlag_Asc() { regOBA("CONDITION_FLAG");return this; }
        public BsTEditDataCQ AddOrderBy_ConditionFlag_Desc() { regOBD("CONDITION_FLAG");return this; }

        protected ConditionValue _editMethod;
        public ConditionValue EditMethod {
            get { if (_editMethod == null) { _editMethod = new ConditionValue(); } return _editMethod; }
        }
        protected override ConditionValue getCValueEditMethod() { return this.EditMethod; }


        public BsTEditDataCQ AddOrderBy_EditMethod_Asc() { regOBA("EDIT_METHOD");return this; }
        public BsTEditDataCQ AddOrderBy_EditMethod_Desc() { regOBD("EDIT_METHOD");return this; }

        protected ConditionValue _editValueType;
        public ConditionValue EditValueType {
            get { if (_editValueType == null) { _editValueType = new ConditionValue(); } return _editValueType; }
        }
        protected override ConditionValue getCValueEditValueType() { return this.EditValueType; }


        public BsTEditDataCQ AddOrderBy_EditValueType_Asc() { regOBA("EDIT_VALUE_TYPE");return this; }
        public BsTEditDataCQ AddOrderBy_EditValueType_Desc() { regOBD("EDIT_VALUE_TYPE");return this; }

        protected ConditionValue _editValue;
        public ConditionValue EditValue {
            get { if (_editValue == null) { _editValue = new ConditionValue(); } return _editValue; }
        }
        protected override ConditionValue getCValueEditValue() { return this.EditValue; }


        public BsTEditDataCQ AddOrderBy_EditValue_Asc() { regOBA("EDIT_VALUE");return this; }
        public BsTEditDataCQ AddOrderBy_EditValue_Desc() { regOBD("EDIT_VALUE");return this; }

        protected ConditionValue _conditionDiv;
        public ConditionValue ConditionDiv {
            get { if (_conditionDiv == null) { _conditionDiv = new ConditionValue(); } return _conditionDiv; }
        }
        protected override ConditionValue getCValueConditionDiv() { return this.ConditionDiv; }


        public BsTEditDataCQ AddOrderBy_ConditionDiv_Asc() { regOBA("CONDITION_DIV");return this; }
        public BsTEditDataCQ AddOrderBy_ConditionDiv_Desc() { regOBD("CONDITION_DIV");return this; }

        public BsTEditDataCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTEditDataCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TEditDataCQ baseQuery = (TEditDataCQ)baseQueryAsSuper;
            TEditDataCQ unionQuery = (TEditDataCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDataEditList()) {
                unionQuery.QueryTDataEditList().reflectRelationOnUnionQuery(baseQuery.QueryTDataEditList(), unionQuery.QueryTDataEditList());
            }

        }
    
        protected TDataEditListCQ _conditionQueryTDataEditList;
        public TDataEditListCQ QueryTDataEditList() {
            return this.ConditionQueryTDataEditList;
        }
        public TDataEditListCQ ConditionQueryTDataEditList {
            get {
                if (_conditionQueryTDataEditList == null) {
                    _conditionQueryTDataEditList = xcreateQueryTDataEditList();
                    xsetupOuterJoin_TDataEditList();
                }
                return _conditionQueryTDataEditList;
            }
        }
        protected TDataEditListCQ xcreateQueryTDataEditList() {
            String nrp = resolveNextRelationPathTDataEditList();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDataEditListCQ cq = new TDataEditListCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDataEditList"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDataEditList() {
            TDataEditListCQ cq = ConditionQueryTDataEditList;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDataEditList() {
            return resolveNextRelationPath("T_EDIT_DATA", "tDataEditList");
        }
        public bool hasConditionQueryTDataEditList() {
            return _conditionQueryTDataEditList != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TEditDataCQ> _scalarSubQueryMap;
	    public Map<String, TEditDataCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TEditDataCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TEditDataCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TEditDataCQ> _myselfInScopeSubQueryMap;
        public Map<String, TEditDataCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TEditDataCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TEditDataCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
