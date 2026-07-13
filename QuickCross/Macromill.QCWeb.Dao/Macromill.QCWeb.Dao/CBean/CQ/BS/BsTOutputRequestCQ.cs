
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTOutputRequestCQ : AbstractBsTOutputRequestCQ {

        protected TOutputRequestCIQ _inlineQuery;

        public BsTOutputRequestCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TOutputRequestCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TOutputRequestCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TOutputRequestCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TOutputRequestCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _outputRequestId;
        public ConditionValue OutputRequestId {
            get { if (_outputRequestId == null) { _outputRequestId = new ConditionValue(); } return _outputRequestId; }
        }
        protected override ConditionValue getCValueOutputRequestId() { return this.OutputRequestId; }


        protected Map<String, TOutputCommonCQ> _outputRequestId_ExistsSubQuery_TOutputCommonListMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_ExistsSubQuery_TOutputCommonList { get { return _outputRequestId_ExistsSubQuery_TOutputCommonListMap; }}
        public override String keepOutputRequestId_ExistsSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            if (_outputRequestId_ExistsSubQuery_TOutputCommonListMap == null) { _outputRequestId_ExistsSubQuery_TOutputCommonListMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_ExistsSubQuery_TOutputCommonListMap.size() + 1);
            _outputRequestId_ExistsSubQuery_TOutputCommonListMap.put(key, subQuery); return "OutputRequestId_ExistsSubQuery_TOutputCommonList." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_NotExistsSubQuery_TOutputCommonListMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_NotExistsSubQuery_TOutputCommonList { get { return _outputRequestId_NotExistsSubQuery_TOutputCommonListMap; }}
        public override String keepOutputRequestId_NotExistsSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            if (_outputRequestId_NotExistsSubQuery_TOutputCommonListMap == null) { _outputRequestId_NotExistsSubQuery_TOutputCommonListMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_NotExistsSubQuery_TOutputCommonListMap.size() + 1);
            _outputRequestId_NotExistsSubQuery_TOutputCommonListMap.put(key, subQuery); return "OutputRequestId_NotExistsSubQuery_TOutputCommonList." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_InScopeSubQuery_TOutputCommonMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_InScopeSubQuery_TOutputCommon { get { return _outputRequestId_InScopeSubQuery_TOutputCommonMap; }}
        public override String keepOutputRequestId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            if (_outputRequestId_InScopeSubQuery_TOutputCommonMap == null) { _outputRequestId_InScopeSubQuery_TOutputCommonMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_InScopeSubQuery_TOutputCommonMap.size() + 1);
            _outputRequestId_InScopeSubQuery_TOutputCommonMap.put(key, subQuery); return "OutputRequestId_InScopeSubQuery_TOutputCommon." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_InScopeSubQuery_TOutputCommonListMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_InScopeSubQuery_TOutputCommonList { get { return _outputRequestId_InScopeSubQuery_TOutputCommonListMap; }}
        public override String keepOutputRequestId_InScopeSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            if (_outputRequestId_InScopeSubQuery_TOutputCommonListMap == null) { _outputRequestId_InScopeSubQuery_TOutputCommonListMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_InScopeSubQuery_TOutputCommonListMap.size() + 1);
            _outputRequestId_InScopeSubQuery_TOutputCommonListMap.put(key, subQuery); return "OutputRequestId_InScopeSubQuery_TOutputCommonList." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_NotInScopeSubQuery_TOutputCommonMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_NotInScopeSubQuery_TOutputCommon { get { return _outputRequestId_NotInScopeSubQuery_TOutputCommonMap; }}
        public override String keepOutputRequestId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            if (_outputRequestId_NotInScopeSubQuery_TOutputCommonMap == null) { _outputRequestId_NotInScopeSubQuery_TOutputCommonMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_NotInScopeSubQuery_TOutputCommonMap.size() + 1);
            _outputRequestId_NotInScopeSubQuery_TOutputCommonMap.put(key, subQuery); return "OutputRequestId_NotInScopeSubQuery_TOutputCommon." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_NotInScopeSubQuery_TOutputCommonListMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_NotInScopeSubQuery_TOutputCommonList { get { return _outputRequestId_NotInScopeSubQuery_TOutputCommonListMap; }}
        public override String keepOutputRequestId_NotInScopeSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            if (_outputRequestId_NotInScopeSubQuery_TOutputCommonListMap == null) { _outputRequestId_NotInScopeSubQuery_TOutputCommonListMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_NotInScopeSubQuery_TOutputCommonListMap.size() + 1);
            _outputRequestId_NotInScopeSubQuery_TOutputCommonListMap.put(key, subQuery); return "OutputRequestId_NotInScopeSubQuery_TOutputCommonList." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_SpecifyDerivedReferrer_TOutputCommonListMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_SpecifyDerivedReferrer_TOutputCommonList { get { return _outputRequestId_SpecifyDerivedReferrer_TOutputCommonListMap; }}
        public override String keepOutputRequestId_SpecifyDerivedReferrer_TOutputCommonList(TOutputCommonCQ subQuery) {
            if (_outputRequestId_SpecifyDerivedReferrer_TOutputCommonListMap == null) { _outputRequestId_SpecifyDerivedReferrer_TOutputCommonListMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_SpecifyDerivedReferrer_TOutputCommonListMap.size() + 1);
            _outputRequestId_SpecifyDerivedReferrer_TOutputCommonListMap.put(key, subQuery); return "OutputRequestId_SpecifyDerivedReferrer_TOutputCommonList." + key;
        }

        protected Map<String, TOutputCommonCQ> _outputRequestId_QueryDerivedReferrer_TOutputCommonListMap;
        public Map<String, TOutputCommonCQ> OutputRequestId_QueryDerivedReferrer_TOutputCommonList { get { return _outputRequestId_QueryDerivedReferrer_TOutputCommonListMap; } }
        public override String keepOutputRequestId_QueryDerivedReferrer_TOutputCommonList(TOutputCommonCQ subQuery) {
            if (_outputRequestId_QueryDerivedReferrer_TOutputCommonListMap == null) { _outputRequestId_QueryDerivedReferrer_TOutputCommonListMap = new LinkedHashMap<String, TOutputCommonCQ>(); }
            String key = "subQueryMapKey" + (_outputRequestId_QueryDerivedReferrer_TOutputCommonListMap.size() + 1);
            _outputRequestId_QueryDerivedReferrer_TOutputCommonListMap.put(key, subQuery); return "OutputRequestId_QueryDerivedReferrer_TOutputCommonList." + key;
        }
        protected Map<String, Object> _outputRequestId_QueryDerivedReferrer_TOutputCommonListParameterMap;
        public Map<String, Object> OutputRequestId_QueryDerivedReferrer_TOutputCommonListParameter { get { return _outputRequestId_QueryDerivedReferrer_TOutputCommonListParameterMap; } }
        public override String keepOutputRequestId_QueryDerivedReferrer_TOutputCommonListParameter(Object parameterValue) {
            if (_outputRequestId_QueryDerivedReferrer_TOutputCommonListParameterMap == null) { _outputRequestId_QueryDerivedReferrer_TOutputCommonListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_outputRequestId_QueryDerivedReferrer_TOutputCommonListParameterMap.size() + 1);
            _outputRequestId_QueryDerivedReferrer_TOutputCommonListParameterMap.put(key, parameterValue); return "OutputRequestId_QueryDerivedReferrer_TOutputCommonListParameter." + key;
        }

        public BsTOutputRequestCQ AddOrderBy_OutputRequestId_Asc() { regOBA("OUTPUT_REQUEST_ID");return this; }
        public BsTOutputRequestCQ AddOrderBy_OutputRequestId_Desc() { regOBD("OUTPUT_REQUEST_ID");return this; }

        protected ConditionValue _requestServerCode;
        public ConditionValue RequestServerCode {
            get { if (_requestServerCode == null) { _requestServerCode = new ConditionValue(); } return _requestServerCode; }
        }
        protected override ConditionValue getCValueRequestServerCode() { return this.RequestServerCode; }


        public BsTOutputRequestCQ AddOrderBy_RequestServerCode_Asc() { regOBA("REQUEST_SERVER_CODE");return this; }
        public BsTOutputRequestCQ AddOrderBy_RequestServerCode_Desc() { regOBD("REQUEST_SERVER_CODE");return this; }

        protected ConditionValue _requestUserId;
        public ConditionValue RequestUserId {
            get { if (_requestUserId == null) { _requestUserId = new ConditionValue(); } return _requestUserId; }
        }
        protected override ConditionValue getCValueRequestUserId() { return this.RequestUserId; }


        public BsTOutputRequestCQ AddOrderBy_RequestUserId_Asc() { regOBA("REQUEST_USER_ID");return this; }
        public BsTOutputRequestCQ AddOrderBy_RequestUserId_Desc() { regOBD("REQUEST_USER_ID");return this; }

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

        public BsTOutputRequestCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTOutputRequestCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _lastDownloadUserid;
        public ConditionValue LastDownloadUserid {
            get { if (_lastDownloadUserid == null) { _lastDownloadUserid = new ConditionValue(); } return _lastDownloadUserid; }
        }
        protected override ConditionValue getCValueLastDownloadUserid() { return this.LastDownloadUserid; }


        public BsTOutputRequestCQ AddOrderBy_LastDownloadUserid_Asc() { regOBA("LAST_DOWNLOAD_USERID");return this; }
        public BsTOutputRequestCQ AddOrderBy_LastDownloadUserid_Desc() { regOBD("LAST_DOWNLOAD_USERID");return this; }

        protected ConditionValue _requestDatetime;
        public ConditionValue RequestDatetime {
            get { if (_requestDatetime == null) { _requestDatetime = new ConditionValue(); } return _requestDatetime; }
        }
        protected override ConditionValue getCValueRequestDatetime() { return this.RequestDatetime; }


        public BsTOutputRequestCQ AddOrderBy_RequestDatetime_Asc() { regOBA("REQUEST_DATETIME");return this; }
        public BsTOutputRequestCQ AddOrderBy_RequestDatetime_Desc() { regOBD("REQUEST_DATETIME");return this; }

        protected ConditionValue _downloadPath;
        public ConditionValue DownloadPath {
            get { if (_downloadPath == null) { _downloadPath = new ConditionValue(); } return _downloadPath; }
        }
        protected override ConditionValue getCValueDownloadPath() { return this.DownloadPath; }


        public BsTOutputRequestCQ AddOrderBy_DownloadPath_Asc() { regOBA("DOWNLOAD_PATH");return this; }
        public BsTOutputRequestCQ AddOrderBy_DownloadPath_Desc() { regOBD("DOWNLOAD_PATH");return this; }

        protected ConditionValue _procServerCode;
        public ConditionValue ProcServerCode {
            get { if (_procServerCode == null) { _procServerCode = new ConditionValue(); } return _procServerCode; }
        }
        protected override ConditionValue getCValueProcServerCode() { return this.ProcServerCode; }


        public BsTOutputRequestCQ AddOrderBy_ProcServerCode_Asc() { regOBA("PROC_SERVER_CODE");return this; }
        public BsTOutputRequestCQ AddOrderBy_ProcServerCode_Desc() { regOBD("PROC_SERVER_CODE");return this; }

        protected ConditionValue _statusCode;
        public ConditionValue StatusCode {
            get { if (_statusCode == null) { _statusCode = new ConditionValue(); } return _statusCode; }
        }
        protected override ConditionValue getCValueStatusCode() { return this.StatusCode; }


        public BsTOutputRequestCQ AddOrderBy_StatusCode_Asc() { regOBA("STATUS_CODE");return this; }
        public BsTOutputRequestCQ AddOrderBy_StatusCode_Desc() { regOBD("STATUS_CODE");return this; }

        protected ConditionValue _description;
        public ConditionValue Description {
            get { if (_description == null) { _description = new ConditionValue(); } return _description; }
        }
        protected override ConditionValue getCValueDescription() { return this.Description; }


        public BsTOutputRequestCQ AddOrderBy_Description_Asc() { regOBA("DESCRIPTION");return this; }
        public BsTOutputRequestCQ AddOrderBy_Description_Desc() { regOBD("DESCRIPTION");return this; }

        protected ConditionValue _endDatetime;
        public ConditionValue EndDatetime {
            get { if (_endDatetime == null) { _endDatetime = new ConditionValue(); } return _endDatetime; }
        }
        protected override ConditionValue getCValueEndDatetime() { return this.EndDatetime; }


        public BsTOutputRequestCQ AddOrderBy_EndDatetime_Asc() { regOBA("END_DATETIME");return this; }
        public BsTOutputRequestCQ AddOrderBy_EndDatetime_Desc() { regOBD("END_DATETIME");return this; }

        protected ConditionValue _lastDownloadDatetime;
        public ConditionValue LastDownloadDatetime {
            get { if (_lastDownloadDatetime == null) { _lastDownloadDatetime = new ConditionValue(); } return _lastDownloadDatetime; }
        }
        protected override ConditionValue getCValueLastDownloadDatetime() { return this.LastDownloadDatetime; }


        public BsTOutputRequestCQ AddOrderBy_LastDownloadDatetime_Asc() { regOBA("LAST_DOWNLOAD_DATETIME");return this; }
        public BsTOutputRequestCQ AddOrderBy_LastDownloadDatetime_Desc() { regOBD("LAST_DOWNLOAD_DATETIME");return this; }

        protected ConditionValue _excelbookType;
        public ConditionValue ExcelbookType {
            get { if (_excelbookType == null) { _excelbookType = new ConditionValue(); } return _excelbookType; }
        }
        protected override ConditionValue getCValueExcelbookType() { return this.ExcelbookType; }


        public BsTOutputRequestCQ AddOrderBy_ExcelbookType_Asc() { regOBA("EXCELBOOK_TYPE");return this; }
        public BsTOutputRequestCQ AddOrderBy_ExcelbookType_Desc() { regOBD("EXCELBOOK_TYPE");return this; }

        protected ConditionValue _numericAnswerViewCode;
        public ConditionValue NumericAnswerViewCode {
            get { if (_numericAnswerViewCode == null) { _numericAnswerViewCode = new ConditionValue(); } return _numericAnswerViewCode; }
        }
        protected override ConditionValue getCValueNumericAnswerViewCode() { return this.NumericAnswerViewCode; }


        public BsTOutputRequestCQ AddOrderBy_NumericAnswerViewCode_Asc() { regOBA("NUMERIC_ANSWER_VIEW_CODE");return this; }
        public BsTOutputRequestCQ AddOrderBy_NumericAnswerViewCode_Desc() { regOBD("NUMERIC_ANSWER_VIEW_CODE");return this; }

        protected ConditionValue _dpTotal;
        public ConditionValue DpTotal {
            get { if (_dpTotal == null) { _dpTotal = new ConditionValue(); } return _dpTotal; }
        }
        protected override ConditionValue getCValueDpTotal() { return this.DpTotal; }


        public BsTOutputRequestCQ AddOrderBy_DpTotal_Asc() { regOBA("DP_TOTAL");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpTotal_Desc() { regOBD("DP_TOTAL");return this; }

        protected ConditionValue _dpAverage;
        public ConditionValue DpAverage {
            get { if (_dpAverage == null) { _dpAverage = new ConditionValue(); } return _dpAverage; }
        }
        protected override ConditionValue getCValueDpAverage() { return this.DpAverage; }


        public BsTOutputRequestCQ AddOrderBy_DpAverage_Asc() { regOBA("DP_AVERAGE");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpAverage_Desc() { regOBD("DP_AVERAGE");return this; }

        protected ConditionValue _dpStandardDiv;
        public ConditionValue DpStandardDiv {
            get { if (_dpStandardDiv == null) { _dpStandardDiv = new ConditionValue(); } return _dpStandardDiv; }
        }
        protected override ConditionValue getCValueDpStandardDiv() { return this.DpStandardDiv; }


        public BsTOutputRequestCQ AddOrderBy_DpStandardDiv_Asc() { regOBA("DP_STANDARD_DIV");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpStandardDiv_Desc() { regOBD("DP_STANDARD_DIV");return this; }

        protected ConditionValue _dpMin;
        public ConditionValue DpMin {
            get { if (_dpMin == null) { _dpMin = new ConditionValue(); } return _dpMin; }
        }
        protected override ConditionValue getCValueDpMin() { return this.DpMin; }


        public BsTOutputRequestCQ AddOrderBy_DpMin_Asc() { regOBA("DP_MIN");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpMin_Desc() { regOBD("DP_MIN");return this; }

        protected ConditionValue _dpMax;
        public ConditionValue DpMax {
            get { if (_dpMax == null) { _dpMax = new ConditionValue(); } return _dpMax; }
        }
        protected override ConditionValue getCValueDpMax() { return this.DpMax; }


        public BsTOutputRequestCQ AddOrderBy_DpMax_Asc() { regOBA("DP_MAX");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpMax_Desc() { regOBD("DP_MAX");return this; }

        protected ConditionValue _dpMedian;
        public ConditionValue DpMedian {
            get { if (_dpMedian == null) { _dpMedian = new ConditionValue(); } return _dpMedian; }
        }
        protected override ConditionValue getCValueDpMedian() { return this.DpMedian; }


        public BsTOutputRequestCQ AddOrderBy_DpMedian_Asc() { regOBA("DP_MEDIAN");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpMedian_Desc() { regOBD("DP_MEDIAN");return this; }

        protected ConditionValue _dpWeight;
        public ConditionValue DpWeight {
            get { if (_dpWeight == null) { _dpWeight = new ConditionValue(); } return _dpWeight; }
        }
        protected override ConditionValue getCValueDpWeight() { return this.DpWeight; }


        public BsTOutputRequestCQ AddOrderBy_DpWeight_Asc() { regOBA("DP_WEIGHT");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpWeight_Desc() { regOBD("DP_WEIGHT");return this; }

        protected ConditionValue _dpWeightavr;
        public ConditionValue DpWeightavr {
            get { if (_dpWeightavr == null) { _dpWeightavr = new ConditionValue(); } return _dpWeightavr; }
        }
        protected override ConditionValue getCValueDpWeightavr() { return this.DpWeightavr; }


        public BsTOutputRequestCQ AddOrderBy_DpWeightavr_Asc() { regOBA("DP_WEIGHTAVR");return this; }
        public BsTOutputRequestCQ AddOrderBy_DpWeightavr_Desc() { regOBD("DP_WEIGHTAVR");return this; }

        protected ConditionValue _procWeight;
        public ConditionValue ProcWeight {
            get { if (_procWeight == null) { _procWeight = new ConditionValue(); } return _procWeight; }
        }
        protected override ConditionValue getCValueProcWeight() { return this.ProcWeight; }


        public BsTOutputRequestCQ AddOrderBy_ProcWeight_Asc() { regOBA("PROC_WEIGHT");return this; }
        public BsTOutputRequestCQ AddOrderBy_ProcWeight_Desc() { regOBD("PROC_WEIGHT");return this; }

        protected ConditionValue _outputReportsetInfoId;
        public ConditionValue OutputReportsetInfoId {
            get { if (_outputReportsetInfoId == null) { _outputReportsetInfoId = new ConditionValue(); } return _outputReportsetInfoId; }
        }
        protected override ConditionValue getCValueOutputReportsetInfoId() { return this.OutputReportsetInfoId; }


        protected Map<String, TOutputReportsetInfoCQ> _outputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfoMap;
        public Map<String, TOutputReportsetInfoCQ> OutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo { get { return _outputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfoMap; }}
        public override String keepOutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo(TOutputReportsetInfoCQ subQuery) {
            if (_outputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfoMap == null) { _outputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfoMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfoMap.size() + 1);
            _outputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfoMap.put(key, subQuery); return "OutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo." + key;
        }

        protected Map<String, TOutputReportsetInfoCQ> _outputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfoMap;
        public Map<String, TOutputReportsetInfoCQ> OutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo { get { return _outputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfoMap; }}
        public override String keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo(TOutputReportsetInfoCQ subQuery) {
            if (_outputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfoMap == null) { _outputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfoMap = new LinkedHashMap<String, TOutputReportsetInfoCQ>(); }
            String key = "subQueryMapKey" + (_outputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfoMap.size() + 1);
            _outputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfoMap.put(key, subQuery); return "OutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo." + key;
        }

        public BsTOutputRequestCQ AddOrderBy_OutputReportsetInfoId_Asc() { regOBA("OUTPUT_REPORTSET_INFO_ID");return this; }
        public BsTOutputRequestCQ AddOrderBy_OutputReportsetInfoId_Desc() { regOBD("OUTPUT_REPORTSET_INFO_ID");return this; }

        protected ConditionValue _deleteFlag;
        public ConditionValue DeleteFlag {
            get { if (_deleteFlag == null) { _deleteFlag = new ConditionValue(); } return _deleteFlag; }
        }
        protected override ConditionValue getCValueDeleteFlag() { return this.DeleteFlag; }


        public BsTOutputRequestCQ AddOrderBy_DeleteFlag_Asc() { regOBA("DELETE_FLAG");return this; }
        public BsTOutputRequestCQ AddOrderBy_DeleteFlag_Desc() { regOBD("DELETE_FLAG");return this; }

        protected ConditionValue _viewSurveyName;
        public ConditionValue ViewSurveyName {
            get { if (_viewSurveyName == null) { _viewSurveyName = new ConditionValue(); } return _viewSurveyName; }
        }
        protected override ConditionValue getCValueViewSurveyName() { return this.ViewSurveyName; }


        public BsTOutputRequestCQ AddOrderBy_ViewSurveyName_Asc() { regOBA("VIEW_SURVEY_NAME");return this; }
        public BsTOutputRequestCQ AddOrderBy_ViewSurveyName_Desc() { regOBD("VIEW_SURVEY_NAME");return this; }

        protected ConditionValue _language;
        public ConditionValue Language {
            get { if (_language == null) { _language = new ConditionValue(); } return _language; }
        }
        protected override ConditionValue getCValueLanguage() { return this.Language; }


        public BsTOutputRequestCQ AddOrderBy_Language_Asc() { regOBA("LANGUAGE");return this; }
        public BsTOutputRequestCQ AddOrderBy_Language_Desc() { regOBD("LANGUAGE");return this; }

        protected ConditionValue _showZeroNaIvCode;
        public ConditionValue ShowZeroNaIvCode {
            get { if (_showZeroNaIvCode == null) { _showZeroNaIvCode = new ConditionValue(); } return _showZeroNaIvCode; }
        }
        protected override ConditionValue getCValueShowZeroNaIvCode() { return this.ShowZeroNaIvCode; }


        public BsTOutputRequestCQ AddOrderBy_ShowZeroNaIvCode_Asc() { regOBA("SHOW_ZERO_NA_IV_CODE");return this; }
        public BsTOutputRequestCQ AddOrderBy_ShowZeroNaIvCode_Desc() { regOBD("SHOW_ZERO_NA_IV_CODE");return this; }

        protected ConditionValue _mergeAxisCellsFlag;
        public ConditionValue MergeAxisCellsFlag {
            get { if (_mergeAxisCellsFlag == null) { _mergeAxisCellsFlag = new ConditionValue(); } return _mergeAxisCellsFlag; }
        }
        protected override ConditionValue getCValueMergeAxisCellsFlag() { return this.MergeAxisCellsFlag; }


        public BsTOutputRequestCQ AddOrderBy_MergeAxisCellsFlag_Asc() { regOBA("MERGE_AXIS_CELLS_FLAG");return this; }
        public BsTOutputRequestCQ AddOrderBy_MergeAxisCellsFlag_Desc() { regOBD("MERGE_AXIS_CELLS_FLAG");return this; }

        protected ConditionValue _scenarioName;
        public ConditionValue ScenarioName {
            get { if (_scenarioName == null) { _scenarioName = new ConditionValue(); } return _scenarioName; }
        }
        protected override ConditionValue getCValueScenarioName() { return this.ScenarioName; }


        public BsTOutputRequestCQ AddOrderBy_ScenarioName_Asc() { regOBA("SCENARIO_NAME");return this; }
        public BsTOutputRequestCQ AddOrderBy_ScenarioName_Desc() { regOBD("SCENARIO_NAME");return this; }

        protected ConditionValue _startDatetime;
        public ConditionValue StartDatetime {
            get { if (_startDatetime == null) { _startDatetime = new ConditionValue(); } return _startDatetime; }
        }
        protected override ConditionValue getCValueStartDatetime() { return this.StartDatetime; }


        public BsTOutputRequestCQ AddOrderBy_StartDatetime_Asc() { regOBA("START_DATETIME");return this; }
        public BsTOutputRequestCQ AddOrderBy_StartDatetime_Desc() { regOBD("START_DATETIME");return this; }

        protected ConditionValue _testLogFlag;
        public ConditionValue TestLogFlag {
            get { if (_testLogFlag == null) { _testLogFlag = new ConditionValue(); } return _testLogFlag; }
        }
        protected override ConditionValue getCValueTestLogFlag() { return this.TestLogFlag; }


        public BsTOutputRequestCQ AddOrderBy_TestLogFlag_Asc() { regOBA("TEST_LOG_FLAG");return this; }
        public BsTOutputRequestCQ AddOrderBy_TestLogFlag_Desc() { regOBD("TEST_LOG_FLAG");return this; }

        protected ConditionValue _tsvFileSizeGt;
        public ConditionValue TsvFileSizeGt {
            get { if (_tsvFileSizeGt == null) { _tsvFileSizeGt = new ConditionValue(); } return _tsvFileSizeGt; }
        }
        protected override ConditionValue getCValueTsvFileSizeGt() { return this.TsvFileSizeGt; }


        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeGt_Asc() { regOBA("TSV_FILE_SIZE_GT");return this; }
        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeGt_Desc() { regOBD("TSV_FILE_SIZE_GT");return this; }

        protected ConditionValue _tsvFileSizeCross;
        public ConditionValue TsvFileSizeCross {
            get { if (_tsvFileSizeCross == null) { _tsvFileSizeCross = new ConditionValue(); } return _tsvFileSizeCross; }
        }
        protected override ConditionValue getCValueTsvFileSizeCross() { return this.TsvFileSizeCross; }


        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeCross_Asc() { regOBA("TSV_FILE_SIZE_CROSS");return this; }
        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeCross_Desc() { regOBD("TSV_FILE_SIZE_CROSS");return this; }

        protected ConditionValue _tsvFileSizeFa;
        public ConditionValue TsvFileSizeFa {
            get { if (_tsvFileSizeFa == null) { _tsvFileSizeFa = new ConditionValue(); } return _tsvFileSizeFa; }
        }
        protected override ConditionValue getCValueTsvFileSizeFa() { return this.TsvFileSizeFa; }


        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeFa_Asc() { regOBA("TSV_FILE_SIZE_FA");return this; }
        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeFa_Desc() { regOBD("TSV_FILE_SIZE_FA");return this; }

        protected ConditionValue _tsvFileSizeDataOutput;
        public ConditionValue TsvFileSizeDataOutput {
            get { if (_tsvFileSizeDataOutput == null) { _tsvFileSizeDataOutput = new ConditionValue(); } return _tsvFileSizeDataOutput; }
        }
        protected override ConditionValue getCValueTsvFileSizeDataOutput() { return this.TsvFileSizeDataOutput; }


        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeDataOutput_Asc() { regOBA("TSV_FILE_SIZE_DATA_OUTPUT");return this; }
        public BsTOutputRequestCQ AddOrderBy_TsvFileSizeDataOutput_Desc() { regOBD("TSV_FILE_SIZE_DATA_OUTPUT");return this; }

        public BsTOutputRequestCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTOutputRequestCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TOutputRequestCQ baseQuery = (TOutputRequestCQ)baseQueryAsSuper;
            TOutputRequestCQ unionQuery = (TOutputRequestCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfo()) {
                unionQuery.QueryTQcwebSurveyInfo().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfo(), unionQuery.QueryTQcwebSurveyInfo());
            }
            if (baseQuery.hasConditionQueryTOutputReportsetInfo()) {
                unionQuery.QueryTOutputReportsetInfo().reflectRelationOnUnionQuery(baseQuery.QueryTOutputReportsetInfo(), unionQuery.QueryTOutputReportsetInfo());
            }
            if (baseQuery.hasConditionQueryTOutputCommon()) {
                unionQuery.QueryTOutputCommon().reflectRelationOnUnionQuery(baseQuery.QueryTOutputCommon(), unionQuery.QueryTOutputCommon());
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
            return resolveNextRelationPath("T_OUTPUT_REQUEST", "tQcwebSurveyInfo");
        }
        public bool hasConditionQueryTQcwebSurveyInfo() {
            return _conditionQueryTQcwebSurveyInfo != null;
        }
        protected TOutputReportsetInfoCQ _conditionQueryTOutputReportsetInfo;
        public TOutputReportsetInfoCQ QueryTOutputReportsetInfo() {
            return this.ConditionQueryTOutputReportsetInfo;
        }
        public TOutputReportsetInfoCQ ConditionQueryTOutputReportsetInfo {
            get {
                if (_conditionQueryTOutputReportsetInfo == null) {
                    _conditionQueryTOutputReportsetInfo = xcreateQueryTOutputReportsetInfo();
                    xsetupOuterJoin_TOutputReportsetInfo();
                }
                return _conditionQueryTOutputReportsetInfo;
            }
        }
        protected TOutputReportsetInfoCQ xcreateQueryTOutputReportsetInfo() {
            String nrp = resolveNextRelationPathTOutputReportsetInfo();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputReportsetInfoCQ cq = new TOutputReportsetInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputReportsetInfo"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputReportsetInfo() {
            TOutputReportsetInfoCQ cq = ConditionQueryTOutputReportsetInfo;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputReportsetInfo() {
            return resolveNextRelationPath("T_OUTPUT_REQUEST", "tOutputReportsetInfo");
        }
        public bool hasConditionQueryTOutputReportsetInfo() {
            return _conditionQueryTOutputReportsetInfo != null;
        }
        protected TOutputCommonCQ _conditionQueryTOutputCommon;
        public TOutputCommonCQ QueryTOutputCommon() {
            return this.ConditionQueryTOutputCommon;
        }
        public TOutputCommonCQ ConditionQueryTOutputCommon {
            get {
                if (_conditionQueryTOutputCommon == null) {
                    _conditionQueryTOutputCommon = xcreateQueryTOutputCommon();
                    xsetupOuterJoin_TOutputCommon();
                }
                return _conditionQueryTOutputCommon;
            }
        }
        protected TOutputCommonCQ xcreateQueryTOutputCommon() {
            String nrp = resolveNextRelationPathTOutputCommon();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TOutputCommonCQ cq = new TOutputCommonCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tOutputCommon"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TOutputCommon() {
            TOutputCommonCQ cq = ConditionQueryTOutputCommon;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("OUTPUT_REQUEST_ID", "Output_Request_ID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTOutputCommon() {
            return resolveNextRelationPath("T_OUTPUT_REQUEST", "tOutputCommon");
        }
        public bool hasConditionQueryTOutputCommon() {
            return _conditionQueryTOutputCommon != null;
        }


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TOutputRequestCQ> _scalarSubQueryMap;
	    public Map<String, TOutputRequestCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TOutputRequestCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TOutputRequestCQ> _myselfInScopeSubQueryMap;
        public Map<String, TOutputRequestCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TOutputRequestCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TOutputRequestCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
