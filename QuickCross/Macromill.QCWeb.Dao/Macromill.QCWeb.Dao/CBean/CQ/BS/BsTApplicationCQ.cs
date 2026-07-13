
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTApplicationCQ : AbstractBsTApplicationCQ {

        protected TApplicationCIQ _inlineQuery;

        public BsTApplicationCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TApplicationCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TApplicationCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TApplicationCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TApplicationCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _identifier;
        public ConditionValue Identifier {
            get { if (_identifier == null) { _identifier = new ConditionValue(); } return _identifier; }
        }
        protected override ConditionValue getCValueIdentifier() { return this.Identifier; }


        public BsTApplicationCQ AddOrderBy_Identifier_Asc() { regOBA("IDENTIFIER");return this; }
        public BsTApplicationCQ AddOrderBy_Identifier_Desc() { regOBD("IDENTIFIER");return this; }

        protected ConditionValue _settingValue;
        public ConditionValue SettingValue {
            get { if (_settingValue == null) { _settingValue = new ConditionValue(); } return _settingValue; }
        }
        protected override ConditionValue getCValueSettingValue() { return this.SettingValue; }


        public BsTApplicationCQ AddOrderBy_SettingValue_Asc() { regOBA("SETTING_VALUE");return this; }
        public BsTApplicationCQ AddOrderBy_SettingValue_Desc() { regOBD("SETTING_VALUE");return this; }

        protected ConditionValue _description;
        public ConditionValue Description {
            get { if (_description == null) { _description = new ConditionValue(); } return _description; }
        }
        protected override ConditionValue getCValueDescription() { return this.Description; }


        public BsTApplicationCQ AddOrderBy_Description_Asc() { regOBA("DESCRIPTION");return this; }
        public BsTApplicationCQ AddOrderBy_Description_Desc() { regOBD("DESCRIPTION");return this; }

        public BsTApplicationCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTApplicationCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TApplicationCQ> _scalarSubQueryMap;
	    public Map<String, TApplicationCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TApplicationCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TApplicationCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TApplicationCQ> _myselfInScopeSubQueryMap;
        public Map<String, TApplicationCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TApplicationCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TApplicationCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
