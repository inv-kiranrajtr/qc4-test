

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
    /// The entity of T_RAWDATA_DELETE_QUE as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     RAWDATA_DELETE_QUE_ID
    /// 
    /// [column]
    ///     RAWDATA_DELETE_QUE_ID, ADD_DATA_NO, QCWEB_JOB_NO, MAIN_SURVEY_ID, DELETE_ORDER_DATE, DELETE_STATUS
    /// 
    /// [sequence]
    ///     T_RawData_Delete_Que_SEQ_01
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
    [Seasar.Dao.Attrs.Table("T_RAWDATA_DELETE_QUE")]
    [System.Serializable]
    public partial class TRawdataDeleteQue : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>RAWDATA_DELETE_QUE_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _rawdataDeleteQueId;

        /// <summary>ADD_DATA_NO: {NotNull, NUMBER(10)}</summary>
        protected long? _addDataNo;

        /// <summary>QCWEB_JOB_NO: {UQ, NotNull, VARCHAR2(10)}</summary>
        protected String _qcwebJobNo;

        /// <summary>MAIN_SURVEY_ID: {NotNull, NUMBER(22)}</summary>
        protected decimal? _mainSurveyId;

        /// <summary>DELETE_ORDER_DATE: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _deleteOrderDate;

        /// <summary>DELETE_STATUS: {NotNull, NUMBER(1), default=[0], classification=DeleteStatus}</summary>
        protected int? _deleteStatus;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_RAWDATA_DELETE_QUE"; } }
        public String TablePropertyName { get { return "TRawdataDeleteQue"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.DeleteStatus DeleteStatusAsDeleteStatus { get {
            return CDef.DeleteStatus.CodeOf(_deleteStatus);
        } set {
            DeleteStatus = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of deleteStatus as NONE_DELETE.
        /// <![CDATA[
        /// 未削除: 未削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteStatus_NONE_DELETE() {
            DeleteStatusAsDeleteStatus = CDef.DeleteStatus.NONE_DELETE;
        }

        /// <summary>
        /// Set the value of deleteStatus as DELETE_EXEC.
        /// <![CDATA[
        /// 削除中: 削除中を示す
        /// ]]>
        /// </summary>
        public void SetDeleteStatus_DELETE_EXEC() {
            DeleteStatusAsDeleteStatus = CDef.DeleteStatus.DELETE_EXEC;
        }

        /// <summary>
        /// Set the value of deleteStatus as DELETE_END.
        /// <![CDATA[
        /// 削除完: 削除完を示す
        /// ]]>
        /// </summary>
        public void SetDeleteStatus_DELETE_END() {
            DeleteStatusAsDeleteStatus = CDef.DeleteStatus.DELETE_END;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of deleteStatus 'NONE_DELETE'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 未削除: 未削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteStatusNONE_DELETE {
            get {
                CDef.DeleteStatus cls = DeleteStatusAsDeleteStatus;
                return cls != null ? cls.Equals(CDef.DeleteStatus.NONE_DELETE) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteStatus 'DELETE_EXEC'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 削除中: 削除中を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteStatusDELETE_EXEC {
            get {
                CDef.DeleteStatus cls = DeleteStatusAsDeleteStatus;
                return cls != null ? cls.Equals(CDef.DeleteStatus.DELETE_EXEC) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteStatus 'DELETE_END'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 削除完: 削除完を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteStatusDELETE_END {
            get {
                CDef.DeleteStatus cls = DeleteStatusAsDeleteStatus;
                return cls != null ? cls.Equals(CDef.DeleteStatus.DELETE_END) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String DeleteStatusName {
            get {
                CDef.DeleteStatus cls = DeleteStatusAsDeleteStatus;
                return cls != null ? cls.Name : null;
            }
        }
        public String DeleteStatusAlias {
            get {
                CDef.DeleteStatus cls = DeleteStatusAsDeleteStatus;
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
                if (_rawdataDeleteQueId == null) { return false; }
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
            if (other == null || !(other is TRawdataDeleteQue)) { return false; }
            TRawdataDeleteQue otherEntity = (TRawdataDeleteQue)other;
            if (!xSV(this.RawdataDeleteQueId, otherEntity.RawdataDeleteQueId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _rawdataDeleteQueId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TRawdataDeleteQue:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.RawdataDeleteQueId);
            sb.Append(c).Append(this.AddDataNo);
            sb.Append(c).Append(this.QcwebJobNo);
            sb.Append(c).Append(this.MainSurveyId);
            sb.Append(c).Append(this.DeleteOrderDate);
            sb.Append(c).Append(this.DeleteStatus);
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
        /// <summary>RAWDATA_DELETE_QUE_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("RAWDATA_DELETE_QUE_ID")]
        public decimal? RawdataDeleteQueId {
            get { return _rawdataDeleteQueId; }
            set {
                __modifiedProperties.AddPropertyName("RawdataDeleteQueId");
                _rawdataDeleteQueId = value;
            }
        }

        /// <summary>ADD_DATA_NO: {NotNull, NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("ADD_DATA_NO")]
        public long? AddDataNo {
            get { return _addDataNo; }
            set {
                __modifiedProperties.AddPropertyName("AddDataNo");
                _addDataNo = value;
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

        /// <summary>DELETE_ORDER_DATE: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_ORDER_DATE")]
        public DateTime? DeleteOrderDate {
            get { return _deleteOrderDate; }
            set {
                __modifiedProperties.AddPropertyName("DeleteOrderDate");
                _deleteOrderDate = value;
            }
        }

        /// <summary>DELETE_STATUS: {NotNull, NUMBER(1), default=[0], classification=DeleteStatus}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_STATUS")]
        public int? DeleteStatus {
            get { return _deleteStatus; }
            set {
                __modifiedProperties.AddPropertyName("DeleteStatus");
                _deleteStatus = value;
            }
        }

        #endregion
    }
}
