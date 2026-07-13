

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
    /// The entity of T_RAWDATA_IMPORT_QUE_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     RAWDATA_IMPORT_QUE_INFO_ID
    /// 
    /// [column]
    ///     RAWDATA_IMPORT_QUE_INFO_ID, QCWEB_JOB_NO, MAIN_SURVEY_ID, SURVEY_DATA_TYPE, FILEPATH, FILE_NAME, IMPORT_STATUS, MESSAGE, QCWEBID, ADD_DATA_NO, REQUEST_DATETIME
    /// 
    /// [sequence]
    ///     T_RawData_Import_Que_Info_SEQ1
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
    ///     tQcwebSurveyInfo
    /// 
    /// [referrer-property]
    ///     tQcwebSurveyInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_RAWDATA_IMPORT_QUE_INFO")]
    [System.Serializable]
    public partial class TRawdataImportQueInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>RAWDATA_IMPORT_QUE_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _rawdataImportQueInfoId;

        /// <summary>QCWEB_JOB_NO: {UQ, NotNull, VARCHAR2(10)}</summary>
        protected String _qcwebJobNo;

        /// <summary>MAIN_SURVEY_ID: {NotNull, NUMBER(22)}</summary>
        protected decimal? _mainSurveyId;

        /// <summary>SURVEY_DATA_TYPE: {NotNull, VARCHAR2(1), default=[0], classification=SurveyDataType}</summary>
        protected String _surveyDataType;

        /// <summary>FILEPATH: {NotNull, VARCHAR2(260)}</summary>
        protected String _filepath;

        /// <summary>FILE_NAME: {NotNull, NVARCHAR2(500)}</summary>
        protected String _fileName;

        /// <summary>IMPORT_STATUS: {NotNull, NUMBER(1), default=[0], classification=ImportStatus}</summary>
        protected int? _importStatus;

        /// <summary>MESSAGE: {NVARCHAR2(1000)}</summary>
        protected String _message;

        /// <summary>QCWEBID: {IX, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>ADD_DATA_NO: {NUMBER(10)}</summary>
        protected long? _addDataNo;

        /// <summary>REQUEST_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _requestDatetime;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_RAWDATA_IMPORT_QUE_INFO"; } }
        public String TablePropertyName { get { return "TRawdataImportQueInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.SurveyDataType SurveyDataTypeAsSurveyDataType { get {
            return CDef.SurveyDataType.CodeOf(_surveyDataType);
        } set {
            SurveyDataType = value != null ? value.Code : null;
        }}

        public CDef.ImportStatus ImportStatusAsImportStatus { get {
            return CDef.ImportStatus.CodeOf(_importStatus);
        } set {
            ImportStatus = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of surveyDataType as NORMAL.
        /// <![CDATA[
        /// 標準納品ファイル: 標準納品ファイルを示す
        /// ]]>
        /// </summary>
        public void SetSurveyDataType_NORMAL() {
            SurveyDataTypeAsSurveyDataType = CDef.SurveyDataType.NORMAL;
        }

        /// <summary>
        /// Set the value of surveyDataType as ADD.
        /// <![CDATA[
        /// 追加納品ファイル: 追加納品ファイルを示す
        /// ]]>
        /// </summary>
        public void SetSurveyDataType_ADD() {
            SurveyDataTypeAsSurveyDataType = CDef.SurveyDataType.ADD;
        }

        /// <summary>
        /// Set the value of importStatus as NONE_IMPORT.
        /// <![CDATA[
        /// 未取込: 未取込を示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_NONE_IMPORT() {
            ImportStatusAsImportStatus = CDef.ImportStatus.NONE_IMPORT;
        }

        /// <summary>
        /// Set the value of importStatus as IMPORT_EXEC.
        /// <![CDATA[
        /// 取込中: 取込中を示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_IMPORT_EXEC() {
            ImportStatusAsImportStatus = CDef.ImportStatus.IMPORT_EXEC;
        }

        /// <summary>
        /// Set the value of importStatus as IMPORT_END_ZIP.
        /// <![CDATA[
        /// 取込完(パスワード付きZIP): 取込完(パスワード付きZIP)を示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_IMPORT_END_ZIP() {
            ImportStatusAsImportStatus = CDef.ImportStatus.IMPORT_END_ZIP;
        }

        /// <summary>
        /// Set the value of importStatus as IMPORT_END.
        /// <![CDATA[
        /// 取込完: 取込完を示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_IMPORT_END() {
            ImportStatusAsImportStatus = CDef.ImportStatus.IMPORT_END;
        }

        /// <summary>
        /// Set the value of importStatus as IMPORT_ERROR.
        /// <![CDATA[
        /// エラー: エラーありを示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_IMPORT_ERROR() {
            ImportStatusAsImportStatus = CDef.ImportStatus.IMPORT_ERROR;
        }

        /// <summary>
        /// Set the value of importStatus as IMPORT_SKIP.
        /// <![CDATA[
        /// スキップ: 処理をスキップしたことを示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_IMPORT_SKIP() {
            ImportStatusAsImportStatus = CDef.ImportStatus.IMPORT_SKIP;
        }

        /// <summary>
        /// Set the value of importStatus as IMPORT_END_PART_ERROR.
        /// <![CDATA[
        /// 取込完(一部エラーあり): 取込完(一部エラーあり)を示す
        /// ]]>
        /// </summary>
        public void SetImportStatus_IMPORT_END_PART_ERROR() {
            ImportStatusAsImportStatus = CDef.ImportStatus.IMPORT_END_PART_ERROR;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of surveyDataType 'NORMAL'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 標準納品ファイル: 標準納品ファイルを示す
        /// ]]>
        /// </summary>
        public bool IsSurveyDataTypeNORMAL {
            get {
                CDef.SurveyDataType cls = SurveyDataTypeAsSurveyDataType;
                return cls != null ? cls.Equals(CDef.SurveyDataType.NORMAL) : false;
            }
        }

        /// <summary>
        /// Is the value of surveyDataType 'ADD'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 追加納品ファイル: 追加納品ファイルを示す
        /// ]]>
        /// </summary>
        public bool IsSurveyDataTypeADD {
            get {
                CDef.SurveyDataType cls = SurveyDataTypeAsSurveyDataType;
                return cls != null ? cls.Equals(CDef.SurveyDataType.ADD) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'NONE_IMPORT'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 未取込: 未取込を示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusNONE_IMPORT {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.NONE_IMPORT) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'IMPORT_EXEC'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 取込中: 取込中を示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusIMPORT_EXEC {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.IMPORT_EXEC) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'IMPORT_END_ZIP'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 取込完(パスワード付きZIP): 取込完(パスワード付きZIP)を示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusIMPORT_END_ZIP {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.IMPORT_END_ZIP) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'IMPORT_END'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 取込完: 取込完を示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusIMPORT_END {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.IMPORT_END) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'IMPORT_ERROR'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// エラー: エラーありを示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusIMPORT_ERROR {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.IMPORT_ERROR) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'IMPORT_SKIP'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// スキップ: 処理をスキップしたことを示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusIMPORT_SKIP {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.IMPORT_SKIP) : false;
            }
        }

        /// <summary>
        /// Is the value of importStatus 'IMPORT_END_PART_ERROR'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 取込完(一部エラーあり): 取込完(一部エラーあり)を示す
        /// ]]>
        /// </summary>
        public bool IsImportStatusIMPORT_END_PART_ERROR {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Equals(CDef.ImportStatus.IMPORT_END_PART_ERROR) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String SurveyDataTypeName {
            get {
                CDef.SurveyDataType cls = SurveyDataTypeAsSurveyDataType;
                return cls != null ? cls.Name : null;
            }
        }
        public String SurveyDataTypeAlias {
            get {
                CDef.SurveyDataType cls = SurveyDataTypeAsSurveyDataType;
                return cls != null ? cls.Alias : null;
            }
        }

        public String ImportStatusName {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
                return cls != null ? cls.Name : null;
            }
        }
        public String ImportStatusAlias {
            get {
                CDef.ImportStatus cls = ImportStatusAsImportStatus;
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
        protected IList<TQcwebSurveyInfo> _tQcwebSurveyInfoList;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoList'.</summary>
        public IList<TQcwebSurveyInfo> TQcwebSurveyInfoList {
            get { if (_tQcwebSurveyInfoList == null) { _tQcwebSurveyInfoList = new List<TQcwebSurveyInfo>(); } return _tQcwebSurveyInfoList; }
            set { _tQcwebSurveyInfoList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_rawdataImportQueInfoId == null) { return false; }
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
            if (other == null || !(other is TRawdataImportQueInfo)) { return false; }
            TRawdataImportQueInfo otherEntity = (TRawdataImportQueInfo)other;
            if (!xSV(this.RawdataImportQueInfoId, otherEntity.RawdataImportQueInfoId)) { return false; }
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
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TRawdataImportQueInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tQcwebSurveyInfoList != null) { foreach (Entity e in _tQcwebSurveyInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TQcwebSurveyInfoList")); } } }
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
            sb.Append(c).Append(this.RequestDatetime);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tQcwebSurveyInfoList != null && _tQcwebSurveyInfoList.Count > 0)
            { sb.Append(c).Append("TQcwebSurveyInfoList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>RAWDATA_IMPORT_QUE_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("RAWDATA_IMPORT_QUE_INFO_ID")]
        public decimal? RawdataImportQueInfoId {
            get { return _rawdataImportQueInfoId; }
            set {
                __modifiedProperties.AddPropertyName("RawdataImportQueInfoId");
                _rawdataImportQueInfoId = value;
            }
        }

        /// <summary>QCWEB_JOB_NO: {UQ, NotNull, VARCHAR2(10)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEB_JOB_NO")]
        public String QcwebJobNo {
            get { return _qcwebJobNo; }
            set {
                __modifiedProperties.AddPropertyName("QcwebJobNo");
                _qcwebJobNo = value;
            }
        }

        /// <summary>MAIN_SURVEY_ID: {NotNull, NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("MAIN_SURVEY_ID")]
        public decimal? MainSurveyId {
            get { return _mainSurveyId; }
            set {
                __modifiedProperties.AddPropertyName("MainSurveyId");
                _mainSurveyId = value;
            }
        }

        /// <summary>SURVEY_DATA_TYPE: {NotNull, VARCHAR2(1), default=[0], classification=SurveyDataType}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_DATA_TYPE")]
        public String SurveyDataType {
            get { return _surveyDataType; }
            set {
                __modifiedProperties.AddPropertyName("SurveyDataType");
                _surveyDataType = value;
            }
        }

        /// <summary>FILEPATH: {NotNull, VARCHAR2(260)}</summary>
        [Seasar.Dao.Attrs.Column("FILEPATH")]
        public String Filepath {
            get { return _filepath; }
            set {
                __modifiedProperties.AddPropertyName("Filepath");
                _filepath = value;
            }
        }

        /// <summary>FILE_NAME: {NotNull, NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("FILE_NAME")]
        public String FileName {
            get { return _fileName; }
            set {
                __modifiedProperties.AddPropertyName("FileName");
                _fileName = value;
            }
        }

        /// <summary>IMPORT_STATUS: {NotNull, NUMBER(1), default=[0], classification=ImportStatus}</summary>
        [Seasar.Dao.Attrs.Column("IMPORT_STATUS")]
        public int? ImportStatus {
            get { return _importStatus; }
            set {
                __modifiedProperties.AddPropertyName("ImportStatus");
                _importStatus = value;
            }
        }

        /// <summary>MESSAGE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("MESSAGE")]
        public String Message {
            get { return _message; }
            set {
                __modifiedProperties.AddPropertyName("Message");
                _message = value;
            }
        }

        /// <summary>QCWEBID: {IX, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
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

        /// <summary>REQUEST_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("REQUEST_DATETIME")]
        public DateTime? RequestDatetime {
            get { return _requestDatetime; }
            set {
                __modifiedProperties.AddPropertyName("RequestDatetime");
                _requestDatetime = value;
            }
        }

        #endregion
    }
}
