
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTRawdataImportQueInfoCQ : AbstractBsTRawdataImportQueInfoCQ {

        protected TRawdataImportQueInfoCIQ _inlineQuery;

        public BsTRawdataImportQueInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TRawdataImportQueInfoCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TRawdataImportQueInfoCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TRawdataImportQueInfoCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TRawdataImportQueInfoCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _rawdataImportQueInfoId;
        public ConditionValue RawdataImportQueInfoId {
            get { if (_rawdataImportQueInfoId == null) { _rawdataImportQueInfoId = new ConditionValue(); } return _rawdataImportQueInfoId; }
        }
        protected override ConditionValue getCValueRawdataImportQueInfoId() { return this.RawdataImportQueInfoId; }


        protected Map<String, TQcwebSurveyInfoCQ> _rawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> RawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList { get { return _rawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepRawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap == null) { _rawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _rawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "RawdataImportQueInfoId_ExistsSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _rawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> RawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList { get { return _rawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepRawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap == null) { _rawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _rawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "RawdataImportQueInfoId_NotExistsSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _rawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> RawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList { get { return _rawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepRawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap == null) { _rawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _rawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "RawdataImportQueInfoId_InScopeSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _rawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> RawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList { get { return _rawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap; }}
        public override String keepRawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap == null) { _rawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap.size() + 1);
            _rawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoListMap.put(key, subQuery); return "RawdataImportQueInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _rawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> RawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList { get { return _rawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap; }}
        public override String keepRawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap == null) { _rawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap.size() + 1);
            _rawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoListMap.put(key, subQuery); return "RawdataImportQueInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap;
        public Map<String, TQcwebSurveyInfoCQ> RawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList { get { return _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap; } }
        public override String keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery) {
            if (_rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap == null) { _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap.size() + 1);
            _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListMap.put(key, subQuery); return "RawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList." + key;
        }
        protected Map<String, Object> _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap;
        public Map<String, Object> RawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter { get { return _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap; } }
        public override String keepRawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(Object parameterValue) {
            if (_rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap == null) { _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap.size() + 1);
            _rawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameterMap.put(key, parameterValue); return "RawdataImportQueInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter." + key;
        }

        public BsTRawdataImportQueInfoCQ AddOrderBy_RawdataImportQueInfoId_Asc() { regOBA("RAWDATA_IMPORT_QUE_INFO_ID");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_RawdataImportQueInfoId_Desc() { regOBD("RAWDATA_IMPORT_QUE_INFO_ID");return this; }

        protected ConditionValue _qcwebJobNo;
        public ConditionValue QcwebJobNo {
            get { if (_qcwebJobNo == null) { _qcwebJobNo = new ConditionValue(); } return _qcwebJobNo; }
        }
        protected override ConditionValue getCValueQcwebJobNo() { return this.QcwebJobNo; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_QcwebJobNo_Asc() { regOBA("QCWEB_JOB_NO");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_QcwebJobNo_Desc() { regOBD("QCWEB_JOB_NO");return this; }

        protected ConditionValue _mainSurveyId;
        public ConditionValue MainSurveyId {
            get { if (_mainSurveyId == null) { _mainSurveyId = new ConditionValue(); } return _mainSurveyId; }
        }
        protected override ConditionValue getCValueMainSurveyId() { return this.MainSurveyId; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_MainSurveyId_Asc() { regOBA("MAIN_SURVEY_ID");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_MainSurveyId_Desc() { regOBD("MAIN_SURVEY_ID");return this; }

        protected ConditionValue _surveyDataType;
        public ConditionValue SurveyDataType {
            get { if (_surveyDataType == null) { _surveyDataType = new ConditionValue(); } return _surveyDataType; }
        }
        protected override ConditionValue getCValueSurveyDataType() { return this.SurveyDataType; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_SurveyDataType_Asc() { regOBA("SURVEY_DATA_TYPE");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_SurveyDataType_Desc() { regOBD("SURVEY_DATA_TYPE");return this; }

        protected ConditionValue _filepath;
        public ConditionValue Filepath {
            get { if (_filepath == null) { _filepath = new ConditionValue(); } return _filepath; }
        }
        protected override ConditionValue getCValueFilepath() { return this.Filepath; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_Filepath_Asc() { regOBA("FILEPATH");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_Filepath_Desc() { regOBD("FILEPATH");return this; }

        protected ConditionValue _fileName;
        public ConditionValue FileName {
            get { if (_fileName == null) { _fileName = new ConditionValue(); } return _fileName; }
        }
        protected override ConditionValue getCValueFileName() { return this.FileName; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_FileName_Asc() { regOBA("FILE_NAME");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_FileName_Desc() { regOBD("FILE_NAME");return this; }

        protected ConditionValue _importStatus;
        public ConditionValue ImportStatus {
            get { if (_importStatus == null) { _importStatus = new ConditionValue(); } return _importStatus; }
        }
        protected override ConditionValue getCValueImportStatus() { return this.ImportStatus; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_ImportStatus_Asc() { regOBA("IMPORT_STATUS");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_ImportStatus_Desc() { regOBD("IMPORT_STATUS");return this; }

        protected ConditionValue _message;
        public ConditionValue Message {
            get { if (_message == null) { _message = new ConditionValue(); } return _message; }
        }
        protected override ConditionValue getCValueMessage() { return this.Message; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_Message_Asc() { regOBA("MESSAGE");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_Message_Desc() { regOBD("MESSAGE");return this; }

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

        public BsTRawdataImportQueInfoCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _addDataNo;
        public ConditionValue AddDataNo {
            get { if (_addDataNo == null) { _addDataNo = new ConditionValue(); } return _addDataNo; }
        }
        protected override ConditionValue getCValueAddDataNo() { return this.AddDataNo; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_AddDataNo_Asc() { regOBA("ADD_DATA_NO");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_AddDataNo_Desc() { regOBD("ADD_DATA_NO");return this; }

        protected ConditionValue _requestDatetime;
        public ConditionValue RequestDatetime {
            get { if (_requestDatetime == null) { _requestDatetime = new ConditionValue(); } return _requestDatetime; }
        }
        protected override ConditionValue getCValueRequestDatetime() { return this.RequestDatetime; }


        public BsTRawdataImportQueInfoCQ AddOrderBy_RequestDatetime_Asc() { regOBA("REQUEST_DATETIME");return this; }
        public BsTRawdataImportQueInfoCQ AddOrderBy_RequestDatetime_Desc() { regOBD("REQUEST_DATETIME");return this; }

        public BsTRawdataImportQueInfoCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTRawdataImportQueInfoCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TRawdataImportQueInfoCQ baseQuery = (TRawdataImportQueInfoCQ)baseQueryAsSuper;
            TRawdataImportQueInfoCQ unionQuery = (TRawdataImportQueInfoCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
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
            return resolveNextRelationPath("T_RAWDATA_IMPORT_QUE_INFO", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TRawdataImportQueInfoCQ> _scalarSubQueryMap;
	    public Map<String, TRawdataImportQueInfoCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TRawdataImportQueInfoCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TRawdataImportQueInfoCQ> _myselfInScopeSubQueryMap;
        public Map<String, TRawdataImportQueInfoCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TRawdataImportQueInfoCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TRawdataImportQueInfoCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
