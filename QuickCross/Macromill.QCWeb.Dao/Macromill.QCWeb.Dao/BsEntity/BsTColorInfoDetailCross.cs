

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
    /// The entity of T_COLOR_INFO_DETAIL_CROSS as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     COLOR_INFO_DETAIL_CROSS_ID
    /// 
    /// [column]
    ///     COLOR_INFO_DETAIL_CROSS_ID, GRAPH_COLOR_NO, COLOR_CODE, PATTERN_CODE, COLOR_SET_INFO_CROSS_ID
    /// 
    /// [sequence]
    ///     T_Color_Info_Detail_CrossSEQ1
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_COLOR_SET_INFO_CROSS
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tColorSetInfoCross
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_COLOR_INFO_DETAIL_CROSS")]
    [System.Serializable]
    public partial class TColorInfoDetailCross : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>COLOR_INFO_DETAIL_CROSS_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _colorInfoDetailCrossId;

        /// <summary>GRAPH_COLOR_NO: {NotNull, NUMBER(5)}</summary>
        protected int? _graphColorNo;

        /// <summary>COLOR_CODE: {NotNull, NUMBER(2)}</summary>
        protected int? _colorCode;

        /// <summary>PATTERN_CODE: {VARCHAR2(2)}</summary>
        protected String _patternCode;

        /// <summary>COLOR_SET_INFO_CROSS_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_COLOR_SET_INFO_CROSS}</summary>
        protected decimal? _colorSetInfoCrossId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_COLOR_INFO_DETAIL_CROSS"; } }
        public String TablePropertyName { get { return "TColorInfoDetailCross"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TColorSetInfoCross _tColorSetInfoCross;

        /// <summary>T_COLOR_SET_INFO_CROSS as 'TColorSetInfoCross'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("COLOR_SET_INFO_CROSS_ID:COLOR_SET_INFO_CROSS_ID")]
        public TColorSetInfoCross TColorSetInfoCross {
            get { return _tColorSetInfoCross; }
            set { _tColorSetInfoCross = value; }
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
                if (_colorInfoDetailCrossId == null) { return false; }
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
            if (other == null || !(other is TColorInfoDetailCross)) { return false; }
            TColorInfoDetailCross otherEntity = (TColorInfoDetailCross)other;
            if (!xSV(this.ColorInfoDetailCrossId, otherEntity.ColorInfoDetailCrossId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _colorInfoDetailCrossId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TColorInfoDetailCross:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tColorSetInfoCross != null)
            { sb.Append(l).Append(xbRDS(_tColorSetInfoCross, "TColorSetInfoCross")); }
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
            sb.Append(c).Append(this.ColorInfoDetailCrossId);
            sb.Append(c).Append(this.GraphColorNo);
            sb.Append(c).Append(this.ColorCode);
            sb.Append(c).Append(this.PatternCode);
            sb.Append(c).Append(this.ColorSetInfoCrossId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tColorSetInfoCross != null) { sb.Append(c).Append("TColorSetInfoCross"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>COLOR_INFO_DETAIL_CROSS_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_INFO_DETAIL_CROSS_ID")]
        public decimal? ColorInfoDetailCrossId {
            get { return _colorInfoDetailCrossId; }
            set {
                __modifiedProperties.AddPropertyName("ColorInfoDetailCrossId");
                _colorInfoDetailCrossId = value;
            }
        }

        /// <summary>GRAPH_COLOR_NO: {NotNull, NUMBER(5)}</summary>
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

        /// <summary>COLOR_SET_INFO_CROSS_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_COLOR_SET_INFO_CROSS}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_SET_INFO_CROSS_ID")]
        public decimal? ColorSetInfoCrossId {
            get { return _colorSetInfoCrossId; }
            set {
                __modifiedProperties.AddPropertyName("ColorSetInfoCrossId");
                _colorSetInfoCrossId = value;
            }
        }

        #endregion
    }
}
