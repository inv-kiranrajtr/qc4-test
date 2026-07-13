

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
    /// The entity of T_OUTPUT_SUB_FA as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_SUB_FA_ID
    /// 
    /// [column]
    ///     OUTPUT_SUB_FA_ID, OUTPUT_COMMON_ID, PAGE_SETTING_PAPER_SIZE, PAGE_SETTING_PAPER_ORIENTATION, FILTERING_EXPRESSION
    /// 
    /// [sequence]
    ///     T_Output_Sub_FA_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_OUTPUT_COMMON
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tOutputCommon
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_SUB_FA")]
    [System.Serializable]
    public partial class TOutputSubFa : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_SUB_FA_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputSubFaId;

        /// <summary>OUTPUT_COMMON_ID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_COMMON}</summary>
        protected decimal? _outputCommonId;

        /// <summary>PAGE_SETTING_PAPER_SIZE: {NUMBER(2)}</summary>
        protected int? _pageSettingPaperSize;

        /// <summary>PAGE_SETTING_PAPER_ORIENTATION: {NUMBER(1)}</summary>
        protected int? _pageSettingPaperOrientation;

        /// <summary>FILTERING_EXPRESSION: {NCLOB(4000)}</summary>
        protected String _filteringExpression;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_SUB_FA"; } }
        public String TablePropertyName { get { return "TOutputSubFa"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TOutputCommon _tOutputCommon;

        /// <summary>T_OUTPUT_COMMON as 'TOutputCommon'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("OUTPUT_COMMON_ID:OUTPUT_COMMON_ID")]
        public TOutputCommon TOutputCommon {
            get { return _tOutputCommon; }
            set { _tOutputCommon = value; }
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
                if (_outputSubFaId == null) { return false; }
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
            if (other == null || !(other is TOutputSubFa)) { return false; }
            TOutputSubFa otherEntity = (TOutputSubFa)other;
            if (!xSV(this.OutputSubFaId, otherEntity.OutputSubFaId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputSubFaId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputSubFa:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tOutputCommon != null)
            { sb.Append(l).Append(xbRDS(_tOutputCommon, "TOutputCommon")); }
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
            sb.Append(c).Append(this.OutputSubFaId);
            sb.Append(c).Append(this.OutputCommonId);
            sb.Append(c).Append(this.PageSettingPaperSize);
            sb.Append(c).Append(this.PageSettingPaperOrientation);
            sb.Append(c).Append(this.FilteringExpression);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tOutputCommon != null) { sb.Append(c).Append("TOutputCommon"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_SUB_FA_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_SUB_FA_ID")]
        public decimal? OutputSubFaId {
            get { return _outputSubFaId; }
            set {
                __modifiedProperties.AddPropertyName("OutputSubFaId");
                _outputSubFaId = value;
            }
        }

        /// <summary>OUTPUT_COMMON_ID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_COMMON}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_COMMON_ID")]
        public decimal? OutputCommonId {
            get { return _outputCommonId; }
            set {
                __modifiedProperties.AddPropertyName("OutputCommonId");
                _outputCommonId = value;
            }
        }

        /// <summary>PAGE_SETTING_PAPER_SIZE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_PAPER_SIZE")]
        public int? PageSettingPaperSize {
            get { return _pageSettingPaperSize; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingPaperSize");
                _pageSettingPaperSize = value;
            }
        }

        /// <summary>PAGE_SETTING_PAPER_ORIENTATION: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_PAPER_ORIENTATION")]
        public int? PageSettingPaperOrientation {
            get { return _pageSettingPaperOrientation; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingPaperOrientation");
                _pageSettingPaperOrientation = value;
            }
        }

        /// <summary>FILTERING_EXPRESSION: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("FILTERING_EXPRESSION")]
        public String FilteringExpression {
            get { return _filteringExpression; }
            set {
                __modifiedProperties.AddPropertyName("FilteringExpression");
                _filteringExpression = value;
            }
        }

        #endregion
    }
}
