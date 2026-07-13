
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTEditMenuMasterCQ : AbstractBsTEditMenuMasterCQ {

        protected TEditMenuMasterCIQ _inlineQuery;

        public BsTEditMenuMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TEditMenuMasterCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TEditMenuMasterCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TEditMenuMasterCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TEditMenuMasterCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _editMenuMasterId;
        public ConditionValue EditMenuMasterId {
            get { if (_editMenuMasterId == null) { _editMenuMasterId = new ConditionValue(); } return _editMenuMasterId; }
        }
        protected override ConditionValue getCValueEditMenuMasterId() { return this.EditMenuMasterId; }


        protected Map<String, TDataEditListCQ> _editMenuMasterId_ExistsSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> EditMenuMasterId_ExistsSubQuery_TDataEditListList { get { return _editMenuMasterId_ExistsSubQuery_TDataEditListListMap; }}
        public override String keepEditMenuMasterId_ExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_editMenuMasterId_ExistsSubQuery_TDataEditListListMap == null) { _editMenuMasterId_ExistsSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_ExistsSubQuery_TDataEditListListMap.size() + 1);
            _editMenuMasterId_ExistsSubQuery_TDataEditListListMap.put(key, subQuery); return "EditMenuMasterId_ExistsSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TDataEditListCQ> _editMenuMasterId_NotExistsSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> EditMenuMasterId_NotExistsSubQuery_TDataEditListList { get { return _editMenuMasterId_NotExistsSubQuery_TDataEditListListMap; }}
        public override String keepEditMenuMasterId_NotExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_editMenuMasterId_NotExistsSubQuery_TDataEditListListMap == null) { _editMenuMasterId_NotExistsSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_NotExistsSubQuery_TDataEditListListMap.size() + 1);
            _editMenuMasterId_NotExistsSubQuery_TDataEditListListMap.put(key, subQuery); return "EditMenuMasterId_NotExistsSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TDataEditListCQ> _editMenuMasterId_InScopeSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> EditMenuMasterId_InScopeSubQuery_TDataEditListList { get { return _editMenuMasterId_InScopeSubQuery_TDataEditListListMap; }}
        public override String keepEditMenuMasterId_InScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_editMenuMasterId_InScopeSubQuery_TDataEditListListMap == null) { _editMenuMasterId_InScopeSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_InScopeSubQuery_TDataEditListListMap.size() + 1);
            _editMenuMasterId_InScopeSubQuery_TDataEditListListMap.put(key, subQuery); return "EditMenuMasterId_InScopeSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TDataEditListCQ> _editMenuMasterId_NotInScopeSubQuery_TDataEditListListMap;
        public Map<String, TDataEditListCQ> EditMenuMasterId_NotInScopeSubQuery_TDataEditListList { get { return _editMenuMasterId_NotInScopeSubQuery_TDataEditListListMap; }}
        public override String keepEditMenuMasterId_NotInScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery) {
            if (_editMenuMasterId_NotInScopeSubQuery_TDataEditListListMap == null) { _editMenuMasterId_NotInScopeSubQuery_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_NotInScopeSubQuery_TDataEditListListMap.size() + 1);
            _editMenuMasterId_NotInScopeSubQuery_TDataEditListListMap.put(key, subQuery); return "EditMenuMasterId_NotInScopeSubQuery_TDataEditListList." + key;
        }

        protected Map<String, TDataEditListCQ> _editMenuMasterId_SpecifyDerivedReferrer_TDataEditListListMap;
        public Map<String, TDataEditListCQ> EditMenuMasterId_SpecifyDerivedReferrer_TDataEditListList { get { return _editMenuMasterId_SpecifyDerivedReferrer_TDataEditListListMap; }}
        public override String keepEditMenuMasterId_SpecifyDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery) {
            if (_editMenuMasterId_SpecifyDerivedReferrer_TDataEditListListMap == null) { _editMenuMasterId_SpecifyDerivedReferrer_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_SpecifyDerivedReferrer_TDataEditListListMap.size() + 1);
            _editMenuMasterId_SpecifyDerivedReferrer_TDataEditListListMap.put(key, subQuery); return "EditMenuMasterId_SpecifyDerivedReferrer_TDataEditListList." + key;
        }

        protected Map<String, TDataEditListCQ> _editMenuMasterId_QueryDerivedReferrer_TDataEditListListMap;
        public Map<String, TDataEditListCQ> EditMenuMasterId_QueryDerivedReferrer_TDataEditListList { get { return _editMenuMasterId_QueryDerivedReferrer_TDataEditListListMap; } }
        public override String keepEditMenuMasterId_QueryDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery) {
            if (_editMenuMasterId_QueryDerivedReferrer_TDataEditListListMap == null) { _editMenuMasterId_QueryDerivedReferrer_TDataEditListListMap = new LinkedHashMap<String, TDataEditListCQ>(); }
            String key = "subQueryMapKey" + (_editMenuMasterId_QueryDerivedReferrer_TDataEditListListMap.size() + 1);
            _editMenuMasterId_QueryDerivedReferrer_TDataEditListListMap.put(key, subQuery); return "EditMenuMasterId_QueryDerivedReferrer_TDataEditListList." + key;
        }
        protected Map<String, Object> _editMenuMasterId_QueryDerivedReferrer_TDataEditListListParameterMap;
        public Map<String, Object> EditMenuMasterId_QueryDerivedReferrer_TDataEditListListParameter { get { return _editMenuMasterId_QueryDerivedReferrer_TDataEditListListParameterMap; } }
        public override String keepEditMenuMasterId_QueryDerivedReferrer_TDataEditListListParameter(Object parameterValue) {
            if (_editMenuMasterId_QueryDerivedReferrer_TDataEditListListParameterMap == null) { _editMenuMasterId_QueryDerivedReferrer_TDataEditListListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_editMenuMasterId_QueryDerivedReferrer_TDataEditListListParameterMap.size() + 1);
            _editMenuMasterId_QueryDerivedReferrer_TDataEditListListParameterMap.put(key, parameterValue); return "EditMenuMasterId_QueryDerivedReferrer_TDataEditListListParameter." + key;
        }

        public BsTEditMenuMasterCQ AddOrderBy_EditMenuMasterId_Asc() { regOBA("EDIT_MENU_MASTER_ID");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_EditMenuMasterId_Desc() { regOBD("EDIT_MENU_MASTER_ID");return this; }

        protected ConditionValue _editClassification;
        public ConditionValue EditClassification {
            get { if (_editClassification == null) { _editClassification = new ConditionValue(); } return _editClassification; }
        }
        protected override ConditionValue getCValueEditClassification() { return this.EditClassification; }


        public BsTEditMenuMasterCQ AddOrderBy_EditClassification_Asc() { regOBA("EDIT_CLASSIFICATION");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_EditClassification_Desc() { regOBD("EDIT_CLASSIFICATION");return this; }

        protected ConditionValue _processType;
        public ConditionValue ProcessType {
            get { if (_processType == null) { _processType = new ConditionValue(); } return _processType; }
        }
        protected override ConditionValue getCValueProcessType() { return this.ProcessType; }


        public BsTEditMenuMasterCQ AddOrderBy_ProcessType_Asc() { regOBA("PROCESS_TYPE");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_ProcessType_Desc() { regOBD("PROCESS_TYPE");return this; }

        protected ConditionValue _explanation;
        public ConditionValue Explanation {
            get { if (_explanation == null) { _explanation = new ConditionValue(); } return _explanation; }
        }
        protected override ConditionValue getCValueExplanation() { return this.Explanation; }


        public BsTEditMenuMasterCQ AddOrderBy_Explanation_Asc() { regOBA("EXPLANATION");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_Explanation_Desc() { regOBD("EXPLANATION");return this; }

        protected ConditionValue _example;
        public ConditionValue Example {
            get { if (_example == null) { _example = new ConditionValue(); } return _example; }
        }
        protected override ConditionValue getCValueExample() { return this.Example; }


        public BsTEditMenuMasterCQ AddOrderBy_Example_Asc() { regOBA("EXAMPLE");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_Example_Desc() { regOBD("EXAMPLE");return this; }

        protected ConditionValue _detailedexplanation;
        public ConditionValue Detailedexplanation {
            get { if (_detailedexplanation == null) { _detailedexplanation = new ConditionValue(); } return _detailedexplanation; }
        }
        protected override ConditionValue getCValueDetailedexplanation() { return this.Detailedexplanation; }


        public BsTEditMenuMasterCQ AddOrderBy_Detailedexplanation_Asc() { regOBA("DETAILEDEXPLANATION");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_Detailedexplanation_Desc() { regOBD("DETAILEDEXPLANATION");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTEditMenuMasterCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _typeBitUnion;
        public ConditionValue TypeBitUnion {
            get { if (_typeBitUnion == null) { _typeBitUnion = new ConditionValue(); } return _typeBitUnion; }
        }
        protected override ConditionValue getCValueTypeBitUnion() { return this.TypeBitUnion; }


        public BsTEditMenuMasterCQ AddOrderBy_TypeBitUnion_Asc() { regOBA("TYPE_BIT_UNION");return this; }
        public BsTEditMenuMasterCQ AddOrderBy_TypeBitUnion_Desc() { regOBD("TYPE_BIT_UNION");return this; }

        public BsTEditMenuMasterCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTEditMenuMasterCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TEditMenuMasterCQ> _scalarSubQueryMap;
	    public Map<String, TEditMenuMasterCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TEditMenuMasterCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TEditMenuMasterCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TEditMenuMasterCQ> _myselfInScopeSubQueryMap;
        public Map<String, TEditMenuMasterCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TEditMenuMasterCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TEditMenuMasterCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
