

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
    /// The entity of TRawdataDeleteQueEntity. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     DELETE_KBN, RAWDATA_DELETE_QUE_ID, QCWEBID, MAIN_SURVEY_ID, SURVEY_INFO_ID
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
    [Seasar.Dao.Attrs.Table("TRawdataDeleteQueEntity")]
    [System.Serializable]
    public partial class TRawdataDeleteQueEntity : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DELETE_KBN: {NUMBER(22)}</summary>
        protected decimal? _deleteKbn;

        /// <summary>RAWDATA_DELETE_QUE_ID: {NUMBER(22)}</summary>
        protected decimal? _rawdataDeleteQueId;

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>MAIN_SURVEY_ID: {NUMBER(22)}</summary>
        protected decimal? _mainSurveyId;

        /// <summary>SURVEY_INFO_ID: {NUMBER(22)}</summary>
        protected decimal? _surveyInfoId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "TRawdataDeleteQueEntity"; } }
        public String TablePropertyName { get { return "TRawdataDeleteQueEntity"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return TRawdataDeleteQueEntityDbm.GetInstance(); } }

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
            if (other == null || !(other is TRawdataDeleteQueEntity)) { return false; }
            TRawdataDeleteQueEntity otherEntity = (TRawdataDeleteQueEntity)other;
            if (!xSV(this.DeleteKbn, otherEntity.DeleteKbn)) { return false; }
            if (!xSV(this.RawdataDeleteQueId, otherEntity.RawdataDeleteQueId)) { return false; }
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            if (!xSV(this.MainSurveyId, otherEntity.MainSurveyId)) { return false; }
            if (!xSV(this.SurveyInfoId, otherEntity.SurveyInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _deleteKbn);
            result = xCH(result, _rawdataDeleteQueId);
            result = xCH(result, _qcwebid);
            result = xCH(result, _mainSurveyId);
            result = xCH(result, _surveyInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TRawdataDeleteQueEntity:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.DeleteKbn);
            sb.Append(c).Append(this.RawdataDeleteQueId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.MainSurveyId);
            sb.Append(c).Append(this.SurveyInfoId);
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
        /// <summary>DELETE_KBN: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_KBN")]
        public decimal? DeleteKbn {
            get { return _deleteKbn; }
            set {
                __modifiedProperties.AddPropertyName("DeleteKbn");
                _deleteKbn = value;
            }
        }

        /// <summary>RAWDATA_DELETE_QUE_ID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("RAWDATA_DELETE_QUE_ID")]
        public decimal? RawdataDeleteQueId {
            get { return _rawdataDeleteQueId; }
            set {
                __modifiedProperties.AddPropertyName("RawdataDeleteQueId");
                _rawdataDeleteQueId = value;
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

        /// <summary>MAIN_SURVEY_ID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("MAIN_SURVEY_ID")]
        public decimal? MainSurveyId {
            get { return _mainSurveyId; }
            set {
                __modifiedProperties.AddPropertyName("MainSurveyId");
                _mainSurveyId = value;
            }
        }

        /// <summary>SURVEY_INFO_ID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_INFO_ID")]
        public decimal? SurveyInfoId {
            get { return _surveyInfoId; }
            set {
                __modifiedProperties.AddPropertyName("SurveyInfoId");
                _surveyInfoId = value;
            }
        }

        #endregion
    }
}
