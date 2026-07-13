
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTFaListAddItemCQ : AbstractBsTFaListAddItemCQ {

        protected TFaListAddItemCIQ _inlineQuery;

        public BsTFaListAddItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TFaListAddItemCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TFaListAddItemCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TFaListAddItemCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TFaListAddItemCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _faListAddItemId;
        public ConditionValue FaListAddItemId {
            get { if (_faListAddItemId == null) { _faListAddItemId = new ConditionValue(); } return _faListAddItemId; }
        }
        protected override ConditionValue getCValueFaListAddItemId() { return this.FaListAddItemId; }


        public BsTFaListAddItemCQ AddOrderBy_FaListAddItemId_Asc() { regOBA("FA_LIST_ADD_ITEM_ID");return this; }
        public BsTFaListAddItemCQ AddOrderBy_FaListAddItemId_Desc() { regOBD("FA_LIST_ADD_ITEM_ID");return this; }

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

        public BsTFaListAddItemCQ AddOrderBy_FaScenarioHeaderId_Asc() { regOBA("FA_SCENARIO_HEADER_ID");return this; }
        public BsTFaListAddItemCQ AddOrderBy_FaScenarioHeaderId_Desc() { regOBD("FA_SCENARIO_HEADER_ID");return this; }

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

        public BsTFaListAddItemCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTFaListAddItemCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTFaListAddItemCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTFaListAddItemCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _lv2title;
        public ConditionValue Lv2title {
            get { if (_lv2title == null) { _lv2title = new ConditionValue(); } return _lv2title; }
        }
        protected override ConditionValue getCValueLv2title() { return this.Lv2title; }


        public BsTFaListAddItemCQ AddOrderBy_Lv2title_Asc() { regOBA("LV2TITLE");return this; }
        public BsTFaListAddItemCQ AddOrderBy_Lv2title_Desc() { regOBD("LV2TITLE");return this; }

        public BsTFaListAddItemCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTFaListAddItemCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TFaListAddItemCQ baseQuery = (TFaListAddItemCQ)baseQueryAsSuper;
            TFaListAddItemCQ unionQuery = (TFaListAddItemCQ)unionQueryAsSuper;
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
            return resolveNextRelationPath("T_FA_LIST_ADD_ITEM", "tFaScenarioHeader");
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
            joinOnMap.put("ITEM_INFO_ID", "ITEM_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTItemInfo() {
            return resolveNextRelationPath("T_FA_LIST_ADD_ITEM", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TFaListAddItemCQ> _scalarSubQueryMap;
	    public Map<String, TFaListAddItemCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TFaListAddItemCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TFaListAddItemCQ> _myselfInScopeSubQueryMap;
        public Map<String, TFaListAddItemCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TFaListAddItemCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
