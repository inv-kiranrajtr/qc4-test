

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
    /// The entity of T_CATEGORY_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     CATEGORY_INFO_ID
    /// 
    /// [column]
    ///     CATEGORY_INFO_ID, ITEM_INFO_ID, CATEGORY_NO, CATEGORY_NAME, WEIGHT_VALUE, ORIGINAL_CATEGORY_NAME, ORIGINAL_WEIGHT_VALUE
    /// 
    /// [sequence]
    ///     T_Category_Info_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_ITEM_INFO
    /// 
    /// [referrer-table]
    ///     T_MATRIX_INFO
    /// 
    /// [foreign-property]
    ///     tItemInfo
    /// 
    /// [referrer-property]
    ///     tMatrixInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_CATEGORY_INFO")]
    [System.Serializable]
    public partial class TCategoryInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>CATEGORY_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _categoryInfoId;

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _itemInfoId;

        /// <summary>CATEGORY_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _categoryNo;

        /// <summary>CATEGORY_NAME: {NVARCHAR2(200)}</summary>
        protected String _categoryName;

        /// <summary>WEIGHT_VALUE: {VARCHAR2(17)}</summary>
        protected String _weightValue;

        /// <summary>ORIGINAL_CATEGORY_NAME: {NVARCHAR2(200)}</summary>
        protected String _originalCategoryName;

        /// <summary>ORIGINAL_WEIGHT_VALUE: {VARCHAR2(17)}</summary>
        protected String _originalWeightValue;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_CATEGORY_INFO"; } }
        public String TablePropertyName { get { return "TCategoryInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TItemInfo _tItemInfo;

        /// <summary>T_ITEM_INFO as 'TItemInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:ITEM_INFO_ID")]
        public TItemInfo TItemInfo {
            get { return _tItemInfo; }
            set { _tItemInfo = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TMatrixInfo> _tMatrixInfoList;

        /// <summary>T_MATRIX_INFO as 'TMatrixInfoList'.</summary>
        public IList<TMatrixInfo> TMatrixInfoList {
            get { if (_tMatrixInfoList == null) { _tMatrixInfoList = new List<TMatrixInfo>(); } return _tMatrixInfoList; }
            set { _tMatrixInfoList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_categoryInfoId == null) { return false; }
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
            if (other == null || !(other is TCategoryInfo)) { return false; }
            TCategoryInfo otherEntity = (TCategoryInfo)other;
            if (!xSV(this.CategoryInfoId, otherEntity.CategoryInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _categoryInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TCategoryInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tItemInfo != null)
            { sb.Append(l).Append(xbRDS(_tItemInfo, "TItemInfo")); }
            if (_tMatrixInfoList != null) { foreach (Entity e in _tMatrixInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TMatrixInfoList")); } } }
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
            sb.Append(c).Append(this.CategoryInfoId);
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.CategoryNo);
            sb.Append(c).Append(this.CategoryName);
            sb.Append(c).Append(this.WeightValue);
            sb.Append(c).Append(this.OriginalCategoryName);
            sb.Append(c).Append(this.OriginalWeightValue);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tItemInfo != null) { sb.Append(c).Append("TItemInfo"); }
            if (_tMatrixInfoList != null && _tMatrixInfoList.Count > 0)
            { sb.Append(c).Append("TMatrixInfoList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>CATEGORY_INFO_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_INFO_ID")]
        public decimal? CategoryInfoId {
            get { return _categoryInfoId; }
            set {
                __modifiedProperties.AddPropertyName("CategoryInfoId");
                _categoryInfoId = value;
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

        /// <summary>CATEGORY_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_NO")]
        public int? CategoryNo {
            get { return _categoryNo; }
            set {
                __modifiedProperties.AddPropertyName("CategoryNo");
                _categoryNo = value;
            }
        }

        /// <summary>CATEGORY_NAME: {NVARCHAR2(200)}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_NAME")]
        public String CategoryName {
            get { return _categoryName; }
            set {
                __modifiedProperties.AddPropertyName("CategoryName");
                _categoryName = value;
            }
        }

        /// <summary>WEIGHT_VALUE: {VARCHAR2(17)}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHT_VALUE")]
        public String WeightValue {
            get { return _weightValue; }
            set {
                __modifiedProperties.AddPropertyName("WeightValue");
                _weightValue = value;
            }
        }

        /// <summary>ORIGINAL_CATEGORY_NAME: {NVARCHAR2(200)}</summary>
        [Seasar.Dao.Attrs.Column("ORIGINAL_CATEGORY_NAME")]
        public String OriginalCategoryName {
            get { return _originalCategoryName; }
            set {
                __modifiedProperties.AddPropertyName("OriginalCategoryName");
                _originalCategoryName = value;
            }
        }

        /// <summary>ORIGINAL_WEIGHT_VALUE: {VARCHAR2(17)}</summary>
        [Seasar.Dao.Attrs.Column("ORIGINAL_WEIGHT_VALUE")]
        public String OriginalWeightValue {
            get { return _originalWeightValue; }
            set {
                __modifiedProperties.AddPropertyName("OriginalWeightValue");
                _originalWeightValue = value;
            }
        }

        #endregion
    }
}
