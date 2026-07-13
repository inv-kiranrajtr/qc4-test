

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
    /// The entity of T_POLYLINE_CATEGORY_LIST as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     POLYLINE_CATEGORY_LIST_ID
    /// 
    /// [column]
    ///     POLYLINE_CATEGORY_LIST_ID, CROSS_SCENARIO_ITEM_ID, AXIS_CATEGORY_NO, AXIS2_CATEGORY_NO, ARRAY_NO_SINGULAR, ARRAY_NO_PLURAL
    /// 
    /// [sequence]
    ///     T_Polyline_Category_List_SEQ01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_CROSS_SCENARIO_ITEM
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tCrossScenarioItem
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_POLYLINE_CATEGORY_LIST")]
    [System.Serializable]
    public partial class TPolylineCategoryList : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>POLYLINE_CATEGORY_LIST_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _polylineCategoryListId;

        /// <summary>CROSS_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27), FK to T_CROSS_SCENARIO_ITEM}</summary>
        protected decimal? _crossScenarioItemId;

        /// <summary>AXIS_CATEGORY_NO: {NUMBER(5)}</summary>
        protected int? _axisCategoryNo;

        /// <summary>AXIS2_CATEGORY_NO: {NUMBER(5)}</summary>
        protected int? _axis2CategoryNo;

        /// <summary>ARRAY_NO_SINGULAR: {NUMBER(6)}</summary>
        protected int? _arrayNoSingular;

        /// <summary>ARRAY_NO_PLURAL: {NUMBER(6)}</summary>
        protected int? _arrayNoPlural;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_POLYLINE_CATEGORY_LIST"; } }
        public String TablePropertyName { get { return "TPolylineCategoryList"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TCrossScenarioItem _tCrossScenarioItem;

        /// <summary>T_CROSS_SCENARIO_ITEM as 'TCrossScenarioItem'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("CROSS_SCENARIO_ITEM_ID:CROSS_SCENARIO_ITEM_ID")]
        public TCrossScenarioItem TCrossScenarioItem {
            get { return _tCrossScenarioItem; }
            set { _tCrossScenarioItem = value; }
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
                if (_polylineCategoryListId == null) { return false; }
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
            if (other == null || !(other is TPolylineCategoryList)) { return false; }
            TPolylineCategoryList otherEntity = (TPolylineCategoryList)other;
            if (!xSV(this.PolylineCategoryListId, otherEntity.PolylineCategoryListId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _polylineCategoryListId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TPolylineCategoryList:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tCrossScenarioItem != null)
            { sb.Append(l).Append(xbRDS(_tCrossScenarioItem, "TCrossScenarioItem")); }
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
            sb.Append(c).Append(this.PolylineCategoryListId);
            sb.Append(c).Append(this.CrossScenarioItemId);
            sb.Append(c).Append(this.AxisCategoryNo);
            sb.Append(c).Append(this.Axis2CategoryNo);
            sb.Append(c).Append(this.ArrayNoSingular);
            sb.Append(c).Append(this.ArrayNoPlural);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tCrossScenarioItem != null) { sb.Append(c).Append("TCrossScenarioItem"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>POLYLINE_CATEGORY_LIST_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("POLYLINE_CATEGORY_LIST_ID")]
        public decimal? PolylineCategoryListId {
            get { return _polylineCategoryListId; }
            set {
                __modifiedProperties.AddPropertyName("PolylineCategoryListId");
                _polylineCategoryListId = value;
            }
        }

        /// <summary>CROSS_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27), FK to T_CROSS_SCENARIO_ITEM}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_SCENARIO_ITEM_ID")]
        public decimal? CrossScenarioItemId {
            get { return _crossScenarioItemId; }
            set {
                __modifiedProperties.AddPropertyName("CrossScenarioItemId");
                _crossScenarioItemId = value;
            }
        }

        /// <summary>AXIS_CATEGORY_NO: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("AXIS_CATEGORY_NO")]
        public int? AxisCategoryNo {
            get { return _axisCategoryNo; }
            set {
                __modifiedProperties.AddPropertyName("AxisCategoryNo");
                _axisCategoryNo = value;
            }
        }

        /// <summary>AXIS2_CATEGORY_NO: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("AXIS2_CATEGORY_NO")]
        public int? Axis2CategoryNo {
            get { return _axis2CategoryNo; }
            set {
                __modifiedProperties.AddPropertyName("Axis2CategoryNo");
                _axis2CategoryNo = value;
            }
        }

        /// <summary>ARRAY_NO_SINGULAR: {NUMBER(6)}</summary>
        [Seasar.Dao.Attrs.Column("ARRAY_NO_SINGULAR")]
        public int? ArrayNoSingular {
            get { return _arrayNoSingular; }
            set {
                __modifiedProperties.AddPropertyName("ArrayNoSingular");
                _arrayNoSingular = value;
            }
        }

        /// <summary>ARRAY_NO_PLURAL: {NUMBER(6)}</summary>
        [Seasar.Dao.Attrs.Column("ARRAY_NO_PLURAL")]
        public int? ArrayNoPlural {
            get { return _arrayNoPlural; }
            set {
                __modifiedProperties.AddPropertyName("ArrayNoPlural");
                _arrayNoPlural = value;
            }
        }

        #endregion
    }
}
