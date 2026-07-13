

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
    /// The entity of T_OUTPUT_REPORTSET_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_REPORTSET_INFO_ID
    /// 
    /// [column]
    ///     OUTPUT_REPORTSET_INFO_ID, OUTPUT_FILE_TYPE_CODE, REPORT_FILEN_NAME_PREFIX, COMMENT_OUTPUT_FLAG, POWERPOINT_TYPE, OUTPUT_TEMPLATE_ID
    /// 
    /// [sequence]
    ///     T_Output_Reportset_Info_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_Output_Template
    /// 
    /// [referrer-table]
    ///     T_OUTPUT_REQUEST
    /// 
    /// [foreign-property]
    ///     tOutputTemplate
    /// 
    /// [referrer-property]
    ///     tOutputRequestList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_REPORTSET_INFO")]
    [System.Serializable]
    public partial class TOutputReportsetInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_REPORTSET_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputReportsetInfoId;

        /// <summary>OUTPUT_FILE_TYPE_CODE: {NotNull, NUMBER(2), default=[0]}</summary>
        protected int? _outputFileTypeCode;

        /// <summary>REPORT_FILEN_NAME_PREFIX: {VARCHAR2(100)}</summary>
        protected String _reportFilenNamePrefix;

        /// <summary>COMMENT_OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _commentOutputFlag;

        /// <summary>POWERPOINT_TYPE: {NUMBER(2)}</summary>
        protected int? _powerpointType;

        /// <summary>OUTPUT_TEMPLATE_ID: {IX, NUMBER(27), FK to T_Output_Template}</summary>
        protected decimal? _outputTemplateId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_REPORTSET_INFO"; } }
        public String TablePropertyName { get { return "TOutputReportsetInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag CommentOutputFlagAsFlag { get {
            return CDef.Flag.CodeOf(_commentOutputFlag);
        } set {
            CommentOutputFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
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

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
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

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
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

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TOutputTemplate _tOutputTemplate;

        /// <summary>T_OUTPUT_TEMPLATE as 'TOutputTemplate'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("OUTPUT_TEMPLATE_ID:OUTPUT_TEMPLATE_ID")]
        public TOutputTemplate TOutputTemplate {
            get { return _tOutputTemplate; }
            set { _tOutputTemplate = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TOutputRequest> _tOutputRequestList;

        /// <summary>T_OUTPUT_REQUEST as 'TOutputRequestList'.</summary>
        public IList<TOutputRequest> TOutputRequestList {
            get { if (_tOutputRequestList == null) { _tOutputRequestList = new List<TOutputRequest>(); } return _tOutputRequestList; }
            set { _tOutputRequestList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_outputReportsetInfoId == null) { return false; }
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
            if (other == null || !(other is TOutputReportsetInfo)) { return false; }
            TOutputReportsetInfo otherEntity = (TOutputReportsetInfo)other;
            if (!xSV(this.OutputReportsetInfoId, otherEntity.OutputReportsetInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputReportsetInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputReportsetInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tOutputTemplate != null)
            { sb.Append(l).Append(xbRDS(_tOutputTemplate, "TOutputTemplate")); }
            if (_tOutputRequestList != null) { foreach (Entity e in _tOutputRequestList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputRequestList")); } } }
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
            sb.Append(c).Append(this.OutputReportsetInfoId);
            sb.Append(c).Append(this.OutputFileTypeCode);
            sb.Append(c).Append(this.ReportFilenNamePrefix);
            sb.Append(c).Append(this.CommentOutputFlag);
            sb.Append(c).Append(this.PowerpointType);
            sb.Append(c).Append(this.OutputTemplateId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tOutputTemplate != null) { sb.Append(c).Append("TOutputTemplate"); }
            if (_tOutputRequestList != null && _tOutputRequestList.Count > 0)
            { sb.Append(c).Append("TOutputRequestList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_REPORTSET_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REPORTSET_INFO_ID")]
        public decimal? OutputReportsetInfoId {
            get { return _outputReportsetInfoId; }
            set {
                __modifiedProperties.AddPropertyName("OutputReportsetInfoId");
                _outputReportsetInfoId = value;
            }
        }

        /// <summary>OUTPUT_FILE_TYPE_CODE: {NotNull, NUMBER(2), default=[0]}</summary>
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

        /// <summary>COMMENT_OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
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

        /// <summary>OUTPUT_TEMPLATE_ID: {IX, NUMBER(27), FK to T_Output_Template}</summary>
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
