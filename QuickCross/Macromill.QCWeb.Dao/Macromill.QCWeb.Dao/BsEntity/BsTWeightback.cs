

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
    /// The entity of T_WEIGHTBACK as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     WEIGHTBACK_ID
    /// 
    /// [column]
    ///     WEIGHTBACK_ID, WEIGHTBACK_ITEM_ID, ASSIST_CALC_FLAG, ASSIST_CALC_TYPE, QCWEBID, LAST_UPDATE_USER, LAST_UPDATE_DATETIME
    /// 
    /// [sequence]
    ///     T_Weightback_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_QCWEB_SURVEY_INFO, T_Weightback_Value
    /// 
    /// [referrer-table]
    ///     T_WEIGHTBACK_VALUE
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tWeightbackValue
    /// 
    /// [referrer-property]
    ///     tWeightbackValueList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_WEIGHTBACK")]
    [System.Serializable]
    public partial class TWeightback : EntityDefinedCommonColumn {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>WEIGHTBACK_ID: {PK, NotNull, NUMBER(27), FK to T_Weightback_Value}</summary>
        protected decimal? _weightbackId;

        /// <summary>WEIGHTBACK_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _weightbackItemId;

        /// <summary>ASSIST_CALC_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _assistCalcFlag;

        /// <summary>ASSIST_CALC_TYPE: {NotNull, VARCHAR2(1)}</summary>
        protected String _assistCalcType;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        protected String _lastUpdateUser;

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastUpdateDatetime;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();

        protected bool __canCommonColumnAutoSetup = true;
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_WEIGHTBACK"; } }
        public String TablePropertyName { get { return "TWeightback"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag AssistCalcFlagAsFlag { get {
            return CDef.Flag.CodeOf(_assistCalcFlag);
        } set {
            AssistCalcFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of assistCalcFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetAssistCalcFlag_True() {
            AssistCalcFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of assistCalcFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetAssistCalcFlag_False() {
            AssistCalcFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of assistCalcFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsAssistCalcFlagTrue {
            get {
                CDef.Flag cls = AssistCalcFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of assistCalcFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsAssistCalcFlagFalse {
            get {
                CDef.Flag cls = AssistCalcFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String AssistCalcFlagName {
            get {
                CDef.Flag cls = AssistCalcFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String AssistCalcFlagAlias {
            get {
                CDef.Flag cls = AssistCalcFlagAsFlag;
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

        protected TWeightbackValue _tWeightbackValue;

        /// <summary>T_WEIGHTBACK_VALUE as 'TWeightbackValue'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("WEIGHTBACK_ID:WEIGHTBACK_ID")]
        public TWeightbackValue TWeightbackValue {
            get { return _tWeightbackValue; }
            set { _tWeightbackValue = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TWeightbackValue> _tWeightbackValueList;

        /// <summary>T_WEIGHTBACK_VALUE as 'TWeightbackValueList'.</summary>
        public IList<TWeightbackValue> TWeightbackValueList {
            get { if (_tWeightbackValueList == null) { _tWeightbackValueList = new List<TWeightbackValue>(); } return _tWeightbackValueList; }
            set { _tWeightbackValueList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_weightbackId == null) { return false; }
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
        //                                                          Common Column Handling
        //                                                          ======================
        public virtual void EnableCommonColumnAutoSetup() {
            __canCommonColumnAutoSetup = true;
        }

        public virtual void DisableCommonColumnAutoSetup() {
            __canCommonColumnAutoSetup = false;
        }

        public virtual bool CanCommonColumnAutoSetup() {// for Framework
            return __canCommonColumnAutoSetup;
        }

        // ===============================================================================
        //                                                                  Basic Override
        //                                                                  ==============
        #region Basic Override
        public override bool Equals(Object other) {
            if (other == null || !(other is TWeightback)) { return false; }
            TWeightback otherEntity = (TWeightback)other;
            if (!xSV(this.WeightbackId, otherEntity.WeightbackId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _weightbackId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TWeightback:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tWeightbackValue != null)
            { sb.Append(l).Append(xbRDS(_tWeightbackValue, "TWeightbackValue")); }
            if (_tWeightbackValueList != null) { foreach (Entity e in _tWeightbackValueList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TWeightbackValueList")); } } }
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
            sb.Append(c).Append(this.WeightbackId);
            sb.Append(c).Append(this.WeightbackItemId);
            sb.Append(c).Append(this.AssistCalcFlag);
            sb.Append(c).Append(this.AssistCalcType);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.LastUpdateUser);
            sb.Append(c).Append(this.LastUpdateDatetime);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tWeightbackValue != null) { sb.Append(c).Append("TWeightbackValue"); }
            if (_tWeightbackValueList != null && _tWeightbackValueList.Count > 0)
            { sb.Append(c).Append("TWeightbackValueList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>WEIGHTBACK_ID: {PK, NotNull, NUMBER(27), FK to T_Weightback_Value}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_ID")]
        public decimal? WeightbackId {
            get { return _weightbackId; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackId");
                _weightbackId = value;
            }
        }

        /// <summary>WEIGHTBACK_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_ITEM_ID")]
        public decimal? WeightbackItemId {
            get { return _weightbackItemId; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackItemId");
                _weightbackItemId = value;
            }
        }

        /// <summary>ASSIST_CALC_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("ASSIST_CALC_FLAG")]
        public int? AssistCalcFlag {
            get { return _assistCalcFlag; }
            set {
                __modifiedProperties.AddPropertyName("AssistCalcFlag");
                _assistCalcFlag = value;
            }
        }

        /// <summary>ASSIST_CALC_TYPE: {NotNull, VARCHAR2(1)}</summary>
        [Seasar.Dao.Attrs.Column("ASSIST_CALC_TYPE")]
        public String AssistCalcType {
            get { return _assistCalcType; }
            set {
                __modifiedProperties.AddPropertyName("AssistCalcType");
                _assistCalcType = value;
            }
        }

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LAST_UPDATE_USER")]
        public String LastUpdateUser {
            get { return _lastUpdateUser; }
            set {
                __modifiedProperties.AddPropertyName("LastUpdateUser");
                _lastUpdateUser = value;
            }
        }

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("LAST_UPDATE_DATETIME")]
        public DateTime? LastUpdateDatetime {
            get { return _lastUpdateDatetime; }
            set {
                __modifiedProperties.AddPropertyName("LastUpdateDatetime");
                _lastUpdateDatetime = value;
            }
        }

        #endregion
    }
}
