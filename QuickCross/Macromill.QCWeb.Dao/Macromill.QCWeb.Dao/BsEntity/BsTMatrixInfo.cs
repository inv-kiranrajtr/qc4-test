

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
    /// The entity of T_MATRIX_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     MATRIX_INFO_ID
    /// 
    /// [column]
    ///     MATRIX_INFO_ID, ITEM_INFO_ID, CHILD_ITEM_INFO_ID, ADD_FA_ITEM_INFO_ID, ADD_FA_CATEGORY_INFO_ID
    /// 
    /// [sequence]
    ///     T_Matrix_Info_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_ITEM_INFO, T_CATEGORY_INFO
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tItemInfoByItemInfoId, tItemInfoByChildItemInfoId, tCategoryInfo
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_MATRIX_INFO")]
    [System.Serializable]
    public partial class TMatrixInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>MATRIX_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _matrixInfoId;

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _itemInfoId;

        /// <summary>CHILD_ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _childItemInfoId;

        /// <summary>ADD_FA_ITEM_INFO_ID: {NUMBER(27)}</summary>
        protected decimal? _addFaItemInfoId;

        /// <summary>ADD_FA_CATEGORY_INFO_ID: {IX, NUMBER(27), FK to T_CATEGORY_INFO}</summary>
        protected decimal? _addFaCategoryInfoId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_MATRIX_INFO"; } }
        public String TablePropertyName { get { return "TMatrixInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TItemInfo _tItemInfoByItemInfoId;

        /// <summary>T_ITEM_INFO as 'TItemInfoByItemInfoId'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:ITEM_INFO_ID")]
        public TItemInfo TItemInfoByItemInfoId {
            get { return _tItemInfoByItemInfoId; }
            set { _tItemInfoByItemInfoId = value; }
        }

        protected TItemInfo _tItemInfoByChildItemInfoId;

        /// <summary>T_ITEM_INFO as 'TItemInfoByChildItemInfoId'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("CHILD_ITEM_INFO_ID:ITEM_INFO_ID")]
        public TItemInfo TItemInfoByChildItemInfoId {
            get { return _tItemInfoByChildItemInfoId; }
            set { _tItemInfoByChildItemInfoId = value; }
        }

        protected TCategoryInfo _tCategoryInfo;

        /// <summary>T_CATEGORY_INFO as 'TCategoryInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("ADD_FA_CATEGORY_INFO_ID:CATEGORY_INFO_ID")]
        public TCategoryInfo TCategoryInfo {
            get { return _tCategoryInfo; }
            set { _tCategoryInfo = value; }
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
                if (_matrixInfoId == null) { return false; }
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
            if (other == null || !(other is TMatrixInfo)) { return false; }
            TMatrixInfo otherEntity = (TMatrixInfo)other;
            if (!xSV(this.MatrixInfoId, otherEntity.MatrixInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _matrixInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TMatrixInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tItemInfoByItemInfoId != null)
            { sb.Append(l).Append(xbRDS(_tItemInfoByItemInfoId, "TItemInfoByItemInfoId")); }
            if (_tItemInfoByChildItemInfoId != null)
            { sb.Append(l).Append(xbRDS(_tItemInfoByChildItemInfoId, "TItemInfoByChildItemInfoId")); }
            if (_tCategoryInfo != null)
            { sb.Append(l).Append(xbRDS(_tCategoryInfo, "TCategoryInfo")); }
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
            sb.Append(c).Append(this.MatrixInfoId);
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.ChildItemInfoId);
            sb.Append(c).Append(this.AddFaItemInfoId);
            sb.Append(c).Append(this.AddFaCategoryInfoId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tItemInfoByItemInfoId != null) { sb.Append(c).Append("TItemInfoByItemInfoId"); }
            if (_tItemInfoByChildItemInfoId != null) { sb.Append(c).Append("TItemInfoByChildItemInfoId"); }
            if (_tCategoryInfo != null) { sb.Append(c).Append("TCategoryInfo"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>MATRIX_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("MATRIX_INFO_ID")]
        public decimal? MatrixInfoId {
            get { return _matrixInfoId; }
            set {
                __modifiedProperties.AddPropertyName("MatrixInfoId");
                _matrixInfoId = value;
            }
        }

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_INFO_ID")]
        public decimal? ItemInfoId {
            get { return _itemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ItemInfoId");
                _itemInfoId = value;
            }
        }

        /// <summary>CHILD_ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        [Seasar.Dao.Attrs.Column("CHILD_ITEM_INFO_ID")]
        public decimal? ChildItemInfoId {
            get { return _childItemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ChildItemInfoId");
                _childItemInfoId = value;
            }
        }

        /// <summary>ADD_FA_ITEM_INFO_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("ADD_FA_ITEM_INFO_ID")]
        public decimal? AddFaItemInfoId {
            get { return _addFaItemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("AddFaItemInfoId");
                _addFaItemInfoId = value;
            }
        }

        /// <summary>ADD_FA_CATEGORY_INFO_ID: {IX, NUMBER(27), FK to T_CATEGORY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("ADD_FA_CATEGORY_INFO_ID")]
        public decimal? AddFaCategoryInfoId {
            get { return _addFaCategoryInfoId; }
            set {
                __modifiedProperties.AddPropertyName("AddFaCategoryInfoId");
                _addFaCategoryInfoId = value;
            }
        }

        #endregion
    }
}
