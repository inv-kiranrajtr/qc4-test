

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
    /// The entity of T_OUTPUT_SETTING as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, OUTPUT_FILE_TYPE, PARTITION_FLAG, LAYOUT_FLAG, OUTPUT_TYPE, NO_ANSWER_CHAR, UNMACTH_CHAR, MULTI_ITEM_TYPE, NUMBER_TYPE
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
    ///     T_QCWEB_SURVEY_INFO, T_Output_History_Item
    /// 
    /// [referrer-table]
    ///     T_OUTPUT_HISTORY_ITEM, T_QCWEB_SURVEY_INFO
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tOutputHistoryItem, tQcwebSurveyInfoAsOne
    /// 
    /// [referrer-property]
    ///     tOutputHistoryItemList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_SETTING")]
    [System.Serializable]
    public partial class TOutputSetting : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>OUTPUT_FILE_TYPE: {NotNull, VARCHAR2(3)}</summary>
        protected String _outputFileType;

        /// <summary>PARTITION_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _partitionFlag;

        /// <summary>LAYOUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _layoutFlag;

        /// <summary>OUTPUT_TYPE: {CHAR(3)}</summary>
        protected String _outputType;

        /// <summary>NO_ANSWER_CHAR: {NotNull, CHAR(3)}</summary>
        protected String _noAnswerChar;

        /// <summary>UNMACTH_CHAR: {NotNull, CHAR(3)}</summary>
        protected String _unmacthChar;

        /// <summary>MULTI_ITEM_TYPE: {NotNull, NUMBER(1)}</summary>
        protected int? _multiItemType;

        /// <summary>NUMBER_TYPE: {NotNull, NUMBER(1)}</summary>
        protected int? _numberType;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_SETTING"; } }
        public String TablePropertyName { get { return "TOutputSetting"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag PartitionFlagAsFlag { get {
            return CDef.Flag.CodeOf(_partitionFlag);
        } set {
            PartitionFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag LayoutFlagAsFlag { get {
            return CDef.Flag.CodeOf(_layoutFlag);
        } set {
            LayoutFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of partitionFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetPartitionFlag_True() {
            PartitionFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of partitionFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetPartitionFlag_False() {
            PartitionFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of layoutFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetLayoutFlag_True() {
            LayoutFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of layoutFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetLayoutFlag_False() {
            LayoutFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of partitionFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsPartitionFlagTrue {
            get {
                CDef.Flag cls = PartitionFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of partitionFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsPartitionFlagFalse {
            get {
                CDef.Flag cls = PartitionFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of layoutFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsLayoutFlagTrue {
            get {
                CDef.Flag cls = LayoutFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of layoutFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsLayoutFlagFalse {
            get {
                CDef.Flag cls = LayoutFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String PartitionFlagName {
            get {
                CDef.Flag cls = PartitionFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String PartitionFlagAlias {
            get {
                CDef.Flag cls = PartitionFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String LayoutFlagName {
            get {
                CDef.Flag cls = LayoutFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String LayoutFlagAlias {
            get {
                CDef.Flag cls = LayoutFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TQcwebSurveyInfo _tQcwebSurveyInfo;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfo {
            get { return _tQcwebSurveyInfo; }
            set { _tQcwebSurveyInfo = value; }
        }

        protected TOutputHistoryItem _tOutputHistoryItem;

        /// <summary>T_OUTPUT_HISTORY_ITEM as 'TOutputHistoryItem'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputHistoryItem TOutputHistoryItem {
            get { return _tOutputHistoryItem; }
            set { _tOutputHistoryItem = value; }
        }

        protected TQcwebSurveyInfo _tQcwebSurveyInfoAsOne;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfoAsOne {
            get { return _tQcwebSurveyInfoAsOne; }
            set { _tQcwebSurveyInfoAsOne = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TOutputHistoryItem> _tOutputHistoryItemList;

        /// <summary>T_OUTPUT_HISTORY_ITEM as 'TOutputHistoryItemList'.</summary>
        public IList<TOutputHistoryItem> TOutputHistoryItemList {
            get { if (_tOutputHistoryItemList == null) { _tOutputHistoryItemList = new List<TOutputHistoryItem>(); } return _tOutputHistoryItemList; }
            set { _tOutputHistoryItemList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_qcwebid == null) { return false; }
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
            if (other == null || !(other is TOutputSetting)) { return false; }
            TOutputSetting otherEntity = (TOutputSetting)other;
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _qcwebid);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputSetting:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tOutputHistoryItem != null)
            { sb.Append(l).Append(xbRDS(_tOutputHistoryItem, "TOutputHistoryItem")); }
            if (_tQcwebSurveyInfoAsOne != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfoAsOne, "TQcwebSurveyInfoAsOne")); }
            if (_tOutputHistoryItemList != null) { foreach (Entity e in _tOutputHistoryItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputHistoryItemList")); } } }
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.OutputFileType);
            sb.Append(c).Append(this.PartitionFlag);
            sb.Append(c).Append(this.LayoutFlag);
            sb.Append(c).Append(this.OutputType);
            sb.Append(c).Append(this.NoAnswerChar);
            sb.Append(c).Append(this.UnmacthChar);
            sb.Append(c).Append(this.MultiItemType);
            sb.Append(c).Append(this.NumberType);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tOutputHistoryItem != null) { sb.Append(c).Append("TOutputHistoryItem"); }
            if (_tQcwebSurveyInfoAsOne != null) { sb.Append(c).Append("TQcwebSurveyInfoAsOne"); }
            if (_tOutputHistoryItemList != null && _tOutputHistoryItemList.Count > 0)
            { sb.Append(c).Append("TOutputHistoryItemList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>OUTPUT_FILE_TYPE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_FILE_TYPE")]
        public String OutputFileType {
            get { return _outputFileType; }
            set {
                __modifiedProperties.AddPropertyName("OutputFileType");
                _outputFileType = value;
            }
        }

        /// <summary>PARTITION_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("PARTITION_FLAG")]
        public int? PartitionFlag {
            get { return _partitionFlag; }
            set {
                __modifiedProperties.AddPropertyName("PartitionFlag");
                _partitionFlag = value;
            }
        }

        /// <summary>LAYOUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("LAYOUT_FLAG")]
        public int? LayoutFlag {
            get { return _layoutFlag; }
            set {
                __modifiedProperties.AddPropertyName("LayoutFlag");
                _layoutFlag = value;
            }
        }

        /// <summary>OUTPUT_TYPE: {CHAR(3)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TYPE")]
        public String OutputType {
            get { return _outputType; }
            set {
                __modifiedProperties.AddPropertyName("OutputType");
                _outputType = value;
            }
        }

        /// <summary>NO_ANSWER_CHAR: {NotNull, CHAR(3)}</summary>
        [Seasar.Dao.Attrs.Column("NO_ANSWER_CHAR")]
        public String NoAnswerChar {
            get { return _noAnswerChar; }
            set {
                __modifiedProperties.AddPropertyName("NoAnswerChar");
                _noAnswerChar = value;
            }
        }

        /// <summary>UNMACTH_CHAR: {NotNull, CHAR(3)}</summary>
        [Seasar.Dao.Attrs.Column("UNMACTH_CHAR")]
        public String UnmacthChar {
            get { return _unmacthChar; }
            set {
                __modifiedProperties.AddPropertyName("UnmacthChar");
                _unmacthChar = value;
            }
        }

        /// <summary>MULTI_ITEM_TYPE: {NotNull, NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("MULTI_ITEM_TYPE")]
        public int? MultiItemType {
            get { return _multiItemType; }
            set {
                __modifiedProperties.AddPropertyName("MultiItemType");
                _multiItemType = value;
            }
        }

        /// <summary>NUMBER_TYPE: {NotNull, NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("NUMBER_TYPE")]
        public int? NumberType {
            get { return _numberType; }
            set {
                __modifiedProperties.AddPropertyName("NumberType");
                _numberType = value;
            }
        }

        #endregion
    }
}
