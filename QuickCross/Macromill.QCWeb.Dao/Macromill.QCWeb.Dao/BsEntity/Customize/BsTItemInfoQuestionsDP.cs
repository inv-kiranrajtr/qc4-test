

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
    /// The entity of TItemInfoQuestionsDP. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     ITEM_INFO_ID, QCWEBID, ITEM_NAME, SOURCE_DIV, ITEMNO, ITEM_TYPE, ANSWER_TYPE, SORT_NUMBER, MATRIX_DIV, LV1TITLE, LV2TITLE, ORIGINAL_LV1TITLE, ORIGINAL_LV2TITLE, CATEGORY_EDIT_ID, DATA_EDIT_ID, STATUS, COMPEL_ITEM_CHANGE_FLAG, SORT_FLAG, SORT_RANGE, MULTIVARIATE_FLAG, CATEGORY_COUNT, CHILD_ITEM_COUNT, DP_DATA_EDIT_ID, EXECUTE_NO, DP_STATUS, NEW_ITEM_NAME, NEW_ANSWER_TYPE, NEW_LV1TITLE, NEW_LV2TITLE, DP_CATEGORY_COUNT
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
    [Seasar.Dao.Attrs.Table("TItemInfoQuestionsDP")]
    [System.Serializable]
    public partial class TItemInfoQuestionsDP : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>ITEM_INFO_ID: {NUMBER(27)}</summary>
        protected decimal? _itemInfoId;

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>ITEM_NAME: {VARCHAR2(26)}</summary>
        protected String _itemName;

        /// <summary>SOURCE_DIV: {CHAR(1)}</summary>
        protected String _sourceDiv;

        /// <summary>ITEMNO: {VARCHAR2(26)}</summary>
        protected String _itemno;

        /// <summary>ITEM_TYPE: {VARCHAR2(3)}</summary>
        protected String _itemType;

        /// <summary>ANSWER_TYPE: {CHAR(1)}</summary>
        protected String _answerType;

        /// <summary>SORT_NUMBER: {NUMBER(5)}</summary>
        protected int? _sortNumber;

        /// <summary>MATRIX_DIV: {NUMBER(1)}</summary>
        protected int? _matrixDiv;

        /// <summary>LV1TITLE: {VARCHAR2(1000)}</summary>
        protected String _lv1title;

        /// <summary>LV2TITLE: {VARCHAR2(1000)}</summary>
        protected String _lv2title;

        /// <summary>ORIGINAL_LV1TITLE: {VARCHAR2(1000)}</summary>
        protected String _originalLv1title;

        /// <summary>ORIGINAL_LV2TITLE: {VARCHAR2(1000)}</summary>
        protected String _originalLv2title;

        /// <summary>CATEGORY_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _categoryEditId;

        /// <summary>DATA_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _dataEditId;

        /// <summary>STATUS: {NUMBER(1)}</summary>
        protected int? _status;

        /// <summary>COMPEL_ITEM_CHANGE_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _compelItemChangeFlag;

        /// <summary>SORT_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _sortFlag;

        /// <summary>SORT_RANGE: {NUMBER(5)}</summary>
        protected int? _sortRange;

        /// <summary>MULTIVARIATE_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _multivariateFlag;

        /// <summary>CATEGORY_COUNT: {NUMBER(22)}</summary>
        protected decimal? _categoryCount;

        /// <summary>CHILD_ITEM_COUNT: {NUMBER(22)}</summary>
        protected decimal? _childItemCount;

        /// <summary>DP_DATA_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _dpDataEditId;

        /// <summary>EXECUTE_NO: {NUMBER(5)}</summary>
        protected int? _executeNo;

        /// <summary>DP_STATUS: {VARCHAR2(2)}</summary>
        protected String _dpStatus;

        /// <summary>NEW_ITEM_NAME: {VARCHAR2(26)}</summary>
        protected String _newItemName;

        /// <summary>NEW_ANSWER_TYPE: {CHAR(1)}</summary>
        protected String _newAnswerType;

        /// <summary>NEW_LV1TITLE: {VARCHAR2(1000)}</summary>
        protected String _newLv1title;

        /// <summary>NEW_LV2TITLE: {VARCHAR2(1000)}</summary>
        protected String _newLv2title;

        /// <summary>DP_CATEGORY_COUNT: {NUMBER(22)}</summary>
        protected decimal? _dpCategoryCount;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "TItemInfoQuestionsDP"; } }
        public String TablePropertyName { get { return "TItemInfoQuestionsDP"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return TItemInfoQuestionsDPDbm.GetInstance(); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag CompelItemChangeFlagAsFlag { get {
            return CDef.Flag.CodeOf(_compelItemChangeFlag);
        } set {
            CompelItemChangeFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag SortFlagAsFlag { get {
            return CDef.Flag.CodeOf(_sortFlag);
        } set {
            SortFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag MultivariateFlagAsFlag { get {
            return CDef.Flag.CodeOf(_multivariateFlag);
        } set {
            MultivariateFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of compelItemChangeFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCompelItemChangeFlag_True() {
            CompelItemChangeFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of compelItemChangeFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCompelItemChangeFlag_False() {
            CompelItemChangeFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of sortFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetSortFlag_True() {
            SortFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of sortFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetSortFlag_False() {
            SortFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of multivariateFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetMultivariateFlag_True() {
            MultivariateFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of multivariateFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetMultivariateFlag_False() {
            MultivariateFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of compelItemChangeFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCompelItemChangeFlagTrue {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of compelItemChangeFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCompelItemChangeFlagFalse {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of sortFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsSortFlagTrue {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of sortFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsSortFlagFalse {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of multivariateFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsMultivariateFlagTrue {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of multivariateFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsMultivariateFlagFalse {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String CompelItemChangeFlagName {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CompelItemChangeFlagAlias {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String SortFlagName {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String SortFlagAlias {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String MultivariateFlagName {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String MultivariateFlagAlias {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

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
            if (other == null || !(other is TItemInfoQuestionsDP)) { return false; }
            TItemInfoQuestionsDP otherEntity = (TItemInfoQuestionsDP)other;
            if (!xSV(this.ItemInfoId, otherEntity.ItemInfoId)) { return false; }
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            if (!xSV(this.ItemName, otherEntity.ItemName)) { return false; }
            if (!xSV(this.SourceDiv, otherEntity.SourceDiv)) { return false; }
            if (!xSV(this.Itemno, otherEntity.Itemno)) { return false; }
            if (!xSV(this.ItemType, otherEntity.ItemType)) { return false; }
            if (!xSV(this.AnswerType, otherEntity.AnswerType)) { return false; }
            if (!xSV(this.SortNumber, otherEntity.SortNumber)) { return false; }
            if (!xSV(this.MatrixDiv, otherEntity.MatrixDiv)) { return false; }
            if (!xSV(this.Lv1title, otherEntity.Lv1title)) { return false; }
            if (!xSV(this.Lv2title, otherEntity.Lv2title)) { return false; }
            if (!xSV(this.OriginalLv1title, otherEntity.OriginalLv1title)) { return false; }
            if (!xSV(this.OriginalLv2title, otherEntity.OriginalLv2title)) { return false; }
            if (!xSV(this.CategoryEditId, otherEntity.CategoryEditId)) { return false; }
            if (!xSV(this.DataEditId, otherEntity.DataEditId)) { return false; }
            if (!xSV(this.Status, otherEntity.Status)) { return false; }
            if (!xSV(this.CompelItemChangeFlag, otherEntity.CompelItemChangeFlag)) { return false; }
            if (!xSV(this.SortFlag, otherEntity.SortFlag)) { return false; }
            if (!xSV(this.SortRange, otherEntity.SortRange)) { return false; }
            if (!xSV(this.MultivariateFlag, otherEntity.MultivariateFlag)) { return false; }
            if (!xSV(this.CategoryCount, otherEntity.CategoryCount)) { return false; }
            if (!xSV(this.ChildItemCount, otherEntity.ChildItemCount)) { return false; }
            if (!xSV(this.DpDataEditId, otherEntity.DpDataEditId)) { return false; }
            if (!xSV(this.ExecuteNo, otherEntity.ExecuteNo)) { return false; }
            if (!xSV(this.DpStatus, otherEntity.DpStatus)) { return false; }
            if (!xSV(this.NewItemName, otherEntity.NewItemName)) { return false; }
            if (!xSV(this.NewAnswerType, otherEntity.NewAnswerType)) { return false; }
            if (!xSV(this.NewLv1title, otherEntity.NewLv1title)) { return false; }
            if (!xSV(this.NewLv2title, otherEntity.NewLv2title)) { return false; }
            if (!xSV(this.DpCategoryCount, otherEntity.DpCategoryCount)) { return false; }
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
            result = xCH(result, _qcwebid);
            result = xCH(result, _itemName);
            result = xCH(result, _sourceDiv);
            result = xCH(result, _itemno);
            result = xCH(result, _itemType);
            result = xCH(result, _answerType);
            result = xCH(result, _sortNumber);
            result = xCH(result, _matrixDiv);
            result = xCH(result, _lv1title);
            result = xCH(result, _lv2title);
            result = xCH(result, _originalLv1title);
            result = xCH(result, _originalLv2title);
            result = xCH(result, _categoryEditId);
            result = xCH(result, _dataEditId);
            result = xCH(result, _status);
            result = xCH(result, _compelItemChangeFlag);
            result = xCH(result, _sortFlag);
            result = xCH(result, _sortRange);
            result = xCH(result, _multivariateFlag);
            result = xCH(result, _categoryCount);
            result = xCH(result, _childItemCount);
            result = xCH(result, _dpDataEditId);
            result = xCH(result, _executeNo);
            result = xCH(result, _dpStatus);
            result = xCH(result, _newItemName);
            result = xCH(result, _newAnswerType);
            result = xCH(result, _newLv1title);
            result = xCH(result, _newLv2title);
            result = xCH(result, _dpCategoryCount);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TItemInfoQuestionsDP:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.ItemName);
            sb.Append(c).Append(this.SourceDiv);
            sb.Append(c).Append(this.Itemno);
            sb.Append(c).Append(this.ItemType);
            sb.Append(c).Append(this.AnswerType);
            sb.Append(c).Append(this.SortNumber);
            sb.Append(c).Append(this.MatrixDiv);
            sb.Append(c).Append(this.Lv1title);
            sb.Append(c).Append(this.Lv2title);
            sb.Append(c).Append(this.OriginalLv1title);
            sb.Append(c).Append(this.OriginalLv2title);
            sb.Append(c).Append(this.CategoryEditId);
            sb.Append(c).Append(this.DataEditId);
            sb.Append(c).Append(this.Status);
            sb.Append(c).Append(this.CompelItemChangeFlag);
            sb.Append(c).Append(this.SortFlag);
            sb.Append(c).Append(this.SortRange);
            sb.Append(c).Append(this.MultivariateFlag);
            sb.Append(c).Append(this.CategoryCount);
            sb.Append(c).Append(this.ChildItemCount);
            sb.Append(c).Append(this.DpDataEditId);
            sb.Append(c).Append(this.ExecuteNo);
            sb.Append(c).Append(this.DpStatus);
            sb.Append(c).Append(this.NewItemName);
            sb.Append(c).Append(this.NewAnswerType);
            sb.Append(c).Append(this.NewLv1title);
            sb.Append(c).Append(this.NewLv2title);
            sb.Append(c).Append(this.DpCategoryCount);
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

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
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

        /// <summary>SOURCE_DIV: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("SOURCE_DIV")]
        public String SourceDiv {
            get { return _sourceDiv; }
            set {
                __modifiedProperties.AddPropertyName("SourceDiv");
                _sourceDiv = value;
            }
        }

        /// <summary>ITEMNO: {VARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("ITEMNO")]
        public String Itemno {
            get { return _itemno; }
            set {
                __modifiedProperties.AddPropertyName("Itemno");
                _itemno = value;
            }
        }

        /// <summary>ITEM_TYPE: {VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_TYPE")]
        public String ItemType {
            get { return _itemType; }
            set {
                __modifiedProperties.AddPropertyName("ItemType");
                _itemType = value;
            }
        }

        /// <summary>ANSWER_TYPE: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("ANSWER_TYPE")]
        public String AnswerType {
            get { return _answerType; }
            set {
                __modifiedProperties.AddPropertyName("AnswerType");
                _answerType = value;
            }
        }

        /// <summary>SORT_NUMBER: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NUMBER")]
        public int? SortNumber {
            get { return _sortNumber; }
            set {
                __modifiedProperties.AddPropertyName("SortNumber");
                _sortNumber = value;
            }
        }

        /// <summary>MATRIX_DIV: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("MATRIX_DIV")]
        public int? MatrixDiv {
            get { return _matrixDiv; }
            set {
                __modifiedProperties.AddPropertyName("MatrixDiv");
                _matrixDiv = value;
            }
        }

        /// <summary>LV1TITLE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LV1TITLE")]
        public String Lv1title {
            get { return _lv1title; }
            set {
                __modifiedProperties.AddPropertyName("Lv1title");
                _lv1title = value;
            }
        }

        /// <summary>LV2TITLE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LV2TITLE")]
        public String Lv2title {
            get { return _lv2title; }
            set {
                __modifiedProperties.AddPropertyName("Lv2title");
                _lv2title = value;
            }
        }

        /// <summary>ORIGINAL_LV1TITLE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("ORIGINAL_LV1TITLE")]
        public String OriginalLv1title {
            get { return _originalLv1title; }
            set {
                __modifiedProperties.AddPropertyName("OriginalLv1title");
                _originalLv1title = value;
            }
        }

        /// <summary>ORIGINAL_LV2TITLE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("ORIGINAL_LV2TITLE")]
        public String OriginalLv2title {
            get { return _originalLv2title; }
            set {
                __modifiedProperties.AddPropertyName("OriginalLv2title");
                _originalLv2title = value;
            }
        }

        /// <summary>CATEGORY_EDIT_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_EDIT_ID")]
        public decimal? CategoryEditId {
            get { return _categoryEditId; }
            set {
                __modifiedProperties.AddPropertyName("CategoryEditId");
                _categoryEditId = value;
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

        /// <summary>COMPEL_ITEM_CHANGE_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("COMPEL_ITEM_CHANGE_FLAG")]
        public int? CompelItemChangeFlag {
            get { return _compelItemChangeFlag; }
            set {
                __modifiedProperties.AddPropertyName("CompelItemChangeFlag");
                _compelItemChangeFlag = value;
            }
        }

        /// <summary>SORT_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("SORT_FLAG")]
        public int? SortFlag {
            get { return _sortFlag; }
            set {
                __modifiedProperties.AddPropertyName("SortFlag");
                _sortFlag = value;
            }
        }

        /// <summary>SORT_RANGE: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_RANGE")]
        public int? SortRange {
            get { return _sortRange; }
            set {
                __modifiedProperties.AddPropertyName("SortRange");
                _sortRange = value;
            }
        }

        /// <summary>MULTIVARIATE_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("MULTIVARIATE_FLAG")]
        public int? MultivariateFlag {
            get { return _multivariateFlag; }
            set {
                __modifiedProperties.AddPropertyName("MultivariateFlag");
                _multivariateFlag = value;
            }
        }

        /// <summary>CATEGORY_COUNT: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_COUNT")]
        public decimal? CategoryCount {
            get { return _categoryCount; }
            set {
                __modifiedProperties.AddPropertyName("CategoryCount");
                _categoryCount = value;
            }
        }

        /// <summary>CHILD_ITEM_COUNT: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("CHILD_ITEM_COUNT")]
        public decimal? ChildItemCount {
            get { return _childItemCount; }
            set {
                __modifiedProperties.AddPropertyName("ChildItemCount");
                _childItemCount = value;
            }
        }

        /// <summary>DP_DATA_EDIT_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DP_DATA_EDIT_ID")]
        public decimal? DpDataEditId {
            get { return _dpDataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DpDataEditId");
                _dpDataEditId = value;
            }
        }

        /// <summary>EXECUTE_NO: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("EXECUTE_NO")]
        public int? ExecuteNo {
            get { return _executeNo; }
            set {
                __modifiedProperties.AddPropertyName("ExecuteNo");
                _executeNo = value;
            }
        }

        /// <summary>DP_STATUS: {VARCHAR2(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_STATUS")]
        public String DpStatus {
            get { return _dpStatus; }
            set {
                __modifiedProperties.AddPropertyName("DpStatus");
                _dpStatus = value;
            }
        }

        /// <summary>NEW_ITEM_NAME: {VARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ITEM_NAME")]
        public String NewItemName {
            get { return _newItemName; }
            set {
                __modifiedProperties.AddPropertyName("NewItemName");
                _newItemName = value;
            }
        }

        /// <summary>NEW_ANSWER_TYPE: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ANSWER_TYPE")]
        public String NewAnswerType {
            get { return _newAnswerType; }
            set {
                __modifiedProperties.AddPropertyName("NewAnswerType");
                _newAnswerType = value;
            }
        }

        /// <summary>NEW_LV1TITLE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_LV1TITLE")]
        public String NewLv1title {
            get { return _newLv1title; }
            set {
                __modifiedProperties.AddPropertyName("NewLv1title");
                _newLv1title = value;
            }
        }

        /// <summary>NEW_LV2TITLE: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_LV2TITLE")]
        public String NewLv2title {
            get { return _newLv2title; }
            set {
                __modifiedProperties.AddPropertyName("NewLv2title");
                _newLv2title = value;
            }
        }

        /// <summary>DP_CATEGORY_COUNT: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("DP_CATEGORY_COUNT")]
        public decimal? DpCategoryCount {
            get { return _dpCategoryCount; }
            set {
                __modifiedProperties.AddPropertyName("DpCategoryCount");
                _dpCategoryCount = value;
            }
        }

        #endregion
    }
}
