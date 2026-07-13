

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
    /// The entity of T_DATA_EDIT_LIST as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     DATA_EDIT_ID
    /// 
    /// [column]
    ///     DATA_EDIT_ID, QCWEBID, EXECUTE_NO, EXECUTE_FLAG, EDIT_MENU_MASTER_ID, DESCRIPTION, CONDITION_ITEM_VIEW_NAME, TARGET_ITEM_VIEW_NAME, STATUS, LATEST_FLAG, DERIVED_DATA_EDIT_ID, DELETE_RESERVE_FLAG, LAST_UPDATE_USER, LAST_UPDATE_DATETIME, EDIT_FLAG
    /// 
    /// [sequence]
    ///     T_Data_Edit_List_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_QCWEB_SURVEY_INFO, T_EDIT_MENU_MASTER, T_DATA_PROCESS_NEW_ITEM(AsOne), T_DELETE_DATA(AsOne), T_EDIT_DATA(AsOne)
    /// 
    /// [referrer-table]
    ///     T_ITEM_INFO, T_DATA_PROCESS_NEW_ITEM, T_DELETE_DATA, T_EDIT_DATA
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tEditMenuMaster, tDataProcessNewItemAsOne, tDeleteDataAsOne, tEditDataAsOne
    /// 
    /// [referrer-property]
    ///     tItemInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DATA_EDIT_LIST")]
    [System.Serializable]
    public partial class TDataEditList : EntityDefinedCommonColumn {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _dataEditId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>EXECUTE_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _executeNo;

        /// <summary>EXECUTE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _executeFlag;

        /// <summary>EDIT_MENU_MASTER_ID: {IX, NotNull, NUMBER(2), FK to T_EDIT_MENU_MASTER}</summary>
        protected int? _editMenuMasterId;

        /// <summary>DESCRIPTION: {NVARCHAR2(1000)}</summary>
        protected String _description;

        /// <summary>CONDITION_ITEM_VIEW_NAME: {NVARCHAR2(500)}</summary>
        protected String _conditionItemViewName;

        /// <summary>TARGET_ITEM_VIEW_NAME: {NVARCHAR2(500)}</summary>
        protected String _targetItemViewName;

        /// <summary>STATUS: {NotNull, VARCHAR2(2)}</summary>
        protected String _status;

        /// <summary>LATEST_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _latestFlag;

        /// <summary>DERIVED_DATA_EDIT_ID: {NUMBER(27)}</summary>
        protected decimal? _derivedDataEditId;

        /// <summary>DELETE_RESERVE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _deleteReserveFlag;

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        protected String _lastUpdateUser;

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastUpdateDatetime;

        /// <summary>EDIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _editFlag;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();

        protected bool __canCommonColumnAutoSetup = true;
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DATA_EDIT_LIST"; } }
        public String TablePropertyName { get { return "TDataEditList"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

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
        protected TQcwebSurveyInfo _tQcwebSurveyInfo;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfo {
            get { return _tQcwebSurveyInfo; }
            set { _tQcwebSurveyInfo = value; }
        }

        protected TEditMenuMaster _tEditMenuMaster;

        /// <summary>T_EDIT_MENU_MASTER as 'TEditMenuMaster'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("EDIT_MENU_MASTER_ID:EDIT_MENU_MASTER_ID")]
        public TEditMenuMaster TEditMenuMaster {
            get { return _tEditMenuMaster; }
            set { _tEditMenuMaster = value; }
        }

        protected TDataProcessNewItem _tDataProcessNewItemAsOne;

        /// <summary>T_DATA_PROCESS_NEW_ITEM as 'TDataProcessNewItemAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDataProcessNewItem TDataProcessNewItemAsOne {
            get { return _tDataProcessNewItemAsOne; }
            set { _tDataProcessNewItemAsOne = value; }
        }

        protected TDeleteData _tDeleteDataAsOne;

        /// <summary>T_DELETE_DATA as 'TDeleteDataAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(3), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDeleteData TDeleteDataAsOne {
            get { return _tDeleteDataAsOne; }
            set { _tDeleteDataAsOne = value; }
        }

        protected TEditData _tEditDataAsOne;

        /// <summary>T_EDIT_DATA as 'TEditDataAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(4), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TEditData TEditDataAsOne {
            get { return _tEditDataAsOne; }
            set { _tEditDataAsOne = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TItemInfo> _tItemInfoList;

        /// <summary>T_ITEM_INFO as 'TItemInfoList'.</summary>
        public IList<TItemInfo> TItemInfoList {
            get { if (_tItemInfoList == null) { _tItemInfoList = new List<TItemInfo>(); } return _tItemInfoList; }
            set { _tItemInfoList = value; }
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
        //                                                          Common Column Handling
        //                                                          ======================
        public virtual void EnableCommonColumnAutoSetup() {
            __canCommonColumnAutoSetup = true;
        }

        public virtual void DisableCommonColumnAutoSetup() {
            __canCommonColumnAutoSetup = false;
        }

        public virtual bool CanCommonColumnAutoSetup() {// for Framework
            return __canCommonColumnAutoSetup;
        }

        // ===============================================================================
        //                                                                  Basic Override
        //                                                                  ==============
        #region Basic Override
        public override bool Equals(Object other) {
            if (other == null || !(other is TDataEditList)) { return false; }
            TDataEditList otherEntity = (TDataEditList)other;
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
            return "TDataEditList:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tEditMenuMaster != null)
            { sb.Append(l).Append(xbRDS(_tEditMenuMaster, "TEditMenuMaster")); }
            if (_tDataProcessNewItemAsOne != null)
            { sb.Append(l).Append(xbRDS(_tDataProcessNewItemAsOne, "TDataProcessNewItemAsOne")); }
            if (_tDeleteDataAsOne != null)
            { sb.Append(l).Append(xbRDS(_tDeleteDataAsOne, "TDeleteDataAsOne")); }
            if (_tEditDataAsOne != null)
            { sb.Append(l).Append(xbRDS(_tEditDataAsOne, "TEditDataAsOne")); }
            if (_tItemInfoList != null) { foreach (Entity e in _tItemInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TItemInfoList")); } } }
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.ExecuteNo);
            sb.Append(c).Append(this.ExecuteFlag);
            sb.Append(c).Append(this.EditMenuMasterId);
            sb.Append(c).Append(this.Description);
            sb.Append(c).Append(this.ConditionItemViewName);
            sb.Append(c).Append(this.TargetItemViewName);
            sb.Append(c).Append(this.Status);
            sb.Append(c).Append(this.LatestFlag);
            sb.Append(c).Append(this.DerivedDataEditId);
            sb.Append(c).Append(this.DeleteReserveFlag);
            sb.Append(c).Append(this.LastUpdateUser);
            sb.Append(c).Append(this.LastUpdateDatetime);
            sb.Append(c).Append(this.EditFlag);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tEditMenuMaster != null) { sb.Append(c).Append("TEditMenuMaster"); }
            if (_tDataProcessNewItemAsOne != null) { sb.Append(c).Append("TDataProcessNewItemAsOne"); }
            if (_tDeleteDataAsOne != null) { sb.Append(c).Append("TDeleteDataAsOne"); }
            if (_tEditDataAsOne != null) { sb.Append(c).Append("TEditDataAsOne"); }
            if (_tItemInfoList != null && _tItemInfoList.Count > 0)
            { sb.Append(c).Append("TItemInfoList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>DATA_EDIT_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
            }
        }

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>EXECUTE_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("EXECUTE_NO")]
        public int? ExecuteNo {
            get { return _executeNo; }
            set {
                __modifiedProperties.AddPropertyName("ExecuteNo");
                _executeNo = value;
            }
        }

        /// <summary>EXECUTE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("EXECUTE_FLAG")]
        public int? ExecuteFlag {
            get { return _executeFlag; }
            set {
                __modifiedProperties.AddPropertyName("ExecuteFlag");
                _executeFlag = value;
            }
        }

        /// <summary>EDIT_MENU_MASTER_ID: {IX, NotNull, NUMBER(2), FK to T_EDIT_MENU_MASTER}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_MENU_MASTER_ID")]
        public int? EditMenuMasterId {
            get { return _editMenuMasterId; }
            set {
                __modifiedProperties.AddPropertyName("EditMenuMasterId");
                _editMenuMasterId = value;
            }
        }

        /// <summary>DESCRIPTION: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("DESCRIPTION")]
        public String Description {
            get { return _description; }
            set {
                __modifiedProperties.AddPropertyName("Description");
                _description = value;
            }
        }

        /// <summary>CONDITION_ITEM_VIEW_NAME: {NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_ITEM_VIEW_NAME")]
        public String ConditionItemViewName {
            get { return _conditionItemViewName; }
            set {
                __modifiedProperties.AddPropertyName("ConditionItemViewName");
                _conditionItemViewName = value;
            }
        }

        /// <summary>TARGET_ITEM_VIEW_NAME: {NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_ITEM_VIEW_NAME")]
        public String TargetItemViewName {
            get { return _targetItemViewName; }
            set {
                __modifiedProperties.AddPropertyName("TargetItemViewName");
                _targetItemViewName = value;
            }
        }

        /// <summary>STATUS: {NotNull, VARCHAR2(2)}</summary>
        [Seasar.Dao.Attrs.Column("STATUS")]
        public String Status {
            get { return _status; }
            set {
                __modifiedProperties.AddPropertyName("Status");
                _status = value;
            }
        }

        /// <summary>LATEST_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
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

        /// <summary>DELETE_RESERVE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_RESERVE_FLAG")]
        public int? DeleteReserveFlag {
            get { return _deleteReserveFlag; }
            set {
                __modifiedProperties.AddPropertyName("DeleteReserveFlag");
                _deleteReserveFlag = value;
            }
        }

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LAST_UPDATE_USER")]
        public String LastUpdateUser {
            get { return _lastUpdateUser; }
            set {
                __modifiedProperties.AddPropertyName("LastUpdateUser");
                _lastUpdateUser = value;
            }
        }

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("LAST_UPDATE_DATETIME")]
        public DateTime? LastUpdateDatetime {
            get { return _lastUpdateDatetime; }
            set {
                __modifiedProperties.AddPropertyName("LastUpdateDatetime");
                _lastUpdateDatetime = value;
            }
        }

        /// <summary>EDIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("EDIT_FLAG")]
        public int? EditFlag {
            get { return _editFlag; }
            set {
                __modifiedProperties.AddPropertyName("EditFlag");
                _editFlag = value;
            }
        }

        #endregion
    }
}
