

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
    /// The entity of T_DEFAULT_ENV_COLOR_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DEF_ENV_COLOR_INFO_ID
    /// 
    /// [column]
    ///     DEF_ENV_COLOR_INFO_ID, QCWEBID, TYPE_CODE, GRADATION_TYPE
    /// 
    /// [sequence]
    ///     T_Default_Env_Color_Info_SEQ_1
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DEFAULT_ENV, T_Default_Env_Color_Dtl
    /// 
    /// [referrer-table]
    ///     T_DEFAULT_ENV_COLOR_DTL
    /// 
    /// [foreign-property]
    ///     tDefaultEnv, tDefaultEnvColorDtl
    /// 
    /// [referrer-property]
    ///     tDefaultEnvColorDtlList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DEFAULT_ENV_COLOR_INFO")]
    [System.Serializable]
    public partial class TDefaultEnvColorInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DEF_ENV_COLOR_INFO_ID: {PK, NotNull, NUMBER(27), FK to T_Default_Env_Color_Dtl}</summary>
        protected decimal? _defEnvColorInfoId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_DEFAULT_ENV}</summary>
        protected decimal? _qcwebid;

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        protected String _typeCode;

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        protected String _gradationType;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO"; } }
        public String TablePropertyName { get { return "TDefaultEnvColorInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDefaultEnv _tDefaultEnv;

        /// <summary>T_DEFAULT_ENV as 'TDefaultEnv'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TDefaultEnv TDefaultEnv {
            get { return _tDefaultEnv; }
            set { _tDefaultEnv = value; }
        }

        protected TDefaultEnvColorDtl _tDefaultEnvColorDtl;

        /// <summary>T_DEFAULT_ENV_COLOR_DTL as 'TDefaultEnvColorDtl'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("DEF_ENV_COLOR_INFO_ID:DEF_ENV_COLOR_INFO_ID")]
        public TDefaultEnvColorDtl TDefaultEnvColorDtl {
            get { return _tDefaultEnvColorDtl; }
            set { _tDefaultEnvColorDtl = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TDefaultEnvColorDtl> _tDefaultEnvColorDtlList;

        /// <summary>T_DEFAULT_ENV_COLOR_DTL as 'TDefaultEnvColorDtlList'.</summary>
        public IList<TDefaultEnvColorDtl> TDefaultEnvColorDtlList {
            get { if (_tDefaultEnvColorDtlList == null) { _tDefaultEnvColorDtlList = new List<TDefaultEnvColorDtl>(); } return _tDefaultEnvColorDtlList; }
            set { _tDefaultEnvColorDtlList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_defEnvColorInfoId == null) { return false; }
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
            if (other == null || !(other is TDefaultEnvColorInfo)) { return false; }
            TDefaultEnvColorInfo otherEntity = (TDefaultEnvColorInfo)other;
            if (!xSV(this.DefEnvColorInfoId, otherEntity.DefEnvColorInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _defEnvColorInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDefaultEnvColorInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDefaultEnv != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnv, "TDefaultEnv")); }
            if (_tDefaultEnvColorDtl != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnvColorDtl, "TDefaultEnvColorDtl")); }
            if (_tDefaultEnvColorDtlList != null) { foreach (Entity e in _tDefaultEnvColorDtlList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDefaultEnvColorDtlList")); } } }
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
            sb.Append(c).Append(this.DefEnvColorInfoId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.TypeCode);
            sb.Append(c).Append(this.GradationType);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDefaultEnv != null) { sb.Append(c).Append("TDefaultEnv"); }
            if (_tDefaultEnvColorDtl != null) { sb.Append(c).Append("TDefaultEnvColorDtl"); }
            if (_tDefaultEnvColorDtlList != null && _tDefaultEnvColorDtlList.Count > 0)
            { sb.Append(c).Append("TDefaultEnvColorDtlList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DEF_ENV_COLOR_INFO_ID: {PK, NotNull, NUMBER(27), FK to T_Default_Env_Color_Dtl}</summary>
        [Seasar.Dao.Attrs.Column("DEF_ENV_COLOR_INFO_ID")]
        public decimal? DefEnvColorInfoId {
            get { return _defEnvColorInfoId; }
            set {
                __modifiedProperties.AddPropertyName("DefEnvColorInfoId");
                _defEnvColorInfoId = value;
            }
        }

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_DEFAULT_ENV}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("TYPE_CODE")]
        public String TypeCode {
            get { return _typeCode; }
            set {
                __modifiedProperties.AddPropertyName("TypeCode");
                _typeCode = value;
            }
        }

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRADATION_TYPE")]
        public String GradationType {
            get { return _gradationType; }
            set {
                __modifiedProperties.AddPropertyName("GradationType");
                _gradationType = value;
            }
        }

        #endregion
    }
}
