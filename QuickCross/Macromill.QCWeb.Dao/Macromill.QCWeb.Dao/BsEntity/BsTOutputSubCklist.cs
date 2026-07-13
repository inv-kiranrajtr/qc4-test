

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
    /// The entity of T_OUTPUT_SUB_CKLIST as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_SUB_CKLIST_ID
    /// 
    /// [column]
    ///     OUTPUT_SUB_CKLIST_ID, OUTPUT_COMMON_ID, TOTAL_COUNT
    /// 
    /// [sequence]
    ///     T_Output_Sub_CKList_SEQ_01
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
    [Seasar.Dao.Attrs.Table("T_OUTPUT_SUB_CKLIST")]
    [System.Serializable]
    public partial class TOutputSubCklist : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_SUB_CKLIST_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputSubCklistId;

        /// <summary>OUTPUT_COMMON_ID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_COMMON}</summary>
        protected decimal? _outputCommonId;

        /// <summary>TOTAL_COUNT: {NotNull, NUMBER(10), default=[0]}</summary>
        protected long? _totalCount;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_SUB_CKLIST"; } }
        public String TablePropertyName { get { return "TOutputSubCklist"; } }

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
                if (_outputSubCklistId == null) { return false; }
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
            if (other == null || !(other is TOutputSubCklist)) { return false; }
            TOutputSubCklist otherEntity = (TOutputSubCklist)other;
            if (!xSV(this.OutputSubCklistId, otherEntity.OutputSubCklistId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputSubCklistId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputSubCklist:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.OutputSubCklistId);
            sb.Append(c).Append(this.OutputCommonId);
            sb.Append(c).Append(this.TotalCount);
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
        /// <summary>OUTPUT_SUB_CKLIST_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_SUB_CKLIST_ID")]
        public decimal? OutputSubCklistId {
            get { return _outputSubCklistId; }
            set {
                __modifiedProperties.AddPropertyName("OutputSubCklistId");
                _outputSubCklistId = value;
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

        /// <summary>TOTAL_COUNT: {NotNull, NUMBER(10), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("TOTAL_COUNT")]
        public long? TotalCount {
            get { return _totalCount; }
            set {
                __modifiedProperties.AddPropertyName("TotalCount");
                _totalCount = value;
            }
        }

        #endregion
    }
}
