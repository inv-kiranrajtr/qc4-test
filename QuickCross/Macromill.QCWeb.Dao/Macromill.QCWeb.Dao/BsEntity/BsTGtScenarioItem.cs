

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
    /// The entity of T_GT_SCENARIO_ITEM as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     GT_SCENARIO_ITEM_ID
    /// 
    /// [column]
    ///     GT_SCENARIO_ITEM_ID, SCENARIO_TOTALIZATION_ID, SORT_NO, ITEM_INFO_ID, SCENARIO_NAME, GRAPH_TYPE, REPORT_TYPE, VIEW_ITEM_STRING, SCENARIO_COMMENT, SURVEY_TYPE, GRAPH_TYPE_REPORT, TEST_TARGET_TYPE
    /// 
    /// [sequence]
    ///     T_GT_Scenario_Item_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_SCENARIO_TOTALIZATION, T_ITEM_INFO
    /// 
    /// [referrer-table]
    ///     T_COLOR_SET_INFO_GT
    /// 
    /// [foreign-property]
    ///     tScenarioTotalization, tItemInfo
    /// 
    /// [referrer-property]
    ///     tColorSetInfoGtList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_GT_SCENARIO_ITEM")]
    [System.Serializable]
    public partial class TGtScenarioItem : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>GT_SCENARIO_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _gtScenarioItemId;

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _itemInfoId;

        /// <summary>SCENARIO_NAME: {NotNull, NVARCHAR2(26)}</summary>
        protected String _scenarioName;

        /// <summary>GRAPH_TYPE: {VARCHAR2(3)}</summary>
        protected String _graphType;

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1), default=[1]}</summary>
        protected int? _reportType;

        /// <summary>VIEW_ITEM_STRING: {NCLOB(4000)}</summary>
        protected String _viewItemString;

        /// <summary>SCENARIO_COMMENT: {NCLOB(4000)}</summary>
        protected String _scenarioComment;

        /// <summary>SURVEY_TYPE: {NUMBER(3)}</summary>
        protected int? _surveyType;

        /// <summary>GRAPH_TYPE_REPORT: {VARCHAR2(3)}</summary>
        protected String _graphTypeReport;

        /// <summary>TEST_TARGET_TYPE: {NUMBER(1)}</summary>
        protected int? _testTargetType;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_GT_SCENARIO_ITEM"; } }
        public String TablePropertyName { get { return "TGtScenarioItem"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

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

        protected TItemInfo _tItemInfo;

        /// <summary>T_ITEM_INFO as 'TItemInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:ITEM_INFO_ID")]
        public TItemInfo TItemInfo {
            get { return _tItemInfo; }
            set { _tItemInfo = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TColorSetInfoGt> _tColorSetInfoGtList;

        /// <summary>T_COLOR_SET_INFO_GT as 'TColorSetInfoGtList'.</summary>
        public IList<TColorSetInfoGt> TColorSetInfoGtList {
            get { if (_tColorSetInfoGtList == null) { _tColorSetInfoGtList = new List<TColorSetInfoGt>(); } return _tColorSetInfoGtList; }
            set { _tColorSetInfoGtList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_gtScenarioItemId == null) { return false; }
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
            if (other == null || !(other is TGtScenarioItem)) { return false; }
            TGtScenarioItem otherEntity = (TGtScenarioItem)other;
            if (!xSV(this.GtScenarioItemId, otherEntity.GtScenarioItemId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _gtScenarioItemId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TGtScenarioItem:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tItemInfo != null)
            { sb.Append(l).Append(xbRDS(_tItemInfo, "TItemInfo")); }
            if (_tColorSetInfoGtList != null) { foreach (Entity e in _tColorSetInfoGtList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TColorSetInfoGtList")); } } }
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
            sb.Append(c).Append(this.GtScenarioItemId);
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.ScenarioName);
            sb.Append(c).Append(this.GraphType);
            sb.Append(c).Append(this.ReportType);
            sb.Append(c).Append(this.ViewItemString);
            sb.Append(c).Append(this.ScenarioComment);
            sb.Append(c).Append(this.SurveyType);
            sb.Append(c).Append(this.GraphTypeReport);
            sb.Append(c).Append(this.TestTargetType);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tItemInfo != null) { sb.Append(c).Append("TItemInfo"); }
            if (_tColorSetInfoGtList != null && _tColorSetInfoGtList.Count > 0)
            { sb.Append(c).Append("TColorSetInfoGtList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>GT_SCENARIO_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("GT_SCENARIO_ITEM_ID")]
        public decimal? GtScenarioItemId {
            get { return _gtScenarioItemId; }
            set {
                __modifiedProperties.AddPropertyName("GtScenarioItemId");
                _gtScenarioItemId = value;
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

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public int? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_INFO_ID")]
        public decimal? ItemInfoId {
            get { return _itemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ItemInfoId");
                _itemInfoId = value;
            }
        }

        /// <summary>SCENARIO_NAME: {NotNull, NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_NAME")]
        public String ScenarioName {
            get { return _scenarioName; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioName");
                _scenarioName = value;
            }
        }

        /// <summary>GRAPH_TYPE: {VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE")]
        public String GraphType {
            get { return _graphType; }
            set {
                __modifiedProperties.AddPropertyName("GraphType");
                _graphType = value;
            }
        }

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1), default=[1]}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_TYPE")]
        public int? ReportType {
            get { return _reportType; }
            set {
                __modifiedProperties.AddPropertyName("ReportType");
                _reportType = value;
            }
        }

        /// <summary>VIEW_ITEM_STRING: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_ITEM_STRING")]
        public String ViewItemString {
            get { return _viewItemString; }
            set {
                __modifiedProperties.AddPropertyName("ViewItemString");
                _viewItemString = value;
            }
        }

        /// <summary>SCENARIO_COMMENT: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_COMMENT")]
        public String ScenarioComment {
            get { return _scenarioComment; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioComment");
                _scenarioComment = value;
            }
        }

        /// <summary>SURVEY_TYPE: {NUMBER(3)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_TYPE")]
        public int? SurveyType {
            get { return _surveyType; }
            set {
                __modifiedProperties.AddPropertyName("SurveyType");
                _surveyType = value;
            }
        }

        /// <summary>GRAPH_TYPE_REPORT: {VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_REPORT")]
        public String GraphTypeReport {
            get { return _graphTypeReport; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeReport");
                _graphTypeReport = value;
            }
        }

        /// <summary>TEST_TARGET_TYPE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_TARGET_TYPE")]
        public int? TestTargetType {
            get { return _testTargetType; }
            set {
                __modifiedProperties.AddPropertyName("TestTargetType");
                _testTargetType = value;
            }
        }

        #endregion
    }
}
