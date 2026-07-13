

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
    /// The entity of T_ACCESS_PERMISSIONS_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, ACCESS_DATETIME, CLIENT_ID, ADMIN_ID, GUEST_ID
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
    [Seasar.Dao.Attrs.Table("T_ACCESS_PERMISSIONS_INFO")]
    [System.Serializable]
    public partial class TAccessPermissionsInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>ACCESS_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _accessDatetime;

        /// <summary>CLIENT_ID: {NotNull, NUMBER(22)}</summary>
        protected decimal? _clientId;

        /// <summary>ADMIN_ID: {NUMBER(22)}</summary>
        protected decimal? _adminId;

        /// <summary>GUEST_ID: {VARCHAR2(1000)}</summary>
        protected String _guestId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_ACCESS_PERMISSIONS_INFO"; } }
        public String TablePropertyName { get { return "TAccessPermissionsInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

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
            if (other == null || !(other is TAccessPermissionsInfo)) { return false; }
            TAccessPermissionsInfo otherEntity = (TAccessPermissionsInfo)other;
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
            return "TAccessPermissionsInfo:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.AccessDatetime);
            sb.Append(c).Append(this.ClientId);
            sb.Append(c).Append(this.AdminId);
            sb.Append(c).Append(this.GuestId);
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

        /// <summary>ACCESS_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("ACCESS_DATETIME")]
        public DateTime? AccessDatetime {
            get { return _accessDatetime; }
            set {
                __modifiedProperties.AddPropertyName("AccessDatetime");
                _accessDatetime = value;
            }
        }

        /// <summary>CLIENT_ID: {NotNull, NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("CLIENT_ID")]
        public decimal? ClientId {
            get { return _clientId; }
            set {
                __modifiedProperties.AddPropertyName("ClientId");
                _clientId = value;
            }
        }

        /// <summary>ADMIN_ID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("ADMIN_ID")]
        public decimal? AdminId {
            get { return _adminId; }
            set {
                __modifiedProperties.AddPropertyName("AdminId");
                _adminId = value;
            }
        }

        /// <summary>GUEST_ID: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("GUEST_ID")]
        public String GuestId {
            get { return _guestId; }
            set {
                __modifiedProperties.AddPropertyName("GuestId");
                _guestId = value;
            }
        }

        #endregion
    }
}
