

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
    /// The entity of T_EDIT_TARGET_ITEM as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     EDIT_TARGET_ITEM_ID
    /// 
    /// [column]
    ///     EDIT_TARGET_ITEM_ID, SORT_NO, TARGET_ITEM_ID, DATA_EDIT_ID
    /// 
    /// [sequence]
    ///     T_Edit_Target_Item_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_EDIT_DATA
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tEditData
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_EDIT_TARGET_ITEM")]
    [System.Serializable]
    public partial class TEditTargetItem : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>EDIT_TARGET_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _editTargetItemId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>TARGET_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _targetItemId;

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_EDIT_DATA}</summary>
        protected decimal? _dataEditId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_EDIT_TARGET_ITEM"; } }
        public String TablePropertyName { get { return "TEditTargetItem"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TEditData _tEditData;

        /// <summary>T_EDIT_DATA as 'TEditData'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TEditData TEditData {
            get { return _tEditData; }
            set { _tEditData = value; }
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
                if (_editTargetItemId == null) { return false; }
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
            if (other == null || !(other is TEditTargetItem)) { return false; }
            TEditTargetItem otherEntity = (TEditTargetItem)other;
            if (!xSV(this.EditTargetItemId, otherEntity.EditTargetItemId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _editTargetItemId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TEditTargetItem:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tEditData != null)
            { sb.Append(l).Append(xbRDS(_tEditData, "TEditData")); }
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
            sb.Append(c).Append(this.EditTargetItemId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.TargetItemId);
            sb.Append(c).Append(this.DataEditId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tEditData != null) { sb.Append(c).Append("TEditData"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>EDIT_TARGET_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_TARGET_ITEM_ID")]
        public decimal? EditTargetItemId {
            get { return _editTargetItemId; }
            set {
                __modifiedProperties.AddPropertyName("EditTargetItemId");
                _editTargetItemId = value;
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

        /// <summary>TARGET_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_ITEM_ID")]
        public decimal? TargetItemId {
            get { return _targetItemId; }
            set {
                __modifiedProperties.AddPropertyName("TargetItemId");
                _targetItemId = value;
            }
        }

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_EDIT_DATA}</summary>
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
