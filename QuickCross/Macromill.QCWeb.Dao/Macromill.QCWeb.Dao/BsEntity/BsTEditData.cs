

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
    /// The entity of T_EDIT_DATA as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DATA_EDIT_ID
    /// 
    /// [column]
    ///     DATA_EDIT_ID, CONDITION_FLAG, EDIT_METHOD, EDIT_VALUE_TYPE, EDIT_VALUE, CONDITION_DIV
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
    ///     T_DATA_EDIT_LIST
    /// 
    /// [referrer-table]
    ///     T_EDIT_CONDITION, T_EDIT_TARGET_ITEM
    /// 
    /// [foreign-property]
    ///     tDataEditList
    /// 
    /// [referrer-property]
    ///     tEditConditionList, tEditTargetItemList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_EDIT_DATA")]
    [System.Serializable]
    public partial class TEditData : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        protected decimal? _dataEditId;

        /// <summary>CONDITION_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _conditionFlag;

        /// <summary>EDIT_METHOD: {NUMBER(1)}</summary>
        protected int? _editMethod;

        /// <summary>EDIT_VALUE_TYPE: {NUMBER(1)}</summary>
        protected int? _editValueType;

        /// <summary>EDIT_VALUE: {NVARCHAR2(1000)}</summary>
        protected String _editValue;

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1)}</summary>
        protected String _conditionDiv;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_EDIT_DATA"; } }
        public String TablePropertyName { get { return "TEditData"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag ConditionFlagAsFlag { get {
            return CDef.Flag.CodeOf(_conditionFlag);
        } set {
            ConditionFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of conditionFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetConditionFlag_True() {
            ConditionFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of conditionFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetConditionFlag_False() {
            ConditionFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of conditionFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsConditionFlagTrue {
            get {
                CDef.Flag cls = ConditionFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of conditionFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsConditionFlagFalse {
            get {
                CDef.Flag cls = ConditionFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String ConditionFlagName {
            get {
                CDef.Flag cls = ConditionFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String ConditionFlagAlias {
            get {
                CDef.Flag cls = ConditionFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

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
        protected IList<TEditCondition> _tEditConditionList;

        /// <summary>T_EDIT_CONDITION as 'TEditConditionList'.</summary>
        public IList<TEditCondition> TEditConditionList {
            get { if (_tEditConditionList == null) { _tEditConditionList = new List<TEditCondition>(); } return _tEditConditionList; }
            set { _tEditConditionList = value; }
        }

        protected IList<TEditTargetItem> _tEditTargetItemList;

        /// <summary>T_EDIT_TARGET_ITEM as 'TEditTargetItemList'.</summary>
        public IList<TEditTargetItem> TEditTargetItemList {
            get { if (_tEditTargetItemList == null) { _tEditTargetItemList = new List<TEditTargetItem>(); } return _tEditTargetItemList; }
            set { _tEditTargetItemList = value; }
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
            if (other == null || !(other is TEditData)) { return false; }
            TEditData otherEntity = (TEditData)other;
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
            return "TEditData:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tDataEditList != null)
            { sb.Append(l).Append(xbRDS(_tDataEditList, "TDataEditList")); }
            if (_tEditConditionList != null) { foreach (Entity e in _tEditConditionList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TEditConditionList")); } } }
            if (_tEditTargetItemList != null) { foreach (Entity e in _tEditTargetItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TEditTargetItemList")); } } }
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
            sb.Append(c).Append(this.ConditionFlag);
            sb.Append(c).Append(this.EditMethod);
            sb.Append(c).Append(this.EditValueType);
            sb.Append(c).Append(this.EditValue);
            sb.Append(c).Append(this.ConditionDiv);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tDataEditList != null) { sb.Append(c).Append("TDataEditList"); }
            if (_tEditConditionList != null && _tEditConditionList.Count > 0)
            { sb.Append(c).Append("TEditConditionList"); }
            if (_tEditTargetItemList != null && _tEditTargetItemList.Count > 0)
            { sb.Append(c).Append("TEditTargetItemList"); }
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

        /// <summary>CONDITION_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_FLAG")]
        public int? ConditionFlag {
            get { return _conditionFlag; }
            set {
                __modifiedProperties.AddPropertyName("ConditionFlag");
                _conditionFlag = value;
            }
        }

        /// <summary>EDIT_METHOD: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_METHOD")]
        public int? EditMethod {
            get { return _editMethod; }
            set {
                __modifiedProperties.AddPropertyName("EditMethod");
                _editMethod = value;
            }
        }

        /// <summary>EDIT_VALUE_TYPE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_VALUE_TYPE")]
        public int? EditValueType {
            get { return _editValueType; }
            set {
                __modifiedProperties.AddPropertyName("EditValueType");
                _editValueType = value;
            }
        }

        /// <summary>EDIT_VALUE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_VALUE")]
        public String EditValue {
            get { return _editValue; }
            set {
                __modifiedProperties.AddPropertyName("EditValue");
                _editValue = value;
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
