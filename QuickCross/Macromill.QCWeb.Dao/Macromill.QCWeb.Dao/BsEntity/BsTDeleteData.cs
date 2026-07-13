

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
    /// The entity of T_DELETE_DATA as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DATA_EDIT_ID
    /// 
    /// [column]
    ///     DATA_EDIT_ID, DELETE_TYPE, CONDITION_DIV
    /// 
    /// [sequence]
    ///     T_Delete_Data_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_DATA_EDIT_LIST
    /// 
    /// [referrer-table]
    ///     T_DELETE_CONDITION, T_DELETE_SAMPLE_ID_LIST
    /// 
    /// [foreign-property]
    ///     tDataEditList
    /// 
    /// [referrer-property]
    ///     tDeleteConditionList, tDeleteSampleIdListList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DELETE_DATA")]
    [System.Serializable]
    public partial class TDeleteData : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        protected decimal? _dataEditId;

        /// <summary>DELETE_TYPE: {NotNull, NUMBER(1), default=[1]}</summary>
        protected int? _deleteType;

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1)}</summary>
        protected String _conditionDiv;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DELETE_DATA"; } }
        public String TablePropertyName { get { return "TDeleteData"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TDataEditList _tDataEditList;

        /// <summary>T_DATA_EDIT_LIST as 'TDataEditList'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDataEditList TDataEditList {
            get { return _tDataEditList; }
            set { _tDataEditList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TDeleteCondition> _tDeleteConditionList;

        /// <summary>T_DELETE_CONDITION as 'TDeleteConditionList'.</summary>
        public IList<TDeleteCondition> TDeleteConditionList {
            get { if (_tDeleteConditionList == null) { _tDeleteConditionList = new List<TDeleteCondition>(); } return _tDeleteConditionList; }
            set { _tDeleteConditionList = value; }
        }

        protected IList<TDeleteSampleIdList> _tDeleteSampleIdListList;

        /// <summary>T_DELETE_SAMPLE_ID_LIST as 'TDeleteSampleIdListList'.</summary>
        public IList<TDeleteSampleIdList> TDeleteSampleIdListList {
            get { if (_tDeleteSampleIdListList == null) { _tDeleteSampleIdListList = new List<TDeleteSampleIdList>(); } return _tDeleteSampleIdListList; }
            set { _tDeleteSampleIdListList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_dataEditId == null) { return false; }
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
            if (other == null || !(other is TDeleteData)) { return false; }
            TDeleteData otherEntity = (TDeleteData)other;
            if (!xSV(this.DataEditId, otherEntity.DataEditId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _dataEditId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDeleteData:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDataEditList != null)
            { sb.Append(l).Append(xbRDS(_tDataEditList, "TDataEditList")); }
            if (_tDeleteConditionList != null) { foreach (Entity e in _tDeleteConditionList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDeleteConditionList")); } } }
            if (_tDeleteSampleIdListList != null) { foreach (Entity e in _tDeleteSampleIdListList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDeleteSampleIdListList")); } } }
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
            sb.Append(c).Append(this.DataEditId);
            sb.Append(c).Append(this.DeleteType);
            sb.Append(c).Append(this.ConditionDiv);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDataEditList != null) { sb.Append(c).Append("TDataEditList"); }
            if (_tDeleteConditionList != null && _tDeleteConditionList.Count > 0)
            { sb.Append(c).Append("TDeleteConditionList"); }
            if (_tDeleteSampleIdListList != null && _tDeleteSampleIdListList.Count > 0)
            { sb.Append(c).Append("TDeleteSampleIdListList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
            }
        }

        /// <summary>DELETE_TYPE: {NotNull, NUMBER(1), default=[1]}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_TYPE")]
        public int? DeleteType {
            get { return _deleteType; }
            set {
                __modifiedProperties.AddPropertyName("DeleteType");
                _deleteType = value;
            }
        }

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1)}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_DIV")]
        public String ConditionDiv {
            get { return _conditionDiv; }
            set {
                __modifiedProperties.AddPropertyName("ConditionDiv");
                _conditionDiv = value;
            }
        }

        #endregion
    }
}
