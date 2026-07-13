

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
    /// The entity of T_CODE_MASTER as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     CODE_MASTER_ID
    /// 
    /// [column]
    ///     CODE_MASTER_ID, GROUP_KEY, CODE_VALUE, MESSAGE_ID, SORT_NO
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
    [Seasar.Dao.Attrs.Table("T_CODE_MASTER")]
    [System.Serializable]
    public partial class TCodeMaster : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>CODE_MASTER_ID: {PK, NotNull, VARCHAR2(3)}</summary>
        protected String _codeMasterId;

        /// <summary>GROUP_KEY: {NotNull, VARCHAR2(30)}</summary>
        protected String _groupKey;

        /// <summary>CODE_VALUE: {NotNull, VARCHAR2(3)}</summary>
        protected String _codeValue;

        /// <summary>MESSAGE_ID: {NotNull, VARCHAR2(60)}</summary>
        protected String _messageId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_CODE_MASTER"; } }
        public String TablePropertyName { get { return "TCodeMaster"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

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
                if (_codeMasterId == null) { return false; }
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
            if (other == null || !(other is TCodeMaster)) { return false; }
            TCodeMaster otherEntity = (TCodeMaster)other;
            if (!xSV(this.CodeMasterId, otherEntity.CodeMasterId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _codeMasterId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TCodeMaster:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.CodeMasterId);
            sb.Append(c).Append(this.GroupKey);
            sb.Append(c).Append(this.CodeValue);
            sb.Append(c).Append(this.MessageId);
            sb.Append(c).Append(this.SortNo);
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
        /// <summary>CODE_MASTER_ID: {PK, NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("CODE_MASTER_ID")]
        public String CodeMasterId {
            get { return _codeMasterId; }
            set {
                __modifiedProperties.AddPropertyName("CodeMasterId");
                _codeMasterId = value;
            }
        }

        /// <summary>GROUP_KEY: {NotNull, VARCHAR2(30)}</summary>
        [Seasar.Dao.Attrs.Column("GROUP_KEY")]
        public String GroupKey {
            get { return _groupKey; }
            set {
                __modifiedProperties.AddPropertyName("GroupKey");
                _groupKey = value;
            }
        }

        /// <summary>CODE_VALUE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("CODE_VALUE")]
        public String CodeValue {
            get { return _codeValue; }
            set {
                __modifiedProperties.AddPropertyName("CodeValue");
                _codeValue = value;
            }
        }

        /// <summary>MESSAGE_ID: {NotNull, VARCHAR2(60)}</summary>
        [Seasar.Dao.Attrs.Column("MESSAGE_ID")]
        public String MessageId {
            get { return _messageId; }
            set {
                __modifiedProperties.AddPropertyName("MessageId");
                _messageId = value;
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

        #endregion
    }
}
