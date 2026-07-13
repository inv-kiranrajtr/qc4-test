

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
    /// The entity of T_WEIGHTBACK_VALUE as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     WEIGHTBACK_VALUE_ID
    /// 
    /// [column]
    ///     WEIGHTBACK_VALUE_ID, WEIGHTBACK_ITEM_NO, PERCENT_VALUE, PARAMETER_N_VALUE, WEIGHTBACK_VALUE, WEIGHTBACK_ID
    /// 
    /// [sequence]
    ///     T_Weightback_Value_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_WEIGHTBACK
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tWeightback
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_WEIGHTBACK_VALUE")]
    [System.Serializable]
    public partial class TWeightbackValue : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>WEIGHTBACK_VALUE_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _weightbackValueId;

        /// <summary>WEIGHTBACK_ITEM_NO: {NotNull, NUMBER(27)}</summary>
        protected decimal? _weightbackItemNo;

        /// <summary>PERCENT_VALUE: {BINARY_DOUBLE(8)}</summary>
        protected String _percentValue;

        /// <summary>PARAMETER_N_VALUE: {BINARY_DOUBLE(8)}</summary>
        protected String _parameterNValue;

        /// <summary>WEIGHTBACK_VALUE: {BINARY_DOUBLE(8)}</summary>
        protected String _weightbackValue;

        /// <summary>WEIGHTBACK_ID: {IX, NotNull, NUMBER(27), FK to T_WEIGHTBACK}</summary>
        protected decimal? _weightbackId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_WEIGHTBACK_VALUE"; } }
        public String TablePropertyName { get { return "TWeightbackValue"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TWeightback _tWeightback;

        /// <summary>T_WEIGHTBACK as 'TWeightback'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("WEIGHTBACK_ID:WEIGHTBACK_ID")]
        public TWeightback TWeightback {
            get { return _tWeightback; }
            set { _tWeightback = value; }
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
                if (_weightbackValueId == null) { return false; }
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
            if (other == null || !(other is TWeightbackValue)) { return false; }
            TWeightbackValue otherEntity = (TWeightbackValue)other;
            if (!xSV(this.WeightbackValueId, otherEntity.WeightbackValueId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _weightbackValueId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TWeightbackValue:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tWeightback != null)
            { sb.Append(l).Append(xbRDS(_tWeightback, "TWeightback")); }
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
            sb.Append(c).Append(this.WeightbackValueId);
            sb.Append(c).Append(this.WeightbackItemNo);
            sb.Append(c).Append(this.PercentValue);
            sb.Append(c).Append(this.ParameterNValue);
            sb.Append(c).Append(this.WeightbackValue);
            sb.Append(c).Append(this.WeightbackId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tWeightback != null) { sb.Append(c).Append("TWeightback"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>WEIGHTBACK_VALUE_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_VALUE_ID")]
        public decimal? WeightbackValueId {
            get { return _weightbackValueId; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackValueId");
                _weightbackValueId = value;
            }
        }

        /// <summary>WEIGHTBACK_ITEM_NO: {NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_ITEM_NO")]
        public decimal? WeightbackItemNo {
            get { return _weightbackItemNo; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackItemNo");
                _weightbackItemNo = value;
            }
        }

        /// <summary>PERCENT_VALUE: {BINARY_DOUBLE(8)}</summary>
        [Seasar.Dao.Attrs.Column("PERCENT_VALUE")]
        public String PercentValue {
            get { return _percentValue; }
            set {
                __modifiedProperties.AddPropertyName("PercentValue");
                _percentValue = value;
            }
        }

        /// <summary>PARAMETER_N_VALUE: {BINARY_DOUBLE(8)}</summary>
        [Seasar.Dao.Attrs.Column("PARAMETER_N_VALUE")]
        public String ParameterNValue {
            get { return _parameterNValue; }
            set {
                __modifiedProperties.AddPropertyName("ParameterNValue");
                _parameterNValue = value;
            }
        }

        /// <summary>WEIGHTBACK_VALUE: {BINARY_DOUBLE(8)}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_VALUE")]
        public String WeightbackValue {
            get { return _weightbackValue; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackValue");
                _weightbackValue = value;
            }
        }

        /// <summary>WEIGHTBACK_ID: {IX, NotNull, NUMBER(27), FK to T_WEIGHTBACK}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_ID")]
        public decimal? WeightbackId {
            get { return _weightbackId; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackId");
                _weightbackId = value;
            }
        }

        #endregion
    }
}
