

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
    /// The entity of T_CATEGORY_OUTPUT_EDIT as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     CATEGORY_OUTPUT_EDIT_ID
    /// 
    /// [column]
    ///     CATEGORY_OUTPUT_EDIT_ID, SCENARIO_TOTALIZATION_ID, OLD_ITEM_ID, NEW_ITEM_ID, TOP_FLAG, TOP_COUNT, TOP_NAME, BOTTOM_FLAG, BOTTOM_COUNT, BOTTOM_NAME
    /// 
    /// [sequence]
    ///     T_Category_Output_Edit_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_SCENARIO_TOTALIZATION, T_Category_Output_Detail
    /// 
    /// [referrer-table]
    ///     T_CATEGORY_OUTPUT_DETAIL
    /// 
    /// [foreign-property]
    ///     tScenarioTotalization, tCategoryOutputDetail
    /// 
    /// [referrer-property]
    ///     tCategoryOutputDetailList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_CATEGORY_OUTPUT_EDIT")]
    [System.Serializable]
    public partial class TCategoryOutputEdit : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>CATEGORY_OUTPUT_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_Category_Output_Detail}</summary>
        protected decimal? _categoryOutputEditId;

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>OLD_ITEM_ID: {NotNull, NUMBER(27)}</summary>
        protected decimal? _oldItemId;

        /// <summary>NEW_ITEM_ID: {NotNull, NUMBER(27)}</summary>
        protected decimal? _newItemId;

        /// <summary>TOP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _topFlag;

        /// <summary>TOP_COUNT: {NUMBER(3)}</summary>
        protected int? _topCount;

        /// <summary>TOP_NAME: {NVARCHAR2(100)}</summary>
        protected String _topName;

        /// <summary>BOTTOM_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _bottomFlag;

        /// <summary>BOTTOM_COUNT: {NUMBER(3)}</summary>
        protected int? _bottomCount;

        /// <summary>BOTTOM_NAME: {NVARCHAR2(100)}</summary>
        protected String _bottomName;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_CATEGORY_OUTPUT_EDIT"; } }
        public String TablePropertyName { get { return "TCategoryOutputEdit"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag TopFlagAsFlag { get {
            return CDef.Flag.CodeOf(_topFlag);
        } set {
            TopFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag BottomFlagAsFlag { get {
            return CDef.Flag.CodeOf(_bottomFlag);
        } set {
            BottomFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of topFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTopFlag_True() {
            TopFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of topFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTopFlag_False() {
            TopFlagAsFlag = CDef.Flag.False;
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

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of topFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTopFlagTrue {
            get {
                CDef.Flag cls = TopFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of topFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTopFlagFalse {
            get {
                CDef.Flag cls = TopFlagAsFlag;
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

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String TopFlagName {
            get {
                CDef.Flag cls = TopFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TopFlagAlias {
            get {
                CDef.Flag cls = TopFlagAsFlag;
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

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TScenarioTotalization _tScenarioTotalization;

        /// <summary>T_SCENARIO_TOTALIZATION as 'TScenarioTotalization'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TScenarioTotalization TScenarioTotalization {
            get { return _tScenarioTotalization; }
            set { _tScenarioTotalization = value; }
        }

        protected TCategoryOutputDetail _tCategoryOutputDetail;

        /// <summary>T_CATEGORY_OUTPUT_DETAIL as 'TCategoryOutputDetail'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("CATEGORY_OUTPUT_EDIT_ID:CATEGORY_OUTPUT_EDIT_ID")]
        public TCategoryOutputDetail TCategoryOutputDetail {
            get { return _tCategoryOutputDetail; }
            set { _tCategoryOutputDetail = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TCategoryOutputDetail> _tCategoryOutputDetailList;

        /// <summary>T_CATEGORY_OUTPUT_DETAIL as 'TCategoryOutputDetailList'.</summary>
        public IList<TCategoryOutputDetail> TCategoryOutputDetailList {
            get { if (_tCategoryOutputDetailList == null) { _tCategoryOutputDetailList = new List<TCategoryOutputDetail>(); } return _tCategoryOutputDetailList; }
            set { _tCategoryOutputDetailList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_categoryOutputEditId == null) { return false; }
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
            if (other == null || !(other is TCategoryOutputEdit)) { return false; }
            TCategoryOutputEdit otherEntity = (TCategoryOutputEdit)other;
            if (!xSV(this.CategoryOutputEditId, otherEntity.CategoryOutputEditId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _categoryOutputEditId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TCategoryOutputEdit:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tCategoryOutputDetail != null)
            { sb.Append(l).Append(xbRDS(_tCategoryOutputDetail, "TCategoryOutputDetail")); }
            if (_tCategoryOutputDetailList != null) { foreach (Entity e in _tCategoryOutputDetailList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TCategoryOutputDetailList")); } } }
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
            sb.Append(c).Append(this.CategoryOutputEditId);
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.OldItemId);
            sb.Append(c).Append(this.NewItemId);
            sb.Append(c).Append(this.TopFlag);
            sb.Append(c).Append(this.TopCount);
            sb.Append(c).Append(this.TopName);
            sb.Append(c).Append(this.BottomFlag);
            sb.Append(c).Append(this.BottomCount);
            sb.Append(c).Append(this.BottomName);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tCategoryOutputDetail != null) { sb.Append(c).Append("TCategoryOutputDetail"); }
            if (_tCategoryOutputDetailList != null && _tCategoryOutputDetailList.Count > 0)
            { sb.Append(c).Append("TCategoryOutputDetailList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>CATEGORY_OUTPUT_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_Category_Output_Detail}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_OUTPUT_EDIT_ID")]
        public decimal? CategoryOutputEditId {
            get { return _categoryOutputEditId; }
            set {
                __modifiedProperties.AddPropertyName("CategoryOutputEditId");
                _categoryOutputEditId = value;
            }
        }

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TOTALIZATION_ID")]
        public decimal? ScenarioTotalizationId {
            get { return _scenarioTotalizationId; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioTotalizationId");
                _scenarioTotalizationId = value;
            }
        }

        /// <summary>OLD_ITEM_ID: {NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OLD_ITEM_ID")]
        public decimal? OldItemId {
            get { return _oldItemId; }
            set {
                __modifiedProperties.AddPropertyName("OldItemId");
                _oldItemId = value;
            }
        }

        /// <summary>NEW_ITEM_ID: {NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ITEM_ID")]
        public decimal? NewItemId {
            get { return _newItemId; }
            set {
                __modifiedProperties.AddPropertyName("NewItemId");
                _newItemId = value;
            }
        }

        /// <summary>TOP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TOP_FLAG")]
        public int? TopFlag {
            get { return _topFlag; }
            set {
                __modifiedProperties.AddPropertyName("TopFlag");
                _topFlag = value;
            }
        }

        /// <summary>TOP_COUNT: {NUMBER(3)}</summary>
        [Seasar.Dao.Attrs.Column("TOP_COUNT")]
        public int? TopCount {
            get { return _topCount; }
            set {
                __modifiedProperties.AddPropertyName("TopCount");
                _topCount = value;
            }
        }

        /// <summary>TOP_NAME: {NVARCHAR2(100)}</summary>
        [Seasar.Dao.Attrs.Column("TOP_NAME")]
        public String TopName {
            get { return _topName; }
            set {
                __modifiedProperties.AddPropertyName("TopName");
                _topName = value;
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

        /// <summary>BOTTOM_COUNT: {NUMBER(3)}</summary>
        [Seasar.Dao.Attrs.Column("BOTTOM_COUNT")]
        public int? BottomCount {
            get { return _bottomCount; }
            set {
                __modifiedProperties.AddPropertyName("BottomCount");
                _bottomCount = value;
            }
        }

        /// <summary>BOTTOM_NAME: {NVARCHAR2(100)}</summary>
        [Seasar.Dao.Attrs.Column("BOTTOM_NAME")]
        public String BottomName {
            get { return _bottomName; }
            set {
                __modifiedProperties.AddPropertyName("BottomName");
                _bottomName = value;
            }
        }

        #endregion
    }
}
