

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
    /// The entity of T_CROSS_SCENARIO_TARGET as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     CROSS_SCENARIO_TARGET_ID
    /// 
    /// [column]
    ///     CROSS_SCENARIO_TARGET_ID, SCENARIO_TOTALIZATION_ID, SCENARIOSET_NO, SORT_NO, SC_ITEM_ID, VIEW_NAME, GRAPH_TYPE, REPORT_TYPE, VIEW_ITEM_STRING, SCENARIO_COMMENT, POLYLINE_FLAG, GRAPH_TYPE_REPORT
    /// 
    /// [sequence]
    ///     T_Cross_Scenario_Target_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_SCENARIO_TOTALIZATION
    /// 
    /// [referrer-table]
    ///     T_COLOR_SET_INFO_CROSS, T_CROSS_SCENARIO_ITEM
    /// 
    /// [foreign-property]
    ///     tScenarioTotalization
    /// 
    /// [referrer-property]
    ///     tColorSetInfoCrossList, tCrossScenarioItemList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_CROSS_SCENARIO_TARGET")]
    [System.Serializable]
    public partial class TCrossScenarioTarget : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>CROSS_SCENARIO_TARGET_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _crossScenarioTargetId;

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>SCENARIOSET_NO: {NotNull, NUMBER(2)}</summary>
        protected int? _scenariosetNo;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>SC_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        protected decimal? _scItemId;

        /// <summary>VIEW_NAME: {NotNull, NVARCHAR2(50)}</summary>
        protected String _viewName;

        /// <summary>GRAPH_TYPE: {VARCHAR2(3)}</summary>
        protected String _graphType;

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1), default=[1]}</summary>
        protected int? _reportType;

        /// <summary>VIEW_ITEM_STRING: {NCLOB(4000)}</summary>
        protected String _viewItemString;

        /// <summary>SCENARIO_COMMENT: {NCLOB(4000)}</summary>
        protected String _scenarioComment;

        /// <summary>POLYLINE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _polylineFlag;

        /// <summary>GRAPH_TYPE_REPORT: {VARCHAR2(3)}</summary>
        protected String _graphTypeReport;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_CROSS_SCENARIO_TARGET"; } }
        public String TablePropertyName { get { return "TCrossScenarioTarget"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag PolylineFlagAsFlag { get {
            return CDef.Flag.CodeOf(_polylineFlag);
        } set {
            PolylineFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of polylineFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetPolylineFlag_True() {
            PolylineFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of polylineFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetPolylineFlag_False() {
            PolylineFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of polylineFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsPolylineFlagTrue {
            get {
                CDef.Flag cls = PolylineFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of polylineFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsPolylineFlagFalse {
            get {
                CDef.Flag cls = PolylineFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String PolylineFlagName {
            get {
                CDef.Flag cls = PolylineFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String PolylineFlagAlias {
            get {
                CDef.Flag cls = PolylineFlagAsFlag;
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

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TColorSetInfoCross> _tColorSetInfoCrossList;

        /// <summary>T_COLOR_SET_INFO_CROSS as 'TColorSetInfoCrossList'.</summary>
        public IList<TColorSetInfoCross> TColorSetInfoCrossList {
            get { if (_tColorSetInfoCrossList == null) { _tColorSetInfoCrossList = new List<TColorSetInfoCross>(); } return _tColorSetInfoCrossList; }
            set { _tColorSetInfoCrossList = value; }
        }

        protected IList<TCrossScenarioItem> _tCrossScenarioItemList;

        /// <summary>T_CROSS_SCENARIO_ITEM as 'TCrossScenarioItemList'.</summary>
        public IList<TCrossScenarioItem> TCrossScenarioItemList {
            get { if (_tCrossScenarioItemList == null) { _tCrossScenarioItemList = new List<TCrossScenarioItem>(); } return _tCrossScenarioItemList; }
            set { _tCrossScenarioItemList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_crossScenarioTargetId == null) { return false; }
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
            if (other == null || !(other is TCrossScenarioTarget)) { return false; }
            TCrossScenarioTarget otherEntity = (TCrossScenarioTarget)other;
            if (!xSV(this.CrossScenarioTargetId, otherEntity.CrossScenarioTargetId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _crossScenarioTargetId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TCrossScenarioTarget:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tColorSetInfoCrossList != null) { foreach (Entity e in _tColorSetInfoCrossList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TColorSetInfoCrossList")); } } }
            if (_tCrossScenarioItemList != null) { foreach (Entity e in _tCrossScenarioItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TCrossScenarioItemList")); } } }
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
            sb.Append(c).Append(this.CrossScenarioTargetId);
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.ScenariosetNo);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.ScItemId);
            sb.Append(c).Append(this.ViewName);
            sb.Append(c).Append(this.GraphType);
            sb.Append(c).Append(this.ReportType);
            sb.Append(c).Append(this.ViewItemString);
            sb.Append(c).Append(this.ScenarioComment);
            sb.Append(c).Append(this.PolylineFlag);
            sb.Append(c).Append(this.GraphTypeReport);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tColorSetInfoCrossList != null && _tColorSetInfoCrossList.Count > 0)
            { sb.Append(c).Append("TColorSetInfoCrossList"); }
            if (_tCrossScenarioItemList != null && _tCrossScenarioItemList.Count > 0)
            { sb.Append(c).Append("TCrossScenarioItemList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>CROSS_SCENARIO_TARGET_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_SCENARIO_TARGET_ID")]
        public decimal? CrossScenarioTargetId {
            get { return _crossScenarioTargetId; }
            set {
                __modifiedProperties.AddPropertyName("CrossScenarioTargetId");
                _crossScenarioTargetId = value;
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

        /// <summary>SCENARIOSET_NO: {NotNull, NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIOSET_NO")]
        public int? ScenariosetNo {
            get { return _scenariosetNo; }
            set {
                __modifiedProperties.AddPropertyName("ScenariosetNo");
                _scenariosetNo = value;
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

        /// <summary>SC_ITEM_ID: {IX, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("SC_ITEM_ID")]
        public decimal? ScItemId {
            get { return _scItemId; }
            set {
                __modifiedProperties.AddPropertyName("ScItemId");
                _scItemId = value;
            }
        }

        /// <summary>VIEW_NAME: {NotNull, NVARCHAR2(50)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_NAME")]
        public String ViewName {
            get { return _viewName; }
            set {
                __modifiedProperties.AddPropertyName("ViewName");
                _viewName = value;
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

        /// <summary>POLYLINE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("POLYLINE_FLAG")]
        public int? PolylineFlag {
            get { return _polylineFlag; }
            set {
                __modifiedProperties.AddPropertyName("PolylineFlag");
                _polylineFlag = value;
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

        #endregion
    }
}
