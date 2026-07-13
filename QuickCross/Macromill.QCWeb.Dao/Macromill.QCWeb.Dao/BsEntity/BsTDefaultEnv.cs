

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
    /// The entity of T_DEFAULT_ENV as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, NOANSWER_DENOMINATOR_FLAG, VISIBLE_UNFIT_FLAG, NOANSWER_UNFIT_FLAG, WEIGHTBACK_FLAG, CELL_JOINCELL_JOIN_FLAG, CHART_DIRECTION_GT_FLAG, CHART_DIRECTION_CROSS_FLAG, NOANSWER_TARGET_FLAG, NOANSWER_AXIS_FLAG, UNFIT_TARGET_FLAG, UNFIT_AXIS_FLAG, TOTALNUM_FLAG, RATE_DIFF_COLOR_MINUS5, RATE_DIFF_COLOR_MINUS10, RATE_DIFF_COLOR_PLUS5, RATE_DIFF_COLOR_PLUS10, GRAPH_TYPE_SA, GRAPH_TYPE_SA_MATRIX, GRAPH_TYPE_MA_SIMPLE, GRAPH_TYPE_MA_CROSS, GRAPH_TYPE_MA_MATRIX, GRAPH_TYPE_N_RATE, GRAPH_TYPE_N_RANKING, SET_EXECUTE_FLAG, TITLE_ALL, TITLE_AXIS_ALL, TITLE_NOANSWER, TITLE_UNFIT, TITLE_BEFORE_WB, FLAG_STATISTICS_PARAMETER, TITLE_STATISTICS_PARAMETER, FLAG_TOTAL, TITLE_TOTAL, DP_SUM, FLAG_AVR, TITLE_AVR, DP_AVR, FLAG_SD, TITLE_SD, DP_SD, FLAG_MIN, TITLE_MIN, DP_MIN, FLAG_MAX, TITLE_MAX, DP_MAX, FLAG_MEDIAN, TITLE_MEDIAN, DP_MEDIAN, DP_WEIGHT, DP_WEIGHT_AVR, EXCEL_TYPE, PP_TYPE, LAST_UPDATE_USER, LAST_UPDATE_DATETIME, TEST_GT_FLAG, TEST_CROSS_FLAG, TEST_TYPE_GT, TEST_TYPE_CROSS, TEST_SIGNIFICANCE_LV_GT, TEST_SIGNIFICANCE_LV_CROSS
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
    ///     T_QCWEB_SURVEY_INFO(AsOne)
    /// 
    /// [referrer-table]
    ///     T_DEFAULT_ENV_COLOR_INFO, T_SCENARIO_TOTALIZATION, T_QCWEB_SURVEY_INFO
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfoAsOne
    /// 
    /// [referrer-property]
    ///     tDefaultEnvColorInfoList, tScenarioTotalizationList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_DEFAULT_ENV")]
    [System.Serializable]
    public partial class TDefaultEnv : EntityDefinedCommonColumn {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>NOANSWER_DENOMINATOR_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _noanswerDenominatorFlag;

        /// <summary>VISIBLE_UNFIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _visibleUnfitFlag;

        /// <summary>NOANSWER_UNFIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _noanswerUnfitFlag;

        /// <summary>WEIGHTBACK_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _weightbackFlag;

        /// <summary>CELL_JOINCELL_JOIN_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _cellJoincellJoinFlag;

        /// <summary>CHART_DIRECTION_GT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _chartDirectionGtFlag;

        /// <summary>CHART_DIRECTION_CROSS_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _chartDirectionCrossFlag;

        /// <summary>NOANSWER_TARGET_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _noanswerTargetFlag;

        /// <summary>NOANSWER_AXIS_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _noanswerAxisFlag;

        /// <summary>UNFIT_TARGET_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _unfitTargetFlag;

        /// <summary>UNFIT_AXIS_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _unfitAxisFlag;

        /// <summary>TOTALNUM_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _totalnumFlag;

        /// <summary>RATE_DIFF_COLOR_MINUS5: {NotNull, NUMBER(2), default=[0]}</summary>
        protected int? _rateDiffColorMinus5;

        /// <summary>RATE_DIFF_COLOR_MINUS10: {NotNull, NUMBER(2), default=[0]}</summary>
        protected int? _rateDiffColorMinus10;

        /// <summary>RATE_DIFF_COLOR_PLUS5: {NotNull, NUMBER(2), default=[0]}</summary>
        protected int? _rateDiffColorPlus5;

        /// <summary>RATE_DIFF_COLOR_PLUS10: {NotNull, NUMBER(2), default=[0]}</summary>
        protected int? _rateDiffColorPlus10;

        /// <summary>GRAPH_TYPE_SA: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeSa;

        /// <summary>GRAPH_TYPE_SA_MATRIX: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeSaMatrix;

        /// <summary>GRAPH_TYPE_MA_SIMPLE: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeMaSimple;

        /// <summary>GRAPH_TYPE_MA_CROSS: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeMaCross;

        /// <summary>GRAPH_TYPE_MA_MATRIX: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeMaMatrix;

        /// <summary>GRAPH_TYPE_N_RATE: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeNRate;

        /// <summary>GRAPH_TYPE_N_RANKING: {NotNull, VARCHAR2(3)}</summary>
        protected String _graphTypeNRanking;

        /// <summary>SET_EXECUTE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _setExecuteFlag;

        /// <summary>TITLE_ALL: {NVARCHAR2(25)}</summary>
        protected String _titleAll;

        /// <summary>TITLE_AXIS_ALL: {NVARCHAR2(25)}</summary>
        protected String _titleAxisAll;

        /// <summary>TITLE_NOANSWER: {NVARCHAR2(25)}</summary>
        protected String _titleNoanswer;

        /// <summary>TITLE_UNFIT: {NVARCHAR2(25)}</summary>
        protected String _titleUnfit;

        /// <summary>TITLE_BEFORE_WB: {NVARCHAR2(25)}</summary>
        protected String _titleBeforeWb;

        /// <summary>FLAG_STATISTICS_PARAMETER: {NUMBER(1)}</summary>
        protected int? _flagStatisticsParameter;

        /// <summary>TITLE_STATISTICS_PARAMETER: {NVARCHAR2(25)}</summary>
        protected String _titleStatisticsParameter;

        /// <summary>FLAG_TOTAL: {NUMBER(1)}</summary>
        protected int? _flagTotal;

        /// <summary>TITLE_TOTAL: {NVARCHAR2(25)}</summary>
        protected String _titleTotal;

        /// <summary>DP_SUM: {NUMBER(2)}</summary>
        protected int? _dpSum;

        /// <summary>FLAG_AVR: {NUMBER(1)}</summary>
        protected int? _flagAvr;

        /// <summary>TITLE_AVR: {NVARCHAR2(25)}</summary>
        protected String _titleAvr;

        /// <summary>DP_AVR: {NUMBER(2)}</summary>
        protected int? _dpAvr;

        /// <summary>FLAG_SD: {NUMBER(1)}</summary>
        protected int? _flagSd;

        /// <summary>TITLE_SD: {NVARCHAR2(25)}</summary>
        protected String _titleSd;

        /// <summary>DP_SD: {NUMBER(2)}</summary>
        protected int? _dpSd;

        /// <summary>FLAG_MIN: {NUMBER(1)}</summary>
        protected int? _flagMin;

        /// <summary>TITLE_MIN: {NVARCHAR2(25)}</summary>
        protected String _titleMin;

        /// <summary>DP_MIN: {NUMBER(2)}</summary>
        protected int? _dpMin;

        /// <summary>FLAG_MAX: {NUMBER(1)}</summary>
        protected int? _flagMax;

        /// <summary>TITLE_MAX: {NVARCHAR2(25)}</summary>
        protected String _titleMax;

        /// <summary>DP_MAX: {NUMBER(2)}</summary>
        protected int? _dpMax;

        /// <summary>FLAG_MEDIAN: {NUMBER(1)}</summary>
        protected int? _flagMedian;

        /// <summary>TITLE_MEDIAN: {NVARCHAR2(25)}</summary>
        protected String _titleMedian;

        /// <summary>DP_MEDIAN: {NUMBER(2)}</summary>
        protected int? _dpMedian;

        /// <summary>DP_WEIGHT: {NUMBER(2)}</summary>
        protected int? _dpWeight;

        /// <summary>DP_WEIGHT_AVR: {NUMBER(2)}</summary>
        protected int? _dpWeightAvr;

        /// <summary>EXCEL_TYPE: {NUMBER(2)}</summary>
        protected int? _excelType;

        /// <summary>PP_TYPE: {NUMBER(2)}</summary>
        protected int? _ppType;

        /// <summary>LAST_UPDATE_USER: {VARCHAR2(1000)}</summary>
        protected String _lastUpdateUser;

        /// <summary>LAST_UPDATE_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _lastUpdateDatetime;

        /// <summary>TEST_GT_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _testGtFlag;

        /// <summary>TEST_CROSS_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _testCrossFlag;

        /// <summary>TEST_TYPE_GT: {NUMBER(1)}</summary>
        protected int? _testTypeGt;

        /// <summary>TEST_TYPE_CROSS: {NUMBER(1)}</summary>
        protected int? _testTypeCross;

        /// <summary>TEST_SIGNIFICANCE_LV_GT: {NUMBER(1)}</summary>
        protected int? _testSignificanceLvGt;

        /// <summary>TEST_SIGNIFICANCE_LV_CROSS: {NUMBER(1)}</summary>
        protected int? _testSignificanceLvCross;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();

        protected bool __canCommonColumnAutoSetup = true;
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_DEFAULT_ENV"; } }
        public String TablePropertyName { get { return "TDefaultEnv"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag NoanswerDenominatorFlagAsFlag { get {
            return CDef.Flag.CodeOf(_noanswerDenominatorFlag);
        } set {
            NoanswerDenominatorFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag VisibleUnfitFlagAsFlag { get {
            return CDef.Flag.CodeOf(_visibleUnfitFlag);
        } set {
            VisibleUnfitFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag NoanswerUnfitFlagAsFlag { get {
            return CDef.Flag.CodeOf(_noanswerUnfitFlag);
        } set {
            NoanswerUnfitFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag WeightbackFlagAsFlag { get {
            return CDef.Flag.CodeOf(_weightbackFlag);
        } set {
            WeightbackFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag CellJoincellJoinFlagAsFlag { get {
            return CDef.Flag.CodeOf(_cellJoincellJoinFlag);
        } set {
            CellJoincellJoinFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag ChartDirectionGtFlagAsFlag { get {
            return CDef.Flag.CodeOf(_chartDirectionGtFlag);
        } set {
            ChartDirectionGtFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag ChartDirectionCrossFlagAsFlag { get {
            return CDef.Flag.CodeOf(_chartDirectionCrossFlag);
        } set {
            ChartDirectionCrossFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag NoanswerTargetFlagAsFlag { get {
            return CDef.Flag.CodeOf(_noanswerTargetFlag);
        } set {
            NoanswerTargetFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag NoanswerAxisFlagAsFlag { get {
            return CDef.Flag.CodeOf(_noanswerAxisFlag);
        } set {
            NoanswerAxisFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag UnfitTargetFlagAsFlag { get {
            return CDef.Flag.CodeOf(_unfitTargetFlag);
        } set {
            UnfitTargetFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag UnfitAxisFlagAsFlag { get {
            return CDef.Flag.CodeOf(_unfitAxisFlag);
        } set {
            UnfitAxisFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag TotalnumFlagAsFlag { get {
            return CDef.Flag.CodeOf(_totalnumFlag);
        } set {
            TotalnumFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag SetExecuteFlagAsFlag { get {
            return CDef.Flag.CodeOf(_setExecuteFlag);
        } set {
            SetExecuteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag TestGtFlagAsFlag { get {
            return CDef.Flag.CodeOf(_testGtFlag);
        } set {
            TestGtFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        public CDef.Flag TestCrossFlagAsFlag { get {
            return CDef.Flag.CodeOf(_testCrossFlag);
        } set {
            TestCrossFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of noanswerDenominatorFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerDenominatorFlag_True() {
            NoanswerDenominatorFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of noanswerDenominatorFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerDenominatorFlag_False() {
            NoanswerDenominatorFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of visibleUnfitFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetVisibleUnfitFlag_True() {
            VisibleUnfitFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of visibleUnfitFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetVisibleUnfitFlag_False() {
            VisibleUnfitFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of noanswerUnfitFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerUnfitFlag_True() {
            NoanswerUnfitFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of noanswerUnfitFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerUnfitFlag_False() {
            NoanswerUnfitFlagAsFlag = CDef.Flag.False;
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
        /// Set the value of cellJoincellJoinFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetCellJoincellJoinFlag_True() {
            CellJoincellJoinFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of cellJoincellJoinFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetCellJoincellJoinFlag_False() {
            CellJoincellJoinFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of chartDirectionGtFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetChartDirectionGtFlag_True() {
            ChartDirectionGtFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of chartDirectionGtFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetChartDirectionGtFlag_False() {
            ChartDirectionGtFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of chartDirectionCrossFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetChartDirectionCrossFlag_True() {
            ChartDirectionCrossFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of chartDirectionCrossFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetChartDirectionCrossFlag_False() {
            ChartDirectionCrossFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of noanswerTargetFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerTargetFlag_True() {
            NoanswerTargetFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of noanswerTargetFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerTargetFlag_False() {
            NoanswerTargetFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of noanswerAxisFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerAxisFlag_True() {
            NoanswerAxisFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of noanswerAxisFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetNoanswerAxisFlag_False() {
            NoanswerAxisFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of unfitTargetFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetUnfitTargetFlag_True() {
            UnfitTargetFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of unfitTargetFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetUnfitTargetFlag_False() {
            UnfitTargetFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of unfitAxisFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetUnfitAxisFlag_True() {
            UnfitAxisFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of unfitAxisFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetUnfitAxisFlag_False() {
            UnfitAxisFlagAsFlag = CDef.Flag.False;
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
        /// Set the value of setExecuteFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetSetExecuteFlag_True() {
            SetExecuteFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of setExecuteFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetSetExecuteFlag_False() {
            SetExecuteFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of testGtFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTestGtFlag_True() {
            TestGtFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of testGtFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTestGtFlag_False() {
            TestGtFlagAsFlag = CDef.Flag.False;
        }

        /// <summary>
        /// Set the value of testCrossFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetTestCrossFlag_True() {
            TestCrossFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of testCrossFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetTestCrossFlag_False() {
            TestCrossFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of noanswerDenominatorFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerDenominatorFlagTrue {
            get {
                CDef.Flag cls = NoanswerDenominatorFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerDenominatorFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerDenominatorFlagFalse {
            get {
                CDef.Flag cls = NoanswerDenominatorFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of visibleUnfitFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsVisibleUnfitFlagTrue {
            get {
                CDef.Flag cls = VisibleUnfitFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of visibleUnfitFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsVisibleUnfitFlagFalse {
            get {
                CDef.Flag cls = VisibleUnfitFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerUnfitFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerUnfitFlagTrue {
            get {
                CDef.Flag cls = NoanswerUnfitFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerUnfitFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerUnfitFlagFalse {
            get {
                CDef.Flag cls = NoanswerUnfitFlagAsFlag;
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
        /// Is the value of cellJoincellJoinFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsCellJoincellJoinFlagTrue {
            get {
                CDef.Flag cls = CellJoincellJoinFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of cellJoincellJoinFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsCellJoincellJoinFlagFalse {
            get {
                CDef.Flag cls = CellJoincellJoinFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of chartDirectionGtFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsChartDirectionGtFlagTrue {
            get {
                CDef.Flag cls = ChartDirectionGtFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of chartDirectionGtFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsChartDirectionGtFlagFalse {
            get {
                CDef.Flag cls = ChartDirectionGtFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of chartDirectionCrossFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsChartDirectionCrossFlagTrue {
            get {
                CDef.Flag cls = ChartDirectionCrossFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of chartDirectionCrossFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsChartDirectionCrossFlagFalse {
            get {
                CDef.Flag cls = ChartDirectionCrossFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerTargetFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerTargetFlagTrue {
            get {
                CDef.Flag cls = NoanswerTargetFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerTargetFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerTargetFlagFalse {
            get {
                CDef.Flag cls = NoanswerTargetFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerAxisFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerAxisFlagTrue {
            get {
                CDef.Flag cls = NoanswerAxisFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of noanswerAxisFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsNoanswerAxisFlagFalse {
            get {
                CDef.Flag cls = NoanswerAxisFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of unfitTargetFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsUnfitTargetFlagTrue {
            get {
                CDef.Flag cls = UnfitTargetFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of unfitTargetFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsUnfitTargetFlagFalse {
            get {
                CDef.Flag cls = UnfitTargetFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of unfitAxisFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsUnfitAxisFlagTrue {
            get {
                CDef.Flag cls = UnfitAxisFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of unfitAxisFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsUnfitAxisFlagFalse {
            get {
                CDef.Flag cls = UnfitAxisFlagAsFlag;
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
        /// Is the value of setExecuteFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsSetExecuteFlagTrue {
            get {
                CDef.Flag cls = SetExecuteFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of setExecuteFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsSetExecuteFlagFalse {
            get {
                CDef.Flag cls = SetExecuteFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of testGtFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTestGtFlagTrue {
            get {
                CDef.Flag cls = TestGtFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of testGtFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTestGtFlagFalse {
            get {
                CDef.Flag cls = TestGtFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        /// <summary>
        /// Is the value of testCrossFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsTestCrossFlagTrue {
            get {
                CDef.Flag cls = TestCrossFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of testCrossFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsTestCrossFlagFalse {
            get {
                CDef.Flag cls = TestCrossFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String NoanswerDenominatorFlagName {
            get {
                CDef.Flag cls = NoanswerDenominatorFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String NoanswerDenominatorFlagAlias {
            get {
                CDef.Flag cls = NoanswerDenominatorFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String VisibleUnfitFlagName {
            get {
                CDef.Flag cls = VisibleUnfitFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String VisibleUnfitFlagAlias {
            get {
                CDef.Flag cls = VisibleUnfitFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String NoanswerUnfitFlagName {
            get {
                CDef.Flag cls = NoanswerUnfitFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String NoanswerUnfitFlagAlias {
            get {
                CDef.Flag cls = NoanswerUnfitFlagAsFlag;
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

        public String CellJoincellJoinFlagName {
            get {
                CDef.Flag cls = CellJoincellJoinFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String CellJoincellJoinFlagAlias {
            get {
                CDef.Flag cls = CellJoincellJoinFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String ChartDirectionGtFlagName {
            get {
                CDef.Flag cls = ChartDirectionGtFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String ChartDirectionGtFlagAlias {
            get {
                CDef.Flag cls = ChartDirectionGtFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String ChartDirectionCrossFlagName {
            get {
                CDef.Flag cls = ChartDirectionCrossFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String ChartDirectionCrossFlagAlias {
            get {
                CDef.Flag cls = ChartDirectionCrossFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String NoanswerTargetFlagName {
            get {
                CDef.Flag cls = NoanswerTargetFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String NoanswerTargetFlagAlias {
            get {
                CDef.Flag cls = NoanswerTargetFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String NoanswerAxisFlagName {
            get {
                CDef.Flag cls = NoanswerAxisFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String NoanswerAxisFlagAlias {
            get {
                CDef.Flag cls = NoanswerAxisFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String UnfitTargetFlagName {
            get {
                CDef.Flag cls = UnfitTargetFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String UnfitTargetFlagAlias {
            get {
                CDef.Flag cls = UnfitTargetFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String UnfitAxisFlagName {
            get {
                CDef.Flag cls = UnfitAxisFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String UnfitAxisFlagAlias {
            get {
                CDef.Flag cls = UnfitAxisFlagAsFlag;
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

        public String SetExecuteFlagName {
            get {
                CDef.Flag cls = SetExecuteFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String SetExecuteFlagAlias {
            get {
                CDef.Flag cls = SetExecuteFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String TestGtFlagName {
            get {
                CDef.Flag cls = TestGtFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TestGtFlagAlias {
            get {
                CDef.Flag cls = TestGtFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        public String TestCrossFlagName {
            get {
                CDef.Flag cls = TestCrossFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String TestCrossFlagAlias {
            get {
                CDef.Flag cls = TestCrossFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TQcwebSurveyInfo _tQcwebSurveyInfoAsOne;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfoAsOne {
            get { return _tQcwebSurveyInfoAsOne; }
            set { _tQcwebSurveyInfoAsOne = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TDefaultEnvColorInfo> _tDefaultEnvColorInfoList;

        /// <summary>T_DEFAULT_ENV_COLOR_INFO as 'TDefaultEnvColorInfoList'.</summary>
        public IList<TDefaultEnvColorInfo> TDefaultEnvColorInfoList {
            get { if (_tDefaultEnvColorInfoList == null) { _tDefaultEnvColorInfoList = new List<TDefaultEnvColorInfo>(); } return _tDefaultEnvColorInfoList; }
            set { _tDefaultEnvColorInfoList = value; }
        }

        protected IList<TScenarioTotalization> _tScenarioTotalizationList;

        /// <summary>T_SCENARIO_TOTALIZATION as 'TScenarioTotalizationList'.</summary>
        public IList<TScenarioTotalization> TScenarioTotalizationList {
            get { if (_tScenarioTotalizationList == null) { _tScenarioTotalizationList = new List<TScenarioTotalization>(); } return _tScenarioTotalizationList; }
            set { _tScenarioTotalizationList = value; }
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
            if (other == null || !(other is TDefaultEnv)) { return false; }
            TDefaultEnv otherEntity = (TDefaultEnv)other;
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
            return "TDefaultEnv:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfoAsOne != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfoAsOne, "TQcwebSurveyInfoAsOne")); }
            if (_tDefaultEnvColorInfoList != null) { foreach (Entity e in _tDefaultEnvColorInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TDefaultEnvColorInfoList")); } } }
            if (_tScenarioTotalizationList != null) { foreach (Entity e in _tScenarioTotalizationList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TScenarioTotalizationList")); } } }
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
            sb.Append(c).Append(this.NoanswerDenominatorFlag);
            sb.Append(c).Append(this.VisibleUnfitFlag);
            sb.Append(c).Append(this.NoanswerUnfitFlag);
            sb.Append(c).Append(this.WeightbackFlag);
            sb.Append(c).Append(this.CellJoincellJoinFlag);
            sb.Append(c).Append(this.ChartDirectionGtFlag);
            sb.Append(c).Append(this.ChartDirectionCrossFlag);
            sb.Append(c).Append(this.NoanswerTargetFlag);
            sb.Append(c).Append(this.NoanswerAxisFlag);
            sb.Append(c).Append(this.UnfitTargetFlag);
            sb.Append(c).Append(this.UnfitAxisFlag);
            sb.Append(c).Append(this.TotalnumFlag);
            sb.Append(c).Append(this.RateDiffColorMinus5);
            sb.Append(c).Append(this.RateDiffColorMinus10);
            sb.Append(c).Append(this.RateDiffColorPlus5);
            sb.Append(c).Append(this.RateDiffColorPlus10);
            sb.Append(c).Append(this.GraphTypeSa);
            sb.Append(c).Append(this.GraphTypeSaMatrix);
            sb.Append(c).Append(this.GraphTypeMaSimple);
            sb.Append(c).Append(this.GraphTypeMaCross);
            sb.Append(c).Append(this.GraphTypeMaMatrix);
            sb.Append(c).Append(this.GraphTypeNRate);
            sb.Append(c).Append(this.GraphTypeNRanking);
            sb.Append(c).Append(this.SetExecuteFlag);
            sb.Append(c).Append(this.TitleAll);
            sb.Append(c).Append(this.TitleAxisAll);
            sb.Append(c).Append(this.TitleNoanswer);
            sb.Append(c).Append(this.TitleUnfit);
            sb.Append(c).Append(this.TitleBeforeWb);
            sb.Append(c).Append(this.FlagStatisticsParameter);
            sb.Append(c).Append(this.TitleStatisticsParameter);
            sb.Append(c).Append(this.FlagTotal);
            sb.Append(c).Append(this.TitleTotal);
            sb.Append(c).Append(this.DpSum);
            sb.Append(c).Append(this.FlagAvr);
            sb.Append(c).Append(this.TitleAvr);
            sb.Append(c).Append(this.DpAvr);
            sb.Append(c).Append(this.FlagSd);
            sb.Append(c).Append(this.TitleSd);
            sb.Append(c).Append(this.DpSd);
            sb.Append(c).Append(this.FlagMin);
            sb.Append(c).Append(this.TitleMin);
            sb.Append(c).Append(this.DpMin);
            sb.Append(c).Append(this.FlagMax);
            sb.Append(c).Append(this.TitleMax);
            sb.Append(c).Append(this.DpMax);
            sb.Append(c).Append(this.FlagMedian);
            sb.Append(c).Append(this.TitleMedian);
            sb.Append(c).Append(this.DpMedian);
            sb.Append(c).Append(this.DpWeight);
            sb.Append(c).Append(this.DpWeightAvr);
            sb.Append(c).Append(this.ExcelType);
            sb.Append(c).Append(this.PpType);
            sb.Append(c).Append(this.LastUpdateUser);
            sb.Append(c).Append(this.LastUpdateDatetime);
            sb.Append(c).Append(this.TestGtFlag);
            sb.Append(c).Append(this.TestCrossFlag);
            sb.Append(c).Append(this.TestTypeGt);
            sb.Append(c).Append(this.TestTypeCross);
            sb.Append(c).Append(this.TestSignificanceLvGt);
            sb.Append(c).Append(this.TestSignificanceLvCross);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfoAsOne != null) { sb.Append(c).Append("TQcwebSurveyInfoAsOne"); }
            if (_tDefaultEnvColorInfoList != null && _tDefaultEnvColorInfoList.Count > 0)
            { sb.Append(c).Append("TDefaultEnvColorInfoList"); }
            if (_tScenarioTotalizationList != null && _tScenarioTotalizationList.Count > 0)
            { sb.Append(c).Append("TScenarioTotalizationList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>NOANSWER_DENOMINATOR_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_DENOMINATOR_FLAG")]
        public int? NoanswerDenominatorFlag {
            get { return _noanswerDenominatorFlag; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerDenominatorFlag");
                _noanswerDenominatorFlag = value;
            }
        }

        /// <summary>VISIBLE_UNFIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("VISIBLE_UNFIT_FLAG")]
        public int? VisibleUnfitFlag {
            get { return _visibleUnfitFlag; }
            set {
                __modifiedProperties.AddPropertyName("VisibleUnfitFlag");
                _visibleUnfitFlag = value;
            }
        }

        /// <summary>NOANSWER_UNFIT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_UNFIT_FLAG")]
        public int? NoanswerUnfitFlag {
            get { return _noanswerUnfitFlag; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerUnfitFlag");
                _noanswerUnfitFlag = value;
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

        /// <summary>CELL_JOINCELL_JOIN_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CELL_JOINCELL_JOIN_FLAG")]
        public int? CellJoincellJoinFlag {
            get { return _cellJoincellJoinFlag; }
            set {
                __modifiedProperties.AddPropertyName("CellJoincellJoinFlag");
                _cellJoincellJoinFlag = value;
            }
        }

        /// <summary>CHART_DIRECTION_GT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CHART_DIRECTION_GT_FLAG")]
        public int? ChartDirectionGtFlag {
            get { return _chartDirectionGtFlag; }
            set {
                __modifiedProperties.AddPropertyName("ChartDirectionGtFlag");
                _chartDirectionGtFlag = value;
            }
        }

        /// <summary>CHART_DIRECTION_CROSS_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("CHART_DIRECTION_CROSS_FLAG")]
        public int? ChartDirectionCrossFlag {
            get { return _chartDirectionCrossFlag; }
            set {
                __modifiedProperties.AddPropertyName("ChartDirectionCrossFlag");
                _chartDirectionCrossFlag = value;
            }
        }

        /// <summary>NOANSWER_TARGET_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_TARGET_FLAG")]
        public int? NoanswerTargetFlag {
            get { return _noanswerTargetFlag; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerTargetFlag");
                _noanswerTargetFlag = value;
            }
        }

        /// <summary>NOANSWER_AXIS_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_AXIS_FLAG")]
        public int? NoanswerAxisFlag {
            get { return _noanswerAxisFlag; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerAxisFlag");
                _noanswerAxisFlag = value;
            }
        }

        /// <summary>UNFIT_TARGET_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("UNFIT_TARGET_FLAG")]
        public int? UnfitTargetFlag {
            get { return _unfitTargetFlag; }
            set {
                __modifiedProperties.AddPropertyName("UnfitTargetFlag");
                _unfitTargetFlag = value;
            }
        }

        /// <summary>UNFIT_AXIS_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("UNFIT_AXIS_FLAG")]
        public int? UnfitAxisFlag {
            get { return _unfitAxisFlag; }
            set {
                __modifiedProperties.AddPropertyName("UnfitAxisFlag");
                _unfitAxisFlag = value;
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

        /// <summary>RATE_DIFF_COLOR_MINUS5: {NotNull, NUMBER(2), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("RATE_DIFF_COLOR_MINUS5")]
        public int? RateDiffColorMinus5 {
            get { return _rateDiffColorMinus5; }
            set {
                __modifiedProperties.AddPropertyName("RateDiffColorMinus5");
                _rateDiffColorMinus5 = value;
            }
        }

        /// <summary>RATE_DIFF_COLOR_MINUS10: {NotNull, NUMBER(2), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("RATE_DIFF_COLOR_MINUS10")]
        public int? RateDiffColorMinus10 {
            get { return _rateDiffColorMinus10; }
            set {
                __modifiedProperties.AddPropertyName("RateDiffColorMinus10");
                _rateDiffColorMinus10 = value;
            }
        }

        /// <summary>RATE_DIFF_COLOR_PLUS5: {NotNull, NUMBER(2), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("RATE_DIFF_COLOR_PLUS5")]
        public int? RateDiffColorPlus5 {
            get { return _rateDiffColorPlus5; }
            set {
                __modifiedProperties.AddPropertyName("RateDiffColorPlus5");
                _rateDiffColorPlus5 = value;
            }
        }

        /// <summary>RATE_DIFF_COLOR_PLUS10: {NotNull, NUMBER(2), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("RATE_DIFF_COLOR_PLUS10")]
        public int? RateDiffColorPlus10 {
            get { return _rateDiffColorPlus10; }
            set {
                __modifiedProperties.AddPropertyName("RateDiffColorPlus10");
                _rateDiffColorPlus10 = value;
            }
        }

        /// <summary>GRAPH_TYPE_SA: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_SA")]
        public String GraphTypeSa {
            get { return _graphTypeSa; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeSa");
                _graphTypeSa = value;
            }
        }

        /// <summary>GRAPH_TYPE_SA_MATRIX: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_SA_MATRIX")]
        public String GraphTypeSaMatrix {
            get { return _graphTypeSaMatrix; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeSaMatrix");
                _graphTypeSaMatrix = value;
            }
        }

        /// <summary>GRAPH_TYPE_MA_SIMPLE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_MA_SIMPLE")]
        public String GraphTypeMaSimple {
            get { return _graphTypeMaSimple; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeMaSimple");
                _graphTypeMaSimple = value;
            }
        }

        /// <summary>GRAPH_TYPE_MA_CROSS: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_MA_CROSS")]
        public String GraphTypeMaCross {
            get { return _graphTypeMaCross; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeMaCross");
                _graphTypeMaCross = value;
            }
        }

        /// <summary>GRAPH_TYPE_MA_MATRIX: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_MA_MATRIX")]
        public String GraphTypeMaMatrix {
            get { return _graphTypeMaMatrix; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeMaMatrix");
                _graphTypeMaMatrix = value;
            }
        }

        /// <summary>GRAPH_TYPE_N_RATE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_N_RATE")]
        public String GraphTypeNRate {
            get { return _graphTypeNRate; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeNRate");
                _graphTypeNRate = value;
            }
        }

        /// <summary>GRAPH_TYPE_N_RANKING: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRAPH_TYPE_N_RANKING")]
        public String GraphTypeNRanking {
            get { return _graphTypeNRanking; }
            set {
                __modifiedProperties.AddPropertyName("GraphTypeNRanking");
                _graphTypeNRanking = value;
            }
        }

        /// <summary>SET_EXECUTE_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("SET_EXECUTE_FLAG")]
        public int? SetExecuteFlag {
            get { return _setExecuteFlag; }
            set {
                __modifiedProperties.AddPropertyName("SetExecuteFlag");
                _setExecuteFlag = value;
            }
        }

        /// <summary>TITLE_ALL: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_ALL")]
        public String TitleAll {
            get { return _titleAll; }
            set {
                __modifiedProperties.AddPropertyName("TitleAll");
                _titleAll = value;
            }
        }

        /// <summary>TITLE_AXIS_ALL: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_AXIS_ALL")]
        public String TitleAxisAll {
            get { return _titleAxisAll; }
            set {
                __modifiedProperties.AddPropertyName("TitleAxisAll");
                _titleAxisAll = value;
            }
        }

        /// <summary>TITLE_NOANSWER: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_NOANSWER")]
        public String TitleNoanswer {
            get { return _titleNoanswer; }
            set {
                __modifiedProperties.AddPropertyName("TitleNoanswer");
                _titleNoanswer = value;
            }
        }

        /// <summary>TITLE_UNFIT: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_UNFIT")]
        public String TitleUnfit {
            get { return _titleUnfit; }
            set {
                __modifiedProperties.AddPropertyName("TitleUnfit");
                _titleUnfit = value;
            }
        }

        /// <summary>TITLE_BEFORE_WB: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_BEFORE_WB")]
        public String TitleBeforeWb {
            get { return _titleBeforeWb; }
            set {
                __modifiedProperties.AddPropertyName("TitleBeforeWb");
                _titleBeforeWb = value;
            }
        }

        /// <summary>FLAG_STATISTICS_PARAMETER: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_STATISTICS_PARAMETER")]
        public int? FlagStatisticsParameter {
            get { return _flagStatisticsParameter; }
            set {
                __modifiedProperties.AddPropertyName("FlagStatisticsParameter");
                _flagStatisticsParameter = value;
            }
        }

        /// <summary>TITLE_STATISTICS_PARAMETER: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_STATISTICS_PARAMETER")]
        public String TitleStatisticsParameter {
            get { return _titleStatisticsParameter; }
            set {
                __modifiedProperties.AddPropertyName("TitleStatisticsParameter");
                _titleStatisticsParameter = value;
            }
        }

        /// <summary>FLAG_TOTAL: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_TOTAL")]
        public int? FlagTotal {
            get { return _flagTotal; }
            set {
                __modifiedProperties.AddPropertyName("FlagTotal");
                _flagTotal = value;
            }
        }

        /// <summary>TITLE_TOTAL: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_TOTAL")]
        public String TitleTotal {
            get { return _titleTotal; }
            set {
                __modifiedProperties.AddPropertyName("TitleTotal");
                _titleTotal = value;
            }
        }

        /// <summary>DP_SUM: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_SUM")]
        public int? DpSum {
            get { return _dpSum; }
            set {
                __modifiedProperties.AddPropertyName("DpSum");
                _dpSum = value;
            }
        }

        /// <summary>FLAG_AVR: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_AVR")]
        public int? FlagAvr {
            get { return _flagAvr; }
            set {
                __modifiedProperties.AddPropertyName("FlagAvr");
                _flagAvr = value;
            }
        }

        /// <summary>TITLE_AVR: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_AVR")]
        public String TitleAvr {
            get { return _titleAvr; }
            set {
                __modifiedProperties.AddPropertyName("TitleAvr");
                _titleAvr = value;
            }
        }

        /// <summary>DP_AVR: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_AVR")]
        public int? DpAvr {
            get { return _dpAvr; }
            set {
                __modifiedProperties.AddPropertyName("DpAvr");
                _dpAvr = value;
            }
        }

        /// <summary>FLAG_SD: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_SD")]
        public int? FlagSd {
            get { return _flagSd; }
            set {
                __modifiedProperties.AddPropertyName("FlagSd");
                _flagSd = value;
            }
        }

        /// <summary>TITLE_SD: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_SD")]
        public String TitleSd {
            get { return _titleSd; }
            set {
                __modifiedProperties.AddPropertyName("TitleSd");
                _titleSd = value;
            }
        }

        /// <summary>DP_SD: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_SD")]
        public int? DpSd {
            get { return _dpSd; }
            set {
                __modifiedProperties.AddPropertyName("DpSd");
                _dpSd = value;
            }
        }

        /// <summary>FLAG_MIN: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_MIN")]
        public int? FlagMin {
            get { return _flagMin; }
            set {
                __modifiedProperties.AddPropertyName("FlagMin");
                _flagMin = value;
            }
        }

        /// <summary>TITLE_MIN: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_MIN")]
        public String TitleMin {
            get { return _titleMin; }
            set {
                __modifiedProperties.AddPropertyName("TitleMin");
                _titleMin = value;
            }
        }

        /// <summary>DP_MIN: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_MIN")]
        public int? DpMin {
            get { return _dpMin; }
            set {
                __modifiedProperties.AddPropertyName("DpMin");
                _dpMin = value;
            }
        }

        /// <summary>FLAG_MAX: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_MAX")]
        public int? FlagMax {
            get { return _flagMax; }
            set {
                __modifiedProperties.AddPropertyName("FlagMax");
                _flagMax = value;
            }
        }

        /// <summary>TITLE_MAX: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_MAX")]
        public String TitleMax {
            get { return _titleMax; }
            set {
                __modifiedProperties.AddPropertyName("TitleMax");
                _titleMax = value;
            }
        }

        /// <summary>DP_MAX: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_MAX")]
        public int? DpMax {
            get { return _dpMax; }
            set {
                __modifiedProperties.AddPropertyName("DpMax");
                _dpMax = value;
            }
        }

        /// <summary>FLAG_MEDIAN: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("FLAG_MEDIAN")]
        public int? FlagMedian {
            get { return _flagMedian; }
            set {
                __modifiedProperties.AddPropertyName("FlagMedian");
                _flagMedian = value;
            }
        }

        /// <summary>TITLE_MEDIAN: {NVARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("TITLE_MEDIAN")]
        public String TitleMedian {
            get { return _titleMedian; }
            set {
                __modifiedProperties.AddPropertyName("TitleMedian");
                _titleMedian = value;
            }
        }

        /// <summary>DP_MEDIAN: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_MEDIAN")]
        public int? DpMedian {
            get { return _dpMedian; }
            set {
                __modifiedProperties.AddPropertyName("DpMedian");
                _dpMedian = value;
            }
        }

        /// <summary>DP_WEIGHT: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_WEIGHT")]
        public int? DpWeight {
            get { return _dpWeight; }
            set {
                __modifiedProperties.AddPropertyName("DpWeight");
                _dpWeight = value;
            }
        }

        /// <summary>DP_WEIGHT_AVR: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("DP_WEIGHT_AVR")]
        public int? DpWeightAvr {
            get { return _dpWeightAvr; }
            set {
                __modifiedProperties.AddPropertyName("DpWeightAvr");
                _dpWeightAvr = value;
            }
        }

        /// <summary>EXCEL_TYPE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("EXCEL_TYPE")]
        public int? ExcelType {
            get { return _excelType; }
            set {
                __modifiedProperties.AddPropertyName("ExcelType");
                _excelType = value;
            }
        }

        /// <summary>PP_TYPE: {NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("PP_TYPE")]
        public int? PpType {
            get { return _ppType; }
            set {
                __modifiedProperties.AddPropertyName("PpType");
                _ppType = value;
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

        /// <summary>TEST_GT_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TEST_GT_FLAG")]
        public int? TestGtFlag {
            get { return _testGtFlag; }
            set {
                __modifiedProperties.AddPropertyName("TestGtFlag");
                _testGtFlag = value;
            }
        }

        /// <summary>TEST_CROSS_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("TEST_CROSS_FLAG")]
        public int? TestCrossFlag {
            get { return _testCrossFlag; }
            set {
                __modifiedProperties.AddPropertyName("TestCrossFlag");
                _testCrossFlag = value;
            }
        }

        /// <summary>TEST_TYPE_GT: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_TYPE_GT")]
        public int? TestTypeGt {
            get { return _testTypeGt; }
            set {
                __modifiedProperties.AddPropertyName("TestTypeGt");
                _testTypeGt = value;
            }
        }

        /// <summary>TEST_TYPE_CROSS: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_TYPE_CROSS")]
        public int? TestTypeCross {
            get { return _testTypeCross; }
            set {
                __modifiedProperties.AddPropertyName("TestTypeCross");
                _testTypeCross = value;
            }
        }

        /// <summary>TEST_SIGNIFICANCE_LV_GT: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_SIGNIFICANCE_LV_GT")]
        public int? TestSignificanceLvGt {
            get { return _testSignificanceLvGt; }
            set {
                __modifiedProperties.AddPropertyName("TestSignificanceLvGt");
                _testSignificanceLvGt = value;
            }
        }

        /// <summary>TEST_SIGNIFICANCE_LV_CROSS: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("TEST_SIGNIFICANCE_LV_CROSS")]
        public int? TestSignificanceLvCross {
            get { return _testSignificanceLvCross; }
            set {
                __modifiedProperties.AddPropertyName("TestSignificanceLvCross");
                _testSignificanceLvCross = value;
            }
        }

        #endregion
    }
}
