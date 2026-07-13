
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTScenarioQuerylistCQ : AbstractBsTScenarioQuerylistCQ {

        protected TScenarioQuerylistCIQ _inlineQuery;

        public BsTScenarioQuerylistCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TScenarioQuerylistCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TScenarioQuerylistCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TScenarioQuerylistCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TScenarioQuerylistCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _scenarioQuerylistId;
        public ConditionValue ScenarioQuerylistId {
            get { if (_scenarioQuerylistId == null) { _scenarioQuerylistId = new ConditionValue(); } return _scenarioQuerylistId; }
        }
        protected override ConditionValue getCValueScenarioQuerylistId() { return this.ScenarioQuerylistId; }


        public BsTScenarioQuerylistCQ AddOrderBy_ScenarioQuerylistId_Asc() { regOBA("SCENARIO_QUERYLIST_ID");return this; }
        public BsTScenarioQuerylistCQ AddOrderBy_ScenarioQuerylistId_Desc() { regOBD("SCENARIO_QUERYLIST_ID");return this; }

        protected ConditionValue _scenarioTotalizationId;
        public ConditionValue ScenarioTotalizationId {
            get { if (_scenarioTotalizationId == null) { _scenarioTotalizationId = new ConditionValue(); } return _scenarioTotalizationId; }
        }
        protected override ConditionValue getCValueScenarioTotalizationId() { return this.ScenarioTotalizationId; }


        protected Map<String, TScenarioTotalizationCQ> _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap;
        public Map<String, TScenarioTotalizationCQ> ScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization { get { return _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap == null) { _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TScenarioTotalizationMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap;
        public Map<String, TScenarioTotalizationCQ> ScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization { get { return _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalizationMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization." + key;
        }

        public BsTScenarioQuerylistCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTScenarioQuerylistCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _seqNo;
        public ConditionValue SeqNo {
            get { if (_seqNo == null) { _seqNo = new ConditionValue(); } return _seqNo; }
        }
        protected override ConditionValue getCValueSeqNo() { return this.SeqNo; }


        public BsTScenarioQuerylistCQ AddOrderBy_SeqNo_Asc() { regOBA("SEQ_NO");return this; }
        public BsTScenarioQuerylistCQ AddOrderBy_SeqNo_Desc() { regOBD("SEQ_NO");return this; }

        protected ConditionValue _itemInfoId;
        public ConditionValue ItemInfoId {
            get { if (_itemInfoId == null) { _itemInfoId = new ConditionValue(); } return _itemInfoId; }
        }
        protected override ConditionValue getCValueItemInfoId() { return this.ItemInfoId; }


        protected Map<String, TItemInfoCQ> _itemInfoId_InScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> ItemInfoId_InScopeSubQuery_TItemInfo { get { return _itemInfoId_InScopeSubQuery_TItemInfoMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TItemInfoMap == null) { _itemInfoId_InScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TItemInfoMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TItemInfoMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TItemInfo." + key;
        }

        protected Map<String, TItemInfoCQ> _itemInfoId_NotInScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> ItemInfoId_NotInScopeSubQuery_TItemInfo { get { return _itemInfoId_NotInScopeSubQuery_TItemInfoMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TItemInfoMap == null) { _itemInfoId_NotInScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TItemInfoMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TItemInfoMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TItemInfo." + key;
        }

        public BsTScenarioQuerylistCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTScenarioQuerylistCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

        protected ConditionValue _operationCode;
        public ConditionValue OperationCode {
            get { if (_operationCode == null) { _operationCode = new ConditionValue(); } return _operationCode; }
        }
        protected override ConditionValue getCValueOperationCode() { return this.OperationCode; }


        public BsTScenarioQuerylistCQ AddOrderBy_OperationCode_Asc() { regOBA("OPERATION_CODE");return this; }
        public BsTScenarioQuerylistCQ AddOrderBy_OperationCode_Desc() { regOBD("OPERATION_CODE");return this; }

        protected ConditionValue _conditionString;
        public ConditionValue ConditionString {
            get { if (_conditionString == null) { _conditionString = new ConditionValue(); } return _conditionString; }
        }
        protected override ConditionValue getCValueConditionString() { return this.ConditionString; }


        public BsTScenarioQuerylistCQ AddOrderBy_ConditionString_Asc() { regOBA("CONDITION_STRING");return this; }
        public BsTScenarioQuerylistCQ AddOrderBy_ConditionString_Desc() { regOBD("CONDITION_STRING");return this; }

        public BsTScenarioQuerylistCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTScenarioQuerylistCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TScenarioQuerylistCQ baseQuery = (TScenarioQuerylistCQ)baseQueryAsSuper;
            TScenarioQuerylistCQ unionQuery = (TScenarioQuerylistCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTItemInfo()) {
                unionQuery.QueryTItemInfo().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfo(), unionQuery.QueryTItemInfo());
            }

        }
    
        protected TScenarioTotalizationCQ _conditionQueryTScenarioTotalization;
        public TScenarioTotalizationCQ QueryTScenarioTotalization() {
            return this.ConditionQueryTScenarioTotalization;
        }
        public TScenarioTotalizationCQ ConditionQueryTScenarioTotalization {
            get {
                if (_conditionQueryTScenarioTotalization == null) {
                    _conditionQueryTScenarioTotalization = xcreateQueryTScenarioTotalization();
                    xsetupOuterJoin_TScenarioTotalization();
                }
                return _conditionQueryTScenarioTotalization;
            }
        }
        protected TScenarioTotalizationCQ xcreateQueryTScenarioTotalization() {
            String nrp = resolveNextRelationPathTScenarioTotalization();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TScenarioTotalizationCQ cq = new TScenarioTotalizationCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tScenarioTotalization"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TScenarioTotalization() {
            TScenarioTotalizationCQ cq = ConditionQueryTScenarioTotalization;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTScenarioTotalization() {
            return resolveNextRelationPath("T_SCENARIO_QUERYLIST", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TItemInfoCQ _conditionQueryTItemInfo;
        public TItemInfoCQ QueryTItemInfo() {
            return this.ConditionQueryTItemInfo;
        }
        public TItemInfoCQ ConditionQueryTItemInfo {
            get {
                if (_conditionQueryTItemInfo == null) {
                    _conditionQueryTItemInfo = xcreateQueryTItemInfo();
                    xsetupOuterJoin_TItemInfo();
                }
                return _conditionQueryTItemInfo;
            }
        }
        protected TItemInfoCQ xcreateQueryTItemInfo() {
            String nrp = resolveNextRelationPathTItemInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TItemInfoCQ cq = new TItemInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tItemInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TItemInfo() {
            TItemInfoCQ cq = ConditionQueryTItemInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ITEM_INFO_ID", "Item_Info_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfo() {
            return resolveNextRelationPath("T_SCENARIO_QUERYLIST", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TScenarioQuerylistCQ> _scalarSubQueryMap;
	    public Map<String, TScenarioQuerylistCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TScenarioQuerylistCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TScenarioQuerylistCQ> _myselfInScopeSubQueryMap;
        public Map<String, TScenarioQuerylistCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TScenarioQuerylistCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
