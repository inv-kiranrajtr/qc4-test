

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
    /// The entity of T_DATA_PROCESS_NEW_CATEGORY as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DATA_PROCESS_NEW_CATEGORY_ID
    /// 
    /// [column]
    ///     DATA_PROCESS_NEW_CATEGORY_ID, NEW_CATEGORY_NO, NEW_CATEGORY_NAME, SRC_ITEM_ID, OPERATION_CODE, CONDITION_STRING, BOTTOM_VALUE, UPPER_VALUE, DATA_EDIT_ID
    /// 
    /// [sequence]
    ///     T_Data_Process_New_CategorySEQ
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
    [Seasar.Dao.Attrs.Table("T_DATA_PROCESS_NEW_CATEGORY")]
    [System.Serializable]
    public partial class TDataProcessNewCategory : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_PROCESS_NEW_CATEGORY_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _dataProcessNewCategoryId;

        /// <summary>NEW_CATEGORY_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _newCategoryNo;

        /// <summary>NEW_CATEGORY_NAME: {NotNull, NVARCHAR2(200)}</summary>
        protected String _newCategoryName;

        /// <summary>SRC_ITEM_ID: {IX, NUMBER(27)}</summary>
        protected decimal? _srcItemId;

        /// <summary>OPERATION_CODE: {VARCHAR2(1)}</summary>
        protected String _operationCode;

        /// <summary>CONDITION_STRING: {VARCHAR2(1000)}</summary>
        protected String _conditionString;

        /// <summary>BOTTOM_VALUE: {NUMBER(10, 2)}</summary>
        protected decimal? _bottomValue;

        /// <summary>UPPER_VALUE: {NUMBER(10, 2)}</summary>
        protected decimal? _upperValue;

        /// <summary>DATA_EDIT_ID: {IX, NotNull, NUMBER(27), FK to T_DATA_PROCESS_NEW_ITEM}</summary>
        protected decimal? _dataEditId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DATA_PROCESS_NEW_CATEGORY"; } }
        public String TablePropertyName { get { return "TDataProcessNewCategory"; } }

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
                if (_dataProcessNewCategoryId == null) { return false; }
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
            if (other == null || !(other is TDataProcessNewCategory)) { return false; }
            TDataProcessNewCategory otherEntity = (TDataProcessNewCategory)other;
            if (!xSV(this.DataProcessNewCategoryId, otherEntity.DataProcessNewCategoryId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _dataProcessNewCategoryId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDataProcessNewCategory:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.DataProcessNewCategoryId);
            sb.Append(c).Append(this.NewCategoryNo);
            sb.Append(c).Append(this.NewCategoryName);
            sb.Append(c).Append(this.SrcItemId);
            sb.Append(c).Append(this.OperationCode);
            sb.Append(c).Append(this.ConditionString);
            sb.Append(c).Append(this.BottomValue);
            sb.Append(c).Append(this.UpperValue);
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
        /// <summary>DATA_PROCESS_NEW_CATEGORY_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DATA_PROCESS_NEW_CATEGORY_ID")]
        public decimal? DataProcessNewCategoryId {
            get { return _dataProcessNewCategoryId; }
            set {
                __modifiedProperties.AddPropertyName("DataProcessNewCategoryId");
                _dataProcessNewCategoryId = value;
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

        /// <summary>NEW_CATEGORY_NAME: {NotNull, NVARCHAR2(200)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_CATEGORY_NAME")]
        public String NewCategoryName {
            get { return _newCategoryName; }
            set {
                __modifiedProperties.AddPropertyName("NewCategoryName");
                _newCategoryName = value;
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

        /// <summary>BOTTOM_VALUE: {NUMBER(10, 2)}</summary>
        [Seasar.Dao.Attrs.Column("BOTTOM_VALUE")]
        public decimal? BottomValue {
            get { return _bottomValue; }
            set {
                __modifiedProperties.AddPropertyName("BottomValue");
                _bottomValue = value;
            }
        }

        /// <summary>UPPER_VALUE: {NUMBER(10, 2)}</summary>
        [Seasar.Dao.Attrs.Column("UPPER_VALUE")]
        public decimal? UpperValue {
            get { return _upperValue; }
            set {
                __modifiedProperties.AddPropertyName("UpperValue");
                _upperValue = value;
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
