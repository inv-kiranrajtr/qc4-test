
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTItemInfoCQ : AbstractBsTItemInfoCQ {

        protected TItemInfoCIQ _inlineQuery;

        public BsTItemInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TItemInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TItemInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TItemInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TItemInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _itemInfoId;
        public ConditionValue ItemInfoId {
            get { if (_itemInfoId == null) { _itemInfoId = new ConditionValue(); } return _itemInfoId; }
        }
        protected override ConditionValue getCValueItemInfoId() { return this.ItemInfoId; }


        protected Map<String, TCategoryInfoCQ> _itemInfoId_ExistsSubQuery_TCategoryInfoListMap;
        public Map<String, TCategoryInfoCQ> ItemInfoId_ExistsSubQuery_TCategoryInfoList { get { return _itemInfoId_ExistsSubQuery_TCategoryInfoListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TCategoryInfoListMap == null) { _itemInfoId_ExistsSubQuery_TCategoryInfoListMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TCategoryInfoListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TCategoryInfoListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TCategoryInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList { get { return _itemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdListMap == null) { _itemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList { get { return _itemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap == null) { _itemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _itemInfoId_ExistsSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ItemInfoId_ExistsSubQuery_TScenarioQuerylistList { get { return _itemInfoId_ExistsSubQuery_TScenarioQuerylistListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TScenarioQuerylistListMap == null) { _itemInfoId_ExistsSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TScenarioQuerylistListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _itemInfoId_ExistsSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ItemInfoId_ExistsSubQuery_TGtScenarioItemList { get { return _itemInfoId_ExistsSubQuery_TGtScenarioItemListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TGtScenarioItemListMap == null) { _itemInfoId_ExistsSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TGtScenarioItemListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _itemInfoId_ExistsSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> ItemInfoId_ExistsSubQuery_TFaScenarioItemList { get { return _itemInfoId_ExistsSubQuery_TFaScenarioItemListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TFaScenarioItemListMap == null) { _itemInfoId_ExistsSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TFaScenarioItemListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _itemInfoId_ExistsSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> ItemInfoId_ExistsSubQuery_TFaListAddItemList { get { return _itemInfoId_ExistsSubQuery_TFaListAddItemListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TFaListAddItemListMap == null) { _itemInfoId_ExistsSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TFaListAddItemListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TFaListAddItemListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _itemInfoId_ExistsSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> ItemInfoId_ExistsSubQuery_TGtMatrixChildList { get { return _itemInfoId_ExistsSubQuery_TGtMatrixChildListMap; }}
        public override String keepItemInfoId_ExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_itemInfoId_ExistsSubQuery_TGtMatrixChildListMap == null) { _itemInfoId_ExistsSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_ExistsSubQuery_TGtMatrixChildListMap.size() + 1);
            _itemInfoId_ExistsSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "ItemInfoId_ExistsSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TCategoryInfoCQ> _itemInfoId_NotExistsSubQuery_TCategoryInfoListMap;
        public Map<String, TCategoryInfoCQ> ItemInfoId_NotExistsSubQuery_TCategoryInfoList { get { return _itemInfoId_NotExistsSubQuery_TCategoryInfoListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TCategoryInfoListMap == null) { _itemInfoId_NotExistsSubQuery_TCategoryInfoListMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TCategoryInfoListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TCategoryInfoListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TCategoryInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList { get { return _itemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdListMap == null) { _itemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList { get { return _itemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap == null) { _itemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _itemInfoId_NotExistsSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ItemInfoId_NotExistsSubQuery_TScenarioQuerylistList { get { return _itemInfoId_NotExistsSubQuery_TScenarioQuerylistListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TScenarioQuerylistListMap == null) { _itemInfoId_NotExistsSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TScenarioQuerylistListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _itemInfoId_NotExistsSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ItemInfoId_NotExistsSubQuery_TGtScenarioItemList { get { return _itemInfoId_NotExistsSubQuery_TGtScenarioItemListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TGtScenarioItemListMap == null) { _itemInfoId_NotExistsSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TGtScenarioItemListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _itemInfoId_NotExistsSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> ItemInfoId_NotExistsSubQuery_TFaScenarioItemList { get { return _itemInfoId_NotExistsSubQuery_TFaScenarioItemListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TFaScenarioItemListMap == null) { _itemInfoId_NotExistsSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TFaScenarioItemListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _itemInfoId_NotExistsSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> ItemInfoId_NotExistsSubQuery_TFaListAddItemList { get { return _itemInfoId_NotExistsSubQuery_TFaListAddItemListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TFaListAddItemListMap == null) { _itemInfoId_NotExistsSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TFaListAddItemListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TFaListAddItemListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _itemInfoId_NotExistsSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> ItemInfoId_NotExistsSubQuery_TGtMatrixChildList { get { return _itemInfoId_NotExistsSubQuery_TGtMatrixChildListMap; }}
        public override String keepItemInfoId_NotExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_itemInfoId_NotExistsSubQuery_TGtMatrixChildListMap == null) { _itemInfoId_NotExistsSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotExistsSubQuery_TGtMatrixChildListMap.size() + 1);
            _itemInfoId_NotExistsSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "ItemInfoId_NotExistsSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_InScopeSubQuery_TMatrixInfoMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_InScopeSubQuery_TMatrixInfo { get { return _itemInfoId_InScopeSubQuery_TMatrixInfoMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TMatrixInfo(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TMatrixInfoMap == null) { _itemInfoId_InScopeSubQuery_TMatrixInfoMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TMatrixInfoMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TMatrixInfoMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TMatrixInfo." + key;
        }

        protected Map<String, TCategoryInfoCQ> _itemInfoId_InScopeSubQuery_TCategoryInfoListMap;
        public Map<String, TCategoryInfoCQ> ItemInfoId_InScopeSubQuery_TCategoryInfoList { get { return _itemInfoId_InScopeSubQuery_TCategoryInfoListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TCategoryInfoListMap == null) { _itemInfoId_InScopeSubQuery_TCategoryInfoListMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TCategoryInfoListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TCategoryInfoListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TCategoryInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList { get { return _itemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdListMap == null) { _itemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList { get { return _itemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap == null) { _itemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _itemInfoId_InScopeSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ItemInfoId_InScopeSubQuery_TScenarioQuerylistList { get { return _itemInfoId_InScopeSubQuery_TScenarioQuerylistListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TScenarioQuerylistListMap == null) { _itemInfoId_InScopeSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TScenarioQuerylistListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _itemInfoId_InScopeSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ItemInfoId_InScopeSubQuery_TGtScenarioItemList { get { return _itemInfoId_InScopeSubQuery_TGtScenarioItemListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TGtScenarioItemListMap == null) { _itemInfoId_InScopeSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TGtScenarioItemListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _itemInfoId_InScopeSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> ItemInfoId_InScopeSubQuery_TFaScenarioItemList { get { return _itemInfoId_InScopeSubQuery_TFaScenarioItemListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TFaScenarioItemListMap == null) { _itemInfoId_InScopeSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TFaScenarioItemListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _itemInfoId_InScopeSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> ItemInfoId_InScopeSubQuery_TFaListAddItemList { get { return _itemInfoId_InScopeSubQuery_TFaListAddItemListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TFaListAddItemListMap == null) { _itemInfoId_InScopeSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TFaListAddItemListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TFaListAddItemListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _itemInfoId_InScopeSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> ItemInfoId_InScopeSubQuery_TGtMatrixChildList { get { return _itemInfoId_InScopeSubQuery_TGtMatrixChildListMap; }}
        public override String keepItemInfoId_InScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_itemInfoId_InScopeSubQuery_TGtMatrixChildListMap == null) { _itemInfoId_InScopeSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_InScopeSubQuery_TGtMatrixChildListMap.size() + 1);
            _itemInfoId_InScopeSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "ItemInfoId_InScopeSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_NotInScopeSubQuery_TMatrixInfoMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_NotInScopeSubQuery_TMatrixInfo { get { return _itemInfoId_NotInScopeSubQuery_TMatrixInfoMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TMatrixInfo(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TMatrixInfoMap == null) { _itemInfoId_NotInScopeSubQuery_TMatrixInfoMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TMatrixInfoMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TMatrixInfoMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TMatrixInfo." + key;
        }

        protected Map<String, TCategoryInfoCQ> _itemInfoId_NotInScopeSubQuery_TCategoryInfoListMap;
        public Map<String, TCategoryInfoCQ> ItemInfoId_NotInScopeSubQuery_TCategoryInfoList { get { return _itemInfoId_NotInScopeSubQuery_TCategoryInfoListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TCategoryInfoListMap == null) { _itemInfoId_NotInScopeSubQuery_TCategoryInfoListMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TCategoryInfoListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TCategoryInfoListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TCategoryInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList { get { return _itemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdListMap == null) { _itemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList { get { return _itemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap == null) { _itemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _itemInfoId_NotInScopeSubQuery_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList { get { return _itemInfoId_NotInScopeSubQuery_TScenarioQuerylistListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TScenarioQuerylistListMap == null) { _itemInfoId_NotInScopeSubQuery_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TScenarioQuerylistListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TScenarioQuerylistListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _itemInfoId_NotInScopeSubQuery_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ItemInfoId_NotInScopeSubQuery_TGtScenarioItemList { get { return _itemInfoId_NotInScopeSubQuery_TGtScenarioItemListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TGtScenarioItemListMap == null) { _itemInfoId_NotInScopeSubQuery_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TGtScenarioItemListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TGtScenarioItemListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TGtScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _itemInfoId_NotInScopeSubQuery_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> ItemInfoId_NotInScopeSubQuery_TFaScenarioItemList { get { return _itemInfoId_NotInScopeSubQuery_TFaScenarioItemListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TFaScenarioItemListMap == null) { _itemInfoId_NotInScopeSubQuery_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TFaScenarioItemListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TFaScenarioItemListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _itemInfoId_NotInScopeSubQuery_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> ItemInfoId_NotInScopeSubQuery_TFaListAddItemList { get { return _itemInfoId_NotInScopeSubQuery_TFaListAddItemListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TFaListAddItemListMap == null) { _itemInfoId_NotInScopeSubQuery_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TFaListAddItemListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TFaListAddItemListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TFaListAddItemList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _itemInfoId_NotInScopeSubQuery_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> ItemInfoId_NotInScopeSubQuery_TGtMatrixChildList { get { return _itemInfoId_NotInScopeSubQuery_TGtMatrixChildListMap; }}
        public override String keepItemInfoId_NotInScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_itemInfoId_NotInScopeSubQuery_TGtMatrixChildListMap == null) { _itemInfoId_NotInScopeSubQuery_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_NotInScopeSubQuery_TGtMatrixChildListMap.size() + 1);
            _itemInfoId_NotInScopeSubQuery_TGtMatrixChildListMap.put(key, subQuery); return "ItemInfoId_NotInScopeSubQuery_TGtMatrixChildList." + key;
        }

        protected Map<String, TCategoryInfoCQ> _itemInfoId_SpecifyDerivedReferrer_TCategoryInfoListMap;
        public Map<String, TCategoryInfoCQ> ItemInfoId_SpecifyDerivedReferrer_TCategoryInfoList { get { return _itemInfoId_SpecifyDerivedReferrer_TCategoryInfoListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TCategoryInfoListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TCategoryInfoListMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TCategoryInfoListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TCategoryInfoListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TCategoryInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdList { get { return _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdList { get { return _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdList." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _itemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ItemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistList { get { return _itemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistList." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _itemInfoId_SpecifyDerivedReferrer_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ItemInfoId_SpecifyDerivedReferrer_TGtScenarioItemList { get { return _itemInfoId_SpecifyDerivedReferrer_TGtScenarioItemListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TGtScenarioItemListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TGtScenarioItemListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TGtScenarioItemListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TGtScenarioItemList." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _itemInfoId_SpecifyDerivedReferrer_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> ItemInfoId_SpecifyDerivedReferrer_TFaScenarioItemList { get { return _itemInfoId_SpecifyDerivedReferrer_TFaScenarioItemListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TFaScenarioItemListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TFaScenarioItemListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TFaScenarioItemListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TFaScenarioItemList." + key;
        }

        protected Map<String, TFaListAddItemCQ> _itemInfoId_SpecifyDerivedReferrer_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> ItemInfoId_SpecifyDerivedReferrer_TFaListAddItemList { get { return _itemInfoId_SpecifyDerivedReferrer_TFaListAddItemListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TFaListAddItemListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TFaListAddItemListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TFaListAddItemListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TFaListAddItemList." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _itemInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> ItemInfoId_SpecifyDerivedReferrer_TGtMatrixChildList { get { return _itemInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap; }}
        public override String keepItemInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_itemInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap == null) { _itemInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap.size() + 1);
            _itemInfoId_SpecifyDerivedReferrer_TGtMatrixChildListMap.put(key, subQuery); return "ItemInfoId_SpecifyDerivedReferrer_TGtMatrixChildList." + key;
        }

        protected Map<String, TCategoryInfoCQ> _itemInfoId_QueryDerivedReferrer_TCategoryInfoListMap;
        public Map<String, TCategoryInfoCQ> ItemInfoId_QueryDerivedReferrer_TCategoryInfoList { get { return _itemInfoId_QueryDerivedReferrer_TCategoryInfoListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TCategoryInfoList(TCategoryInfoCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TCategoryInfoListMap == null) { _itemInfoId_QueryDerivedReferrer_TCategoryInfoListMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TCategoryInfoListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TCategoryInfoListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TCategoryInfoList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TCategoryInfoListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TCategoryInfoListParameter { get { return _itemInfoId_QueryDerivedReferrer_TCategoryInfoListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TCategoryInfoListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TCategoryInfoListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TCategoryInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TCategoryInfoListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TCategoryInfoListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TCategoryInfoListParameter." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdList { get { return _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListMap == null) { _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameter { get { return _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameter." + key;
        }

        protected Map<String, TMatrixInfoCQ> _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap;
        public Map<String, TMatrixInfoCQ> ItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdList { get { return _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap == null) { _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameter { get { return _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameter." + key;
        }

        protected Map<String, TScenarioQuerylistCQ> _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListMap;
        public Map<String, TScenarioQuerylistCQ> ItemInfoId_QueryDerivedReferrer_TScenarioQuerylistList { get { return _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListMap == null) { _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListMap = new LinkedHashMap<String, TScenarioQuerylistCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TScenarioQuerylistList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameter { get { return _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameter." + key;
        }

        protected Map<String, TGtScenarioItemCQ> _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListMap;
        public Map<String, TGtScenarioItemCQ> ItemInfoId_QueryDerivedReferrer_TGtScenarioItemList { get { return _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TGtScenarioItemListMap == null) { _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListMap = new LinkedHashMap<String, TGtScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TGtScenarioItemListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TGtScenarioItemList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameter { get { return _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameter." + key;
        }

        protected Map<String, TFaScenarioItemCQ> _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListMap;
        public Map<String, TFaScenarioItemCQ> ItemInfoId_QueryDerivedReferrer_TFaScenarioItemList { get { return _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TFaScenarioItemListMap == null) { _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListMap = new LinkedHashMap<String, TFaScenarioItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TFaScenarioItemListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TFaScenarioItemList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameter { get { return _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameter." + key;
        }

        protected Map<String, TFaListAddItemCQ> _itemInfoId_QueryDerivedReferrer_TFaListAddItemListMap;
        public Map<String, TFaListAddItemCQ> ItemInfoId_QueryDerivedReferrer_TFaListAddItemList { get { return _itemInfoId_QueryDerivedReferrer_TFaListAddItemListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TFaListAddItemListMap == null) { _itemInfoId_QueryDerivedReferrer_TFaListAddItemListMap = new LinkedHashMap<String, TFaListAddItemCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TFaListAddItemListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TFaListAddItemListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TFaListAddItemList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TFaListAddItemListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TFaListAddItemListParameter { get { return _itemInfoId_QueryDerivedReferrer_TFaListAddItemListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TFaListAddItemListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TFaListAddItemListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TFaListAddItemListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TFaListAddItemListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TFaListAddItemListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TFaListAddItemListParameter." + key;
        }

        protected Map<String, TGtMatrixChildCQ> _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListMap;
        public Map<String, TGtMatrixChildCQ> ItemInfoId_QueryDerivedReferrer_TGtMatrixChildList { get { return _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            if (_itemInfoId_QueryDerivedReferrer_TGtMatrixChildListMap == null) { _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListMap = new LinkedHashMap<String, TGtMatrixChildCQ>(); }
            String key = "subQueryMapKey" + (_itemInfoId_QueryDerivedReferrer_TGtMatrixChildListMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListMap.put(key, subQuery); return "ItemInfoId_QueryDerivedReferrer_TGtMatrixChildList." + key;
        }
        protected Map<String, Object> _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap;
        public Map<String, Object> ItemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter { get { return _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap; } }
        public override String keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(Object parameterValue) {
            if (_itemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap == null) { _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_itemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap.size() + 1);
            _itemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameterMap.put(key, parameterValue); return "ItemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter." + key;
        }

        public BsTItemInfoCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTItemInfoCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

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

        public BsTItemInfoCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTItemInfoCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _itemName;
        public ConditionValue ItemName {
            get { if (_itemName == null) { _itemName = new ConditionValue(); } return _itemName; }
        }
        protected override ConditionValue getCValueItemName() { return this.ItemName; }


        public BsTItemInfoCQ AddOrderBy_ItemName_Asc() { regOBA("ITEM_NAME");return this; }
        public BsTItemInfoCQ AddOrderBy_ItemName_Desc() { regOBD("ITEM_NAME");return this; }

        protected ConditionValue _sourceDiv;
        public ConditionValue SourceDiv {
            get { if (_sourceDiv == null) { _sourceDiv = new ConditionValue(); } return _sourceDiv; }
        }
        protected override ConditionValue getCValueSourceDiv() { return this.SourceDiv; }


        public BsTItemInfoCQ AddOrderBy_SourceDiv_Asc() { regOBA("SOURCE_DIV");return this; }
        public BsTItemInfoCQ AddOrderBy_SourceDiv_Desc() { regOBD("SOURCE_DIV");return this; }

        protected ConditionValue _itemno;
        public ConditionValue Itemno {
            get { if (_itemno == null) { _itemno = new ConditionValue(); } return _itemno; }
        }
        protected override ConditionValue getCValueItemno() { return this.Itemno; }


        public BsTItemInfoCQ AddOrderBy_Itemno_Asc() { regOBA("ITEMNO");return this; }
        public BsTItemInfoCQ AddOrderBy_Itemno_Desc() { regOBD("ITEMNO");return this; }

        protected ConditionValue _itemType;
        public ConditionValue ItemType {
            get { if (_itemType == null) { _itemType = new ConditionValue(); } return _itemType; }
        }
        protected override ConditionValue getCValueItemType() { return this.ItemType; }


        public BsTItemInfoCQ AddOrderBy_ItemType_Asc() { regOBA("ITEM_TYPE");return this; }
        public BsTItemInfoCQ AddOrderBy_ItemType_Desc() { regOBD("ITEM_TYPE");return this; }

        protected ConditionValue _answerType;
        public ConditionValue AnswerType {
            get { if (_answerType == null) { _answerType = new ConditionValue(); } return _answerType; }
        }
        protected override ConditionValue getCValueAnswerType() { return this.AnswerType; }


        public BsTItemInfoCQ AddOrderBy_AnswerType_Asc() { regOBA("ANSWER_TYPE");return this; }
        public BsTItemInfoCQ AddOrderBy_AnswerType_Desc() { regOBD("ANSWER_TYPE");return this; }

        protected ConditionValue _sortNumber;
        public ConditionValue SortNumber {
            get { if (_sortNumber == null) { _sortNumber = new ConditionValue(); } return _sortNumber; }
        }
        protected override ConditionValue getCValueSortNumber() { return this.SortNumber; }


        public BsTItemInfoCQ AddOrderBy_SortNumber_Asc() { regOBA("SORT_NUMBER");return this; }
        public BsTItemInfoCQ AddOrderBy_SortNumber_Desc() { regOBD("SORT_NUMBER");return this; }

        protected ConditionValue _matrixDiv;
        public ConditionValue MatrixDiv {
            get { if (_matrixDiv == null) { _matrixDiv = new ConditionValue(); } return _matrixDiv; }
        }
        protected override ConditionValue getCValueMatrixDiv() { return this.MatrixDiv; }


        public BsTItemInfoCQ AddOrderBy_MatrixDiv_Asc() { regOBA("MATRIX_DIV");return this; }
        public BsTItemInfoCQ AddOrderBy_MatrixDiv_Desc() { regOBD("MATRIX_DIV");return this; }

        protected ConditionValue _lv1title;
        public ConditionValue Lv1title {
            get { if (_lv1title == null) { _lv1title = new ConditionValue(); } return _lv1title; }
        }
        protected override ConditionValue getCValueLv1title() { return this.Lv1title; }


        public BsTItemInfoCQ AddOrderBy_Lv1title_Asc() { regOBA("LV1TITLE");return this; }
        public BsTItemInfoCQ AddOrderBy_Lv1title_Desc() { regOBD("LV1TITLE");return this; }

        protected ConditionValue _lv2title;
        public ConditionValue Lv2title {
            get { if (_lv2title == null) { _lv2title = new ConditionValue(); } return _lv2title; }
        }
        protected override ConditionValue getCValueLv2title() { return this.Lv2title; }


        public BsTItemInfoCQ AddOrderBy_Lv2title_Asc() { regOBA("LV2TITLE");return this; }
        public BsTItemInfoCQ AddOrderBy_Lv2title_Desc() { regOBD("LV2TITLE");return this; }

        protected ConditionValue _originalLv1title;
        public ConditionValue OriginalLv1title {
            get { if (_originalLv1title == null) { _originalLv1title = new ConditionValue(); } return _originalLv1title; }
        }
        protected override ConditionValue getCValueOriginalLv1title() { return this.OriginalLv1title; }


        public BsTItemInfoCQ AddOrderBy_OriginalLv1title_Asc() { regOBA("ORIGINAL_LV1TITLE");return this; }
        public BsTItemInfoCQ AddOrderBy_OriginalLv1title_Desc() { regOBD("ORIGINAL_LV1TITLE");return this; }

        protected ConditionValue _originalLv2title;
        public ConditionValue OriginalLv2title {
            get { if (_originalLv2title == null) { _originalLv2title = new ConditionValue(); } return _originalLv2title; }
        }
        protected override ConditionValue getCValueOriginalLv2title() { return this.OriginalLv2title; }


        public BsTItemInfoCQ AddOrderBy_OriginalLv2title_Asc() { regOBA("ORIGINAL_LV2TITLE");return this; }
        public BsTItemInfoCQ AddOrderBy_OriginalLv2title_Desc() { regOBD("ORIGINAL_LV2TITLE");return this; }

        protected ConditionValue _tableName;
        public ConditionValue TableName {
            get { if (_tableName == null) { _tableName = new ConditionValue(); } return _tableName; }
        }
        protected override ConditionValue getCValueTableName() { return this.TableName; }


        public BsTItemInfoCQ AddOrderBy_TableName_Asc() { regOBA("TABLE_NAME");return this; }
        public BsTItemInfoCQ AddOrderBy_TableName_Desc() { regOBD("TABLE_NAME");return this; }

        protected ConditionValue _columnName;
        public ConditionValue ColumnName {
            get { if (_columnName == null) { _columnName = new ConditionValue(); } return _columnName; }
        }
        protected override ConditionValue getCValueColumnName() { return this.ColumnName; }


        public BsTItemInfoCQ AddOrderBy_ColumnName_Asc() { regOBA("COLUMN_NAME");return this; }
        public BsTItemInfoCQ AddOrderBy_ColumnName_Desc() { regOBD("COLUMN_NAME");return this; }

        protected ConditionValue _categoryEditId;
        public ConditionValue CategoryEditId {
            get { if (_categoryEditId == null) { _categoryEditId = new ConditionValue(); } return _categoryEditId; }
        }
        protected override ConditionValue getCValueCategoryEditId() { return this.CategoryEditId; }


        protected Map<String, TScenarioTotalizationCQ> _categoryEditId_InScopeSubQuery_TScenarioTotalizationMap;
        public Map<String, TScenarioTotalizationCQ> CategoryEditId_InScopeSubQuery_TScenarioTotalization { get { return _categoryEditId_InScopeSubQuery_TScenarioTotalizationMap; }}
        public override String keepCategoryEditId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            if (_categoryEditId_InScopeSubQuery_TScenarioTotalizationMap == null) { _categoryEditId_InScopeSubQuery_TScenarioTotalizationMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_categoryEditId_InScopeSubQuery_TScenarioTotalizationMap.size() + 1);
            _categoryEditId_InScopeSubQuery_TScenarioTotalizationMap.put(key, subQuery); return "CategoryEditId_InScopeSubQuery_TScenarioTotalization." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _categoryEditId_NotInScopeSubQuery_TScenarioTotalizationMap;
        public Map<String, TScenarioTotalizationCQ> CategoryEditId_NotInScopeSubQuery_TScenarioTotalization { get { return _categoryEditId_NotInScopeSubQuery_TScenarioTotalizationMap; }}
        public override String keepCategoryEditId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            if (_categoryEditId_NotInScopeSubQuery_TScenarioTotalizationMap == null) { _categoryEditId_NotInScopeSubQuery_TScenarioTotalizationMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_categoryEditId_NotInScopeSubQuery_TScenarioTotalizationMap.size() + 1);
            _categoryEditId_NotInScopeSubQuery_TScenarioTotalizationMap.put(key, subQuery); return "CategoryEditId_NotInScopeSubQuery_TScenarioTotalization." + key;
        }

        public BsTItemInfoCQ AddOrderBy_CategoryEditId_Asc() { regOBA("CATEGORY_EDIT_ID");return this; }
        public BsTItemInfoCQ AddOrderBy_CategoryEditId_Desc() { regOBD("CATEGORY_EDIT_ID");return this; }

        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TDataEditListCQ> _dataEditId_InScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_InScopeSubQuery_TDataEditList { get { return _dataEditId_InScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataEditListMap == null) { _dataEditId_InScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_NotInScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_NotInScopeSubQuery_TDataEditList { get { return _dataEditId_NotInScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataEditListMap == null) { _dataEditId_NotInScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataEditList." + key;
        }

        public BsTItemInfoCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTItemInfoCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        protected ConditionValue _status;
        public ConditionValue Status {
            get { if (_status == null) { _status = new ConditionValue(); } return _status; }
        }
        protected override ConditionValue getCValueStatus() { return this.Status; }


        public BsTItemInfoCQ AddOrderBy_Status_Asc() { regOBA("STATUS");return this; }
        public BsTItemInfoCQ AddOrderBy_Status_Desc() { regOBD("STATUS");return this; }

        protected ConditionValue _tableNameOrg;
        public ConditionValue TableNameOrg {
            get { if (_tableNameOrg == null) { _tableNameOrg = new ConditionValue(); } return _tableNameOrg; }
        }
        protected override ConditionValue getCValueTableNameOrg() { return this.TableNameOrg; }


        public BsTItemInfoCQ AddOrderBy_TableNameOrg_Asc() { regOBA("TABLE_NAME_ORG");return this; }
        public BsTItemInfoCQ AddOrderBy_TableNameOrg_Desc() { regOBD("TABLE_NAME_ORG");return this; }

        protected ConditionValue _columnNameOrg;
        public ConditionValue ColumnNameOrg {
            get { if (_columnNameOrg == null) { _columnNameOrg = new ConditionValue(); } return _columnNameOrg; }
        }
        protected override ConditionValue getCValueColumnNameOrg() { return this.ColumnNameOrg; }


        public BsTItemInfoCQ AddOrderBy_ColumnNameOrg_Asc() { regOBA("COLUMN_NAME_ORG");return this; }
        public BsTItemInfoCQ AddOrderBy_ColumnNameOrg_Desc() { regOBD("COLUMN_NAME_ORG");return this; }

        protected ConditionValue _compelItemChangeFlag;
        public ConditionValue CompelItemChangeFlag {
            get { if (_compelItemChangeFlag == null) { _compelItemChangeFlag = new ConditionValue(); } return _compelItemChangeFlag; }
        }
        protected override ConditionValue getCValueCompelItemChangeFlag() { return this.CompelItemChangeFlag; }


        public BsTItemInfoCQ AddOrderBy_CompelItemChangeFlag_Asc() { regOBA("COMPEL_ITEM_CHANGE_FLAG");return this; }
        public BsTItemInfoCQ AddOrderBy_CompelItemChangeFlag_Desc() { regOBD("COMPEL_ITEM_CHANGE_FLAG");return this; }

        protected ConditionValue _sortFlag;
        public ConditionValue SortFlag {
            get { if (_sortFlag == null) { _sortFlag = new ConditionValue(); } return _sortFlag; }
        }
        protected override ConditionValue getCValueSortFlag() { return this.SortFlag; }


        public BsTItemInfoCQ AddOrderBy_SortFlag_Asc() { regOBA("SORT_FLAG");return this; }
        public BsTItemInfoCQ AddOrderBy_SortFlag_Desc() { regOBD("SORT_FLAG");return this; }

        protected ConditionValue _sortRange;
        public ConditionValue SortRange {
            get { if (_sortRange == null) { _sortRange = new ConditionValue(); } return _sortRange; }
        }
        protected override ConditionValue getCValueSortRange() { return this.SortRange; }


        public BsTItemInfoCQ AddOrderBy_SortRange_Asc() { regOBA("SORT_RANGE");return this; }
        public BsTItemInfoCQ AddOrderBy_SortRange_Desc() { regOBD("SORT_RANGE");return this; }

        protected ConditionValue _multivariateFlag;
        public ConditionValue MultivariateFlag {
            get { if (_multivariateFlag == null) { _multivariateFlag = new ConditionValue(); } return _multivariateFlag; }
        }
        protected override ConditionValue getCValueMultivariateFlag() { return this.MultivariateFlag; }


        public BsTItemInfoCQ AddOrderBy_MultivariateFlag_Asc() { regOBA("MULTIVARIATE_FLAG");return this; }
        public BsTItemInfoCQ AddOrderBy_MultivariateFlag_Desc() { regOBD("MULTIVARIATE_FLAG");return this; }

        protected ConditionValue _tableNo;
        public ConditionValue TableNo {
            get { if (_tableNo == null) { _tableNo = new ConditionValue(); } return _tableNo; }
        }
        protected override ConditionValue getCValueTableNo() { return this.TableNo; }


        public BsTItemInfoCQ AddOrderBy_TableNo_Asc() { regOBA("TABLE_NO");return this; }
        public BsTItemInfoCQ AddOrderBy_TableNo_Desc() { regOBD("TABLE_NO");return this; }

        protected ConditionValue _columnNo;
        public ConditionValue ColumnNo {
            get { if (_columnNo == null) { _columnNo = new ConditionValue(); } return _columnNo; }
        }
        protected override ConditionValue getCValueColumnNo() { return this.ColumnNo; }


        public BsTItemInfoCQ AddOrderBy_ColumnNo_Asc() { regOBA("COLUMN_NO");return this; }
        public BsTItemInfoCQ AddOrderBy_ColumnNo_Desc() { regOBD("COLUMN_NO");return this; }

        protected ConditionValue _tableNoOrg;
        public ConditionValue TableNoOrg {
            get { if (_tableNoOrg == null) { _tableNoOrg = new ConditionValue(); } return _tableNoOrg; }
        }
        protected override ConditionValue getCValueTableNoOrg() { return this.TableNoOrg; }


        public BsTItemInfoCQ AddOrderBy_TableNoOrg_Asc() { regOBA("TABLE_NO_ORG");return this; }
        public BsTItemInfoCQ AddOrderBy_TableNoOrg_Desc() { regOBD("TABLE_NO_ORG");return this; }

        protected ConditionValue _columnNoOrg;
        public ConditionValue ColumnNoOrg {
            get { if (_columnNoOrg == null) { _columnNoOrg = new ConditionValue(); } return _columnNoOrg; }
        }
        protected override ConditionValue getCValueColumnNoOrg() { return this.ColumnNoOrg; }


        public BsTItemInfoCQ AddOrderBy_ColumnNoOrg_Asc() { regOBA("COLUMN_NO_ORG");return this; }
        public BsTItemInfoCQ AddOrderBy_ColumnNoOrg_Desc() { regOBD("COLUMN_NO_ORG");return this; }

        protected ConditionValue _lastUpdateUser;
        public ConditionValue LastUpdateUser {
            get { if (_lastUpdateUser == null) { _lastUpdateUser = new ConditionValue(); } return _lastUpdateUser; }
        }
        protected override ConditionValue getCValueLastUpdateUser() { return this.LastUpdateUser; }


        public BsTItemInfoCQ AddOrderBy_LastUpdateUser_Asc() { regOBA("LAST_UPDATE_USER");return this; }
        public BsTItemInfoCQ AddOrderBy_LastUpdateUser_Desc() { regOBD("LAST_UPDATE_USER");return this; }

        protected ConditionValue _lastUpdateDatetime;
        public ConditionValue LastUpdateDatetime {
            get { if (_lastUpdateDatetime == null) { _lastUpdateDatetime = new ConditionValue(); } return _lastUpdateDatetime; }
        }
        protected override ConditionValue getCValueLastUpdateDatetime() { return this.LastUpdateDatetime; }


        public BsTItemInfoCQ AddOrderBy_LastUpdateDatetime_Asc() { regOBA("LAST_UPDATE_DATETIME");return this; }
        public BsTItemInfoCQ AddOrderBy_LastUpdateDatetime_Desc() { regOBD("LAST_UPDATE_DATETIME");return this; }

        protected ConditionValue _newAtQc3Flag;
        public ConditionValue NewAtQc3Flag {
            get { if (_newAtQc3Flag == null) { _newAtQc3Flag = new ConditionValue(); } return _newAtQc3Flag; }
        }
        protected override ConditionValue getCValueNewAtQc3Flag() { return this.NewAtQc3Flag; }


        public BsTItemInfoCQ AddOrderBy_NewAtQc3Flag_Asc() { regOBA("NEW_AT_QC3_FLAG");return this; }
        public BsTItemInfoCQ AddOrderBy_NewAtQc3Flag_Desc() { regOBD("NEW_AT_QC3_FLAG");return this; }

        protected ConditionValue _sortRangeOrg;
        public ConditionValue SortRangeOrg {
            get { if (_sortRangeOrg == null) { _sortRangeOrg = new ConditionValue(); } return _sortRangeOrg; }
        }
        protected override ConditionValue getCValueSortRangeOrg() { return this.SortRangeOrg; }


        public BsTItemInfoCQ AddOrderBy_SortRangeOrg_Asc() { regOBA("SORT_RANGE_ORG");return this; }
        public BsTItemInfoCQ AddOrderBy_SortRangeOrg_Desc() { regOBD("SORT_RANGE_ORG");return this; }

        public BsTItemInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTItemInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TItemInfoCQ baseQuery = (TItemInfoCQ)baseQueryAsSuper;
            TItemInfoCQ unionQuery = (TItemInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTMatrixInfo()) {
                unionQuery.QueryTMatrixInfo().reflectRelationOnUnionQuery(baseQuery.QueryTMatrixInfo(), unionQuery.QueryTMatrixInfo());
            }
            if (baseQuery.hasConditionQueryTFaListAddItem()) {
                unionQuery.QueryTFaListAddItem().reflectRelationOnUnionQuery(baseQuery.QueryTFaListAddItem(), unionQuery.QueryTFaListAddItem());
            }
            if (baseQuery.hasConditionQueryTFaScenarioItem()) {
                unionQuery.QueryTFaScenarioItem().reflectRelationOnUnionQuery(baseQuery.QueryTFaScenarioItem(), unionQuery.QueryTFaScenarioItem());
            }
            if (baseQuery.hasConditionQueryTTableControl()) {
                unionQuery.QueryTTableControl().reflectRelationOnUnionQuery(baseQuery.QueryTTableControl(), unionQuery.QueryTTableControl());
            }
            if (baseQuery.hasConditionQueryTScenarioTotalization()) {
                unionQuery.QueryTScenarioTotalization().reflectRelationOnUnionQuery(baseQuery.QueryTScenarioTotalization(), unionQuery.QueryTScenarioTotalization());
            }
            if (baseQuery.hasConditionQueryTDataEditList()) {
                unionQuery.QueryTDataEditList().reflectRelationOnUnionQuery(baseQuery.QueryTDataEditList(), unionQuery.QueryTDataEditList());
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
            return resolveNextRelationPath("T_ITEM_INFO", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TMatrixInfoCQ _conditionQueryTMatrixInfo;
        public TMatrixInfoCQ QueryTMatrixInfo() {
            return this.ConditionQueryTMatrixInfo;
        }
        public TMatrixInfoCQ ConditionQueryTMatrixInfo {
            get {
                if (_conditionQueryTMatrixInfo == null) {
                    _conditionQueryTMatrixInfo = xcreateQueryTMatrixInfo();
                    xsetupOuterJoin_TMatrixInfo();
                }
                return _conditionQueryTMatrixInfo;
            }
        }
        protected TMatrixInfoCQ xcreateQueryTMatrixInfo() {
            String nrp = resolveNextRelationPathTMatrixInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TMatrixInfoCQ cq = new TMatrixInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tMatrixInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TMatrixInfo() {
            TMatrixInfoCQ cq = ConditionQueryTMatrixInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ITEM_INFO_ID", "Child_Item_Info_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTMatrixInfo() {
            return resolveNextRelationPath("T_ITEM_INFO", "tMatrixInfo");
        }
        public bool hasConditionQueryTMatrixInfo() {
            return _conditionQueryTMatrixInfo != null;
        }
        protected TFaListAddItemCQ _conditionQueryTFaListAddItem;
        public TFaListAddItemCQ QueryTFaListAddItem() {
            return this.ConditionQueryTFaListAddItem;
        }
        public TFaListAddItemCQ ConditionQueryTFaListAddItem {
            get {
                if (_conditionQueryTFaListAddItem == null) {
                    _conditionQueryTFaListAddItem = xcreateQueryTFaListAddItem();
                    xsetupOuterJoin_TFaListAddItem();
                }
                return _conditionQueryTFaListAddItem;
            }
        }
        protected TFaListAddItemCQ xcreateQueryTFaListAddItem() {
            String nrp = resolveNextRelationPathTFaListAddItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TFaListAddItemCQ cq = new TFaListAddItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tFaListAddItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TFaListAddItem() {
            TFaListAddItemCQ cq = ConditionQueryTFaListAddItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ITEM_INFO_ID", "Item_Info_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTFaListAddItem() {
            return resolveNextRelationPath("T_ITEM_INFO", "tFaListAddItem");
        }
        public bool hasConditionQueryTFaListAddItem() {
            return _conditionQueryTFaListAddItem != null;
        }
        protected TFaScenarioItemCQ _conditionQueryTFaScenarioItem;
        public TFaScenarioItemCQ QueryTFaScenarioItem() {
            return this.ConditionQueryTFaScenarioItem;
        }
        public TFaScenarioItemCQ ConditionQueryTFaScenarioItem {
            get {
                if (_conditionQueryTFaScenarioItem == null) {
                    _conditionQueryTFaScenarioItem = xcreateQueryTFaScenarioItem();
                    xsetupOuterJoin_TFaScenarioItem();
                }
                return _conditionQueryTFaScenarioItem;
            }
        }
        protected TFaScenarioItemCQ xcreateQueryTFaScenarioItem() {
            String nrp = resolveNextRelationPathTFaScenarioItem();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TFaScenarioItemCQ cq = new TFaScenarioItemCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tFaScenarioItem"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TFaScenarioItem() {
            TFaScenarioItemCQ cq = ConditionQueryTFaScenarioItem;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("ITEM_INFO_ID", "FA_Target_Item_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTFaScenarioItem() {
            return resolveNextRelationPath("T_ITEM_INFO", "tFaScenarioItem");
        }
        public bool hasConditionQueryTFaScenarioItem() {
            return _conditionQueryTFaScenarioItem != null;
        }
        protected TTableControlCQ _conditionQueryTTableControl;
        public TTableControlCQ QueryTTableControl() {
            return this.ConditionQueryTTableControl;
        }
        public TTableControlCQ ConditionQueryTTableControl {
            get {
                if (_conditionQueryTTableControl == null) {
                    _conditionQueryTTableControl = xcreateQueryTTableControl();
                    xsetupOuterJoin_TTableControl();
                }
                return _conditionQueryTTableControl;
            }
        }
        protected TTableControlCQ xcreateQueryTTableControl() {
            String nrp = resolveNextRelationPathTTableControl();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TTableControlCQ cq = new TTableControlCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tTableControl"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TTableControl() {
            TTableControlCQ cq = ConditionQueryTTableControl;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTTableControl() {
            return resolveNextRelationPath("T_ITEM_INFO", "tTableControl");
        }
        public bool hasConditionQueryTTableControl() {
            return _conditionQueryTTableControl != null;
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
            joinOnMap.put("CATEGORY_EDIT_ID", "Scenario_Totalization_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTScenarioTotalization() {
            return resolveNextRelationPath("T_ITEM_INFO", "tScenarioTotalization");
        }
        public bool hasConditionQueryTScenarioTotalization() {
            return _conditionQueryTScenarioTotalization != null;
        }
        protected TDataEditListCQ _conditionQueryTDataEditList;
        public TDataEditListCQ QueryTDataEditList() {
            return this.ConditionQueryTDataEditList;
        }
        public TDataEditListCQ ConditionQueryTDataEditList {
            get {
                if (_conditionQueryTDataEditList == null) {
                    _conditionQueryTDataEditList = xcreateQueryTDataEditList();
                    xsetupOuterJoin_TDataEditList();
                }
                return _conditionQueryTDataEditList;
            }
        }
        protected TDataEditListCQ xcreateQueryTDataEditList() {
            String nrp = resolveNextRelationPathTDataEditList();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDataEditListCQ cq = new TDataEditListCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDataEditList"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDataEditList() {
            TDataEditListCQ cq = ConditionQueryTDataEditList;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DATA_EDIT_ID", "DATA_EDIT_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDataEditList() {
            return resolveNextRelationPath("T_ITEM_INFO", "tDataEditList");
        }
        public bool hasConditionQueryTDataEditList() {
            return _conditionQueryTDataEditList != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TItemInfoCQ> _scalarSubQueryMap;
	    public Map<String, TItemInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TItemInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TItemInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TItemInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TItemInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TItemInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
