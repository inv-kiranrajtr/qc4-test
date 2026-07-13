
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTWeightbackCQ : AbstractBsTWeightbackCQ {

        protected TWeightbackCIQ _inlineQuery;

        public BsTWeightbackCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TWeightbackCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TWeightbackCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TWeightbackCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TWeightbackCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _weightbackId;
        public ConditionValue WeightbackId {
            get { if (_weightbackId == null) { _weightbackId = new ConditionValue(); } return _weightbackId; }
        }
        protected override ConditionValue getCValueWeightbackId() { return this.WeightbackId; }


        protected Map<String, TWeightbackValueCQ> _weightbackId_ExistsSubQuery_TWeightbackValueListMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_ExistsSubQuery_TWeightbackValueList { get { return _weightbackId_ExistsSubQuery_TWeightbackValueListMap; }}
        public override String keepWeightbackId_ExistsSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            if (_weightbackId_ExistsSubQuery_TWeightbackValueListMap == null) { _weightbackId_ExistsSubQuery_TWeightbackValueListMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_ExistsSubQuery_TWeightbackValueListMap.size() + 1);
            _weightbackId_ExistsSubQuery_TWeightbackValueListMap.put(key, subQuery); return "WeightbackId_ExistsSubQuery_TWeightbackValueList." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_NotExistsSubQuery_TWeightbackValueListMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_NotExistsSubQuery_TWeightbackValueList { get { return _weightbackId_NotExistsSubQuery_TWeightbackValueListMap; }}
        public override String keepWeightbackId_NotExistsSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            if (_weightbackId_NotExistsSubQuery_TWeightbackValueListMap == null) { _weightbackId_NotExistsSubQuery_TWeightbackValueListMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_NotExistsSubQuery_TWeightbackValueListMap.size() + 1);
            _weightbackId_NotExistsSubQuery_TWeightbackValueListMap.put(key, subQuery); return "WeightbackId_NotExistsSubQuery_TWeightbackValueList." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_InScopeSubQuery_TWeightbackValueMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_InScopeSubQuery_TWeightbackValue { get { return _weightbackId_InScopeSubQuery_TWeightbackValueMap; }}
        public override String keepWeightbackId_InScopeSubQuery_TWeightbackValue(TWeightbackValueCQ subQuery) {
            if (_weightbackId_InScopeSubQuery_TWeightbackValueMap == null) { _weightbackId_InScopeSubQuery_TWeightbackValueMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_InScopeSubQuery_TWeightbackValueMap.size() + 1);
            _weightbackId_InScopeSubQuery_TWeightbackValueMap.put(key, subQuery); return "WeightbackId_InScopeSubQuery_TWeightbackValue." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_InScopeSubQuery_TWeightbackValueListMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_InScopeSubQuery_TWeightbackValueList { get { return _weightbackId_InScopeSubQuery_TWeightbackValueListMap; }}
        public override String keepWeightbackId_InScopeSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            if (_weightbackId_InScopeSubQuery_TWeightbackValueListMap == null) { _weightbackId_InScopeSubQuery_TWeightbackValueListMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_InScopeSubQuery_TWeightbackValueListMap.size() + 1);
            _weightbackId_InScopeSubQuery_TWeightbackValueListMap.put(key, subQuery); return "WeightbackId_InScopeSubQuery_TWeightbackValueList." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_NotInScopeSubQuery_TWeightbackValueMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_NotInScopeSubQuery_TWeightbackValue { get { return _weightbackId_NotInScopeSubQuery_TWeightbackValueMap; }}
        public override String keepWeightbackId_NotInScopeSubQuery_TWeightbackValue(TWeightbackValueCQ subQuery) {
            if (_weightbackId_NotInScopeSubQuery_TWeightbackValueMap == null) { _weightbackId_NotInScopeSubQuery_TWeightbackValueMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_NotInScopeSubQuery_TWeightbackValueMap.size() + 1);
            _weightbackId_NotInScopeSubQuery_TWeightbackValueMap.put(key, subQuery); return "WeightbackId_NotInScopeSubQuery_TWeightbackValue." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_NotInScopeSubQuery_TWeightbackValueListMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_NotInScopeSubQuery_TWeightbackValueList { get { return _weightbackId_NotInScopeSubQuery_TWeightbackValueListMap; }}
        public override String keepWeightbackId_NotInScopeSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            if (_weightbackId_NotInScopeSubQuery_TWeightbackValueListMap == null) { _weightbackId_NotInScopeSubQuery_TWeightbackValueListMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_NotInScopeSubQuery_TWeightbackValueListMap.size() + 1);
            _weightbackId_NotInScopeSubQuery_TWeightbackValueListMap.put(key, subQuery); return "WeightbackId_NotInScopeSubQuery_TWeightbackValueList." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_SpecifyDerivedReferrer_TWeightbackValueListMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_SpecifyDerivedReferrer_TWeightbackValueList { get { return _weightbackId_SpecifyDerivedReferrer_TWeightbackValueListMap; }}
        public override String keepWeightbackId_SpecifyDerivedReferrer_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            if (_weightbackId_SpecifyDerivedReferrer_TWeightbackValueListMap == null) { _weightbackId_SpecifyDerivedReferrer_TWeightbackValueListMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_SpecifyDerivedReferrer_TWeightbackValueListMap.size() + 1);
            _weightbackId_SpecifyDerivedReferrer_TWeightbackValueListMap.put(key, subQuery); return "WeightbackId_SpecifyDerivedReferrer_TWeightbackValueList." + key;
        }

        protected Map<String, TWeightbackValueCQ> _weightbackId_QueryDerivedReferrer_TWeightbackValueListMap;
        public Map<String, TWeightbackValueCQ> WeightbackId_QueryDerivedReferrer_TWeightbackValueList { get { return _weightbackId_QueryDerivedReferrer_TWeightbackValueListMap; } }
        public override String keepWeightbackId_QueryDerivedReferrer_TWeightbackValueList(TWeightbackValueCQ subQuery) {
            if (_weightbackId_QueryDerivedReferrer_TWeightbackValueListMap == null) { _weightbackId_QueryDerivedReferrer_TWeightbackValueListMap = new LinkedHashMap<String, TWeightbackValueCQ>(); }
            String key = "subQueryMapKey" + (_weightbackId_QueryDerivedReferrer_TWeightbackValueListMap.size() + 1);
            _weightbackId_QueryDerivedReferrer_TWeightbackValueListMap.put(key, subQuery); return "WeightbackId_QueryDerivedReferrer_TWeightbackValueList." + key;
        }
        protected Map<String, Object> _weightbackId_QueryDerivedReferrer_TWeightbackValueListParameterMap;
        public Map<String, Object> WeightbackId_QueryDerivedReferrer_TWeightbackValueListParameter { get { return _weightbackId_QueryDerivedReferrer_TWeightbackValueListParameterMap; } }
        public override String keepWeightbackId_QueryDerivedReferrer_TWeightbackValueListParameter(Object parameterValue) {
            if (_weightbackId_QueryDerivedReferrer_TWeightbackValueListParameterMap == null) { _weightbackId_QueryDerivedReferrer_TWeightbackValueListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_weightbackId_QueryDerivedReferrer_TWeightbackValueListParameterMap.size() + 1);
            _weightbackId_QueryDerivedReferrer_TWeightbackValueListParameterMap.put(key, parameterValue); return "WeightbackId_QueryDerivedReferrer_TWeightbackValueListParameter." + key;
        }

        public BsTWeightbackCQ AddOrderBy_WeightbackId_Asc() { regOBA("WEIGHTBACK_ID");return this; }
        public BsTWeightbackCQ AddOrderBy_WeightbackId_Desc() { regOBD("WEIGHTBACK_ID");return this; }

        protected ConditionValue _weightbackItemId;
        public ConditionValue WeightbackItemId {
            get { if (_weightbackItemId == null) { _weightbackItemId = new ConditionValue(); } return _weightbackItemId; }
        }
        protected override ConditionValue getCValueWeightbackItemId() { return this.WeightbackItemId; }


        public BsTWeightbackCQ AddOrderBy_WeightbackItemId_Asc() { regOBA("WEIGHTBACK_ITEM_ID");return this; }
        public BsTWeightbackCQ AddOrderBy_WeightbackItemId_Desc() { regOBD("WEIGHTBACK_ITEM_ID");return this; }

        protected ConditionValue _assistCalcFlag;
        public ConditionValue AssistCalcFlag {
            get { if (_assistCalcFlag == null) { _assistCalcFlag = new ConditionValue(); } return _assistCalcFlag; }
        }
        protected override ConditionValue getCValueAssistCalcFlag() { return this.AssistCalcFlag; }


        public BsTWeightbackCQ AddOrderBy_AssistCalcFlag_Asc() { regOBA("ASSIST_CALC_FLAG");return this; }
        public BsTWeightbackCQ AddOrderBy_AssistCalcFlag_Desc() { regOBD("ASSIST_CALC_FLAG");return this; }

        protected ConditionValue _assistCalcType;
        public ConditionValue AssistCalcType {
            get { if (_assistCalcType == null) { _assistCalcType = new ConditionValue(); } return _assistCalcType; }
        }
        protected override ConditionValue getCValueAssistCalcType() { return this.AssistCalcType; }


        public BsTWeightbackCQ AddOrderBy_AssistCalcType_Asc() { regOBA("ASSIST_CALC_TYPE");return this; }
        public BsTWeightbackCQ AddOrderBy_AssistCalcType_Desc() { regOBD("ASSIST_CALC_TYPE");return this; }

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

        public BsTWeightbackCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTWeightbackCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _lastUpdateUser;
        public ConditionValue LastUpdateUser {
            get { if (_lastUpdateUser == null) { _lastUpdateUser = new ConditionValue(); } return _lastUpdateUser; }
        }
        protected override ConditionValue getCValueLastUpdateUser() { return this.LastUpdateUser; }


        public BsTWeightbackCQ AddOrderBy_LastUpdateUser_Asc() { regOBA("LAST_UPDATE_USER");return this; }
        public BsTWeightbackCQ AddOrderBy_LastUpdateUser_Desc() { regOBD("LAST_UPDATE_USER");return this; }

        protected ConditionValue _lastUpdateDatetime;
        public ConditionValue LastUpdateDatetime {
            get { if (_lastUpdateDatetime == null) { _lastUpdateDatetime = new ConditionValue(); } return _lastUpdateDatetime; }
        }
        protected override ConditionValue getCValueLastUpdateDatetime() { return this.LastUpdateDatetime; }


        public BsTWeightbackCQ AddOrderBy_LastUpdateDatetime_Asc() { regOBA("LAST_UPDATE_DATETIME");return this; }
        public BsTWeightbackCQ AddOrderBy_LastUpdateDatetime_Desc() { regOBD("LAST_UPDATE_DATETIME");return this; }

        public BsTWeightbackCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTWeightbackCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TWeightbackCQ baseQuery = (TWeightbackCQ)baseQueryAsSuper;
            TWeightbackCQ unionQuery = (TWeightbackCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTWeightbackValue()) {
                unionQuery.QueryTWeightbackValue().reflectRelationOnUnionQuery(baseQuery.QueryTWeightbackValue(), unionQuery.QueryTWeightbackValue());
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
            return resolveNextRelationPath("T_WEIGHTBACK", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TWeightbackValueCQ _conditionQueryTWeightbackValue;
        public TWeightbackValueCQ QueryTWeightbackValue() {
            return this.ConditionQueryTWeightbackValue;
        }
        public TWeightbackValueCQ ConditionQueryTWeightbackValue {
            get {
                if (_conditionQueryTWeightbackValue == null) {
                    _conditionQueryTWeightbackValue = xcreateQueryTWeightbackValue();
                    xsetupOuterJoin_TWeightbackValue();
                }
                return _conditionQueryTWeightbackValue;
            }
        }
        protected TWeightbackValueCQ xcreateQueryTWeightbackValue() {
            String nrp = resolveNextRelationPathTWeightbackValue();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TWeightbackValueCQ cq = new TWeightbackValueCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tWeightbackValue"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TWeightbackValue() {
            TWeightbackValueCQ cq = ConditionQueryTWeightbackValue;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("WEIGHTBACK_ID", "Weightback_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTWeightbackValue() {
            return resolveNextRelationPath("T_WEIGHTBACK", "tWeightbackValue");
        }
        public bool hasConditionQueryTWeightbackValue() {
            return _conditionQueryTWeightbackValue != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TWeightbackCQ> _scalarSubQueryMap;
	    public Map<String, TWeightbackCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TWeightbackCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TWeightbackCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TWeightbackCQ> _myselfInScopeSubQueryMap;
        public Map<String, TWeightbackCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TWeightbackCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TWeightbackCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
