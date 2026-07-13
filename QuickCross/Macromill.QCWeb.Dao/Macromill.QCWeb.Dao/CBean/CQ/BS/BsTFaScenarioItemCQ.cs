
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTFaScenarioItemCQ : AbstractBsTFaScenarioItemCQ {

        protected TFaScenarioItemCIQ _inlineQuery;

        public BsTFaScenarioItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TFaScenarioItemCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TFaScenarioItemCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TFaScenarioItemCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TFaScenarioItemCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _faScenarioItemId;
        public ConditionValue FaScenarioItemId {
            get { if (_faScenarioItemId == null) { _faScenarioItemId = new ConditionValue(); } return _faScenarioItemId; }
        }
        protected override ConditionValue getCValueFaScenarioItemId() { return this.FaScenarioItemId; }


        public BsTFaScenarioItemCQ AddOrderBy_FaScenarioItemId_Asc() { regOBA("FA_SCENARIO_ITEM_ID");return this; }
        public BsTFaScenarioItemCQ AddOrderBy_FaScenarioItemId_Desc() { regOBD("FA_SCENARIO_ITEM_ID");return this; }

        protected ConditionValue _faScenarioHeaderId;
        public ConditionValue FaScenarioHeaderId {
            get { if (_faScenarioHeaderId == null) { _faScenarioHeaderId = new ConditionValue(); } return _faScenarioHeaderId; }
        }
        protected override ConditionValue getCValueFaScenarioHeaderId() { return this.FaScenarioHeaderId; }


        protected Map<String, TFaScenarioHeaderCQ> _faScenarioHeaderId_InScopeSubQuery_TFaScenarioHeaderMap;
        public Map<String, TFaScenarioHeaderCQ> FaScenarioHeaderId_InScopeSubQuery_TFaScenarioHeader { get { return _faScenarioHeaderId_InScopeSubQuery_TFaScenarioHeaderMap; }}
        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioHeader(TFaScenarioHeaderCQ subQuery) {
            if (_faScenarioHeaderId_InScopeSubQuery_TFaScenarioHeaderMap == null) { _faScenarioHeaderId_InScopeSubQuery_TFaScenarioHeaderMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_InScopeSubQuery_TFaScenarioHeaderMap.size() + 1);
            _faScenarioHeaderId_InScopeSubQuery_TFaScenarioHeaderMap.put(key, subQuery); return "FaScenarioHeaderId_InScopeSubQuery_TFaScenarioHeader." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeaderMap;
        public Map<String, TFaScenarioHeaderCQ> FaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeader { get { return _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeaderMap; }}
        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeader(TFaScenarioHeaderCQ subQuery) {
            if (_faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeaderMap == null) { _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeaderMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeaderMap.size() + 1);
            _faScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeaderMap.put(key, subQuery); return "FaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeader." + key;
        }

        public BsTFaScenarioItemCQ AddOrderBy_FaScenarioHeaderId_Asc() { regOBA("FA_SCENARIO_HEADER_ID");return this; }
        public BsTFaScenarioItemCQ AddOrderBy_FaScenarioHeaderId_Desc() { regOBD("FA_SCENARIO_HEADER_ID");return this; }

        protected ConditionValue _faTargetItemId;
        public ConditionValue FaTargetItemId {
            get { if (_faTargetItemId == null) { _faTargetItemId = new ConditionValue(); } return _faTargetItemId; }
        }
        protected override ConditionValue getCValueFaTargetItemId() { return this.FaTargetItemId; }


        protected Map<String, TItemInfoCQ> _faTargetItemId_InScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> FaTargetItemId_InScopeSubQuery_TItemInfo { get { return _faTargetItemId_InScopeSubQuery_TItemInfoMap; }}
        public override String keepFaTargetItemId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_faTargetItemId_InScopeSubQuery_TItemInfoMap == null) { _faTargetItemId_InScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_faTargetItemId_InScopeSubQuery_TItemInfoMap.size() + 1);
            _faTargetItemId_InScopeSubQuery_TItemInfoMap.put(key, subQuery); return "FaTargetItemId_InScopeSubQuery_TItemInfo." + key;
        }

        protected Map<String, TItemInfoCQ> _faTargetItemId_NotInScopeSubQuery_TItemInfoMap;
        public Map<String, TItemInfoCQ> FaTargetItemId_NotInScopeSubQuery_TItemInfo { get { return _faTargetItemId_NotInScopeSubQuery_TItemInfoMap; }}
        public override String keepFaTargetItemId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            if (_faTargetItemId_NotInScopeSubQuery_TItemInfoMap == null) { _faTargetItemId_NotInScopeSubQuery_TItemInfoMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_faTargetItemId_NotInScopeSubQuery_TItemInfoMap.size() + 1);
            _faTargetItemId_NotInScopeSubQuery_TItemInfoMap.put(key, subQuery); return "FaTargetItemId_NotInScopeSubQuery_TItemInfo." + key;
        }

        public BsTFaScenarioItemCQ AddOrderBy_FaTargetItemId_Asc() { regOBA("FA_TARGET_ITEM_ID");return this; }
        public BsTFaScenarioItemCQ AddOrderBy_FaTargetItemId_Desc() { regOBD("FA_TARGET_ITEM_ID");return this; }

        protected ConditionValue _titleString;
        public ConditionValue TitleString {
            get { if (_titleString == null) { _titleString = new ConditionValue(); } return _titleString; }
        }
        protected override ConditionValue getCValueTitleString() { return this.TitleString; }


        public BsTFaScenarioItemCQ AddOrderBy_TitleString_Asc() { regOBA("TITLE_STRING");return this; }
        public BsTFaScenarioItemCQ AddOrderBy_TitleString_Desc() { regOBD("TITLE_STRING");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTFaScenarioItemCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTFaScenarioItemCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        public BsTFaScenarioItemCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTFaScenarioItemCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TFaScenarioItemCQ baseQuery = (TFaScenarioItemCQ)baseQueryAsSuper;
            TFaScenarioItemCQ unionQuery = (TFaScenarioItemCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTFaScenarioHeader()) {
                unionQuery.QueryTFaScenarioHeader().reflectRelationOnUnionQuery(baseQuery.QueryTFaScenarioHeader(), unionQuery.QueryTFaScenarioHeader());
            }
            if (baseQuery.hasConditionQueryTItemInfo()) {
                unionQuery.QueryTItemInfo().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfo(), unionQuery.QueryTItemInfo());
            }

        }
    
        protected TFaScenarioHeaderCQ _conditionQueryTFaScenarioHeader;
        public TFaScenarioHeaderCQ QueryTFaScenarioHeader() {
            return this.ConditionQueryTFaScenarioHeader;
        }
        public TFaScenarioHeaderCQ ConditionQueryTFaScenarioHeader {
            get {
                if (_conditionQueryTFaScenarioHeader == null) {
                    _conditionQueryTFaScenarioHeader = xcreateQueryTFaScenarioHeader();
                    xsetupOuterJoin_TFaScenarioHeader();
                }
                return _conditionQueryTFaScenarioHeader;
            }
        }
        protected TFaScenarioHeaderCQ xcreateQueryTFaScenarioHeader() {
            String nrp = resolveNextRelationPathTFaScenarioHeader();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TFaScenarioHeaderCQ cq = new TFaScenarioHeaderCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tFaScenarioHeader"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TFaScenarioHeader() {
            TFaScenarioHeaderCQ cq = ConditionQueryTFaScenarioHeader;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTFaScenarioHeader() {
            return resolveNextRelationPath("T_FA_SCENARIO_ITEM", "tFaScenarioHeader");
        }
        public bool hasConditionQueryTFaScenarioHeader() {
            return _conditionQueryTFaScenarioHeader != null;
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
            joinOnMap.put("FA_TARGET_ITEM_ID", "ITEM_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfo() {
            return resolveNextRelationPath("T_FA_SCENARIO_ITEM", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TFaScenarioItemCQ> _scalarSubQueryMap;
	    public Map<String, TFaScenarioItemCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TFaScenarioItemCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TFaScenarioItemCQ> _myselfInScopeSubQueryMap;
        public Map<String, TFaScenarioItemCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TFaScenarioItemCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
