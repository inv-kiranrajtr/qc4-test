
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDeleteDataCQ : AbstractBsTDeleteDataCQ {

        protected TDeleteDataCIQ _inlineQuery;

        public BsTDeleteDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDeleteDataCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDeleteDataCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDeleteDataCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDeleteDataCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TDeleteConditionCQ> _dataEditId_ExistsSubQuery_TDeleteConditionListMap;
        public Map<String, TDeleteConditionCQ> DataEditId_ExistsSubQuery_TDeleteConditionList { get { return _dataEditId_ExistsSubQuery_TDeleteConditionListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TDeleteConditionListMap == null) { _dataEditId_ExistsSubQuery_TDeleteConditionListMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TDeleteConditionListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TDeleteConditionListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TDeleteConditionList." + key;
        }

        protected Map<String, TDeleteSampleIdListCQ> _dataEditId_ExistsSubQuery_TDeleteSampleIdListListMap;
        public Map<String, TDeleteSampleIdListCQ> DataEditId_ExistsSubQuery_TDeleteSampleIdListList { get { return _dataEditId_ExistsSubQuery_TDeleteSampleIdListListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TDeleteSampleIdListListMap == null) { _dataEditId_ExistsSubQuery_TDeleteSampleIdListListMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TDeleteSampleIdListListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TDeleteSampleIdListListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TDeleteSampleIdListList." + key;
        }

        protected Map<String, TDeleteConditionCQ> _dataEditId_NotExistsSubQuery_TDeleteConditionListMap;
        public Map<String, TDeleteConditionCQ> DataEditId_NotExistsSubQuery_TDeleteConditionList { get { return _dataEditId_NotExistsSubQuery_TDeleteConditionListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TDeleteConditionListMap == null) { _dataEditId_NotExistsSubQuery_TDeleteConditionListMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TDeleteConditionListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TDeleteConditionListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TDeleteConditionList." + key;
        }

        protected Map<String, TDeleteSampleIdListCQ> _dataEditId_NotExistsSubQuery_TDeleteSampleIdListListMap;
        public Map<String, TDeleteSampleIdListCQ> DataEditId_NotExistsSubQuery_TDeleteSampleIdListList { get { return _dataEditId_NotExistsSubQuery_TDeleteSampleIdListListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TDeleteSampleIdListListMap == null) { _dataEditId_NotExistsSubQuery_TDeleteSampleIdListListMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TDeleteSampleIdListListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TDeleteSampleIdListListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TDeleteSampleIdListList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_InScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_InScopeSubQuery_TDataEditList { get { return _dataEditId_InScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataEditListMap == null) { _dataEditId_InScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TDeleteConditionCQ> _dataEditId_InScopeSubQuery_TDeleteConditionListMap;
        public Map<String, TDeleteConditionCQ> DataEditId_InScopeSubQuery_TDeleteConditionList { get { return _dataEditId_InScopeSubQuery_TDeleteConditionListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDeleteConditionListMap == null) { _dataEditId_InScopeSubQuery_TDeleteConditionListMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDeleteConditionListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDeleteConditionListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDeleteConditionList." + key;
        }

        protected Map<String, TDeleteSampleIdListCQ> _dataEditId_InScopeSubQuery_TDeleteSampleIdListListMap;
        public Map<String, TDeleteSampleIdListCQ> DataEditId_InScopeSubQuery_TDeleteSampleIdListList { get { return _dataEditId_InScopeSubQuery_TDeleteSampleIdListListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDeleteSampleIdListListMap == null) { _dataEditId_InScopeSubQuery_TDeleteSampleIdListListMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDeleteSampleIdListListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDeleteSampleIdListListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDeleteSampleIdListList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_NotInScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_NotInScopeSubQuery_TDataEditList { get { return _dataEditId_NotInScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataEditListMap == null) { _dataEditId_NotInScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TDeleteConditionCQ> _dataEditId_NotInScopeSubQuery_TDeleteConditionListMap;
        public Map<String, TDeleteConditionCQ> DataEditId_NotInScopeSubQuery_TDeleteConditionList { get { return _dataEditId_NotInScopeSubQuery_TDeleteConditionListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDeleteConditionListMap == null) { _dataEditId_NotInScopeSubQuery_TDeleteConditionListMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDeleteConditionListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDeleteConditionListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDeleteConditionList." + key;
        }

        protected Map<String, TDeleteSampleIdListCQ> _dataEditId_NotInScopeSubQuery_TDeleteSampleIdListListMap;
        public Map<String, TDeleteSampleIdListCQ> DataEditId_NotInScopeSubQuery_TDeleteSampleIdListList { get { return _dataEditId_NotInScopeSubQuery_TDeleteSampleIdListListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDeleteSampleIdListListMap == null) { _dataEditId_NotInScopeSubQuery_TDeleteSampleIdListListMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDeleteSampleIdListListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDeleteSampleIdListListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDeleteSampleIdListList." + key;
        }

        protected Map<String, TDeleteConditionCQ> _dataEditId_SpecifyDerivedReferrer_TDeleteConditionListMap;
        public Map<String, TDeleteConditionCQ> DataEditId_SpecifyDerivedReferrer_TDeleteConditionList { get { return _dataEditId_SpecifyDerivedReferrer_TDeleteConditionListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TDeleteConditionList(TDeleteConditionCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TDeleteConditionListMap == null) { _dataEditId_SpecifyDerivedReferrer_TDeleteConditionListMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TDeleteConditionListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TDeleteConditionListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TDeleteConditionList." + key;
        }

        protected Map<String, TDeleteSampleIdListCQ> _dataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListListMap;
        public Map<String, TDeleteSampleIdListCQ> DataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListList { get { return _dataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListListMap == null) { _dataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListListMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListList." + key;
        }

        protected Map<String, TDeleteConditionCQ> _dataEditId_QueryDerivedReferrer_TDeleteConditionListMap;
        public Map<String, TDeleteConditionCQ> DataEditId_QueryDerivedReferrer_TDeleteConditionList { get { return _dataEditId_QueryDerivedReferrer_TDeleteConditionListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDeleteConditionList(TDeleteConditionCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TDeleteConditionListMap == null) { _dataEditId_QueryDerivedReferrer_TDeleteConditionListMap = new LinkedHashMap<String, TDeleteConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TDeleteConditionListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDeleteConditionListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TDeleteConditionList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TDeleteConditionListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TDeleteConditionListParameter { get { return _dataEditId_QueryDerivedReferrer_TDeleteConditionListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDeleteConditionListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TDeleteConditionListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TDeleteConditionListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TDeleteConditionListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDeleteConditionListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TDeleteConditionListParameter." + key;
        }

        protected Map<String, TDeleteSampleIdListCQ> _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListMap;
        public Map<String, TDeleteSampleIdListCQ> DataEditId_QueryDerivedReferrer_TDeleteSampleIdListList { get { return _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListMap == null) { _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TDeleteSampleIdListList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameter { get { return _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameter." + key;
        }

        public BsTDeleteDataCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDeleteDataCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        protected ConditionValue _deleteType;
        public ConditionValue DeleteType {
            get { if (_deleteType == null) { _deleteType = new ConditionValue(); } return _deleteType; }
        }
        protected override ConditionValue getCValueDeleteType() { return this.DeleteType; }


        public BsTDeleteDataCQ AddOrderBy_DeleteType_Asc() { regOBA("DELETE_TYPE");return this; }
        public BsTDeleteDataCQ AddOrderBy_DeleteType_Desc() { regOBD("DELETE_TYPE");return this; }

        protected ConditionValue _conditionDiv;
        public ConditionValue ConditionDiv {
            get { if (_conditionDiv == null) { _conditionDiv = new ConditionValue(); } return _conditionDiv; }
        }
        protected override ConditionValue getCValueConditionDiv() { return this.ConditionDiv; }


        public BsTDeleteDataCQ AddOrderBy_ConditionDiv_Asc() { regOBA("CONDITION_DIV");return this; }
        public BsTDeleteDataCQ AddOrderBy_ConditionDiv_Desc() { regOBD("CONDITION_DIV");return this; }

        public BsTDeleteDataCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDeleteDataCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDeleteDataCQ baseQuery = (TDeleteDataCQ)baseQueryAsSuper;
            TDeleteDataCQ unionQuery = (TDeleteDataCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_DELETE_DATA", "tDataEditList");
        }
        public bool hasConditionQueryTDataEditList() {
            return _conditionQueryTDataEditList != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDeleteDataCQ> _scalarSubQueryMap;
	    public Map<String, TDeleteDataCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDeleteDataCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDeleteDataCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDeleteDataCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDeleteDataCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDeleteDataCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
