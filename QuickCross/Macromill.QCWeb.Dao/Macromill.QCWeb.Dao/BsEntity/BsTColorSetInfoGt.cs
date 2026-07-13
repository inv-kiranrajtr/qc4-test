

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
    /// The entity of T_COLOR_SET_INFO_GT as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     COLOR_SET_INFO_GT_ID
    /// 
    /// [column]
    ///     COLOR_SET_INFO_GT_ID, TYPE_CODE, GRADATION_TYPE, GT_SCENARIO_ITEM_ID
    /// 
    /// [sequence]
    ///     T_Color_Set_Info_GT_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_GT_SCENARIO_ITEM
    /// 
    /// [referrer-table]
    ///     T_COLOR_INFO_DETAIL_GT
    /// 
    /// [foreign-property]
    ///     tGtScenarioItem
    /// 
    /// [referrer-property]
    ///     tColorInfoDetailGtList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_COLOR_SET_INFO_GT")]
    [System.Serializable]
    public partial class TColorSetInfoGt : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>COLOR_SET_INFO_GT_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _colorSetInfoGtId;

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        protected String _typeCode;

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        protected String _gradationType;

        /// <summary>GT_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_GT_SCENARIO_ITEM}</summary>
        protected decimal? _gtScenarioItemId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_COLOR_SET_INFO_GT"; } }
        public String TablePropertyName { get { return "TColorSetInfoGt"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TGtScenarioItem _tGtScenarioItem;

        /// <summary>T_GT_SCENARIO_ITEM as 'TGtScenarioItem'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("GT_SCENARIO_ITEM_ID:GT_SCENARIO_ITEM_ID")]
        public TGtScenarioItem TGtScenarioItem {
            get { return _tGtScenarioItem; }
            set { _tGtScenarioItem = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TColorInfoDetailGt> _tColorInfoDetailGtList;

        /// <summary>T_COLOR_INFO_DETAIL_GT as 'TColorInfoDetailGtList'.</summary>
        public IList<TColorInfoDetailGt> TColorInfoDetailGtList {
            get { if (_tColorInfoDetailGtList == null) { _tColorInfoDetailGtList = new List<TColorInfoDetailGt>(); } return _tColorInfoDetailGtList; }
            set { _tColorInfoDetailGtList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_colorSetInfoGtId == null) { return false; }
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
            if (other == null || !(other is TColorSetInfoGt)) { return false; }
            TColorSetInfoGt otherEntity = (TColorSetInfoGt)other;
            if (!xSV(this.ColorSetInfoGtId, otherEntity.ColorSetInfoGtId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _colorSetInfoGtId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TColorSetInfoGt:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tGtScenarioItem != null)
            { sb.Append(l).Append(xbRDS(_tGtScenarioItem, "TGtScenarioItem")); }
            if (_tColorInfoDetailGtList != null) { foreach (Entity e in _tColorInfoDetailGtList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TColorInfoDetailGtList")); } } }
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
            sb.Append(c).Append(this.ColorSetInfoGtId);
            sb.Append(c).Append(this.TypeCode);
            sb.Append(c).Append(this.GradationType);
            sb.Append(c).Append(this.GtScenarioItemId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tGtScenarioItem != null) { sb.Append(c).Append("TGtScenarioItem"); }
            if (_tColorInfoDetailGtList != null && _tColorInfoDetailGtList.Count > 0)
            { sb.Append(c).Append("TColorInfoDetailGtList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>COLOR_SET_INFO_GT_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_SET_INFO_GT_ID")]
        public decimal? ColorSetInfoGtId {
            get { return _colorSetInfoGtId; }
            set {
                __modifiedProperties.AddPropertyName("ColorSetInfoGtId");
                _colorSetInfoGtId = value;
            }
        }

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("TYPE_CODE")]
        public String TypeCode {
            get { return _typeCode; }
            set {
                __modifiedProperties.AddPropertyName("TypeCode");
                _typeCode = value;
            }
        }

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRADATION_TYPE")]
        public String GradationType {
            get { return _gradationType; }
            set {
                __modifiedProperties.AddPropertyName("GradationType");
                _gradationType = value;
            }
        }

        /// <summary>GT_SCENARIO_ITEM_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_GT_SCENARIO_ITEM}</summary>
        [Seasar.Dao.Attrs.Column("GT_SCENARIO_ITEM_ID")]
        public decimal? GtScenarioItemId {
            get { return _gtScenarioItemId; }
            set {
                __modifiedProperties.AddPropertyName("GtScenarioItemId");
                _gtScenarioItemId = value;
            }
        }

        #endregion
    }
}
