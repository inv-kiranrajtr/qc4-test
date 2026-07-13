
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDefaultEnvColorInfoCQ : AbstractBsTDefaultEnvColorInfoCQ {

        protected TDefaultEnvColorInfoCIQ _inlineQuery;

        public BsTDefaultEnvColorInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDefaultEnvColorInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDefaultEnvColorInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDefaultEnvColorInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDefaultEnvColorInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _defEnvColorInfoId;
        public ConditionValue DefEnvColorInfoId {
            get { if (_defEnvColorInfoId == null) { _defEnvColorInfoId = new ConditionValue(); } return _defEnvColorInfoId; }
        }
        protected override ConditionValue getCValueDefEnvColorInfoId() { return this.DefEnvColorInfoId; }


        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlListMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList { get { return _defEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlListMap; }}
        public override String keepDefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlListMap == null) { _defEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlListMap.size() + 1);
            _defEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlListMap.put(key, subQuery); return "DefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlListMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList { get { return _defEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlListMap; }}
        public override String keepDefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlListMap == null) { _defEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlListMap.size() + 1);
            _defEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlListMap.put(key, subQuery); return "DefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl { get { return _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlMap; }}
        public override String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlMap == null) { _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlMap.size() + 1);
            _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlMap.put(key, subQuery); return "DefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlListMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList { get { return _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlListMap; }}
        public override String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlListMap == null) { _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlListMap.size() + 1);
            _defEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlListMap.put(key, subQuery); return "DefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl { get { return _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlMap; }}
        public override String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlMap == null) { _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlMap.size() + 1);
            _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlMap.put(key, subQuery); return "DefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlListMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList { get { return _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlListMap; }}
        public override String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlListMap == null) { _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlListMap.size() + 1);
            _defEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlListMap.put(key, subQuery); return "DefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlListMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlList { get { return _defEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlListMap; }}
        public override String keepDefEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlListMap == null) { _defEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlListMap.size() + 1);
            _defEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlListMap.put(key, subQuery); return "DefEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlList." + key;
        }

        protected Map<String, TDefaultEnvColorDtlCQ> _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListMap;
        public Map<String, TDefaultEnvColorDtlCQ> DefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlList { get { return _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListMap; } }
        public override String keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery) {
            if (_defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListMap == null) { _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListMap = new LinkedHashMap<String, TDefaultEnvColorDtlCQ>(); }
            String key = "subQueryMapKey" + (_defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListMap.size() + 1);
            _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListMap.put(key, subQuery); return "DefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlList." + key;
        }
        protected Map<String, Object> _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameterMap;
        public Map<String, Object> DefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameter { get { return _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameterMap; } }
        public override String keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameter(Object parameterValue) {
            if (_defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameterMap == null) { _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameterMap.size() + 1);
            _defEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameterMap.put(key, parameterValue); return "DefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameter." + key;
        }

        public BsTDefaultEnvColorInfoCQ AddOrderBy_DefEnvColorInfoId_Asc() { regOBA("DEF_ENV_COLOR_INFO_ID");return this; }
        public BsTDefaultEnvColorInfoCQ AddOrderBy_DefEnvColorInfoId_Desc() { regOBD("DEF_ENV_COLOR_INFO_ID");return this; }

        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TDefaultEnvCQ> _qcwebid_InScopeSubQuery_TDefaultEnvMap;
        public Map<String, TDefaultEnvCQ> Qcwebid_InScopeSubQuery_TDefaultEnv { get { return _qcwebid_InScopeSubQuery_TDefaultEnvMap; }}
        public override String keepQcwebid_InScopeSubQuery_TDefaultEnv(TDefaultEnvCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TDefaultEnvMap == null) { _qcwebid_InScopeSubQuery_TDefaultEnvMap = new LinkedHashMap<String, TDefaultEnvCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TDefaultEnvMap.size() + 1);
            _qcwebid_InScopeSubQuery_TDefaultEnvMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TDefaultEnv." + key;
        }

        protected Map<String, TDefaultEnvCQ> _qcwebid_NotInScopeSubQuery_TDefaultEnvMap;
        public Map<String, TDefaultEnvCQ> Qcwebid_NotInScopeSubQuery_TDefaultEnv { get { return _qcwebid_NotInScopeSubQuery_TDefaultEnvMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TDefaultEnv(TDefaultEnvCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TDefaultEnvMap == null) { _qcwebid_NotInScopeSubQuery_TDefaultEnvMap = new LinkedHashMap<String, TDefaultEnvCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TDefaultEnvMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TDefaultEnvMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TDefaultEnv." + key;
        }

        public BsTDefaultEnvColorInfoCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTDefaultEnvColorInfoCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _typeCode;
        public ConditionValue TypeCode {
            get { if (_typeCode == null) { _typeCode = new ConditionValue(); } return _typeCode; }
        }
        protected override ConditionValue getCValueTypeCode() { return this.TypeCode; }


        public BsTDefaultEnvColorInfoCQ AddOrderBy_TypeCode_Asc() { regOBA("TYPE_CODE");return this; }
        public BsTDefaultEnvColorInfoCQ AddOrderBy_TypeCode_Desc() { regOBD("TYPE_CODE");return this; }

        protected ConditionValue _gradationType;
        public ConditionValue GradationType {
            get { if (_gradationType == null) { _gradationType = new ConditionValue(); } return _gradationType; }
        }
        protected override ConditionValue getCValueGradationType() { return this.GradationType; }


        public BsTDefaultEnvColorInfoCQ AddOrderBy_GradationType_Asc() { regOBA("GRADATION_TYPE");return this; }
        public BsTDefaultEnvColorInfoCQ AddOrderBy_GradationType_Desc() { regOBD("GRADATION_TYPE");return this; }

        public BsTDefaultEnvColorInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDefaultEnvColorInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDefaultEnvColorInfoCQ baseQuery = (TDefaultEnvColorInfoCQ)baseQueryAsSuper;
            TDefaultEnvColorInfoCQ unionQuery = (TDefaultEnvColorInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTDefaultEnv()) {
                unionQuery.QueryTDefaultEnv().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnv(), unionQuery.QueryTDefaultEnv());
            }
            if (baseQuery.hasConditionQueryTDefaultEnvColorDtl()) {
                unionQuery.QueryTDefaultEnvColorDtl().reflectRelationOnUnionQuery(baseQuery.QueryTDefaultEnvColorDtl(), unionQuery.QueryTDefaultEnvColorDtl());
            }

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
            joinOnMap.put("QCWEBID", "QCWEBID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnv() {
            return resolveNextRelationPath("T_DEFAULT_ENV_COLOR_INFO", "tDefaultEnv");
        }
        public bool hasConditionQueryTDefaultEnv() {
            return _conditionQueryTDefaultEnv != null;
        }
        protected TDefaultEnvColorDtlCQ _conditionQueryTDefaultEnvColorDtl;
        public TDefaultEnvColorDtlCQ QueryTDefaultEnvColorDtl() {
            return this.ConditionQueryTDefaultEnvColorDtl;
        }
        public TDefaultEnvColorDtlCQ ConditionQueryTDefaultEnvColorDtl {
            get {
                if (_conditionQueryTDefaultEnvColorDtl == null) {
                    _conditionQueryTDefaultEnvColorDtl = xcreateQueryTDefaultEnvColorDtl();
                    xsetupOuterJoin_TDefaultEnvColorDtl();
                }
                return _conditionQueryTDefaultEnvColorDtl;
            }
        }
        protected TDefaultEnvColorDtlCQ xcreateQueryTDefaultEnvColorDtl() {
            String nrp = resolveNextRelationPathTDefaultEnvColorDtl();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TDefaultEnvColorDtlCQ cq = new TDefaultEnvColorDtlCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tDefaultEnvColorDtl"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TDefaultEnvColorDtl() {
            TDefaultEnvColorDtlCQ cq = ConditionQueryTDefaultEnvColorDtl;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("DEF_ENV_COLOR_INFO_ID", "Def_Env_Color_Info_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTDefaultEnvColorDtl() {
            return resolveNextRelationPath("T_DEFAULT_ENV_COLOR_INFO", "tDefaultEnvColorDtl");
        }
        public bool hasConditionQueryTDefaultEnvColorDtl() {
            return _conditionQueryTDefaultEnvColorDtl != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDefaultEnvColorInfoCQ> _scalarSubQueryMap;
	    public Map<String, TDefaultEnvColorInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDefaultEnvColorInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDefaultEnvColorInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDefaultEnvColorInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDefaultEnvColorInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
