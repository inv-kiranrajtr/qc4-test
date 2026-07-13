

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
    /// The entity of T_OUTPUT_TEMPLATE as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_TEMPLATE_ID
    /// 
    /// [column]
    ///     OUTPUT_TEMPLATE_ID, OUTPUT_TEMPLATE_MASTER_ID, UPLOAD_PATH, QCWEBID, ALIAS, CREATE_DATETIME, DELETE_FLAG
    /// 
    /// [sequence]
    ///     T_Output_Template_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_OUTPUT_TEMPLATE_MASTER, T_QCWEB_SURVEY_INFO
    /// 
    /// [referrer-table]
    ///     T_OUTPUT_REPORTSET_INFO
    /// 
    /// [foreign-property]
    ///     tOutputTemplateMaster, tQcwebSurveyInfo
    /// 
    /// [referrer-property]
    ///     tOutputReportsetInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_TEMPLATE")]
    [System.Serializable]
    public partial class TOutputTemplate : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_TEMPLATE_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputTemplateId;

        /// <summary>OUTPUT_TEMPLATE_MASTER_ID: {IX, NUMBER(27), FK to T_OUTPUT_TEMPLATE_MASTER}</summary>
        protected decimal? _outputTemplateMasterId;

        /// <summary>UPLOAD_PATH: {NotNull, VARCHAR2(780)}</summary>
        protected String _uploadPath;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>ALIAS: {NotNull, VARCHAR2(780)}</summary>
        protected String _alias;

        /// <summary>CREATE_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _createDatetime;

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), default=[0], classification=DeleteFlag}</summary>
        protected int? _deleteFlag;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_TEMPLATE"; } }
        public String TablePropertyName { get { return "TOutputTemplate"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.DeleteFlag DeleteFlagAsDeleteFlag { get {
            return CDef.DeleteFlag.CodeOf(_deleteFlag);
        } set {
            DeleteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
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

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
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

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
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

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TOutputTemplateMaster _tOutputTemplateMaster;

        /// <summary>T_OUTPUT_TEMPLATE_MASTER as 'TOutputTemplateMaster'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("OUTPUT_TEMPLATE_MASTER_ID:OUTPUT_TEMPLATE_MASTER_ID")]
        public TOutputTemplateMaster TOutputTemplateMaster {
            get { return _tOutputTemplateMaster; }
            set { _tOutputTemplateMaster = value; }
        }

        protected TQcwebSurveyInfo _tQcwebSurveyInfo;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfo {
            get { return _tQcwebSurveyInfo; }
            set { _tQcwebSurveyInfo = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TOutputReportsetInfo> _tOutputReportsetInfoList;

        /// <summary>T_OUTPUT_REPORTSET_INFO as 'TOutputReportsetInfoList'.</summary>
        public IList<TOutputReportsetInfo> TOutputReportsetInfoList {
            get { if (_tOutputReportsetInfoList == null) { _tOutputReportsetInfoList = new List<TOutputReportsetInfo>(); } return _tOutputReportsetInfoList; }
            set { _tOutputReportsetInfoList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_outputTemplateId == null) { return false; }
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
            if (other == null || !(other is TOutputTemplate)) { return false; }
            TOutputTemplate otherEntity = (TOutputTemplate)other;
            if (!xSV(this.OutputTemplateId, otherEntity.OutputTemplateId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputTemplateId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputTemplate:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tOutputTemplateMaster != null)
            { sb.Append(l).Append(xbRDS(_tOutputTemplateMaster, "TOutputTemplateMaster")); }
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tOutputReportsetInfoList != null) { foreach (Entity e in _tOutputReportsetInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputReportsetInfoList")); } } }
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
            sb.Append(c).Append(this.OutputTemplateId);
            sb.Append(c).Append(this.OutputTemplateMasterId);
            sb.Append(c).Append(this.UploadPath);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.Alias);
            sb.Append(c).Append(this.CreateDatetime);
            sb.Append(c).Append(this.DeleteFlag);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tOutputTemplateMaster != null) { sb.Append(c).Append("TOutputTemplateMaster"); }
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tOutputReportsetInfoList != null && _tOutputReportsetInfoList.Count > 0)
            { sb.Append(c).Append("TOutputReportsetInfoList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_TEMPLATE_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TEMPLATE_ID")]
        public decimal? OutputTemplateId {
            get { return _outputTemplateId; }
            set {
                __modifiedProperties.AddPropertyName("OutputTemplateId");
                _outputTemplateId = value;
            }
        }

        /// <summary>OUTPUT_TEMPLATE_MASTER_ID: {IX, NUMBER(27), FK to T_OUTPUT_TEMPLATE_MASTER}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TEMPLATE_MASTER_ID")]
        public decimal? OutputTemplateMasterId {
            get { return _outputTemplateMasterId; }
            set {
                __modifiedProperties.AddPropertyName("OutputTemplateMasterId");
                _outputTemplateMasterId = value;
            }
        }

        /// <summary>UPLOAD_PATH: {NotNull, VARCHAR2(780)}</summary>
        [Seasar.Dao.Attrs.Column("UPLOAD_PATH")]
        public String UploadPath {
            get { return _uploadPath; }
            set {
                __modifiedProperties.AddPropertyName("UploadPath");
                _uploadPath = value;
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

        /// <summary>ALIAS: {NotNull, VARCHAR2(780)}</summary>
        [Seasar.Dao.Attrs.Column("ALIAS")]
        public String Alias {
            get { return _alias; }
            set {
                __modifiedProperties.AddPropertyName("Alias");
                _alias = value;
            }
        }

        /// <summary>CREATE_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("CREATE_DATETIME")]
        public DateTime? CreateDatetime {
            get { return _createDatetime; }
            set {
                __modifiedProperties.AddPropertyName("CreateDatetime");
                _createDatetime = value;
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

        #endregion
    }
}
