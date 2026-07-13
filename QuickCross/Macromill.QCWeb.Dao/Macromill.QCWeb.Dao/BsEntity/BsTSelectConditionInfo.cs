

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
    /// The entity of T_SELECT_CONDITION_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     SELECT_NO, QCWEBID
    /// 
    /// [column]
    ///     SELECT_NO, QCWEBID, QUESTION_NO, CHILD_QUESTION_NO, SELECT_CONDITION
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
    [Seasar.Dao.Attrs.Table("T_SELECT_CONDITION_INFO")]
    [System.Serializable]
    public partial class TSelectConditionInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SELECT_NO: {PK, NotNull, NUMBER(10)}</summary>
        protected long? _selectNo;

        /// <summary>QCWEBID: {PK, IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>QUESTION_NO: {NVARCHAR2(26)}</summary>
        protected String _questionNo;

        /// <summary>CHILD_QUESTION_NO: {NVARCHAR2(26)}</summary>
        protected String _childQuestionNo;

        /// <summary>SELECT_CONDITION: {NCLOB(4000)}</summary>
        protected String _selectCondition;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_SELECT_CONDITION_INFO"; } }
        public String TablePropertyName { get { return "TSelectConditionInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

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
                if (_selectNo == null) { return false; }
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
            if (other == null || !(other is TSelectConditionInfo)) { return false; }
            TSelectConditionInfo otherEntity = (TSelectConditionInfo)other;
            if (!xSV(this.SelectNo, otherEntity.SelectNo)) { return false; }
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
            result = xCH(result, _selectNo);
            result = xCH(result, _qcwebid);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TSelectConditionInfo:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.SelectNo);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.QuestionNo);
            sb.Append(c).Append(this.ChildQuestionNo);
            sb.Append(c).Append(this.SelectCondition);
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
        /// <summary>SELECT_NO: {PK, NotNull, NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("SELECT_NO")]
        public long? SelectNo {
            get { return _selectNo; }
            set {
                __modifiedProperties.AddPropertyName("SelectNo");
                _selectNo = value;
            }
        }

        /// <summary>QCWEBID: {PK, IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>QUESTION_NO: {NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("QUESTION_NO")]
        public String QuestionNo {
            get { return _questionNo; }
            set {
                __modifiedProperties.AddPropertyName("QuestionNo");
                _questionNo = value;
            }
        }

        /// <summary>CHILD_QUESTION_NO: {NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("CHILD_QUESTION_NO")]
        public String ChildQuestionNo {
            get { return _childQuestionNo; }
            set {
                __modifiedProperties.AddPropertyName("ChildQuestionNo");
                _childQuestionNo = value;
            }
        }

        /// <summary>SELECT_CONDITION: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("SELECT_CONDITION")]
        public String SelectCondition {
            get { return _selectCondition; }
            set {
                __modifiedProperties.AddPropertyName("SelectCondition");
                _selectCondition = value;
            }
        }

        #endregion
    }
}
