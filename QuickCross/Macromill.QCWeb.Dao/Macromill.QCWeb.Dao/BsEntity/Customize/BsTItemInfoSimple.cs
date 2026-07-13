

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Helper;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Dao.BsEntity.Customize.Dbm;


namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    /// <summary>
    /// The entity of TItemInfoSimple. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     ITEM_INFO_ID, ITEM_NAME, DATA_EDIT_ID, STATUS
    /// 
    /// [sequence]
    ///     
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("TItemInfoSimple")]
    [System.Serializable]
    public partial class TItemInfoSimple : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>ITEM_INFO_ID: {NUMBER(27)}</summary>
        protected decimal? _itemInfoId;

        /// <summary>ITEM_NAME: {VARCHAR2(26)}</summary>
        protected String _itemName;

        /// <summary>DATA_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _dataEditId;

        /// <summary>STATUS: {NUMBER(1)}</summary>
        protected int? _status;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "TItemInfoSimple"; } }
        public String TablePropertyName { get { return "TItemInfoSimple"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return TItemInfoSimpleDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
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
                return false;
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
            if (other == null || !(other is TItemInfoSimple)) { return false; }
            TItemInfoSimple otherEntity = (TItemInfoSimple)other;
            if (!xSV(this.ItemInfoId, otherEntity.ItemInfoId)) { return false; }
            if (!xSV(this.ItemName, otherEntity.ItemName)) { return false; }
            if (!xSV(this.DataEditId, otherEntity.DataEditId)) { return false; }
            if (!xSV(this.Status, otherEntity.Status)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _itemInfoId);
            result = xCH(result, _itemName);
            result = xCH(result, _dataEditId);
            result = xCH(result, _status);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TItemInfoSimple:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            return sb.ToString();
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
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.ItemName);
            sb.Append(c).Append(this.DataEditId);
            sb.Append(c).Append(this.Status);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            return "";
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>ITEM_INFO_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_INFO_ID")]
        public decimal? ItemInfoId {
            get { return _itemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ItemInfoId");
                _itemInfoId = value;
            }
        }

        /// <summary>ITEM_NAME: {VARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_NAME")]
        public String ItemName {
            get { return _itemName; }
            set {
                __modifiedProperties.AddPropertyName("ItemName");
                _itemName = value;
            }
        }

        /// <summary>DATA_EDIT_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
            }
        }

        /// <summary>STATUS: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("STATUS")]
        public int? Status {
            get { return _status; }
            set {
                __modifiedProperties.AddPropertyName("Status");
                _status = value;
            }
        }

        #endregion
    }
}
