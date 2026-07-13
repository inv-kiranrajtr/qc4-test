

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
    /// The entity of T_CROSS_SCENARIO_ITEM as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     CROSS_SCENARIO_ITEM_ID
    /// 
    /// [column]
    ///     CROSS_SCENARIO_ITEM_ID, CROSS_SCENARIO_TARGET_ID, SORT_NO, AXIS1_ITEM_ID, AXIS2_ITEM_ID, VIEW_ITEM_NAME, GRAPH_TYPE, REPORT_TYPE, TITLE_STRING, SCENARIO_COMMENT
    /// 
    /// [sequence]
    ///     T_Cross_Scenario_Item_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_CROSS_SCENARIO_TARGET, T_Polyline_Category_List
    /// 
    /// [referrer-table]
    ///     T_POLYLINE_CATEGORY_LIST
    /// 
    /// [foreign-property]
    ///     tCrossScenarioTarget, tPolylineCategoryList
    /// 
    /// [referrer-property]
    ///     tPolylineCategoryListList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_CROSS_SCENARIO_ITEM")]
    [System.Serializable]
    public partial class TCrossScenarioItem : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>CROSS_SCENARIO_ITEM_ID: {PK, NotNull, NUMBER(27), FK to T_Polyline_Category_List}</summary>
        protected decimal? _crossScenarioItemId;

        /// <summary>CROSS_SCENARIO_TARGET_ID: {IX, NotNull, NUMBER(27), FK to T_CROSS_SCENARIO_TARGET}</summary>
        protected decimal? _crossScenarioTargetId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>AXIS1_ITEM_ID: {IX, NUMBER(27)}</summary>
        protected decimal? _axis1ItemId;

        /// <summary>AXIS2_ITEM_ID: {IX, NUMBER(27)}</summary>
        protected decimal? _axis2ItemId;

        /// <summary>VIEW_ITEM_NAME: {NVARCHAR2(100)}</summary>
        protected String _viewItemName;

        /// <summary>GRAPH_TYPE: {VARCHAR2(3)}</summary>
        protected String _graphType;

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1), default=[0]}</summary>
        protected int? _reportType;

        /// <summary>TITLE_STRING: {NCLOB(4000)}</summary>
        protected String _titleString;

        /// <summary>SCENARIO_COMMENT: {NCLOB(4000)}</summary>
        protected String _scenarioComment;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_CROSS_SCENARIO_ITEM"; } }
        public String TablePropertyName { get { return "TCrossScenarioItem"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TCrossScenarioTarget _tCrossScenarioTarget;

        /// <summary>T_CROSS_SCENARIO_TARGET as 'TCrossScenarioTarget'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("CROSS_SCENARIO_TARGET_ID:CROSS_SCENARIO_TARGET_ID")]
        public TCrossScenarioTarget TCrossScenarioTarget {
            get { return _tCrossScenarioTarget; }
            set { _tCrossScenarioTarget = value; }
        }

        protected TPolylineCategoryList _tPolylineCategoryList;

        /// <summary>T_POLYLINE_CATEGORY_LIST as 'TPolylineCategoryList'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("CROSS_SCENARIO_ITEM_ID:CROSS_SCENARIO_ITEM_ID")]
        public TPolylineCategoryList TPolylineCategoryList {
            get { return _tPolylineCategoryList; }
            set { _tPolylineCategoryList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TPolylineCategoryList> _tPolylineCategoryListList;

        /// <summary>T_POLYLINE_CATEGORY_LIST as 'TPolylineCategoryListList'.</summary>
        public IList<TPolylineCategoryList> TPolylineCategoryListList {
            get { if (_tPolylineCategoryListList == null) { _tPolylineCategoryListList = new List<TPolylineCategoryList>(); } return _tPolylineCategoryListList; }
            set { _tPolylineCategoryListList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_crossScenarioItemId == null) { return false; }
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
            if (other == null || !(other is TCrossScenarioItem)) { return false; }
            TCrossScenarioItem otherEntity = (TCrossScenarioItem)other;
            if (!xSV(this.CrossScenarioItemId, otherEntity.CrossScenarioItemId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _crossScenarioItemId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TCrossScenarioItem:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tCrossScenarioTarget != null)
            { sb.Append(l).Append(xbRDS(_tCrossScenarioTarget, "TCrossScenarioTarget")); }
            if (_tPolylineCategoryList != null)
            { sb.Append(l).Append(xbRDS(_tPolylineCategoryList, "TPolylineCategoryList")); }
            if (_tPolylineCategoryListList != null) { foreach (Entity e in _tPolylineCategoryListList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TPolylineCategoryListList")); } } }
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
            sb.Append(c).Append(this.CrossScenarioItemId);
            sb.Append(c).Append(this.CrossScenarioTargetId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.Axis1ItemId);
            sb.Append(c).Append(this.Axis2ItemId);
            sb.Append(c).Append(this.ViewItemName);
            sb.Append(c).Append(this.GraphType);
            sb.Append(c).Append(this.ReportType);
            sb.Append(c).Append(this.TitleString);
            sb.Append(c).Append(this.ScenarioComment);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tCrossScenarioTarget != null) { sb.Append(c).Append("TCrossScenarioTarget"); }
            if (_tPolylineCategoryList != null) { sb.Append(c).Append("TPolylineCategoryList"); }
            if (_tPolylineCategoryListList != null && _tPolylineCategoryListList.Count > 0)
            { sb.Append(c).Append("TPolylineCategoryListList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>CROSS_SCENARIO_ITEM_ID: {PK, NotNull, NUMBER(27), FK to T_Polyline_Category_List}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_SCENARIO_ITEM_ID")]
        public decimal? CrossScenarioItemId {
            get { return _crossScenarioItemId; }
            set {
                __modifiedProperties.AddPropertyName("CrossScenarioItemId");
                _crossScenarioItemId = value;
            }
        }

        /// <summary>CROSS_SCENARIO_TARGET_ID: {IX, NotNull, NUMBER(27), FK to T_CROSS_SCENARIO_TARGET}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_SCENARIO_TARGET_ID")]
        public decimal? CrossScenarioTargetId {
            get { return _crossScenarioTargetId; }
            set {
                __modifiedProperties.AddPropertyName("CrossScenarioTargetId");
                _crossScenarioTargetId = value;
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

        /// <summary>AXIS1_ITEM_ID: {IX, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("AXIS1_ITEM_ID")]
        public decimal? Axis1ItemId {
            get { return _axis1ItemId; }
            set {
                __modifiedProperties.AddPropertyName("Axis1ItemId");
                _axis1ItemId = value;
            }
        }

        /// <summary>AXIS2_ITEM_ID: {IX, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("AXIS2_ITEM_ID")]
        public decimal? Axis2ItemId {
            get { return _axis2ItemId; }
            set {
                __modifiedProperties.AddPropertyName("Axis2ItemId");
                _axis2ItemId = value;
            }
        }

        /// <summary>VIEW_ITEM_NAME: {NVARCHAR2(100)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_ITEM_NAME")]
        public String ViewItemName {
            get { return _viewItemName; }
            set {
                __modifiedProperties.AddPropertyName("ViewItemName");
                _viewItemName = value;
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

        /// <summary>REPORT_TYPE: {NotNull, NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_TYPE")]
        public int? ReportType {
            get { return _reportType; }
            set {
                __modifiedProperties.AddPropertyName("ReportType");
                _reportType = value;
            }
        }

        /// <summary>TITLE_STRING: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_STRING")]
        public String TitleString {
            get { return _titleString; }
            set {
                __modifiedProperties.AddPropertyName("TitleString");
                _titleString = value;
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

        #endregion
    }
}
