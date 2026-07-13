
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDefaultEnvColorInfoCCQ : AbstractBsTDefaultEnvColorInfoCCQ {

        protected TDefaultEnvColorInfoCCIQ _inlineQuery;

        public BsTDefaultEnvColorInfoCCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDefaultEnvColorInfoCCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDefaultEnvColorInfoCCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDefaultEnvColorInfoCCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDefaultEnvColorInfoCCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _defEnvColorInfoCId;
        public ConditionValue DefEnvColorInfoCId {
            get { if (_defEnvColorInfoCId == null) { _defEnvColorInfoCId = new ConditionValue(); } return _defEnvColorInfoCId; }
        }
        protected override ConditionValue getCValueDefEnvColorInfoCId() { return this.DefEnvColorInfoCId; }


        protected Map<String, TDefaultEnvColorDtlCCQ> _defEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCListMap;
        public Map<String, TDefaultEnvColorDtlCCQ> DefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList { get { return _defEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCListMap; }}
        public override String keepDefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            if (_defEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCListMap == null) { _defEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCListMap.size() + 1);
            _defEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCListMap.put(key, subQuery); return "DefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCCQ> _defEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCListMap;
        public Map<String, TDefaultEnvColorDtlCCQ> DefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList { get { return _defEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCListMap; }}
        public override String keepDefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            if (_defEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCListMap == null) { _defEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCListMap.size() + 1);
            _defEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCListMap.put(key, subQuery); return "DefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCCQ> _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCListMap;
        public Map<String, TDefaultEnvColorDtlCCQ> DefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList { get { return _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCListMap; }}
        public override String keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            if (_defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCListMap == null) { _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCListMap.size() + 1);
            _defEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCListMap.put(key, subQuery); return "DefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCCQ> _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCListMap;
        public Map<String, TDefaultEnvColorDtlCCQ> DefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList { get { return _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCListMap; }}
        public override String keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            if (_defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCListMap == null) { _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCListMap.size() + 1);
            _defEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCListMap.put(key, subQuery); return "DefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCCQ> _defEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCListMap;
        public Map<String, TDefaultEnvColorDtlCCQ> DefEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCList { get { return _defEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCListMap; }}
        public override String keepDefEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            if (_defEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCListMap == null) { _defEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCListMap.size() + 1);
            _defEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCListMap.put(key, subQuery); return "DefEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCCQ> _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListMap;
        public Map<String, TDefaultEnvColorDtlCCQ> DefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCList { get { return _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListMap; } }
        public override String keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery) {
            if (_defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListMap == null) { _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListMap.size() + 1);
            _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListMap.put(key, subQuery); return "DefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCList." + key;
        }
        protected Map<String, Object> _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameterMap;
        public Map<String, Object> DefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameter { get { return _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameterMap; } }
        public override String keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameter(Object parameterValue) {
            if (_defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameterMap == null) { _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameterMap.size() + 1);
            _defEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameterMap.put(key, parameterValue); return "DefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameter." + key;
        }

        public BsTDefaultEnvColorInfoCCQ AddOrderBy_DefEnvColorInfoCId_Asc() { regOBA("DEF_ENV_COLOR_INFO_C_ID");return this; }
        public BsTDefaultEnvColorInfoCCQ AddOrderBy_DefEnvColorInfoCId_Desc() { regOBD("DEF_ENV_COLOR_INFO_C_ID");return this; }

        protected ConditionValue _language;
        public ConditionValue Language {
            get { if (_language == null) { _language = new ConditionValue(); } return _language; }
        }
        protected override ConditionValue getCValueLanguage() { return this.Language; }


        protected Map<String, TDefaultEnvBaseCQ> _language_InScopeSubQuery_TDefaultEnvBaseMap;
        public Map<String, TDefaultEnvBaseCQ> Language_InScopeSubQuery_TDefaultEnvBase { get { return _language_InScopeSubQuery_TDefaultEnvBaseMap; }}
        public override String keepLanguage_InScopeSubQuery_TDefaultEnvBase(TDefaultEnvBaseCQ subQuery) {
            if (_language_InScopeSubQuery_TDefaultEnvBaseMap == null) { _language_InScopeSubQuery_TDefaultEnvBaseMap = new LinkedHashMap<String, TDefaultEnvBaseCQ>(); }
            String key = "subQueryMapKey" + (_language_InScopeSubQuery_TDefaultEnvBaseMap.size() + 1);
            _language_InScopeSubQuery_TDefaultEnvBaseMap.put(key, subQuery); return "Language_InScopeSubQuery_TDefaultEnvBase." + key;
        }

        protected Map<String, TDefaultEnvBaseCQ> _language_NotInScopeSubQuery_TDefaultEnvBaseMap;
        public Map<String, TDefaultEnvBaseCQ> Language_NotInScopeSubQuery_TDefaultEnvBase { get { return _language_NotInScopeSubQuery_TDefaultEnvBaseMap; }}
        public override String keepLanguage_NotInScopeSubQuery_TDefaultEnvBase(TDefaultEnvBaseCQ subQuery) {
            if (_language_NotInScopeSubQuery_TDefaultEnvBaseMap == null) { _language_NotInScopeSubQuery_TDefaultEnvBaseMap = new LinkedHashMap<String, TDefaultEnvBaseCQ>(); }
            String key = "subQueryMapKey" + (_language_NotInScopeSubQuery_TDefaultEnvBaseMap.size() + 1);
            _language_NotInScopeSubQuery_TDefaultEnvBaseMap.put(key, subQuery); return "Language_NotInScopeSubQuery_TDefaultEnvBase." + key;
        }

        public BsTDefaultEnvColorInfoCCQ AddOrderBy_Language_Asc() { regOBA("LANGUAGE");return this; }
        public BsTDefaultEnvColorInfoCCQ AddOrderBy_Language_Desc() { regOBD("LANGUAGE");return this; }

        protected ConditionValue _typeCode;
        public ConditionValue TypeCode {
            get { if (_typeCode == null) { _typeCode = new ConditionValue(); } return _typeCode; }
        }
        protected override ConditionValue getCValueTypeCode() { return this.TypeCode; }


        public BsTDefaultEnvColorInfoCCQ AddOrderBy_TypeCode_Asc() { regOBA("TYPE_CODE");return this; }
        public BsTDefaultEnvColorInfoCCQ AddOrderBy_TypeCode_Desc() { regOBD("TYPE_CODE");return this; }

        protected ConditionValue _gradationType;
        public ConditionValue GradationType {
            get { if (_gradationType == null) { _gradationType = new ConditionValue(); } return _gradationType; }
        }
        protected override ConditionValue getCValueGradationType() { return this.GradationType; }


        public BsTDefaultEnvColorInfoCCQ AddOrderBy_GradationType_Asc() { regOBA("GRADATION_TYPE");return this; }
        public BsTDefaultEnvColorInfoCCQ AddOrderBy_GradationType_Desc() { regOBD("GRADATION_TYPE");return this; }

        public BsTDefaultEnvColorInfoCCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDefaultEnvColorInfoCCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDefaultEnvColorInfoCCQ baseQuery = (TDefaultEnvColorInfoCCQ)baseQueryAsSuper;
            TDefaultEnvColorInfoCCQ unionQuery = (TDefaultEnvColorInfoCCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDefaultEnvBase()) {
                unionQuery.QueryTDefaultEnvBase().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnvBase(), unionQuery.QueryTDefaultEnvBase());
            }

        }
    
        protected TDefaultEnvBaseCQ _conditionQueryTDefaultEnvBase;
        public TDefaultEnvBaseCQ QueryTDefaultEnvBase() {
            return this.ConditionQueryTDefaultEnvBase;
        }
        public TDefaultEnvBaseCQ ConditionQueryTDefaultEnvBase {
            get {
                if (_conditionQueryTDefaultEnvBase == null) {
                    _conditionQueryTDefaultEnvBase = xcreateQueryTDefaultEnvBase();
                    xsetupOuterJoin_TDefaultEnvBase();
                }
                return _conditionQueryTDefaultEnvBase;
            }
        }
        protected TDefaultEnvBaseCQ xcreateQueryTDefaultEnvBase() {
            String nrp = resolveNextRelationPathTDefaultEnvBase();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvBaseCQ cq = new TDefaultEnvBaseCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnvBase"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnvBase() {
            TDefaultEnvBaseCQ cq = ConditionQueryTDefaultEnvBase;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("LANGUAGE", "LANGUAGE");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnvBase() {
            return resolveNextRelationPath("T_DEFAULT_ENV_COLOR_INFO_C", "tDefaultEnvBase");
        }
        public bool hasConditionQueryTDefaultEnvBase() {
            return _conditionQueryTDefaultEnvBase != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDefaultEnvColorInfoCCQ> _scalarSubQueryMap;
	    public Map<String, TDefaultEnvColorInfoCCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDefaultEnvColorInfoCCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDefaultEnvColorInfoCCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDefaultEnvColorInfoCCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDefaultEnvColorInfoCCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
