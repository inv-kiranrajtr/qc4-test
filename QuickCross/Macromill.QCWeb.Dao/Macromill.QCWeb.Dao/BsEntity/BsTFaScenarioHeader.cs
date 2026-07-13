

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
    /// The entity of T_FA_SCENARIO_HEADER as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     FA_SCENARIO_HEADER_ID
    /// 
    /// [column]
    ///     FA_SCENARIO_HEADER_ID, SCENARIO_TOTALIZATION_ID, SCENARIO_COMMENT, VIEW_NAME
    /// 
    /// [sequence]
    ///     T_FA_Scenario_Header_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_SCENARIO_TOTALIZATION, T_FA_Scenario_Item, T_FA_LIST_ADD_ITEM
    /// 
    /// [referrer-table]
    ///     T_FA_LIST_ADD_ITEM, T_FA_SCENARIO_ITEM
    /// 
    /// [foreign-property]
    ///     tScenarioTotalization, tFaScenarioItem, tFaListAddItem
    /// 
    /// [referrer-property]
    ///     tFaListAddItemList, tFaScenarioItemList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_FA_SCENARIO_HEADER")]
    [System.Serializable]
    public partial class TFaScenarioHeader : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>FA_SCENARIO_HEADER_ID: {PK, NotNull, NUMBER(27), FK to T_FA_Scenario_Item}</summary>
        protected decimal? _faScenarioHeaderId;

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>SCENARIO_COMMENT: {NCLOB(4000)}</summary>
        protected String _scenarioComment;

        /// <summary>VIEW_NAME: {NotNull, NVARCHAR2(1000)}</summary>
        protected String _viewName;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_FA_SCENARIO_HEADER"; } }
        public String TablePropertyName { get { return "TFaScenarioHeader"; } }

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

        protected TFaScenarioItem _tFaScenarioItem;

        /// <summary>T_FA_SCENARIO_ITEM as 'TFaScenarioItem'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("FA_SCENARIO_HEADER_ID:FA_SCENARIO_HEADER_ID")]
        public TFaScenarioItem TFaScenarioItem {
            get { return _tFaScenarioItem; }
            set { _tFaScenarioItem = value; }
        }

        protected TFaListAddItem _tFaListAddItem;

        /// <summary>T_FA_LIST_ADD_ITEM as 'TFaListAddItem'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("FA_SCENARIO_HEADER_ID:FA_SCENARIO_HEADER_ID")]
        public TFaListAddItem TFaListAddItem {
            get { return _tFaListAddItem; }
            set { _tFaListAddItem = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TFaListAddItem> _tFaListAddItemList;

        /// <summary>T_FA_LIST_ADD_ITEM as 'TFaListAddItemList'.</summary>
        public IList<TFaListAddItem> TFaListAddItemList {
            get { if (_tFaListAddItemList == null) { _tFaListAddItemList = new List<TFaListAddItem>(); } return _tFaListAddItemList; }
            set { _tFaListAddItemList = value; }
        }

        protected IList<TFaScenarioItem> _tFaScenarioItemList;

        /// <summary>T_FA_SCENARIO_ITEM as 'TFaScenarioItemList'.</summary>
        public IList<TFaScenarioItem> TFaScenarioItemList {
            get { if (_tFaScenarioItemList == null) { _tFaScenarioItemList = new List<TFaScenarioItem>(); } return _tFaScenarioItemList; }
            set { _tFaScenarioItemList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_faScenarioHeaderId == null) { return false; }
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
            if (other == null || !(other is TFaScenarioHeader)) { return false; }
            TFaScenarioHeader otherEntity = (TFaScenarioHeader)other;
            if (!xSV(this.FaScenarioHeaderId, otherEntity.FaScenarioHeaderId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _faScenarioHeaderId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TFaScenarioHeader:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tFaScenarioItem != null)
            { sb.Append(l).Append(xbRDS(_tFaScenarioItem, "TFaScenarioItem")); }
            if (_tFaListAddItem != null)
            { sb.Append(l).Append(xbRDS(_tFaListAddItem, "TFaListAddItem")); }
            if (_tFaListAddItemList != null) { foreach (Entity e in _tFaListAddItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TFaListAddItemList")); } } }
            if (_tFaScenarioItemList != null) { foreach (Entity e in _tFaScenarioItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TFaScenarioItemList")); } } }
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
            sb.Append(c).Append(this.FaScenarioHeaderId);
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.ScenarioComment);
            sb.Append(c).Append(this.ViewName);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tFaScenarioItem != null) { sb.Append(c).Append("TFaScenarioItem"); }
            if (_tFaListAddItem != null) { sb.Append(c).Append("TFaListAddItem"); }
            if (_tFaListAddItemList != null && _tFaListAddItemList.Count > 0)
            { sb.Append(c).Append("TFaListAddItemList"); }
            if (_tFaScenarioItemList != null && _tFaScenarioItemList.Count > 0)
            { sb.Append(c).Append("TFaScenarioItemList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>FA_SCENARIO_HEADER_ID: {PK, NotNull, NUMBER(27), FK to T_FA_Scenario_Item}</summary>
        [Seasar.Dao.Attrs.Column("FA_SCENARIO_HEADER_ID")]
        public decimal? FaScenarioHeaderId {
            get { return _faScenarioHeaderId; }
            set {
                __modifiedProperties.AddPropertyName("FaScenarioHeaderId");
                _faScenarioHeaderId = value;
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

        /// <summary>SCENARIO_COMMENT: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_COMMENT")]
        public String ScenarioComment {
            get { return _scenarioComment; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioComment");
                _scenarioComment = value;
            }
        }

        /// <summary>VIEW_NAME: {NotNull, NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_NAME")]
        public String ViewName {
            get { return _viewName; }
            set {
                __modifiedProperties.AddPropertyName("ViewName");
                _viewName = value;
            }
        }

        #endregion
    }
}
