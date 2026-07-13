

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
    /// The entity of T_SURVEY_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     SURVEY_INFO_ID
    /// 
    /// [column]
    ///     SURVEY_INFO_ID, MAIN_SURVEY_ID, SCHEDULE_DELETE_DATE, DELETE_FLAG
    /// 
    /// [sequence]
    ///     T_Survey_Info_SEQ_01
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
    ///     T_QCWEB_SURVEY_INFO
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     tQcwebSurveyInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_SURVEY_INFO")]
    [System.Serializable]
    public partial class TSurveyInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SURVEY_INFO_ID: {PK, NotNull, NUMBER(27), default=[0]}</summary>
        protected decimal? _surveyInfoId;

        /// <summary>MAIN_SURVEY_ID: {IX, NotNull, NUMBER(22)}</summary>
        protected decimal? _mainSurveyId;

        /// <summary>SCHEDULE_DELETE_DATE: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _scheduleDeleteDate;

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), default=[0], classification=DeleteFlag}</summary>
        protected int? _deleteFlag;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_SURVEY_INFO"; } }
        public String TablePropertyName { get { return "TSurveyInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.DeleteFlag DeleteFlagAsDeleteFlag { get {
            return CDef.DeleteFlag.CodeOf(_deleteFlag);
        } set {
            DeleteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of deleteFlag as True.
        /// <![CDATA[
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_True() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.True;
        }

        /// <summary>
        /// Set the value of deleteFlag as False.
        /// <![CDATA[
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_False() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of deleteFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagTrue {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagFalse {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String DeleteFlagName {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String DeleteFlagAlias {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
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
        protected IList<TQcwebSurveyInfo> _tQcwebSurveyInfoList;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoList'.</summary>
        public IList<TQcwebSurveyInfo> TQcwebSurveyInfoList {
            get { if (_tQcwebSurveyInfoList == null) { _tQcwebSurveyInfoList = new List<TQcwebSurveyInfo>(); } return _tQcwebSurveyInfoList; }
            set { _tQcwebSurveyInfoList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_surveyInfoId == null) { return false; }
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
            if (other == null || !(other is TSurveyInfo)) { return false; }
            TSurveyInfo otherEntity = (TSurveyInfo)other;
            if (!xSV(this.SurveyInfoId, otherEntity.SurveyInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _surveyInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TSurveyInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfoList != null) { foreach (Entity e in _tQcwebSurveyInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TQcwebSurveyInfoList")); } } }
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
            sb.Append(c).Append(this.SurveyInfoId);
            sb.Append(c).Append(this.MainSurveyId);
            sb.Append(c).Append(this.ScheduleDeleteDate);
            sb.Append(c).Append(this.DeleteFlag);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfoList != null && _tQcwebSurveyInfoList.Count > 0)
            { sb.Append(c).Append("TQcwebSurveyInfoList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>SURVEY_INFO_ID: {PK, NotNull, NUMBER(27), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_INFO_ID")]
        public decimal? SurveyInfoId {
            get { return _surveyInfoId; }
            set {
                __modifiedProperties.AddPropertyName("SurveyInfoId");
                _surveyInfoId = value;
            }
        }

        /// <summary>MAIN_SURVEY_ID: {IX, NotNull, NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("MAIN_SURVEY_ID")]
        public decimal? MainSurveyId {
            get { return _mainSurveyId; }
            set {
                __modifiedProperties.AddPropertyName("MainSurveyId");
                _mainSurveyId = value;
            }
        }

        /// <summary>SCHEDULE_DELETE_DATE: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("SCHEDULE_DELETE_DATE")]
        public DateTime? ScheduleDeleteDate {
            get { return _scheduleDeleteDate; }
            set {
                __modifiedProperties.AddPropertyName("ScheduleDeleteDate");
                _scheduleDeleteDate = value;
            }
        }

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), default=[0], classification=DeleteFlag}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_FLAG")]
        public int? DeleteFlag {
            get { return _deleteFlag; }
            set {
                __modifiedProperties.AddPropertyName("DeleteFlag");
                _deleteFlag = value;
            }
        }

        #endregion
    }
}
