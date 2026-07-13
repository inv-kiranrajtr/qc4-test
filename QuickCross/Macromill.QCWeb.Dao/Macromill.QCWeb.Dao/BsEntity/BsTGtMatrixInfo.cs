

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
    /// The entity of T_GT_MATRIX_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     GT_MATRIX_INFO_ID
    /// 
    /// [column]
    ///     GT_MATRIX_INFO_ID, SCENARIO_TOTALIZATION_ID, BASE_ITEM_ID, NEW_ITEM_ID, TOTALIZATION_TYPE, LV1TITLE, ITEM_NAME
    /// 
    /// [sequence]
    ///     T_GT_Matrix_Info_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_SCENARIO_TOTALIZATION, T_GT_Matrix_Child
    /// 
    /// [referrer-table]
    ///     T_GT_MATRIX_CHILD
    /// 
    /// [foreign-property]
    ///     tScenarioTotalization, tGtMatrixChild
    /// 
    /// [referrer-property]
    ///     tGtMatrixChildList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_GT_MATRIX_INFO")]
    [System.Serializable]
    public partial class TGtMatrixInfo : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>GT_MATRIX_INFO_ID: {PK, NotNull, NUMBER(27), FK to T_GT_Matrix_Child}</summary>
        protected decimal? _gtMatrixInfoId;

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>BASE_ITEM_ID: {NotNull, NUMBER(27)}</summary>
        protected decimal? _baseItemId;

        /// <summary>NEW_ITEM_ID: {NotNull, NUMBER(27), default=[0]}</summary>
        protected decimal? _newItemId;

        /// <summary>TOTALIZATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        protected String _totalizationType;

        /// <summary>LV1TITLE: {NVARCHAR2(1000)}</summary>
        protected String _lv1title;

        /// <summary>ITEM_NAME: {NVARCHAR2(26)}</summary>
        protected String _itemName;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_GT_MATRIX_INFO"; } }
        public String TablePropertyName { get { return "TGtMatrixInfo"; } }

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

        protected TGtMatrixChild _tGtMatrixChild;

        /// <summary>T_GT_MATRIX_CHILD as 'TGtMatrixChild'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("GT_MATRIX_INFO_ID:GT_MATRIX_INFO_ID")]
        public TGtMatrixChild TGtMatrixChild {
            get { return _tGtMatrixChild; }
            set { _tGtMatrixChild = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TGtMatrixChild> _tGtMatrixChildList;

        /// <summary>T_GT_MATRIX_CHILD as 'TGtMatrixChildList'.</summary>
        public IList<TGtMatrixChild> TGtMatrixChildList {
            get { if (_tGtMatrixChildList == null) { _tGtMatrixChildList = new List<TGtMatrixChild>(); } return _tGtMatrixChildList; }
            set { _tGtMatrixChildList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_gtMatrixInfoId == null) { return false; }
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
            if (other == null || !(other is TGtMatrixInfo)) { return false; }
            TGtMatrixInfo otherEntity = (TGtMatrixInfo)other;
            if (!xSV(this.GtMatrixInfoId, otherEntity.GtMatrixInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _gtMatrixInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TGtMatrixInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tGtMatrixChild != null)
            { sb.Append(l).Append(xbRDS(_tGtMatrixChild, "TGtMatrixChild")); }
            if (_tGtMatrixChildList != null) { foreach (Entity e in _tGtMatrixChildList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TGtMatrixChildList")); } } }
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
            sb.Append(c).Append(this.GtMatrixInfoId);
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.BaseItemId);
            sb.Append(c).Append(this.NewItemId);
            sb.Append(c).Append(this.TotalizationType);
            sb.Append(c).Append(this.Lv1title);
            sb.Append(c).Append(this.ItemName);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tGtMatrixChild != null) { sb.Append(c).Append("TGtMatrixChild"); }
            if (_tGtMatrixChildList != null && _tGtMatrixChildList.Count > 0)
            { sb.Append(c).Append("TGtMatrixChildList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>GT_MATRIX_INFO_ID: {PK, NotNull, NUMBER(27), FK to T_GT_Matrix_Child}</summary>
        [Seasar.Dao.Attrs.Column("GT_MATRIX_INFO_ID")]
        public decimal? GtMatrixInfoId {
            get { return _gtMatrixInfoId; }
            set {
                __modifiedProperties.AddPropertyName("GtMatrixInfoId");
                _gtMatrixInfoId = value;
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

        /// <summary>BASE_ITEM_ID: {NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("BASE_ITEM_ID")]
        public decimal? BaseItemId {
            get { return _baseItemId; }
            set {
                __modifiedProperties.AddPropertyName("BaseItemId");
                _baseItemId = value;
            }
        }

        /// <summary>NEW_ITEM_ID: {NotNull, NUMBER(27), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("NEW_ITEM_ID")]
        public decimal? NewItemId {
            get { return _newItemId; }
            set {
                __modifiedProperties.AddPropertyName("NewItemId");
                _newItemId = value;
            }
        }

        /// <summary>TOTALIZATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("TOTALIZATION_TYPE")]
        public String TotalizationType {
            get { return _totalizationType; }
            set {
                __modifiedProperties.AddPropertyName("TotalizationType");
                _totalizationType = value;
            }
        }

        /// <summary>LV1TITLE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LV1TITLE")]
        public String Lv1title {
            get { return _lv1title; }
            set {
                __modifiedProperties.AddPropertyName("Lv1title");
                _lv1title = value;
            }
        }

        /// <summary>ITEM_NAME: {NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_NAME")]
        public String ItemName {
            get { return _itemName; }
            set {
                __modifiedProperties.AddPropertyName("ItemName");
                _itemName = value;
            }
        }

        #endregion
    }
}
