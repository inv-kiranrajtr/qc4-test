
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTMaintenanceCQ : AbstractBsTMaintenanceCQ {

        protected TMaintenanceCIQ _inlineQuery;

        public BsTMaintenanceCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TMaintenanceCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TMaintenanceCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TMaintenanceCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TMaintenanceCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _maintenanceId;
        public ConditionValue MaintenanceId {
            get { if (_maintenanceId == null) { _maintenanceId = new ConditionValue(); } return _maintenanceId; }
        }
        protected override ConditionValue getCValueMaintenanceId() { return this.MaintenanceId; }


        public BsTMaintenanceCQ AddOrderBy_MaintenanceId_Asc() { regOBA("MAINTENANCE_ID");return this; }
        public BsTMaintenanceCQ AddOrderBy_MaintenanceId_Desc() { regOBD("MAINTENANCE_ID");return this; }

        protected ConditionValue _limitTime;
        public ConditionValue LimitTime {
            get { if (_limitTime == null) { _limitTime = new ConditionValue(); } return _limitTime; }
        }
        protected override ConditionValue getCValueLimitTime() { return this.LimitTime; }


        public BsTMaintenanceCQ AddOrderBy_LimitTime_Asc() { regOBA("LIMIT_TIME");return this; }
        public BsTMaintenanceCQ AddOrderBy_LimitTime_Desc() { regOBD("LIMIT_TIME");return this; }

        public BsTMaintenanceCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTMaintenanceCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TMaintenanceCQ> _scalarSubQueryMap;
	    public Map<String, TMaintenanceCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TMaintenanceCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TMaintenanceCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TMaintenanceCQ> _myselfInScopeSubQueryMap;
        public Map<String, TMaintenanceCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TMaintenanceCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TMaintenanceCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
