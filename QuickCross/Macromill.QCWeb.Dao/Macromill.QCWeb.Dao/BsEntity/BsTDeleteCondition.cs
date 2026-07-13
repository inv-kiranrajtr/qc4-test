

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
    /// The entity of T_DELETE_CONDITION as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DELETE_CONDITION_ID
    /// 
    /// [column]
    ///     DELETE_CONDITION_ID, SORT_NO, ITEM_ID, OPERATION_CODE, OPERATION_VALUE, DATA_EDIT_ID
    /// 
    /// [sequence]
    ///     T_Delete_Condition_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DELETE_DATA
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tDeleteData
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DELETE_CONDITION")]
    [System.Serializable]
    public partial class TDeleteCondition : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DELETE_CONDITION_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _deleteConditionId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _itemId;

        /// <summary>OPERATION_CODE: {NotNull, VARCHAR2(1)}</summary>
        protected String _operationCode;

        /// <summary>OPERATION_VALUE: {VARCHAR2(1000)}</summary>
        protected String _operationValue;

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_DELETE_DATA}</summary>
        protected decimal? _dataEditId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DELETE_CONDITION"; } }
        public String TablePropertyName { get { return "TDeleteCondition"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDeleteData _tDeleteData;

        /// <summary>T_DELETE_DATA as 'TDeleteData'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDeleteData TDeleteData {
            get { return _tDeleteData; }
            set { _tDeleteData = value; }
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
                if (_deleteConditionId == null) { return false; }
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
            if (other == null || !(other is TDeleteCondition)) { return false; }
            TDeleteCondition otherEntity = (TDeleteCondition)other;
            if (!xSV(this.DeleteConditionId, otherEntity.DeleteConditionId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _deleteConditionId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDeleteCondition:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDeleteData != null)
            { sb.Append(l).Append(xbRDS(_tDeleteData, "TDeleteData")); }
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
            sb.Append(c).Append(this.DeleteConditionId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.ItemId);
            sb.Append(c).Append(this.OperationCode);
            sb.Append(c).Append(this.OperationValue);
            sb.Append(c).Append(this.DataEditId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDeleteData != null) { sb.Append(c).Append("TDeleteData"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DELETE_CONDITION_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_CONDITION_ID")]
        public decimal? DeleteConditionId {
            get { return _deleteConditionId; }
            set {
                __modifiedProperties.AddPropertyName("DeleteConditionId");
                _deleteConditionId = value;
            }
        }

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public int? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_ID")]
        public decimal? ItemId {
            get { return _itemId; }
            set {
                __modifiedProperties.AddPropertyName("ItemId");
                _itemId = value;
            }
        }

        /// <summary>OPERATION_CODE: {NotNull, VARCHAR2(1)}</summary>
        [Seasar.Dao.Attrs.Column("OPERATION_CODE")]
        public String OperationCode {
            get { return _operationCode; }
            set {
                __modifiedProperties.AddPropertyName("OperationCode");
                _operationCode = value;
            }
        }

        /// <summary>OPERATION_VALUE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("OPERATION_VALUE")]
        public String OperationValue {
            get { return _operationValue; }
            set {
                __modifiedProperties.AddPropertyName("OperationValue");
                _operationValue = value;
            }
        }

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_DELETE_DATA}</summary>
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
