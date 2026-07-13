
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputWpMasterCQ : AbstractBsTOutputWpMasterCQ {

        protected TOutputWpMasterCIQ _inlineQuery;

        public BsTOutputWpMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputWpMasterCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputWpMasterCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputWpMasterCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputWpMasterCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputWpMasterId;
        public ConditionValue OutputWpMasterId {
            get { if (_outputWpMasterId == null) { _outputWpMasterId = new ConditionValue(); } return _outputWpMasterId; }
        }
        protected override ConditionValue getCValueOutputWpMasterId() { return this.OutputWpMasterId; }


        public BsTOutputWpMasterCQ AddOrderBy_OutputWpMasterId_Asc() { regOBA("OUTPUT_WP_MASTER_ID");return this; }
        public BsTOutputWpMasterCQ AddOrderBy_OutputWpMasterId_Desc() { regOBD("OUTPUT_WP_MASTER_ID");return this; }

        protected ConditionValue _point;
        public ConditionValue Point {
            get { if (_point == null) { _point = new ConditionValue(); } return _point; }
        }
        protected override ConditionValue getCValuePoint() { return this.Point; }


        public BsTOutputWpMasterCQ AddOrderBy_Point_Asc() { regOBA("POINT");return this; }
        public BsTOutputWpMasterCQ AddOrderBy_Point_Desc() { regOBD("POINT");return this; }

        public BsTOutputWpMasterCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputWpMasterCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputWpMasterCQ> _scalarSubQueryMap;
	    public Map<String, TOutputWpMasterCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputWpMasterCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputWpMasterCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputWpMasterCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputWpMasterCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputWpMasterCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputWpMasterCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
