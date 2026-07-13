
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTCategoryInfoCQ : AbstractBsTCategoryInfoCQ {

        protected TCategoryInfoCIQ _inlineQuery;

        public BsTCategoryInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TCategoryInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TCategoryInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TCategoryInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TCategoryInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _categoryInfoId;
        public ConditionValue CategoryInfoId {
            get { if (_categoryInfoId == null) { _categoryInfoId = new ConditionValue(); } return _categoryInfoId; }
        }
        protected override ConditionValue getCValueCategoryInfoId() { return this.CategoryInfoId; }


        protected Map<String, TMatrixInfoCQ> _categoryInfoId_ExistsSubQuery_TMatrixInfoListMap;
        public Map<String, TMatrixInfoCQ> CategoryInfoId_ExistsSubQuery_TMatrixInfoList { get { return _categoryInfoId_ExistsSubQuery_TMatrixInfoListMap; }}
        public override String keepCategoryInfoId_ExistsSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery) {
            if (_categoryInfoId_ExistsSubQuery_TMatrixInfoListMap == null) { _categoryInfoId_ExistsSubQuery_TMatrixInfoListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_categoryInfoId_ExistsSubQuery_TMatrixInfoListMap.size() + 1);
            _categoryInfoId_ExistsSubQuery_TMatrixInfoListMap.put(key, subQuery); return "CategoryInfoId_ExistsSubQuery_TMatrixInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _categoryInfoId_NotExistsSubQuery_TMatrixInfoListMap;
        public Map<String, TMatrixInfoCQ> CategoryInfoId_NotExistsSubQuery_TMatrixInfoList { get { return _categoryInfoId_NotExistsSubQuery_TMatrixInfoListMap; }}
        public override String keepCategoryInfoId_NotExistsSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery) {
            if (_categoryInfoId_NotExistsSubQuery_TMatrixInfoListMap == null) { _categoryInfoId_NotExistsSubQuery_TMatrixInfoListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_categoryInfoId_NotExistsSubQuery_TMatrixInfoListMap.size() + 1);
            _categoryInfoId_NotExistsSubQuery_TMatrixInfoListMap.put(key, subQuery); return "CategoryInfoId_NotExistsSubQuery_TMatrixInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _categoryInfoId_InScopeSubQuery_TMatrixInfoListMap;
        public Map<String, TMatrixInfoCQ> CategoryInfoId_InScopeSubQuery_TMatrixInfoList { get { return _categoryInfoId_InScopeSubQuery_TMatrixInfoListMap; }}
        public override String keepCategoryInfoId_InScopeSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery) {
            if (_categoryInfoId_InScopeSubQuery_TMatrixInfoListMap == null) { _categoryInfoId_InScopeSubQuery_TMatrixInfoListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_categoryInfoId_InScopeSubQuery_TMatrixInfoListMap.size() + 1);
            _categoryInfoId_InScopeSubQuery_TMatrixInfoListMap.put(key, subQuery); return "CategoryInfoId_InScopeSubQuery_TMatrixInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _categoryInfoId_NotInScopeSubQuery_TMatrixInfoListMap;
        public Map<String, TMatrixInfoCQ> CategoryInfoId_NotInScopeSubQuery_TMatrixInfoList { get { return _categoryInfoId_NotInScopeSubQuery_TMatrixInfoListMap; }}
        public override String keepCategoryInfoId_NotInScopeSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery) {
            if (_categoryInfoId_NotInScopeSubQuery_TMatrixInfoListMap == null) { _categoryInfoId_NotInScopeSubQuery_TMatrixInfoListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_categoryInfoId_NotInScopeSubQuery_TMatrixInfoListMap.size() + 1);
            _categoryInfoId_NotInScopeSubQuery_TMatrixInfoListMap.put(key, subQuery); return "CategoryInfoId_NotInScopeSubQuery_TMatrixInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _categoryInfoId_SpecifyDerivedReferrer_TMatrixInfoListMap;
        public Map<String, TMatrixInfoCQ> CategoryInfoId_SpecifyDerivedReferrer_TMatrixInfoList { get { return _categoryInfoId_SpecifyDerivedReferrer_TMatrixInfoListMap; }}
        public override String keepCategoryInfoId_SpecifyDerivedReferrer_TMatrixInfoList(TMatrixInfoCQ subQuery) {
            if (_categoryInfoId_SpecifyDerivedReferrer_TMatrixInfoListMap == null) { _categoryInfoId_SpecifyDerivedReferrer_TMatrixInfoListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_categoryInfoId_SpecifyDerivedReferrer_TMatrixInfoListMap.size() + 1);
            _categoryInfoId_SpecifyDerivedReferrer_TMatrixInfoListMap.put(key, subQuery); return "CategoryInfoId_SpecifyDerivedReferrer_TMatrixInfoList." + key;
        }

        protected Map<String, TMatrixInfoCQ> _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListMap;
        public Map<String, TMatrixInfoCQ> CategoryInfoId_QueryDerivedReferrer_TMatrixInfoList { get { return _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListMap; } }
        public override String keepCategoryInfoId_QueryDerivedReferrer_TMatrixInfoList(TMatrixInfoCQ subQuery) {
            if (_categoryInfoId_QueryDerivedReferrer_TMatrixInfoListMap == null) { _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListMap = new LinkedHashMap<String, TMatrixInfoCQ>(); }
            String key = "subQueryMapKey" + (_categoryInfoId_QueryDerivedReferrer_TMatrixInfoListMap.size() + 1);
            _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListMap.put(key, subQuery); return "CategoryInfoId_QueryDerivedReferrer_TMatrixInfoList." + key;
        }
        protected Map<String, Object> _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameterMap;
        public Map<String, Object> CategoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameter { get { return _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameterMap; } }
        public override String keepCategoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameter(Object parameterValue) {
            if (_categoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameterMap == null) { _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_categoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameterMap.size() + 1);
            _categoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameterMap.put(key, parameterValue); return "CategoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameter." + key;
        }

        public BsTCategoryInfoCQ AddOrderBy_CategoryInfoId_Asc() { regOBA("CATEGORY_INFO_ID");return this; }
        public BsTCategoryInfoCQ AddOrderBy_CategoryInfoId_Desc() { regOBD("CATEGORY_INFO_ID");return this; }

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

        public BsTCategoryInfoCQ AddOrderBy_ItemInfoId_Asc() { regOBA("ITEM_INFO_ID");return this; }
        public BsTCategoryInfoCQ AddOrderBy_ItemInfoId_Desc() { regOBD("ITEM_INFO_ID");return this; }

        protected ConditionValue _categoryNo;
        public ConditionValue CategoryNo {
            get { if (_categoryNo == null) { _categoryNo = new ConditionValue(); } return _categoryNo; }
        }
        protected override ConditionValue getCValueCategoryNo() { return this.CategoryNo; }


        public BsTCategoryInfoCQ AddOrderBy_CategoryNo_Asc() { regOBA("CATEGORY_NO");return this; }
        public BsTCategoryInfoCQ AddOrderBy_CategoryNo_Desc() { regOBD("CATEGORY_NO");return this; }

        protected ConditionValue _categoryName;
        public ConditionValue CategoryName {
            get { if (_categoryName == null) { _categoryName = new ConditionValue(); } return _categoryName; }
        }
        protected override ConditionValue getCValueCategoryName() { return this.CategoryName; }


        public BsTCategoryInfoCQ AddOrderBy_CategoryName_Asc() { regOBA("CATEGORY_NAME");return this; }
        public BsTCategoryInfoCQ AddOrderBy_CategoryName_Desc() { regOBD("CATEGORY_NAME");return this; }

        protected ConditionValue _weightValue;
        public ConditionValue WeightValue {
            get { if (_weightValue == null) { _weightValue = new ConditionValue(); } return _weightValue; }
        }
        protected override ConditionValue getCValueWeightValue() { return this.WeightValue; }


        public BsTCategoryInfoCQ AddOrderBy_WeightValue_Asc() { regOBA("WEIGHT_VALUE");return this; }
        public BsTCategoryInfoCQ AddOrderBy_WeightValue_Desc() { regOBD("WEIGHT_VALUE");return this; }

        protected ConditionValue _originalCategoryName;
        public ConditionValue OriginalCategoryName {
            get { if (_originalCategoryName == null) { _originalCategoryName = new ConditionValue(); } return _originalCategoryName; }
        }
        protected override ConditionValue getCValueOriginalCategoryName() { return this.OriginalCategoryName; }


        public BsTCategoryInfoCQ AddOrderBy_OriginalCategoryName_Asc() { regOBA("ORIGINAL_CATEGORY_NAME");return this; }
        public BsTCategoryInfoCQ AddOrderBy_OriginalCategoryName_Desc() { regOBD("ORIGINAL_CATEGORY_NAME");return this; }

        protected ConditionValue _originalWeightValue;
        public ConditionValue OriginalWeightValue {
            get { if (_originalWeightValue == null) { _originalWeightValue = new ConditionValue(); } return _originalWeightValue; }
        }
        protected override ConditionValue getCValueOriginalWeightValue() { return this.OriginalWeightValue; }


        public BsTCategoryInfoCQ AddOrderBy_OriginalWeightValue_Asc() { regOBA("ORIGINAL_WEIGHT_VALUE");return this; }
        public BsTCategoryInfoCQ AddOrderBy_OriginalWeightValue_Desc() { regOBD("ORIGINAL_WEIGHT_VALUE");return this; }

        public BsTCategoryInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTCategoryInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TCategoryInfoCQ baseQuery = (TCategoryInfoCQ)baseQueryAsSuper;
            TCategoryInfoCQ unionQuery = (TCategoryInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTItemInfo()) {
                unionQuery.QueryTItemInfo().reflectRelationOnUnionQuery(baseQuery.QueryTItemInfo(), unionQuery.QueryTItemInfo());
            }

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
            return resolveNextRelationPath("T_CATEGORY_INFO", "tItemInfo");
        }
        public bool hasConditionQueryTItemInfo() {
            return _conditionQueryTItemInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TCategoryInfoCQ> _scalarSubQueryMap;
	    public Map<String, TCategoryInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TCategoryInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TCategoryInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TCategoryInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TCategoryInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TCategoryInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
