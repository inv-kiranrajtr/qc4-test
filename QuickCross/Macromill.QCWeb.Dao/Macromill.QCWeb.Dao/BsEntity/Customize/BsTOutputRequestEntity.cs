

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
    /// The entity of TOutputRequestEntity. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     OUTPUT_REPORTSET_INFO_ID, DOWNLOAD_PATH, OUTPUT_REQUEST_ID
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
    [Seasar.Dao.Attrs.Table("TOutputRequestEntity")]
    [System.Serializable]
    public partial class TOutputRequestEntity : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_REPORTSET_INFO_ID: {NUMBER(27)}</summary>
        protected decimal? _outputReportsetInfoId;

        /// <summary>DOWNLOAD_PATH: {VARCHAR2(260)}</summary>
        protected String _downloadPath;

        /// <summary>OUTPUT_REQUEST_ID: {NUMBER(27)}</summary>
        protected decimal? _outputRequestId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "TOutputRequestEntity"; } }
        public String TablePropertyName { get { return "TOutputRequestEntity"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return TOutputRequestEntityDbm.GetInstance(); } }

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
            if (other == null || !(other is TOutputRequestEntity)) { return false; }
            TOutputRequestEntity otherEntity = (TOutputRequestEntity)other;
            if (!xSV(this.OutputReportsetInfoId, otherEntity.OutputReportsetInfoId)) { return false; }
            if (!xSV(this.DownloadPath, otherEntity.DownloadPath)) { return false; }
            if (!xSV(this.OutputRequestId, otherEntity.OutputRequestId)) { return false; }
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
            result = xCH(result, _downloadPath);
            result = xCH(result, _outputRequestId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputRequestEntity:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.OutputReportsetInfoId);
            sb.Append(c).Append(this.DownloadPath);
            sb.Append(c).Append(this.OutputRequestId);
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
        /// <summary>OUTPUT_REPORTSET_INFO_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REPORTSET_INFO_ID")]
        public decimal? OutputReportsetInfoId {
            get { return _outputReportsetInfoId; }
            set {
                __modifiedProperties.AddPropertyName("OutputReportsetInfoId");
                _outputReportsetInfoId = value;
            }
        }

        /// <summary>DOWNLOAD_PATH: {VARCHAR2(260)}</summary>
        [Seasar.Dao.Attrs.Column("DOWNLOAD_PATH")]
        public String DownloadPath {
            get { return _downloadPath; }
            set {
                __modifiedProperties.AddPropertyName("DownloadPath");
                _downloadPath = value;
            }
        }

        /// <summary>OUTPUT_REQUEST_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REQUEST_ID")]
        public decimal? OutputRequestId {
            get { return _outputRequestId; }
            set {
                __modifiedProperties.AddPropertyName("OutputRequestId");
                _outputRequestId = value;
            }
        }

        #endregion
    }
}
