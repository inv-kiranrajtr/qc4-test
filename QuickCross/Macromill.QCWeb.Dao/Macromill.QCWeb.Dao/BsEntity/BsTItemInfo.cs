

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
    /// The entity of T_ITEM_INFO as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     ITEM_INFO_ID
    /// 
    /// [column]
    ///     ITEM_INFO_ID, QCWEBID, ITEM_NAME, SOURCE_DIV, ITEMNO, ITEM_TYPE, ANSWER_TYPE, SORT_NUMBER, MATRIX_DIV, LV1TITLE, LV2TITLE, ORIGINAL_LV1TITLE, ORIGINAL_LV2TITLE, TABLE_NAME, COLUMN_NAME, CATEGORY_EDIT_ID, DATA_EDIT_ID, STATUS, TABLE_NAME_ORG, COLUMN_NAME_ORG, COMPEL_ITEM_CHANGE_FLAG, SORT_FLAG, SORT_RANGE, MULTIVARIATE_FLAG, TABLE_NO, COLUMN_NO, TABLE_NO_ORG, COLUMN_NO_ORG, LAST_UPDATE_USER, LAST_UPDATE_DATETIME, NEW_AT_QC3_FLAG, SORT_RANGE_ORG
    /// 
    /// [sequence]
    ///     T_Item_Info_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_QCWEB_SURVEY_INFO, T_Matrix_Info, T_FA_List_Add_Item, T_FA_Scenario_Item, T_TABLE_CONTROL, T_SCENARIO_TOTALIZATION, T_DATA_EDIT_LIST
    /// 
    /// [referrer-table]
    ///     T_CATEGORY_INFO, T_MATRIX_INFO, T_SCENARIO_QUERYLIST, T_GT_SCENARIO_ITEM, T_FA_SCENARIO_ITEM, T_FA_LIST_ADD_ITEM, T_GT_MATRIX_CHILD
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tMatrixInfo, tFaListAddItem, tFaScenarioItem, tTableControl, tScenarioTotalization, tDataEditList
    /// 
    /// [referrer-property]
    ///     tCategoryInfoList, tMatrixInfoByItemInfoIdList, tMatrixInfoByChildItemInfoIdList, tScenarioQuerylistList, tGtScenarioItemList, tFaScenarioItemList, tFaListAddItemList, tGtMatrixChildList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_ITEM_INFO")]
    [System.Serializable]
    public partial class TItemInfo : EntityDefinedCommonColumn {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>ITEM_INFO_ID: {PK, NotNull, NUMBER(27), FK to T_Matrix_Info}</summary>
        protected decimal? _itemInfoId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>ITEM_NAME: {NotNull, NVARCHAR2(26)}</summary>
        protected String _itemName;

        /// <summary>SOURCE_DIV: {NotNull, CHAR(1), classification=SourceDiv}</summary>
        protected String _sourceDiv;

        /// <summary>ITEMNO: {NVARCHAR2(26)}</summary>
        protected String _itemno;

        /// <summary>ITEM_TYPE: {VARCHAR2(3), classification=ItemType}</summary>
        protected String _itemType;

        /// <summary>ANSWER_TYPE: {NotNull, CHAR(1), classification=AnswerType}</summary>
        protected String _answerType;

        /// <summary>SORT_NUMBER: {NotNull, NUMBER(5)}</summary>
        protected int? _sortNumber;

        /// <summary>MATRIX_DIV: {NotNull, NUMBER(1), classification=MatrixType}</summary>
        protected int? _matrixDiv;

        /// <summary>LV1TITLE: {NVARCHAR2(1000)}</summary>
        protected String _lv1title;

        /// <summary>LV2TITLE: {NVARCHAR2(1000)}</summary>
        protected String _lv2title;

        /// <summary>ORIGINAL_LV1TITLE: {NVARCHAR2(1000)}</summary>
        protected String _originalLv1title;

        /// <summary>ORIGINAL_LV2TITLE: {NVARCHAR2(1000)}</summary>
        protected String _originalLv2title;

        /// <summary>TABLE_NAME: {VARCHAR2(25)}</summary>
        protected String _tableName;

        /// <summary>COLUMN_NAME: {VARCHAR2(30)}</summary>
        protected String _columnName;

        /// <summary>CATEGORY_EDIT_ID: {IX, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _categoryEditId;

        /// <summary>DATA_EDIT_ID: {IX, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        protected decimal? _dataEditId;

        /// <summary>STATUS: {NotNull, NUMBER(1), default=[1], classification=ItemStatus}</summary>
        protected int? _status;

        /// <summary>TABLE_NAME_ORG: {VARCHAR2(25)}</summary>
        protected String _tableNameOrg;

        /// <summary>COLUMN_NAME_ORG: {VARCHAR2(30)}</summary>
        protected String _columnNameOrg;

        /// <summary>COMPEL_ITEM_CHANGE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _compelItemChangeFlag;

        /// <summary>SORT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _sortFlag;

        /// <summary>SORT_RANGE: {NUMBER(5)}</summary>
        protected int? _sortRange;

        /// <summary>MULTIVARIATE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _multivariateFlag;

        /// <summary>TABLE_NO: {NUMBER(2)}</summary>
        protected int? _tableNo;

        /// <summary>COLUMN_NO: {NUMBER(3)}</summary>
        protected int? _columnNo;

        /// <summary>TABLE_NO_ORG: {NUMBER(2)}</summary>
        protected int? _tableNoOrg;

        /// <summary>COLUMN_NO_ORG: {NUMBER(2)}</summary>
        protected int? _columnNoOrg;

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        protected String _lastUpdateUser;

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastUpdateDatetime;

        /// <summary>NEW_AT_QC3_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _newAtQc3Flag;

        /// <summary>SORT_RANGE_ORG: {NUMBER(5)}</summary>
        protected int? _sortRangeOrg;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();

        protected bool __canCommonColumnAutoSetup = true;
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_ITEM_INFO"; } }
        public String TablePropertyName { get { return "TItemInfo"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.SourceDiv SourceDivAsSourceDiv { get {
            return CDef.SourceDiv.CodeOf(_sourceDiv);
        } set {
            SourceDiv = value != null ? value.Code : null;
        }}

        public CDef.ItemType ItemTypeAsItemType { get {
            return CDef.ItemType.CodeOf(_itemType);
        } set {
            ItemType = value != null ? value.Code : null;
        }}

        public CDef.AnswerType AnswerTypeAsAnswerType { get {
            return CDef.AnswerType.CodeOf(_answerType);
        } set {
            AnswerType = value != null ? value.Code : null;
        }}

        public CDef.MatrixType MatrixDivAsMatrixType { get {
            return CDef.MatrixType.CodeOf(_matrixDiv);
        } set {
            MatrixDiv = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.ItemStatus StatusAsItemStatus { get {
            return CDef.ItemStatus.CodeOf(_status);
        } set {
            Status = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag CompelItemChangeFlagAsFlag { get {
            return CDef.Flag.CodeOf(_compelItemChangeFlag);
        } set {
            CompelItemChangeFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag SortFlagAsFlag { get {
            return CDef.Flag.CodeOf(_sortFlag);
        } set {
            SortFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag MultivariateFlagAsFlag { get {
            return CDef.Flag.CodeOf(_multivariateFlag);
        } set {
            MultivariateFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag NewAtQc3FlagAsFlag { get {
            return CDef.Flag.CodeOf(_newAtQc3Flag);
        } set {
            NewAtQc3Flag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of sourceDiv as Original.
        /// <![CDATA[
        /// Original: QC3から取り込まれたオリジナルデータを示す
        /// ]]>
        /// </summary>
        public void SetSourceDiv_Original() {
            SourceDivAsSourceDiv = CDef.SourceDiv.Original;
        }

        /// <summary>
        /// Set the value of sourceDiv as DataEdit.
        /// <![CDATA[
        /// 加工データ: データ加工で作成されたデータを示す
        /// ]]>
        /// </summary>
        public void SetSourceDiv_DataEdit() {
            SourceDivAsSourceDiv = CDef.SourceDiv.DataEdit;
        }

        /// <summary>
        /// Set the value of sourceDiv as ScenarioDataEdit.
        /// <![CDATA[
        /// シナリオ加工データ: シナリオ内の操作で作成されたデータを示す
        /// ]]>
        /// </summary>
        public void SetSourceDiv_ScenarioDataEdit() {
            SourceDivAsSourceDiv = CDef.SourceDiv.ScenarioDataEdit;
        }

        /// <summary>
        /// Set the value of itemType as SAR.
        /// <![CDATA[
        /// SAR: SARを示す
        /// ]]>
        /// </summary>
        public void SetItemType_SAR() {
            ItemTypeAsItemType = CDef.ItemType.SAR;
        }

        /// <summary>
        /// Set the value of itemType as SAS.
        /// <![CDATA[
        /// SAS: SASを示す
        /// ]]>
        /// </summary>
        public void SetItemType_SAS() {
            ItemTypeAsItemType = CDef.ItemType.SAS;
        }

        /// <summary>
        /// Set the value of itemType as SAP.
        /// <![CDATA[
        /// SAP: SAPを示す
        /// ]]>
        /// </summary>
        public void SetItemType_SAP() {
            ItemTypeAsItemType = CDef.ItemType.SAP;
        }

        /// <summary>
        /// Set the value of itemType as MAC.
        /// <![CDATA[
        /// MAC: MACを示す
        /// ]]>
        /// </summary>
        public void SetItemType_MAC() {
            ItemTypeAsItemType = CDef.ItemType.MAC;
        }

        /// <summary>
        /// Set the value of itemType as MTS.
        /// <![CDATA[
        /// MTS: MTSを示す
        /// ]]>
        /// </summary>
        public void SetItemType_MTS() {
            ItemTypeAsItemType = CDef.ItemType.MTS;
        }

        /// <summary>
        /// Set the value of itemType as MTM.
        /// <![CDATA[
        /// MTM: MTSを示す
        /// ]]>
        /// </summary>
        public void SetItemType_MTM() {
            ItemTypeAsItemType = CDef.ItemType.MTM;
        }

        /// <summary>
        /// Set the value of itemType as MTT.
        /// <![CDATA[
        /// MTT: MTTを示す
        /// ]]>
        /// </summary>
        public void SetItemType_MTT() {
            ItemTypeAsItemType = CDef.ItemType.MTT;
        }

        /// <summary>
        /// Set the value of itemType as RNK.
        /// <![CDATA[
        /// RNK: RNKを示す
        /// ]]>
        /// </summary>
        public void SetItemType_RNK() {
            ItemTypeAsItemType = CDef.ItemType.RNK;
        }

        /// <summary>
        /// Set the value of itemType as RAT.
        /// <![CDATA[
        /// RAT: RATを示す
        /// ]]>
        /// </summary>
        public void SetItemType_RAT() {
            ItemTypeAsItemType = CDef.ItemType.RAT;
        }

        /// <summary>
        /// Set the value of itemType as FAS.
        /// <![CDATA[
        /// FAS: FASを示す
        /// ]]>
        /// </summary>
        public void SetItemType_FAS() {
            ItemTypeAsItemType = CDef.ItemType.FAS;
        }

        /// <summary>
        /// Set the value of itemType as FAL.
        /// <![CDATA[
        /// FAL: FALを示す
        /// ]]>
        /// </summary>
        public void SetItemType_FAL() {
            ItemTypeAsItemType = CDef.ItemType.FAL;
        }

        /// <summary>
        /// Set the value of answerType as SA.
        /// <![CDATA[
        /// SA: SAを示す
        /// ]]>
        /// </summary>
        public void SetAnswerType_SA() {
            AnswerTypeAsAnswerType = CDef.AnswerType.SA;
        }

        /// <summary>
        /// Set the value of answerType as MA.
        /// <![CDATA[
        /// MA: MAを示す
        /// ]]>
        /// </summary>
        public void SetAnswerType_MA() {
            AnswerTypeAsAnswerType = CDef.AnswerType.MA;
        }

        /// <summary>
        /// Set the value of answerType as N.
        /// <![CDATA[
        /// N: Nを示す
        /// ]]>
        /// </summary>
        public void SetAnswerType_N() {
            AnswerTypeAsAnswerType = CDef.AnswerType.N;
        }

        /// <summary>
        /// Set the value of answerType as FA.
        /// <![CDATA[
        /// FA: FAを示す
        /// ]]>
        /// </summary>
        public void SetAnswerType_FA() {
            AnswerTypeAsAnswerType = CDef.AnswerType.FA;
        }

        /// <summary>
        /// Set the value of answerType as D.
        /// <![CDATA[
        /// D: Dを示す
        /// ]]>
        /// </summary>
        public void SetAnswerType_D() {
            AnswerTypeAsAnswerType = CDef.AnswerType.D;
        }

        /// <summary>
        /// Set the value of matrixDiv as NormalItem.
        /// <![CDATA[
        /// 通常アイテム: 通常アイテムを示す
        /// ]]>
        /// </summary>
        public void SetMatrixDiv_NormalItem() {
            MatrixDivAsMatrixType = CDef.MatrixType.NormalItem;
        }

        /// <summary>
        /// Set the value of matrixDiv as MatrixParent.
        /// <![CDATA[
        /// 親アイテム: 親アイテムを示す
        /// ]]>
        /// </summary>
        public void SetMatrixDiv_MatrixParent() {
            MatrixDivAsMatrixType = CDef.MatrixType.MatrixParent;
        }

        /// <summary>
        /// Set the value of matrixDiv as FirstChild.
        /// <![CDATA[
        /// 子マトリックス（親作成元アイテム）: 子マトリックス（親作成元アイテム）を示す
        /// ]]>
        /// </summary>
        public void SetMatrixDiv_FirstChild() {
            MatrixDivAsMatrixType = CDef.MatrixType.FirstChild;
        }

        /// <summary>
        /// Set the value of matrixDiv as MatrixChild.
        /// <![CDATA[
        /// 子マトリックス（通常子アイテム）: 子マトリックス（通常子アイテム）を示す
        /// ]]>
        /// </summary>
        public void SetMatrixDiv_MatrixChild() {
            MatrixDivAsMatrixType = CDef.MatrixType.MatrixChild;
        }

        /// <summary>
        /// Set the value of matrixDiv as SubFA.
        /// <![CDATA[
        /// 子マトリックス（付加FA）: 子マトリックス（付加FA）を示す
        /// ]]>
        /// </summary>
        public void SetMatrixDiv_SubFA() {
            MatrixDivAsMatrixType = CDef.MatrixType.SubFA;
        }

        /// <summary>
        /// Set the value of status as Invalid.
        /// <![CDATA[
        /// 無効(論理削除): 無効を示す
        /// ]]>
        /// </summary>
        public void SetStatus_Invalid() {
            StatusAsItemStatus = CDef.ItemStatus.Invalid;
        }

        /// <summary>
        /// Set the value of status as Effective.
        /// <![CDATA[
        /// 有効: 有効を示す
        /// ]]>
        /// </summary>
        public void SetStatus_Effective() {
            StatusAsItemStatus = CDef.ItemStatus.Effective;
        }

        /// <summary>
        /// Set the value of status as Temporary.
        /// <![CDATA[
        /// 仮登録: 仮登録を示す
        /// ]]>
        /// </summary>
        public void SetStatus_Temporary() {
            StatusAsItemStatus = CDef.ItemStatus.Temporary;
        }

        /// <summary>
        /// Set the value of compelItemChangeFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCompelItemChangeFlag_True() {
            CompelItemChangeFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of compelItemChangeFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCompelItemChangeFlag_False() {
            CompelItemChangeFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of sortFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetSortFlag_True() {
            SortFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of sortFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetSortFlag_False() {
            SortFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of multivariateFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetMultivariateFlag_True() {
            MultivariateFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of multivariateFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetMultivariateFlag_False() {
            MultivariateFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of newAtQc3Flag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetNewAtQc3Flag_True() {
            NewAtQc3FlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of newAtQc3Flag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetNewAtQc3Flag_False() {
            NewAtQc3FlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of sourceDiv 'Original'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// Original: QC3から取り込まれたオリジナルデータを示す
        /// ]]>
        /// </summary>
        public bool IsSourceDivOriginal {
            get {
                CDef.SourceDiv cls = SourceDivAsSourceDiv;
                return cls != null ? cls.Equals(CDef.SourceDiv.Original) : false;
            }
        }

        /// <summary>
        /// Is the value of sourceDiv 'DataEdit'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 加工データ: データ加工で作成されたデータを示す
        /// ]]>
        /// </summary>
        public bool IsSourceDivDataEdit {
            get {
                CDef.SourceDiv cls = SourceDivAsSourceDiv;
                return cls != null ? cls.Equals(CDef.SourceDiv.DataEdit) : false;
            }
        }

        /// <summary>
        /// Is the value of sourceDiv 'ScenarioDataEdit'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// シナリオ加工データ: シナリオ内の操作で作成されたデータを示す
        /// ]]>
        /// </summary>
        public bool IsSourceDivScenarioDataEdit {
            get {
                CDef.SourceDiv cls = SourceDivAsSourceDiv;
                return cls != null ? cls.Equals(CDef.SourceDiv.ScenarioDataEdit) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'SAR'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// SAR: SARを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeSAR {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.SAR) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'SAS'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// SAS: SASを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeSAS {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.SAS) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'SAP'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// SAP: SAPを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeSAP {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.SAP) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'MAC'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// MAC: MACを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeMAC {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.MAC) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'MTS'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// MTS: MTSを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeMTS {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.MTS) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'MTM'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// MTM: MTSを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeMTM {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.MTM) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'MTT'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// MTT: MTTを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeMTT {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.MTT) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'RNK'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// RNK: RNKを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeRNK {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.RNK) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'RAT'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// RAT: RATを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeRAT {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.RAT) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'FAS'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// FAS: FASを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeFAS {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.FAS) : false;
            }
        }

        /// <summary>
        /// Is the value of itemType 'FAL'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// FAL: FALを示す
        /// ]]>
        /// </summary>
        public bool IsItemTypeFAL {
            get {
                CDef.ItemType cls = ItemTypeAsItemType;
                return cls != null ? cls.Equals(CDef.ItemType.FAL) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'SA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// SA: SAを示す
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeSA {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.SA) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'MA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// MA: MAを示す
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeMA {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.MA) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'N'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// N: Nを示す
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeN {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.N) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'FA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// FA: FAを示す
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeFA {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.FA) : false;
            }
        }

        /// <summary>
        /// Is the value of answerType 'D'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// D: Dを示す
        /// ]]>
        /// </summary>
        public bool IsAnswerTypeD {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Equals(CDef.AnswerType.D) : false;
            }
        }

        /// <summary>
        /// Is the value of matrixDiv 'NormalItem'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 通常アイテム: 通常アイテムを示す
        /// ]]>
        /// </summary>
        public bool IsMatrixDivNormalItem {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Equals(CDef.MatrixType.NormalItem) : false;
            }
        }

        /// <summary>
        /// Is the value of matrixDiv 'MatrixParent'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 親アイテム: 親アイテムを示す
        /// ]]>
        /// </summary>
        public bool IsMatrixDivMatrixParent {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Equals(CDef.MatrixType.MatrixParent) : false;
            }
        }

        /// <summary>
        /// Is the value of matrixDiv 'FirstChild'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 子マトリックス（親作成元アイテム）: 子マトリックス（親作成元アイテム）を示す
        /// ]]>
        /// </summary>
        public bool IsMatrixDivFirstChild {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Equals(CDef.MatrixType.FirstChild) : false;
            }
        }

        /// <summary>
        /// Is the value of matrixDiv 'MatrixChild'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 子マトリックス（通常子アイテム）: 子マトリックス（通常子アイテム）を示す
        /// ]]>
        /// </summary>
        public bool IsMatrixDivMatrixChild {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Equals(CDef.MatrixType.MatrixChild) : false;
            }
        }

        /// <summary>
        /// Is the value of matrixDiv 'SubFA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 子マトリックス（付加FA）: 子マトリックス（付加FA）を示す
        /// ]]>
        /// </summary>
        public bool IsMatrixDivSubFA {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Equals(CDef.MatrixType.SubFA) : false;
            }
        }

        /// <summary>
        /// Is the value of status 'Invalid'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 無効(論理削除): 無効を示す
        /// ]]>
        /// </summary>
        public bool IsStatusInvalid {
            get {
                CDef.ItemStatus cls = StatusAsItemStatus;
                return cls != null ? cls.Equals(CDef.ItemStatus.Invalid) : false;
            }
        }

        /// <summary>
        /// Is the value of status 'Effective'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 有効: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsStatusEffective {
            get {
                CDef.ItemStatus cls = StatusAsItemStatus;
                return cls != null ? cls.Equals(CDef.ItemStatus.Effective) : false;
            }
        }

        /// <summary>
        /// Is the value of status 'Temporary'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// 仮登録: 仮登録を示す
        /// ]]>
        /// </summary>
        public bool IsStatusTemporary {
            get {
                CDef.ItemStatus cls = StatusAsItemStatus;
                return cls != null ? cls.Equals(CDef.ItemStatus.Temporary) : false;
            }
        }

        /// <summary>
        /// Is the value of compelItemChangeFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCompelItemChangeFlagTrue {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of compelItemChangeFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCompelItemChangeFlagFalse {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of sortFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsSortFlagTrue {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of sortFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsSortFlagFalse {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of multivariateFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsMultivariateFlagTrue {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of multivariateFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsMultivariateFlagFalse {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of newAtQc3Flag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsNewAtQc3FlagTrue {
            get {
                CDef.Flag cls = NewAtQc3FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of newAtQc3Flag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsNewAtQc3FlagFalse {
            get {
                CDef.Flag cls = NewAtQc3FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String SourceDivName {
            get {
                CDef.SourceDiv cls = SourceDivAsSourceDiv;
                return cls != null ? cls.Name : null;
            }
        }
        public String SourceDivAlias {
            get {
                CDef.SourceDiv cls = SourceDivAsSourceDiv;
                return cls != null ? cls.Alias : null;
            }
        }

        public String AnswerTypeName {
            get {
                CDef.AnswerType cls = AnswerTypeAsAnswerType;
                return cls != null ? cls.Name : null;
            }
        }
        public String MatrixDivName {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Name : null;
            }
        }
        public String MatrixDivAlias {
            get {
                CDef.MatrixType cls = MatrixDivAsMatrixType;
                return cls != null ? cls.Alias : null;
            }
        }

        public String StatusName {
            get {
                CDef.ItemStatus cls = StatusAsItemStatus;
                return cls != null ? cls.Name : null;
            }
        }
        public String StatusAlias {
            get {
                CDef.ItemStatus cls = StatusAsItemStatus;
                return cls != null ? cls.Alias : null;
            }
        }

        public String CompelItemChangeFlagName {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CompelItemChangeFlagAlias {
            get {
                CDef.Flag cls = CompelItemChangeFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String SortFlagName {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String SortFlagAlias {
            get {
                CDef.Flag cls = SortFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String MultivariateFlagName {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String MultivariateFlagAlias {
            get {
                CDef.Flag cls = MultivariateFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String NewAtQc3FlagName {
            get {
                CDef.Flag cls = NewAtQc3FlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String NewAtQc3FlagAlias {
            get {
                CDef.Flag cls = NewAtQc3FlagAsFlag;
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

        protected TMatrixInfo _tMatrixInfo;

        /// <summary>T_MATRIX_INFO as 'TMatrixInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:CHILD_ITEM_INFO_ID")]
        public TMatrixInfo TMatrixInfo {
            get { return _tMatrixInfo; }
            set { _tMatrixInfo = value; }
        }

        protected TFaListAddItem _tFaListAddItem;

        /// <summary>T_FA_LIST_ADD_ITEM as 'TFaListAddItem'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:ITEM_INFO_ID")]
        public TFaListAddItem TFaListAddItem {
            get { return _tFaListAddItem; }
            set { _tFaListAddItem = value; }
        }

        protected TFaScenarioItem _tFaScenarioItem;

        /// <summary>T_FA_SCENARIO_ITEM as 'TFaScenarioItem'.</summary>
        [Seasar.Dao.Attrs.Relno(3), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:FA_TARGET_ITEM_ID")]
        public TFaScenarioItem TFaScenarioItem {
            get { return _tFaScenarioItem; }
            set { _tFaScenarioItem = value; }
        }

        protected TTableControl _tTableControl;

        /// <summary>T_TABLE_CONTROL as 'TTableControl'.</summary>
        [Seasar.Dao.Attrs.Relno(4), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TTableControl TTableControl {
            get { return _tTableControl; }
            set { _tTableControl = value; }
        }

        protected TScenarioTotalization _tScenarioTotalization;

        /// <summary>T_SCENARIO_TOTALIZATION as 'TScenarioTotalization'.</summary>
        [Seasar.Dao.Attrs.Relno(5), Seasar.Dao.Attrs.Relkeys("CATEGORY_EDIT_ID:SCENARIO_TOTALIZATION_ID")]
        public TScenarioTotalization TScenarioTotalization {
            get { return _tScenarioTotalization; }
            set { _tScenarioTotalization = value; }
        }

        protected TDataEditList _tDataEditList;

        /// <summary>T_DATA_EDIT_LIST as 'TDataEditList'.</summary>
        [Seasar.Dao.Attrs.Relno(6), Seasar.Dao.Attrs.Relkeys("DATA_EDIT_ID:DATA_EDIT_ID")]
        public TDataEditList TDataEditList {
            get { return _tDataEditList; }
            set { _tDataEditList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TCategoryInfo> _tCategoryInfoList;

        /// <summary>T_CATEGORY_INFO as 'TCategoryInfoList'.</summary>
        public IList<TCategoryInfo> TCategoryInfoList {
            get { if (_tCategoryInfoList == null) { _tCategoryInfoList = new List<TCategoryInfo>(); } return _tCategoryInfoList; }
            set { _tCategoryInfoList = value; }
        }

        protected IList<TMatrixInfo> _tMatrixInfoByItemInfoIdList;

        /// <summary>T_MATRIX_INFO as 'TMatrixInfoByItemInfoIdList'.</summary>
        public IList<TMatrixInfo> TMatrixInfoByItemInfoIdList {
            get { if (_tMatrixInfoByItemInfoIdList == null) { _tMatrixInfoByItemInfoIdList = new List<TMatrixInfo>(); } return _tMatrixInfoByItemInfoIdList; }
            set { _tMatrixInfoByItemInfoIdList = value; }
        }

        protected IList<TMatrixInfo> _tMatrixInfoByChildItemInfoIdList;

        /// <summary>T_MATRIX_INFO as 'TMatrixInfoByChildItemInfoIdList'.</summary>
        public IList<TMatrixInfo> TMatrixInfoByChildItemInfoIdList {
            get { if (_tMatrixInfoByChildItemInfoIdList == null) { _tMatrixInfoByChildItemInfoIdList = new List<TMatrixInfo>(); } return _tMatrixInfoByChildItemInfoIdList; }
            set { _tMatrixInfoByChildItemInfoIdList = value; }
        }

        protected IList<TScenarioQuerylist> _tScenarioQuerylistList;

        /// <summary>T_SCENARIO_QUERYLIST as 'TScenarioQuerylistList'.</summary>
        public IList<TScenarioQuerylist> TScenarioQuerylistList {
            get { if (_tScenarioQuerylistList == null) { _tScenarioQuerylistList = new List<TScenarioQuerylist>(); } return _tScenarioQuerylistList; }
            set { _tScenarioQuerylistList = value; }
        }

        protected IList<TGtScenarioItem> _tGtScenarioItemList;

        /// <summary>T_GT_SCENARIO_ITEM as 'TGtScenarioItemList'.</summary>
        public IList<TGtScenarioItem> TGtScenarioItemList {
            get { if (_tGtScenarioItemList == null) { _tGtScenarioItemList = new List<TGtScenarioItem>(); } return _tGtScenarioItemList; }
            set { _tGtScenarioItemList = value; }
        }

        protected IList<TFaScenarioItem> _tFaScenarioItemList;

        /// <summary>T_FA_SCENARIO_ITEM as 'TFaScenarioItemList'.</summary>
        public IList<TFaScenarioItem> TFaScenarioItemList {
            get { if (_tFaScenarioItemList == null) { _tFaScenarioItemList = new List<TFaScenarioItem>(); } return _tFaScenarioItemList; }
            set { _tFaScenarioItemList = value; }
        }

        protected IList<TFaListAddItem> _tFaListAddItemList;

        /// <summary>T_FA_LIST_ADD_ITEM as 'TFaListAddItemList'.</summary>
        public IList<TFaListAddItem> TFaListAddItemList {
            get { if (_tFaListAddItemList == null) { _tFaListAddItemList = new List<TFaListAddItem>(); } return _tFaListAddItemList; }
            set { _tFaListAddItemList = value; }
        }

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
                if (_itemInfoId == null) { return false; }
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
            if (other == null || !(other is TItemInfo)) { return false; }
            TItemInfo otherEntity = (TItemInfo)other;
            if (!xSV(this.ItemInfoId, otherEntity.ItemInfoId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _itemInfoId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TItemInfo:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tMatrixInfo != null)
            { sb.Append(l).Append(xbRDS(_tMatrixInfo, "TMatrixInfo")); }
            if (_tFaListAddItem != null)
            { sb.Append(l).Append(xbRDS(_tFaListAddItem, "TFaListAddItem")); }
            if (_tFaScenarioItem != null)
            { sb.Append(l).Append(xbRDS(_tFaScenarioItem, "TFaScenarioItem")); }
            if (_tTableControl != null)
            { sb.Append(l).Append(xbRDS(_tTableControl, "TTableControl")); }
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tDataEditList != null)
            { sb.Append(l).Append(xbRDS(_tDataEditList, "TDataEditList")); }
            if (_tCategoryInfoList != null) { foreach (Entity e in _tCategoryInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TCategoryInfoList")); } } }
            if (_tMatrixInfoByItemInfoIdList != null) { foreach (Entity e in _tMatrixInfoByItemInfoIdList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TMatrixInfoByItemInfoIdList")); } } }
            if (_tMatrixInfoByChildItemInfoIdList != null) { foreach (Entity e in _tMatrixInfoByChildItemInfoIdList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TMatrixInfoByChildItemInfoIdList")); } } }
            if (_tScenarioQuerylistList != null) { foreach (Entity e in _tScenarioQuerylistList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TScenarioQuerylistList")); } } }
            if (_tGtScenarioItemList != null) { foreach (Entity e in _tGtScenarioItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TGtScenarioItemList")); } } }
            if (_tFaScenarioItemList != null) { foreach (Entity e in _tFaScenarioItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TFaScenarioItemList")); } } }
            if (_tFaListAddItemList != null) { foreach (Entity e in _tFaListAddItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TFaListAddItemList")); } } }
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
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.ItemName);
            sb.Append(c).Append(this.SourceDiv);
            sb.Append(c).Append(this.Itemno);
            sb.Append(c).Append(this.ItemType);
            sb.Append(c).Append(this.AnswerType);
            sb.Append(c).Append(this.SortNumber);
            sb.Append(c).Append(this.MatrixDiv);
            sb.Append(c).Append(this.Lv1title);
            sb.Append(c).Append(this.Lv2title);
            sb.Append(c).Append(this.OriginalLv1title);
            sb.Append(c).Append(this.OriginalLv2title);
            sb.Append(c).Append(this.TableName);
            sb.Append(c).Append(this.ColumnName);
            sb.Append(c).Append(this.CategoryEditId);
            sb.Append(c).Append(this.DataEditId);
            sb.Append(c).Append(this.Status);
            sb.Append(c).Append(this.TableNameOrg);
            sb.Append(c).Append(this.ColumnNameOrg);
            sb.Append(c).Append(this.CompelItemChangeFlag);
            sb.Append(c).Append(this.SortFlag);
            sb.Append(c).Append(this.SortRange);
            sb.Append(c).Append(this.MultivariateFlag);
            sb.Append(c).Append(this.TableNo);
            sb.Append(c).Append(this.ColumnNo);
            sb.Append(c).Append(this.TableNoOrg);
            sb.Append(c).Append(this.ColumnNoOrg);
            sb.Append(c).Append(this.LastUpdateUser);
            sb.Append(c).Append(this.LastUpdateDatetime);
            sb.Append(c).Append(this.NewAtQc3Flag);
            sb.Append(c).Append(this.SortRangeOrg);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tMatrixInfo != null) { sb.Append(c).Append("TMatrixInfo"); }
            if (_tFaListAddItem != null) { sb.Append(c).Append("TFaListAddItem"); }
            if (_tFaScenarioItem != null) { sb.Append(c).Append("TFaScenarioItem"); }
            if (_tTableControl != null) { sb.Append(c).Append("TTableControl"); }
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tDataEditList != null) { sb.Append(c).Append("TDataEditList"); }
            if (_tCategoryInfoList != null && _tCategoryInfoList.Count > 0)
            { sb.Append(c).Append("TCategoryInfoList"); }
            if (_tMatrixInfoByItemInfoIdList != null && _tMatrixInfoByItemInfoIdList.Count > 0)
            { sb.Append(c).Append("TMatrixInfoByItemInfoIdList"); }
            if (_tMatrixInfoByChildItemInfoIdList != null && _tMatrixInfoByChildItemInfoIdList.Count > 0)
            { sb.Append(c).Append("TMatrixInfoByChildItemInfoIdList"); }
            if (_tScenarioQuerylistList != null && _tScenarioQuerylistList.Count > 0)
            { sb.Append(c).Append("TScenarioQuerylistList"); }
            if (_tGtScenarioItemList != null && _tGtScenarioItemList.Count > 0)
            { sb.Append(c).Append("TGtScenarioItemList"); }
            if (_tFaScenarioItemList != null && _tFaScenarioItemList.Count > 0)
            { sb.Append(c).Append("TFaScenarioItemList"); }
            if (_tFaListAddItemList != null && _tFaListAddItemList.Count > 0)
            { sb.Append(c).Append("TFaListAddItemList"); }
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
        /// <summary>ITEM_INFO_ID: {PK, NotNull, NUMBER(27), FK to T_Matrix_Info}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_INFO_ID")]
        public decimal? ItemInfoId {
            get { return _itemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ItemInfoId");
                _itemInfoId = value;
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

        /// <summary>ITEM_NAME: {NotNull, NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_NAME")]
        public String ItemName {
            get { return _itemName; }
            set {
                __modifiedProperties.AddPropertyName("ItemName");
                _itemName = value;
            }
        }

        /// <summary>SOURCE_DIV: {NotNull, CHAR(1), classification=SourceDiv}</summary>
        [Seasar.Dao.Attrs.Column("SOURCE_DIV")]
        public String SourceDiv {
            get { return _sourceDiv; }
            set {
                __modifiedProperties.AddPropertyName("SourceDiv");
                _sourceDiv = value;
            }
        }

        /// <summary>ITEMNO: {NVARCHAR2(26)}</summary>
        [Seasar.Dao.Attrs.Column("ITEMNO")]
        public String Itemno {
            get { return _itemno; }
            set {
                __modifiedProperties.AddPropertyName("Itemno");
                _itemno = value;
            }
        }

        /// <summary>ITEM_TYPE: {VARCHAR2(3), classification=ItemType}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_TYPE")]
        public String ItemType {
            get { return _itemType; }
            set {
                __modifiedProperties.AddPropertyName("ItemType");
                _itemType = value;
            }
        }

        /// <summary>ANSWER_TYPE: {NotNull, CHAR(1), classification=AnswerType}</summary>
        [Seasar.Dao.Attrs.Column("ANSWER_TYPE")]
        public String AnswerType {
            get { return _answerType; }
            set {
                __modifiedProperties.AddPropertyName("AnswerType");
                _answerType = value;
            }
        }

        /// <summary>SORT_NUMBER: {NotNull, NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NUMBER")]
        public int? SortNumber {
            get { return _sortNumber; }
            set {
                __modifiedProperties.AddPropertyName("SortNumber");
                _sortNumber = value;
            }
        }

        /// <summary>MATRIX_DIV: {NotNull, NUMBER(1), classification=MatrixType}</summary>
        [Seasar.Dao.Attrs.Column("MATRIX_DIV")]
        public int? MatrixDiv {
            get { return _matrixDiv; }
            set {
                __modifiedProperties.AddPropertyName("MatrixDiv");
                _matrixDiv = value;
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

        /// <summary>LV2TITLE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("LV2TITLE")]
        public String Lv2title {
            get { return _lv2title; }
            set {
                __modifiedProperties.AddPropertyName("Lv2title");
                _lv2title = value;
            }
        }

        /// <summary>ORIGINAL_LV1TITLE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("ORIGINAL_LV1TITLE")]
        public String OriginalLv1title {
            get { return _originalLv1title; }
            set {
                __modifiedProperties.AddPropertyName("OriginalLv1title");
                _originalLv1title = value;
            }
        }

        /// <summary>ORIGINAL_LV2TITLE: {NVARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("ORIGINAL_LV2TITLE")]
        public String OriginalLv2title {
            get { return _originalLv2title; }
            set {
                __modifiedProperties.AddPropertyName("OriginalLv2title");
                _originalLv2title = value;
            }
        }

        /// <summary>TABLE_NAME: {VARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TABLE_NAME")]
        public String TableName {
            get { return _tableName; }
            set {
                __modifiedProperties.AddPropertyName("TableName");
                _tableName = value;
            }
        }

        /// <summary>COLUMN_NAME: {VARCHAR2(30)}</summary>
        [Seasar.Dao.Attrs.Column("COLUMN_NAME")]
        public String ColumnName {
            get { return _columnName; }
            set {
                __modifiedProperties.AddPropertyName("ColumnName");
                _columnName = value;
            }
        }

        /// <summary>CATEGORY_EDIT_ID: {IX, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        [Seasar.Dao.Attrs.Column("CATEGORY_EDIT_ID")]
        public decimal? CategoryEditId {
            get { return _categoryEditId; }
            set {
                __modifiedProperties.AddPropertyName("CategoryEditId");
                _categoryEditId = value;
            }
        }

        /// <summary>DATA_EDIT_ID: {IX, NUMBER(27), FK to T_DATA_EDIT_LIST}</summary>
        [Seasar.Dao.Attrs.Column("DATA_EDIT_ID")]
        public decimal? DataEditId {
            get { return _dataEditId; }
            set {
                __modifiedProperties.AddPropertyName("DataEditId");
                _dataEditId = value;
            }
        }

        /// <summary>STATUS: {NotNull, NUMBER(1), default=[1], classification=ItemStatus}</summary>
        [Seasar.Dao.Attrs.Column("STATUS")]
        public int? Status {
            get { return _status; }
            set {
                __modifiedProperties.AddPropertyName("Status");
                _status = value;
            }
        }

        /// <summary>TABLE_NAME_ORG: {VARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TABLE_NAME_ORG")]
        public String TableNameOrg {
            get { return _tableNameOrg; }
            set {
                __modifiedProperties.AddPropertyName("TableNameOrg");
                _tableNameOrg = value;
            }
        }

        /// <summary>COLUMN_NAME_ORG: {VARCHAR2(30)}</summary>
        [Seasar.Dao.Attrs.Column("COLUMN_NAME_ORG")]
        public String ColumnNameOrg {
            get { return _columnNameOrg; }
            set {
                __modifiedProperties.AddPropertyName("ColumnNameOrg");
                _columnNameOrg = value;
            }
        }

        /// <summary>COMPEL_ITEM_CHANGE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("COMPEL_ITEM_CHANGE_FLAG")]
        public int? CompelItemChangeFlag {
            get { return _compelItemChangeFlag; }
            set {
                __modifiedProperties.AddPropertyName("CompelItemChangeFlag");
                _compelItemChangeFlag = value;
            }
        }

        /// <summary>SORT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("SORT_FLAG")]
        public int? SortFlag {
            get { return _sortFlag; }
            set {
                __modifiedProperties.AddPropertyName("SortFlag");
                _sortFlag = value;
            }
        }

        /// <summary>SORT_RANGE: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_RANGE")]
        public int? SortRange {
            get { return _sortRange; }
            set {
                __modifiedProperties.AddPropertyName("SortRange");
                _sortRange = value;
            }
        }

        /// <summary>MULTIVARIATE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("MULTIVARIATE_FLAG")]
        public int? MultivariateFlag {
            get { return _multivariateFlag; }
            set {
                __modifiedProperties.AddPropertyName("MultivariateFlag");
                _multivariateFlag = value;
            }
        }

        /// <summary>TABLE_NO: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("TABLE_NO")]
        public int? TableNo {
            get { return _tableNo; }
            set {
                __modifiedProperties.AddPropertyName("TableNo");
                _tableNo = value;
            }
        }

        /// <summary>COLUMN_NO: {NUMBER(3)}</summary>
        [Seasar.Dao.Attrs.Column("COLUMN_NO")]
        public int? ColumnNo {
            get { return _columnNo; }
            set {
                __modifiedProperties.AddPropertyName("ColumnNo");
                _columnNo = value;
            }
        }

        /// <summary>TABLE_NO_ORG: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("TABLE_NO_ORG")]
        public int? TableNoOrg {
            get { return _tableNoOrg; }
            set {
                __modifiedProperties.AddPropertyName("TableNoOrg");
                _tableNoOrg = value;
            }
        }

        /// <summary>COLUMN_NO_ORG: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("COLUMN_NO_ORG")]
        public int? ColumnNoOrg {
            get { return _columnNoOrg; }
            set {
                __modifiedProperties.AddPropertyName("ColumnNoOrg");
                _columnNoOrg = value;
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

        /// <summary>NEW_AT_QC3_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("NEW_AT_QC3_FLAG")]
        public int? NewAtQc3Flag {
            get { return _newAtQc3Flag; }
            set {
                __modifiedProperties.AddPropertyName("NewAtQc3Flag");
                _newAtQc3Flag = value;
            }
        }

        /// <summary>SORT_RANGE_ORG: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_RANGE_ORG")]
        public int? SortRangeOrg {
            get { return _sortRangeOrg; }
            set {
                __modifiedProperties.AddPropertyName("SortRangeOrg");
                _sortRangeOrg = value;
            }
        }

        #endregion
    }
}
