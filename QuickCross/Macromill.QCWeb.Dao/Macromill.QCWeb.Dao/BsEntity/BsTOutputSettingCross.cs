

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
    /// The entity of T_OUTPUT_SETTING_CROSS as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, OUTPUT_TYPE, CROSS_NP_FLAG, CROSS_N_FLAG, CROSS_P_FLAG, PAGE_SETTING_NP_FLAG, PAGE_SETTING_N_FLAG, PAGE_SETTING_P_FLAG, PAGE_SETTING_PAPER_SIZE, PAGE_SETTING_PAPER_ORIENTATION
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
    ///     T_QCWEB_SURVEY_INFO
    /// 
    /// [referrer-table]
    ///     T_QCWEB_SURVEY_INFO
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tQcwebSurveyInfoAsOne
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_SETTING_CROSS")]
    [System.Serializable]
    public partial class TOutputSettingCross : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>OUTPUT_TYPE: {NotNull, NUMBER(1), default=[0]}</summary>
        protected int? _outputType;

        /// <summary>CROSS_NP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _crossNpFlag;

        /// <summary>CROSS_N_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _crossNFlag;

        /// <summary>CROSS_P_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _crossPFlag;

        /// <summary>PAGE_SETTING_NP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _pageSettingNpFlag;

        /// <summary>PAGE_SETTING_N_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _pageSettingNFlag;

        /// <summary>PAGE_SETTING_P_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _pageSettingPFlag;

        /// <summary>PAGE_SETTING_PAPER_SIZE: {NUMBER(2)}</summary>
        protected int? _pageSettingPaperSize;

        /// <summary>PAGE_SETTING_PAPER_ORIENTATION: {NUMBER(1)}</summary>
        protected int? _pageSettingPaperOrientation;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_SETTING_CROSS"; } }
        public String TablePropertyName { get { return "TOutputSettingCross"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag CrossNpFlagAsFlag { get {
            return CDef.Flag.CodeOf(_crossNpFlag);
        } set {
            CrossNpFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag CrossNFlagAsFlag { get {
            return CDef.Flag.CodeOf(_crossNFlag);
        } set {
            CrossNFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag CrossPFlagAsFlag { get {
            return CDef.Flag.CodeOf(_crossPFlag);
        } set {
            CrossPFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag PageSettingNpFlagAsFlag { get {
            return CDef.Flag.CodeOf(_pageSettingNpFlag);
        } set {
            PageSettingNpFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag PageSettingNFlagAsFlag { get {
            return CDef.Flag.CodeOf(_pageSettingNFlag);
        } set {
            PageSettingNFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag PageSettingPFlagAsFlag { get {
            return CDef.Flag.CodeOf(_pageSettingPFlag);
        } set {
            PageSettingPFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of crossNpFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCrossNpFlag_True() {
            CrossNpFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of crossNpFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCrossNpFlag_False() {
            CrossNpFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of crossNFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCrossNFlag_True() {
            CrossNFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of crossNFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCrossNFlag_False() {
            CrossNFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of crossPFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCrossPFlag_True() {
            CrossPFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of crossPFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCrossPFlag_False() {
            CrossPFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of pageSettingNpFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetPageSettingNpFlag_True() {
            PageSettingNpFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of pageSettingNpFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetPageSettingNpFlag_False() {
            PageSettingNpFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of pageSettingNFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetPageSettingNFlag_True() {
            PageSettingNFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of pageSettingNFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetPageSettingNFlag_False() {
            PageSettingNFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of pageSettingPFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetPageSettingPFlag_True() {
            PageSettingPFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of pageSettingPFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetPageSettingPFlag_False() {
            PageSettingPFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of crossNpFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCrossNpFlagTrue {
            get {
                CDef.Flag cls = CrossNpFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of crossNpFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCrossNpFlagFalse {
            get {
                CDef.Flag cls = CrossNpFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of crossNFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCrossNFlagTrue {
            get {
                CDef.Flag cls = CrossNFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of crossNFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCrossNFlagFalse {
            get {
                CDef.Flag cls = CrossNFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of crossPFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCrossPFlagTrue {
            get {
                CDef.Flag cls = CrossPFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of crossPFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCrossPFlagFalse {
            get {
                CDef.Flag cls = CrossPFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of pageSettingNpFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsPageSettingNpFlagTrue {
            get {
                CDef.Flag cls = PageSettingNpFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of pageSettingNpFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsPageSettingNpFlagFalse {
            get {
                CDef.Flag cls = PageSettingNpFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of pageSettingNFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsPageSettingNFlagTrue {
            get {
                CDef.Flag cls = PageSettingNFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of pageSettingNFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsPageSettingNFlagFalse {
            get {
                CDef.Flag cls = PageSettingNFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of pageSettingPFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsPageSettingPFlagTrue {
            get {
                CDef.Flag cls = PageSettingPFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of pageSettingPFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsPageSettingPFlagFalse {
            get {
                CDef.Flag cls = PageSettingPFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String CrossNpFlagName {
            get {
                CDef.Flag cls = CrossNpFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CrossNpFlagAlias {
            get {
                CDef.Flag cls = CrossNpFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String CrossNFlagName {
            get {
                CDef.Flag cls = CrossNFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CrossNFlagAlias {
            get {
                CDef.Flag cls = CrossNFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String CrossPFlagName {
            get {
                CDef.Flag cls = CrossPFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CrossPFlagAlias {
            get {
                CDef.Flag cls = CrossPFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String PageSettingNpFlagName {
            get {
                CDef.Flag cls = PageSettingNpFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String PageSettingNpFlagAlias {
            get {
                CDef.Flag cls = PageSettingNpFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String PageSettingNFlagName {
            get {
                CDef.Flag cls = PageSettingNFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String PageSettingNFlagAlias {
            get {
                CDef.Flag cls = PageSettingNFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String PageSettingPFlagName {
            get {
                CDef.Flag cls = PageSettingPFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String PageSettingPFlagAlias {
            get {
                CDef.Flag cls = PageSettingPFlagAsFlag;
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

        protected TQcwebSurveyInfo _tQcwebSurveyInfoAsOne;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfoAsOne {
            get { return _tQcwebSurveyInfoAsOne; }
            set { _tQcwebSurveyInfoAsOne = value; }
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
            if (other == null || !(other is TOutputSettingCross)) { return false; }
            TOutputSettingCross otherEntity = (TOutputSettingCross)other;
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
            return "TOutputSettingCross:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tQcwebSurveyInfoAsOne != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfoAsOne, "TQcwebSurveyInfoAsOne")); }
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
            sb.Append(c).Append(this.OutputType);
            sb.Append(c).Append(this.CrossNpFlag);
            sb.Append(c).Append(this.CrossNFlag);
            sb.Append(c).Append(this.CrossPFlag);
            sb.Append(c).Append(this.PageSettingNpFlag);
            sb.Append(c).Append(this.PageSettingNFlag);
            sb.Append(c).Append(this.PageSettingPFlag);
            sb.Append(c).Append(this.PageSettingPaperSize);
            sb.Append(c).Append(this.PageSettingPaperOrientation);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tQcwebSurveyInfoAsOne != null) { sb.Append(c).Append("TQcwebSurveyInfoAsOne"); }
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

        /// <summary>OUTPUT_TYPE: {NotNull, NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TYPE")]
        public int? OutputType {
            get { return _outputType; }
            set {
                __modifiedProperties.AddPropertyName("OutputType");
                _outputType = value;
            }
        }

        /// <summary>CROSS_NP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_NP_FLAG")]
        public int? CrossNpFlag {
            get { return _crossNpFlag; }
            set {
                __modifiedProperties.AddPropertyName("CrossNpFlag");
                _crossNpFlag = value;
            }
        }

        /// <summary>CROSS_N_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_N_FLAG")]
        public int? CrossNFlag {
            get { return _crossNFlag; }
            set {
                __modifiedProperties.AddPropertyName("CrossNFlag");
                _crossNFlag = value;
            }
        }

        /// <summary>CROSS_P_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_P_FLAG")]
        public int? CrossPFlag {
            get { return _crossPFlag; }
            set {
                __modifiedProperties.AddPropertyName("CrossPFlag");
                _crossPFlag = value;
            }
        }

        /// <summary>PAGE_SETTING_NP_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_NP_FLAG")]
        public int? PageSettingNpFlag {
            get { return _pageSettingNpFlag; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingNpFlag");
                _pageSettingNpFlag = value;
            }
        }

        /// <summary>PAGE_SETTING_N_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_N_FLAG")]
        public int? PageSettingNFlag {
            get { return _pageSettingNFlag; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingNFlag");
                _pageSettingNFlag = value;
            }
        }

        /// <summary>PAGE_SETTING_P_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_P_FLAG")]
        public int? PageSettingPFlag {
            get { return _pageSettingPFlag; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingPFlag");
                _pageSettingPFlag = value;
            }
        }

        /// <summary>PAGE_SETTING_PAPER_SIZE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_PAPER_SIZE")]
        public int? PageSettingPaperSize {
            get { return _pageSettingPaperSize; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingPaperSize");
                _pageSettingPaperSize = value;
            }
        }

        /// <summary>PAGE_SETTING_PAPER_ORIENTATION: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("PAGE_SETTING_PAPER_ORIENTATION")]
        public int? PageSettingPaperOrientation {
            get { return _pageSettingPaperOrientation; }
            set {
                __modifiedProperties.AddPropertyName("PageSettingPaperOrientation");
                _pageSettingPaperOrientation = value;
            }
        }

        #endregion
    }
}
