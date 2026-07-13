

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
    /// The entity of T_REPORT as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     REPORT_ID
    /// 
    /// [column]
    ///     REPORT_ID, REPORTSET_ID, TARGET_SCENARIO_ITEM_ID, SORT_NO, CHILD_DIV, SCENARIO_TYPE
    /// 
    /// [sequence]
    ///     T_Report_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_REPORTSET, T_Report_Child
    /// 
    /// [referrer-table]
    ///     T_REPORT_CHILD
    /// 
    /// [foreign-property]
    ///     tReportset, tReportChild
    /// 
    /// [referrer-property]
    ///     tReportChildList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_REPORT")]
    [System.Serializable]
    public partial class TReport : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>REPORT_ID: {PK, NotNull, NUMBER(27), FK to T_Report_Child}</summary>
        protected decimal? _reportId;

        /// <summary>REPORTSET_ID: {IX, NotNull, NUMBER(27), FK to T_REPORTSET}</summary>
        protected decimal? _reportsetId;

        /// <summary>TARGET_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _targetScenarioItemId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>CHILD_DIV: {NotNull, NUMBER(1)}</summary>
        protected int? _childDiv;

        /// <summary>SCENARIO_TYPE: {NotNull, CHAR(1)}</summary>
        protected String _scenarioType;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_REPORT"; } }
        public String TablePropertyName { get { return "TReport"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TReportset _tReportset;

        /// <summary>T_REPORTSET as 'TReportset'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("REPORTSET_ID:REPORTSET_ID")]
        public TReportset TReportset {
            get { return _tReportset; }
            set { _tReportset = value; }
        }

        protected TReportChild _tReportChild;

        /// <summary>T_REPORT_CHILD as 'TReportChild'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("REPORT_ID:PARENT_REPORT_ID")]
        public TReportChild TReportChild {
            get { return _tReportChild; }
            set { _tReportChild = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TReportChild> _tReportChildList;

        /// <summary>T_REPORT_CHILD as 'TReportChildList'.</summary>
        public IList<TReportChild> TReportChildList {
            get { if (_tReportChildList == null) { _tReportChildList = new List<TReportChild>(); } return _tReportChildList; }
            set { _tReportChildList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_reportId == null) { return false; }
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
            if (other == null || !(other is TReport)) { return false; }
            TReport otherEntity = (TReport)other;
            if (!xSV(this.ReportId, otherEntity.ReportId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _reportId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TReport:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tReportset != null)
            { sb.Append(l).Append(xbRDS(_tReportset, "TReportset")); }
            if (_tReportChild != null)
            { sb.Append(l).Append(xbRDS(_tReportChild, "TReportChild")); }
            if (_tReportChildList != null) { foreach (Entity e in _tReportChildList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TReportChildList")); } } }
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
            sb.Append(c).Append(this.ReportId);
            sb.Append(c).Append(this.ReportsetId);
            sb.Append(c).Append(this.TargetScenarioItemId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.ChildDiv);
            sb.Append(c).Append(this.ScenarioType);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tReportset != null) { sb.Append(c).Append("TReportset"); }
            if (_tReportChild != null) { sb.Append(c).Append("TReportChild"); }
            if (_tReportChildList != null && _tReportChildList.Count > 0)
            { sb.Append(c).Append("TReportChildList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>REPORT_ID: {PK, NotNull, NUMBER(27), FK to T_Report_Child}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_ID")]
        public decimal? ReportId {
            get { return _reportId; }
            set {
                __modifiedProperties.AddPropertyName("ReportId");
                _reportId = value;
            }
        }

        /// <summary>REPORTSET_ID: {IX, NotNull, NUMBER(27), FK to T_REPORTSET}</summary>
        [Seasar.Dao.Attrs.Column("REPORTSET_ID")]
        public decimal? ReportsetId {
            get { return _reportsetId; }
            set {
                __modifiedProperties.AddPropertyName("ReportsetId");
                _reportsetId = value;
            }
        }

        /// <summary>TARGET_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_SCENARIO_ITEM_ID")]
        public decimal? TargetScenarioItemId {
            get { return _targetScenarioItemId; }
            set {
                __modifiedProperties.AddPropertyName("TargetScenarioItemId");
                _targetScenarioItemId = value;
            }
        }

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public int? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>CHILD_DIV: {NotNull, NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("CHILD_DIV")]
        public int? ChildDiv {
            get { return _childDiv; }
            set {
                __modifiedProperties.AddPropertyName("ChildDiv");
                _childDiv = value;
            }
        }

        /// <summary>SCENARIO_TYPE: {NotNull, CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TYPE")]
        public String ScenarioType {
            get { return _scenarioType; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioType");
                _scenarioType = value;
            }
        }

        #endregion
    }
}
