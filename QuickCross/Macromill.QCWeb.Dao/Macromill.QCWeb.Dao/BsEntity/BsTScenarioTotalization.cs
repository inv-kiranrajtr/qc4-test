

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
    /// The entity of T_SCENARIO_TOTALIZATION as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     SCENARIO_TOTALIZATION_ID
    /// 
    /// [column]
    ///     SCENARIO_TOTALIZATION_ID, QCWEBID, SCENARIO_TYPE, SCENARIO_NAME, CONDITION_DIV, FILTER_FLAG, SORT_NO, WEIGHTBACK_FLAG, WEIGHTBACK_CODE, TOTALNUM_FLAG, GRAPH_OUTPUT_FLAG, PIE_CHART_CHOICE_FLAG, MINIMUM_RATE, AXIS_NOANSWER_ONOFF, TARGET_NOANSWER_ONOFF, POLYLINE_ONOFF, MARKING_N, RANKING_FLAG, RATE_FLAG, RATE1_FLAG, RATE1_SIGN, RATE1_RANGE, RATE1_BACKCOLOR1, RATE1_BACKCOLOR2, RATE2_FLAG, RATE2_SIGN, RATE2_RANGE, RATE2_BACKCOLOR1, RATE2_BACKCOLOR2, LAST_UPDATE_USER, LAST_UPDATE_DATETIME, TEST_FLAG, TEST_TYPE, TEST_SIGNIFICANCE_LV
    /// 
    /// [sequence]
    ///     T_Scenario_Totalization_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_QCWEB_SURVEY_INFO, T_GT_Scenario_Item, T_Cross_Scenario_Target, T_FA_Scenario_Header, T_Scenario_QueryList, T_Category_Output_Edit, T_GT_Matrix_Info, T_DEFAULT_ENV
    /// 
    /// [referrer-table]
    ///     T_CATEGORY_OUTPUT_EDIT, T_CROSS_SCENARIO_TARGET, T_FA_SCENARIO_HEADER, T_GT_MATRIX_INFO, T_GT_SCENARIO_ITEM, T_SCENARIO_QUERYLIST, T_ITEM_INFO
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfo, tGtScenarioItem, tCrossScenarioTarget, tFaScenarioHeader, tScenarioQuerylist, tCategoryOutputEdit, tGtMatrixInfo, tDefaultEnv
    /// 
    /// [referrer-property]
    ///     tCategoryOutputEditList, tCrossScenarioTargetList, tFaScenarioHeaderList, tGtMatrixInfoList, tGtScenarioItemList, tScenarioQuerylistList, tItemInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_SCENARIO_TOTALIZATION")]
    [System.Serializable]
    public partial class TScenarioTotalization : EntityDefinedCommonColumn {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SCENARIO_TOTALIZATION_ID: {PK, NotNull, NUMBER(27), FK to T_GT_Scenario_Item}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_QCWEB_SURVEY_INFO}</summary>
        protected decimal? _qcwebid;

        /// <summary>SCENARIO_TYPE: {NotNull, CHAR(1), classification=ScenarioType}</summary>
        protected String _scenarioType;

        /// <summary>SCENARIO_NAME: {NotNull, NVARCHAR2(50)}</summary>
        protected String _scenarioName;

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1), default=[1], classification=ConditionDiv}</summary>
        protected String _conditionDiv;

        /// <summary>FILTER_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _filterFlag;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>WEIGHTBACK_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _weightbackFlag;

        /// <summary>WEIGHTBACK_CODE: {IX, NUMBER(27)}</summary>
        protected decimal? _weightbackCode;

        /// <summary>TOTALNUM_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _totalnumFlag;

        /// <summary>GRAPH_OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _graphOutputFlag;

        /// <summary>PIE_CHART_CHOICE_FLAG: {NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _pieChartChoiceFlag;

        /// <summary>MINIMUM_RATE: {NUMBER(3), default=[0]}</summary>
        protected int? _minimumRate;

        /// <summary>AXIS_NOANSWER_ONOFF: {NUMBER(1), default=[0]}</summary>
        protected int? _axisNoanswerOnoff;

        /// <summary>TARGET_NOANSWER_ONOFF: {NUMBER(1), default=[0]}</summary>
        protected int? _targetNoanswerOnoff;

        /// <summary>POLYLINE_ONOFF: {NUMBER(1), default=[0]}</summary>
        protected int? _polylineOnoff;

        /// <summary>MARKING_N: {NUMBER(10)}</summary>
        protected long? _markingN;

        /// <summary>RANKING_FLAG: {NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _rankingFlag;

        /// <summary>RATE_FLAG: {NUMBER(1), default=[1], classification=Flag}</summary>
        protected int? _rateFlag;

        /// <summary>RATE1_FLAG: {NUMBER(1), default=[1], classification=Flag}</summary>
        protected int? _rate1Flag;

        /// <summary>RATE1_SIGN: {NUMBER(1), default=[1], classification=RateSign}</summary>
        protected int? _rate1Sign;

        /// <summary>RATE1_RANGE: {NUMBER(10)}</summary>
        protected long? _rate1Range;

        /// <summary>RATE1_BACKCOLOR1: {NUMBER(2)}</summary>
        protected int? _rate1Backcolor1;

        /// <summary>RATE1_BACKCOLOR2: {NUMBER(2)}</summary>
        protected int? _rate1Backcolor2;

        /// <summary>RATE2_FLAG: {NUMBER(1), default=[1], classification=Flag}</summary>
        protected int? _rate2Flag;

        /// <summary>RATE2_SIGN: {NUMBER(1), default=[1], classification=RateSign}</summary>
        protected int? _rate2Sign;

        /// <summary>RATE2_RANGE: {NUMBER(10)}</summary>
        protected long? _rate2Range;

        /// <summary>RATE2_BACKCOLOR1: {NUMBER(2)}</summary>
        protected int? _rate2Backcolor1;

        /// <summary>RATE2_BACKCOLOR2: {NUMBER(2)}</summary>
        protected int? _rate2Backcolor2;

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        protected String _lastUpdateUser;

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastUpdateDatetime;

        /// <summary>TEST_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _testFlag;

        /// <summary>TEST_TYPE: {NUMBER(1)}</summary>
        protected int? _testType;

        /// <summary>TEST_SIGNIFICANCE_LV: {NUMBER(1)}</summary>
        protected int? _testSignificanceLv;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();

        protected bool __canCommonColumnAutoSetup = true;
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_SCENARIO_TOTALIZATION"; } }
        public String TablePropertyName { get { return "TScenarioTotalization"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.ScenarioType ScenarioTypeAsScenarioType { get {
            return CDef.ScenarioType.CodeOf(_scenarioType);
        } set {
            ScenarioType = value != null ? value.Code : null;
        }}

        public CDef.ConditionDiv ConditionDivAsConditionDiv { get {
            return CDef.ConditionDiv.CodeOf(_conditionDiv);
        } set {
            ConditionDiv = value != null ? value.Code : null;
        }}

        public CDef.Flag FilterFlagAsFlag { get {
            return CDef.Flag.CodeOf(_filterFlag);
        } set {
            FilterFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag WeightbackFlagAsFlag { get {
            return CDef.Flag.CodeOf(_weightbackFlag);
        } set {
            WeightbackFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag TotalnumFlagAsFlag { get {
            return CDef.Flag.CodeOf(_totalnumFlag);
        } set {
            TotalnumFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag GraphOutputFlagAsFlag { get {
            return CDef.Flag.CodeOf(_graphOutputFlag);
        } set {
            GraphOutputFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag PieChartChoiceFlagAsFlag { get {
            return CDef.Flag.CodeOf(_pieChartChoiceFlag);
        } set {
            PieChartChoiceFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag RankingFlagAsFlag { get {
            return CDef.Flag.CodeOf(_rankingFlag);
        } set {
            RankingFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag RateFlagAsFlag { get {
            return CDef.Flag.CodeOf(_rateFlag);
        } set {
            RateFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag Rate1FlagAsFlag { get {
            return CDef.Flag.CodeOf(_rate1Flag);
        } set {
            Rate1Flag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.RateSign Rate1SignAsRateSign { get {
            return CDef.RateSign.CodeOf(_rate1Sign);
        } set {
            Rate1Sign = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag Rate2FlagAsFlag { get {
            return CDef.Flag.CodeOf(_rate2Flag);
        } set {
            Rate2Flag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.RateSign Rate2SignAsRateSign { get {
            return CDef.RateSign.CodeOf(_rate2Sign);
        } set {
            Rate2Sign = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag TestFlagAsFlag { get {
            return CDef.Flag.CodeOf(_testFlag);
        } set {
            TestFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of scenarioType as GT.
        /// <![CDATA[
        /// GT: GTシナリオを示す
        /// ]]>
        /// </summary>
        public void SetScenarioType_GT() {
            ScenarioTypeAsScenarioType = CDef.ScenarioType.GT;
        }

        /// <summary>
        /// Set the value of scenarioType as CROSS.
        /// <![CDATA[
        /// CROSS: クロスシナリオを示す
        /// ]]>
        /// </summary>
        public void SetScenarioType_CROSS() {
            ScenarioTypeAsScenarioType = CDef.ScenarioType.CROSS;
        }

        /// <summary>
        /// Set the value of scenarioType as FA.
        /// <![CDATA[
        /// FA: FAシナリオを示す
        /// ]]>
        /// </summary>
        public void SetScenarioType_FA() {
            ScenarioTypeAsScenarioType = CDef.ScenarioType.FA;
        }

        /// <summary>
        /// Set the value of conditionDiv as AND.
        /// <![CDATA[
        /// &: ANDを示す
        /// ]]>
        /// </summary>
        public void SetConditionDiv_AND() {
            ConditionDivAsConditionDiv = CDef.ConditionDiv.AND;
        }

        /// <summary>
        /// Set the value of conditionDiv as OR.
        /// <![CDATA[
        /// |: ORを示す
        /// ]]>
        /// </summary>
        public void SetConditionDiv_OR() {
            ConditionDivAsConditionDiv = CDef.ConditionDiv.OR;
        }

        /// <summary>
        /// Set the value of filterFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetFilterFlag_True() {
            FilterFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of filterFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetFilterFlag_False() {
            FilterFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of weightbackFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetWeightbackFlag_True() {
            WeightbackFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of weightbackFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetWeightbackFlag_False() {
            WeightbackFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of totalnumFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTotalnumFlag_True() {
            TotalnumFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of totalnumFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTotalnumFlag_False() {
            TotalnumFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of graphOutputFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetGraphOutputFlag_True() {
            GraphOutputFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of graphOutputFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetGraphOutputFlag_False() {
            GraphOutputFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of pieChartChoiceFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetPieChartChoiceFlag_True() {
            PieChartChoiceFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of pieChartChoiceFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetPieChartChoiceFlag_False() {
            PieChartChoiceFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of rankingFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetRankingFlag_True() {
            RankingFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of rankingFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetRankingFlag_False() {
            RankingFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of rateFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetRateFlag_True() {
            RateFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of rateFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetRateFlag_False() {
            RateFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of rate1Flag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetRate1Flag_True() {
            Rate1FlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of rate1Flag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetRate1Flag_False() {
            Rate1FlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of rate1Sign as PlusAndMinus.
        /// <![CDATA[
        /// ±: プラス・マイナスを示す
        /// ]]>
        /// </summary>
        public void SetRate1Sign_PlusAndMinus() {
            Rate1SignAsRateSign = CDef.RateSign.PlusAndMinus;
        }

        /// <summary>
        /// Set the value of rate1Sign as Plus.
        /// <![CDATA[
        /// +: プラスを示す
        /// ]]>
        /// </summary>
        public void SetRate1Sign_Plus() {
            Rate1SignAsRateSign = CDef.RateSign.Plus;
        }

        /// <summary>
        /// Set the value of rate1Sign as Minus.
        /// <![CDATA[
        /// -: マイナスを示す
        /// ]]>
        /// </summary>
        public void SetRate1Sign_Minus() {
            Rate1SignAsRateSign = CDef.RateSign.Minus;
        }

        /// <summary>
        /// Set the value of rate2Flag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetRate2Flag_True() {
            Rate2FlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of rate2Flag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetRate2Flag_False() {
            Rate2FlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of rate2Sign as PlusAndMinus.
        /// <![CDATA[
        /// ±: プラス・マイナスを示す
        /// ]]>
        /// </summary>
        public void SetRate2Sign_PlusAndMinus() {
            Rate2SignAsRateSign = CDef.RateSign.PlusAndMinus;
        }

        /// <summary>
        /// Set the value of rate2Sign as Plus.
        /// <![CDATA[
        /// +: プラスを示す
        /// ]]>
        /// </summary>
        public void SetRate2Sign_Plus() {
            Rate2SignAsRateSign = CDef.RateSign.Plus;
        }

        /// <summary>
        /// Set the value of rate2Sign as Minus.
        /// <![CDATA[
        /// -: マイナスを示す
        /// ]]>
        /// </summary>
        public void SetRate2Sign_Minus() {
            Rate2SignAsRateSign = CDef.RateSign.Minus;
        }

        /// <summary>
        /// Set the value of testFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTestFlag_True() {
            TestFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of testFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTestFlag_False() {
            TestFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of scenarioType 'GT'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// GT: GTシナリオを示す
        /// ]]>
        /// </summary>
        public bool IsScenarioTypeGT {
            get {
                CDef.ScenarioType cls = ScenarioTypeAsScenarioType;
                return cls != null ? cls.Equals(CDef.ScenarioType.GT) : false;
            }
        }

        /// <summary>
        /// Is the value of scenarioType 'CROSS'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// CROSS: クロスシナリオを示す
        /// ]]>
        /// </summary>
        public bool IsScenarioTypeCROSS {
            get {
                CDef.ScenarioType cls = ScenarioTypeAsScenarioType;
                return cls != null ? cls.Equals(CDef.ScenarioType.CROSS) : false;
            }
        }

        /// <summary>
        /// Is the value of scenarioType 'FA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// FA: FAシナリオを示す
        /// ]]>
        /// </summary>
        public bool IsScenarioTypeFA {
            get {
                CDef.ScenarioType cls = ScenarioTypeAsScenarioType;
                return cls != null ? cls.Equals(CDef.ScenarioType.FA) : false;
            }
        }

        /// <summary>
        /// Is the value of conditionDiv 'AND'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// &: ANDを示す
        /// ]]>
        /// </summary>
        public bool IsConditionDivAND {
            get {
                CDef.ConditionDiv cls = ConditionDivAsConditionDiv;
                return cls != null ? cls.Equals(CDef.ConditionDiv.AND) : false;
            }
        }

        /// <summary>
        /// Is the value of conditionDiv 'OR'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// |: ORを示す
        /// ]]>
        /// </summary>
        public bool IsConditionDivOR {
            get {
                CDef.ConditionDiv cls = ConditionDivAsConditionDiv;
                return cls != null ? cls.Equals(CDef.ConditionDiv.OR) : false;
            }
        }

        /// <summary>
        /// Is the value of filterFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsFilterFlagTrue {
            get {
                CDef.Flag cls = FilterFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of filterFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsFilterFlagFalse {
            get {
                CDef.Flag cls = FilterFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of weightbackFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsWeightbackFlagTrue {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of weightbackFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsWeightbackFlagFalse {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of totalnumFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTotalnumFlagTrue {
            get {
                CDef.Flag cls = TotalnumFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of totalnumFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTotalnumFlagFalse {
            get {
                CDef.Flag cls = TotalnumFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of graphOutputFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsGraphOutputFlagTrue {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of graphOutputFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsGraphOutputFlagFalse {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of pieChartChoiceFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsPieChartChoiceFlagTrue {
            get {
                CDef.Flag cls = PieChartChoiceFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of pieChartChoiceFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsPieChartChoiceFlagFalse {
            get {
                CDef.Flag cls = PieChartChoiceFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of rankingFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsRankingFlagTrue {
            get {
                CDef.Flag cls = RankingFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of rankingFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsRankingFlagFalse {
            get {
                CDef.Flag cls = RankingFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of rateFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsRateFlagTrue {
            get {
                CDef.Flag cls = RateFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of rateFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsRateFlagFalse {
            get {
                CDef.Flag cls = RateFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of rate1Flag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsRate1FlagTrue {
            get {
                CDef.Flag cls = Rate1FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of rate1Flag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsRate1FlagFalse {
            get {
                CDef.Flag cls = Rate1FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of rate1Sign 'PlusAndMinus'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// ±: プラス・マイナスを示す
        /// ]]>
        /// </summary>
        public bool IsRate1SignPlusAndMinus {
            get {
                CDef.RateSign cls = Rate1SignAsRateSign;
                return cls != null ? cls.Equals(CDef.RateSign.PlusAndMinus) : false;
            }
        }

        /// <summary>
        /// Is the value of rate1Sign 'Plus'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// +: プラスを示す
        /// ]]>
        /// </summary>
        public bool IsRate1SignPlus {
            get {
                CDef.RateSign cls = Rate1SignAsRateSign;
                return cls != null ? cls.Equals(CDef.RateSign.Plus) : false;
            }
        }

        /// <summary>
        /// Is the value of rate1Sign 'Minus'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// -: マイナスを示す
        /// ]]>
        /// </summary>
        public bool IsRate1SignMinus {
            get {
                CDef.RateSign cls = Rate1SignAsRateSign;
                return cls != null ? cls.Equals(CDef.RateSign.Minus) : false;
            }
        }

        /// <summary>
        /// Is the value of rate2Flag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsRate2FlagTrue {
            get {
                CDef.Flag cls = Rate2FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of rate2Flag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsRate2FlagFalse {
            get {
                CDef.Flag cls = Rate2FlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of rate2Sign 'PlusAndMinus'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// ±: プラス・マイナスを示す
        /// ]]>
        /// </summary>
        public bool IsRate2SignPlusAndMinus {
            get {
                CDef.RateSign cls = Rate2SignAsRateSign;
                return cls != null ? cls.Equals(CDef.RateSign.PlusAndMinus) : false;
            }
        }

        /// <summary>
        /// Is the value of rate2Sign 'Plus'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// +: プラスを示す
        /// ]]>
        /// </summary>
        public bool IsRate2SignPlus {
            get {
                CDef.RateSign cls = Rate2SignAsRateSign;
                return cls != null ? cls.Equals(CDef.RateSign.Plus) : false;
            }
        }

        /// <summary>
        /// Is the value of rate2Sign 'Minus'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// -: マイナスを示す
        /// ]]>
        /// </summary>
        public bool IsRate2SignMinus {
            get {
                CDef.RateSign cls = Rate2SignAsRateSign;
                return cls != null ? cls.Equals(CDef.RateSign.Minus) : false;
            }
        }

        /// <summary>
        /// Is the value of testFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTestFlagTrue {
            get {
                CDef.Flag cls = TestFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of testFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTestFlagFalse {
            get {
                CDef.Flag cls = TestFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String ScenarioTypeName {
            get {
                CDef.ScenarioType cls = ScenarioTypeAsScenarioType;
                return cls != null ? cls.Name : null;
            }
        }
        public String ConditionDivName {
            get {
                CDef.ConditionDiv cls = ConditionDivAsConditionDiv;
                return cls != null ? cls.Name : null;
            }
        }
        public String ConditionDivAlias {
            get {
                CDef.ConditionDiv cls = ConditionDivAsConditionDiv;
                return cls != null ? cls.Alias : null;
            }
        }

        public String FilterFlagName {
            get {
                CDef.Flag cls = FilterFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String FilterFlagAlias {
            get {
                CDef.Flag cls = FilterFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String WeightbackFlagName {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String WeightbackFlagAlias {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String TotalnumFlagName {
            get {
                CDef.Flag cls = TotalnumFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TotalnumFlagAlias {
            get {
                CDef.Flag cls = TotalnumFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String GraphOutputFlagName {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String GraphOutputFlagAlias {
            get {
                CDef.Flag cls = GraphOutputFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String PieChartChoiceFlagName {
            get {
                CDef.Flag cls = PieChartChoiceFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String PieChartChoiceFlagAlias {
            get {
                CDef.Flag cls = PieChartChoiceFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String RankingFlagName {
            get {
                CDef.Flag cls = RankingFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String RankingFlagAlias {
            get {
                CDef.Flag cls = RankingFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String RateFlagName {
            get {
                CDef.Flag cls = RateFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String RateFlagAlias {
            get {
                CDef.Flag cls = RateFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String Rate1FlagName {
            get {
                CDef.Flag cls = Rate1FlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String Rate1FlagAlias {
            get {
                CDef.Flag cls = Rate1FlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String Rate1SignName {
            get {
                CDef.RateSign cls = Rate1SignAsRateSign;
                return cls != null ? cls.Name : null;
            }
        }
        public String Rate1SignAlias {
            get {
                CDef.RateSign cls = Rate1SignAsRateSign;
                return cls != null ? cls.Alias : null;
            }
        }

        public String Rate2FlagName {
            get {
                CDef.Flag cls = Rate2FlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String Rate2FlagAlias {
            get {
                CDef.Flag cls = Rate2FlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String Rate2SignName {
            get {
                CDef.RateSign cls = Rate2SignAsRateSign;
                return cls != null ? cls.Name : null;
            }
        }
        public String Rate2SignAlias {
            get {
                CDef.RateSign cls = Rate2SignAsRateSign;
                return cls != null ? cls.Alias : null;
            }
        }

        public String TestFlagName {
            get {
                CDef.Flag cls = TestFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TestFlagAlias {
            get {
                CDef.Flag cls = TestFlagAsFlag;
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

        protected TGtScenarioItem _tGtScenarioItem;

        /// <summary>T_GT_SCENARIO_ITEM as 'TGtScenarioItem'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TGtScenarioItem TGtScenarioItem {
            get { return _tGtScenarioItem; }
            set { _tGtScenarioItem = value; }
        }

        protected TCrossScenarioTarget _tCrossScenarioTarget;

        /// <summary>T_CROSS_SCENARIO_TARGET as 'TCrossScenarioTarget'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TCrossScenarioTarget TCrossScenarioTarget {
            get { return _tCrossScenarioTarget; }
            set { _tCrossScenarioTarget = value; }
        }

        protected TFaScenarioHeader _tFaScenarioHeader;

        /// <summary>T_FA_SCENARIO_HEADER as 'TFaScenarioHeader'.</summary>
        [Seasar.Dao.Attrs.Relno(3), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TFaScenarioHeader TFaScenarioHeader {
            get { return _tFaScenarioHeader; }
            set { _tFaScenarioHeader = value; }
        }

        protected TScenarioQuerylist _tScenarioQuerylist;

        /// <summary>T_SCENARIO_QUERYLIST as 'TScenarioQuerylist'.</summary>
        [Seasar.Dao.Attrs.Relno(4), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TScenarioQuerylist TScenarioQuerylist {
            get { return _tScenarioQuerylist; }
            set { _tScenarioQuerylist = value; }
        }

        protected TCategoryOutputEdit _tCategoryOutputEdit;

        /// <summary>T_CATEGORY_OUTPUT_EDIT as 'TCategoryOutputEdit'.</summary>
        [Seasar.Dao.Attrs.Relno(5), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TCategoryOutputEdit TCategoryOutputEdit {
            get { return _tCategoryOutputEdit; }
            set { _tCategoryOutputEdit = value; }
        }

        protected TGtMatrixInfo _tGtMatrixInfo;

        /// <summary>T_GT_MATRIX_INFO as 'TGtMatrixInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(6), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TGtMatrixInfo TGtMatrixInfo {
            get { return _tGtMatrixInfo; }
            set { _tGtMatrixInfo = value; }
        }

        protected TDefaultEnv _tDefaultEnv;

        /// <summary>T_DEFAULT_ENV as 'TDefaultEnv'.</summary>
        [Seasar.Dao.Attrs.Relno(7), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TDefaultEnv TDefaultEnv {
            get { return _tDefaultEnv; }
            set { _tDefaultEnv = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TCategoryOutputEdit> _tCategoryOutputEditList;

        /// <summary>T_CATEGORY_OUTPUT_EDIT as 'TCategoryOutputEditList'.</summary>
        public IList<TCategoryOutputEdit> TCategoryOutputEditList {
            get { if (_tCategoryOutputEditList == null) { _tCategoryOutputEditList = new List<TCategoryOutputEdit>(); } return _tCategoryOutputEditList; }
            set { _tCategoryOutputEditList = value; }
        }

        protected IList<TCrossScenarioTarget> _tCrossScenarioTargetList;

        /// <summary>T_CROSS_SCENARIO_TARGET as 'TCrossScenarioTargetList'.</summary>
        public IList<TCrossScenarioTarget> TCrossScenarioTargetList {
            get { if (_tCrossScenarioTargetList == null) { _tCrossScenarioTargetList = new List<TCrossScenarioTarget>(); } return _tCrossScenarioTargetList; }
            set { _tCrossScenarioTargetList = value; }
        }

        protected IList<TFaScenarioHeader> _tFaScenarioHeaderList;

        /// <summary>T_FA_SCENARIO_HEADER as 'TFaScenarioHeaderList'.</summary>
        public IList<TFaScenarioHeader> TFaScenarioHeaderList {
            get { if (_tFaScenarioHeaderList == null) { _tFaScenarioHeaderList = new List<TFaScenarioHeader>(); } return _tFaScenarioHeaderList; }
            set { _tFaScenarioHeaderList = value; }
        }

        protected IList<TGtMatrixInfo> _tGtMatrixInfoList;

        /// <summary>T_GT_MATRIX_INFO as 'TGtMatrixInfoList'.</summary>
        public IList<TGtMatrixInfo> TGtMatrixInfoList {
            get { if (_tGtMatrixInfoList == null) { _tGtMatrixInfoList = new List<TGtMatrixInfo>(); } return _tGtMatrixInfoList; }
            set { _tGtMatrixInfoList = value; }
        }

        protected IList<TGtScenarioItem> _tGtScenarioItemList;

        /// <summary>T_GT_SCENARIO_ITEM as 'TGtScenarioItemList'.</summary>
        public IList<TGtScenarioItem> TGtScenarioItemList {
            get { if (_tGtScenarioItemList == null) { _tGtScenarioItemList = new List<TGtScenarioItem>(); } return _tGtScenarioItemList; }
            set { _tGtScenarioItemList = value; }
        }

        protected IList<TScenarioQuerylist> _tScenarioQuerylistList;

        /// <summary>T_SCENARIO_QUERYLIST as 'TScenarioQuerylistList'.</summary>
        public IList<TScenarioQuerylist> TScenarioQuerylistList {
            get { if (_tScenarioQuerylistList == null) { _tScenarioQuerylistList = new List<TScenarioQuerylist>(); } return _tScenarioQuerylistList; }
            set { _tScenarioQuerylistList = value; }
        }

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
                if (_scenarioTotalizationId == null) { return false; }
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
            if (other == null || !(other is TScenarioTotalization)) { return false; }
            TScenarioTotalization otherEntity = (TScenarioTotalization)other;
            if (!xSV(this.ScenarioTotalizationId, otherEntity.ScenarioTotalizationId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _scenarioTotalizationId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TScenarioTotalization:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfo != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfo, "TQcwebSurveyInfo")); }
            if (_tGtScenarioItem != null)
            { sb.Append(l).Append(xbRDS(_tGtScenarioItem, "TGtScenarioItem")); }
            if (_tCrossScenarioTarget != null)
            { sb.Append(l).Append(xbRDS(_tCrossScenarioTarget, "TCrossScenarioTarget")); }
            if (_tFaScenarioHeader != null)
            { sb.Append(l).Append(xbRDS(_tFaScenarioHeader, "TFaScenarioHeader")); }
            if (_tScenarioQuerylist != null)
            { sb.Append(l).Append(xbRDS(_tScenarioQuerylist, "TScenarioQuerylist")); }
            if (_tCategoryOutputEdit != null)
            { sb.Append(l).Append(xbRDS(_tCategoryOutputEdit, "TCategoryOutputEdit")); }
            if (_tGtMatrixInfo != null)
            { sb.Append(l).Append(xbRDS(_tGtMatrixInfo, "TGtMatrixInfo")); }
            if (_tDefaultEnv != null)
            { sb.Append(l).Append(xbRDS(_tDefaultEnv, "TDefaultEnv")); }
            if (_tCategoryOutputEditList != null) { foreach (Entity e in _tCategoryOutputEditList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TCategoryOutputEditList")); } } }
            if (_tCrossScenarioTargetList != null) { foreach (Entity e in _tCrossScenarioTargetList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TCrossScenarioTargetList")); } } }
            if (_tFaScenarioHeaderList != null) { foreach (Entity e in _tFaScenarioHeaderList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TFaScenarioHeaderList")); } } }
            if (_tGtMatrixInfoList != null) { foreach (Entity e in _tGtMatrixInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TGtMatrixInfoList")); } } }
            if (_tGtScenarioItemList != null) { foreach (Entity e in _tGtScenarioItemList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TGtScenarioItemList")); } } }
            if (_tScenarioQuerylistList != null) { foreach (Entity e in _tScenarioQuerylistList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TScenarioQuerylistList")); } } }
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
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.ScenarioType);
            sb.Append(c).Append(this.ScenarioName);
            sb.Append(c).Append(this.ConditionDiv);
            sb.Append(c).Append(this.FilterFlag);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.WeightbackFlag);
            sb.Append(c).Append(this.WeightbackCode);
            sb.Append(c).Append(this.TotalnumFlag);
            sb.Append(c).Append(this.GraphOutputFlag);
            sb.Append(c).Append(this.PieChartChoiceFlag);
            sb.Append(c).Append(this.MinimumRate);
            sb.Append(c).Append(this.AxisNoanswerOnoff);
            sb.Append(c).Append(this.TargetNoanswerOnoff);
            sb.Append(c).Append(this.PolylineOnoff);
            sb.Append(c).Append(this.MarkingN);
            sb.Append(c).Append(this.RankingFlag);
            sb.Append(c).Append(this.RateFlag);
            sb.Append(c).Append(this.Rate1Flag);
            sb.Append(c).Append(this.Rate1Sign);
            sb.Append(c).Append(this.Rate1Range);
            sb.Append(c).Append(this.Rate1Backcolor1);
            sb.Append(c).Append(this.Rate1Backcolor2);
            sb.Append(c).Append(this.Rate2Flag);
            sb.Append(c).Append(this.Rate2Sign);
            sb.Append(c).Append(this.Rate2Range);
            sb.Append(c).Append(this.Rate2Backcolor1);
            sb.Append(c).Append(this.Rate2Backcolor2);
            sb.Append(c).Append(this.LastUpdateUser);
            sb.Append(c).Append(this.LastUpdateDatetime);
            sb.Append(c).Append(this.TestFlag);
            sb.Append(c).Append(this.TestType);
            sb.Append(c).Append(this.TestSignificanceLv);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfo != null) { sb.Append(c).Append("TQcwebSurveyInfo"); }
            if (_tGtScenarioItem != null) { sb.Append(c).Append("TGtScenarioItem"); }
            if (_tCrossScenarioTarget != null) { sb.Append(c).Append("TCrossScenarioTarget"); }
            if (_tFaScenarioHeader != null) { sb.Append(c).Append("TFaScenarioHeader"); }
            if (_tScenarioQuerylist != null) { sb.Append(c).Append("TScenarioQuerylist"); }
            if (_tCategoryOutputEdit != null) { sb.Append(c).Append("TCategoryOutputEdit"); }
            if (_tGtMatrixInfo != null) { sb.Append(c).Append("TGtMatrixInfo"); }
            if (_tDefaultEnv != null) { sb.Append(c).Append("TDefaultEnv"); }
            if (_tCategoryOutputEditList != null && _tCategoryOutputEditList.Count > 0)
            { sb.Append(c).Append("TCategoryOutputEditList"); }
            if (_tCrossScenarioTargetList != null && _tCrossScenarioTargetList.Count > 0)
            { sb.Append(c).Append("TCrossScenarioTargetList"); }
            if (_tFaScenarioHeaderList != null && _tFaScenarioHeaderList.Count > 0)
            { sb.Append(c).Append("TFaScenarioHeaderList"); }
            if (_tGtMatrixInfoList != null && _tGtMatrixInfoList.Count > 0)
            { sb.Append(c).Append("TGtMatrixInfoList"); }
            if (_tGtScenarioItemList != null && _tGtScenarioItemList.Count > 0)
            { sb.Append(c).Append("TGtScenarioItemList"); }
            if (_tScenarioQuerylistList != null && _tScenarioQuerylistList.Count > 0)
            { sb.Append(c).Append("TScenarioQuerylistList"); }
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
        /// <summary>SCENARIO_TOTALIZATION_ID: {PK, NotNull, NUMBER(27), FK to T_GT_Scenario_Item}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TOTALIZATION_ID")]
        public decimal? ScenarioTotalizationId {
            get { return _scenarioTotalizationId; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioTotalizationId");
                _scenarioTotalizationId = value;
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

        /// <summary>SCENARIO_TYPE: {NotNull, CHAR(1), classification=ScenarioType}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TYPE")]
        public String ScenarioType {
            get { return _scenarioType; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioType");
                _scenarioType = value;
            }
        }

        /// <summary>SCENARIO_NAME: {NotNull, NVARCHAR2(50)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_NAME")]
        public String ScenarioName {
            get { return _scenarioName; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioName");
                _scenarioName = value;
            }
        }

        /// <summary>CONDITION_DIV: {NotNull, VARCHAR2(1), default=[1], classification=ConditionDiv}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_DIV")]
        public String ConditionDiv {
            get { return _conditionDiv; }
            set {
                __modifiedProperties.AddPropertyName("ConditionDiv");
                _conditionDiv = value;
            }
        }

        /// <summary>FILTER_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("FILTER_FLAG")]
        public int? FilterFlag {
            get { return _filterFlag; }
            set {
                __modifiedProperties.AddPropertyName("FilterFlag");
                _filterFlag = value;
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

        /// <summary>WEIGHTBACK_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_FLAG")]
        public int? WeightbackFlag {
            get { return _weightbackFlag; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackFlag");
                _weightbackFlag = value;
            }
        }

        /// <summary>WEIGHTBACK_CODE: {IX, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_CODE")]
        public decimal? WeightbackCode {
            get { return _weightbackCode; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackCode");
                _weightbackCode = value;
            }
        }

        /// <summary>TOTALNUM_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TOTALNUM_FLAG")]
        public int? TotalnumFlag {
            get { return _totalnumFlag; }
            set {
                __modifiedProperties.AddPropertyName("TotalnumFlag");
                _totalnumFlag = value;
            }
        }

        /// <summary>GRAPH_OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_OUTPUT_FLAG")]
        public int? GraphOutputFlag {
            get { return _graphOutputFlag; }
            set {
                __modifiedProperties.AddPropertyName("GraphOutputFlag");
                _graphOutputFlag = value;
            }
        }

        /// <summary>PIE_CHART_CHOICE_FLAG: {NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("PIE_CHART_CHOICE_FLAG")]
        public int? PieChartChoiceFlag {
            get { return _pieChartChoiceFlag; }
            set {
                __modifiedProperties.AddPropertyName("PieChartChoiceFlag");
                _pieChartChoiceFlag = value;
            }
        }

        /// <summary>MINIMUM_RATE: {NUMBER(3), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("MINIMUM_RATE")]
        public int? MinimumRate {
            get { return _minimumRate; }
            set {
                __modifiedProperties.AddPropertyName("MinimumRate");
                _minimumRate = value;
            }
        }

        /// <summary>AXIS_NOANSWER_ONOFF: {NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("AXIS_NOANSWER_ONOFF")]
        public int? AxisNoanswerOnoff {
            get { return _axisNoanswerOnoff; }
            set {
                __modifiedProperties.AddPropertyName("AxisNoanswerOnoff");
                _axisNoanswerOnoff = value;
            }
        }

        /// <summary>TARGET_NOANSWER_ONOFF: {NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("TARGET_NOANSWER_ONOFF")]
        public int? TargetNoanswerOnoff {
            get { return _targetNoanswerOnoff; }
            set {
                __modifiedProperties.AddPropertyName("TargetNoanswerOnoff");
                _targetNoanswerOnoff = value;
            }
        }

        /// <summary>POLYLINE_ONOFF: {NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("POLYLINE_ONOFF")]
        public int? PolylineOnoff {
            get { return _polylineOnoff; }
            set {
                __modifiedProperties.AddPropertyName("PolylineOnoff");
                _polylineOnoff = value;
            }
        }

        /// <summary>MARKING_N: {NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("MARKING_N")]
        public long? MarkingN {
            get { return _markingN; }
            set {
                __modifiedProperties.AddPropertyName("MarkingN");
                _markingN = value;
            }
        }

        /// <summary>RANKING_FLAG: {NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("RANKING_FLAG")]
        public int? RankingFlag {
            get { return _rankingFlag; }
            set {
                __modifiedProperties.AddPropertyName("RankingFlag");
                _rankingFlag = value;
            }
        }

        /// <summary>RATE_FLAG: {NUMBER(1), default=[1], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("RATE_FLAG")]
        public int? RateFlag {
            get { return _rateFlag; }
            set {
                __modifiedProperties.AddPropertyName("RateFlag");
                _rateFlag = value;
            }
        }

        /// <summary>RATE1_FLAG: {NUMBER(1), default=[1], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("RATE1_FLAG")]
        public int? Rate1Flag {
            get { return _rate1Flag; }
            set {
                __modifiedProperties.AddPropertyName("Rate1Flag");
                _rate1Flag = value;
            }
        }

        /// <summary>RATE1_SIGN: {NUMBER(1), default=[1], classification=RateSign}</summary>
        [Seasar.Dao.Attrs.Column("RATE1_SIGN")]
        public int? Rate1Sign {
            get { return _rate1Sign; }
            set {
                __modifiedProperties.AddPropertyName("Rate1Sign");
                _rate1Sign = value;
            }
        }

        /// <summary>RATE1_RANGE: {NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("RATE1_RANGE")]
        public long? Rate1Range {
            get { return _rate1Range; }
            set {
                __modifiedProperties.AddPropertyName("Rate1Range");
                _rate1Range = value;
            }
        }

        /// <summary>RATE1_BACKCOLOR1: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("RATE1_BACKCOLOR1")]
        public int? Rate1Backcolor1 {
            get { return _rate1Backcolor1; }
            set {
                __modifiedProperties.AddPropertyName("Rate1Backcolor1");
                _rate1Backcolor1 = value;
            }
        }

        /// <summary>RATE1_BACKCOLOR2: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("RATE1_BACKCOLOR2")]
        public int? Rate1Backcolor2 {
            get { return _rate1Backcolor2; }
            set {
                __modifiedProperties.AddPropertyName("Rate1Backcolor2");
                _rate1Backcolor2 = value;
            }
        }

        /// <summary>RATE2_FLAG: {NUMBER(1), default=[1], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("RATE2_FLAG")]
        public int? Rate2Flag {
            get { return _rate2Flag; }
            set {
                __modifiedProperties.AddPropertyName("Rate2Flag");
                _rate2Flag = value;
            }
        }

        /// <summary>RATE2_SIGN: {NUMBER(1), default=[1], classification=RateSign}</summary>
        [Seasar.Dao.Attrs.Column("RATE2_SIGN")]
        public int? Rate2Sign {
            get { return _rate2Sign; }
            set {
                __modifiedProperties.AddPropertyName("Rate2Sign");
                _rate2Sign = value;
            }
        }

        /// <summary>RATE2_RANGE: {NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("RATE2_RANGE")]
        public long? Rate2Range {
            get { return _rate2Range; }
            set {
                __modifiedProperties.AddPropertyName("Rate2Range");
                _rate2Range = value;
            }
        }

        /// <summary>RATE2_BACKCOLOR1: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("RATE2_BACKCOLOR1")]
        public int? Rate2Backcolor1 {
            get { return _rate2Backcolor1; }
            set {
                __modifiedProperties.AddPropertyName("Rate2Backcolor1");
                _rate2Backcolor1 = value;
            }
        }

        /// <summary>RATE2_BACKCOLOR2: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("RATE2_BACKCOLOR2")]
        public int? Rate2Backcolor2 {
            get { return _rate2Backcolor2; }
            set {
                __modifiedProperties.AddPropertyName("Rate2Backcolor2");
                _rate2Backcolor2 = value;
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

        /// <summary>TEST_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TEST_FLAG")]
        public int? TestFlag {
            get { return _testFlag; }
            set {
                __modifiedProperties.AddPropertyName("TestFlag");
                _testFlag = value;
            }
        }

        /// <summary>TEST_TYPE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_TYPE")]
        public int? TestType {
            get { return _testType; }
            set {
                __modifiedProperties.AddPropertyName("TestType");
                _testType = value;
            }
        }

        /// <summary>TEST_SIGNIFICANCE_LV: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_SIGNIFICANCE_LV")]
        public int? TestSignificanceLv {
            get { return _testSignificanceLv; }
            set {
                __modifiedProperties.AddPropertyName("TestSignificanceLv");
                _testSignificanceLv = value;
            }
        }

        #endregion
    }
}
