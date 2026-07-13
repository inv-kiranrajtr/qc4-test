

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
    /// The entity of T_DATA_PROCESS_NEW_ITEM as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DATA_EDIT_ID
    /// 
    /// [column]
    ///     DATA_EDIT_ID, SRC_ITEM_ID, NEW_ITEM_ID, NEW_ITEM_NAME, NEW_LV1TITLE, NEW_LV2TITLE, NEW_ANSWER_TYPE, NEW_CATEGORY_COUNT, UNFIT_FLAG, CONDITION_DIV, SERIES_FLAG, UPPER_FLAG, BOTTOM_FLAG, NOANSWER_ZERO_FLAG, SELECT_METHOD, TARGET_CATEGORY_CONDITION, CALC_TYPE, FORMULA_STRING
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
    ///     T_DATA_EDIT_LIST
    /// 
    /// [referrer-table]
    ///     T_DATA_PROCESS_NEW_CATEGORY, T_DATA_PROCESS_NEW_ITEM_SRC, T_INTEG_CONDITION
    /// 
    /// [foreign-property]
    ///     tDataEditList
    /// 
    /// [referrer-property]
    ///     tDataProcessNewCategoryList, tDataProcessNewItemSrcList, tIntegConditionList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DATA_PROCESS_NEW_ITEM")]
    [System.Serializable]
    public partial class TDataProcessNewItem : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        protected decimal? _dataEditId;

        /// <summary>SRC_ITEM_ID: {IX, NUMBER(27)}</summary>
        protected decimal? _srcItemId;

        /// <summary>NEW_ITEM_ID: {IX+, NotNull, NUMBER(27)}</summary>
        protected decimal? _newItemId;

        /// <summary>NEW_ITEM_NAME: {NotNull, NVARCHAR2(26)}</summary>
        protected String _newItemName;

        /// <summary>NEW_LV1TITLE: {NVARCHAR2(1000)}</summary>
        protected String _newLv1title;

        /// <summary>NEW_LV2TITLE: {NVARCHAR2(1000)}</summary>
        protected String _newLv2title;

        /// <summary>NEW_ANSWER_TYPE: {NotNull, CHAR(1)}</summary>
        protected String _newAnswerType;

        /// <summary>NEW_CATEGORY_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _newCategoryCount;

        /// <summary>UNFIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _unfitFlag;

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1), default=[1]}</summary>
        protected String _conditionDiv;

        /// <summary>SERIES_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _seriesFlag;

        /// <summary>UPPER_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _upperFlag;

        /// <summary>BOTTOM_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _bottomFlag;

        /// <summary>NOANSWER_ZERO_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _noanswerZeroFlag;

        /// <summary>SELECT_METHOD: {NotNull, NUMBER(1), default=[1]}</summary>
        protected int? _selectMethod;

        /// <summary>TARGET_CATEGORY_CONDITION: {VARCHAR2(1000)}</summary>
        protected String _targetCategoryCondition;

        /// <summary>CALC_TYPE: {CHAR(3)}</summary>
        protected String _calcType;

        /// <summary>FORMULA_STRING: {NVARCHAR2(2000)}</summary>
        protected String _formulaString;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM"; } }
        public String TablePropertyName { get { return "TDataProcessNewItem"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag UnfitFlagAsFlag { get {
            return CDef.Flag.CodeOf(_unfitFlag);
        } set {
            UnfitFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag SeriesFlagAsFlag { get {
            return CDef.Flag.CodeOf(_seriesFlag);
        } set {
            SeriesFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag UpperFlagAsFlag { get {
            return CDef.Flag.CodeOf(_upperFlag);
        } set {
            UpperFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag BottomFlagAsFlag { get {
            return CDef.Flag.CodeOf(_bottomFlag);
        } set {
            BottomFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag NoanswerZeroFlagAsFlag { get {
            return CDef.Flag.CodeOf(_noanswerZeroFlag);
        } set {
            NoanswerZeroFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of unfitFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetUnfitFlag_True() {
            UnfitFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of unfitFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetUnfitFlag_False() {
            UnfitFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of seriesFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetSeriesFlag_True() {
            SeriesFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of seriesFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetSeriesFlag_False() {
            SeriesFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of upperFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetUpperFlag_True() {
            UpperFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of upperFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetUpperFlag_False() {
            UpperFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of bottomFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetBottomFlag_True() {
            BottomFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of bottomFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetBottomFlag_False() {
            BottomFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of noanswerZeroFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerZeroFlag_True() {
            NoanswerZeroFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of noanswerZeroFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerZeroFlag_False() {
            NoanswerZeroFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of unfitFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsUnfitFlagTrue {
            get {
                CDef.Flag cls = UnfitFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of unfitFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsUnfitFlagFalse {
            get {
                CDef.Flag cls = UnfitFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of seriesFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsSeriesFlagTrue {
            get {
                CDef.Flag cls = SeriesFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of seriesFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsSeriesFlagFalse {
            get {
                CDef.Flag cls = SeriesFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of upperFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsUpperFlagTrue {
            get {
                CDef.Flag cls = UpperFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of upperFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsUpperFlagFalse {
            get {
                CDef.Flag cls = UpperFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of bottomFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsBottomFlagTrue {
            get {
                CDef.Flag cls = BottomFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of bottomFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsBottomFlagFalse {
            get {
                CDef.Flag cls = BottomFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerZeroFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerZeroFlagTrue {
            get {
                CDef.Flag cls = NoanswerZeroFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerZeroFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerZeroFlagFalse {
            get {
                CDef.Flag cls = NoanswerZeroFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String UnfitFlagName {
            get {
                CDef.Flag cls = UnfitFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String UnfitFlagAlias {
            get {
                CDef.Flag cls = UnfitFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String SeriesFlagName {
            get {
                CDef.Flag cls = SeriesFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String SeriesFlagAlias {
            get {
                CDef.Flag cls = SeriesFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String UpperFlagName {
            get {
                CDef.Flag cls = UpperFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String UpperFlagAlias {
            get {
                CDef.Flag cls = UpperFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String BottomFlagName {
            get {
                CDef.Flag cls = BottomFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String BottomFlagAlias {
            get {
                CDef.Flag cls = BottomFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String NoanswerZeroFlagName {
            get {
                CDef.Flag cls = NoanswerZeroFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String NoanswerZeroFlagAlias {
            get {
                CDef.Flag cls = NoanswerZeroFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDataEditList _tDataEditList;

        /// <summary>T_DATA_EDIT_LIST as 'TDataEditList'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDataEditList TDataEditList {
            get { return _tDataEditList; }
            set { _tDataEditList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TDataProcessNewCategory> _tDataProcessNewCategoryList;

        /// <summary>T_DATA_PROCESS_NEW_CATEGORY as 'TDataProcessNewCategoryList'.</summary>
        public IList<TDataProcessNewCategory> TDataProcessNewCategoryList {
            get { if (_tDataProcessNewCategoryList == null) { _tDataProcessNewCategoryList = new List<TDataProcessNewCategory>(); } return _tDataProcessNewCategoryList; }
            set { _tDataProcessNewCategoryList = value; }
        }

        protected IList<TDataProcessNewItemSrc> _tDataProcessNewItemSrcList;

        /// <summary>T_DATA_PROCESS_NEW_ITEM_SRC as 'TDataProcessNewItemSrcList'.</summary>
        public IList<TDataProcessNewItemSrc> TDataProcessNewItemSrcList {
            get { if (_tDataProcessNewItemSrcList == null) { _tDataProcessNewItemSrcList = new List<TDataProcessNewItemSrc>(); } return _tDataProcessNewItemSrcList; }
            set { _tDataProcessNewItemSrcList = value; }
        }

        protected IList<TIntegCondition> _tIntegConditionList;

        /// <summary>T_INTEG_CONDITION as 'TIntegConditionList'.</summary>
        public IList<TIntegCondition> TIntegConditionList {
            get { if (_tIntegConditionList == null) { _tIntegConditionList = new List<TIntegCondition>(); } return _tIntegConditionList; }
            set { _tIntegConditionList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_dataEditId == null) { return false; }
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
            if (other == null || !(other is TDataProcessNewItem)) { return false; }
            TDataProcessNewItem otherEntity = (TDataProcessNewItem)other;
            if (!xSV(this.DataEditId, otherEntity.DataEditId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _dataEditId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDataProcessNewItem:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDataEditList != null)
            { sb.Append(l).Append(xbRDS(_tDataEditList, "TDataEditList")); }
            if (_tDataProcessNewCategoryList != null) { foreach (Entity e in _tDataProcessNewCategoryList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDataProcessNewCategoryList")); } } }
            if (_tDataProcessNewItemSrcList != null) { foreach (Entity e in _tDataProcessNewItemSrcList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDataProcessNewItemSrcList")); } } }
            if (_tIntegConditionList != null) { foreach (Entity e in _tIntegConditionList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TIntegConditionList")); } } }
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
            sb.Append(c).Append(this.DataEditId);
            sb.Append(c).Append(this.SrcItemId);
            sb.Append(c).Append(this.NewItemId);
            sb.Append(c).Append(this.NewItemName);
            sb.Append(c).Append(this.NewLv1title);
            sb.Append(c).Append(this.NewLv2title);
            sb.Append(c).Append(this.NewAnswerType);
            sb.Append(c).Append(this.NewCategoryCount);
            sb.Append(c).Append(this.UnfitFlag);
            sb.Append(c).Append(this.ConditionDiv);
            sb.Append(c).Append(this.SeriesFlag);
            sb.Append(c).Append(this.UpperFlag);
            sb.Append(c).Append(this.BottomFlag);
            sb.Append(c).Append(this.NoanswerZeroFlag);
            sb.Append(c).Append(this.SelectMethod);
            sb.Append(c).Append(this.TargetCategoryCondition);
            sb.Append(c).Append(this.CalcType);
            sb.Append(c).Append(this.FormulaString);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDataEditList != null) { sb.Append(c).Append("TDataEditList"); }
            if (_tDataProcessNewCategoryList != null && _tDataProcessNewCategoryList.Count > 0)
            { sb.Append(c).Append("TDataProcessNewCategoryList"); }
            if (_tDataProcessNewItemSrcList != null && _tDataProcessNewItemSrcList.Count > 0)
            { sb.Append(c).Append("TDataProcessNewItemSrcList"); }
            if (_tIntegConditionList != null && _tIntegConditionList.Count > 0)
            { sb.Append(c).Append("TIntegConditionList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
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

        /// <summary>NEW_ITEM_ID: {IX+, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ITEM_ID")]
        public decimal? NewItemId {
            get { return _newItemId; }
            set {
                __modifiedProperties.AddPropertyName("NewItemId");
                _newItemId = value;
            }
        }

        /// <summary>NEW_ITEM_NAME: {NotNull, NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ITEM_NAME")]
        public String NewItemName {
            get { return _newItemName; }
            set {
                __modifiedProperties.AddPropertyName("NewItemName");
                _newItemName = value;
            }
        }

        /// <summary>NEW_LV1TITLE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_LV1TITLE")]
        public String NewLv1title {
            get { return _newLv1title; }
            set {
                __modifiedProperties.AddPropertyName("NewLv1title");
                _newLv1title = value;
            }
        }

        /// <summary>NEW_LV2TITLE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_LV2TITLE")]
        public String NewLv2title {
            get { return _newLv2title; }
            set {
                __modifiedProperties.AddPropertyName("NewLv2title");
                _newLv2title = value;
            }
        }

        /// <summary>NEW_ANSWER_TYPE: {NotNull, CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ANSWER_TYPE")]
        public String NewAnswerType {
            get { return _newAnswerType; }
            set {
                __modifiedProperties.AddPropertyName("NewAnswerType");
                _newAnswerType = value;
            }
        }

        /// <summary>NEW_CATEGORY_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("NEW_CATEGORY_COUNT")]
        public int? NewCategoryCount {
            get { return _newCategoryCount; }
            set {
                __modifiedProperties.AddPropertyName("NewCategoryCount");
                _newCategoryCount = value;
            }
        }

        /// <summary>UNFIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("UNFIT_FLAG")]
        public int? UnfitFlag {
            get { return _unfitFlag; }
            set {
                __modifiedProperties.AddPropertyName("UnfitFlag");
                _unfitFlag = value;
            }
        }

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1), default=[1]}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_DIV")]
        public String ConditionDiv {
            get { return _conditionDiv; }
            set {
                __modifiedProperties.AddPropertyName("ConditionDiv");
                _conditionDiv = value;
            }
        }

        /// <summary>SERIES_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("SERIES_FLAG")]
        public int? SeriesFlag {
            get { return _seriesFlag; }
            set {
                __modifiedProperties.AddPropertyName("SeriesFlag");
                _seriesFlag = value;
            }
        }

        /// <summary>UPPER_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("UPPER_FLAG")]
        public int? UpperFlag {
            get { return _upperFlag; }
            set {
                __modifiedProperties.AddPropertyName("UpperFlag");
                _upperFlag = value;
            }
        }

        /// <summary>BOTTOM_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("BOTTOM_FLAG")]
        public int? BottomFlag {
            get { return _bottomFlag; }
            set {
                __modifiedProperties.AddPropertyName("BottomFlag");
                _bottomFlag = value;
            }
        }

        /// <summary>NOANSWER_ZERO_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_ZERO_FLAG")]
        public int? NoanswerZeroFlag {
            get { return _noanswerZeroFlag; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerZeroFlag");
                _noanswerZeroFlag = value;
            }
        }

        /// <summary>SELECT_METHOD: {NotNull, NUMBER(1), default=[1]}</summary>
        [Seasar.Dao.Attrs.Column("SELECT_METHOD")]
        public int? SelectMethod {
            get { return _selectMethod; }
            set {
                __modifiedProperties.AddPropertyName("SelectMethod");
                _selectMethod = value;
            }
        }

        /// <summary>TARGET_CATEGORY_CONDITION: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_CATEGORY_CONDITION")]
        public String TargetCategoryCondition {
            get { return _targetCategoryCondition; }
            set {
                __modifiedProperties.AddPropertyName("TargetCategoryCondition");
                _targetCategoryCondition = value;
            }
        }

        /// <summary>CALC_TYPE: {CHAR(3)}</summary>
        [Seasar.Dao.Attrs.Column("CALC_TYPE")]
        public String CalcType {
            get { return _calcType; }
            set {
                __modifiedProperties.AddPropertyName("CalcType");
                _calcType = value;
            }
        }

        /// <summary>FORMULA_STRING: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FORMULA_STRING")]
        public String FormulaString {
            get { return _formulaString; }
            set {
                __modifiedProperties.AddPropertyName("FormulaString");
                _formulaString = value;
            }
        }

        #endregion
    }
}
