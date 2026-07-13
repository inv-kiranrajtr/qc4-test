
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDeleteSampleIdListCQ : AbstractBsTDeleteSampleIdListCQ {

        protected TDeleteSampleIdListCIQ _inlineQuery;

        public BsTDeleteSampleIdListCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDeleteSampleIdListCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDeleteSampleIdListCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDeleteSampleIdListCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDeleteSampleIdListCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _deleteSampleId;
        public ConditionValue DeleteSampleId {
            get { if (_deleteSampleId == null) { _deleteSampleId = new ConditionValue(); } return _deleteSampleId; }
        }
        protected override ConditionValue getCValueDeleteSampleId() { return this.DeleteSampleId; }


        public BsTDeleteSampleIdListCQ AddOrderBy_DeleteSampleId_Asc() { regOBA("DELETE_SAMPLE_ID");return this; }
        public BsTDeleteSampleIdListCQ AddOrderBy_DeleteSampleId_Desc() { regOBD("DELETE_SAMPLE_ID");return this; }

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

        public BsTDeleteSampleIdListCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDeleteSampleIdListCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        protected ConditionValue _deleteSampleIdText;
        public ConditionValue DeleteSampleIdText {
            get { if (_deleteSampleIdText == null) { _deleteSampleIdText = new ConditionValue(); } return _deleteSampleIdText; }
        }
        protected override ConditionValue getCValueDeleteSampleIdText() { return this.DeleteSampleIdText; }


        public BsTDeleteSampleIdListCQ AddOrderBy_DeleteSampleIdText_Asc() { regOBA("DELETE_SAMPLE_ID_TEXT");return this; }
        public BsTDeleteSampleIdListCQ AddOrderBy_DeleteSampleIdText_Desc() { regOBD("DELETE_SAMPLE_ID_TEXT");return this; }

        public BsTDeleteSampleIdListCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDeleteSampleIdListCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDeleteSampleIdListCQ baseQuery = (TDeleteSampleIdListCQ)baseQueryAsSuper;
            TDeleteSampleIdListCQ unionQuery = (TDeleteSampleIdListCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_DELETE_SAMPLE_ID_LIST", "tDeleteData");
        }
        public bool hasConditionQueryTDeleteData() {
            return _conditionQueryTDeleteData != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDeleteSampleIdListCQ> _scalarSubQueryMap;
	    public Map<String, TDeleteSampleIdListCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDeleteSampleIdListCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDeleteSampleIdListCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDeleteSampleIdListCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDeleteSampleIdListCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDeleteSampleIdListCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
