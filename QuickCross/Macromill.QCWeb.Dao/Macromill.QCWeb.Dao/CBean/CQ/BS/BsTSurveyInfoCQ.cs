
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTSurveyInfoCQ : AbstractBsTSurveyInfoCQ {

        protected TSurveyInfoCIQ _inlineQuery;

        public BsTSurveyInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TSurveyInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TSurveyInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TSurveyInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TSurveyInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _surveyInfoId;
        public ConditionValue SurveyInfoId {
            get { if (_surveyInfoId == null) { _surveyInfoId = new ConditionValue(); } return _surveyInfoId; }
        }
        protected override ConditionValue getCValueSurveyInfoId() { return this.SurveyInfoId; }


        protected Map<String, TQcwebSurveyInfoCQ> _surveyInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> SurveyInfoId_ExistsSubQuery_TQcwebSurveyInfoList { get { return _surveyInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepSurveyInfoId_ExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_surveyInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap == null) { _surveyInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _surveyInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "SurveyInfoId_ExistsSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _surveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> SurveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoList { get { return _surveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepSurveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_surveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap == null) { _surveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _surveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "SurveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _surveyInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> SurveyInfoId_InScopeSubQuery_TQcwebSurveyInfoList { get { return _surveyInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepSurveyInfoId_InScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_surveyInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap == null) { _surveyInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _surveyInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "SurveyInfoId_InScopeSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _surveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> SurveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList { get { return _surveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepSurveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_surveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap == null) { _surveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _surveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "SurveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _surveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> SurveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList { get { return _surveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap; }}
        public override String keepSurveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_surveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap == null) { _surveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap.size() + 1);
            _surveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap.put(key, subQuery); return "SurveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> SurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList { get { return _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap; } }
        public override String keepSurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap == null) { _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap.size() + 1);
            _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap.put(key, subQuery); return "SurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList." + key;
        }
        protected Map<String, Object> _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap;
        public Map<String, Object> SurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter { get { return _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap; } }
        public override String keepSurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(Object parameterValue) {
            if (_surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap == null) { _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap.size() + 1);
            _surveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap.put(key, parameterValue); return "SurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter." + key;
        }

        public BsTSurveyInfoCQ AddOrderBy_SurveyInfoId_Asc() { regOBA("SURVEY_INFO_ID");return this; }
        public BsTSurveyInfoCQ AddOrderBy_SurveyInfoId_Desc() { regOBD("SURVEY_INFO_ID");return this; }

        protected ConditionValue _mainSurveyId;
        public ConditionValue MainSurveyId {
            get { if (_mainSurveyId == null) { _mainSurveyId = new ConditionValue(); } return _mainSurveyId; }
        }
        protected override ConditionValue getCValueMainSurveyId() { return this.MainSurveyId; }


        public BsTSurveyInfoCQ AddOrderBy_MainSurveyId_Asc() { regOBA("MAIN_SURVEY_ID");return this; }
        public BsTSurveyInfoCQ AddOrderBy_MainSurveyId_Desc() { regOBD("MAIN_SURVEY_ID");return this; }

        protected ConditionValue _scheduleDeleteDate;
        public ConditionValue ScheduleDeleteDate {
            get { if (_scheduleDeleteDate == null) { _scheduleDeleteDate = new ConditionValue(); } return _scheduleDeleteDate; }
        }
        protected override ConditionValue getCValueScheduleDeleteDate() { return this.ScheduleDeleteDate; }


        public BsTSurveyInfoCQ AddOrderBy_ScheduleDeleteDate_Asc() { regOBA("SCHEDULE_DELETE_DATE");return this; }
        public BsTSurveyInfoCQ AddOrderBy_ScheduleDeleteDate_Desc() { regOBD("SCHEDULE_DELETE_DATE");return this; }

        protected ConditionValue _deleteFlag;
        public ConditionValue DeleteFlag {
            get { if (_deleteFlag == null) { _deleteFlag = new ConditionValue(); } return _deleteFlag; }
        }
        protected override ConditionValue getCValueDeleteFlag() { return this.DeleteFlag; }


        public BsTSurveyInfoCQ AddOrderBy_DeleteFlag_Asc() { regOBA("DELETE_FLAG");return this; }
        public BsTSurveyInfoCQ AddOrderBy_DeleteFlag_Desc() { regOBD("DELETE_FLAG");return this; }

        public BsTSurveyInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTSurveyInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TSurveyInfoCQ> _scalarSubQueryMap;
	    public Map<String, TSurveyInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TSurveyInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TSurveyInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TSurveyInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TSurveyInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TSurveyInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
