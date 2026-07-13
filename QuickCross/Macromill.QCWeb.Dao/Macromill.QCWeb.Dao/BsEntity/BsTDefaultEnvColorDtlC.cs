

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
    /// The entity of T_DEFAULT_ENV_COLOR_DTL_C as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DEF_ENV_COLOR_DTL_C_ID
    /// 
    /// [column]
    ///     DEF_ENV_COLOR_DTL_C_ID, DEF_ENV_COLOR_INFO_C_ID, GRAPH_COLOR_NO, COLOR_CODE, PATTERN_CODE
    /// 
    /// [sequence]
    ///     T_Default_Env_Color_Dtl_C_SEQ1
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DEFAULT_ENV_COLOR_INFO_C
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tDefaultEnvColorInfoC
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DEFAULT_ENV_COLOR_DTL_C")]
    [System.Serializable]
    public partial class TDefaultEnvColorDtlC : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DEF_ENV_COLOR_DTL_C_ID: {PK, NotNull, NUMBER(8)}</summary>
        protected int? _defEnvColorDtlCId;

        /// <summary>DEF_ENV_COLOR_INFO_C_ID: {IX, NotNull, NUMBER(8), FK to T_DEFAULT_ENV_COLOR_INFO_C}</summary>
        protected int? _defEnvColorInfoCId;

        /// <summary>GRAPH_COLOR_NO: {NotNull, NUMBER(2), default=[0]}</summary>
        protected int? _graphColorNo;

        /// <summary>COLOR_CODE: {NotNull, NUMBER(2)}</summary>
        protected int? _colorCode;

        /// <summary>PATTERN_CODE: {VARCHAR2(2)}</summary>
        protected String _patternCode;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL_C"; } }
        public String TablePropertyName { get { return "TDefaultEnvColorDtlC"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDefaultEnvColorInfoC _tDefaultEnvColorInfoC;

        /// <summary>T_DEFAULT_ENV_COLOR_INFO_C as 'TDefaultEnvColorInfoC'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DEF_ENV_COLOR_INFO_C_ID:DEF_ENV_COLOR_INFO_C_ID")]
        public TDefaultEnvColorInfoC TDefaultEnvColorInfoC {
            get { return _tDefaultEnvColorInfoC; }
            set { _tDefaultEnvColorInfoC = value; }
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
                if (_defEnvColorDtlCId == null) { return false; }
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
            if (other == null || !(other is TDefaultEnvColorDtlC)) { return false; }
            TDefaultEnvColorDtlC otherEntity = (TDefaultEnvColorDtlC)other;
            if (!xSV(this.DefEnvColorDtlCId, otherEntity.DefEnvColorDtlCId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _defEnvColorDtlCId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDefaultEnvColorDtlC:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDefaultEnvColorInfoC != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnvColorInfoC, "TDefaultEnvColorInfoC")); }
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
            sb.Append(c).Append(this.DefEnvColorDtlCId);
            sb.Append(c).Append(this.DefEnvColorInfoCId);
            sb.Append(c).Append(this.GraphColorNo);
            sb.Append(c).Append(this.ColorCode);
            sb.Append(c).Append(this.PatternCode);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDefaultEnvColorInfoC != null) { sb.Append(c).Append("TDefaultEnvColorInfoC"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DEF_ENV_COLOR_DTL_C_ID: {PK, NotNull, NUMBER(8)}</summary>
        [Seasar.Dao.Attrs.Column("DEF_ENV_COLOR_DTL_C_ID")]
        public int? DefEnvColorDtlCId {
            get { return _defEnvColorDtlCId; }
            set {
                __modifiedProperties.AddPropertyName("DefEnvColorDtlCId");
                _defEnvColorDtlCId = value;
            }
        }

        /// <summary>DEF_ENV_COLOR_INFO_C_ID: {IX, NotNull, NUMBER(8), FK to T_DEFAULT_ENV_COLOR_INFO_C}</summary>
        [Seasar.Dao.Attrs.Column("DEF_ENV_COLOR_INFO_C_ID")]
        public int? DefEnvColorInfoCId {
            get { return _defEnvColorInfoCId; }
            set {
                __modifiedProperties.AddPropertyName("DefEnvColorInfoCId");
                _defEnvColorInfoCId = value;
            }
        }

        /// <summary>GRAPH_COLOR_NO: {NotNull, NUMBER(2), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_COLOR_NO")]
        public int? GraphColorNo {
            get { return _graphColorNo; }
            set {
                __modifiedProperties.AddPropertyName("GraphColorNo");
                _graphColorNo = value;
            }
        }

        /// <summary>COLOR_CODE: {NotNull, NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_CODE")]
        public int? ColorCode {
            get { return _colorCode; }
            set {
                __modifiedProperties.AddPropertyName("ColorCode");
                _colorCode = value;
            }
        }

        /// <summary>PATTERN_CODE: {VARCHAR2(2)}</summary>
        [Seasar.Dao.Attrs.Column("PATTERN_CODE")]
        public String PatternCode {
            get { return _patternCode; }
            set {
                __modifiedProperties.AddPropertyName("PatternCode");
                _patternCode = value;
            }
        }

        #endregion
    }
}
