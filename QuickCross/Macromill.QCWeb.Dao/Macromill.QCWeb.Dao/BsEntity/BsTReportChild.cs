

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
    /// The entity of T_REPORT_CHILD as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     REPORT_CHILD_ID
    /// 
    /// [column]
    ///     REPORT_CHILD_ID, PARENT_REPORT_ID, TARGET_SCENARIO_ITEM_ID, SORT_NO
    /// 
    /// [sequence]
    ///     T_Report_Child_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_REPORT
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tReport
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_REPORT_CHILD")]
    [System.Serializable]
    public partial class TReportChild : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>REPORT_CHILD_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _reportChildId;

        /// <summary>PARENT_REPORT_ID: {IX, NotNull, NUMBER(27), FK to T_REPORT}</summary>
        protected decimal? _parentReportId;

        /// <summary>TARGET_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _targetScenarioItemId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_REPORT_CHILD"; } }
        public String TablePropertyName { get { return "TReportChild"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TReport _tReport;

        /// <summary>T_REPORT as 'TReport'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("PARENT_REPORT_ID:REPORT_ID")]
        public TReport TReport {
            get { return _tReport; }
            set { _tReport = value; }
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
                if (_reportChildId == null) { return false; }
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
            if (other == null || !(other is TReportChild)) { return false; }
            TReportChild otherEntity = (TReportChild)other;
            if (!xSV(this.ReportChildId, otherEntity.ReportChildId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _reportChildId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TReportChild:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tReport != null)
            { sb.Append(l).Append(xbRDS(_tReport, "TReport")); }
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
            sb.Append(c).Append(this.ReportChildId);
            sb.Append(c).Append(this.ParentReportId);
            sb.Append(c).Append(this.TargetScenarioItemId);
            sb.Append(c).Append(this.SortNo);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tReport != null) { sb.Append(c).Append("TReport"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>REPORT_CHILD_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_CHILD_ID")]
        public decimal? ReportChildId {
            get { return _reportChildId; }
            set {
                __modifiedProperties.AddPropertyName("ReportChildId");
                _reportChildId = value;
            }
        }

        /// <summary>PARENT_REPORT_ID: {IX, NotNull, NUMBER(27), FK to T_REPORT}</summary>
        [Seasar.Dao.Attrs.Column("PARENT_REPORT_ID")]
        public decimal? ParentReportId {
            get { return _parentReportId; }
            set {
                __modifiedProperties.AddPropertyName("ParentReportId");
                _parentReportId = value;
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

        #endregion
    }
}
