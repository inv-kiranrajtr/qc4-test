

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
    /// The entity of T_OUTPUT_SUB_CROSS as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_SUB_CROSS_ID
    /// 
    /// [column]
    ///     OUTPUT_SUB_CROSS_ID, OUTPUT_COMMON_ID, OUTPUT_TYPE, OUTPUT_TABLE_TYPE, OUTPUT_TABLE_ORIENTATION, PAGE_SETTING_TABLE_TYPE, PAGE_SETTING_PAPER_SIZE, PAGE_SETTING_PAPER_ORIENTATION, MARKING_MIN_PARAMETER, MARKING_CODE, MARKING_LEVEL, LEVEL2PLUSCOLOR, LEVEL1PLUSCOLOR, LEVEL1MINUSCOLOR, LEVEL2MINUSCOLOR, LEVEL2PERCENT, LEVEL1PERCENT, FILTERING_EXPRESSION
    /// 
    /// [sequence]
    ///     T_Output_Sub_Cross_SEQ_01
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
    [Seasar.Dao.Attrs.Table("T_OUTPUT_SUB_CROSS")]
    [System.Serializable]
    public partial class TOutputSubCross : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_SUB_CROSS_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputSubCrossId;

        /// <summary>OUTPUT_COMMON_ID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_COMMON}</summary>
        protected decimal? _outputCommonId;

        /// <summary>OUTPUT_TYPE: {NotNull, NUMBER(1), default=[0]}</summary>
        protected int? _outputType;

        /// <summary>OUTPUT_TABLE_TYPE: {NotNull, NUMBER(1)}</summary>
        protected int? _outputTableType;

        /// <summary>OUTPUT_TABLE_ORIENTATION: {NotNull, NUMBER(1), default=[0]}</summary>
        protected int? _outputTableOrientation;

        /// <summary>PAGE_SETTING_TABLE_TYPE: {NUMBER(1)}</summary>
        protected int? _pageSettingTableType;

        /// <summary>PAGE_SETTING_PAPER_SIZE: {NUMBER(2)}</summary>
        protected int? _pageSettingPaperSize;

        /// <summary>PAGE_SETTING_PAPER_ORIENTATION: {NUMBER(1)}</summary>
        protected int? _pageSettingPaperOrientation;

        /// <summary>MARKING_MIN_PARAMETER: {NUMBER(10)}</summary>
        protected long? _markingMinParameter;

        /// <summary>MARKING_CODE: {NUMBER(3), default=[0]}</summary>
        protected int? _markingCode;

        /// <summary>MARKING_LEVEL: {NUMBER(1)}</summary>
        protected int? _markingLevel;

        /// <summary>LEVEL2PLUSCOLOR: {NUMBER(2), default=[6]}</summary>
        protected int? _level2pluscolor;

        /// <summary>LEVEL1PLUSCOLOR: {NUMBER(2), default=[36]}</summary>
        protected int? _level1pluscolor;

        /// <summary>LEVEL1MINUSCOLOR: {NUMBER(2), default=[34]}</summary>
        protected int? _level1minuscolor;

        /// <summary>LEVEL2MINUSCOLOR: {NUMBER(2), default=[37]}</summary>
        protected int? _level2minuscolor;

        /// <summary>LEVEL2PERCENT: {NUMBER(2), default=[10]}</summary>
        protected int? _level2percent;

        /// <summary>LEVEL1PERCENT: {NUMBER(2), default=[5]}</summary>
        protected int? _level1percent;

        /// <summary>FILTERING_EXPRESSION: {NCLOB(4000)}</summary>
        protected String _filteringExpression;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_SUB_CROSS"; } }
        public String TablePropertyName { get { return "TOutputSubCross"; } }

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
                if (_outputSubCrossId == null) { return false; }
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
            if (other == null || !(other is TOutputSubCross)) { return false; }
            TOutputSubCross otherEntity = (TOutputSubCross)other;
            if (!xSV(this.OutputSubCrossId, otherEntity.OutputSubCrossId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputSubCrossId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputSubCross:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.OutputSubCrossId);
            sb.Append(c).Append(this.OutputCommonId);
            sb.Append(c).Append(this.OutputType);
            sb.Append(c).Append(this.OutputTableType);
            sb.Append(c).Append(this.OutputTableOrientation);
            sb.Append(c).Append(this.PageSettingTableType);
            sb.Append(c).Append(this.PageSettingPaperSize);
            sb.Append(c).Append(this.PageSettingPaperOrientation);
            sb.Append(c).Append(this.MarkingMinParameter);
            sb.Append(c).Append(this.MarkingCode);
            sb.Append(c).Append(this.MarkingLevel);
            sb.Append(c).Append(this.Level2pluscolor);
            sb.Append(c).Append(this.Level1pluscolor);
            sb.Append(c).Append(this.Level1minuscolor);
            sb.Append(c).Append(this.Level2minuscolor);
            sb.Append(c).Append(this.Level2percent);
            sb.Append(c).Append(this.Level1percent);
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
        /// <summary>OUTPUT_SUB_CROSS_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_SUB_CROSS_ID")]
        public decimal? OutputSubCrossId {
            get { return _outputSubCrossId; }
            set {
                __modifiedProperties.AddPropertyName("OutputSubCrossId");
                _outputSubCrossId = value;
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

        /// <summary>OUTPUT_TYPE: {NotNull, NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TYPE")]
        public int? OutputType {
            get { return _outputType; }
            set {
                __modifiedProperties.AddPropertyName("OutputType");
                _outputType = value;
            }
        }

        /// <summary>OUTPUT_TABLE_TYPE: {NotNull, NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TABLE_TYPE")]
        public int? OutputTableType {
            get { return _outputTableType; }
            set {
                __modifiedProperties.AddPropertyName("OutputTableType");
                _outputTableType = value;
            }
        }

        /// <summary>OUTPUT_TABLE_ORIENTATION: {NotNull, NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TABLE_ORIENTATION")]
        public int? OutputTableOrientation {
            get { return _outputTableOrientation; }
            set {
                __modifiedProperties.AddPropertyName("OutputTableOrientation");
                _outputTableOrientation = value;
            }
        }

        /// <summary>PAGE_SETTING_TABLE_TYPE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_TABLE_TYPE")]
        public int? PageSettingTableType {
            get { return _pageSettingTableType; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingTableType");
                _pageSettingTableType = value;
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

        /// <summary>MARKING_MIN_PARAMETER: {NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("MARKING_MIN_PARAMETER")]
        public long? MarkingMinParameter {
            get { return _markingMinParameter; }
            set {
                __modifiedProperties.AddPropertyName("MarkingMinParameter");
                _markingMinParameter = value;
            }
        }

        /// <summary>MARKING_CODE: {NUMBER(3), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("MARKING_CODE")]
        public int? MarkingCode {
            get { return _markingCode; }
            set {
                __modifiedProperties.AddPropertyName("MarkingCode");
                _markingCode = value;
            }
        }

        /// <summary>MARKING_LEVEL: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("MARKING_LEVEL")]
        public int? MarkingLevel {
            get { return _markingLevel; }
            set {
                __modifiedProperties.AddPropertyName("MarkingLevel");
                _markingLevel = value;
            }
        }

        /// <summary>LEVEL2PLUSCOLOR: {NUMBER(2), default=[6]}</summary>
        [Seasar.Dao.Attrs.Column("LEVEL2PLUSCOLOR")]
        public int? Level2pluscolor {
            get { return _level2pluscolor; }
            set {
                __modifiedProperties.AddPropertyName("Level2pluscolor");
                _level2pluscolor = value;
            }
        }

        /// <summary>LEVEL1PLUSCOLOR: {NUMBER(2), default=[36]}</summary>
        [Seasar.Dao.Attrs.Column("LEVEL1PLUSCOLOR")]
        public int? Level1pluscolor {
            get { return _level1pluscolor; }
            set {
                __modifiedProperties.AddPropertyName("Level1pluscolor");
                _level1pluscolor = value;
            }
        }

        /// <summary>LEVEL1MINUSCOLOR: {NUMBER(2), default=[34]}</summary>
        [Seasar.Dao.Attrs.Column("LEVEL1MINUSCOLOR")]
        public int? Level1minuscolor {
            get { return _level1minuscolor; }
            set {
                __modifiedProperties.AddPropertyName("Level1minuscolor");
                _level1minuscolor = value;
            }
        }

        /// <summary>LEVEL2MINUSCOLOR: {NUMBER(2), default=[37]}</summary>
        [Seasar.Dao.Attrs.Column("LEVEL2MINUSCOLOR")]
        public int? Level2minuscolor {
            get { return _level2minuscolor; }
            set {
                __modifiedProperties.AddPropertyName("Level2minuscolor");
                _level2minuscolor = value;
            }
        }

        /// <summary>LEVEL2PERCENT: {NUMBER(2), default=[10]}</summary>
        [Seasar.Dao.Attrs.Column("LEVEL2PERCENT")]
        public int? Level2percent {
            get { return _level2percent; }
            set {
                __modifiedProperties.AddPropertyName("Level2percent");
                _level2percent = value;
            }
        }

        /// <summary>LEVEL1PERCENT: {NUMBER(2), default=[5]}</summary>
        [Seasar.Dao.Attrs.Column("LEVEL1PERCENT")]
        public int? Level1percent {
            get { return _level1percent; }
            set {
                __modifiedProperties.AddPropertyName("Level1percent");
                _level1percent = value;
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
