

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
    /// The entity of T_QCWEB_SURVEY_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, ADD_DATA_NO, SURVEY_NAME_ORG, IMPORT_DATETIME, IMPORT_FILE_NAME, DELETE_FLAG, VIEW_SURVEY_NAME, GT_COUNT, CROSS_COUNT, FA_COUNT, VERSION_NO, LAST_UPDATE_USER, LAST_UPDATE_DATETIME, SURVEY_INFO_ID, RAWDATA_IMPORT_QUE_INFO_ID, UTF8_FLAG
    /// 
    /// [sequence]
    ///     T_QCWeb_Survey_Info_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     VERSION_NO
    /// 
    /// [foreign-table]
    ///     T_SURVEY_INFO, T_RAWDATA_IMPORT_QUE_INFO, T_Allocation_Cell_Info, T_Select_Condition_Info, T_Item_Info, T_Table_Control, T_Default_Env, T_Default_Env_Color_Info, T_Scenario_Totalization, T_Reportset, T_Data_Edit_List, T_Output_Setting, T_Output_Request, T_Access_Permissions_Info, T_Session_Controler, T_Notice, T_Output_Setting_GT, T_Output_Setting_Cross, T_Output_Setting_FA, T_Output_Setting_Report, T_QCWEB_SURVEY_DETAIL, T_ACCESS_PERMISSIONS_INFO(AsOne), T_OUTPUT_SETTING(AsOne), T_OUTPUT_SETTING_CROSS(AsOne), T_OUTPUT_SETTING_FA(AsOne), T_OUTPUT_SETTING_GT(AsOne), T_OUTPUT_SETTING_REPORT(AsOne)
    /// 
    /// [referrer-table]
    ///     T_ALLOCATION_CELL_INFO, T_DATA_EDIT_LIST, T_ITEM_INFO, T_NOTICE, T_OUTPUT_REQUEST, T_OUTPUT_TEMPLATE, T_QCWEB_SURVEY_DETAIL, T_RAWDATA_IMPORT_QUE_INFO, T_REPORTSET, T_SCENARIO_TOTALIZATION, T_SELECT_CONDITION_INFO, T_SESSION_CONTROLER, T_WEIGHTBACK, T_ACCESS_PERMISSIONS_INFO, T_OUTPUT_SETTING, T_OUTPUT_SETTING_CROSS, T_OUTPUT_SETTING_FA, T_OUTPUT_SETTING_GT, T_OUTPUT_SETTING_REPORT
    /// 
    /// [foreign-property]
    ///     tSurveyInfo, tRawdataImportQueInfo, tAllocationCellInfo, tSelectConditionInfo, tItemInfo, tTableControl, tDefaultEnv, tDefaultEnvColorInfo, tScenarioTotalization, tReportset, tDataEditList, tOutputSetting, tOutputRequest, tAccessPermissionsInfo, tSessionControler, tNotice, tOutputSettingGt, tOutputSettingCross, tOutputSettingFa, tOutputSettingReport, tQcwebSurveyDetail, tAccessPermissionsInfoAsOne, tOutputSettingAsOne, tOutputSettingCrossAsOne, tOutputSettingFaAsOne, tOutputSettingGtAsOne, tOutputSettingReportAsOne
    /// 
    /// [referrer-property]
    ///     tAllocationCellInfoList, tDataEditListList, tItemInfoList, tNoticeList, tOutputRequestList, tOutputTemplateList, tQcwebSurveyDetailList, tRawdataImportQueInfoList, tReportsetList, tScenarioTotalizationList, tSelectConditionInfoList, tSessionControlerList, tWeightbackList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_QCWEB_SURVEY_INFO")]
    [Seasar.Dao.Attrs.VersionNoProperty("VersionNo")]
    [System.Serializable]
    public partial class TQcwebSurveyInfo : EntityDefinedCommonColumn {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_Allocation_Cell_Info}</summary>
        protected decimal? _qcwebid;

        /// <summary>ADD_DATA_NO: {NUMBER(10)}</summary>
        protected long? _addDataNo;

        /// <summary>SURVEY_NAME_ORG: {NotNull, NVARCHAR2(500)}</summary>
        protected String _surveyNameOrg;

        /// <summary>IMPORT_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _importDatetime;

        /// <summary>IMPORT_FILE_NAME: {NotNull, VARCHAR2(255)}</summary>
        protected String _importFileName;

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), classification=DeleteFlag}</summary>
        protected int? _deleteFlag;

        /// <summary>VIEW_SURVEY_NAME: {NotNull, NVARCHAR2(500)}</summary>
        protected String _viewSurveyName;

        /// <summary>GT_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _gtCount;

        /// <summary>CROSS_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _crossCount;

        /// <summary>FA_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _faCount;

        /// <summary>VERSION_NO: {NotNull, NUMBER(27)}</summary>
        protected decimal? _versionNo;

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        protected String _lastUpdateUser;

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastUpdateDatetime;

        /// <summary>SURVEY_INFO_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_SURVEY_INFO}</summary>
        protected decimal? _surveyInfoId;

        /// <summary>RAWDATA_IMPORT_QUE_INFO_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_RAWDATA_IMPORT_QUE_INFO}</summary>
        protected decimal? _rawdataImportQueInfoId;

        /// <summary>UTF8_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _utf8Flag;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();

        protected bool __canCommonColumnAutoSetup = true;
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_QCWEB_SURVEY_INFO"; } }
        public String TablePropertyName { get { return "TQcwebSurveyInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.DeleteFlag DeleteFlagAsDeleteFlag { get {
            return CDef.DeleteFlag.CodeOf(_deleteFlag);
        } set {
            DeleteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag Utf8FlagAsFlag { get {
            return CDef.Flag.CodeOf(_utf8Flag);
        } set {
            Utf8Flag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of deleteFlag as True.
        /// <![CDATA[
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_True() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.True;
        }

        /// <summary>
        /// Set the value of deleteFlag as False.
        /// <![CDATA[
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_False() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.False;
        }

        /// <summary>
        /// Set the value of utf8Flag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetUtf8Flag_True() {
            Utf8FlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of utf8Flag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetUtf8Flag_False() {
            Utf8FlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of deleteFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagTrue {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagFalse {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of utf8Flag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsUtf8FlagTrue {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of utf8Flag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsUtf8FlagFalse {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String DeleteFlagName {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String DeleteFlagAlias {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String Utf8FlagName {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String Utf8FlagAlias {
            get {
                CDef.Flag cls = Utf8FlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TSurveyInfo _tSurveyInfo;

        /// <summary>T_SURVEY_INFO as 'TSurveyInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("SURVEY_INFO_ID:SURVEY_INFO_ID")]
        public TSurveyInfo TSurveyInfo {
            get { return _tSurveyInfo; }
            set { _tSurveyInfo = value; }
        }

        protected TRawdataImportQueInfo _tRawdataImportQueInfo;

        /// <summary>T_RAWDATA_IMPORT_QUE_INFO as 'TRawdataImportQueInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("RAWDATA_IMPORT_QUE_INFO_ID:RAWDATA_IMPORT_QUE_INFO_ID")]
        public TRawdataImportQueInfo TRawdataImportQueInfo {
            get { return _tRawdataImportQueInfo; }
            set { _tRawdataImportQueInfo = value; }
        }

        protected TAllocationCellInfo _tAllocationCellInfo;

        /// <summary>T_ALLOCATION_CELL_INFO as 'TAllocationCellInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TAllocationCellInfo TAllocationCellInfo {
            get { return _tAllocationCellInfo; }
            set { _tAllocationCellInfo = value; }
        }

        protected TSelectConditionInfo _tSelectConditionInfo;

        /// <summary>T_SELECT_CONDITION_INFO as 'TSelectConditionInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(3), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TSelectConditionInfo TSelectConditionInfo {
            get { return _tSelectConditionInfo; }
            set { _tSelectConditionInfo = value; }
        }

        protected TItemInfo _tItemInfo;

        /// <summary>T_ITEM_INFO as 'TItemInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(4), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TItemInfo TItemInfo {
            get { return _tItemInfo; }
            set { _tItemInfo = value; }
        }

        protected TTableControl _tTableControl;

        /// <summary>T_TABLE_CONTROL as 'TTableControl'.</summary>
        [Seasar.Dao.Attrs.Relno(5), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TTableControl TTableControl {
            get { return _tTableControl; }
            set { _tTableControl = value; }
        }

        protected TDefaultEnv _tDefaultEnv;

        /// <summary>T_DEFAULT_ENV as 'TDefaultEnv'.</summary>
        [Seasar.Dao.Attrs.Relno(6), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TDefaultEnv TDefaultEnv {
            get { return _tDefaultEnv; }
            set { _tDefaultEnv = value; }
        }

        protected TDefaultEnvColorInfo _tDefaultEnvColorInfo;

        /// <summary>T_DEFAULT_ENV_COLOR_INFO as 'TDefaultEnvColorInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(7), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TDefaultEnvColorInfo TDefaultEnvColorInfo {
            get { return _tDefaultEnvColorInfo; }
            set { _tDefaultEnvColorInfo = value; }
        }

        protected TScenarioTotalization _tScenarioTotalization;

        /// <summary>T_SCENARIO_TOTALIZATION as 'TScenarioTotalization'.</summary>
        [Seasar.Dao.Attrs.Relno(8), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TScenarioTotalization TScenarioTotalization {
            get { return _tScenarioTotalization; }
            set { _tScenarioTotalization = value; }
        }

        protected TReportset _tReportset;

        /// <summary>T_REPORTSET as 'TReportset'.</summary>
        [Seasar.Dao.Attrs.Relno(9), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TReportset TReportset {
            get { return _tReportset; }
            set { _tReportset = value; }
        }

        protected TDataEditList _tDataEditList;

        /// <summary>T_DATA_EDIT_LIST as 'TDataEditList'.</summary>
        [Seasar.Dao.Attrs.Relno(10), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TDataEditList TDataEditList {
            get { return _tDataEditList; }
            set { _tDataEditList = value; }
        }

        protected TOutputSetting _tOutputSetting;

        /// <summary>T_OUTPUT_SETTING as 'TOutputSetting'.</summary>
        [Seasar.Dao.Attrs.Relno(11), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSetting TOutputSetting {
            get { return _tOutputSetting; }
            set { _tOutputSetting = value; }
        }

        protected TOutputRequest _tOutputRequest;

        /// <summary>T_OUTPUT_REQUEST as 'TOutputRequest'.</summary>
        [Seasar.Dao.Attrs.Relno(12), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputRequest TOutputRequest {
            get { return _tOutputRequest; }
            set { _tOutputRequest = value; }
        }

        protected TAccessPermissionsInfo _tAccessPermissionsInfo;

        /// <summary>T_ACCESS_PERMISSIONS_INFO as 'TAccessPermissionsInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(13), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TAccessPermissionsInfo TAccessPermissionsInfo {
            get { return _tAccessPermissionsInfo; }
            set { _tAccessPermissionsInfo = value; }
        }

        protected TSessionControler _tSessionControler;

        /// <summary>T_SESSION_CONTROLER as 'TSessionControler'.</summary>
        [Seasar.Dao.Attrs.Relno(14), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TSessionControler TSessionControler {
            get { return _tSessionControler; }
            set { _tSessionControler = value; }
        }

        protected TNotice _tNotice;

        /// <summary>T_NOTICE as 'TNotice'.</summary>
        [Seasar.Dao.Attrs.Relno(15), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TNotice TNotice {
            get { return _tNotice; }
            set { _tNotice = value; }
        }

        protected TOutputSettingGt _tOutputSettingGt;

        /// <summary>T_OUTPUT_SETTING_GT as 'TOutputSettingGt'.</summary>
        [Seasar.Dao.Attrs.Relno(16), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingGt TOutputSettingGt {
            get { return _tOutputSettingGt; }
            set { _tOutputSettingGt = value; }
        }

        protected TOutputSettingCross _tOutputSettingCross;

        /// <summary>T_OUTPUT_SETTING_CROSS as 'TOutputSettingCross'.</summary>
        [Seasar.Dao.Attrs.Relno(17), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingCross TOutputSettingCross {
            get { return _tOutputSettingCross; }
            set { _tOutputSettingCross = value; }
        }

        protected TOutputSettingFa _tOutputSettingFa;

        /// <summary>T_OUTPUT_SETTING_FA as 'TOutputSettingFa'.</summary>
        [Seasar.Dao.Attrs.Relno(18), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingFa TOutputSettingFa {
            get { return _tOutputSettingFa; }
            set { _tOutputSettingFa = value; }
        }

        protected TOutputSettingReport _tOutputSettingReport;

        /// <summary>T_OUTPUT_SETTING_REPORT as 'TOutputSettingReport'.</summary>
        [Seasar.Dao.Attrs.Relno(19), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingReport TOutputSettingReport {
            get { return _tOutputSettingReport; }
            set { _tOutputSettingReport = value; }
        }

        protected TQcwebSurveyDetail _tQcwebSurveyDetail;

        /// <summary>T_QCWEB_SURVEY_DETAIL as 'TQcwebSurveyDetail'.</summary>
        [Seasar.Dao.Attrs.Relno(20), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyDetail TQcwebSurveyDetail {
            get { return _tQcwebSurveyDetail; }
            set { _tQcwebSurveyDetail = value; }
        }

        protected TAccessPermissionsInfo _tAccessPermissionsInfoAsOne;

        /// <summary>T_ACCESS_PERMISSIONS_INFO as 'TAccessPermissionsInfoAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(21), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TAccessPermissionsInfo TAccessPermissionsInfoAsOne {
            get { return _tAccessPermissionsInfoAsOne; }
            set { _tAccessPermissionsInfoAsOne = value; }
        }

        protected TOutputSetting _tOutputSettingAsOne;

        /// <summary>T_OUTPUT_SETTING as 'TOutputSettingAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(22), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSetting TOutputSettingAsOne {
            get { return _tOutputSettingAsOne; }
            set { _tOutputSettingAsOne = value; }
        }

        protected TOutputSettingCross _tOutputSettingCrossAsOne;

        /// <summary>T_OUTPUT_SETTING_CROSS as 'TOutputSettingCrossAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(23), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingCross TOutputSettingCrossAsOne {
            get { return _tOutputSettingCrossAsOne; }
            set { _tOutputSettingCrossAsOne = value; }
        }

        protected TOutputSettingFa _tOutputSettingFaAsOne;

        /// <summary>T_OUTPUT_SETTING_FA as 'TOutputSettingFaAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(24), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingFa TOutputSettingFaAsOne {
            get { return _tOutputSettingFaAsOne; }
            set { _tOutputSettingFaAsOne = value; }
        }

        protected TOutputSettingGt _tOutputSettingGtAsOne;

        /// <summary>T_OUTPUT_SETTING_GT as 'TOutputSettingGtAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(25), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingGt TOutputSettingGtAsOne {
            get { return _tOutputSettingGtAsOne; }
            set { _tOutputSettingGtAsOne = value; }
        }

        protected TOutputSettingReport _tOutputSettingReportAsOne;

        /// <summary>T_OUTPUT_SETTING_REPORT as 'TOutputSettingReportAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(26), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSettingReport TOutputSettingReportAsOne {
            get { return _tOutputSettingReportAsOne; }
            set { _tOutputSettingReportAsOne = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TAllocationCellInfo> _tAllocationCellInfoList;

        /// <summary>T_ALLOCATION_CELL_INFO as 'TAllocationCellInfoList'.</summary>
        public IList<TAllocationCellInfo> TAllocationCellInfoList {
            get { if (_tAllocationCellInfoList == null) { _tAllocationCellInfoList = new List<TAllocationCellInfo>(); } return _tAllocationCellInfoList; }
            set { _tAllocationCellInfoList = value; }
        }

        protected IList<TDataEditList> _tDataEditListList;

        /// <summary>T_DATA_EDIT_LIST as 'TDataEditListList'.</summary>
        public IList<TDataEditList> TDataEditListList {
            get { if (_tDataEditListList == null) { _tDataEditListList = new List<TDataEditList>(); } return _tDataEditListList; }
            set { _tDataEditListList = value; }
        }

        protected IList<TItemInfo> _tItemInfoList;

        /// <summary>T_ITEM_INFO as 'TItemInfoList'.</summary>
        public IList<TItemInfo> TItemInfoList {
            get { if (_tItemInfoList == null) { _tItemInfoList = new List<TItemInfo>(); } return _tItemInfoList; }
            set { _tItemInfoList = value; }
        }

        protected IList<TNotice> _tNoticeList;

        /// <summary>T_NOTICE as 'TNoticeList'.</summary>
        public IList<TNotice> TNoticeList {
            get { if (_tNoticeList == null) { _tNoticeList = new List<TNotice>(); } return _tNoticeList; }
            set { _tNoticeList = value; }
        }

        protected IList<TOutputRequest> _tOutputRequestList;

        /// <summary>T_OUTPUT_REQUEST as 'TOutputRequestList'.</summary>
        public IList<TOutputRequest> TOutputRequestList {
            get { if (_tOutputRequestList == null) { _tOutputRequestList = new List<TOutputRequest>(); } return _tOutputRequestList; }
            set { _tOutputRequestList = value; }
        }

        protected IList<TOutputTemplate> _tOutputTemplateList;

        /// <summary>T_OUTPUT_TEMPLATE as 'TOutputTemplateList'.</summary>
        public IList<TOutputTemplate> TOutputTemplateList {
            get { if (_tOutputTemplateList == null) { _tOutputTemplateList = new List<TOutputTemplate>(); } return _tOutputTemplateList; }
            set { _tOutputTemplateList = value; }
        }

        protected IList<TQcwebSurveyDetail> _tQcwebSurveyDetailList;

        /// <summary>T_QCWEB_SURVEY_DETAIL as 'TQcwebSurveyDetailList'.</summary>
        public IList<TQcwebSurveyDetail> TQcwebSurveyDetailList {
            get { if (_tQcwebSurveyDetailList == null) { _tQcwebSurveyDetailList = new List<TQcwebSurveyDetail>(); } return _tQcwebSurveyDetailList; }
            set { _tQcwebSurveyDetailList = value; }
        }

        protected IList<TRawdataImportQueInfo> _tRawdataImportQueInfoList;

        /// <summary>T_RAWDATA_IMPORT_QUE_INFO as 'TRawdataImportQueInfoList'.</summary>
        public IList<TRawdataImportQueInfo> TRawdataImportQueInfoList {
            get { if (_tRawdataImportQueInfoList == null) { _tRawdataImportQueInfoList = new List<TRawdataImportQueInfo>(); } return _tRawdataImportQueInfoList; }
            set { _tRawdataImportQueInfoList = value; }
        }

        protected IList<TReportset> _tReportsetList;

        /// <summary>T_REPORTSET as 'TReportsetList'.</summary>
        public IList<TReportset> TReportsetList {
            get { if (_tReportsetList == null) { _tReportsetList = new List<TReportset>(); } return _tReportsetList; }
            set { _tReportsetList = value; }
        }

        protected IList<TScenarioTotalization> _tScenarioTotalizationList;

        /// <summary>T_SCENARIO_TOTALIZATION as 'TScenarioTotalizationList'.</summary>
        public IList<TScenarioTotalization> TScenarioTotalizationList {
            get { if (_tScenarioTotalizationList == null) { _tScenarioTotalizationList = new List<TScenarioTotalization>(); } return _tScenarioTotalizationList; }
            set { _tScenarioTotalizationList = value; }
        }

        protected IList<TSelectConditionInfo> _tSelectConditionInfoList;

        /// <summary>T_SELECT_CONDITION_INFO as 'TSelectConditionInfoList'.</summary>
        public IList<TSelectConditionInfo> TSelectConditionInfoList {
            get { if (_tSelectConditionInfoList == null) { _tSelectConditionInfoList = new List<TSelectConditionInfo>(); } return _tSelectConditionInfoList; }
            set { _tSelectConditionInfoList = value; }
        }

        protected IList<TSessionControler> _tSessionControlerList;

        /// <summary>T_SESSION_CONTROLER as 'TSessionControlerList'.</summary>
        public IList<TSessionControler> TSessionControlerList {
            get { if (_tSessionControlerList == null) { _tSessionControlerList = new List<TSessionControler>(); } return _tSessionControlerList; }
            set { _tSessionControlerList = value; }
        }

        protected IList<TWeightback> _tWeightbackList;

        /// <summary>T_WEIGHTBACK as 'TWeightbackList'.</summary>
        public IList<TWeightback> TWeightbackList {
            get { if (_tWeightbackList == null) { _tWeightbackList = new List<TWeightback>(); } return _tWeightbackList; }
            set { _tWeightbackList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_qcwebid == null) { return false; }
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
            if (other == null || !(other is TQcwebSurveyInfo)) { return false; }
            TQcwebSurveyInfo otherEntity = (TQcwebSurveyInfo)other;
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _qcwebid);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TQcwebSurveyInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tSurveyInfo, "TSurveyInfo")); }
            if (_tRawdataImportQueInfo != null)
            { sb.Append(l).Append(xbRDS(_tRawdataImportQueInfo, "TRawdataImportQueInfo")); }
            if (_tAllocationCellInfo != null)
            { sb.Append(l).Append(xbRDS(_tAllocationCellInfo, "TAllocationCellInfo")); }
            if (_tSelectConditionInfo != null)
            { sb.Append(l).Append(xbRDS(_tSelectConditionInfo, "TSelectConditionInfo")); }
            if (_tItemInfo != null)
            { sb.Append(l).Append(xbRDS(_tItemInfo, "TItemInfo")); }
            if (_tTableControl != null)
            { sb.Append(l).Append(xbRDS(_tTableControl, "TTableControl")); }
            if (_tDefaultEnv != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnv, "TDefaultEnv")); }
            if (_tDefaultEnvColorInfo != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnvColorInfo, "TDefaultEnvColorInfo")); }
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tReportset != null)
            { sb.Append(l).Append(xbRDS(_tReportset, "TReportset")); }
            if (_tDataEditList != null)
            { sb.Append(l).Append(xbRDS(_tDataEditList, "TDataEditList")); }
            if (_tOutputSetting != null)
            { sb.Append(l).Append(xbRDS(_tOutputSetting, "TOutputSetting")); }
            if (_tOutputRequest != null)
            { sb.Append(l).Append(xbRDS(_tOutputRequest, "TOutputRequest")); }
            if (_tAccessPermissionsInfo != null)
            { sb.Append(l).Append(xbRDS(_tAccessPermissionsInfo, "TAccessPermissionsInfo")); }
            if (_tSessionControler != null)
            { sb.Append(l).Append(xbRDS(_tSessionControler, "TSessionControler")); }
            if (_tNotice != null)
            { sb.Append(l).Append(xbRDS(_tNotice, "TNotice")); }
            if (_tOutputSettingGt != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingGt, "TOutputSettingGt")); }
            if (_tOutputSettingCross != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingCross, "TOutputSettingCross")); }
            if (_tOutputSettingFa != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingFa, "TOutputSettingFa")); }
            if (_tOutputSettingReport != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingReport, "TOutputSettingReport")); }
            if (_tQcwebSurveyDetail != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyDetail, "TQcwebSurveyDetail")); }
            if (_tAccessPermissionsInfoAsOne != null)
            { sb.Append(l).Append(xbRDS(_tAccessPermissionsInfoAsOne, "TAccessPermissionsInfoAsOne")); }
            if (_tOutputSettingAsOne != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingAsOne, "TOutputSettingAsOne")); }
            if (_tOutputSettingCrossAsOne != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingCrossAsOne, "TOutputSettingCrossAsOne")); }
            if (_tOutputSettingFaAsOne != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingFaAsOne, "TOutputSettingFaAsOne")); }
            if (_tOutputSettingGtAsOne != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingGtAsOne, "TOutputSettingGtAsOne")); }
            if (_tOutputSettingReportAsOne != null)
            { sb.Append(l).Append(xbRDS(_tOutputSettingReportAsOne, "TOutputSettingReportAsOne")); }
            if (_tAllocationCellInfoList != null) { foreach (Entity e in _tAllocationCellInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TAllocationCellInfoList")); } } }
            if (_tDataEditListList != null) { foreach (Entity e in _tDataEditListList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDataEditListList")); } } }
            if (_tItemInfoList != null) { foreach (Entity e in _tItemInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TItemInfoList")); } } }
            if (_tNoticeList != null) { foreach (Entity e in _tNoticeList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TNoticeList")); } } }
            if (_tOutputRequestList != null) { foreach (Entity e in _tOutputRequestList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputRequestList")); } } }
            if (_tOutputTemplateList != null) { foreach (Entity e in _tOutputTemplateList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputTemplateList")); } } }
            if (_tQcwebSurveyDetailList != null) { foreach (Entity e in _tQcwebSurveyDetailList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TQcwebSurveyDetailList")); } } }
            if (_tRawdataImportQueInfoList != null) { foreach (Entity e in _tRawdataImportQueInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TRawdataImportQueInfoList")); } } }
            if (_tReportsetList != null) { foreach (Entity e in _tReportsetList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TReportsetList")); } } }
            if (_tScenarioTotalizationList != null) { foreach (Entity e in _tScenarioTotalizationList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TScenarioTotalizationList")); } } }
            if (_tSelectConditionInfoList != null) { foreach (Entity e in _tSelectConditionInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TSelectConditionInfoList")); } } }
            if (_tSessionControlerList != null) { foreach (Entity e in _tSessionControlerList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TSessionControlerList")); } } }
            if (_tWeightbackList != null) { foreach (Entity e in _tWeightbackList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TWeightbackList")); } } }
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.AddDataNo);
            sb.Append(c).Append(this.SurveyNameOrg);
            sb.Append(c).Append(this.ImportDatetime);
            sb.Append(c).Append(this.ImportFileName);
            sb.Append(c).Append(this.DeleteFlag);
            sb.Append(c).Append(this.ViewSurveyName);
            sb.Append(c).Append(this.GtCount);
            sb.Append(c).Append(this.CrossCount);
            sb.Append(c).Append(this.FaCount);
            sb.Append(c).Append(this.VersionNo);
            sb.Append(c).Append(this.LastUpdateUser);
            sb.Append(c).Append(this.LastUpdateDatetime);
            sb.Append(c).Append(this.SurveyInfoId);
            sb.Append(c).Append(this.RawdataImportQueInfoId);
            sb.Append(c).Append(this.Utf8Flag);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tSurveyInfo != null) { sb.Append(c).Append("TSurveyInfo"); }
            if (_tRawdataImportQueInfo != null) { sb.Append(c).Append("TRawdataImportQueInfo"); }
            if (_tAllocationCellInfo != null) { sb.Append(c).Append("TAllocationCellInfo"); }
            if (_tSelectConditionInfo != null) { sb.Append(c).Append("TSelectConditionInfo"); }
            if (_tItemInfo != null) { sb.Append(c).Append("TItemInfo"); }
            if (_tTableControl != null) { sb.Append(c).Append("TTableControl"); }
            if (_tDefaultEnv != null) { sb.Append(c).Append("TDefaultEnv"); }
            if (_tDefaultEnvColorInfo != null) { sb.Append(c).Append("TDefaultEnvColorInfo"); }
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tReportset != null) { sb.Append(c).Append("TReportset"); }
            if (_tDataEditList != null) { sb.Append(c).Append("TDataEditList"); }
            if (_tOutputSetting != null) { sb.Append(c).Append("TOutputSetting"); }
            if (_tOutputRequest != null) { sb.Append(c).Append("TOutputRequest"); }
            if (_tAccessPermissionsInfo != null) { sb.Append(c).Append("TAccessPermissionsInfo"); }
            if (_tSessionControler != null) { sb.Append(c).Append("TSessionControler"); }
            if (_tNotice != null) { sb.Append(c).Append("TNotice"); }
            if (_tOutputSettingGt != null) { sb.Append(c).Append("TOutputSettingGt"); }
            if (_tOutputSettingCross != null) { sb.Append(c).Append("TOutputSettingCross"); }
            if (_tOutputSettingFa != null) { sb.Append(c).Append("TOutputSettingFa"); }
            if (_tOutputSettingReport != null) { sb.Append(c).Append("TOutputSettingReport"); }
            if (_tQcwebSurveyDetail != null) { sb.Append(c).Append("TQcwebSurveyDetail"); }
            if (_tAccessPermissionsInfoAsOne != null) { sb.Append(c).Append("TAccessPermissionsInfoAsOne"); }
            if (_tOutputSettingAsOne != null) { sb.Append(c).Append("TOutputSettingAsOne"); }
            if (_tOutputSettingCrossAsOne != null) { sb.Append(c).Append("TOutputSettingCrossAsOne"); }
            if (_tOutputSettingFaAsOne != null) { sb.Append(c).Append("TOutputSettingFaAsOne"); }
            if (_tOutputSettingGtAsOne != null) { sb.Append(c).Append("TOutputSettingGtAsOne"); }
            if (_tOutputSettingReportAsOne != null) { sb.Append(c).Append("TOutputSettingReportAsOne"); }
            if (_tAllocationCellInfoList != null && _tAllocationCellInfoList.Count > 0)
            { sb.Append(c).Append("TAllocationCellInfoList"); }
            if (_tDataEditListList != null && _tDataEditListList.Count > 0)
            { sb.Append(c).Append("TDataEditListList"); }
            if (_tItemInfoList != null && _tItemInfoList.Count > 0)
            { sb.Append(c).Append("TItemInfoList"); }
            if (_tNoticeList != null && _tNoticeList.Count > 0)
            { sb.Append(c).Append("TNoticeList"); }
            if (_tOutputRequestList != null && _tOutputRequestList.Count > 0)
            { sb.Append(c).Append("TOutputRequestList"); }
            if (_tOutputTemplateList != null && _tOutputTemplateList.Count > 0)
            { sb.Append(c).Append("TOutputTemplateList"); }
            if (_tQcwebSurveyDetailList != null && _tQcwebSurveyDetailList.Count > 0)
            { sb.Append(c).Append("TQcwebSurveyDetailList"); }
            if (_tRawdataImportQueInfoList != null && _tRawdataImportQueInfoList.Count > 0)
            { sb.Append(c).Append("TRawdataImportQueInfoList"); }
            if (_tReportsetList != null && _tReportsetList.Count > 0)
            { sb.Append(c).Append("TReportsetList"); }
            if (_tScenarioTotalizationList != null && _tScenarioTotalizationList.Count > 0)
            { sb.Append(c).Append("TScenarioTotalizationList"); }
            if (_tSelectConditionInfoList != null && _tSelectConditionInfoList.Count > 0)
            { sb.Append(c).Append("TSelectConditionInfoList"); }
            if (_tSessionControlerList != null && _tSessionControlerList.Count > 0)
            { sb.Append(c).Append("TSessionControlerList"); }
            if (_tWeightbackList != null && _tWeightbackList.Count > 0)
            { sb.Append(c).Append("TWeightbackList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27), FK to T_Allocation_Cell_Info}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>ADD_DATA_NO: {NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("ADD_DATA_NO")]
        public long? AddDataNo {
            get { return _addDataNo; }
            set {
                __modifiedProperties.AddPropertyName("AddDataNo");
                _addDataNo = value;
            }
        }

        /// <summary>SURVEY_NAME_ORG: {NotNull, NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_NAME_ORG")]
        public String SurveyNameOrg {
            get { return _surveyNameOrg; }
            set {
                __modifiedProperties.AddPropertyName("SurveyNameOrg");
                _surveyNameOrg = value;
            }
        }

        /// <summary>IMPORT_DATETIME: {NotNull, TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("IMPORT_DATETIME")]
        public DateTime? ImportDatetime {
            get { return _importDatetime; }
            set {
                __modifiedProperties.AddPropertyName("ImportDatetime");
                _importDatetime = value;
            }
        }

        /// <summary>IMPORT_FILE_NAME: {NotNull, VARCHAR2(255)}</summary>
        [Seasar.Dao.Attrs.Column("IMPORT_FILE_NAME")]
        public String ImportFileName {
            get { return _importFileName; }
            set {
                __modifiedProperties.AddPropertyName("ImportFileName");
                _importFileName = value;
            }
        }

        /// <summary>DELETE_FLAG: {NotNull, NUMBER(1), classification=DeleteFlag}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_FLAG")]
        public int? DeleteFlag {
            get { return _deleteFlag; }
            set {
                __modifiedProperties.AddPropertyName("DeleteFlag");
                _deleteFlag = value;
            }
        }

        /// <summary>VIEW_SURVEY_NAME: {NotNull, NVARCHAR2(500)}</summary>
        [Seasar.Dao.Attrs.Column("VIEW_SURVEY_NAME")]
        public String ViewSurveyName {
            get { return _viewSurveyName; }
            set {
                __modifiedProperties.AddPropertyName("ViewSurveyName");
                _viewSurveyName = value;
            }
        }

        /// <summary>GT_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("GT_COUNT")]
        public int? GtCount {
            get { return _gtCount; }
            set {
                __modifiedProperties.AddPropertyName("GtCount");
                _gtCount = value;
            }
        }

        /// <summary>CROSS_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_COUNT")]
        public int? CrossCount {
            get { return _crossCount; }
            set {
                __modifiedProperties.AddPropertyName("CrossCount");
                _crossCount = value;
            }
        }

        /// <summary>FA_COUNT: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("FA_COUNT")]
        public int? FaCount {
            get { return _faCount; }
            set {
                __modifiedProperties.AddPropertyName("FaCount");
                _faCount = value;
            }
        }

        /// <summary>VERSION_NO: {NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("VERSION_NO")]
        public decimal? VersionNo {
            get { return _versionNo; }
            set {
                __modifiedProperties.AddPropertyName("VersionNo");
                _versionNo = value;
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

        /// <summary>SURVEY_INFO_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_SURVEY_INFO}</summary>
        [Seasar.Dao.Attrs.Column("SURVEY_INFO_ID")]
        public decimal? SurveyInfoId {
            get { return _surveyInfoId; }
            set {
                __modifiedProperties.AddPropertyName("SurveyInfoId");
                _surveyInfoId = value;
            }
        }

        /// <summary>RAWDATA_IMPORT_QUE_INFO_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_RAWDATA_IMPORT_QUE_INFO}</summary>
        [Seasar.Dao.Attrs.Column("RAWDATA_IMPORT_QUE_INFO_ID")]
        public decimal? RawdataImportQueInfoId {
            get { return _rawdataImportQueInfoId; }
            set {
                __modifiedProperties.AddPropertyName("RawdataImportQueInfoId");
                _rawdataImportQueInfoId = value;
            }
        }

        /// <summary>UTF8_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("UTF8_FLAG")]
        public int? Utf8Flag {
            get { return _utf8Flag; }
            set {
                __modifiedProperties.AddPropertyName("Utf8Flag");
                _utf8Flag = value;
            }
        }

        #endregion
    }
}
