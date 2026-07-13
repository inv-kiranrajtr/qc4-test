
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDataProcessNewItemCQ : AbstractBsTDataProcessNewItemCQ {

        protected TDataProcessNewItemCIQ _inlineQuery;

        public BsTDataProcessNewItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDataProcessNewItemCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDataProcessNewItemCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDataProcessNewItemCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDataProcessNewItemCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _dataEditId;
        public ConditionValue DataEditId {
            get { if (_dataEditId == null) { _dataEditId = new ConditionValue(); } return _dataEditId; }
        }
        protected override ConditionValue getCValueDataEditId() { return this.DataEditId; }


        protected Map<String, TDataProcessNewCategoryCQ> _dataEditId_ExistsSubQuery_TDataProcessNewCategoryListMap;
        public Map<String, TDataProcessNewCategoryCQ> DataEditId_ExistsSubQuery_TDataProcessNewCategoryList { get { return _dataEditId_ExistsSubQuery_TDataProcessNewCategoryListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TDataProcessNewCategoryListMap == null) { _dataEditId_ExistsSubQuery_TDataProcessNewCategoryListMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TDataProcessNewCategoryListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TDataProcessNewCategoryListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TDataProcessNewCategoryList." + key;
        }

        protected Map<String, TDataProcessNewItemSrcCQ> _dataEditId_ExistsSubQuery_TDataProcessNewItemSrcListMap;
        public Map<String, TDataProcessNewItemSrcCQ> DataEditId_ExistsSubQuery_TDataProcessNewItemSrcList { get { return _dataEditId_ExistsSubQuery_TDataProcessNewItemSrcListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TDataProcessNewItemSrcListMap == null) { _dataEditId_ExistsSubQuery_TDataProcessNewItemSrcListMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TDataProcessNewItemSrcListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TDataProcessNewItemSrcListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TDataProcessNewItemSrcList." + key;
        }

        protected Map<String, TIntegConditionCQ> _dataEditId_ExistsSubQuery_TIntegConditionListMap;
        public Map<String, TIntegConditionCQ> DataEditId_ExistsSubQuery_TIntegConditionList { get { return _dataEditId_ExistsSubQuery_TIntegConditionListMap; }}
        public override String keepDataEditId_ExistsSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            if (_dataEditId_ExistsSubQuery_TIntegConditionListMap == null) { _dataEditId_ExistsSubQuery_TIntegConditionListMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_ExistsSubQuery_TIntegConditionListMap.size() + 1);
            _dataEditId_ExistsSubQuery_TIntegConditionListMap.put(key, subQuery); return "DataEditId_ExistsSubQuery_TIntegConditionList." + key;
        }

        protected Map<String, TDataProcessNewCategoryCQ> _dataEditId_NotExistsSubQuery_TDataProcessNewCategoryListMap;
        public Map<String, TDataProcessNewCategoryCQ> DataEditId_NotExistsSubQuery_TDataProcessNewCategoryList { get { return _dataEditId_NotExistsSubQuery_TDataProcessNewCategoryListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TDataProcessNewCategoryListMap == null) { _dataEditId_NotExistsSubQuery_TDataProcessNewCategoryListMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TDataProcessNewCategoryListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TDataProcessNewCategoryListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TDataProcessNewCategoryList." + key;
        }

        protected Map<String, TDataProcessNewItemSrcCQ> _dataEditId_NotExistsSubQuery_TDataProcessNewItemSrcListMap;
        public Map<String, TDataProcessNewItemSrcCQ> DataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList { get { return _dataEditId_NotExistsSubQuery_TDataProcessNewItemSrcListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TDataProcessNewItemSrcListMap == null) { _dataEditId_NotExistsSubQuery_TDataProcessNewItemSrcListMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TDataProcessNewItemSrcListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TDataProcessNewItemSrcListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList." + key;
        }

        protected Map<String, TIntegConditionCQ> _dataEditId_NotExistsSubQuery_TIntegConditionListMap;
        public Map<String, TIntegConditionCQ> DataEditId_NotExistsSubQuery_TIntegConditionList { get { return _dataEditId_NotExistsSubQuery_TIntegConditionListMap; }}
        public override String keepDataEditId_NotExistsSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            if (_dataEditId_NotExistsSubQuery_TIntegConditionListMap == null) { _dataEditId_NotExistsSubQuery_TIntegConditionListMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotExistsSubQuery_TIntegConditionListMap.size() + 1);
            _dataEditId_NotExistsSubQuery_TIntegConditionListMap.put(key, subQuery); return "DataEditId_NotExistsSubQuery_TIntegConditionList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_InScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_InScopeSubQuery_TDataEditList { get { return _dataEditId_InScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataEditListMap == null) { _dataEditId_InScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TDataProcessNewCategoryCQ> _dataEditId_InScopeSubQuery_TDataProcessNewCategoryListMap;
        public Map<String, TDataProcessNewCategoryCQ> DataEditId_InScopeSubQuery_TDataProcessNewCategoryList { get { return _dataEditId_InScopeSubQuery_TDataProcessNewCategoryListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataProcessNewCategoryListMap == null) { _dataEditId_InScopeSubQuery_TDataProcessNewCategoryListMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataProcessNewCategoryListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataProcessNewCategoryListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataProcessNewCategoryList." + key;
        }

        protected Map<String, TDataProcessNewItemSrcCQ> _dataEditId_InScopeSubQuery_TDataProcessNewItemSrcListMap;
        public Map<String, TDataProcessNewItemSrcCQ> DataEditId_InScopeSubQuery_TDataProcessNewItemSrcList { get { return _dataEditId_InScopeSubQuery_TDataProcessNewItemSrcListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TDataProcessNewItemSrcListMap == null) { _dataEditId_InScopeSubQuery_TDataProcessNewItemSrcListMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TDataProcessNewItemSrcListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TDataProcessNewItemSrcListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TDataProcessNewItemSrcList." + key;
        }

        protected Map<String, TIntegConditionCQ> _dataEditId_InScopeSubQuery_TIntegConditionListMap;
        public Map<String, TIntegConditionCQ> DataEditId_InScopeSubQuery_TIntegConditionList { get { return _dataEditId_InScopeSubQuery_TIntegConditionListMap; }}
        public override String keepDataEditId_InScopeSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            if (_dataEditId_InScopeSubQuery_TIntegConditionListMap == null) { _dataEditId_InScopeSubQuery_TIntegConditionListMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_InScopeSubQuery_TIntegConditionListMap.size() + 1);
            _dataEditId_InScopeSubQuery_TIntegConditionListMap.put(key, subQuery); return "DataEditId_InScopeSubQuery_TIntegConditionList." + key;
        }

        protected Map<String, TDataEditListCQ> _dataEditId_NotInScopeSubQuery_TDataEditListMap;
        public Map<String, TDataEditListCQ> DataEditId_NotInScopeSubQuery_TDataEditList { get { return _dataEditId_NotInScopeSubQuery_TDataEditListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataEditListMap == null) { _dataEditId_NotInScopeSubQuery_TDataEditListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataEditListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataEditListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataEditList." + key;
        }

        protected Map<String, TDataProcessNewCategoryCQ> _dataEditId_NotInScopeSubQuery_TDataProcessNewCategoryListMap;
        public Map<String, TDataProcessNewCategoryCQ> DataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList { get { return _dataEditId_NotInScopeSubQuery_TDataProcessNewCategoryListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataProcessNewCategoryListMap == null) { _dataEditId_NotInScopeSubQuery_TDataProcessNewCategoryListMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataProcessNewCategoryListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataProcessNewCategoryListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList." + key;
        }

        protected Map<String, TDataProcessNewItemSrcCQ> _dataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcListMap;
        public Map<String, TDataProcessNewItemSrcCQ> DataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList { get { return _dataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcListMap == null) { _dataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcListMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList." + key;
        }

        protected Map<String, TIntegConditionCQ> _dataEditId_NotInScopeSubQuery_TIntegConditionListMap;
        public Map<String, TIntegConditionCQ> DataEditId_NotInScopeSubQuery_TIntegConditionList { get { return _dataEditId_NotInScopeSubQuery_TIntegConditionListMap; }}
        public override String keepDataEditId_NotInScopeSubQuery_TIntegConditionList(TIntegConditionCQ subQuery) {
            if (_dataEditId_NotInScopeSubQuery_TIntegConditionListMap == null) { _dataEditId_NotInScopeSubQuery_TIntegConditionListMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_NotInScopeSubQuery_TIntegConditionListMap.size() + 1);
            _dataEditId_NotInScopeSubQuery_TIntegConditionListMap.put(key, subQuery); return "DataEditId_NotInScopeSubQuery_TIntegConditionList." + key;
        }

        protected Map<String, TDataProcessNewCategoryCQ> _dataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryListMap;
        public Map<String, TDataProcessNewCategoryCQ> DataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryList { get { return _dataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryListMap == null) { _dataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryListMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryList." + key;
        }

        protected Map<String, TDataProcessNewItemSrcCQ> _dataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcListMap;
        public Map<String, TDataProcessNewItemSrcCQ> DataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcList { get { return _dataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcListMap == null) { _dataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcListMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcList." + key;
        }

        protected Map<String, TIntegConditionCQ> _dataEditId_SpecifyDerivedReferrer_TIntegConditionListMap;
        public Map<String, TIntegConditionCQ> DataEditId_SpecifyDerivedReferrer_TIntegConditionList { get { return _dataEditId_SpecifyDerivedReferrer_TIntegConditionListMap; }}
        public override String keepDataEditId_SpecifyDerivedReferrer_TIntegConditionList(TIntegConditionCQ subQuery) {
            if (_dataEditId_SpecifyDerivedReferrer_TIntegConditionListMap == null) { _dataEditId_SpecifyDerivedReferrer_TIntegConditionListMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_SpecifyDerivedReferrer_TIntegConditionListMap.size() + 1);
            _dataEditId_SpecifyDerivedReferrer_TIntegConditionListMap.put(key, subQuery); return "DataEditId_SpecifyDerivedReferrer_TIntegConditionList." + key;
        }

        protected Map<String, TDataProcessNewCategoryCQ> _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListMap;
        public Map<String, TDataProcessNewCategoryCQ> DataEditId_QueryDerivedReferrer_TDataProcessNewCategoryList { get { return _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListMap == null) { _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListMap = new LinkedHashMap<String, TDataProcessNewCategoryCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TDataProcessNewCategoryList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameter { get { return _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameter." + key;
        }

        protected Map<String, TDataProcessNewItemSrcCQ> _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListMap;
        public Map<String, TDataProcessNewItemSrcCQ> DataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcList { get { return _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListMap == null) { _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListMap = new LinkedHashMap<String, TDataProcessNewItemSrcCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameter { get { return _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameter." + key;
        }

        protected Map<String, TIntegConditionCQ> _dataEditId_QueryDerivedReferrer_TIntegConditionListMap;
        public Map<String, TIntegConditionCQ> DataEditId_QueryDerivedReferrer_TIntegConditionList { get { return _dataEditId_QueryDerivedReferrer_TIntegConditionListMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TIntegConditionList(TIntegConditionCQ subQuery) {
            if (_dataEditId_QueryDerivedReferrer_TIntegConditionListMap == null) { _dataEditId_QueryDerivedReferrer_TIntegConditionListMap = new LinkedHashMap<String, TIntegConditionCQ>(); }
            String key = "subQueryMapKey" + (_dataEditId_QueryDerivedReferrer_TIntegConditionListMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TIntegConditionListMap.put(key, subQuery); return "DataEditId_QueryDerivedReferrer_TIntegConditionList." + key;
        }
        protected Map<String, Object> _dataEditId_QueryDerivedReferrer_TIntegConditionListParameterMap;
        public Map<String, Object> DataEditId_QueryDerivedReferrer_TIntegConditionListParameter { get { return _dataEditId_QueryDerivedReferrer_TIntegConditionListParameterMap; } }
        public override String keepDataEditId_QueryDerivedReferrer_TIntegConditionListParameter(Object parameterValue) {
            if (_dataEditId_QueryDerivedReferrer_TIntegConditionListParameterMap == null) { _dataEditId_QueryDerivedReferrer_TIntegConditionListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_dataEditId_QueryDerivedReferrer_TIntegConditionListParameterMap.size() + 1);
            _dataEditId_QueryDerivedReferrer_TIntegConditionListParameterMap.put(key, parameterValue); return "DataEditId_QueryDerivedReferrer_TIntegConditionListParameter." + key;
        }

        public BsTDataProcessNewItemCQ AddOrderBy_DataEditId_Asc() { regOBA("DATA_EDIT_ID");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_DataEditId_Desc() { regOBD("DATA_EDIT_ID");return this; }

        protected ConditionValue _srcItemId;
        public ConditionValue SrcItemId {
            get { if (_srcItemId == null) { _srcItemId = new ConditionValue(); } return _srcItemId; }
        }
        protected override ConditionValue getCValueSrcItemId() { return this.SrcItemId; }


        public BsTDataProcessNewItemCQ AddOrderBy_SrcItemId_Asc() { regOBA("SRC_ITEM_ID");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_SrcItemId_Desc() { regOBD("SRC_ITEM_ID");return this; }

        protected ConditionValue _newItemId;
        public ConditionValue NewItemId {
            get { if (_newItemId == null) { _newItemId = new ConditionValue(); } return _newItemId; }
        }
        protected override ConditionValue getCValueNewItemId() { return this.NewItemId; }


        public BsTDataProcessNewItemCQ AddOrderBy_NewItemId_Asc() { regOBA("NEW_ITEM_ID");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NewItemId_Desc() { regOBD("NEW_ITEM_ID");return this; }

        protected ConditionValue _newItemName;
        public ConditionValue NewItemName {
            get { if (_newItemName == null) { _newItemName = new ConditionValue(); } return _newItemName; }
        }
        protected override ConditionValue getCValueNewItemName() { return this.NewItemName; }


        public BsTDataProcessNewItemCQ AddOrderBy_NewItemName_Asc() { regOBA("NEW_ITEM_NAME");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NewItemName_Desc() { regOBD("NEW_ITEM_NAME");return this; }

        protected ConditionValue _newLv1title;
        public ConditionValue NewLv1title {
            get { if (_newLv1title == null) { _newLv1title = new ConditionValue(); } return _newLv1title; }
        }
        protected override ConditionValue getCValueNewLv1title() { return this.NewLv1title; }


        public BsTDataProcessNewItemCQ AddOrderBy_NewLv1title_Asc() { regOBA("NEW_LV1TITLE");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NewLv1title_Desc() { regOBD("NEW_LV1TITLE");return this; }

        protected ConditionValue _newLv2title;
        public ConditionValue NewLv2title {
            get { if (_newLv2title == null) { _newLv2title = new ConditionValue(); } return _newLv2title; }
        }
        protected override ConditionValue getCValueNewLv2title() { return this.NewLv2title; }


        public BsTDataProcessNewItemCQ AddOrderBy_NewLv2title_Asc() { regOBA("NEW_LV2TITLE");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NewLv2title_Desc() { regOBD("NEW_LV2TITLE");return this; }

        protected ConditionValue _newAnswerType;
        public ConditionValue NewAnswerType {
            get { if (_newAnswerType == null) { _newAnswerType = new ConditionValue(); } return _newAnswerType; }
        }
        protected override ConditionValue getCValueNewAnswerType() { return this.NewAnswerType; }


        public BsTDataProcessNewItemCQ AddOrderBy_NewAnswerType_Asc() { regOBA("NEW_ANSWER_TYPE");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NewAnswerType_Desc() { regOBD("NEW_ANSWER_TYPE");return this; }

        protected ConditionValue _newCategoryCount;
        public ConditionValue NewCategoryCount {
            get { if (_newCategoryCount == null) { _newCategoryCount = new ConditionValue(); } return _newCategoryCount; }
        }
        protected override ConditionValue getCValueNewCategoryCount() { return this.NewCategoryCount; }


        public BsTDataProcessNewItemCQ AddOrderBy_NewCategoryCount_Asc() { regOBA("NEW_CATEGORY_COUNT");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NewCategoryCount_Desc() { regOBD("NEW_CATEGORY_COUNT");return this; }

        protected ConditionValue _unfitFlag;
        public ConditionValue UnfitFlag {
            get { if (_unfitFlag == null) { _unfitFlag = new ConditionValue(); } return _unfitFlag; }
        }
        protected override ConditionValue getCValueUnfitFlag() { return this.UnfitFlag; }


        public BsTDataProcessNewItemCQ AddOrderBy_UnfitFlag_Asc() { regOBA("UNFIT_FLAG");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_UnfitFlag_Desc() { regOBD("UNFIT_FLAG");return this; }

        protected ConditionValue _conditionDiv;
        public ConditionValue ConditionDiv {
            get { if (_conditionDiv == null) { _conditionDiv = new ConditionValue(); } return _conditionDiv; }
        }
        protected override ConditionValue getCValueConditionDiv() { return this.ConditionDiv; }


        public BsTDataProcessNewItemCQ AddOrderBy_ConditionDiv_Asc() { regOBA("CONDITION_DIV");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_ConditionDiv_Desc() { regOBD("CONDITION_DIV");return this; }

        protected ConditionValue _seriesFlag;
        public ConditionValue SeriesFlag {
            get { if (_seriesFlag == null) { _seriesFlag = new ConditionValue(); } return _seriesFlag; }
        }
        protected override ConditionValue getCValueSeriesFlag() { return this.SeriesFlag; }


        public BsTDataProcessNewItemCQ AddOrderBy_SeriesFlag_Asc() { regOBA("SERIES_FLAG");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_SeriesFlag_Desc() { regOBD("SERIES_FLAG");return this; }

        protected ConditionValue _upperFlag;
        public ConditionValue UpperFlag {
            get { if (_upperFlag == null) { _upperFlag = new ConditionValue(); } return _upperFlag; }
        }
        protected override ConditionValue getCValueUpperFlag() { return this.UpperFlag; }


        public BsTDataProcessNewItemCQ AddOrderBy_UpperFlag_Asc() { regOBA("UPPER_FLAG");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_UpperFlag_Desc() { regOBD("UPPER_FLAG");return this; }

        protected ConditionValue _bottomFlag;
        public ConditionValue BottomFlag {
            get { if (_bottomFlag == null) { _bottomFlag = new ConditionValue(); } return _bottomFlag; }
        }
        protected override ConditionValue getCValueBottomFlag() { return this.BottomFlag; }


        public BsTDataProcessNewItemCQ AddOrderBy_BottomFlag_Asc() { regOBA("BOTTOM_FLAG");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_BottomFlag_Desc() { regOBD("BOTTOM_FLAG");return this; }

        protected ConditionValue _noanswerZeroFlag;
        public ConditionValue NoanswerZeroFlag {
            get { if (_noanswerZeroFlag == null) { _noanswerZeroFlag = new ConditionValue(); } return _noanswerZeroFlag; }
        }
        protected override ConditionValue getCValueNoanswerZeroFlag() { return this.NoanswerZeroFlag; }


        public BsTDataProcessNewItemCQ AddOrderBy_NoanswerZeroFlag_Asc() { regOBA("NOANSWER_ZERO_FLAG");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_NoanswerZeroFlag_Desc() { regOBD("NOANSWER_ZERO_FLAG");return this; }

        protected ConditionValue _selectMethod;
        public ConditionValue SelectMethod {
            get { if (_selectMethod == null) { _selectMethod = new ConditionValue(); } return _selectMethod; }
        }
        protected override ConditionValue getCValueSelectMethod() { return this.SelectMethod; }


        public BsTDataProcessNewItemCQ AddOrderBy_SelectMethod_Asc() { regOBA("SELECT_METHOD");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_SelectMethod_Desc() { regOBD("SELECT_METHOD");return this; }

        protected ConditionValue _targetCategoryCondition;
        public ConditionValue TargetCategoryCondition {
            get { if (_targetCategoryCondition == null) { _targetCategoryCondition = new ConditionValue(); } return _targetCategoryCondition; }
        }
        protected override ConditionValue getCValueTargetCategoryCondition() { return this.TargetCategoryCondition; }


        public BsTDataProcessNewItemCQ AddOrderBy_TargetCategoryCondition_Asc() { regOBA("TARGET_CATEGORY_CONDITION");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_TargetCategoryCondition_Desc() { regOBD("TARGET_CATEGORY_CONDITION");return this; }

        protected ConditionValue _calcType;
        public ConditionValue CalcType {
            get { if (_calcType == null) { _calcType = new ConditionValue(); } return _calcType; }
        }
        protected override ConditionValue getCValueCalcType() { return this.CalcType; }


        public BsTDataProcessNewItemCQ AddOrderBy_CalcType_Asc() { regOBA("CALC_TYPE");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_CalcType_Desc() { regOBD("CALC_TYPE");return this; }

        protected ConditionValue _formulaString;
        public ConditionValue FormulaString {
            get { if (_formulaString == null) { _formulaString = new ConditionValue(); } return _formulaString; }
        }
        protected override ConditionValue getCValueFormulaString() { return this.FormulaString; }


        public BsTDataProcessNewItemCQ AddOrderBy_FormulaString_Asc() { regOBA("FORMULA_STRING");return this; }
        public BsTDataProcessNewItemCQ AddOrderBy_FormulaString_Desc() { regOBD("FORMULA_STRING");return this; }

        public BsTDataProcessNewItemCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDataProcessNewItemCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDataProcessNewItemCQ baseQuery = (TDataProcessNewItemCQ)baseQueryAsSuper;
            TDataProcessNewItemCQ unionQuery = (TDataProcessNewItemCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDataEditList()) {
                unionQuery.QueryTDataEditList().reflectRelationOnUnionQuery(baseQuery.QueryTDataEditList(), unionQuery.QueryTDataEditList());
            }

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
            return resolveNextRelationPath("T_DATA_PROCESS_NEW_ITEM", "tDataEditList");
        }
        public bool hasConditionQueryTDataEditList() {
            return _conditionQueryTDataEditList != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDataProcessNewItemCQ> _scalarSubQueryMap;
	    public Map<String, TDataProcessNewItemCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDataProcessNewItemCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDataProcessNewItemCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDataProcessNewItemCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDataProcessNewItemCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDataProcessNewItemCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
