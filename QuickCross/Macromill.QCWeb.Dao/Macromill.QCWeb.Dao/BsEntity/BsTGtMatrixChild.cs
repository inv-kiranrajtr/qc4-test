

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
    /// The entity of T_GT_MATRIX_CHILD as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     GT_MATRIX_CHILDID
    /// 
    /// [column]
    ///     GT_MATRIX_CHILDID, GT_MATRIX_INFO_ID, CHILD_ITEM_ID
    /// 
    /// [sequence]
    ///     T_GT_Matrix_Child_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_GT_MATRIX_INFO, T_ITEM_INFO
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tGtMatrixInfo, tItemInfo
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_GT_MATRIX_CHILD")]
    [System.Serializable]
    public partial class TGtMatrixChild : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>GT_MATRIX_CHILDID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _gtMatrixChildid;

        /// <summary>GT_MATRIX_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_GT_MATRIX_INFO}</summary>
        protected decimal? _gtMatrixInfoId;

        /// <summary>CHILD_ITEM_ID: {NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _childItemId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_GT_MATRIX_CHILD"; } }
        public String TablePropertyName { get { return "TGtMatrixChild"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TGtMatrixInfo _tGtMatrixInfo;

        /// <summary>T_GT_MATRIX_INFO as 'TGtMatrixInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("GT_MATRIX_INFO_ID:GT_MATRIX_INFO_ID")]
        public TGtMatrixInfo TGtMatrixInfo {
            get { return _tGtMatrixInfo; }
            set { _tGtMatrixInfo = value; }
        }

        protected TItemInfo _tItemInfo;

        /// <summary>T_ITEM_INFO as 'TItemInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("CHILD_ITEM_ID:ITEM_INFO_ID")]
        public TItemInfo TItemInfo {
            get { return _tItemInfo; }
            set { _tItemInfo = value; }
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
                if (_gtMatrixChildid == null) { return false; }
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
            if (other == null || !(other is TGtMatrixChild)) { return false; }
            TGtMatrixChild otherEntity = (TGtMatrixChild)other;
            if (!xSV(this.GtMatrixChildid, otherEntity.GtMatrixChildid)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _gtMatrixChildid);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TGtMatrixChild:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tGtMatrixInfo != null)
            { sb.Append(l).Append(xbRDS(_tGtMatrixInfo, "TGtMatrixInfo")); }
            if (_tItemInfo != null)
            { sb.Append(l).Append(xbRDS(_tItemInfo, "TItemInfo")); }
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
            sb.Append(c).Append(this.GtMatrixChildid);
            sb.Append(c).Append(this.GtMatrixInfoId);
            sb.Append(c).Append(this.ChildItemId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tGtMatrixInfo != null) { sb.Append(c).Append("TGtMatrixInfo"); }
            if (_tItemInfo != null) { sb.Append(c).Append("TItemInfo"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>GT_MATRIX_CHILDID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("GT_MATRIX_CHILDID")]
        public decimal? GtMatrixChildid {
            get { return _gtMatrixChildid; }
            set {
                __modifiedProperties.AddPropertyName("GtMatrixChildid");
                _gtMatrixChildid = value;
            }
        }

        /// <summary>GT_MATRIX_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_GT_MATRIX_INFO}</summary>
        [Seasar.Dao.Attrs.Column("GT_MATRIX_INFO_ID")]
        public decimal? GtMatrixInfoId {
            get { return _gtMatrixInfoId; }
            set {
                __modifiedProperties.AddPropertyName("GtMatrixInfoId");
                _gtMatrixInfoId = value;
            }
        }

        /// <summary>CHILD_ITEM_ID: {NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        [Seasar.Dao.Attrs.Column("CHILD_ITEM_ID")]
        public decimal? ChildItemId {
            get { return _childItemId; }
            set {
                __modifiedProperties.AddPropertyName("ChildItemId");
                _childItemId = value;
            }
        }

        #endregion
    }
}
