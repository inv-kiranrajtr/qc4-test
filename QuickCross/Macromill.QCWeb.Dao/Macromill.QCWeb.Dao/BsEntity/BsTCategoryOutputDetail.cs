

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
    /// The entity of T_CATEGORY_OUTPUT_DETAIL as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     CATEGORY_OUTPUT_EDIT_DETAIL_ID
    /// 
    /// [column]
    ///     CATEGORY_OUTPUT_EDIT_DETAIL_ID, CATEGORY_OUTPUT_EDIT_ID, OLD_CATEGORY_NO, NEW_CATEGORY_NO
    /// 
    /// [sequence]
    ///     T_Category_Output_Detail_SEQ01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_CATEGORY_OUTPUT_EDIT
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tCategoryOutputEdit
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_CATEGORY_OUTPUT_DETAIL")]
    [System.Serializable]
    public partial class TCategoryOutputDetail : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>CATEGORY_OUTPUT_EDIT_DETAIL_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _categoryOutputEditDetailId;

        /// <summary>CATEGORY_OUTPUT_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_CATEGORY_OUTPUT_EDIT}</summary>
        protected decimal? _categoryOutputEditId;

        /// <summary>OLD_CATEGORY_NO: {NotNull, NUMBER(5)}</summary>
        protected int? _oldCategoryNo;

        /// <summary>NEW_CATEGORY_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _newCategoryNo;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_CATEGORY_OUTPUT_DETAIL"; } }
        public String TablePropertyName { get { return "TCategoryOutputDetail"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TCategoryOutputEdit _tCategoryOutputEdit;

        /// <summary>T_CATEGORY_OUTPUT_EDIT as 'TCategoryOutputEdit'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("CATEGORY_OUTPUT_EDIT_ID:CATEGORY_OUTPUT_EDIT_ID")]
        public TCategoryOutputEdit TCategoryOutputEdit {
            get { return _tCategoryOutputEdit; }
            set { _tCategoryOutputEdit = value; }
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
                if (_categoryOutputEditDetailId == null) { return false; }
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
            if (other == null || !(other is TCategoryOutputDetail)) { return false; }
            TCategoryOutputDetail otherEntity = (TCategoryOutputDetail)other;
            if (!xSV(this.CategoryOutputEditDetailId, otherEntity.CategoryOutputEditDetailId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _categoryOutputEditDetailId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TCategoryOutputDetail:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tCategoryOutputEdit != null)
            { sb.Append(l).Append(xbRDS(_tCategoryOutputEdit, "TCategoryOutputEdit")); }
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
            sb.Append(c).Append(this.CategoryOutputEditDetailId);
            sb.Append(c).Append(this.CategoryOutputEditId);
            sb.Append(c).Append(this.OldCategoryNo);
            sb.Append(c).Append(this.NewCategoryNo);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tCategoryOutputEdit != null) { sb.Append(c).Append("TCategoryOutputEdit"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>CATEGORY_OUTPUT_EDIT_DETAIL_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_OUTPUT_EDIT_DETAIL_ID")]
        public decimal? CategoryOutputEditDetailId {
            get { return _categoryOutputEditDetailId; }
            set {
                __modifiedProperties.AddPropertyName("CategoryOutputEditDetailId");
                _categoryOutputEditDetailId = value;
            }
        }

        /// <summary>CATEGORY_OUTPUT_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_CATEGORY_OUTPUT_EDIT}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_OUTPUT_EDIT_ID")]
        public decimal? CategoryOutputEditId {
            get { return _categoryOutputEditId; }
            set {
                __modifiedProperties.AddPropertyName("CategoryOutputEditId");
                _categoryOutputEditId = value;
            }
        }

        /// <summary>OLD_CATEGORY_NO: {NotNull, NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("OLD_CATEGORY_NO")]
        public int? OldCategoryNo {
            get { return _oldCategoryNo; }
            set {
                __modifiedProperties.AddPropertyName("OldCategoryNo");
                _oldCategoryNo = value;
            }
        }

        /// <summary>NEW_CATEGORY_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("NEW_CATEGORY_NO")]
        public int? NewCategoryNo {
            get { return _newCategoryNo; }
            set {
                __modifiedProperties.AddPropertyName("NewCategoryNo");
                _newCategoryNo = value;
            }
        }

        #endregion
    }
}
