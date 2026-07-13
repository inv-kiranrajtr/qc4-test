
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTScenarioTotalizationCQ : AbstractBsTScenarioTotalizationCQ {

        protected TScenarioTotalizationCIQ _inlineQuery;

        public BsTScenarioTotalizationCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TScenarioTotalizationCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TScenarioTotalizationCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TScenarioTotalizationCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TScenarioTotalizationCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _scenarioTotalizationId;
        public ConditionValue ScenarioTotalizationId {
            get { if (_scenarioTotalizationId == null) { _scenarioTotalizationId = new ConditionValue(); } return _scenarioTotalizationId; }
        }
        protected override ConditionValue getCValueScenarioTotalizationId() { return this.ScenarioTotalizationId; }


        protected Map<String, TCategoryOutputEditCQ> _scenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditListMap;
        public Map<String, TCategoryOutputEditCQ> ScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList { get { return _scenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditListMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _scenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetListMap;
        public Map<String, TCrossScenarioTargetCQ> ScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList { get { return _scenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetListMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _scenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderListMap;
        public Map<String, TFaScenarioHeaderCQ> ScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList { get { return _scenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderListMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _scenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoListMap;
        public Map<String, TGtMatrixInfoCQ> ScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList { get { return _scenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoListMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_ExistsSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList { get { return _scenarioTotalizationId_ExistsSubQuery_TGtScenarioItemListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TGtScenarioItemListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TGtScenarioItemListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _scenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList { get { return _scenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TItemInfoCQ> _scenarioTotalizationId_ExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> ScenarioTotalizationId_ExistsSubQuery_TItemInfoList { get { return _scenarioTotalizationId_ExistsSubQuery_TItemInfoListMap; }}
        public override String keepScenarioTotalizationId_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_scenarioTotalizationId_ExistsSubQuery_TItemInfoListMap == null) { _scenarioTotalizationId_ExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_ExistsSubQuery_TItemInfoListMap.size() + 1);
            _scenarioTotalizationId_ExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_ExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TCategoryOutputEditCQ> _scenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditListMap;
        public Map<String, TCategoryOutputEditCQ> ScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList { get { return _scenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditListMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _scenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetListMap;
        public Map<String, TCrossScenarioTargetCQ> ScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList { get { return _scenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetListMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _scenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderListMap;
        public Map<String, TFaScenarioHeaderCQ> ScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList { get { return _scenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderListMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _scenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoListMap;
        public Map<String, TGtMatrixInfoCQ> ScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList { get { return _scenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoListMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList { get { return _scenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _scenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList { get { return _scenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TItemInfoCQ> _scenarioTotalizationId_NotExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> ScenarioTotalizationId_NotExistsSubQuery_TItemInfoList { get { return _scenarioTotalizationId_NotExistsSubQuery_TItemInfoListMap; }}
        public override String keepScenarioTotalizationId_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_scenarioTotalizationId_NotExistsSubQuery_TItemInfoListMap == null) { _scenarioTotalizationId_NotExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotExistsSubQuery_TItemInfoListMap.size() + 1);
            _scenarioTotalizationId_NotExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_NotExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem { get { return _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemMap == null) { _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem." + key;
        }

        protected Map<String, TCategoryOutputEditCQ> _scenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditListMap;
        public Map<String, TCategoryOutputEditCQ> ScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList { get { return _scenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditListMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _scenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetListMap;
        public Map<String, TCrossScenarioTargetCQ> ScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList { get { return _scenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetListMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _scenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderListMap;
        public Map<String, TFaScenarioHeaderCQ> ScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList { get { return _scenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderListMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _scenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoListMap;
        public Map<String, TGtMatrixInfoCQ> ScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList { get { return _scenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoListMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList { get { return _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _scenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList { get { return _scenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TItemInfoCQ> _scenarioTotalizationId_InScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> ScenarioTotalizationId_InScopeSubQuery_TItemInfoList { get { return _scenarioTotalizationId_InScopeSubQuery_TItemInfoListMap; }}
        public override String keepScenarioTotalizationId_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_scenarioTotalizationId_InScopeSubQuery_TItemInfoListMap == null) { _scenarioTotalizationId_InScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_InScopeSubQuery_TItemInfoListMap.size() + 1);
            _scenarioTotalizationId_InScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_InScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem { get { return _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem." + key;
        }

        protected Map<String, TCategoryOutputEditCQ> _scenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditListMap;
        public Map<String, TCategoryOutputEditCQ> ScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditListMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _scenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetListMap;
        public Map<String, TCrossScenarioTargetCQ> ScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetListMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _scenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderListMap;
        public Map<String, TFaScenarioHeaderCQ> ScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderListMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _scenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoListMap;
        public Map<String, TGtMatrixInfoCQ> ScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoListMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _scenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TItemInfoCQ> _scenarioTotalizationId_NotInScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> ScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList { get { return _scenarioTotalizationId_NotInScopeSubQuery_TItemInfoListMap; }}
        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_scenarioTotalizationId_NotInScopeSubQuery_TItemInfoListMap == null) { _scenarioTotalizationId_NotInScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_NotInScopeSubQuery_TItemInfoListMap.size() + 1);
            _scenarioTotalizationId_NotInScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TCategoryOutputEditCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditListMap;
        public Map<String, TCategoryOutputEditCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditListMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditList." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetListMap;
        public Map<String, TCrossScenarioTargetCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetListMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetList." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderListMap;
        public Map<String, TFaScenarioHeaderCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderListMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderList." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoListMap;
        public Map<String, TGtMatrixInfoCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoListMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistList." + key;
        }

        protected Map<String, TItemInfoCQ> _scenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> ScenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoList { get { return _scenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoListMap; }}
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_scenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoListMap == null) { _scenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoListMap.size() + 1);
            _scenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoList." + key;
        }

        protected Map<String, TCategoryOutputEditCQ> _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListMap;
        public Map<String, TCategoryOutputEditCQ> ScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListMap = new LinkedHashMap<String, TCategoryOutputEditCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameter." + key;
        }

        protected Map<String, TCrossScenarioTargetCQ> _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListMap;
        public Map<String, TCrossScenarioTargetCQ> ScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListMap = new LinkedHashMap<String, TCrossScenarioTargetCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameter." + key;
        }

        protected Map<String, TFaScenarioHeaderCQ> _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListMap;
        public Map<String, TFaScenarioHeaderCQ> ScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListMap = new LinkedHashMap<String, TFaScenarioHeaderCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameter." + key;
        }

        protected Map<String, TGtMatrixInfoCQ> _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListMap;
        public Map<String, TGtMatrixInfoCQ> ScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListMap = new LinkedHashMap<String, TGtMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameter." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameter." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameter." + key;
        }

        protected Map<String, TItemInfoCQ> _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> ScenarioTotalizationId_QueryDerivedReferrer_TItemInfoList { get { return _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "ScenarioTotalizationId_QueryDerivedReferrer_TItemInfoList." + key;
        }
        protected Map<String, Object> _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameterMap;
        public Map<String, Object> ScenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameter { get { return _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameterMap; } }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            if (_scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameterMap == null) { _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameterMap.size() + 1);
            _scenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameterMap.put(key, parameterValue); return "ScenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameter." + key;
        }

        public BsTScenarioTotalizationCQ AddOrderBy_ScenarioTotalizationId_Asc() { regOBA("SCENARIO_TOTALIZATION_ID");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_ScenarioTotalizationId_Desc() { regOBD("SCENARIO_TOTALIZATION_ID");return this; }

        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfo." + key;
        }

        public BsTScenarioTotalizationCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _scenarioType;
        public ConditionValue ScenarioType {
            get { if (_scenarioType == null) { _scenarioType = new ConditionValue(); } return _scenarioType; }
        }
        protected override ConditionValue getCValueScenarioType() { return this.ScenarioType; }


        public BsTScenarioTotalizationCQ AddOrderBy_ScenarioType_Asc() { regOBA("SCENARIO_TYPE");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_ScenarioType_Desc() { regOBD("SCENARIO_TYPE");return this; }

        protected ConditionValue _scenarioName;
        public ConditionValue ScenarioName {
            get { if (_scenarioName == null) { _scenarioName = new ConditionValue(); } return _scenarioName; }
        }
        protected override ConditionValue getCValueScenarioName() { return this.ScenarioName; }


        public BsTScenarioTotalizationCQ AddOrderBy_ScenarioName_Asc() { regOBA("SCENARIO_NAME");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_ScenarioName_Desc() { regOBD("SCENARIO_NAME");return this; }

        protected ConditionValue _conditionDiv;
        public ConditionValue ConditionDiv {
            get { if (_conditionDiv == null) { _conditionDiv = new ConditionValue(); } return _conditionDiv; }
        }
        protected override ConditionValue getCValueConditionDiv() { return this.ConditionDiv; }


        public BsTScenarioTotalizationCQ AddOrderBy_ConditionDiv_Asc() { regOBA("CONDITION_DIV");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_ConditionDiv_Desc() { regOBD("CONDITION_DIV");return this; }

        protected ConditionValue _filterFlag;
        public ConditionValue FilterFlag {
            get { if (_filterFlag == null) { _filterFlag = new ConditionValue(); } return _filterFlag; }
        }
        protected override ConditionValue getCValueFilterFlag() { return this.FilterFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_FilterFlag_Asc() { regOBA("FILTER_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_FilterFlag_Desc() { regOBD("FILTER_FLAG");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTScenarioTotalizationCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _weightbackFlag;
        public ConditionValue WeightbackFlag {
            get { if (_weightbackFlag == null) { _weightbackFlag = new ConditionValue(); } return _weightbackFlag; }
        }
        protected override ConditionValue getCValueWeightbackFlag() { return this.WeightbackFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_WeightbackFlag_Asc() { regOBA("WEIGHTBACK_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_WeightbackFlag_Desc() { regOBD("WEIGHTBACK_FLAG");return this; }

        protected ConditionValue _weightbackCode;
        public ConditionValue WeightbackCode {
            get { if (_weightbackCode == null) { _weightbackCode = new ConditionValue(); } return _weightbackCode; }
        }
        protected override ConditionValue getCValueWeightbackCode() { return this.WeightbackCode; }


        public BsTScenarioTotalizationCQ AddOrderBy_WeightbackCode_Asc() { regOBA("WEIGHTBACK_CODE");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_WeightbackCode_Desc() { regOBD("WEIGHTBACK_CODE");return this; }

        protected ConditionValue _totalnumFlag;
        public ConditionValue TotalnumFlag {
            get { if (_totalnumFlag == null) { _totalnumFlag = new ConditionValue(); } return _totalnumFlag; }
        }
        protected override ConditionValue getCValueTotalnumFlag() { return this.TotalnumFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_TotalnumFlag_Asc() { regOBA("TOTALNUM_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_TotalnumFlag_Desc() { regOBD("TOTALNUM_FLAG");return this; }

        protected ConditionValue _graphOutputFlag;
        public ConditionValue GraphOutputFlag {
            get { if (_graphOutputFlag == null) { _graphOutputFlag = new ConditionValue(); } return _graphOutputFlag; }
        }
        protected override ConditionValue getCValueGraphOutputFlag() { return this.GraphOutputFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_GraphOutputFlag_Asc() { regOBA("GRAPH_OUTPUT_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_GraphOutputFlag_Desc() { regOBD("GRAPH_OUTPUT_FLAG");return this; }

        protected ConditionValue _pieChartChoiceFlag;
        public ConditionValue PieChartChoiceFlag {
            get { if (_pieChartChoiceFlag == null) { _pieChartChoiceFlag = new ConditionValue(); } return _pieChartChoiceFlag; }
        }
        protected override ConditionValue getCValuePieChartChoiceFlag() { return this.PieChartChoiceFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_PieChartChoiceFlag_Asc() { regOBA("PIE_CHART_CHOICE_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_PieChartChoiceFlag_Desc() { regOBD("PIE_CHART_CHOICE_FLAG");return this; }

        protected ConditionValue _minimumRate;
        public ConditionValue MinimumRate {
            get { if (_minimumRate == null) { _minimumRate = new ConditionValue(); } return _minimumRate; }
        }
        protected override ConditionValue getCValueMinimumRate() { return this.MinimumRate; }


        public BsTScenarioTotalizationCQ AddOrderBy_MinimumRate_Asc() { regOBA("MINIMUM_RATE");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_MinimumRate_Desc() { regOBD("MINIMUM_RATE");return this; }

        protected ConditionValue _axisNoanswerOnoff;
        public ConditionValue AxisNoanswerOnoff {
            get { if (_axisNoanswerOnoff == null) { _axisNoanswerOnoff = new ConditionValue(); } return _axisNoanswerOnoff; }
        }
        protected override ConditionValue getCValueAxisNoanswerOnoff() { return this.AxisNoanswerOnoff; }


        public BsTScenarioTotalizationCQ AddOrderBy_AxisNoanswerOnoff_Asc() { regOBA("AXIS_NOANSWER_ONOFF");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_AxisNoanswerOnoff_Desc() { regOBD("AXIS_NOANSWER_ONOFF");return this; }

        protected ConditionValue _targetNoanswerOnoff;
        public ConditionValue TargetNoanswerOnoff {
            get { if (_targetNoanswerOnoff == null) { _targetNoanswerOnoff = new ConditionValue(); } return _targetNoanswerOnoff; }
        }
        protected override ConditionValue getCValueTargetNoanswerOnoff() { return this.TargetNoanswerOnoff; }


        public BsTScenarioTotalizationCQ AddOrderBy_TargetNoanswerOnoff_Asc() { regOBA("TARGET_NOANSWER_ONOFF");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_TargetNoanswerOnoff_Desc() { regOBD("TARGET_NOANSWER_ONOFF");return this; }

        protected ConditionValue _polylineOnoff;
        public ConditionValue PolylineOnoff {
            get { if (_polylineOnoff == null) { _polylineOnoff = new ConditionValue(); } return _polylineOnoff; }
        }
        protected override ConditionValue getCValuePolylineOnoff() { return this.PolylineOnoff; }


        public BsTScenarioTotalizationCQ AddOrderBy_PolylineOnoff_Asc() { regOBA("POLYLINE_ONOFF");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_PolylineOnoff_Desc() { regOBD("POLYLINE_ONOFF");return this; }

        protected ConditionValue _markingN;
        public ConditionValue MarkingN {
            get { if (_markingN == null) { _markingN = new ConditionValue(); } return _markingN; }
        }
        protected override ConditionValue getCValueMarkingN() { return this.MarkingN; }


        public BsTScenarioTotalizationCQ AddOrderBy_MarkingN_Asc() { regOBA("MARKING_N");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_MarkingN_Desc() { regOBD("MARKING_N");return this; }

        protected ConditionValue _rankingFlag;
        public ConditionValue RankingFlag {
            get { if (_rankingFlag == null) { _rankingFlag = new ConditionValue(); } return _rankingFlag; }
        }
        protected override ConditionValue getCValueRankingFlag() { return this.RankingFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_RankingFlag_Asc() { regOBA("RANKING_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_RankingFlag_Desc() { regOBD("RANKING_FLAG");return this; }

        protected ConditionValue _rateFlag;
        public ConditionValue RateFlag {
            get { if (_rateFlag == null) { _rateFlag = new ConditionValue(); } return _rateFlag; }
        }
        protected override ConditionValue getCValueRateFlag() { return this.RateFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_RateFlag_Asc() { regOBA("RATE_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_RateFlag_Desc() { regOBD("RATE_FLAG");return this; }

        protected ConditionValue _rate1Flag;
        public ConditionValue Rate1Flag {
            get { if (_rate1Flag == null) { _rate1Flag = new ConditionValue(); } return _rate1Flag; }
        }
        protected override ConditionValue getCValueRate1Flag() { return this.Rate1Flag; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Flag_Asc() { regOBA("RATE1_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Flag_Desc() { regOBD("RATE1_FLAG");return this; }

        protected ConditionValue _rate1Sign;
        public ConditionValue Rate1Sign {
            get { if (_rate1Sign == null) { _rate1Sign = new ConditionValue(); } return _rate1Sign; }
        }
        protected override ConditionValue getCValueRate1Sign() { return this.Rate1Sign; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Sign_Asc() { regOBA("RATE1_SIGN");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Sign_Desc() { regOBD("RATE1_SIGN");return this; }

        protected ConditionValue _rate1Range;
        public ConditionValue Rate1Range {
            get { if (_rate1Range == null) { _rate1Range = new ConditionValue(); } return _rate1Range; }
        }
        protected override ConditionValue getCValueRate1Range() { return this.Rate1Range; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Range_Asc() { regOBA("RATE1_RANGE");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Range_Desc() { regOBD("RATE1_RANGE");return this; }

        protected ConditionValue _rate1Backcolor1;
        public ConditionValue Rate1Backcolor1 {
            get { if (_rate1Backcolor1 == null) { _rate1Backcolor1 = new ConditionValue(); } return _rate1Backcolor1; }
        }
        protected override ConditionValue getCValueRate1Backcolor1() { return this.Rate1Backcolor1; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Backcolor1_Asc() { regOBA("RATE1_BACKCOLOR1");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Backcolor1_Desc() { regOBD("RATE1_BACKCOLOR1");return this; }

        protected ConditionValue _rate1Backcolor2;
        public ConditionValue Rate1Backcolor2 {
            get { if (_rate1Backcolor2 == null) { _rate1Backcolor2 = new ConditionValue(); } return _rate1Backcolor2; }
        }
        protected override ConditionValue getCValueRate1Backcolor2() { return this.Rate1Backcolor2; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Backcolor2_Asc() { regOBA("RATE1_BACKCOLOR2");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate1Backcolor2_Desc() { regOBD("RATE1_BACKCOLOR2");return this; }

        protected ConditionValue _rate2Flag;
        public ConditionValue Rate2Flag {
            get { if (_rate2Flag == null) { _rate2Flag = new ConditionValue(); } return _rate2Flag; }
        }
        protected override ConditionValue getCValueRate2Flag() { return this.Rate2Flag; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Flag_Asc() { regOBA("RATE2_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Flag_Desc() { regOBD("RATE2_FLAG");return this; }

        protected ConditionValue _rate2Sign;
        public ConditionValue Rate2Sign {
            get { if (_rate2Sign == null) { _rate2Sign = new ConditionValue(); } return _rate2Sign; }
        }
        protected override ConditionValue getCValueRate2Sign() { return this.Rate2Sign; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Sign_Asc() { regOBA("RATE2_SIGN");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Sign_Desc() { regOBD("RATE2_SIGN");return this; }

        protected ConditionValue _rate2Range;
        public ConditionValue Rate2Range {
            get { if (_rate2Range == null) { _rate2Range = new ConditionValue(); } return _rate2Range; }
        }
        protected override ConditionValue getCValueRate2Range() { return this.Rate2Range; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Range_Asc() { regOBA("RATE2_RANGE");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Range_Desc() { regOBD("RATE2_RANGE");return this; }

        protected ConditionValue _rate2Backcolor1;
        public ConditionValue Rate2Backcolor1 {
            get { if (_rate2Backcolor1 == null) { _rate2Backcolor1 = new ConditionValue(); } return _rate2Backcolor1; }
        }
        protected override ConditionValue getCValueRate2Backcolor1() { return this.Rate2Backcolor1; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Backcolor1_Asc() { regOBA("RATE2_BACKCOLOR1");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Backcolor1_Desc() { regOBD("RATE2_BACKCOLOR1");return this; }

        protected ConditionValue _rate2Backcolor2;
        public ConditionValue Rate2Backcolor2 {
            get { if (_rate2Backcolor2 == null) { _rate2Backcolor2 = new ConditionValue(); } return _rate2Backcolor2; }
        }
        protected override ConditionValue getCValueRate2Backcolor2() { return this.Rate2Backcolor2; }


        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Backcolor2_Asc() { regOBA("RATE2_BACKCOLOR2");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_Rate2Backcolor2_Desc() { regOBD("RATE2_BACKCOLOR2");return this; }

        protected ConditionValue _lastUpdateUser;
        public ConditionValue LastUpdateUser {
            get { if (_lastUpdateUser == null) { _lastUpdateUser = new ConditionValue(); } return _lastUpdateUser; }
        }
        protected override ConditionValue getCValueLastUpdateUser() { return this.LastUpdateUser; }


        public BsTScenarioTotalizationCQ AddOrderBy_LastUpdateUser_Asc() { regOBA("LAST_UPDATE_USER");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_LastUpdateUser_Desc() { regOBD("LAST_UPDATE_USER");return this; }

        protected ConditionValue _lastUpdateDatetime;
        public ConditionValue LastUpdateDatetime {
            get { if (_lastUpdateDatetime == null) { _lastUpdateDatetime = new ConditionValue(); } return _lastUpdateDatetime; }
        }
        protected override ConditionValue getCValueLastUpdateDatetime() { return this.LastUpdateDatetime; }


        public BsTScenarioTotalizationCQ AddOrderBy_LastUpdateDatetime_Asc() { regOBA("LAST_UPDATE_DATETIME");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_LastUpdateDatetime_Desc() { regOBD("LAST_UPDATE_DATETIME");return this; }

        protected ConditionValue _testFlag;
        public ConditionValue TestFlag {
            get { if (_testFlag == null) { _testFlag = new ConditionValue(); } return _testFlag; }
        }
        protected override ConditionValue getCValueTestFlag() { return this.TestFlag; }


        public BsTScenarioTotalizationCQ AddOrderBy_TestFlag_Asc() { regOBA("TEST_FLAG");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_TestFlag_Desc() { regOBD("TEST_FLAG");return this; }

        protected ConditionValue _testType;
        public ConditionValue TestType {
            get { if (_testType == null) { _testType = new ConditionValue(); } return _testType; }
        }
        protected override ConditionValue getCValueTestType() { return this.TestType; }


        public BsTScenarioTotalizationCQ AddOrderBy_TestType_Asc() { regOBA("TEST_TYPE");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_TestType_Desc() { regOBD("TEST_TYPE");return this; }

        protected ConditionValue _testSignificanceLv;
        public ConditionValue TestSignificanceLv {
            get { if (_testSignificanceLv == null) { _testSignificanceLv = new ConditionValue(); } return _testSignificanceLv; }
        }
        protected override ConditionValue getCValueTestSignificanceLv() { return this.TestSignificanceLv; }


        public BsTScenarioTotalizationCQ AddOrderBy_TestSignificanceLv_Asc() { regOBA("TEST_SIGNIFICANCE_LV");return this; }
        public BsTScenarioTotalizationCQ AddOrderBy_TestSignificanceLv_Desc() { regOBD("TEST_SIGNIFICANCE_LV");return this; }

        public BsTScenarioTotalizationCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTScenarioTotalizationCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TScenarioTotalizationCQ baseQuery = (TScenarioTotalizationCQ)baseQueryAsSuper;
            TScenarioTotalizationCQ unionQuery = (TScenarioTotalizationCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTGtScenarioItem()) {
                unionQuery.QueryTGtScenarioItem().reflectRelationOnUnionQuery(baseQuery.QueryTGtScenarioItem(), unionQuery.QueryTGtScenarioItem());
            }
            if (baseQuery.hasConditionQueryTCrossScenarioTarget()) {
                unionQuery.QueryTCrossScenarioTarget().reflectRelationOnUnionQuery(baseQuery.QueryTCrossScenarioTarget(), unionQuery.QueryTCrossScenarioTarget());
            }
            if (baseQuery.hasConditionQueryTFaScenarioHeader()) {
                unionQuery.QueryTFaScenarioHeader().reflectRelationOnUnionQuery(baseQuery.QueryTFaScenarioHeader(), unionQuery.QueryTFaScenarioHeader());
            }
            if (baseQuery.hasConditionQueryTScenarioQuerylist()) {
                unionQuery.QueryTScenarioQuerylist().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioQuerylist(), unionQuery.QueryTScenarioQuerylist());
            }
            if (baseQuery.hasConditionQueryTCategoryOutputEdit()) {
                unionQuery.QueryTCategoryOutputEdit().reflectRelationOnUnionQuery(baseQuery.QueryTCategoryOutputEdit(), unionQuery.QueryTCategoryOutputEdit());
            }
            if (baseQuery.hasConditionQueryTGtMatrixInfo()) {
                unionQuery.QueryTGtMatrixInfo().reflectRelationOnUnionQuery(baseQuery.QueryTGtMatrixInfo(), unionQuery.QueryTGtMatrixInfo());
            }
            if (baseQuery.hasConditionQueryTDefaultEnv()) {
                unionQuery.QueryTDefaultEnv().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnv(), unionQuery.QueryTDefaultEnv());
            }

        }
    
        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfo;
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfo() {
            return this.ConditionQueryTQcwebSurveyInfo;
        }
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfo {
            get {
                if (_conditionQueryTQcwebSurveyInfo == null) {
                    _conditionQueryTQcwebSurveyInfo = xcreateQueryTQcwebSurveyInfo();
                    xsetupOuterJoin_TQcwebSurveyInfo();
                }
                return _conditionQueryTQcwebSurveyInfo;
            }
        }
        protected TQcwebSurveyInfoCQ xcreateQueryTQcwebSurveyInfo() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfo() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfo() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TGtScenarioItemCQ _conditionQueryTGtScenarioItem;
        public TGtScenarioItemCQ QueryTGtScenarioItem() {
            return this.ConditionQueryTGtScenarioItem;
        }
        public TGtScenarioItemCQ ConditionQueryTGtScenarioItem {
            get {
                if (_conditionQueryTGtScenarioItem == null) {
                    _conditionQueryTGtScenarioItem = xcreateQueryTGtScenarioItem();
                    xsetupOuterJoin_TGtScenarioItem();
                }
                return _conditionQueryTGtScenarioItem;
            }
        }
        protected TGtScenarioItemCQ xcreateQueryTGtScenarioItem() {
            String nrp = resolveNextRelationPathTGtScenarioItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TGtScenarioItemCQ cq = new TGtScenarioItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tGtScenarioItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TGtScenarioItem() {
            TGtScenarioItemCQ cq = ConditionQueryTGtScenarioItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTGtScenarioItem() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tGtScenarioItem");
        }
        public bool hasConditionQueryTGtScenarioItem() {
            return _conditionQueryTGtScenarioItem != null;
        }
        protected TCrossScenarioTargetCQ _conditionQueryTCrossScenarioTarget;
        public TCrossScenarioTargetCQ QueryTCrossScenarioTarget() {
            return this.ConditionQueryTCrossScenarioTarget;
        }
        public TCrossScenarioTargetCQ ConditionQueryTCrossScenarioTarget {
            get {
                if (_conditionQueryTCrossScenarioTarget == null) {
                    _conditionQueryTCrossScenarioTarget = xcreateQueryTCrossScenarioTarget();
                    xsetupOuterJoin_TCrossScenarioTarget();
                }
                return _conditionQueryTCrossScenarioTarget;
            }
        }
        protected TCrossScenarioTargetCQ xcreateQueryTCrossScenarioTarget() {
            String nrp = resolveNextRelationPathTCrossScenarioTarget();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCrossScenarioTargetCQ cq = new TCrossScenarioTargetCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCrossScenarioTarget"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCrossScenarioTarget() {
            TCrossScenarioTargetCQ cq = ConditionQueryTCrossScenarioTarget;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCrossScenarioTarget() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tCrossScenarioTarget");
        }
        public bool hasConditionQueryTCrossScenarioTarget() {
            return _conditionQueryTCrossScenarioTarget != null;
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
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTFaScenarioHeader() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tFaScenarioHeader");
        }
        public bool hasConditionQueryTFaScenarioHeader() {
            return _conditionQueryTFaScenarioHeader != null;
        }
        protected TScenarioQuerylistCQ _conditionQueryTScenarioQuerylist;
        public TScenarioQuerylistCQ QueryTScenarioQuerylist() {
            return this.ConditionQueryTScenarioQuerylist;
        }
        public TScenarioQuerylistCQ ConditionQueryTScenarioQuerylist {
            get {
                if (_conditionQueryTScenarioQuerylist == null) {
                    _conditionQueryTScenarioQuerylist = xcreateQueryTScenarioQuerylist();
                    xsetupOuterJoin_TScenarioQuerylist();
                }
                return _conditionQueryTScenarioQuerylist;
            }
        }
        protected TScenarioQuerylistCQ xcreateQueryTScenarioQuerylist() {
            String nrp = resolveNextRelationPathTScenarioQuerylist();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TScenarioQuerylistCQ cq = new TScenarioQuerylistCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tScenarioQuerylist"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TScenarioQuerylist() {
            TScenarioQuerylistCQ cq = ConditionQueryTScenarioQuerylist;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTScenarioQuerylist() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tScenarioQuerylist");
        }
        public bool hasConditionQueryTScenarioQuerylist() {
            return _conditionQueryTScenarioQuerylist != null;
        }
        protected TCategoryOutputEditCQ _conditionQueryTCategoryOutputEdit;
        public TCategoryOutputEditCQ QueryTCategoryOutputEdit() {
            return this.ConditionQueryTCategoryOutputEdit;
        }
        public TCategoryOutputEditCQ ConditionQueryTCategoryOutputEdit {
            get {
                if (_conditionQueryTCategoryOutputEdit == null) {
                    _conditionQueryTCategoryOutputEdit = xcreateQueryTCategoryOutputEdit();
                    xsetupOuterJoin_TCategoryOutputEdit();
                }
                return _conditionQueryTCategoryOutputEdit;
            }
        }
        protected TCategoryOutputEditCQ xcreateQueryTCategoryOutputEdit() {
            String nrp = resolveNextRelationPathTCategoryOutputEdit();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TCategoryOutputEditCQ cq = new TCategoryOutputEditCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tCategoryOutputEdit"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TCategoryOutputEdit() {
            TCategoryOutputEditCQ cq = ConditionQueryTCategoryOutputEdit;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTCategoryOutputEdit() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tCategoryOutputEdit");
        }
        public bool hasConditionQueryTCategoryOutputEdit() {
            return _conditionQueryTCategoryOutputEdit != null;
        }
        protected TGtMatrixInfoCQ _conditionQueryTGtMatrixInfo;
        public TGtMatrixInfoCQ QueryTGtMatrixInfo() {
            return this.ConditionQueryTGtMatrixInfo;
        }
        public TGtMatrixInfoCQ ConditionQueryTGtMatrixInfo {
            get {
                if (_conditionQueryTGtMatrixInfo == null) {
                    _conditionQueryTGtMatrixInfo = xcreateQueryTGtMatrixInfo();
                    xsetupOuterJoin_TGtMatrixInfo();
                }
                return _conditionQueryTGtMatrixInfo;
            }
        }
        protected TGtMatrixInfoCQ xcreateQueryTGtMatrixInfo() {
            String nrp = resolveNextRelationPathTGtMatrixInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TGtMatrixInfoCQ cq = new TGtMatrixInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tGtMatrixInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TGtMatrixInfo() {
            TGtMatrixInfoCQ cq = ConditionQueryTGtMatrixInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTGtMatrixInfo() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tGtMatrixInfo");
        }
        public bool hasConditionQueryTGtMatrixInfo() {
            return _conditionQueryTGtMatrixInfo != null;
        }
        protected TDefaultEnvCQ _conditionQueryTDefaultEnv;
        public TDefaultEnvCQ QueryTDefaultEnv() {
            return this.ConditionQueryTDefaultEnv;
        }
        public TDefaultEnvCQ ConditionQueryTDefaultEnv {
            get {
                if (_conditionQueryTDefaultEnv == null) {
                    _conditionQueryTDefaultEnv = xcreateQueryTDefaultEnv();
                    xsetupOuterJoin_TDefaultEnv();
                }
                return _conditionQueryTDefaultEnv;
            }
        }
        protected TDefaultEnvCQ xcreateQueryTDefaultEnv() {
            String nrp = resolveNextRelationPathTDefaultEnv();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvCQ cq = new TDefaultEnvCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnv"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnv() {
            TDefaultEnvCQ cq = ConditionQueryTDefaultEnv;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnv() {
            return resolveNextRelationPath("T_SCENARIO_TOTALIZATION", "tDefaultEnv");
        }
        public bool hasConditionQueryTDefaultEnv() {
            return _conditionQueryTDefaultEnv != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TScenarioTotalizationCQ> _scalarSubQueryMap;
	    public Map<String, TScenarioTotalizationCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TScenarioTotalizationCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TScenarioTotalizationCQ> _myselfInScopeSubQueryMap;
        public Map<String, TScenarioTotalizationCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TScenarioTotalizationCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
