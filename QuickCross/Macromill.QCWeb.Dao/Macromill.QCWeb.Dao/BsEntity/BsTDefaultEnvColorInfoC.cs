

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
    /// The entity of T_DEFAULT_ENV_COLOR_INFO_C as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DEF_ENV_COLOR_INFO_C_ID
    /// 
    /// [column]
    ///     DEF_ENV_COLOR_INFO_C_ID, LANGUAGE, TYPE_CODE, GRADATION_TYPE
    /// 
    /// [sequence]
    ///     T_Default_Env_Color_Info_CSEQ1
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DEFAULT_ENV_BASE
    /// 
    /// [referrer-table]
    ///     T_DEFAULT_ENV_COLOR_DTL_C
    /// 
    /// [foreign-property]
    ///     tDefaultEnvBase
    /// 
    /// [referrer-property]
    ///     tDefaultEnvColorDtlCList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DEFAULT_ENV_COLOR_INFO_C")]
    [System.Serializable]
    public partial class TDefaultEnvColorInfoC : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DEF_ENV_COLOR_INFO_C_ID: {PK, NotNull, NUMBER(8)}</summary>
        protected int? _defEnvColorInfoCId;

        /// <summary>LANGUAGE: {IX, NotNull, VARCHAR2(5), FK to T_DEFAULT_ENV_BASE}</summary>
        protected String _language;

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        protected String _typeCode;

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        protected String _gradationType;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO_C"; } }
        public String TablePropertyName { get { return "TDefaultEnvColorInfoC"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDefaultEnvBase _tDefaultEnvBase;

        /// <summary>T_DEFAULT_ENV_BASE as 'TDefaultEnvBase'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("LANGUAGE:LANGUAGE")]
        public TDefaultEnvBase TDefaultEnvBase {
            get { return _tDefaultEnvBase; }
            set { _tDefaultEnvBase = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TDefaultEnvColorDtlC> _tDefaultEnvColorDtlCList;

        /// <summary>T_DEFAULT_ENV_COLOR_DTL_C as 'TDefaultEnvColorDtlCList'.</summary>
        public IList<TDefaultEnvColorDtlC> TDefaultEnvColorDtlCList {
            get { if (_tDefaultEnvColorDtlCList == null) { _tDefaultEnvColorDtlCList = new List<TDefaultEnvColorDtlC>(); } return _tDefaultEnvColorDtlCList; }
            set { _tDefaultEnvColorDtlCList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_defEnvColorInfoCId == null) { return false; }
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
            if (other == null || !(other is TDefaultEnvColorInfoC)) { return false; }
            TDefaultEnvColorInfoC otherEntity = (TDefaultEnvColorInfoC)other;
            if (!xSV(this.DefEnvColorInfoCId, otherEntity.DefEnvColorInfoCId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _defEnvColorInfoCId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDefaultEnvColorInfoC:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDefaultEnvBase != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnvBase, "TDefaultEnvBase")); }
            if (_tDefaultEnvColorDtlCList != null) { foreach (Entity e in _tDefaultEnvColorDtlCList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDefaultEnvColorDtlCList")); } } }
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
            sb.Append(c).Append(this.DefEnvColorInfoCId);
            sb.Append(c).Append(this.Language);
            sb.Append(c).Append(this.TypeCode);
            sb.Append(c).Append(this.GradationType);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDefaultEnvBase != null) { sb.Append(c).Append("TDefaultEnvBase"); }
            if (_tDefaultEnvColorDtlCList != null && _tDefaultEnvColorDtlCList.Count > 0)
            { sb.Append(c).Append("TDefaultEnvColorDtlCList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DEF_ENV_COLOR_INFO_C_ID: {PK, NotNull, NUMBER(8)}</summary>
        [Seasar.Dao.Attrs.Column("DEF_ENV_COLOR_INFO_C_ID")]
        public int? DefEnvColorInfoCId {
            get { return _defEnvColorInfoCId; }
            set {
                __modifiedProperties.AddPropertyName("DefEnvColorInfoCId");
                _defEnvColorInfoCId = value;
            }
        }

        /// <summary>LANGUAGE: {IX, NotNull, VARCHAR2(5), FK to T_DEFAULT_ENV_BASE}</summary>
        [Seasar.Dao.Attrs.Column("LANGUAGE")]
        public String Language {
            get { return _language; }
            set {
                __modifiedProperties.AddPropertyName("Language");
                _language = value;
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
