

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Helper;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.BsEntity.Dbm;


namespace Macromill.QCWeb.Dao.ExEntity {

    /// <summary>
    /// The entity of T_OUTPUT_REQUEST as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_REQUEST_ID
    /// 
    /// [column]
    ///     OUTPUT_REQUEST_ID, REQUEST_SERVER_CODE, REQUEST_USER_ID, QCWEBID, LAST_DOWNLOAD_USERID, REQUEST_DATETIME, DOWNLOAD_PATH, PROC_SERVER_CODE, STATUS_CODE, DESCRIPTION, END_DATETIME, LAST_DOWNLOAD_DATETIME, EXCELBOOK_TYPE, NUMERIC_ANSWER_VIEW_CODE, DP_TOTAL, DP_AVERAGE, DP_STANDARD_DIV, DP_MIN, DP_MAX, DP_MEDIAN, DP_WEIGHT, DP_WEIGHTAVR, PROC_WEIGHT, OUTPUT_REPORTSET_INFO_ID, DELETE_FLAG, VIEW_SURVEY_NAME, LANGUAGE, SHOW_ZERO_NA_IV_CODE, MERGE_AXIS_CELLS_FLAG, SCENARIO_NAME, START_DATETIME, TEST_LOG_FLAG, TSV_FILE_SIZE_GT, TSV_FILE_SIZE_CROSS, TSV_FILE_SIZE_FA, TSV_FILE_SIZE_DATA_OUTPUT
    /// 
    /// [sequence]
    ///     T_Output_Request_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_QCWEB_SURVEY_INFO, T_OUTPUT_REPORTSET_INFO, T_Output_Common
    /// 
    /// [referrer-table]
    ///     T_OUTPUT_COMMON
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tOutputReportsetInfo, tOutputCommon
    /// 
    /// [referrer-property]
    ///     tOutputCommonList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_REQUEST")]
    [System.Serializable]
    public partial class TOutputRequest : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_REQUEST_ID: {PK, NotNull, NUMBER(27), FK to T_Output_Common}</summary>
        protected decimal? _outputRequestId;

        /// <summary>REQUEST_SERVER_CODE: {NotNull, VARCHAR2(24)}</summary>
        protected String _requestServerCode;

        /// <summary>REQUEST_USER_ID: {NotNull, VARCHAR2(1000)}</summary>
        protected String _requestUserId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>LAST_DOWNLOAD_USERID: {VARCHAR2(1000)}</summary>
        protected String _lastDownloadUserid;

        /// <summary>REQUEST_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _requestDatetime;

        /// <summary>DOWNLOAD_PATH: {VARCHAR2(260)}</summary>
        protected String _downloadPath;

        /// <summary>PROC_SERVER_CODE: {VARCHAR2(24), default=[0]}</summary>
        protected String _procServerCode;

        /// <summary>STATUS_CODE: {NotNull, NUMBER(5)}</summary>
        protected int? _statusCode;

        /// <summary>DESCRIPTION: {NVARCHAR2(256)}</summary>
        protected String _description;

        /// <summary>END_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _endDatetime;

        /// <summary>LAST_DOWNLOAD_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastDownloadDatetime;

        /// <summary>EXCELBOOK_TYPE: {NUMBER(2), classification=ExcelbookType}</summary>
        protected int? _excelbookType;

        /// <summary>NUMERIC_ANSWER_VIEW_CODE: {NUMBER(3)}</summary>
        protected int? _numericAnswerViewCode;

        /// <summary>DP_TOTAL: {NUMBER(2)}</summary>
        protected int? _dpTotal;

        /// <summary>DP_AVERAGE: {NUMBER(2)}</summary>
        protected int? _dpAverage;

        /// <summary>DP_STANDARD_DIV: {NUMBER(2)}</summary>
        protected int? _dpStandardDiv;

        /// <summary>DP_MIN: {NUMBER(2)}</summary>
        protected int? _dpMin;

        /// <summary>DP_MAX: {NUMBER(2)}</summary>
        protected int? _dpMax;

        /// <summary>DP_MEDIAN: {NUMBER(2)}</summary>
        protected int? _dpMedian;

        /// <summary>DP_WEIGHT: {NUMBER(2)}</summary>
        protected int? _dpWeight;

        /// <summary>DP_WEIGHTAVR: {NUMBER(2)}</summary>
        protected int? _dpWeightavr;

        /// <summary>PROC_WEIGHT: {NUMBER(2)}</summary>
        protected int? _procWeight;

        /// <summary>OUTPUT_REPORTSET_INFO_ID: {IX, NUMBER(27), FK to T_OUTPUT_REPORTSET_INFO}</summary>
        protected decimal? _outputReportsetInfoId;

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), default=[0], classification=DeleteFlag}</summary>
        protected int? _deleteFlag;

        /// <summary>VIEW_SURVEY_NAME: {NVARCHAR2(500)}</summary>
        protected String _viewSurveyName;

        /// <summary>LANGUAGE: {NotNull, VARCHAR2(5)}</summary>
        protected String _language;

        /// <summary>SHOW_ZERO_NA_IV_CODE: {NUMBER(1), default=[0]}</summary>
        protected int? _showZeroNaIvCode;

        /// <summary>MERGE_AXIS_CELLS_FLAG: {CHAR(1), default=[1], classification=Flag}</summary>
        protected String _mergeAxisCellsFlag;

        /// <summary>SCENARIO_NAME: {NVARCHAR2(200)}</summary>
        protected String _scenarioName;

        /// <summary>START_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _startDatetime;

        /// <summary>TEST_LOG_FLAG: {NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _testLogFlag;

        /// <summary>TSV_FILE_SIZE_GT: {NotNull, NUMBER(14), default=[0]}</summary>
        protected long? _tsvFileSizeGt;

        /// <summary>TSV_FILE_SIZE_CROSS: {NotNull, NUMBER(14), default=[0]}</summary>
        protected long? _tsvFileSizeCross;

        /// <summary>TSV_FILE_SIZE_FA: {NotNull, NUMBER(14), default=[0]}</summary>
        protected long? _tsvFileSizeFa;

        /// <summary>TSV_FILE_SIZE_DATA_OUTPUT: {NotNull, NUMBER(14), default=[0]}</summary>
        protected long? _tsvFileSizeDataOutput;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_REQUEST"; } }
        public String TablePropertyName { get { return "TOutputRequest"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.ExcelbookType ExcelbookTypeAsExcelbookType { get {
            return CDef.ExcelbookType.CodeOf(_excelbookType);
        } set {
            ExcelbookType = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.DeleteFlag DeleteFlagAsDeleteFlag { get {
            return CDef.DeleteFlag.CodeOf(_deleteFlag);
        } set {
            DeleteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag MergeAxisCellsFlagAsFlag { get {
            return CDef.Flag.CodeOf(_mergeAxisCellsFlag);
        } set {
            MergeAxisCellsFlag = value != null ? value.Code : null;
        }}

        public CDef.Flag TestLogFlagAsFlag { get {
            return CDef.Flag.CodeOf(_testLogFlag);
        } set {
            TestLogFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of excelbookType as EXL2003.
        /// <![CDATA[
        /// 2003形式: 2003形式を示す
        /// ]]>
        /// </summary>
        public void SetExcelbookType_EXL2003() {
            ExcelbookTypeAsExcelbookType = CDef.ExcelbookType.EXL2003;
        }

        /// <summary>
        /// Set the value of excelbookType as EXL2007.
        /// <![CDATA[
        /// 2007形式: 2007形式を示す
        /// ]]>
        /// </summary>
        public void SetExcelbookType_EXL2007() {
            ExcelbookTypeAsExcelbookType = CDef.ExcelbookType.EXL2007;
        }

        /// <summary>
        /// Set the value of deleteFlag as True.
        /// <![CDATA[
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_True() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.True;
        }

        /// <summary>
        /// Set the value of deleteFlag as False.
        /// <![CDATA[
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_False() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.False;
        }

        /// <summary>
        /// Set the value of mergeAxisCellsFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetMergeAxisCellsFlag_True() {
            MergeAxisCellsFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of mergeAxisCellsFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetMergeAxisCellsFlag_False() {
            MergeAxisCellsFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of testLogFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTestLogFlag_True() {
            TestLogFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of testLogFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTestLogFlag_False() {
            TestLogFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of excelbookType 'EXL2003'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 2003形式: 2003形式を示す
        /// ]]>
        /// </summary>
        public bool IsExcelbookTypeEXL2003 {
            get {
                CDef.ExcelbookType cls = ExcelbookTypeAsExcelbookType;
                return cls != null ? cls.Equals(CDef.ExcelbookType.EXL2003) : false;
            }
        }

        /// <summary>
        /// Is the value of excelbookType 'EXL2007'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 2007形式: 2007形式を示す
        /// ]]>
        /// </summary>
        public bool IsExcelbookTypeEXL2007 {
            get {
                CDef.ExcelbookType cls = ExcelbookTypeAsExcelbookType;
                return cls != null ? cls.Equals(CDef.ExcelbookType.EXL2007) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagTrue {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagFalse {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of mergeAxisCellsFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsMergeAxisCellsFlagTrue {
            get {
                CDef.Flag cls = MergeAxisCellsFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of mergeAxisCellsFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsMergeAxisCellsFlagFalse {
            get {
                CDef.Flag cls = MergeAxisCellsFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of testLogFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTestLogFlagTrue {
            get {
                CDef.Flag cls = TestLogFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of testLogFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTestLogFlagFalse {
            get {
                CDef.Flag cls = TestLogFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String ExcelbookTypeName {
            get {
                CDef.ExcelbookType cls = ExcelbookTypeAsExcelbookType;
                return cls != null ? cls.Name : null;
            }
        }
        public String ExcelbookTypeAlias {
            get {
                CDef.ExcelbookType cls = ExcelbookTypeAsExcelbookType;
                return cls != null ? cls.Alias : null;
            }
        }

        public String DeleteFlagName {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String DeleteFlagAlias {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String MergeAxisCellsFlagName {
            get {
                CDef.Flag cls = MergeAxisCellsFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String MergeAxisCellsFlagAlias {
            get {
                CDef.Flag cls = MergeAxisCellsFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String TestLogFlagName {
            get {
                CDef.Flag cls = TestLogFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TestLogFlagAlias {
            get {
                CDef.Flag cls = TestLogFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TQcwebSurveyInfo _tQcwebSurveyInfo;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfo {
            get { return _tQcwebSurveyInfo; }
            set { _tQcwebSurveyInfo = value; }
        }

        protected TOutputReportsetInfo _tOutputReportsetInfo;

        /// <summary>T_OUTPUT_REPORTSET_INFO as 'TOutputReportsetInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("OUTPUT_REPORTSET_INFO_ID:OUTPUT_REPORTSET_INFO_ID")]
        public TOutputReportsetInfo TOutputReportsetInfo {
            get { return _tOutputReportsetInfo; }
            set { _tOutputReportsetInfo = value; }
        }

        protected TOutputCommon _tOutputCommon;

        /// <summary>T_OUTPUT_COMMON as 'TOutputCommon'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("OUTPUT_REQUEST_ID:OUTPUT_REQUEST_ID")]
        public TOutputCommon TOutputCommon {
            get { return _tOutputCommon; }
            set { _tOutputCommon = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TOutputCommon> _tOutputCommonList;

        /// <summary>T_OUTPUT_COMMON as 'TOutputCommonList'.</summary>
        public IList<TOutputCommon> TOutputCommonList {
            get { if (_tOutputCommonList == null) { _tOutputCommonList = new List<TOutputCommon>(); } return _tOutputCommonList; }
            set { _tOutputCommonList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_outputRequestId == null) { return false; }
                return true;
            }
        }

        // ===============================================================================
        //                                                             Modified Properties
        //                                                             ===================
        public virtual IDictionary<String, Object> ModifiedPropertyNames {
            get { return __modifiedProperties.PropertyNames; }
        }

        public virtual void ClearModifiedPropertyNames() {
            __modifiedProperties.Clear();
        }

        // ===============================================================================
        //                                                                  Basic Override
        //                                                                  ==============
        #region Basic Override
        public override bool Equals(Object other) {
            if (other == null || !(other is TOutputRequest)) { return false; }
            TOutputRequest otherEntity = (TOutputRequest)other;
            if (!xSV(this.OutputRequestId, otherEntity.OutputRequestId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputRequestId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputRequest:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tOutputReportsetInfo != null)
            { sb.Append(l).Append(xbRDS(_tOutputReportsetInfo, "TOutputReportsetInfo")); }
            if (_tOutputCommon != null)
            { sb.Append(l).Append(xbRDS(_tOutputCommon, "TOutputCommon")); }
            if (_tOutputCommonList != null) { foreach (Entity e in _tOutputCommonList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputCommonList")); } } }
            return sb.ToString();
        }
        protected String xbRDS(Entity e, String name) { // buildRelationDisplayString()
            return e.BuildDisplayString(name, true, true);
        }

        public virtual String BuildDisplayString(String name, bool column, bool relation) {
            StringBuilder sb = new StringBuilder();
            if (name != null) { sb.Append(name).Append(column || relation ? ":" : ""); }
            if (column) { sb.Append(BuildColumnString()); }
            if (relation) { sb.Append(BuildRelationString()); }
            return sb.ToString();
        }
        protected virtual String BuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(this.OutputRequestId);
            sb.Append(c).Append(this.RequestServerCode);
            sb.Append(c).Append(this.RequestUserId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.LastDownloadUserid);
            sb.Append(c).Append(this.RequestDatetime);
            sb.Append(c).Append(this.DownloadPath);
            sb.Append(c).Append(this.ProcServerCode);
            sb.Append(c).Append(this.StatusCode);
            sb.Append(c).Append(this.Description);
            sb.Append(c).Append(this.EndDatetime);
            sb.Append(c).Append(this.LastDownloadDatetime);
            sb.Append(c).Append(this.ExcelbookType);
            sb.Append(c).Append(this.NumericAnswerViewCode);
            sb.Append(c).Append(this.DpTotal);
            sb.Append(c).Append(this.DpAverage);
            sb.Append(c).Append(this.DpStandardDiv);
            sb.Append(c).Append(this.DpMin);
            sb.Append(c).Append(this.DpMax);
            sb.Append(c).Append(this.DpMedian);
            sb.Append(c).Append(this.DpWeight);
            sb.Append(c).Append(this.DpWeightavr);
            sb.Append(c).Append(this.ProcWeight);
            sb.Append(c).Append(this.OutputReportsetInfoId);
            sb.Append(c).Append(this.DeleteFlag);
            sb.Append(c).Append(this.ViewSurveyName);
            sb.Append(c).Append(this.Language);
            sb.Append(c).Append(this.ShowZeroNaIvCode);
            sb.Append(c).Append(this.MergeAxisCellsFlag);
            sb.Append(c).Append(this.ScenarioName);
            sb.Append(c).Append(this.StartDatetime);
            sb.Append(c).Append(this.TestLogFlag);
            sb.Append(c).Append(this.TsvFileSizeGt);
            sb.Append(c).Append(this.TsvFileSizeCross);
            sb.Append(c).Append(this.TsvFileSizeFa);
            sb.Append(c).Append(this.TsvFileSizeDataOutput);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tOutputReportsetInfo != null) { sb.Append(c).Append("TOutputReportsetInfo"); }
            if (_tOutputCommon != null) { sb.Append(c).Append("TOutputCommon"); }
            if (_tOutputCommonList != null && _tOutputCommonList.Count > 0)
            { sb.Append(c).Append("TOutputCommonList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_REQUEST_ID: {PK, NotNull, NUMBER(27), FK to T_Output_Common}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REQUEST_ID")]
        public decimal? OutputRequestId {
            get { return _outputRequestId; }
            set {
                __modifiedProperties.AddPropertyName("OutputRequestId");
                _outputRequestId = value;
            }
        }

        /// <summary>REQUEST_SERVER_CODE: {NotNull, VARCHAR2(24)}</summary>
        [Seasar.Dao.Attrs.Column("REQUEST_SERVER_CODE")]
        public String RequestServerCode {
            get { return _requestServerCode; }
            set {
                __modifiedProperties.AddPropertyName("RequestServerCode");
                _requestServerCode = value;
            }
        }

        /// <summary>REQUEST_USER_ID: {NotNull, VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("REQUEST_USER_ID")]
        public String RequestUserId {
            get { return _requestUserId; }
            set {
                __modifiedProperties.AddPropertyName("RequestUserId");
                _requestUserId = value;
            }
        }

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>LAST_DOWNLOAD_USERID: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LAST_DOWNLOAD_USERID")]
        public String LastDownloadUserid {
            get { return _lastDownloadUserid; }
            set {
                __modifiedProperties.AddPropertyName("LastDownloadUserid");
                _lastDownloadUserid = value;
            }
        }

        /// <summary>REQUEST_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("REQUEST_DATETIME")]
        public DateTime? RequestDatetime {
            get { return _requestDatetime; }
            set {
                __modifiedProperties.AddPropertyName("RequestDatetime");
                _requestDatetime = value;
            }
        }

        /// <summary>DOWNLOAD_PATH: {VARCHAR2(260)}</summary>
        [Seasar.Dao.Attrs.Column("DOWNLOAD_PATH")]
        public String DownloadPath {
            get { return _downloadPath; }
            set {
                __modifiedProperties.AddPropertyName("DownloadPath");
                _downloadPath = value;
            }
        }

        /// <summary>PROC_SERVER_CODE: {VARCHAR2(24), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("PROC_SERVER_CODE")]
        public String ProcServerCode {
            get { return _procServerCode; }
            set {
                __modifiedProperties.AddPropertyName("ProcServerCode");
                _procServerCode = value;
            }
        }

        /// <summary>STATUS_CODE: {NotNull, NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("STATUS_CODE")]
        public int? StatusCode {
            get { return _statusCode; }
            set {
                __modifiedProperties.AddPropertyName("StatusCode");
                _statusCode = value;
            }
        }

        /// <summary>DESCRIPTION: {NVARCHAR2(256)}</summary>
        [Seasar.Dao.Attrs.Column("DESCRIPTION")]
        public String Description {
            get { return _description; }
            set {
                __modifiedProperties.AddPropertyName("Description");
                _description = value;
            }
        }

        /// <summary>END_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("END_DATETIME")]
        public DateTime? EndDatetime {
            get { return _endDatetime; }
            set {
                __modifiedProperties.AddPropertyName("EndDatetime");
                _endDatetime = value;
            }
        }

        /// <summary>LAST_DOWNLOAD_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("LAST_DOWNLOAD_DATETIME")]
        public DateTime? LastDownloadDatetime {
            get { return _lastDownloadDatetime; }
            set {
                __modifiedProperties.AddPropertyName("LastDownloadDatetime");
                _lastDownloadDatetime = value;
            }
        }

        /// <summary>EXCELBOOK_TYPE: {NUMBER(2), classification=ExcelbookType}</summary>
        [Seasar.Dao.Attrs.Column("EXCELBOOK_TYPE")]
        public int? ExcelbookType {
            get { return _excelbookType; }
            set {
                __modifiedProperties.AddPropertyName("ExcelbookType");
                _excelbookType = value;
            }
        }

        /// <summary>NUMERIC_ANSWER_VIEW_CODE: {NUMBER(3)}</summary>
        [Seasar.Dao.Attrs.Column("NUMERIC_ANSWER_VIEW_CODE")]
        public int? NumericAnswerViewCode {
            get { return _numericAnswerViewCode; }
            set {
                __modifiedProperties.AddPropertyName("NumericAnswerViewCode");
                _numericAnswerViewCode = value;
            }
        }

        /// <summary>DP_TOTAL: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_TOTAL")]
        public int? DpTotal {
            get { return _dpTotal; }
            set {
                __modifiedProperties.AddPropertyName("DpTotal");
                _dpTotal = value;
            }
        }

        /// <summary>DP_AVERAGE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_AVERAGE")]
        public int? DpAverage {
            get { return _dpAverage; }
            set {
                __modifiedProperties.AddPropertyName("DpAverage");
                _dpAverage = value;
            }
        }

        /// <summary>DP_STANDARD_DIV: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_STANDARD_DIV")]
        public int? DpStandardDiv {
            get { return _dpStandardDiv; }
            set {
                __modifiedProperties.AddPropertyName("DpStandardDiv");
                _dpStandardDiv = value;
            }
        }

        /// <summary>DP_MIN: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_MIN")]
        public int? DpMin {
            get { return _dpMin; }
            set {
                __modifiedProperties.AddPropertyName("DpMin");
                _dpMin = value;
            }
        }

        /// <summary>DP_MAX: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_MAX")]
        public int? DpMax {
            get { return _dpMax; }
            set {
                __modifiedProperties.AddPropertyName("DpMax");
                _dpMax = value;
            }
        }

        /// <summary>DP_MEDIAN: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_MEDIAN")]
        public int? DpMedian {
            get { return _dpMedian; }
            set {
                __modifiedProperties.AddPropertyName("DpMedian");
                _dpMedian = value;
            }
        }

        /// <summary>DP_WEIGHT: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_WEIGHT")]
        public int? DpWeight {
            get { return _dpWeight; }
            set {
                __modifiedProperties.AddPropertyName("DpWeight");
                _dpWeight = value;
            }
        }

        /// <summary>DP_WEIGHTAVR: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_WEIGHTAVR")]
        public int? DpWeightavr {
            get { return _dpWeightavr; }
            set {
                __modifiedProperties.AddPropertyName("DpWeightavr");
                _dpWeightavr = value;
            }
        }

        /// <summary>PROC_WEIGHT: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("PROC_WEIGHT")]
        public int? ProcWeight {
            get { return _procWeight; }
            set {
                __modifiedProperties.AddPropertyName("ProcWeight");
                _procWeight = value;
            }
        }

        /// <summary>OUTPUT_REPORTSET_INFO_ID: {IX, NUMBER(27), FK to T_OUTPUT_REPORTSET_INFO}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REPORTSET_INFO_ID")]
        public decimal? OutputReportsetInfoId {
            get { return _outputReportsetInfoId; }
            set {
                __modifiedProperties.AddPropertyName("OutputReportsetInfoId");
                _outputReportsetInfoId = value;
            }
        }

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), default=[0], classification=DeleteFlag}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_FLAG")]
        public int? DeleteFlag {
            get { return _deleteFlag; }
            set {
                __modifiedProperties.AddPropertyName("DeleteFlag");
                _deleteFlag = value;
            }
        }

        /// <summary>VIEW_SURVEY_NAME: {NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_SURVEY_NAME")]
        public String ViewSurveyName {
            get { return _viewSurveyName; }
            set {
                __modifiedProperties.AddPropertyName("ViewSurveyName");
                _viewSurveyName = value;
            }
        }

        /// <summary>LANGUAGE: {NotNull, VARCHAR2(5)}</summary>
        [Seasar.Dao.Attrs.Column("LANGUAGE")]
        public String Language {
            get { return _language; }
            set {
                __modifiedProperties.AddPropertyName("Language");
                _language = value;
            }
        }

        /// <summary>SHOW_ZERO_NA_IV_CODE: {NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SHOW_ZERO_NA_IV_CODE")]
        public int? ShowZeroNaIvCode {
            get { return _showZeroNaIvCode; }
            set {
                __modifiedProperties.AddPropertyName("ShowZeroNaIvCode");
                _showZeroNaIvCode = value;
            }
        }

        /// <summary>MERGE_AXIS_CELLS_FLAG: {CHAR(1), default=[1], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("MERGE_AXIS_CELLS_FLAG")]
        public String MergeAxisCellsFlag {
            get { return _mergeAxisCellsFlag; }
            set {
                __modifiedProperties.AddPropertyName("MergeAxisCellsFlag");
                _mergeAxisCellsFlag = value;
            }
        }

        /// <summary>SCENARIO_NAME: {NVARCHAR2(200)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_NAME")]
        public String ScenarioName {
            get { return _scenarioName; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioName");
                _scenarioName = value;
            }
        }

        /// <summary>START_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("START_DATETIME")]
        public DateTime? StartDatetime {
            get { return _startDatetime; }
            set {
                __modifiedProperties.AddPropertyName("StartDatetime");
                _startDatetime = value;
            }
        }

        /// <summary>TEST_LOG_FLAG: {NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TEST_LOG_FLAG")]
        public int? TestLogFlag {
            get { return _testLogFlag; }
            set {
                __modifiedProperties.AddPropertyName("TestLogFlag");
                _testLogFlag = value;
            }
        }

        /// <summary>TSV_FILE_SIZE_GT: {NotNull, NUMBER(14), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("TSV_FILE_SIZE_GT")]
        public long? TsvFileSizeGt {
            get { return _tsvFileSizeGt; }
            set {
                __modifiedProperties.AddPropertyName("TsvFileSizeGt");
                _tsvFileSizeGt = value;
            }
        }

        /// <summary>TSV_FILE_SIZE_CROSS: {NotNull, NUMBER(14), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("TSV_FILE_SIZE_CROSS")]
        public long? TsvFileSizeCross {
            get { return _tsvFileSizeCross; }
            set {
                __modifiedProperties.AddPropertyName("TsvFileSizeCross");
                _tsvFileSizeCross = value;
            }
        }

        /// <summary>TSV_FILE_SIZE_FA: {NotNull, NUMBER(14), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("TSV_FILE_SIZE_FA")]
        public long? TsvFileSizeFa {
            get { return _tsvFileSizeFa; }
            set {
                __modifiedProperties.AddPropertyName("TsvFileSizeFa");
                _tsvFileSizeFa = value;
            }
        }

        /// <summary>TSV_FILE_SIZE_DATA_OUTPUT: {NotNull, NUMBER(14), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("TSV_FILE_SIZE_DATA_OUTPUT")]
        public long? TsvFileSizeDataOutput {
            get { return _tsvFileSizeDataOutput; }
            set {
                __modifiedProperties.AddPropertyName("TsvFileSizeDataOutput");
                _tsvFileSizeDataOutput = value;
            }
        }

        #endregion
    }
}
