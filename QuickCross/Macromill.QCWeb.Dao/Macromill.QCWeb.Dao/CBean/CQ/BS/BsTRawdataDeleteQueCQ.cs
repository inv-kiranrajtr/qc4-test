
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTRawdataDeleteQueCQ : AbstractBsTRawdataDeleteQueCQ {

        protected TRawdataDeleteQueCIQ _inlineQuery;

        public BsTRawdataDeleteQueCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TRawdataDeleteQueCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TRawdataDeleteQueCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TRawdataDeleteQueCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TRawdataDeleteQueCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _rawdataDeleteQueId;
        public ConditionValue RawdataDeleteQueId {
            get { if (_rawdataDeleteQueId == null) { _rawdataDeleteQueId = new ConditionValue(); } return _rawdataDeleteQueId; }
        }
        protected override ConditionValue getCValueRawdataDeleteQueId() { return this.RawdataDeleteQueId; }


        public BsTRawdataDeleteQueCQ AddOrderBy_RawdataDeleteQueId_Asc() { regOBA("RAWDATA_DELETE_QUE_ID");return this; }
        public BsTRawdataDeleteQueCQ AddOrderBy_RawdataDeleteQueId_Desc() { regOBD("RAWDATA_DELETE_QUE_ID");return this; }

        protected ConditionValue _addDataNo;
        public ConditionValue AddDataNo {
            get { if (_addDataNo == null) { _addDataNo = new ConditionValue(); } return _addDataNo; }
        }
        protected override ConditionValue getCValueAddDataNo() { return this.AddDataNo; }


        public BsTRawdataDeleteQueCQ AddOrderBy_AddDataNo_Asc() { regOBA("ADD_DATA_NO");return this; }
        public BsTRawdataDeleteQueCQ AddOrderBy_AddDataNo_Desc() { regOBD("ADD_DATA_NO");return this; }

        protected ConditionValue _qcwebJobNo;
        public ConditionValue QcwebJobNo {
            get { if (_qcwebJobNo == null) { _qcwebJobNo = new ConditionValue(); } return _qcwebJobNo; }
        }
        protected override ConditionValue getCValueQcwebJobNo() { return this.QcwebJobNo; }


        public BsTRawdataDeleteQueCQ AddOrderBy_QcwebJobNo_Asc() { regOBA("QCWEB_JOB_NO");return this; }
        public BsTRawdataDeleteQueCQ AddOrderBy_QcwebJobNo_Desc() { regOBD("QCWEB_JOB_NO");return this; }

        protected ConditionValue _mainSurveyId;
        public ConditionValue MainSurveyId {
            get { if (_mainSurveyId == null) { _mainSurveyId = new ConditionValue(); } return _mainSurveyId; }
        }
        protected override ConditionValue getCValueMainSurveyId() { return this.MainSurveyId; }


        public BsTRawdataDeleteQueCQ AddOrderBy_MainSurveyId_Asc() { regOBA("MAIN_SURVEY_ID");return this; }
        public BsTRawdataDeleteQueCQ AddOrderBy_MainSurveyId_Desc() { regOBD("MAIN_SURVEY_ID");return this; }

        protected ConditionValue _deleteOrderDate;
        public ConditionValue DeleteOrderDate {
            get { if (_deleteOrderDate == null) { _deleteOrderDate = new ConditionValue(); } return _deleteOrderDate; }
        }
        protected override ConditionValue getCValueDeleteOrderDate() { return this.DeleteOrderDate; }


        public BsTRawdataDeleteQueCQ AddOrderBy_DeleteOrderDate_Asc() { regOBA("DELETE_ORDER_DATE");return this; }
        public BsTRawdataDeleteQueCQ AddOrderBy_DeleteOrderDate_Desc() { regOBD("DELETE_ORDER_DATE");return this; }

        protected ConditionValue _deleteStatus;
        public ConditionValue DeleteStatus {
            get { if (_deleteStatus == null) { _deleteStatus = new ConditionValue(); } return _deleteStatus; }
        }
        protected override ConditionValue getCValueDeleteStatus() { return this.DeleteStatus; }


        public BsTRawdataDeleteQueCQ AddOrderBy_DeleteStatus_Asc() { regOBA("DELETE_STATUS");return this; }
        public BsTRawdataDeleteQueCQ AddOrderBy_DeleteStatus_Desc() { regOBD("DELETE_STATUS");return this; }

        public BsTRawdataDeleteQueCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTRawdataDeleteQueCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TRawdataDeleteQueCQ> _scalarSubQueryMap;
	    public Map<String, TRawdataDeleteQueCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TRawdataDeleteQueCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TRawdataDeleteQueCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TRawdataDeleteQueCQ> _myselfInScopeSubQueryMap;
        public Map<String, TRawdataDeleteQueCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TRawdataDeleteQueCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TRawdataDeleteQueCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
