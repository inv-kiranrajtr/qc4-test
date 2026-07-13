
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTTableControlCQ : AbstractBsTTableControlCQ {

        protected TTableControlCIQ _inlineQuery;

        public BsTTableControlCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TTableControlCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TTableControlCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TTableControlCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TTableControlCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TTableDetailInfoCQ> _qcwebid_ExistsSubQuery_TTableDetailInfoListMap;
        public Map<String, TTableDetailInfoCQ> Qcwebid_ExistsSubQuery_TTableDetailInfoList { get { return _qcwebid_ExistsSubQuery_TTableDetailInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TTableDetailInfoListMap == null) { _qcwebid_ExistsSubQuery_TTableDetailInfoListMap = new LinkedHashMap<String, TTableDetailInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TTableDetailInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TTableDetailInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TTableDetailInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_ExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_ExistsSubQuery_TItemInfoList { get { return _qcwebid_ExistsSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TItemInfoListMap == null) { _qcwebid_ExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TTableDetailInfoCQ> _qcwebid_NotExistsSubQuery_TTableDetailInfoListMap;
        public Map<String, TTableDetailInfoCQ> Qcwebid_NotExistsSubQuery_TTableDetailInfoList { get { return _qcwebid_NotExistsSubQuery_TTableDetailInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TTableDetailInfoListMap == null) { _qcwebid_NotExistsSubQuery_TTableDetailInfoListMap = new LinkedHashMap<String, TTableDetailInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TTableDetailInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TTableDetailInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TTableDetailInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_NotExistsSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_NotExistsSubQuery_TItemInfoList { get { return _qcwebid_NotExistsSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TItemInfoListMap == null) { _qcwebid_NotExistsSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TTableDetailInfoCQ> _qcwebid_InScopeSubQuery_TTableDetailInfoListMap;
        public Map<String, TTableDetailInfoCQ> Qcwebid_InScopeSubQuery_TTableDetailInfoList { get { return _qcwebid_InScopeSubQuery_TTableDetailInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TTableDetailInfoListMap == null) { _qcwebid_InScopeSubQuery_TTableDetailInfoListMap = new LinkedHashMap<String, TTableDetailInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TTableDetailInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TTableDetailInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TTableDetailInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_InScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_InScopeSubQuery_TItemInfoList { get { return _qcwebid_InScopeSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TItemInfoListMap == null) { _qcwebid_InScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TTableDetailInfoCQ> _qcwebid_NotInScopeSubQuery_TTableDetailInfoListMap;
        public Map<String, TTableDetailInfoCQ> Qcwebid_NotInScopeSubQuery_TTableDetailInfoList { get { return _qcwebid_NotInScopeSubQuery_TTableDetailInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TTableDetailInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TTableDetailInfoListMap = new LinkedHashMap<String, TTableDetailInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TTableDetailInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TTableDetailInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TTableDetailInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_NotInScopeSubQuery_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_NotInScopeSubQuery_TItemInfoList { get { return _qcwebid_NotInScopeSubQuery_TItemInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TItemInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TItemInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TItemInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TItemInfoList." + key;
        }

        protected Map<String, TTableDetailInfoCQ> _qcwebid_SpecifyDerivedReferrer_TTableDetailInfoListMap;
        public Map<String, TTableDetailInfoCQ> Qcwebid_SpecifyDerivedReferrer_TTableDetailInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TTableDetailInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TTableDetailInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TTableDetailInfoListMap = new LinkedHashMap<String, TTableDetailInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TTableDetailInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TTableDetailInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TTableDetailInfoList." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_SpecifyDerivedReferrer_TItemInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TItemInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TItemInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TItemInfoList." + key;
        }

        protected Map<String, TTableDetailInfoCQ> _qcwebid_QueryDerivedReferrer_TTableDetailInfoListMap;
        public Map<String, TTableDetailInfoCQ> Qcwebid_QueryDerivedReferrer_TTableDetailInfoList { get { return _qcwebid_QueryDerivedReferrer_TTableDetailInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TTableDetailInfoList(TTableDetailInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TTableDetailInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TTableDetailInfoListMap = new LinkedHashMap<String, TTableDetailInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TTableDetailInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TTableDetailInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TTableDetailInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TTableDetailInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TTableDetailInfoListParameter." + key;
        }

        protected Map<String, TItemInfoCQ> _qcwebid_QueryDerivedReferrer_TItemInfoListMap;
        public Map<String, TItemInfoCQ> Qcwebid_QueryDerivedReferrer_TItemInfoList { get { return _qcwebid_QueryDerivedReferrer_TItemInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TItemInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TItemInfoListMap = new LinkedHashMap<String, TItemInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TItemInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TItemInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TItemInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TItemInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TItemInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TItemInfoListParameter." + key;
        }

        public BsTTableControlCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTTableControlCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _baseTableName;
        public ConditionValue BaseTableName {
            get { if (_baseTableName == null) { _baseTableName = new ConditionValue(); } return _baseTableName; }
        }
        protected override ConditionValue getCValueBaseTableName() { return this.BaseTableName; }


        public BsTTableControlCQ AddOrderBy_BaseTableName_Asc() { regOBA("BASE_TABLE_NAME");return this; }
        public BsTTableControlCQ AddOrderBy_BaseTableName_Desc() { regOBD("BASE_TABLE_NAME");return this; }

        protected ConditionValue _activeTableNo;
        public ConditionValue ActiveTableNo {
            get { if (_activeTableNo == null) { _activeTableNo = new ConditionValue(); } return _activeTableNo; }
        }
        protected override ConditionValue getCValueActiveTableNo() { return this.ActiveTableNo; }


        public BsTTableControlCQ AddOrderBy_ActiveTableNo_Asc() { regOBA("ACTIVE_TABLE_NO");return this; }
        public BsTTableControlCQ AddOrderBy_ActiveTableNo_Desc() { regOBD("ACTIVE_TABLE_NO");return this; }

        protected ConditionValue _maxNo;
        public ConditionValue MaxNo {
            get { if (_maxNo == null) { _maxNo = new ConditionValue(); } return _maxNo; }
        }
        protected override ConditionValue getCValueMaxNo() { return this.MaxNo; }


        public BsTTableControlCQ AddOrderBy_MaxNo_Asc() { regOBA("MAX_NO");return this; }
        public BsTTableControlCQ AddOrderBy_MaxNo_Desc() { regOBD("MAX_NO");return this; }

        public BsTTableControlCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTTableControlCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TTableControlCQ baseQuery = (TTableControlCQ)baseQueryAsSuper;
            TTableControlCQ unionQuery = (TTableControlCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfoAsOne()) {
                unionQuery.QueryTQcwebSurveyInfoAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfoAsOne(), unionQuery.QueryTQcwebSurveyInfoAsOne());
            }

        }
    


        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfoAsOne {
            get {
                if (_conditionQueryTQcwebSurveyInfoAsOne == null) {
                    _conditionQueryTQcwebSurveyInfoAsOne = createQueryTQcwebSurveyInfoAsOne();
                    xsetupOuterJoin_TQcwebSurveyInfoAsOne();
                }
                return _conditionQueryTQcwebSurveyInfoAsOne;
            }
        }
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfoAsOne() { return this.ConditionQueryTQcwebSurveyInfoAsOne; }
        protected TQcwebSurveyInfoCQ createQueryTQcwebSurveyInfoAsOne() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfoAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfoAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfoAsOne() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfoAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfoAsOne() {
            return resolveNextRelationPath("T_TABLE_CONTROL", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TTableControlCQ> _scalarSubQueryMap;
	    public Map<String, TTableControlCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TTableControlCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TTableControlCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TTableControlCQ> _myselfInScopeSubQueryMap;
        public Map<String, TTableControlCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TTableControlCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TTableControlCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
