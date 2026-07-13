
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputHistoryItemCQ : AbstractBsTOutputHistoryItemCQ {

        protected TOutputHistoryItemCIQ _inlineQuery;

        public BsTOutputHistoryItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputHistoryItemCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputHistoryItemCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputHistoryItemCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputHistoryItemCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputHistoryItemId;
        public ConditionValue OutputHistoryItemId {
            get { if (_outputHistoryItemId == null) { _outputHistoryItemId = new ConditionValue(); } return _outputHistoryItemId; }
        }
        protected override ConditionValue getCValueOutputHistoryItemId() { return this.OutputHistoryItemId; }


        public BsTOutputHistoryItemCQ AddOrderBy_OutputHistoryItemId_Asc() { regOBA("OUTPUT_HISTORY_ITEM_ID");return this; }
        public BsTOutputHistoryItemCQ AddOrderBy_OutputHistoryItemId_Desc() { regOBD("OUTPUT_HISTORY_ITEM_ID");return this; }

        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TOutputSettingCQ> _qcwebid_InScopeSubQuery_TOutputSettingMap;
        public Map<String, TOutputSettingCQ> Qcwebid_InScopeSubQuery_TOutputSetting { get { return _qcwebid_InScopeSubQuery_TOutputSettingMap; }}
        public override String keepQcwebid_InScopeSubQuery_TOutputSetting(TOutputSettingCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TOutputSettingMap == null) { _qcwebid_InScopeSubQuery_TOutputSettingMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TOutputSettingMap.size() + 1);
            _qcwebid_InScopeSubQuery_TOutputSettingMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TOutputSetting." + key;
        }

        protected Map<String, TOutputSettingCQ> _qcwebid_NotInScopeSubQuery_TOutputSettingMap;
        public Map<String, TOutputSettingCQ> Qcwebid_NotInScopeSubQuery_TOutputSetting { get { return _qcwebid_NotInScopeSubQuery_TOutputSettingMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TOutputSetting(TOutputSettingCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TOutputSettingMap == null) { _qcwebid_NotInScopeSubQuery_TOutputSettingMap = new LinkedHashMap<String, TOutputSettingCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TOutputSettingMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TOutputSettingMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TOutputSetting." + key;
        }

        public BsTOutputHistoryItemCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputHistoryItemCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _itemInfoId;
        public ConditionValue ItemInfoId {
            get { if (_itemInfoId == null) { _itemInfoId = new ConditionValue(); } return _itemInfoId; }
        }
        protected override ConditionValue getCValueItemInfoId() { return this.ItemInfoId; }


        public BsTOutputHistoryItemCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTOutputHistoryItemCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTOutputHistoryItemCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTOutputHistoryItemCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _outputFlag;
        public ConditionValue OutputFlag {
            get { if (_outputFlag == null) { _outputFlag = new ConditionValue(); } return _outputFlag; }
        }
        protected override ConditionValue getCValueOutputFlag() { return this.OutputFlag; }


        public BsTOutputHistoryItemCQ AddOrderBy_OutputFlag_Asc() { regOBA("OUTPUT_FLAG");return this; }
        public BsTOutputHistoryItemCQ AddOrderBy_OutputFlag_Desc() { regOBD("OUTPUT_FLAG");return this; }

        public BsTOutputHistoryItemCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputHistoryItemCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputHistoryItemCQ baseQuery = (TOutputHistoryItemCQ)baseQueryAsSuper;
            TOutputHistoryItemCQ unionQuery = (TOutputHistoryItemCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTOutputSetting()) {
                unionQuery.QueryTOutputSetting().reflectRelationOnUnionQuery(baseQuery.QueryTOutputSetting(), unionQuery.QueryTOutputSetting());
            }

        }
    
        protected TOutputSettingCQ _conditionQueryTOutputSetting;
        public TOutputSettingCQ QueryTOutputSetting() {
            return this.ConditionQueryTOutputSetting;
        }
        public TOutputSettingCQ ConditionQueryTOutputSetting {
            get {
                if (_conditionQueryTOutputSetting == null) {
                    _conditionQueryTOutputSetting = xcreateQueryTOutputSetting();
                    xsetupOuterJoin_TOutputSetting();
                }
                return _conditionQueryTOutputSetting;
            }
        }
        protected TOutputSettingCQ xcreateQueryTOutputSetting() {
            String nrp = resolveNextRelationPathTOutputSetting();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputSettingCQ cq = new TOutputSettingCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputSetting"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputSetting() {
            TOutputSettingCQ cq = ConditionQueryTOutputSetting;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputSetting() {
            return resolveNextRelationPath("T_OUTPUT_HISTORY_ITEM", "tOutputSetting");
        }
        public bool hasConditionQueryTOutputSetting() {
            return _conditionQueryTOutputSetting != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputHistoryItemCQ> _scalarSubQueryMap;
	    public Map<String, TOutputHistoryItemCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputHistoryItemCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputHistoryItemCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputHistoryItemCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputHistoryItemCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputHistoryItemCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
