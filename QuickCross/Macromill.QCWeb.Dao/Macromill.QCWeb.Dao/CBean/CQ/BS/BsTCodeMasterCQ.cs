
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTCodeMasterCQ : AbstractBsTCodeMasterCQ {

        protected TCodeMasterCIQ _inlineQuery;

        public BsTCodeMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TCodeMasterCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TCodeMasterCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TCodeMasterCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TCodeMasterCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _codeMasterId;
        public ConditionValue CodeMasterId {
            get { if (_codeMasterId == null) { _codeMasterId = new ConditionValue(); } return _codeMasterId; }
        }
        protected override ConditionValue getCValueCodeMasterId() { return this.CodeMasterId; }


        public BsTCodeMasterCQ AddOrderBy_CodeMasterId_Asc() { regOBA("CODE_MASTER_ID");return this; }
        public BsTCodeMasterCQ AddOrderBy_CodeMasterId_Desc() { regOBD("CODE_MASTER_ID");return this; }

        protected ConditionValue _groupKey;
        public ConditionValue GroupKey {
            get { if (_groupKey == null) { _groupKey = new ConditionValue(); } return _groupKey; }
        }
        protected override ConditionValue getCValueGroupKey() { return this.GroupKey; }


        public BsTCodeMasterCQ AddOrderBy_GroupKey_Asc() { regOBA("GROUP_KEY");return this; }
        public BsTCodeMasterCQ AddOrderBy_GroupKey_Desc() { regOBD("GROUP_KEY");return this; }

        protected ConditionValue _codeValue;
        public ConditionValue CodeValue {
            get { if (_codeValue == null) { _codeValue = new ConditionValue(); } return _codeValue; }
        }
        protected override ConditionValue getCValueCodeValue() { return this.CodeValue; }


        public BsTCodeMasterCQ AddOrderBy_CodeValue_Asc() { regOBA("CODE_VALUE");return this; }
        public BsTCodeMasterCQ AddOrderBy_CodeValue_Desc() { regOBD("CODE_VALUE");return this; }

        protected ConditionValue _messageId;
        public ConditionValue MessageId {
            get { if (_messageId == null) { _messageId = new ConditionValue(); } return _messageId; }
        }
        protected override ConditionValue getCValueMessageId() { return this.MessageId; }


        public BsTCodeMasterCQ AddOrderBy_MessageId_Asc() { regOBA("MESSAGE_ID");return this; }
        public BsTCodeMasterCQ AddOrderBy_MessageId_Desc() { regOBD("MESSAGE_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTCodeMasterCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTCodeMasterCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        public BsTCodeMasterCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTCodeMasterCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TCodeMasterCQ> _scalarSubQueryMap;
	    public Map<String, TCodeMasterCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TCodeMasterCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TCodeMasterCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TCodeMasterCQ> _myselfInScopeSubQueryMap;
        public Map<String, TCodeMasterCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TCodeMasterCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TCodeMasterCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
