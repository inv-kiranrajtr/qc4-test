

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
    /// The entity of T_QCWEB_SURVEY_DETAIL as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEB_DETAIL_ID
    /// 
    /// [column]
    ///     QCWEB_DETAIL_ID, QCWEBID, SURVEY_NO, SURVEY_NAME, QC3UNIQUE_ID, SURVEY_METHOD, SERVICE_TYPE, SURVEY_DATE
    /// 
    /// [sequence]
    ///     T_QCWeb_Survey_Detail_SEQ_01
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
    [Seasar.Dao.Attrs.Table("T_QCWEB_SURVEY_DETAIL")]
    [System.Serializable]
    public partial class TQcwebSurveyDetail : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEB_DETAIL_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _qcwebDetailId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>SURVEY_NO: {NUMBER(5)}</summary>
        protected int? _surveyNo;

        /// <summary>SURVEY_NAME: {NVARCHAR2(500)}</summary>
        protected String _surveyName;

        /// <summary>QC3UNIQUE_ID: {NUMBER(22)}</summary>
        protected decimal? _qc3uniqueId;

        /// <summary>SURVEY_METHOD: {NVARCHAR2(30)}</summary>
        protected String _surveyMethod;

        /// <summary>SERVICE_TYPE: {VARCHAR2(15)}</summary>
        protected String _serviceType;

        /// <summary>SURVEY_DATE: {NVARCHAR2(44)}</summary>
        protected String _surveyDate;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_QCWEB_SURVEY_DETAIL"; } }
        public String TablePropertyName { get { return "TQcwebSurveyDetail"; } }

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
                if (_qcwebDetailId == null) { return false; }
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
            if (other == null || !(other is TQcwebSurveyDetail)) { return false; }
            TQcwebSurveyDetail otherEntity = (TQcwebSurveyDetail)other;
            if (!xSV(this.QcwebDetailId, otherEntity.QcwebDetailId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _qcwebDetailId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TQcwebSurveyDetail:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.QcwebDetailId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.SurveyNo);
            sb.Append(c).Append(this.SurveyName);
            sb.Append(c).Append(this.Qc3uniqueId);
            sb.Append(c).Append(this.SurveyMethod);
            sb.Append(c).Append(this.ServiceType);
            sb.Append(c).Append(this.SurveyDate);
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
        /// <summary>QCWEB_DETAIL_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEB_DETAIL_ID")]
        public decimal? QcwebDetailId {
            get { return _qcwebDetailId; }
            set {
                __modifiedProperties.AddPropertyName("QcwebDetailId");
                _qcwebDetailId = value;
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

        /// <summary>SURVEY_NO: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_NO")]
        public int? SurveyNo {
            get { return _surveyNo; }
            set {
                __modifiedProperties.AddPropertyName("SurveyNo");
                _surveyNo = value;
            }
        }

        /// <summary>SURVEY_NAME: {NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_NAME")]
        public String SurveyName {
            get { return _surveyName; }
            set {
                __modifiedProperties.AddPropertyName("SurveyName");
                _surveyName = value;
            }
        }

        /// <summary>QC3UNIQUE_ID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("QC3UNIQUE_ID")]
        public decimal? Qc3uniqueId {
            get { return _qc3uniqueId; }
            set {
                __modifiedProperties.AddPropertyName("Qc3uniqueId");
                _qc3uniqueId = value;
            }
        }

        /// <summary>SURVEY_METHOD: {NVARCHAR2(30)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_METHOD")]
        public String SurveyMethod {
            get { return _surveyMethod; }
            set {
                __modifiedProperties.AddPropertyName("SurveyMethod");
                _surveyMethod = value;
            }
        }

        /// <summary>SERVICE_TYPE: {VARCHAR2(15)}</summary>
        [Seasar.Dao.Attrs.Column("SERVICE_TYPE")]
        public String ServiceType {
            get { return _serviceType; }
            set {
                __modifiedProperties.AddPropertyName("ServiceType");
                _serviceType = value;
            }
        }

        /// <summary>SURVEY_DATE: {NVARCHAR2(44)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_DATE")]
        public String SurveyDate {
            get { return _surveyDate; }
            set {
                __modifiedProperties.AddPropertyName("SurveyDate");
                _surveyDate = value;
            }
        }

        #endregion
    }
}
