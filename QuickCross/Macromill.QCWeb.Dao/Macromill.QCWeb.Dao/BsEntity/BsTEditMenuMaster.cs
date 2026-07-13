

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
    /// The entity of T_EDIT_MENU_MASTER as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     EDIT_MENU_MASTER_ID
    /// 
    /// [column]
    ///     EDIT_MENU_MASTER_ID, EDIT_CLASSIFICATION, PROCESS_TYPE, EXPLANATION, EXAMPLE, DETAILEDEXPLANATION, SORT_NO, TYPE_BIT_UNION
    /// 
    /// [sequence]
    ///     
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     
    /// 
    /// [referrer-table]
    ///     T_DATA_EDIT_LIST
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     tDataEditListList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_EDIT_MENU_MASTER")]
    [System.Serializable]
    public partial class TEditMenuMaster : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>EDIT_MENU_MASTER_ID: {PK, NotNull, NUMBER(2)}</summary>
        protected int? _editMenuMasterId;

        /// <summary>EDIT_CLASSIFICATION: {VARCHAR2(60)}</summary>
        protected String _editClassification;

        /// <summary>PROCESS_TYPE: {VARCHAR2(60)}</summary>
        protected String _processType;

        /// <summary>EXPLANATION: {VARCHAR2(60)}</summary>
        protected String _explanation;

        /// <summary>EXAMPLE: {VARCHAR2(60)}</summary>
        protected String _example;

        /// <summary>DETAILEDEXPLANATION: {VARCHAR2(60)}</summary>
        protected String _detailedexplanation;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>TYPE_BIT_UNION: {VARCHAR2(10)}</summary>
        protected String _typeBitUnion;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_EDIT_MENU_MASTER"; } }
        public String TablePropertyName { get { return "TEditMenuMaster"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TDataEditList> _tDataEditListList;

        /// <summary>T_DATA_EDIT_LIST as 'TDataEditListList'.</summary>
        public IList<TDataEditList> TDataEditListList {
            get { if (_tDataEditListList == null) { _tDataEditListList = new List<TDataEditList>(); } return _tDataEditListList; }
            set { _tDataEditListList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_editMenuMasterId == null) { return false; }
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
            if (other == null || !(other is TEditMenuMaster)) { return false; }
            TEditMenuMaster otherEntity = (TEditMenuMaster)other;
            if (!xSV(this.EditMenuMasterId, otherEntity.EditMenuMasterId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _editMenuMasterId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TEditMenuMaster:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDataEditListList != null) { foreach (Entity e in _tDataEditListList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDataEditListList")); } } }
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
            sb.Append(c).Append(this.EditMenuMasterId);
            sb.Append(c).Append(this.EditClassification);
            sb.Append(c).Append(this.ProcessType);
            sb.Append(c).Append(this.Explanation);
            sb.Append(c).Append(this.Example);
            sb.Append(c).Append(this.Detailedexplanation);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.TypeBitUnion);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDataEditListList != null && _tDataEditListList.Count > 0)
            { sb.Append(c).Append("TDataEditListList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>EDIT_MENU_MASTER_ID: {PK, NotNull, NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_MENU_MASTER_ID")]
        public int? EditMenuMasterId {
            get { return _editMenuMasterId; }
            set {
                __modifiedProperties.AddPropertyName("EditMenuMasterId");
                _editMenuMasterId = value;
            }
        }

        /// <summary>EDIT_CLASSIFICATION: {VARCHAR2(60)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_CLASSIFICATION")]
        public String EditClassification {
            get { return _editClassification; }
            set {
                __modifiedProperties.AddPropertyName("EditClassification");
                _editClassification = value;
            }
        }

        /// <summary>PROCESS_TYPE: {VARCHAR2(60)}</summary>
        [Seasar.Dao.Attrs.Column("PROCESS_TYPE")]
        public String ProcessType {
            get { return _processType; }
            set {
                __modifiedProperties.AddPropertyName("ProcessType");
                _processType = value;
            }
        }

        /// <summary>EXPLANATION: {VARCHAR2(60)}</summary>
        [Seasar.Dao.Attrs.Column("EXPLANATION")]
        public String Explanation {
            get { return _explanation; }
            set {
                __modifiedProperties.AddPropertyName("Explanation");
                _explanation = value;
            }
        }

        /// <summary>EXAMPLE: {VARCHAR2(60)}</summary>
        [Seasar.Dao.Attrs.Column("EXAMPLE")]
        public String Example {
            get { return _example; }
            set {
                __modifiedProperties.AddPropertyName("Example");
                _example = value;
            }
        }

        /// <summary>DETAILEDEXPLANATION: {VARCHAR2(60)}</summary>
        [Seasar.Dao.Attrs.Column("DETAILEDEXPLANATION")]
        public String Detailedexplanation {
            get { return _detailedexplanation; }
            set {
                __modifiedProperties.AddPropertyName("Detailedexplanation");
                _detailedexplanation = value;
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

        /// <summary>TYPE_BIT_UNION: {VARCHAR2(10)}</summary>
        [Seasar.Dao.Attrs.Column("TYPE_BIT_UNION")]
        public String TypeBitUnion {
            get { return _typeBitUnion; }
            set {
                __modifiedProperties.AddPropertyName("TypeBitUnion");
                _typeBitUnion = value;
            }
        }

        #endregion
    }
}
