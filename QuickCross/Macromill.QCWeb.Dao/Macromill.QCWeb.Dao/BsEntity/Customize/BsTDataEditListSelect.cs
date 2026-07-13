

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Helper;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Dao.BsEntity.Customize.Dbm;


namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    /// <summary>
    /// The entity of TDataEditListSelect. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     DATA_EDIT_ID, QCWEBID, EXECUTE_NO, EXECUTE_FLAG, EDIT_MENU_MASTER_ID, CONDITION_ITEM_VIEW_NAME, TARGET_ITEM_VIEW_NAME, STATUS, LATEST_FLAG, DERIVED_DATA_EDIT_ID, DELETE_RESERVE_FLAG, DESCRIPTION, EDIT_FLAG, TARGET_CSV, CONDITION_CSV, FORMULA_STRING
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
    ///     
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("TDataEditListSelect")]
    [System.Serializable]
    public partial class TDataEditListSelect : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _dataEditId;

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>EXECUTE_NO: {NUMBER(5)}</summary>
        protected int? _executeNo;

        /// <summary>EXECUTE_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _executeFlag;

        /// <summary>EDIT_MENU_MASTER_ID: {NUMBER(2)}</summary>
        protected int? _editMenuMasterId;

        /// <summary>CONDITION_ITEM_VIEW_NAME: {VARCHAR2(500)}</summary>
        protected String _conditionItemViewName;

        /// <summary>TARGET_ITEM_VIEW_NAME: {VARCHAR2(500)}</summary>
        protected String _targetItemViewName;

        /// <summary>STATUS: {VARCHAR2(2)}</summary>
        protected String _status;

        /// <summary>LATEST_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _latestFlag;

        /// <summary>DERIVED_DATA_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _derivedDataEditId;

        /// <summary>DELETE_RESERVE_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _deleteReserveFlag;

        /// <summary>DESCRIPTION: {VARCHAR2(1000)}</summary>
        protected String _description;

        /// <summary>EDIT_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _editFlag;

        /// <summary>TARGET_CSV: {VARCHAR2(4000)}</summary>
        protected String _targetCsv;

        /// <summary>CONDITION_CSV: {VARCHAR2(4000)}</summary>
        protected String _conditionCsv;

        /// <summary>FORMULA_STRING: {VARCHAR2(2000)}</summary>
        protected String _formulaString;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "TDataEditListSelect"; } }
        public String TablePropertyName { get { return "TDataEditListSelect"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return TDataEditListSelectDbm.GetInstance(); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag ExecuteFlagAsFlag { get {
            return CDef.Flag.CodeOf(_executeFlag);
        } set {
            ExecuteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag LatestFlagAsFlag { get {
            return CDef.Flag.CodeOf(_latestFlag);
        } set {
            LatestFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag DeleteReserveFlagAsFlag { get {
            return CDef.Flag.CodeOf(_deleteReserveFlag);
        } set {
            DeleteReserveFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag EditFlagAsFlag { get {
            return CDef.Flag.CodeOf(_editFlag);
        } set {
            EditFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of executeFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetExecuteFlag_True() {
            ExecuteFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of executeFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetExecuteFlag_False() {
            ExecuteFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of latestFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetLatestFlag_True() {
            LatestFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of latestFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetLatestFlag_False() {
            LatestFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of deleteReserveFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetDeleteReserveFlag_True() {
            DeleteReserveFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of deleteReserveFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetDeleteReserveFlag_False() {
            DeleteReserveFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of editFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetEditFlag_True() {
            EditFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of editFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetEditFlag_False() {
            EditFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of executeFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsExecuteFlagTrue {
            get {
                CDef.Flag cls = ExecuteFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of executeFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsExecuteFlagFalse {
            get {
                CDef.Flag cls = ExecuteFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of latestFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsLatestFlagTrue {
            get {
                CDef.Flag cls = LatestFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of latestFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsLatestFlagFalse {
            get {
                CDef.Flag cls = LatestFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteReserveFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteReserveFlagTrue {
            get {
                CDef.Flag cls = DeleteReserveFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteReserveFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteReserveFlagFalse {
            get {
                CDef.Flag cls = DeleteReserveFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of editFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsEditFlagTrue {
            get {
                CDef.Flag cls = EditFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of editFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsEditFlagFalse {
            get {
                CDef.Flag cls = EditFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String ExecuteFlagName {
            get {
                CDef.Flag cls = ExecuteFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String ExecuteFlagAlias {
            get {
                CDef.Flag cls = ExecuteFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String LatestFlagName {
            get {
                CDef.Flag cls = LatestFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String LatestFlagAlias {
            get {
                CDef.Flag cls = LatestFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String DeleteReserveFlagName {
            get {
                CDef.Flag cls = DeleteReserveFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String DeleteReserveFlagAlias {
            get {
                CDef.Flag cls = DeleteReserveFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String EditFlagName {
            get {
                CDef.Flag cls = EditFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String EditFlagAlias {
            get {
                CDef.Flag cls = EditFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
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
                return false;
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
            if (other == null || !(other is TDataEditListSelect)) { return false; }
            TDataEditListSelect otherEntity = (TDataEditListSelect)other;
            if (!xSV(this.DataEditId, otherEntity.DataEditId)) { return false; }
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            if (!xSV(this.ExecuteNo, otherEntity.ExecuteNo)) { return false; }
            if (!xSV(this.ExecuteFlag, otherEntity.ExecuteFlag)) { return false; }
            if (!xSV(this.EditMenuMasterId, otherEntity.EditMenuMasterId)) { return false; }
            if (!xSV(this.ConditionItemViewName, otherEntity.ConditionItemViewName)) { return false; }
            if (!xSV(this.TargetItemViewName, otherEntity.TargetItemViewName)) { return false; }
            if (!xSV(this.Status, otherEntity.Status)) { return false; }
            if (!xSV(this.LatestFlag, otherEntity.LatestFlag)) { return false; }
            if (!xSV(this.DerivedDataEditId, otherEntity.DerivedDataEditId)) { return false; }
            if (!xSV(this.DeleteReserveFlag, otherEntity.DeleteReserveFlag)) { return false; }
            if (!xSV(this.Description, otherEntity.Description)) { return false; }
            if (!xSV(this.EditFlag, otherEntity.EditFlag)) { return false; }
            if (!xSV(this.TargetCsv, otherEntity.TargetCsv)) { return false; }
            if (!xSV(this.ConditionCsv, otherEntity.ConditionCsv)) { return false; }
            if (!xSV(this.FormulaString, otherEntity.FormulaString)) { return false; }
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
            result = xCH(result, _qcwebid);
            result = xCH(result, _executeNo);
            result = xCH(result, _executeFlag);
            result = xCH(result, _editMenuMasterId);
            result = xCH(result, _conditionItemViewName);
            result = xCH(result, _targetItemViewName);
            result = xCH(result, _status);
            result = xCH(result, _latestFlag);
            result = xCH(result, _derivedDataEditId);
            result = xCH(result, _deleteReserveFlag);
            result = xCH(result, _description);
            result = xCH(result, _editFlag);
            result = xCH(result, _targetCsv);
            result = xCH(result, _conditionCsv);
            result = xCH(result, _formulaString);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TDataEditListSelect:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            return sb.ToString();
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.ExecuteNo);
            sb.Append(c).Append(this.ExecuteFlag);
            sb.Append(c).Append(this.EditMenuMasterId);
            sb.Append(c).Append(this.ConditionItemViewName);
            sb.Append(c).Append(this.TargetItemViewName);
            sb.Append(c).Append(this.Status);
            sb.Append(c).Append(this.LatestFlag);
            sb.Append(c).Append(this.DerivedDataEditId);
            sb.Append(c).Append(this.DeleteReserveFlag);
            sb.Append(c).Append(this.Description);
            sb.Append(c).Append(this.EditFlag);
            sb.Append(c).Append(this.TargetCsv);
            sb.Append(c).Append(this.ConditionCsv);
            sb.Append(c).Append(this.FormulaString);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            return "";
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DATA_EDIT_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
            }
        }

        /// <summary>QCWEBID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>EXECUTE_NO: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("EXECUTE_NO")]
        public int? ExecuteNo {
            get { return _executeNo; }
            set {
                __modifiedProperties.AddPropertyName("ExecuteNo");
                _executeNo = value;
            }
        }

        /// <summary>EXECUTE_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("EXECUTE_FLAG")]
        public int? ExecuteFlag {
            get { return _executeFlag; }
            set {
                __modifiedProperties.AddPropertyName("ExecuteFlag");
                _executeFlag = value;
            }
        }

        /// <summary>EDIT_MENU_MASTER_ID: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_MENU_MASTER_ID")]
        public int? EditMenuMasterId {
            get { return _editMenuMasterId; }
            set {
                __modifiedProperties.AddPropertyName("EditMenuMasterId");
                _editMenuMasterId = value;
            }
        }

        /// <summary>CONDITION_ITEM_VIEW_NAME: {VARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_ITEM_VIEW_NAME")]
        public String ConditionItemViewName {
            get { return _conditionItemViewName; }
            set {
                __modifiedProperties.AddPropertyName("ConditionItemViewName");
                _conditionItemViewName = value;
            }
        }

        /// <summary>TARGET_ITEM_VIEW_NAME: {VARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_ITEM_VIEW_NAME")]
        public String TargetItemViewName {
            get { return _targetItemViewName; }
            set {
                __modifiedProperties.AddPropertyName("TargetItemViewName");
                _targetItemViewName = value;
            }
        }

        /// <summary>STATUS: {VARCHAR2(2)}</summary>
        [Seasar.Dao.Attrs.Column("STATUS")]
        public String Status {
            get { return _status; }
            set {
                __modifiedProperties.AddPropertyName("Status");
                _status = value;
            }
        }

        /// <summary>LATEST_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("LATEST_FLAG")]
        public int? LatestFlag {
            get { return _latestFlag; }
            set {
                __modifiedProperties.AddPropertyName("LatestFlag");
                _latestFlag = value;
            }
        }

        /// <summary>DERIVED_DATA_EDIT_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DERIVED_DATA_EDIT_ID")]
        public decimal? DerivedDataEditId {
            get { return _derivedDataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DerivedDataEditId");
                _derivedDataEditId = value;
            }
        }

        /// <summary>DELETE_RESERVE_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_RESERVE_FLAG")]
        public int? DeleteReserveFlag {
            get { return _deleteReserveFlag; }
            set {
                __modifiedProperties.AddPropertyName("DeleteReserveFlag");
                _deleteReserveFlag = value;
            }
        }

        /// <summary>DESCRIPTION: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("DESCRIPTION")]
        public String Description {
            get { return _description; }
            set {
                __modifiedProperties.AddPropertyName("Description");
                _description = value;
            }
        }

        /// <summary>EDIT_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_FLAG")]
        public int? EditFlag {
            get { return _editFlag; }
            set {
                __modifiedProperties.AddPropertyName("EditFlag");
                _editFlag = value;
            }
        }

        /// <summary>TARGET_CSV: {VARCHAR2(4000)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_CSV")]
        public String TargetCsv {
            get { return _targetCsv; }
            set {
                __modifiedProperties.AddPropertyName("TargetCsv");
                _targetCsv = value;
            }
        }

        /// <summary>CONDITION_CSV: {VARCHAR2(4000)}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_CSV")]
        public String ConditionCsv {
            get { return _conditionCsv; }
            set {
                __modifiedProperties.AddPropertyName("ConditionCsv");
                _conditionCsv = value;
            }
        }

        /// <summary>FORMULA_STRING: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FORMULA_STRING")]
        public String FormulaString {
            get { return _formulaString; }
            set {
                __modifiedProperties.AddPropertyName("FormulaString");
                _formulaString = value;
            }
        }

        #endregion
    }
}
