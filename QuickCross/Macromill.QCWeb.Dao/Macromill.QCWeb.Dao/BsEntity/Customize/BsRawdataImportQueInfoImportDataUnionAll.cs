

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
    /// The entity of RawdataImportQueInfoImportDataUnionAll. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     RAWDATA_IMPORT_QUE_INFO_ID, QCWEB_JOB_NO, MAIN_SURVEY_ID, SURVEY_DATA_TYPE, FILEPATH, FILE_NAME, IMPORT_STATUS, MESSAGE, QCWEBID, ADD_DATA_NO
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
    [Seasar.Dao.Attrs.Table("RawdataImportQueInfoImportDataUnionAll")]
    [System.Serializable]
    public partial class RawdataImportQueInfoImportDataUnionAll : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>RAWDATA_IMPORT_QUE_INFO_ID: {NUMBER(22)}</summary>
        protected decimal? _rawdataImportQueInfoId;

        /// <summary>QCWEB_JOB_NO: {VARCHAR2(10)}</summary>
        protected String _qcwebJobNo;

        /// <summary>MAIN_SURVEY_ID: {NUMBER(22)}</summary>
        protected decimal? _mainSurveyId;

        /// <summary>SURVEY_DATA_TYPE: {VARCHAR2(1)}</summary>
        protected String _surveyDataType;

        /// <summary>FILEPATH: {VARCHAR2(260)}</summary>
        protected String _filepath;

        /// <summary>FILE_NAME: {VARCHAR2(500)}</summary>
        protected String _fileName;

        /// <summary>IMPORT_STATUS: {NUMBER(1)}</summary>
        protected int? _importStatus;

        /// <summary>MESSAGE: {VARCHAR2(1000)}</summary>
        protected String _message;

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>ADD_DATA_NO: {NUMBER(10)}</summary>
        protected long? _addDataNo;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "RawdataImportQueInfoImportDataUnionAll"; } }
        public String TablePropertyName { get { return "RawdataImportQueInfoImportDataUnionAll"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return RawdataImportQueInfoImportDataUnionAllDbm.GetInstance(); } }

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
            if (other == null || !(other is RawdataImportQueInfoImportDataUnionAll)) { return false; }
            RawdataImportQueInfoImportDataUnionAll otherEntity = (RawdataImportQueInfoImportDataUnionAll)other;
            if (!xSV(this.RawdataImportQueInfoId, otherEntity.RawdataImportQueInfoId)) { return false; }
            if (!xSV(this.QcwebJobNo, otherEntity.QcwebJobNo)) { return false; }
            if (!xSV(this.MainSurveyId, otherEntity.MainSurveyId)) { return false; }
            if (!xSV(this.SurveyDataType, otherEntity.SurveyDataType)) { return false; }
            if (!xSV(this.Filepath, otherEntity.Filepath)) { return false; }
            if (!xSV(this.FileName, otherEntity.FileName)) { return false; }
            if (!xSV(this.ImportStatus, otherEntity.ImportStatus)) { return false; }
            if (!xSV(this.Message, otherEntity.Message)) { return false; }
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            if (!xSV(this.AddDataNo, otherEntity.AddDataNo)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _rawdataImportQueInfoId);
            result = xCH(result, _qcwebJobNo);
            result = xCH(result, _mainSurveyId);
            result = xCH(result, _surveyDataType);
            result = xCH(result, _filepath);
            result = xCH(result, _fileName);
            result = xCH(result, _importStatus);
            result = xCH(result, _message);
            result = xCH(result, _qcwebid);
            result = xCH(result, _addDataNo);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "RawdataImportQueInfoImportDataUnionAll:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.RawdataImportQueInfoId);
            sb.Append(c).Append(this.QcwebJobNo);
            sb.Append(c).Append(this.MainSurveyId);
            sb.Append(c).Append(this.SurveyDataType);
            sb.Append(c).Append(this.Filepath);
            sb.Append(c).Append(this.FileName);
            sb.Append(c).Append(this.ImportStatus);
            sb.Append(c).Append(this.Message);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.AddDataNo);
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
        /// <summary>RAWDATA_IMPORT_QUE_INFO_ID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("RAWDATA_IMPORT_QUE_INFO_ID")]
        public decimal? RawdataImportQueInfoId {
            get { return _rawdataImportQueInfoId; }
            set {
                __modifiedProperties.AddPropertyName("RawdataImportQueInfoId");
                _rawdataImportQueInfoId = value;
            }
        }

        /// <summary>QCWEB_JOB_NO: {VARCHAR2(10)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEB_JOB_NO")]
        public String QcwebJobNo {
            get { return _qcwebJobNo; }
            set {
                __modifiedProperties.AddPropertyName("QcwebJobNo");
                _qcwebJobNo = value;
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

        /// <summary>SURVEY_DATA_TYPE: {VARCHAR2(1)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_DATA_TYPE")]
        public String SurveyDataType {
            get { return _surveyDataType; }
            set {
                __modifiedProperties.AddPropertyName("SurveyDataType");
                _surveyDataType = value;
            }
        }

        /// <summary>FILEPATH: {VARCHAR2(260)}</summary>
        [Seasar.Dao.Attrs.Column("FILEPATH")]
        public String Filepath {
            get { return _filepath; }
            set {
                __modifiedProperties.AddPropertyName("Filepath");
                _filepath = value;
            }
        }

        /// <summary>FILE_NAME: {VARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("FILE_NAME")]
        public String FileName {
            get { return _fileName; }
            set {
                __modifiedProperties.AddPropertyName("FileName");
                _fileName = value;
            }
        }

        /// <summary>IMPORT_STATUS: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("IMPORT_STATUS")]
        public int? ImportStatus {
            get { return _importStatus; }
            set {
                __modifiedProperties.AddPropertyName("ImportStatus");
                _importStatus = value;
            }
        }

        /// <summary>MESSAGE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("MESSAGE")]
        public String Message {
            get { return _message; }
            set {
                __modifiedProperties.AddPropertyName("Message");
                _message = value;
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

        /// <summary>ADD_DATA_NO: {NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("ADD_DATA_NO")]
        public long? AddDataNo {
            get { return _addDataNo; }
            set {
                __modifiedProperties.AddPropertyName("AddDataNo");
                _addDataNo = value;
            }
        }

        #endregion
    }
}
