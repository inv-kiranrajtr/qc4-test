

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
    /// The entity of T_OUTPUT_SETTING_REPORT as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, FILE_TYPE_EXCEL_FLAG, FILE_TYPE_PP_FLAG, FILE_TYPE_PDF_FLAG, REPORT_TYPE, GRAPH_OUTPUT_FLAG, ASC_FLAG, COMMENT_VISIBLE_FLAG, SURVEY_REPORT_FLAG, OUTPUT_TEMPLATE_ID
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
    ///     T_QCWEB_SURVEY_INFO
    /// 
    /// [referrer-table]
    ///     T_QCWEB_SURVEY_INFO
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tQcwebSurveyInfoAsOne
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_SETTING_REPORT")]
    [System.Serializable]
    public partial class TOutputSettingReport : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>FILE_TYPE_EXCEL_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _fileTypeExcelFlag;

        /// <summary>FILE_TYPE_PP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _fileTypePpFlag;

        /// <summary>FILE_TYPE_PDF_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _fileTypePdfFlag;

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1)}</summary>
        protected int? _reportType;

        /// <summary>GRAPH_OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _graphOutputFlag;

        /// <summary>ASC_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _ascFlag;

        /// <summary>COMMENT_VISIBLE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _commentVisibleFlag;

        /// <summary>SURVEY_REPORT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _surveyReportFlag;

        /// <summary>OUTPUT_TEMPLATE_ID: {IX, NUMBER(27)}</summary>
        protected decimal? _outputTemplateId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_SETTING_REPORT"; } }
        public String TablePropertyName { get { return "TOutputSettingReport"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag FileTypeExcelFlagAsFlag { get {
            return CDef.Flag.CodeOf(_fileTypeExcelFlag);
        } set {
            FileTypeExcelFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag FileTypePpFlagAsFlag { get {
            return CDef.Flag.CodeOf(_fileTypePpFlag);
        } set {
            FileTypePpFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag FileTypePdfFlagAsFlag { get {
            return CDef.Flag.CodeOf(_fileTypePdfFlag);
        } set {
            FileTypePdfFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag GraphOutputFlagAsFlag { get {
            return CDef.Flag.CodeOf(_graphOutputFlag);
        } set {
            GraphOutputFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag AscFlagAsFlag { get {
            return CDef.Flag.CodeOf(_ascFlag);
        } set {
            AscFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag CommentVisibleFlagAsFlag { get {
            return CDef.Flag.CodeOf(_commentVisibleFlag);
        } set {
            CommentVisibleFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag SurveyReportFlagAsFlag { get {
            return CDef.Flag.CodeOf(_surveyReportFlag);
        } set {
            SurveyReportFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of fileTypeExcelFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetFileTypeExcelFlag_True() {
            FileTypeExcelFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of fileTypeExcelFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetFileTypeExcelFlag_False() {
            FileTypeExcelFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of fileTypePpFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetFileTypePpFlag_True() {
            FileTypePpFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of fileTypePpFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetFileTypePpFlag_False() {
            FileTypePpFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of fileTypePdfFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetFileTypePdfFlag_True() {
            FileTypePdfFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of fileTypePdfFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetFileTypePdfFlag_False() {
            FileTypePdfFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of graphOutputFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetGraphOutputFlag_True() {
            GraphOutputFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of graphOutputFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetGraphOutputFlag_False() {
            GraphOutputFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of ascFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetAscFlag_True() {
            AscFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of ascFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetAscFlag_False() {
            AscFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of commentVisibleFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCommentVisibleFlag_True() {
            CommentVisibleFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of commentVisibleFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCommentVisibleFlag_False() {
            CommentVisibleFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of surveyReportFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetSurveyReportFlag_True() {
            SurveyReportFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of surveyReportFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetSurveyReportFlag_False() {
            SurveyReportFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of fileTypeExcelFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsFileTypeExcelFlagTrue {
            get {
                CDef.Flag cls = FileTypeExcelFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of fileTypeExcelFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsFileTypeExcelFlagFalse {
            get {
                CDef.Flag cls = FileTypeExcelFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of fileTypePpFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsFileTypePpFlagTrue {
            get {
                CDef.Flag cls = FileTypePpFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of fileTypePpFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsFileTypePpFlagFalse {
            get {
                CDef.Flag cls = FileTypePpFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of fileTypePdfFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsFileTypePdfFlagTrue {
            get {
                CDef.Flag cls = FileTypePdfFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of fileTypePdfFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsFileTypePdfFlagFalse {
            get {
                CDef.Flag cls = FileTypePdfFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of graphOutputFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsGraphOutputFlagTrue {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of graphOutputFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsGraphOutputFlagFalse {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of ascFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsAscFlagTrue {
            get {
                CDef.Flag cls = AscFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of ascFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsAscFlagFalse {
            get {
                CDef.Flag cls = AscFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of commentVisibleFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCommentVisibleFlagTrue {
            get {
                CDef.Flag cls = CommentVisibleFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of commentVisibleFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCommentVisibleFlagFalse {
            get {
                CDef.Flag cls = CommentVisibleFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of surveyReportFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsSurveyReportFlagTrue {
            get {
                CDef.Flag cls = SurveyReportFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of surveyReportFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsSurveyReportFlagFalse {
            get {
                CDef.Flag cls = SurveyReportFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String FileTypeExcelFlagName {
            get {
                CDef.Flag cls = FileTypeExcelFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String FileTypeExcelFlagAlias {
            get {
                CDef.Flag cls = FileTypeExcelFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String FileTypePpFlagName {
            get {
                CDef.Flag cls = FileTypePpFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String FileTypePpFlagAlias {
            get {
                CDef.Flag cls = FileTypePpFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String FileTypePdfFlagName {
            get {
                CDef.Flag cls = FileTypePdfFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String FileTypePdfFlagAlias {
            get {
                CDef.Flag cls = FileTypePdfFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String GraphOutputFlagName {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String GraphOutputFlagAlias {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String AscFlagName {
            get {
                CDef.Flag cls = AscFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String AscFlagAlias {
            get {
                CDef.Flag cls = AscFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String CommentVisibleFlagName {
            get {
                CDef.Flag cls = CommentVisibleFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CommentVisibleFlagAlias {
            get {
                CDef.Flag cls = CommentVisibleFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String SurveyReportFlagName {
            get {
                CDef.Flag cls = SurveyReportFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String SurveyReportFlagAlias {
            get {
                CDef.Flag cls = SurveyReportFlagAsFlag;
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

        protected TQcwebSurveyInfo _tQcwebSurveyInfoAsOne;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfoAsOne {
            get { return _tQcwebSurveyInfoAsOne; }
            set { _tQcwebSurveyInfoAsOne = value; }
        }

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
                if (_qcwebid == null) { return false; }
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
            if (other == null || !(other is TOutputSettingReport)) { return false; }
            TOutputSettingReport otherEntity = (TOutputSettingReport)other;
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _qcwebid);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputSettingReport:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tQcwebSurveyInfoAsOne != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfoAsOne, "TQcwebSurveyInfoAsOne")); }
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.FileTypeExcelFlag);
            sb.Append(c).Append(this.FileTypePpFlag);
            sb.Append(c).Append(this.FileTypePdfFlag);
            sb.Append(c).Append(this.ReportType);
            sb.Append(c).Append(this.GraphOutputFlag);
            sb.Append(c).Append(this.AscFlag);
            sb.Append(c).Append(this.CommentVisibleFlag);
            sb.Append(c).Append(this.SurveyReportFlag);
            sb.Append(c).Append(this.OutputTemplateId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tQcwebSurveyInfoAsOne != null) { sb.Append(c).Append("TQcwebSurveyInfoAsOne"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>FILE_TYPE_EXCEL_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("FILE_TYPE_EXCEL_FLAG")]
        public int? FileTypeExcelFlag {
            get { return _fileTypeExcelFlag; }
            set {
                __modifiedProperties.AddPropertyName("FileTypeExcelFlag");
                _fileTypeExcelFlag = value;
            }
        }

        /// <summary>FILE_TYPE_PP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("FILE_TYPE_PP_FLAG")]
        public int? FileTypePpFlag {
            get { return _fileTypePpFlag; }
            set {
                __modifiedProperties.AddPropertyName("FileTypePpFlag");
                _fileTypePpFlag = value;
            }
        }

        /// <summary>FILE_TYPE_PDF_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("FILE_TYPE_PDF_FLAG")]
        public int? FileTypePdfFlag {
            get { return _fileTypePdfFlag; }
            set {
                __modifiedProperties.AddPropertyName("FileTypePdfFlag");
                _fileTypePdfFlag = value;
            }
        }

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_TYPE")]
        public int? ReportType {
            get { return _reportType; }
            set {
                __modifiedProperties.AddPropertyName("ReportType");
                _reportType = value;
            }
        }

        /// <summary>GRAPH_OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_OUTPUT_FLAG")]
        public int? GraphOutputFlag {
            get { return _graphOutputFlag; }
            set {
                __modifiedProperties.AddPropertyName("GraphOutputFlag");
                _graphOutputFlag = value;
            }
        }

        /// <summary>ASC_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("ASC_FLAG")]
        public int? AscFlag {
            get { return _ascFlag; }
            set {
                __modifiedProperties.AddPropertyName("AscFlag");
                _ascFlag = value;
            }
        }

        /// <summary>COMMENT_VISIBLE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("COMMENT_VISIBLE_FLAG")]
        public int? CommentVisibleFlag {
            get { return _commentVisibleFlag; }
            set {
                __modifiedProperties.AddPropertyName("CommentVisibleFlag");
                _commentVisibleFlag = value;
            }
        }

        /// <summary>SURVEY_REPORT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_REPORT_FLAG")]
        public int? SurveyReportFlag {
            get { return _surveyReportFlag; }
            set {
                __modifiedProperties.AddPropertyName("SurveyReportFlag");
                _surveyReportFlag = value;
            }
        }

        /// <summary>OUTPUT_TEMPLATE_ID: {IX, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TEMPLATE_ID")]
        public decimal? OutputTemplateId {
            get { return _outputTemplateId; }
            set {
                __modifiedProperties.AddPropertyName("OutputTemplateId");
                _outputTemplateId = value;
            }
        }

        #endregion
    }
}
