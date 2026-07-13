

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
    /// The entity of T_DATA_PROCESS_NEW_ITEM_SRC as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DATA_PROCESS_NEW_ITEM_SRC_ID
    /// 
    /// [column]
    ///     DATA_PROCESS_NEW_ITEM_SRC_ID, SRC_ITEM_ID, NEW_ITEM_ID, SORT_NO, TARGET_FLAG, DATA_EDIT_ID
    /// 
    /// [sequence]
    ///     T_Data_Process_New_Item_SrcSEQ
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
    [Seasar.Dao.Attrs.Table("T_DATA_PROCESS_NEW_ITEM_SRC")]
    [System.Serializable]
    public partial class TDataProcessNewItemSrc : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_PROCESS_NEW_ITEM_SRC_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _dataProcessNewItemSrcId;

        /// <summary>SRC_ITEM_ID: {IX, NUMBER(27)}</summary>
        protected decimal? _srcItemId;

        /// <summary>NEW_ITEM_ID: {NUMBER(27)}</summary>
        protected decimal? _newItemId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>TARGET_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _targetFlag;

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_DATA_PROCESS_NEW_ITEM}</summary>
        protected decimal? _dataEditId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM_SRC"; } }
        public String TablePropertyName { get { return "TDataProcessNewItemSrc"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag TargetFlagAsFlag { get {
            return CDef.Flag.CodeOf(_targetFlag);
        } set {
            TargetFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of targetFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTargetFlag_True() {
            TargetFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of targetFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTargetFlag_False() {
            TargetFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of targetFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTargetFlagTrue {
            get {
                CDef.Flag cls = TargetFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of targetFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTargetFlagFalse {
            get {
                CDef.Flag cls = TargetFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String TargetFlagName {
            get {
                CDef.Flag cls = TargetFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TargetFlagAlias {
            get {
                CDef.Flag cls = TargetFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

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
                if (_dataProcessNewItemSrcId == null) { return false; }
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
            if (other == null || !(other is TDataProcessNewItemSrc)) { return false; }
            TDataProcessNewItemSrc otherEntity = (TDataProcessNewItemSrc)other;
            if (!xSV(this.DataProcessNewItemSrcId, otherEntity.DataProcessNewItemSrcId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _dataProcessNewItemSrcId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDataProcessNewItemSrc:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.DataProcessNewItemSrcId);
            sb.Append(c).Append(this.SrcItemId);
            sb.Append(c).Append(this.NewItemId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.TargetFlag);
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
        /// <summary>DATA_PROCESS_NEW_ITEM_SRC_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DATA_PROCESS_NEW_ITEM_SRC_ID")]
        public decimal? DataProcessNewItemSrcId {
            get { return _dataProcessNewItemSrcId; }
            set {
                __modifiedProperties.AddPropertyName("DataProcessNewItemSrcId");
                _dataProcessNewItemSrcId = value;
            }
        }

        /// <summary>SRC_ITEM_ID: {IX, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("SRC_ITEM_ID")]
        public decimal? SrcItemId {
            get { return _srcItemId; }
            set {
                __modifiedProperties.AddPropertyName("SrcItemId");
                _srcItemId = value;
            }
        }

        /// <summary>NEW_ITEM_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ITEM_ID")]
        public decimal? NewItemId {
            get { return _newItemId; }
            set {
                __modifiedProperties.AddPropertyName("NewItemId");
                _newItemId = value;
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

        /// <summary>TARGET_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_FLAG")]
        public int? TargetFlag {
            get { return _targetFlag; }
            set {
                __modifiedProperties.AddPropertyName("TargetFlag");
                _targetFlag = value;
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
