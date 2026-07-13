

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
    /// The entity of T_FA_LIST_ADD_ITEM as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     FA_LIST_ADD_ITEM_ID
    /// 
    /// [column]
    ///     FA_LIST_ADD_ITEM_ID, FA_SCENARIO_HEADER_ID, ITEM_INFO_ID, SORT_NO, LV2TITLE
    /// 
    /// [sequence]
    ///     T_FA_List_Add_Item_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_FA_SCENARIO_HEADER, T_ITEM_INFO
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tFaScenarioHeader, tItemInfo
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_FA_LIST_ADD_ITEM")]
    [System.Serializable]
    public partial class TFaListAddItem : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>FA_LIST_ADD_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _faListAddItemId;

        /// <summary>FA_SCENARIO_HEADER_ID: {IX, NotNull, NUMBER(27), FK to T_FA_SCENARIO_HEADER}</summary>
        protected decimal? _faScenarioHeaderId;

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _itemInfoId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>LV2TITLE: {NCLOB(4000)}</summary>
        protected String _lv2title;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_FA_LIST_ADD_ITEM"; } }
        public String TablePropertyName { get { return "TFaListAddItem"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TFaScenarioHeader _tFaScenarioHeader;

        /// <summary>T_FA_SCENARIO_HEADER as 'TFaScenarioHeader'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("FA_SCENARIO_HEADER_ID:FA_SCENARIO_HEADER_ID")]
        public TFaScenarioHeader TFaScenarioHeader {
            get { return _tFaScenarioHeader; }
            set { _tFaScenarioHeader = value; }
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
        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_faListAddItemId == null) { return false; }
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
            if (other == null || !(other is TFaListAddItem)) { return false; }
            TFaListAddItem otherEntity = (TFaListAddItem)other;
            if (!xSV(this.FaListAddItemId, otherEntity.FaListAddItemId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _faListAddItemId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TFaListAddItem:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tFaScenarioHeader != null)
            { sb.Append(l).Append(xbRDS(_tFaScenarioHeader, "TFaScenarioHeader")); }
            if (_tItemInfo != null)
            { sb.Append(l).Append(xbRDS(_tItemInfo, "TItemInfo")); }
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
            sb.Append(c).Append(this.FaListAddItemId);
            sb.Append(c).Append(this.FaScenarioHeaderId);
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.Lv2title);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tFaScenarioHeader != null) { sb.Append(c).Append("TFaScenarioHeader"); }
            if (_tItemInfo != null) { sb.Append(c).Append("TItemInfo"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>FA_LIST_ADD_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("FA_LIST_ADD_ITEM_ID")]
        public decimal? FaListAddItemId {
            get { return _faListAddItemId; }
            set {
                __modifiedProperties.AddPropertyName("FaListAddItemId");
                _faListAddItemId = value;
            }
        }

        /// <summary>FA_SCENARIO_HEADER_ID: {IX, NotNull, NUMBER(27), FK to T_FA_SCENARIO_HEADER}</summary>
        [Seasar.Dao.Attrs.Column("FA_SCENARIO_HEADER_ID")]
        public decimal? FaScenarioHeaderId {
            get { return _faScenarioHeaderId; }
            set {
                __modifiedProperties.AddPropertyName("FaScenarioHeaderId");
                _faScenarioHeaderId = value;
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

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public int? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>LV2TITLE: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("LV2TITLE")]
        public String Lv2title {
            get { return _lv2title; }
            set {
                __modifiedProperties.AddPropertyName("Lv2title");
                _lv2title = value;
            }
        }

        #endregion
    }
}
