

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
    /// The entity of T_NOTICE as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     NOTICE_ID
    /// 
    /// [column]
    ///     NOTICE_ID, QCWEBID, USER_ID, DELETE_FLAG, NOTICE_INFO, NOTICE_TYPE, LINK_URL, EXPIRATION_STARTDATE, EXPIRATION_ENDDATE
    /// 
    /// [sequence]
    ///     T_Notice_SEQ_01
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
    ///     
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_NOTICE")]
    [System.Serializable]
    public partial class TNotice : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>NOTICE_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _noticeId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>USER_ID: {IX, NotNull, VARCHAR2(1000)}</summary>
        protected String _userId;

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), default=[0], classification=DeleteFlag}</summary>
        protected int? _deleteFlag;

        /// <summary>NOTICE_INFO: {NotNull, NVARCHAR2(1024)}</summary>
        protected String _noticeInfo;

        /// <summary>NOTICE_TYPE: {VARCHAR2(3)}</summary>
        protected String _noticeType;

        /// <summary>LINK_URL: {VARCHAR2(1024)}</summary>
        protected String _linkUrl;

        /// <summary>EXPIRATION_STARTDATE: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _expirationStartdate;

        /// <summary>EXPIRATION_ENDDATE: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _expirationEnddate;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_NOTICE"; } }
        public String TablePropertyName { get { return "TNotice"; } }

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
        protected TQcwebSurveyInfo _tQcwebSurveyInfo;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfo {
            get { return _tQcwebSurveyInfo; }
            set { _tQcwebSurveyInfo = value; }
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
                if (_noticeId == null) { return false; }
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
            if (other == null || !(other is TNotice)) { return false; }
            TNotice otherEntity = (TNotice)other;
            if (!xSV(this.NoticeId, otherEntity.NoticeId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _noticeId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TNotice:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
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
            sb.Append(c).Append(this.NoticeId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.UserId);
            sb.Append(c).Append(this.DeleteFlag);
            sb.Append(c).Append(this.NoticeInfo);
            sb.Append(c).Append(this.NoticeType);
            sb.Append(c).Append(this.LinkUrl);
            sb.Append(c).Append(this.ExpirationStartdate);
            sb.Append(c).Append(this.ExpirationEnddate);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>NOTICE_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("NOTICE_ID")]
        public decimal? NoticeId {
            get { return _noticeId; }
            set {
                __modifiedProperties.AddPropertyName("NoticeId");
                _noticeId = value;
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

        /// <summary>USER_ID: {IX, NotNull, VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("USER_ID")]
        public String UserId {
            get { return _userId; }
            set {
                __modifiedProperties.AddPropertyName("UserId");
                _userId = value;
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

        /// <summary>NOTICE_INFO: {NotNull, NVARCHAR2(1024)}</summary>
        [Seasar.Dao.Attrs.Column("NOTICE_INFO")]
        public String NoticeInfo {
            get { return _noticeInfo; }
            set {
                __modifiedProperties.AddPropertyName("NoticeInfo");
                _noticeInfo = value;
            }
        }

        /// <summary>NOTICE_TYPE: {VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("NOTICE_TYPE")]
        public String NoticeType {
            get { return _noticeType; }
            set {
                __modifiedProperties.AddPropertyName("NoticeType");
                _noticeType = value;
            }
        }

        /// <summary>LINK_URL: {VARCHAR2(1024)}</summary>
        [Seasar.Dao.Attrs.Column("LINK_URL")]
        public String LinkUrl {
            get { return _linkUrl; }
            set {
                __modifiedProperties.AddPropertyName("LinkUrl");
                _linkUrl = value;
            }
        }

        /// <summary>EXPIRATION_STARTDATE: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("EXPIRATION_STARTDATE")]
        public DateTime? ExpirationStartdate {
            get { return _expirationStartdate; }
            set {
                __modifiedProperties.AddPropertyName("ExpirationStartdate");
                _expirationStartdate = value;
            }
        }

        /// <summary>EXPIRATION_ENDDATE: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("EXPIRATION_ENDDATE")]
        public DateTime? ExpirationEnddate {
            get { return _expirationEnddate; }
            set {
                __modifiedProperties.AddPropertyName("ExpirationEnddate");
                _expirationEnddate = value;
            }
        }

        #endregion
    }
}
