

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
    /// The entity of T_INTEG_CONDITION as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     INTEG_CONDITION_ID
    /// 
    /// [column]
    ///     INTEG_CONDITION_ID, CONDITION_NO, SRC_ITEM_ID, SOURCE_ITEM_NO, OPERATION_CODE, CONDITION_STRING, DATA_EDIT_ID
    /// 
    /// [sequence]
    ///     T_Integ_Condition_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DATA_PROCESS_NEW_ITEM
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tDataProcessNewItem
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_INTEG_CONDITION")]
    [System.Serializable]
    public partial class TIntegCondition : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>INTEG_CONDITION_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _integConditionId;

        /// <summary>CONDITION_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _conditionNo;

        /// <summary>SRC_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _srcItemId;

        /// <summary>SOURCE_ITEM_NO: {NotNull, NUMBER(5)}</summary>
        protected int? _sourceItemNo;

        /// <summary>OPERATION_CODE: {VARCHAR2(1)}</summary>
        protected String _operationCode;

        /// <summary>CONDITION_STRING: {VARCHAR2(1000)}</summary>
        protected String _conditionString;

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_DATA_PROCESS_NEW_ITEM}</summary>
        protected decimal? _dataEditId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_INTEG_CONDITION"; } }
        public String TablePropertyName { get { return "TIntegCondition"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDataProcessNewItem _tDataProcessNewItem;

        /// <summary>T_DATA_PROCESS_NEW_ITEM as 'TDataProcessNewItem'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDataProcessNewItem TDataProcessNewItem {
            get { return _tDataProcessNewItem; }
            set { _tDataProcessNewItem = value; }
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
                if (_integConditionId == null) { return false; }
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
            if (other == null || !(other is TIntegCondition)) { return false; }
            TIntegCondition otherEntity = (TIntegCondition)other;
            if (!xSV(this.IntegConditionId, otherEntity.IntegConditionId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _integConditionId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TIntegCondition:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDataProcessNewItem != null)
            { sb.Append(l).Append(xbRDS(_tDataProcessNewItem, "TDataProcessNewItem")); }
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
            sb.Append(c).Append(this.IntegConditionId);
            sb.Append(c).Append(this.ConditionNo);
            sb.Append(c).Append(this.SrcItemId);
            sb.Append(c).Append(this.SourceItemNo);
            sb.Append(c).Append(this.OperationCode);
            sb.Append(c).Append(this.ConditionString);
            sb.Append(c).Append(this.DataEditId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDataProcessNewItem != null) { sb.Append(c).Append("TDataProcessNewItem"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>INTEG_CONDITION_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("INTEG_CONDITION_ID")]
        public decimal? IntegConditionId {
            get { return _integConditionId; }
            set {
                __modifiedProperties.AddPropertyName("IntegConditionId");
                _integConditionId = value;
            }
        }

        /// <summary>CONDITION_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_NO")]
        public int? ConditionNo {
            get { return _conditionNo; }
            set {
                __modifiedProperties.AddPropertyName("ConditionNo");
                _conditionNo = value;
            }
        }

        /// <summary>SRC_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("SRC_ITEM_ID")]
        public decimal? SrcItemId {
            get { return _srcItemId; }
            set {
                __modifiedProperties.AddPropertyName("SrcItemId");
                _srcItemId = value;
            }
        }

        /// <summary>SOURCE_ITEM_NO: {NotNull, NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SOURCE_ITEM_NO")]
        public int? SourceItemNo {
            get { return _sourceItemNo; }
            set {
                __modifiedProperties.AddPropertyName("SourceItemNo");
                _sourceItemNo = value;
            }
        }

        /// <summary>OPERATION_CODE: {VARCHAR2(1)}</summary>
        [Seasar.Dao.Attrs.Column("OPERATION_CODE")]
        public String OperationCode {
            get { return _operationCode; }
            set {
                __modifiedProperties.AddPropertyName("OperationCode");
                _operationCode = value;
            }
        }

        /// <summary>CONDITION_STRING: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_STRING")]
        public String ConditionString {
            get { return _conditionString; }
            set {
                __modifiedProperties.AddPropertyName("ConditionString");
                _conditionString = value;
            }
        }

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_DATA_PROCESS_NEW_ITEM}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
            }
        }

        #endregion
    }
}
