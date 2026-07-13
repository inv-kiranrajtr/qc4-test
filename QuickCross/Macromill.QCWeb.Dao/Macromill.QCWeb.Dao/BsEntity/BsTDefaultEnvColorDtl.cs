

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
    /// The entity of T_DEFAULT_ENV_COLOR_DTL as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DEF_ENV_COLOR_DTL_ID
    /// 
    /// [column]
    ///     DEF_ENV_COLOR_DTL_ID, DEF_ENV_COLOR_INFO_ID, GRAPH_COLOR_NO, COLOR_CODE, PATTERN_CODE
    /// 
    /// [sequence]
    ///     T_Default_Env_Color_Dtl_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DEFAULT_ENV_COLOR_INFO
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tDefaultEnvColorInfo
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DEFAULT_ENV_COLOR_DTL")]
    [System.Serializable]
    public partial class TDefaultEnvColorDtl : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DEF_ENV_COLOR_DTL_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _defEnvColorDtlId;

        /// <summary>DEF_ENV_COLOR_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_DEFAULT_ENV_COLOR_INFO}</summary>
        protected decimal? _defEnvColorInfoId;

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
        public String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL"; } }
        public String TablePropertyName { get { return "TDefaultEnvColorDtl"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDefaultEnvColorInfo _tDefaultEnvColorInfo;

        /// <summary>T_DEFAULT_ENV_COLOR_INFO as 'TDefaultEnvColorInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DEF_ENV_COLOR_INFO_ID:DEF_ENV_COLOR_INFO_ID")]
        public TDefaultEnvColorInfo TDefaultEnvColorInfo {
            get { return _tDefaultEnvColorInfo; }
            set { _tDefaultEnvColorInfo = value; }
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
                if (_defEnvColorDtlId == null) { return false; }
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
            if (other == null || !(other is TDefaultEnvColorDtl)) { return false; }
            TDefaultEnvColorDtl otherEntity = (TDefaultEnvColorDtl)other;
            if (!xSV(this.DefEnvColorDtlId, otherEntity.DefEnvColorDtlId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _defEnvColorDtlId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDefaultEnvColorDtl:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDefaultEnvColorInfo != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnvColorInfo, "TDefaultEnvColorInfo")); }
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
            sb.Append(c).Append(this.DefEnvColorDtlId);
            sb.Append(c).Append(this.DefEnvColorInfoId);
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
            if (_tDefaultEnvColorInfo != null) { sb.Append(c).Append("TDefaultEnvColorInfo"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DEF_ENV_COLOR_DTL_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DEF_ENV_COLOR_DTL_ID")]
        public decimal? DefEnvColorDtlId {
            get { return _defEnvColorDtlId; }
            set {
                __modifiedProperties.AddPropertyName("DefEnvColorDtlId");
                _defEnvColorDtlId = value;
            }
        }

        /// <summary>DEF_ENV_COLOR_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_DEFAULT_ENV_COLOR_INFO}</summary>
        [Seasar.Dao.Attrs.Column("DEF_ENV_COLOR_INFO_ID")]
        public decimal? DefEnvColorInfoId {
            get { return _defEnvColorInfoId; }
            set {
                __modifiedProperties.AddPropertyName("DefEnvColorInfoId");
                _defEnvColorInfoId = value;
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
