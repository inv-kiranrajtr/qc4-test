

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Helper;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Dao.BsEntity.Customize.Dbm;


namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    /// <summary>
    /// The entity of TOutputReportsetInfoRequest. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     OUTPUT_REQUEST_ID, QCWEBID, REQUEST_SERVER_CODE, DOWNLOAD_PATH, PROC_SERVER_CODE, VIEW_SURVEY_NAME, EXCELBOOK_TYPE, NUMERIC_ANSWER_VIEW_CODE, DP_TOTAL, DP_AVERAGE, DP_STANDARD_DIV, DP_MIN, DP_MAX, DP_MEDIAN, DP_WEIGHT, DP_WEIGHTAVR, LANGUAGE, SHOW_ZERO_NA_IV_CODE, MERGE_AXIS_CELLS_FLAG, PROC_WEIGHT, OUTPUT_REPORTSET_INFO_ID, OUTPUT_FILE_TYPE_CODE, REPORT_FILEN_NAME_PREFIX, COMMENT_OUTPUT_FLAG, POWERPOINT_TYPE, OUTPUT_TEMPLATE_ID, UPLOAD_PATH, PATH, OUTPUT_COMMON_ID, OUTPUT_TYPE, TSV_FILE_PATH, EXCELBOOK_NAME_PREFIX, WB_SETTING_CODE, NOANSWER_VISIBLE_CODE, UNMATCH_VISIBLE_CODE, UTF8_FLAG
    /// 
    /// [sequence]
    ///     
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("TOutputReportsetInfoRequest")]
    [System.Serializable]
    public partial class TOutputReportsetInfoRequest : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_REQUEST_ID: {NUMBER(27)}</summary>
        protected decimal? _outputRequestId;

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>REQUEST_SERVER_CODE: {VARCHAR2(24)}</summary>
        protected String _requestServerCode;

        /// <summary>DOWNLOAD_PATH: {VARCHAR2(260)}</summary>
        protected String _downloadPath;

        /// <summary>PROC_SERVER_CODE: {VARCHAR2(24)}</summary>
        protected String _procServerCode;

        /// <summary>VIEW_SURVEY_NAME: {VARCHAR2(500)}</summary>
        protected String _viewSurveyName;

        /// <summary>EXCELBOOK_TYPE: {NUMBER(2)}</summary>
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

        /// <summary>LANGUAGE: {VARCHAR2(5)}</summary>
        protected String _language;

        /// <summary>SHOW_ZERO_NA_IV_CODE: {NUMBER(1)}</summary>
        protected int? _showZeroNaIvCode;

        /// <summary>MERGE_AXIS_CELLS_FLAG: {CHAR(1), classification=Flag}</summary>
        protected String _mergeAxisCellsFlag;

        /// <summary>PROC_WEIGHT: {NUMBER(2)}</summary>
        protected int? _procWeight;

        /// <summary>OUTPUT_REPORTSET_INFO_ID: {NUMBER(27)}</summary>
        protected decimal? _outputReportsetInfoId;

        /// <summary>OUTPUT_FILE_TYPE_CODE: {NUMBER(2)}</summary>
        protected int? _outputFileTypeCode;

        /// <summary>REPORT_FILEN_NAME_PREFIX: {VARCHAR2(100)}</summary>
        protected String _reportFilenNamePrefix;

        /// <summary>COMMENT_OUTPUT_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _commentOutputFlag;

        /// <summary>POWERPOINT_TYPE: {NUMBER(2)}</summary>
        protected int? _powerpointType;

        /// <summary>OUTPUT_TEMPLATE_ID: {NUMBER(27)}</summary>
        protected decimal? _outputTemplateId;

        /// <summary>UPLOAD_PATH: {VARCHAR2(780)}</summary>
        protected String _uploadPath;

        /// <summary>PATH: {VARCHAR2(780)}</summary>
        protected String _path;

        /// <summary>OUTPUT_COMMON_ID: {NUMBER(27)}</summary>
        protected decimal? _outputCommonId;

        /// <summary>OUTPUT_TYPE: {NUMBER(1)}</summary>
        protected int? _outputType;

        /// <summary>TSV_FILE_PATH: {CLOB(4000)}</summary>
        protected String _tsvFilePath;

        /// <summary>EXCELBOOK_NAME_PREFIX: {VARCHAR2(100)}</summary>
        protected String _excelbookNamePrefix;

        /// <summary>WB_SETTING_CODE: {NUMBER(1)}</summary>
        protected int? _wbSettingCode;

        /// <summary>NOANSWER_VISIBLE_CODE: {NUMBER(1)}</summary>
        protected int? _noanswerVisibleCode;

        /// <summary>UNMATCH_VISIBLE_CODE: {NUMBER(1)}</summary>
        protected int? _unmatchVisibleCode;

        /// <summary>UTF8_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _utf8Flag;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "TOutputReportsetInfoRequest"; } }
        public String TablePropertyName { get { return "TOutputReportsetInfoRequest"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return TOutputReportsetInfoRequestDbm.GetInstance(); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag MergeAxisCellsFlagAsFlag { get {
            return CDef.Flag.CodeOf(_mergeAxisCellsFlag);
        } set {
            MergeAxisCellsFlag = value != null ? value.Code : null;
        }}

        public CDef.Flag CommentOutputFlagAsFlag { get {
            return CDef.Flag.CodeOf(_commentOutputFlag);
        } set {
            CommentOutputFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag Utf8FlagAsFlag { get {
            return CDef.Flag.CodeOf(_utf8Flag);
        } set {
            Utf8Flag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
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
        /// Set the value of commentOutputFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCommentOutputFlag_True() {
            CommentOutputFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of commentOutputFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCommentOutputFlag_False() {
            CommentOutputFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of utf8Flag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetUtf8Flag_True() {
            Utf8FlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of utf8Flag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetUtf8Flag_False() {
            Utf8FlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
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
        /// Is the value of commentOutputFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCommentOutputFlagTrue {
            get {
                CDef.Flag cls = CommentOutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of commentOutputFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCommentOutputFlagFalse {
            get {
                CDef.Flag cls = CommentOutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of utf8Flag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsUtf8FlagTrue {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of utf8Flag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsUtf8FlagFalse {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
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

        public String CommentOutputFlagName {
            get {
                CDef.Flag cls = CommentOutputFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CommentOutputFlagAlias {
            get {
                CDef.Flag cls = CommentOutputFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String Utf8FlagName {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String Utf8FlagAlias {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                return false;
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
            if (other == null || !(other is TOutputReportsetInfoRequest)) { return false; }
            TOutputReportsetInfoRequest otherEntity = (TOutputReportsetInfoRequest)other;
            if (!xSV(this.OutputRequestId, otherEntity.OutputRequestId)) { return false; }
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            if (!xSV(this.RequestServerCode, otherEntity.RequestServerCode)) { return false; }
            if (!xSV(this.DownloadPath, otherEntity.DownloadPath)) { return false; }
            if (!xSV(this.ProcServerCode, otherEntity.ProcServerCode)) { return false; }
            if (!xSV(this.ViewSurveyName, otherEntity.ViewSurveyName)) { return false; }
            if (!xSV(this.ExcelbookType, otherEntity.ExcelbookType)) { return false; }
            if (!xSV(this.NumericAnswerViewCode, otherEntity.NumericAnswerViewCode)) { return false; }
            if (!xSV(this.DpTotal, otherEntity.DpTotal)) { return false; }
            if (!xSV(this.DpAverage, otherEntity.DpAverage)) { return false; }
            if (!xSV(this.DpStandardDiv, otherEntity.DpStandardDiv)) { return false; }
            if (!xSV(this.DpMin, otherEntity.DpMin)) { return false; }
            if (!xSV(this.DpMax, otherEntity.DpMax)) { return false; }
            if (!xSV(this.DpMedian, otherEntity.DpMedian)) { return false; }
            if (!xSV(this.DpWeight, otherEntity.DpWeight)) { return false; }
            if (!xSV(this.DpWeightavr, otherEntity.DpWeightavr)) { return false; }
            if (!xSV(this.Language, otherEntity.Language)) { return false; }
            if (!xSV(this.ShowZeroNaIvCode, otherEntity.ShowZeroNaIvCode)) { return false; }
            if (!xSV(this.MergeAxisCellsFlag, otherEntity.MergeAxisCellsFlag)) { return false; }
            if (!xSV(this.ProcWeight, otherEntity.ProcWeight)) { return false; }
            if (!xSV(this.OutputReportsetInfoId, otherEntity.OutputReportsetInfoId)) { return false; }
            if (!xSV(this.OutputFileTypeCode, otherEntity.OutputFileTypeCode)) { return false; }
            if (!xSV(this.ReportFilenNamePrefix, otherEntity.ReportFilenNamePrefix)) { return false; }
            if (!xSV(this.CommentOutputFlag, otherEntity.CommentOutputFlag)) { return false; }
            if (!xSV(this.PowerpointType, otherEntity.PowerpointType)) { return false; }
            if (!xSV(this.OutputTemplateId, otherEntity.OutputTemplateId)) { return false; }
            if (!xSV(this.UploadPath, otherEntity.UploadPath)) { return false; }
            if (!xSV(this.Path, otherEntity.Path)) { return false; }
            if (!xSV(this.OutputCommonId, otherEntity.OutputCommonId)) { return false; }
            if (!xSV(this.OutputType, otherEntity.OutputType)) { return false; }
            if (!xSV(this.TsvFilePath, otherEntity.TsvFilePath)) { return false; }
            if (!xSV(this.ExcelbookNamePrefix, otherEntity.ExcelbookNamePrefix)) { return false; }
            if (!xSV(this.WbSettingCode, otherEntity.WbSettingCode)) { return false; }
            if (!xSV(this.NoanswerVisibleCode, otherEntity.NoanswerVisibleCode)) { return false; }
            if (!xSV(this.UnmatchVisibleCode, otherEntity.UnmatchVisibleCode)) { return false; }
            if (!xSV(this.Utf8Flag, otherEntity.Utf8Flag)) { return false; }
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
            result = xCH(result, _qcwebid);
            result = xCH(result, _requestServerCode);
            result = xCH(result, _downloadPath);
            result = xCH(result, _procServerCode);
            result = xCH(result, _viewSurveyName);
            result = xCH(result, _excelbookType);
            result = xCH(result, _numericAnswerViewCode);
            result = xCH(result, _dpTotal);
            result = xCH(result, _dpAverage);
            result = xCH(result, _dpStandardDiv);
            result = xCH(result, _dpMin);
            result = xCH(result, _dpMax);
            result = xCH(result, _dpMedian);
            result = xCH(result, _dpWeight);
            result = xCH(result, _dpWeightavr);
            result = xCH(result, _language);
            result = xCH(result, _showZeroNaIvCode);
            result = xCH(result, _mergeAxisCellsFlag);
            result = xCH(result, _procWeight);
            result = xCH(result, _outputReportsetInfoId);
            result = xCH(result, _outputFileTypeCode);
            result = xCH(result, _reportFilenNamePrefix);
            result = xCH(result, _commentOutputFlag);
            result = xCH(result, _powerpointType);
            result = xCH(result, _outputTemplateId);
            result = xCH(result, _uploadPath);
            result = xCH(result, _path);
            result = xCH(result, _outputCommonId);
            result = xCH(result, _outputType);
            result = xCH(result, _tsvFilePath);
            result = xCH(result, _excelbookNamePrefix);
            result = xCH(result, _wbSettingCode);
            result = xCH(result, _noanswerVisibleCode);
            result = xCH(result, _unmatchVisibleCode);
            result = xCH(result, _utf8Flag);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputReportsetInfoRequest:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            return sb.ToString();
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.RequestServerCode);
            sb.Append(c).Append(this.DownloadPath);
            sb.Append(c).Append(this.ProcServerCode);
            sb.Append(c).Append(this.ViewSurveyName);
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
            sb.Append(c).Append(this.Language);
            sb.Append(c).Append(this.ShowZeroNaIvCode);
            sb.Append(c).Append(this.MergeAxisCellsFlag);
            sb.Append(c).Append(this.ProcWeight);
            sb.Append(c).Append(this.OutputReportsetInfoId);
            sb.Append(c).Append(this.OutputFileTypeCode);
            sb.Append(c).Append(this.ReportFilenNamePrefix);
            sb.Append(c).Append(this.CommentOutputFlag);
            sb.Append(c).Append(this.PowerpointType);
            sb.Append(c).Append(this.OutputTemplateId);
            sb.Append(c).Append(this.UploadPath);
            sb.Append(c).Append(this.Path);
            sb.Append(c).Append(this.OutputCommonId);
            sb.Append(c).Append(this.OutputType);
            sb.Append(c).Append(this.TsvFilePath);
            sb.Append(c).Append(this.ExcelbookNamePrefix);
            sb.Append(c).Append(this.WbSettingCode);
            sb.Append(c).Append(this.NoanswerVisibleCode);
            sb.Append(c).Append(this.UnmatchVisibleCode);
            sb.Append(c).Append(this.Utf8Flag);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            return "";
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_REQUEST_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REQUEST_ID")]
        public decimal? OutputRequestId {
            get { return _outputRequestId; }
            set {
                __modifiedProperties.AddPropertyName("OutputRequestId");
                _outputRequestId = value;
            }
        }

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>REQUEST_SERVER_CODE: {VARCHAR2(24)}</summary>
        [Seasar.Dao.Attrs.Column("REQUEST_SERVER_CODE")]
        public String RequestServerCode {
            get { return _requestServerCode; }
            set {
                __modifiedProperties.AddPropertyName("RequestServerCode");
                _requestServerCode = value;
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

        /// <summary>PROC_SERVER_CODE: {VARCHAR2(24)}</summary>
        [Seasar.Dao.Attrs.Column("PROC_SERVER_CODE")]
        public String ProcServerCode {
            get { return _procServerCode; }
            set {
                __modifiedProperties.AddPropertyName("ProcServerCode");
                _procServerCode = value;
            }
        }

        /// <summary>VIEW_SURVEY_NAME: {VARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_SURVEY_NAME")]
        public String ViewSurveyName {
            get { return _viewSurveyName; }
            set {
                __modifiedProperties.AddPropertyName("ViewSurveyName");
                _viewSurveyName = value;
            }
        }

        /// <summary>EXCELBOOK_TYPE: {NUMBER(2)}</summary>
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

        /// <summary>LANGUAGE: {VARCHAR2(5)}</summary>
        [Seasar.Dao.Attrs.Column("LANGUAGE")]
        public String Language {
            get { return _language; }
            set {
                __modifiedProperties.AddPropertyName("Language");
                _language = value;
            }
        }

        /// <summary>SHOW_ZERO_NA_IV_CODE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("SHOW_ZERO_NA_IV_CODE")]
        public int? ShowZeroNaIvCode {
            get { return _showZeroNaIvCode; }
            set {
                __modifiedProperties.AddPropertyName("ShowZeroNaIvCode");
                _showZeroNaIvCode = value;
            }
        }

        /// <summary>MERGE_AXIS_CELLS_FLAG: {CHAR(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("MERGE_AXIS_CELLS_FLAG")]
        public String MergeAxisCellsFlag {
            get { return _mergeAxisCellsFlag; }
            set {
                __modifiedProperties.AddPropertyName("MergeAxisCellsFlag");
                _mergeAxisCellsFlag = value;
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

        /// <summary>OUTPUT_REPORTSET_INFO_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REPORTSET_INFO_ID")]
        public decimal? OutputReportsetInfoId {
            get { return _outputReportsetInfoId; }
            set {
                __modifiedProperties.AddPropertyName("OutputReportsetInfoId");
                _outputReportsetInfoId = value;
            }
        }

        /// <summary>OUTPUT_FILE_TYPE_CODE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_FILE_TYPE_CODE")]
        public int? OutputFileTypeCode {
            get { return _outputFileTypeCode; }
            set {
                __modifiedProperties.AddPropertyName("OutputFileTypeCode");
                _outputFileTypeCode = value;
            }
        }

        /// <summary>REPORT_FILEN_NAME_PREFIX: {VARCHAR2(100)}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_FILEN_NAME_PREFIX")]
        public String ReportFilenNamePrefix {
            get { return _reportFilenNamePrefix; }
            set {
                __modifiedProperties.AddPropertyName("ReportFilenNamePrefix");
                _reportFilenNamePrefix = value;
            }
        }

        /// <summary>COMMENT_OUTPUT_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("COMMENT_OUTPUT_FLAG")]
        public int? CommentOutputFlag {
            get { return _commentOutputFlag; }
            set {
                __modifiedProperties.AddPropertyName("CommentOutputFlag");
                _commentOutputFlag = value;
            }
        }

        /// <summary>POWERPOINT_TYPE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("POWERPOINT_TYPE")]
        public int? PowerpointType {
            get { return _powerpointType; }
            set {
                __modifiedProperties.AddPropertyName("PowerpointType");
                _powerpointType = value;
            }
        }

        /// <summary>OUTPUT_TEMPLATE_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TEMPLATE_ID")]
        public decimal? OutputTemplateId {
            get { return _outputTemplateId; }
            set {
                __modifiedProperties.AddPropertyName("OutputTemplateId");
                _outputTemplateId = value;
            }
        }

        /// <summary>UPLOAD_PATH: {VARCHAR2(780)}</summary>
        [Seasar.Dao.Attrs.Column("UPLOAD_PATH")]
        public String UploadPath {
            get { return _uploadPath; }
            set {
                __modifiedProperties.AddPropertyName("UploadPath");
                _uploadPath = value;
            }
        }

        /// <summary>PATH: {VARCHAR2(780)}</summary>
        [Seasar.Dao.Attrs.Column("PATH")]
        public String Path {
            get { return _path; }
            set {
                __modifiedProperties.AddPropertyName("Path");
                _path = value;
            }
        }

        /// <summary>OUTPUT_COMMON_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_COMMON_ID")]
        public decimal? OutputCommonId {
            get { return _outputCommonId; }
            set {
                __modifiedProperties.AddPropertyName("OutputCommonId");
                _outputCommonId = value;
            }
        }

        /// <summary>OUTPUT_TYPE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TYPE")]
        public int? OutputType {
            get { return _outputType; }
            set {
                __modifiedProperties.AddPropertyName("OutputType");
                _outputType = value;
            }
        }

        /// <summary>TSV_FILE_PATH: {CLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("TSV_FILE_PATH")]
        public String TsvFilePath {
            get { return _tsvFilePath; }
            set {
                __modifiedProperties.AddPropertyName("TsvFilePath");
                _tsvFilePath = value;
            }
        }

        /// <summary>EXCELBOOK_NAME_PREFIX: {VARCHAR2(100)}</summary>
        [Seasar.Dao.Attrs.Column("EXCELBOOK_NAME_PREFIX")]
        public String ExcelbookNamePrefix {
            get { return _excelbookNamePrefix; }
            set {
                __modifiedProperties.AddPropertyName("ExcelbookNamePrefix");
                _excelbookNamePrefix = value;
            }
        }

        /// <summary>WB_SETTING_CODE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("WB_SETTING_CODE")]
        public int? WbSettingCode {
            get { return _wbSettingCode; }
            set {
                __modifiedProperties.AddPropertyName("WbSettingCode");
                _wbSettingCode = value;
            }
        }

        /// <summary>NOANSWER_VISIBLE_CODE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_VISIBLE_CODE")]
        public int? NoanswerVisibleCode {
            get { return _noanswerVisibleCode; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerVisibleCode");
                _noanswerVisibleCode = value;
            }
        }

        /// <summary>UNMATCH_VISIBLE_CODE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("UNMATCH_VISIBLE_CODE")]
        public int? UnmatchVisibleCode {
            get { return _unmatchVisibleCode; }
            set {
                __modifiedProperties.AddPropertyName("UnmatchVisibleCode");
                _unmatchVisibleCode = value;
            }
        }

        /// <summary>UTF8_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("UTF8_FLAG")]
        public int? Utf8Flag {
            get { return _utf8Flag; }
            set {
                __modifiedProperties.AddPropertyName("Utf8Flag");
                _utf8Flag = value;
            }
        }

        #endregion
    }
}
