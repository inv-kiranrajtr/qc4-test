

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
    /// The entity of T_COLOR_INFO_DETAIL_GT as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     COLOR_INFO_DETAIL_GT_ID
    /// 
    /// [column]
    ///     COLOR_INFO_DETAIL_GT_ID, GRAPH_COLOR_NO, COLOR_CODE, PATTERN_CODE, COLOR_SET_INFO_GT_ID
    /// 
    /// [sequence]
    ///     T_Color_Info_Detail_GT_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_COLOR_SET_INFO_GT
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tColorSetInfoGt
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_COLOR_INFO_DETAIL_GT")]
    [System.Serializable]
    public partial class TColorInfoDetailGt : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>COLOR_INFO_DETAIL_GT_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _colorInfoDetailGtId;

        /// <summary>GRAPH_COLOR_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _graphColorNo;

        /// <summary>COLOR_CODE: {NotNull, NUMBER(2)}</summary>
        protected int? _colorCode;

        /// <summary>PATTERN_CODE: {VARCHAR2(2)}</summary>
        protected String _patternCode;

        /// <summary>COLOR_SET_INFO_GT_ID: {IX, NotNull, NUMBER(27), FK to T_COLOR_SET_INFO_GT}</summary>
        protected decimal? _colorSetInfoGtId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_COLOR_INFO_DETAIL_GT"; } }
        public String TablePropertyName { get { return "TColorInfoDetailGt"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TColorSetInfoGt _tColorSetInfoGt;

        /// <summary>T_COLOR_SET_INFO_GT as 'TColorSetInfoGt'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("COLOR_SET_INFO_GT_ID:COLOR_SET_INFO_GT_ID")]
        public TColorSetInfoGt TColorSetInfoGt {
            get { return _tColorSetInfoGt; }
            set { _tColorSetInfoGt = value; }
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
                if (_colorInfoDetailGtId == null) { return false; }
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
            if (other == null || !(other is TColorInfoDetailGt)) { return false; }
            TColorInfoDetailGt otherEntity = (TColorInfoDetailGt)other;
            if (!xSV(this.ColorInfoDetailGtId, otherEntity.ColorInfoDetailGtId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _colorInfoDetailGtId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TColorInfoDetailGt:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tColorSetInfoGt != null)
            { sb.Append(l).Append(xbRDS(_tColorSetInfoGt, "TColorSetInfoGt")); }
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
            sb.Append(c).Append(this.ColorInfoDetailGtId);
            sb.Append(c).Append(this.GraphColorNo);
            sb.Append(c).Append(this.ColorCode);
            sb.Append(c).Append(this.PatternCode);
            sb.Append(c).Append(this.ColorSetInfoGtId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tColorSetInfoGt != null) { sb.Append(c).Append("TColorSetInfoGt"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>COLOR_INFO_DETAIL_GT_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_INFO_DETAIL_GT_ID")]
        public decimal? ColorInfoDetailGtId {
            get { return _colorInfoDetailGtId; }
            set {
                __modifiedProperties.AddPropertyName("ColorInfoDetailGtId");
                _colorInfoDetailGtId = value;
            }
        }

        /// <summary>GRAPH_COLOR_NO: {NotNull, NUMBER(5), default=[0]}</summary>
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

        /// <summary>COLOR_SET_INFO_GT_ID: {IX, NotNull, NUMBER(27), FK to T_COLOR_SET_INFO_GT}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_SET_INFO_GT_ID")]
        public decimal? ColorSetInfoGtId {
            get { return _colorSetInfoGtId; }
            set {
                __modifiedProperties.AddPropertyName("ColorSetInfoGtId");
                _colorSetInfoGtId = value;
            }
        }

        #endregion
    }
}
