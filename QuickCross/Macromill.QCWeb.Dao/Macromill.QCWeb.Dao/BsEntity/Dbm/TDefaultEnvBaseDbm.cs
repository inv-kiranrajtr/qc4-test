
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity;

using Macromill.QCWeb.Dao.ExDao;
using Macromill.QCWeb.Dao.CBean;

namespace Macromill.QCWeb.Dao.BsEntity.Dbm {

    public class TDefaultEnvBaseDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TDefaultEnvBase);

        private static readonly TDefaultEnvBaseDbm _instance = new TDefaultEnvBaseDbm();
        private TDefaultEnvBaseDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TDefaultEnvBaseDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_BASE"; } }
        public override String TablePropertyName { get { return "TDefaultEnvBase"; } }
        public override String TableSqlName { get { return "T_DEFAULT_ENV_BASE"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnLanguage;
        protected ColumnInfo _columnNoanswerDenominatorFlag;
        protected ColumnInfo _columnVisibleUnfitFlag;
        protected ColumnInfo _columnNoanswerUnfitFlag;
        protected ColumnInfo _columnWeightbackFlag;
        protected ColumnInfo _columnCellJoincellJoinFlag;
        protected ColumnInfo _columnChartDirectionGtFlag;
        protected ColumnInfo _columnChartDirectionCrossFlag;
        protected ColumnInfo _columnNoanswerTargetFlag;
        protected ColumnInfo _columnNoanswerAxisFlag;
        protected ColumnInfo _columnUnfitTargetFlag;
        protected ColumnInfo _columnUnfitAxisFlag;
        protected ColumnInfo _columnTotalnumFlag;
        protected ColumnInfo _columnRateDiffColorMinus5;
        protected ColumnInfo _columnRateDiffColorMinus10;
        protected ColumnInfo _columnRateDiffColorPlus5;
        protected ColumnInfo _columnRateDiffColorPlus10;
        protected ColumnInfo _columnGraphTypeSa;
        protected ColumnInfo _columnGraphTypeSaMatrix;
        protected ColumnInfo _columnGraphTypeMaSimple;
        protected ColumnInfo _columnGraphTypeMaCross;
        protected ColumnInfo _columnGraphTypeMaMatrix;
        protected ColumnInfo _columnGraphTypeNRate;
        protected ColumnInfo _columnGraphTypeNRanking;
        protected ColumnInfo _columnSetExecuteFlag;
        protected ColumnInfo _columnTitleAll;
        protected ColumnInfo _columnTitleAxisAll;
        protected ColumnInfo _columnTitleNoanswer;
        protected ColumnInfo _columnTitleUnfit;
        protected ColumnInfo _columnTitleBeforeWb;
        protected ColumnInfo _columnFlagStatisticsParameter;
        protected ColumnInfo _columnTitleStatisticsParameter;
        protected ColumnInfo _columnFlagTotal;
        protected ColumnInfo _columnTitleTotal;
        protected ColumnInfo _columnDpSum;
        protected ColumnInfo _columnFlagAvr;
        protected ColumnInfo _columnTitleAvr;
        protected ColumnInfo _columnDpAvr;
        protected ColumnInfo _columnFlagSd;
        protected ColumnInfo _columnTitleSd;
        protected ColumnInfo _columnDpSd;
        protected ColumnInfo _columnFlagMin;
        protected ColumnInfo _columnTitleMin;
        protected ColumnInfo _columnDpMin;
        protected ColumnInfo _columnFlagMax;
        protected ColumnInfo _columnTitleMax;
        protected ColumnInfo _columnDpMax;
        protected ColumnInfo _columnFlagMedian;
        protected ColumnInfo _columnTitleMedian;
        protected ColumnInfo _columnDpMedian;
        protected ColumnInfo _columnDpWeight;
        protected ColumnInfo _columnDpWeightAvr;
        protected ColumnInfo _columnTestGtFlag;
        protected ColumnInfo _columnTestCrossFlag;
        protected ColumnInfo _columnTestTypeGt;
        protected ColumnInfo _columnTestTypeCross;
        protected ColumnInfo _columnTestSignificanceLvGt;
        protected ColumnInfo _columnTestSignificanceLvCross;

        public ColumnInfo ColumnLanguage { get { return _columnLanguage; } }
        public ColumnInfo ColumnNoanswerDenominatorFlag { get { return _columnNoanswerDenominatorFlag; } }
        public ColumnInfo ColumnVisibleUnfitFlag { get { return _columnVisibleUnfitFlag; } }
        public ColumnInfo ColumnNoanswerUnfitFlag { get { return _columnNoanswerUnfitFlag; } }
        public ColumnInfo ColumnWeightbackFlag { get { return _columnWeightbackFlag; } }
        public ColumnInfo ColumnCellJoincellJoinFlag { get { return _columnCellJoincellJoinFlag; } }
        public ColumnInfo ColumnChartDirectionGtFlag { get { return _columnChartDirectionGtFlag; } }
        public ColumnInfo ColumnChartDirectionCrossFlag { get { return _columnChartDirectionCrossFlag; } }
        public ColumnInfo ColumnNoanswerTargetFlag { get { return _columnNoanswerTargetFlag; } }
        public ColumnInfo ColumnNoanswerAxisFlag { get { return _columnNoanswerAxisFlag; } }
        public ColumnInfo ColumnUnfitTargetFlag { get { return _columnUnfitTargetFlag; } }
        public ColumnInfo ColumnUnfitAxisFlag { get { return _columnUnfitAxisFlag; } }
        public ColumnInfo ColumnTotalnumFlag { get { return _columnTotalnumFlag; } }
        public ColumnInfo ColumnRateDiffColorMinus5 { get { return _columnRateDiffColorMinus5; } }
        public ColumnInfo ColumnRateDiffColorMinus10 { get { return _columnRateDiffColorMinus10; } }
        public ColumnInfo ColumnRateDiffColorPlus5 { get { return _columnRateDiffColorPlus5; } }
        public ColumnInfo ColumnRateDiffColorPlus10 { get { return _columnRateDiffColorPlus10; } }
        public ColumnInfo ColumnGraphTypeSa { get { return _columnGraphTypeSa; } }
        public ColumnInfo ColumnGraphTypeSaMatrix { get { return _columnGraphTypeSaMatrix; } }
        public ColumnInfo ColumnGraphTypeMaSimple { get { return _columnGraphTypeMaSimple; } }
        public ColumnInfo ColumnGraphTypeMaCross { get { return _columnGraphTypeMaCross; } }
        public ColumnInfo ColumnGraphTypeMaMatrix { get { return _columnGraphTypeMaMatrix; } }
        public ColumnInfo ColumnGraphTypeNRate { get { return _columnGraphTypeNRate; } }
        public ColumnInfo ColumnGraphTypeNRanking { get { return _columnGraphTypeNRanking; } }
        public ColumnInfo ColumnSetExecuteFlag { get { return _columnSetExecuteFlag; } }
        public ColumnInfo ColumnTitleAll { get { return _columnTitleAll; } }
        public ColumnInfo ColumnTitleAxisAll { get { return _columnTitleAxisAll; } }
        public ColumnInfo ColumnTitleNoanswer { get { return _columnTitleNoanswer; } }
        public ColumnInfo ColumnTitleUnfit { get { return _columnTitleUnfit; } }
        public ColumnInfo ColumnTitleBeforeWb { get { return _columnTitleBeforeWb; } }
        public ColumnInfo ColumnFlagStatisticsParameter { get { return _columnFlagStatisticsParameter; } }
        public ColumnInfo ColumnTitleStatisticsParameter { get { return _columnTitleStatisticsParameter; } }
        public ColumnInfo ColumnFlagTotal { get { return _columnFlagTotal; } }
        public ColumnInfo ColumnTitleTotal { get { return _columnTitleTotal; } }
        public ColumnInfo ColumnDpSum { get { return _columnDpSum; } }
        public ColumnInfo ColumnFlagAvr { get { return _columnFlagAvr; } }
        public ColumnInfo ColumnTitleAvr { get { return _columnTitleAvr; } }
        public ColumnInfo ColumnDpAvr { get { return _columnDpAvr; } }
        public ColumnInfo ColumnFlagSd { get { return _columnFlagSd; } }
        public ColumnInfo ColumnTitleSd { get { return _columnTitleSd; } }
        public ColumnInfo ColumnDpSd { get { return _columnDpSd; } }
        public ColumnInfo ColumnFlagMin { get { return _columnFlagMin; } }
        public ColumnInfo ColumnTitleMin { get { return _columnTitleMin; } }
        public ColumnInfo ColumnDpMin { get { return _columnDpMin; } }
        public ColumnInfo ColumnFlagMax { get { return _columnFlagMax; } }
        public ColumnInfo ColumnTitleMax { get { return _columnTitleMax; } }
        public ColumnInfo ColumnDpMax { get { return _columnDpMax; } }
        public ColumnInfo ColumnFlagMedian { get { return _columnFlagMedian; } }
        public ColumnInfo ColumnTitleMedian { get { return _columnTitleMedian; } }
        public ColumnInfo ColumnDpMedian { get { return _columnDpMedian; } }
        public ColumnInfo ColumnDpWeight { get { return _columnDpWeight; } }
        public ColumnInfo ColumnDpWeightAvr { get { return _columnDpWeightAvr; } }
        public ColumnInfo ColumnTestGtFlag { get { return _columnTestGtFlag; } }
        public ColumnInfo ColumnTestCrossFlag { get { return _columnTestCrossFlag; } }
        public ColumnInfo ColumnTestTypeGt { get { return _columnTestTypeGt; } }
        public ColumnInfo ColumnTestTypeCross { get { return _columnTestTypeCross; } }
        public ColumnInfo ColumnTestSignificanceLvGt { get { return _columnTestSignificanceLvGt; } }
        public ColumnInfo ColumnTestSignificanceLvCross { get { return _columnTestSignificanceLvCross; } }

        protected void InitializeColumnInfo() {
            _columnLanguage = cci("LANGUAGE", "LANGUAGE", null, null, true, "Language", typeof(String), true, "VARCHAR2", 5, 0, false, OptimisticLockType.NONE, null, null, "TDefaultEnvColorInfoCList");
            _columnNoanswerDenominatorFlag = cci("NOANSWER_DENOMINATOR_FLAG", "NOANSWER_DENOMINATOR_FLAG", null, null, true, "NoanswerDenominatorFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnVisibleUnfitFlag = cci("VISIBLE_UNFIT_FLAG", "VISIBLE_UNFIT_FLAG", null, null, true, "VisibleUnfitFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoanswerUnfitFlag = cci("NOANSWER_UNFIT_FLAG", "NOANSWER_UNFIT_FLAG", null, null, true, "NoanswerUnfitFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackFlag = cci("WEIGHTBACK_FLAG", "WEIGHTBACK_FLAG", null, null, true, "WeightbackFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCellJoincellJoinFlag = cci("CELL_JOINCELL_JOIN_FLAG", "CELL_JOINCELL_JOIN_FLAG", null, null, true, "CellJoincellJoinFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnChartDirectionGtFlag = cci("CHART_DIRECTION_GT_FLAG", "CHART_DIRECTION_GT_FLAG", null, null, true, "ChartDirectionGtFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnChartDirectionCrossFlag = cci("CHART_DIRECTION_CROSS_FLAG", "CHART_DIRECTION_CROSS_FLAG", null, null, true, "ChartDirectionCrossFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoanswerTargetFlag = cci("NOANSWER_TARGET_FLAG", "NOANSWER_TARGET_FLAG", null, null, true, "NoanswerTargetFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnNoanswerAxisFlag = cci("NOANSWER_AXIS_FLAG", "NOANSWER_AXIS_FLAG", null, null, true, "NoanswerAxisFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUnfitTargetFlag = cci("UNFIT_TARGET_FLAG", "UNFIT_TARGET_FLAG", null, null, true, "UnfitTargetFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnUnfitAxisFlag = cci("UNFIT_AXIS_FLAG", "UNFIT_AXIS_FLAG", null, null, true, "UnfitAxisFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTotalnumFlag = cci("TOTALNUM_FLAG", "TOTALNUM_FLAG", null, null, true, "TotalnumFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRateDiffColorMinus5 = cci("RATE_DIFF_COLOR_MINUS5", "RATE_DIFF_COLOR_MINUS5", null, null, true, "RateDiffColorMinus5", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRateDiffColorMinus10 = cci("RATE_DIFF_COLOR_MINUS10", "RATE_DIFF_COLOR_MINUS10", null, null, true, "RateDiffColorMinus10", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRateDiffColorPlus5 = cci("RATE_DIFF_COLOR_PLUS5", "RATE_DIFF_COLOR_PLUS5", null, null, true, "RateDiffColorPlus5", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRateDiffColorPlus10 = cci("RATE_DIFF_COLOR_PLUS10", "RATE_DIFF_COLOR_PLUS10", null, null, true, "RateDiffColorPlus10", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeSa = cci("GRAPH_TYPE_SA", "GRAPH_TYPE_SA", null, null, true, "GraphTypeSa", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeSaMatrix = cci("GRAPH_TYPE_SA_MATRIX", "GRAPH_TYPE_SA_MATRIX", null, null, true, "GraphTypeSaMatrix", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeMaSimple = cci("GRAPH_TYPE_MA_SIMPLE", "GRAPH_TYPE_MA_SIMPLE", null, null, true, "GraphTypeMaSimple", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeMaCross = cci("GRAPH_TYPE_MA_CROSS", "GRAPH_TYPE_MA_CROSS", null, null, true, "GraphTypeMaCross", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeMaMatrix = cci("GRAPH_TYPE_MA_MATRIX", "GRAPH_TYPE_MA_MATRIX", null, null, true, "GraphTypeMaMatrix", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeNRate = cci("GRAPH_TYPE_N_RATE", "GRAPH_TYPE_N_RATE", null, null, true, "GraphTypeNRate", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphTypeNRanking = cci("GRAPH_TYPE_N_RANKING", "GRAPH_TYPE_N_RANKING", null, null, true, "GraphTypeNRanking", typeof(String), false, "CHAR", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSetExecuteFlag = cci("SET_EXECUTE_FLAG", "SET_EXECUTE_FLAG", null, null, true, "SetExecuteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleAll = cci("TITLE_ALL", "TITLE_ALL", null, null, false, "TitleAll", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleAxisAll = cci("TITLE_AXIS_ALL", "TITLE_AXIS_ALL", null, null, false, "TitleAxisAll", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleNoanswer = cci("TITLE_NOANSWER", "TITLE_NOANSWER", null, null, false, "TitleNoanswer", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleUnfit = cci("TITLE_UNFIT", "TITLE_UNFIT", null, null, false, "TitleUnfit", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleBeforeWb = cci("TITLE_BEFORE_WB", "TITLE_BEFORE_WB", null, null, false, "TitleBeforeWb", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagStatisticsParameter = cci("FLAG_STATISTICS_PARAMETER", "FLAG_STATISTICS_PARAMETER", null, null, false, "FlagStatisticsParameter", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleStatisticsParameter = cci("TITLE_STATISTICS_PARAMETER", "TITLE_STATISTICS_PARAMETER", null, null, false, "TitleStatisticsParameter", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagTotal = cci("FLAG_TOTAL", "FLAG_TOTAL", null, null, false, "FlagTotal", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleTotal = cci("TITLE_TOTAL", "TITLE_TOTAL", null, null, false, "TitleTotal", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpSum = cci("DP_SUM", "DP_SUM", null, null, false, "DpSum", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagAvr = cci("FLAG_AVR", "FLAG_AVR", null, null, false, "FlagAvr", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleAvr = cci("TITLE_AVR", "TITLE_AVR", null, null, false, "TitleAvr", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpAvr = cci("DP_AVR", "DP_AVR", null, null, false, "DpAvr", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagSd = cci("FLAG_SD", "FLAG_SD", null, null, false, "FlagSd", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleSd = cci("TITLE_SD", "TITLE_SD", null, null, false, "TitleSd", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpSd = cci("DP_SD", "DP_SD", null, null, false, "DpSd", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagMin = cci("FLAG_MIN", "FLAG_MIN", null, null, false, "FlagMin", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleMin = cci("TITLE_MIN", "TITLE_MIN", null, null, false, "TitleMin", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpMin = cci("DP_MIN", "DP_MIN", null, null, false, "DpMin", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagMax = cci("FLAG_MAX", "FLAG_MAX", null, null, false, "FlagMax", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleMax = cci("TITLE_MAX", "TITLE_MAX", null, null, false, "TitleMax", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpMax = cci("DP_MAX", "DP_MAX", null, null, false, "DpMax", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFlagMedian = cci("FLAG_MEDIAN", "FLAG_MEDIAN", null, null, false, "FlagMedian", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTitleMedian = cci("TITLE_MEDIAN", "TITLE_MEDIAN", null, null, false, "TitleMedian", typeof(String), false, "NVARCHAR2", 25, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpMedian = cci("DP_MEDIAN", "DP_MEDIAN", null, null, false, "DpMedian", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpWeight = cci("DP_WEIGHT", "DP_WEIGHT", null, null, false, "DpWeight", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDpWeightAvr = cci("DP_WEIGHT_AVR", "DP_WEIGHT_AVR", null, null, false, "DpWeightAvr", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestGtFlag = cci("TEST_GT_FLAG", "TEST_GT_FLAG", null, null, false, "TestGtFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestCrossFlag = cci("TEST_CROSS_FLAG", "TEST_CROSS_FLAG", null, null, false, "TestCrossFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestTypeGt = cci("TEST_TYPE_GT", "TEST_TYPE_GT", null, null, false, "TestTypeGt", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestTypeCross = cci("TEST_TYPE_CROSS", "TEST_TYPE_CROSS", null, null, false, "TestTypeCross", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestSignificanceLvGt = cci("TEST_SIGNIFICANCE_LV_GT", "TEST_SIGNIFICANCE_LV_GT", null, null, false, "TestSignificanceLvGt", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestSignificanceLvCross = cci("TEST_SIGNIFICANCE_LV_CROSS", "TEST_SIGNIFICANCE_LV_CROSS", null, null, false, "TestSignificanceLvCross", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnLanguage);
            _columnInfoList.add(ColumnNoanswerDenominatorFlag);
            _columnInfoList.add(ColumnVisibleUnfitFlag);
            _columnInfoList.add(ColumnNoanswerUnfitFlag);
            _columnInfoList.add(ColumnWeightbackFlag);
            _columnInfoList.add(ColumnCellJoincellJoinFlag);
            _columnInfoList.add(ColumnChartDirectionGtFlag);
            _columnInfoList.add(ColumnChartDirectionCrossFlag);
            _columnInfoList.add(ColumnNoanswerTargetFlag);
            _columnInfoList.add(ColumnNoanswerAxisFlag);
            _columnInfoList.add(ColumnUnfitTargetFlag);
            _columnInfoList.add(ColumnUnfitAxisFlag);
            _columnInfoList.add(ColumnTotalnumFlag);
            _columnInfoList.add(ColumnRateDiffColorMinus5);
            _columnInfoList.add(ColumnRateDiffColorMinus10);
            _columnInfoList.add(ColumnRateDiffColorPlus5);
            _columnInfoList.add(ColumnRateDiffColorPlus10);
            _columnInfoList.add(ColumnGraphTypeSa);
            _columnInfoList.add(ColumnGraphTypeSaMatrix);
            _columnInfoList.add(ColumnGraphTypeMaSimple);
            _columnInfoList.add(ColumnGraphTypeMaCross);
            _columnInfoList.add(ColumnGraphTypeMaMatrix);
            _columnInfoList.add(ColumnGraphTypeNRate);
            _columnInfoList.add(ColumnGraphTypeNRanking);
            _columnInfoList.add(ColumnSetExecuteFlag);
            _columnInfoList.add(ColumnTitleAll);
            _columnInfoList.add(ColumnTitleAxisAll);
            _columnInfoList.add(ColumnTitleNoanswer);
            _columnInfoList.add(ColumnTitleUnfit);
            _columnInfoList.add(ColumnTitleBeforeWb);
            _columnInfoList.add(ColumnFlagStatisticsParameter);
            _columnInfoList.add(ColumnTitleStatisticsParameter);
            _columnInfoList.add(ColumnFlagTotal);
            _columnInfoList.add(ColumnTitleTotal);
            _columnInfoList.add(ColumnDpSum);
            _columnInfoList.add(ColumnFlagAvr);
            _columnInfoList.add(ColumnTitleAvr);
            _columnInfoList.add(ColumnDpAvr);
            _columnInfoList.add(ColumnFlagSd);
            _columnInfoList.add(ColumnTitleSd);
            _columnInfoList.add(ColumnDpSd);
            _columnInfoList.add(ColumnFlagMin);
            _columnInfoList.add(ColumnTitleMin);
            _columnInfoList.add(ColumnDpMin);
            _columnInfoList.add(ColumnFlagMax);
            _columnInfoList.add(ColumnTitleMax);
            _columnInfoList.add(ColumnDpMax);
            _columnInfoList.add(ColumnFlagMedian);
            _columnInfoList.add(ColumnTitleMedian);
            _columnInfoList.add(ColumnDpMedian);
            _columnInfoList.add(ColumnDpWeight);
            _columnInfoList.add(ColumnDpWeightAvr);
            _columnInfoList.add(ColumnTestGtFlag);
            _columnInfoList.add(ColumnTestCrossFlag);
            _columnInfoList.add(ColumnTestTypeGt);
            _columnInfoList.add(ColumnTestTypeCross);
            _columnInfoList.add(ColumnTestSignificanceLvGt);
            _columnInfoList.add(ColumnTestSignificanceLvCross);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnLanguage);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return false; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTDefaultEnvColorInfoCList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnLanguage, TDefaultEnvColorInfoCDbm.GetInstance().ColumnLanguage);
            return cri("TDefaultEnvColorInfoCList", this, TDefaultEnvColorInfoCDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasCommonColumn { get { return false; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_DEFAULT_ENV_BASE";
        public static readonly String TABLE_PROPERTY_NAME = "TDefaultEnvBase";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_LANGUAGE = "LANGUAGE";
        public static readonly String DB_NAME_NOANSWER_DENOMINATOR_FLAG = "NOANSWER_DENOMINATOR_FLAG";
        public static readonly String DB_NAME_VISIBLE_UNFIT_FLAG = "VISIBLE_UNFIT_FLAG";
        public static readonly String DB_NAME_NOANSWER_UNFIT_FLAG = "NOANSWER_UNFIT_FLAG";
        public static readonly String DB_NAME_WEIGHTBACK_FLAG = "WEIGHTBACK_FLAG";
        public static readonly String DB_NAME_CELL_JOINCELL_JOIN_FLAG = "CELL_JOINCELL_JOIN_FLAG";
        public static readonly String DB_NAME_CHART_DIRECTION_GT_FLAG = "CHART_DIRECTION_GT_FLAG";
        public static readonly String DB_NAME_CHART_DIRECTION_CROSS_FLAG = "CHART_DIRECTION_CROSS_FLAG";
        public static readonly String DB_NAME_NOANSWER_TARGET_FLAG = "NOANSWER_TARGET_FLAG";
        public static readonly String DB_NAME_NOANSWER_AXIS_FLAG = "NOANSWER_AXIS_FLAG";
        public static readonly String DB_NAME_UNFIT_TARGET_FLAG = "UNFIT_TARGET_FLAG";
        public static readonly String DB_NAME_UNFIT_AXIS_FLAG = "UNFIT_AXIS_FLAG";
        public static readonly String DB_NAME_TOTALNUM_FLAG = "TOTALNUM_FLAG";
        public static readonly String DB_NAME_RATE_DIFF_COLOR_MINUS5 = "RATE_DIFF_COLOR_MINUS5";
        public static readonly String DB_NAME_RATE_DIFF_COLOR_MINUS10 = "RATE_DIFF_COLOR_MINUS10";
        public static readonly String DB_NAME_RATE_DIFF_COLOR_PLUS5 = "RATE_DIFF_COLOR_PLUS5";
        public static readonly String DB_NAME_RATE_DIFF_COLOR_PLUS10 = "RATE_DIFF_COLOR_PLUS10";
        public static readonly String DB_NAME_GRAPH_TYPE_SA = "GRAPH_TYPE_SA";
        public static readonly String DB_NAME_GRAPH_TYPE_SA_MATRIX = "GRAPH_TYPE_SA_MATRIX";
        public static readonly String DB_NAME_GRAPH_TYPE_MA_SIMPLE = "GRAPH_TYPE_MA_SIMPLE";
        public static readonly String DB_NAME_GRAPH_TYPE_MA_CROSS = "GRAPH_TYPE_MA_CROSS";
        public static readonly String DB_NAME_GRAPH_TYPE_MA_MATRIX = "GRAPH_TYPE_MA_MATRIX";
        public static readonly String DB_NAME_GRAPH_TYPE_N_RATE = "GRAPH_TYPE_N_RATE";
        public static readonly String DB_NAME_GRAPH_TYPE_N_RANKING = "GRAPH_TYPE_N_RANKING";
        public static readonly String DB_NAME_SET_EXECUTE_FLAG = "SET_EXECUTE_FLAG";
        public static readonly String DB_NAME_TITLE_ALL = "TITLE_ALL";
        public static readonly String DB_NAME_TITLE_AXIS_ALL = "TITLE_AXIS_ALL";
        public static readonly String DB_NAME_TITLE_NOANSWER = "TITLE_NOANSWER";
        public static readonly String DB_NAME_TITLE_UNFIT = "TITLE_UNFIT";
        public static readonly String DB_NAME_TITLE_BEFORE_WB = "TITLE_BEFORE_WB";
        public static readonly String DB_NAME_FLAG_STATISTICS_PARAMETER = "FLAG_STATISTICS_PARAMETER";
        public static readonly String DB_NAME_TITLE_STATISTICS_PARAMETER = "TITLE_STATISTICS_PARAMETER";
        public static readonly String DB_NAME_FLAG_TOTAL = "FLAG_TOTAL";
        public static readonly String DB_NAME_TITLE_TOTAL = "TITLE_TOTAL";
        public static readonly String DB_NAME_DP_SUM = "DP_SUM";
        public static readonly String DB_NAME_FLAG_AVR = "FLAG_AVR";
        public static readonly String DB_NAME_TITLE_AVR = "TITLE_AVR";
        public static readonly String DB_NAME_DP_AVR = "DP_AVR";
        public static readonly String DB_NAME_FLAG_SD = "FLAG_SD";
        public static readonly String DB_NAME_TITLE_SD = "TITLE_SD";
        public static readonly String DB_NAME_DP_SD = "DP_SD";
        public static readonly String DB_NAME_FLAG_MIN = "FLAG_MIN";
        public static readonly String DB_NAME_TITLE_MIN = "TITLE_MIN";
        public static readonly String DB_NAME_DP_MIN = "DP_MIN";
        public static readonly String DB_NAME_FLAG_MAX = "FLAG_MAX";
        public static readonly String DB_NAME_TITLE_MAX = "TITLE_MAX";
        public static readonly String DB_NAME_DP_MAX = "DP_MAX";
        public static readonly String DB_NAME_FLAG_MEDIAN = "FLAG_MEDIAN";
        public static readonly String DB_NAME_TITLE_MEDIAN = "TITLE_MEDIAN";
        public static readonly String DB_NAME_DP_MEDIAN = "DP_MEDIAN";
        public static readonly String DB_NAME_DP_WEIGHT = "DP_WEIGHT";
        public static readonly String DB_NAME_DP_WEIGHT_AVR = "DP_WEIGHT_AVR";
        public static readonly String DB_NAME_TEST_GT_FLAG = "TEST_GT_FLAG";
        public static readonly String DB_NAME_TEST_CROSS_FLAG = "TEST_CROSS_FLAG";
        public static readonly String DB_NAME_TEST_TYPE_GT = "TEST_TYPE_GT";
        public static readonly String DB_NAME_TEST_TYPE_CROSS = "TEST_TYPE_CROSS";
        public static readonly String DB_NAME_TEST_SIGNIFICANCE_LV_GT = "TEST_SIGNIFICANCE_LV_GT";
        public static readonly String DB_NAME_TEST_SIGNIFICANCE_LV_CROSS = "TEST_SIGNIFICANCE_LV_CROSS";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_LANGUAGE = "Language";
        public static readonly String PROPERTY_NAME_NOANSWER_DENOMINATOR_FLAG = "NoanswerDenominatorFlag";
        public static readonly String PROPERTY_NAME_VISIBLE_UNFIT_FLAG = "VisibleUnfitFlag";
        public static readonly String PROPERTY_NAME_NOANSWER_UNFIT_FLAG = "NoanswerUnfitFlag";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_FLAG = "WeightbackFlag";
        public static readonly String PROPERTY_NAME_CELL_JOINCELL_JOIN_FLAG = "CellJoincellJoinFlag";
        public static readonly String PROPERTY_NAME_CHART_DIRECTION_GT_FLAG = "ChartDirectionGtFlag";
        public static readonly String PROPERTY_NAME_CHART_DIRECTION_CROSS_FLAG = "ChartDirectionCrossFlag";
        public static readonly String PROPERTY_NAME_NOANSWER_TARGET_FLAG = "NoanswerTargetFlag";
        public static readonly String PROPERTY_NAME_NOANSWER_AXIS_FLAG = "NoanswerAxisFlag";
        public static readonly String PROPERTY_NAME_UNFIT_TARGET_FLAG = "UnfitTargetFlag";
        public static readonly String PROPERTY_NAME_UNFIT_AXIS_FLAG = "UnfitAxisFlag";
        public static readonly String PROPERTY_NAME_TOTALNUM_FLAG = "TotalnumFlag";
        public static readonly String PROPERTY_NAME_RATE_DIFF_COLOR_MINUS5 = "RateDiffColorMinus5";
        public static readonly String PROPERTY_NAME_RATE_DIFF_COLOR_MINUS10 = "RateDiffColorMinus10";
        public static readonly String PROPERTY_NAME_RATE_DIFF_COLOR_PLUS5 = "RateDiffColorPlus5";
        public static readonly String PROPERTY_NAME_RATE_DIFF_COLOR_PLUS10 = "RateDiffColorPlus10";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_SA = "GraphTypeSa";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_SA_MATRIX = "GraphTypeSaMatrix";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_MA_SIMPLE = "GraphTypeMaSimple";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_MA_CROSS = "GraphTypeMaCross";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_MA_MATRIX = "GraphTypeMaMatrix";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_N_RATE = "GraphTypeNRate";
        public static readonly String PROPERTY_NAME_GRAPH_TYPE_N_RANKING = "GraphTypeNRanking";
        public static readonly String PROPERTY_NAME_SET_EXECUTE_FLAG = "SetExecuteFlag";
        public static readonly String PROPERTY_NAME_TITLE_ALL = "TitleAll";
        public static readonly String PROPERTY_NAME_TITLE_AXIS_ALL = "TitleAxisAll";
        public static readonly String PROPERTY_NAME_TITLE_NOANSWER = "TitleNoanswer";
        public static readonly String PROPERTY_NAME_TITLE_UNFIT = "TitleUnfit";
        public static readonly String PROPERTY_NAME_TITLE_BEFORE_WB = "TitleBeforeWb";
        public static readonly String PROPERTY_NAME_FLAG_STATISTICS_PARAMETER = "FlagStatisticsParameter";
        public static readonly String PROPERTY_NAME_TITLE_STATISTICS_PARAMETER = "TitleStatisticsParameter";
        public static readonly String PROPERTY_NAME_FLAG_TOTAL = "FlagTotal";
        public static readonly String PROPERTY_NAME_TITLE_TOTAL = "TitleTotal";
        public static readonly String PROPERTY_NAME_DP_SUM = "DpSum";
        public static readonly String PROPERTY_NAME_FLAG_AVR = "FlagAvr";
        public static readonly String PROPERTY_NAME_TITLE_AVR = "TitleAvr";
        public static readonly String PROPERTY_NAME_DP_AVR = "DpAvr";
        public static readonly String PROPERTY_NAME_FLAG_SD = "FlagSd";
        public static readonly String PROPERTY_NAME_TITLE_SD = "TitleSd";
        public static readonly String PROPERTY_NAME_DP_SD = "DpSd";
        public static readonly String PROPERTY_NAME_FLAG_MIN = "FlagMin";
        public static readonly String PROPERTY_NAME_TITLE_MIN = "TitleMin";
        public static readonly String PROPERTY_NAME_DP_MIN = "DpMin";
        public static readonly String PROPERTY_NAME_FLAG_MAX = "FlagMax";
        public static readonly String PROPERTY_NAME_TITLE_MAX = "TitleMax";
        public static readonly String PROPERTY_NAME_DP_MAX = "DpMax";
        public static readonly String PROPERTY_NAME_FLAG_MEDIAN = "FlagMedian";
        public static readonly String PROPERTY_NAME_TITLE_MEDIAN = "TitleMedian";
        public static readonly String PROPERTY_NAME_DP_MEDIAN = "DpMedian";
        public static readonly String PROPERTY_NAME_DP_WEIGHT = "DpWeight";
        public static readonly String PROPERTY_NAME_DP_WEIGHT_AVR = "DpWeightAvr";
        public static readonly String PROPERTY_NAME_TEST_GT_FLAG = "TestGtFlag";
        public static readonly String PROPERTY_NAME_TEST_CROSS_FLAG = "TestCrossFlag";
        public static readonly String PROPERTY_NAME_TEST_TYPE_GT = "TestTypeGt";
        public static readonly String PROPERTY_NAME_TEST_TYPE_CROSS = "TestTypeCross";
        public static readonly String PROPERTY_NAME_TEST_SIGNIFICANCE_LV_GT = "TestSignificanceLvGt";
        public static readonly String PROPERTY_NAME_TEST_SIGNIFICANCE_LV_CROSS = "TestSignificanceLvCross";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TDefaultEnvColorInfoCList = "TDefaultEnvColorInfoCList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TDefaultEnvBaseDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_LANGUAGE.ToLower(), PROPERTY_NAME_LANGUAGE);
                map.put(DB_NAME_NOANSWER_DENOMINATOR_FLAG.ToLower(), PROPERTY_NAME_NOANSWER_DENOMINATOR_FLAG);
                map.put(DB_NAME_VISIBLE_UNFIT_FLAG.ToLower(), PROPERTY_NAME_VISIBLE_UNFIT_FLAG);
                map.put(DB_NAME_NOANSWER_UNFIT_FLAG.ToLower(), PROPERTY_NAME_NOANSWER_UNFIT_FLAG);
                map.put(DB_NAME_WEIGHTBACK_FLAG.ToLower(), PROPERTY_NAME_WEIGHTBACK_FLAG);
                map.put(DB_NAME_CELL_JOINCELL_JOIN_FLAG.ToLower(), PROPERTY_NAME_CELL_JOINCELL_JOIN_FLAG);
                map.put(DB_NAME_CHART_DIRECTION_GT_FLAG.ToLower(), PROPERTY_NAME_CHART_DIRECTION_GT_FLAG);
                map.put(DB_NAME_CHART_DIRECTION_CROSS_FLAG.ToLower(), PROPERTY_NAME_CHART_DIRECTION_CROSS_FLAG);
                map.put(DB_NAME_NOANSWER_TARGET_FLAG.ToLower(), PROPERTY_NAME_NOANSWER_TARGET_FLAG);
                map.put(DB_NAME_NOANSWER_AXIS_FLAG.ToLower(), PROPERTY_NAME_NOANSWER_AXIS_FLAG);
                map.put(DB_NAME_UNFIT_TARGET_FLAG.ToLower(), PROPERTY_NAME_UNFIT_TARGET_FLAG);
                map.put(DB_NAME_UNFIT_AXIS_FLAG.ToLower(), PROPERTY_NAME_UNFIT_AXIS_FLAG);
                map.put(DB_NAME_TOTALNUM_FLAG.ToLower(), PROPERTY_NAME_TOTALNUM_FLAG);
                map.put(DB_NAME_RATE_DIFF_COLOR_MINUS5.ToLower(), PROPERTY_NAME_RATE_DIFF_COLOR_MINUS5);
                map.put(DB_NAME_RATE_DIFF_COLOR_MINUS10.ToLower(), PROPERTY_NAME_RATE_DIFF_COLOR_MINUS10);
                map.put(DB_NAME_RATE_DIFF_COLOR_PLUS5.ToLower(), PROPERTY_NAME_RATE_DIFF_COLOR_PLUS5);
                map.put(DB_NAME_RATE_DIFF_COLOR_PLUS10.ToLower(), PROPERTY_NAME_RATE_DIFF_COLOR_PLUS10);
                map.put(DB_NAME_GRAPH_TYPE_SA.ToLower(), PROPERTY_NAME_GRAPH_TYPE_SA);
                map.put(DB_NAME_GRAPH_TYPE_SA_MATRIX.ToLower(), PROPERTY_NAME_GRAPH_TYPE_SA_MATRIX);
                map.put(DB_NAME_GRAPH_TYPE_MA_SIMPLE.ToLower(), PROPERTY_NAME_GRAPH_TYPE_MA_SIMPLE);
                map.put(DB_NAME_GRAPH_TYPE_MA_CROSS.ToLower(), PROPERTY_NAME_GRAPH_TYPE_MA_CROSS);
                map.put(DB_NAME_GRAPH_TYPE_MA_MATRIX.ToLower(), PROPERTY_NAME_GRAPH_TYPE_MA_MATRIX);
                map.put(DB_NAME_GRAPH_TYPE_N_RATE.ToLower(), PROPERTY_NAME_GRAPH_TYPE_N_RATE);
                map.put(DB_NAME_GRAPH_TYPE_N_RANKING.ToLower(), PROPERTY_NAME_GRAPH_TYPE_N_RANKING);
                map.put(DB_NAME_SET_EXECUTE_FLAG.ToLower(), PROPERTY_NAME_SET_EXECUTE_FLAG);
                map.put(DB_NAME_TITLE_ALL.ToLower(), PROPERTY_NAME_TITLE_ALL);
                map.put(DB_NAME_TITLE_AXIS_ALL.ToLower(), PROPERTY_NAME_TITLE_AXIS_ALL);
                map.put(DB_NAME_TITLE_NOANSWER.ToLower(), PROPERTY_NAME_TITLE_NOANSWER);
                map.put(DB_NAME_TITLE_UNFIT.ToLower(), PROPERTY_NAME_TITLE_UNFIT);
                map.put(DB_NAME_TITLE_BEFORE_WB.ToLower(), PROPERTY_NAME_TITLE_BEFORE_WB);
                map.put(DB_NAME_FLAG_STATISTICS_PARAMETER.ToLower(), PROPERTY_NAME_FLAG_STATISTICS_PARAMETER);
                map.put(DB_NAME_TITLE_STATISTICS_PARAMETER.ToLower(), PROPERTY_NAME_TITLE_STATISTICS_PARAMETER);
                map.put(DB_NAME_FLAG_TOTAL.ToLower(), PROPERTY_NAME_FLAG_TOTAL);
                map.put(DB_NAME_TITLE_TOTAL.ToLower(), PROPERTY_NAME_TITLE_TOTAL);
                map.put(DB_NAME_DP_SUM.ToLower(), PROPERTY_NAME_DP_SUM);
                map.put(DB_NAME_FLAG_AVR.ToLower(), PROPERTY_NAME_FLAG_AVR);
                map.put(DB_NAME_TITLE_AVR.ToLower(), PROPERTY_NAME_TITLE_AVR);
                map.put(DB_NAME_DP_AVR.ToLower(), PROPERTY_NAME_DP_AVR);
                map.put(DB_NAME_FLAG_SD.ToLower(), PROPERTY_NAME_FLAG_SD);
                map.put(DB_NAME_TITLE_SD.ToLower(), PROPERTY_NAME_TITLE_SD);
                map.put(DB_NAME_DP_SD.ToLower(), PROPERTY_NAME_DP_SD);
                map.put(DB_NAME_FLAG_MIN.ToLower(), PROPERTY_NAME_FLAG_MIN);
                map.put(DB_NAME_TITLE_MIN.ToLower(), PROPERTY_NAME_TITLE_MIN);
                map.put(DB_NAME_DP_MIN.ToLower(), PROPERTY_NAME_DP_MIN);
                map.put(DB_NAME_FLAG_MAX.ToLower(), PROPERTY_NAME_FLAG_MAX);
                map.put(DB_NAME_TITLE_MAX.ToLower(), PROPERTY_NAME_TITLE_MAX);
                map.put(DB_NAME_DP_MAX.ToLower(), PROPERTY_NAME_DP_MAX);
                map.put(DB_NAME_FLAG_MEDIAN.ToLower(), PROPERTY_NAME_FLAG_MEDIAN);
                map.put(DB_NAME_TITLE_MEDIAN.ToLower(), PROPERTY_NAME_TITLE_MEDIAN);
                map.put(DB_NAME_DP_MEDIAN.ToLower(), PROPERTY_NAME_DP_MEDIAN);
                map.put(DB_NAME_DP_WEIGHT.ToLower(), PROPERTY_NAME_DP_WEIGHT);
                map.put(DB_NAME_DP_WEIGHT_AVR.ToLower(), PROPERTY_NAME_DP_WEIGHT_AVR);
                map.put(DB_NAME_TEST_GT_FLAG.ToLower(), PROPERTY_NAME_TEST_GT_FLAG);
                map.put(DB_NAME_TEST_CROSS_FLAG.ToLower(), PROPERTY_NAME_TEST_CROSS_FLAG);
                map.put(DB_NAME_TEST_TYPE_GT.ToLower(), PROPERTY_NAME_TEST_TYPE_GT);
                map.put(DB_NAME_TEST_TYPE_CROSS.ToLower(), PROPERTY_NAME_TEST_TYPE_CROSS);
                map.put(DB_NAME_TEST_SIGNIFICANCE_LV_GT.ToLower(), PROPERTY_NAME_TEST_SIGNIFICANCE_LV_GT);
                map.put(DB_NAME_TEST_SIGNIFICANCE_LV_CROSS.ToLower(), PROPERTY_NAME_TEST_SIGNIFICANCE_LV_CROSS);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_LANGUAGE.ToLower(), DB_NAME_LANGUAGE);
                map.put(PROPERTY_NAME_NOANSWER_DENOMINATOR_FLAG.ToLower(), DB_NAME_NOANSWER_DENOMINATOR_FLAG);
                map.put(PROPERTY_NAME_VISIBLE_UNFIT_FLAG.ToLower(), DB_NAME_VISIBLE_UNFIT_FLAG);
                map.put(PROPERTY_NAME_NOANSWER_UNFIT_FLAG.ToLower(), DB_NAME_NOANSWER_UNFIT_FLAG);
                map.put(PROPERTY_NAME_WEIGHTBACK_FLAG.ToLower(), DB_NAME_WEIGHTBACK_FLAG);
                map.put(PROPERTY_NAME_CELL_JOINCELL_JOIN_FLAG.ToLower(), DB_NAME_CELL_JOINCELL_JOIN_FLAG);
                map.put(PROPERTY_NAME_CHART_DIRECTION_GT_FLAG.ToLower(), DB_NAME_CHART_DIRECTION_GT_FLAG);
                map.put(PROPERTY_NAME_CHART_DIRECTION_CROSS_FLAG.ToLower(), DB_NAME_CHART_DIRECTION_CROSS_FLAG);
                map.put(PROPERTY_NAME_NOANSWER_TARGET_FLAG.ToLower(), DB_NAME_NOANSWER_TARGET_FLAG);
                map.put(PROPERTY_NAME_NOANSWER_AXIS_FLAG.ToLower(), DB_NAME_NOANSWER_AXIS_FLAG);
                map.put(PROPERTY_NAME_UNFIT_TARGET_FLAG.ToLower(), DB_NAME_UNFIT_TARGET_FLAG);
                map.put(PROPERTY_NAME_UNFIT_AXIS_FLAG.ToLower(), DB_NAME_UNFIT_AXIS_FLAG);
                map.put(PROPERTY_NAME_TOTALNUM_FLAG.ToLower(), DB_NAME_TOTALNUM_FLAG);
                map.put(PROPERTY_NAME_RATE_DIFF_COLOR_MINUS5.ToLower(), DB_NAME_RATE_DIFF_COLOR_MINUS5);
                map.put(PROPERTY_NAME_RATE_DIFF_COLOR_MINUS10.ToLower(), DB_NAME_RATE_DIFF_COLOR_MINUS10);
                map.put(PROPERTY_NAME_RATE_DIFF_COLOR_PLUS5.ToLower(), DB_NAME_RATE_DIFF_COLOR_PLUS5);
                map.put(PROPERTY_NAME_RATE_DIFF_COLOR_PLUS10.ToLower(), DB_NAME_RATE_DIFF_COLOR_PLUS10);
                map.put(PROPERTY_NAME_GRAPH_TYPE_SA.ToLower(), DB_NAME_GRAPH_TYPE_SA);
                map.put(PROPERTY_NAME_GRAPH_TYPE_SA_MATRIX.ToLower(), DB_NAME_GRAPH_TYPE_SA_MATRIX);
                map.put(PROPERTY_NAME_GRAPH_TYPE_MA_SIMPLE.ToLower(), DB_NAME_GRAPH_TYPE_MA_SIMPLE);
                map.put(PROPERTY_NAME_GRAPH_TYPE_MA_CROSS.ToLower(), DB_NAME_GRAPH_TYPE_MA_CROSS);
                map.put(PROPERTY_NAME_GRAPH_TYPE_MA_MATRIX.ToLower(), DB_NAME_GRAPH_TYPE_MA_MATRIX);
                map.put(PROPERTY_NAME_GRAPH_TYPE_N_RATE.ToLower(), DB_NAME_GRAPH_TYPE_N_RATE);
                map.put(PROPERTY_NAME_GRAPH_TYPE_N_RANKING.ToLower(), DB_NAME_GRAPH_TYPE_N_RANKING);
                map.put(PROPERTY_NAME_SET_EXECUTE_FLAG.ToLower(), DB_NAME_SET_EXECUTE_FLAG);
                map.put(PROPERTY_NAME_TITLE_ALL.ToLower(), DB_NAME_TITLE_ALL);
                map.put(PROPERTY_NAME_TITLE_AXIS_ALL.ToLower(), DB_NAME_TITLE_AXIS_ALL);
                map.put(PROPERTY_NAME_TITLE_NOANSWER.ToLower(), DB_NAME_TITLE_NOANSWER);
                map.put(PROPERTY_NAME_TITLE_UNFIT.ToLower(), DB_NAME_TITLE_UNFIT);
                map.put(PROPERTY_NAME_TITLE_BEFORE_WB.ToLower(), DB_NAME_TITLE_BEFORE_WB);
                map.put(PROPERTY_NAME_FLAG_STATISTICS_PARAMETER.ToLower(), DB_NAME_FLAG_STATISTICS_PARAMETER);
                map.put(PROPERTY_NAME_TITLE_STATISTICS_PARAMETER.ToLower(), DB_NAME_TITLE_STATISTICS_PARAMETER);
                map.put(PROPERTY_NAME_FLAG_TOTAL.ToLower(), DB_NAME_FLAG_TOTAL);
                map.put(PROPERTY_NAME_TITLE_TOTAL.ToLower(), DB_NAME_TITLE_TOTAL);
                map.put(PROPERTY_NAME_DP_SUM.ToLower(), DB_NAME_DP_SUM);
                map.put(PROPERTY_NAME_FLAG_AVR.ToLower(), DB_NAME_FLAG_AVR);
                map.put(PROPERTY_NAME_TITLE_AVR.ToLower(), DB_NAME_TITLE_AVR);
                map.put(PROPERTY_NAME_DP_AVR.ToLower(), DB_NAME_DP_AVR);
                map.put(PROPERTY_NAME_FLAG_SD.ToLower(), DB_NAME_FLAG_SD);
                map.put(PROPERTY_NAME_TITLE_SD.ToLower(), DB_NAME_TITLE_SD);
                map.put(PROPERTY_NAME_DP_SD.ToLower(), DB_NAME_DP_SD);
                map.put(PROPERTY_NAME_FLAG_MIN.ToLower(), DB_NAME_FLAG_MIN);
                map.put(PROPERTY_NAME_TITLE_MIN.ToLower(), DB_NAME_TITLE_MIN);
                map.put(PROPERTY_NAME_DP_MIN.ToLower(), DB_NAME_DP_MIN);
                map.put(PROPERTY_NAME_FLAG_MAX.ToLower(), DB_NAME_FLAG_MAX);
                map.put(PROPERTY_NAME_TITLE_MAX.ToLower(), DB_NAME_TITLE_MAX);
                map.put(PROPERTY_NAME_DP_MAX.ToLower(), DB_NAME_DP_MAX);
                map.put(PROPERTY_NAME_FLAG_MEDIAN.ToLower(), DB_NAME_FLAG_MEDIAN);
                map.put(PROPERTY_NAME_TITLE_MEDIAN.ToLower(), DB_NAME_TITLE_MEDIAN);
                map.put(PROPERTY_NAME_DP_MEDIAN.ToLower(), DB_NAME_DP_MEDIAN);
                map.put(PROPERTY_NAME_DP_WEIGHT.ToLower(), DB_NAME_DP_WEIGHT);
                map.put(PROPERTY_NAME_DP_WEIGHT_AVR.ToLower(), DB_NAME_DP_WEIGHT_AVR);
                map.put(PROPERTY_NAME_TEST_GT_FLAG.ToLower(), DB_NAME_TEST_GT_FLAG);
                map.put(PROPERTY_NAME_TEST_CROSS_FLAG.ToLower(), DB_NAME_TEST_CROSS_FLAG);
                map.put(PROPERTY_NAME_TEST_TYPE_GT.ToLower(), DB_NAME_TEST_TYPE_GT);
                map.put(PROPERTY_NAME_TEST_TYPE_CROSS.ToLower(), DB_NAME_TEST_TYPE_CROSS);
                map.put(PROPERTY_NAME_TEST_SIGNIFICANCE_LV_GT.ToLower(), DB_NAME_TEST_SIGNIFICANCE_LV_GT);
                map.put(PROPERTY_NAME_TEST_SIGNIFICANCE_LV_CROSS.ToLower(), DB_NAME_TEST_SIGNIFICANCE_LV_CROSS);
                _propertyNameDbNameKeyToLowerMap = map;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                        Name Map
        //                                                                        ========
        #region Name Map
        public override Map<String, String> DbNamePropertyNameKeyToLowerMap { get { return _dbNamePropertyNameKeyToLowerMap; } }
        public override Map<String, String> PropertyNameDbNameKeyToLowerMap { get { return _propertyNameDbNameKeyToLowerMap; } }
        #endregion

        // ===============================================================================
        //                                                                       Type Name
        //                                                                       =========
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TDefaultEnvBase"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TDefaultEnvBaseDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TDefaultEnvBaseCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TDefaultEnvBaseBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TDefaultEnvBase NewMyEntity() { return new TDefaultEnvBase(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TDefaultEnvBaseCB NewMyConditionBean() { return new TDefaultEnvBaseCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TDefaultEnvBase>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TDefaultEnvBase>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("LANGUAGE", "Language", new EntityPropertyLanguageSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_DENOMINATOR_FLAG", "NoanswerDenominatorFlag", new EntityPropertyNoanswerDenominatorFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("VISIBLE_UNFIT_FLAG", "VisibleUnfitFlag", new EntityPropertyVisibleUnfitFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_UNFIT_FLAG", "NoanswerUnfitFlag", new EntityPropertyNoanswerUnfitFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_FLAG", "WeightbackFlag", new EntityPropertyWeightbackFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CELL_JOINCELL_JOIN_FLAG", "CellJoincellJoinFlag", new EntityPropertyCellJoincellJoinFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHART_DIRECTION_GT_FLAG", "ChartDirectionGtFlag", new EntityPropertyChartDirectionGtFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHART_DIRECTION_CROSS_FLAG", "ChartDirectionCrossFlag", new EntityPropertyChartDirectionCrossFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_TARGET_FLAG", "NoanswerTargetFlag", new EntityPropertyNoanswerTargetFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("NOANSWER_AXIS_FLAG", "NoanswerAxisFlag", new EntityPropertyNoanswerAxisFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UNFIT_TARGET_FLAG", "UnfitTargetFlag", new EntityPropertyUnfitTargetFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("UNFIT_AXIS_FLAG", "UnfitAxisFlag", new EntityPropertyUnfitAxisFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOTALNUM_FLAG", "TotalnumFlag", new EntityPropertyTotalnumFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE_DIFF_COLOR_MINUS5", "RateDiffColorMinus5", new EntityPropertyRateDiffColorMinus5Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE_DIFF_COLOR_MINUS10", "RateDiffColorMinus10", new EntityPropertyRateDiffColorMinus10Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE_DIFF_COLOR_PLUS5", "RateDiffColorPlus5", new EntityPropertyRateDiffColorPlus5Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE_DIFF_COLOR_PLUS10", "RateDiffColorPlus10", new EntityPropertyRateDiffColorPlus10Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_SA", "GraphTypeSa", new EntityPropertyGraphTypeSaSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_SA_MATRIX", "GraphTypeSaMatrix", new EntityPropertyGraphTypeSaMatrixSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_MA_SIMPLE", "GraphTypeMaSimple", new EntityPropertyGraphTypeMaSimpleSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_MA_CROSS", "GraphTypeMaCross", new EntityPropertyGraphTypeMaCrossSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_MA_MATRIX", "GraphTypeMaMatrix", new EntityPropertyGraphTypeMaMatrixSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_N_RATE", "GraphTypeNRate", new EntityPropertyGraphTypeNRateSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_TYPE_N_RANKING", "GraphTypeNRanking", new EntityPropertyGraphTypeNRankingSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SET_EXECUTE_FLAG", "SetExecuteFlag", new EntityPropertySetExecuteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_ALL", "TitleAll", new EntityPropertyTitleAllSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_AXIS_ALL", "TitleAxisAll", new EntityPropertyTitleAxisAllSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_NOANSWER", "TitleNoanswer", new EntityPropertyTitleNoanswerSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_UNFIT", "TitleUnfit", new EntityPropertyTitleUnfitSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_BEFORE_WB", "TitleBeforeWb", new EntityPropertyTitleBeforeWbSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_STATISTICS_PARAMETER", "FlagStatisticsParameter", new EntityPropertyFlagStatisticsParameterSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_STATISTICS_PARAMETER", "TitleStatisticsParameter", new EntityPropertyTitleStatisticsParameterSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_TOTAL", "FlagTotal", new EntityPropertyFlagTotalSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_TOTAL", "TitleTotal", new EntityPropertyTitleTotalSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_SUM", "DpSum", new EntityPropertyDpSumSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_AVR", "FlagAvr", new EntityPropertyFlagAvrSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_AVR", "TitleAvr", new EntityPropertyTitleAvrSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_AVR", "DpAvr", new EntityPropertyDpAvrSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_SD", "FlagSd", new EntityPropertyFlagSdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_SD", "TitleSd", new EntityPropertyTitleSdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_SD", "DpSd", new EntityPropertyDpSdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_MIN", "FlagMin", new EntityPropertyFlagMinSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_MIN", "TitleMin", new EntityPropertyTitleMinSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_MIN", "DpMin", new EntityPropertyDpMinSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_MAX", "FlagMax", new EntityPropertyFlagMaxSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_MAX", "TitleMax", new EntityPropertyTitleMaxSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_MAX", "DpMax", new EntityPropertyDpMaxSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FLAG_MEDIAN", "FlagMedian", new EntityPropertyFlagMedianSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TITLE_MEDIAN", "TitleMedian", new EntityPropertyTitleMedianSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_MEDIAN", "DpMedian", new EntityPropertyDpMedianSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_WEIGHT", "DpWeight", new EntityPropertyDpWeightSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DP_WEIGHT_AVR", "DpWeightAvr", new EntityPropertyDpWeightAvrSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_GT_FLAG", "TestGtFlag", new EntityPropertyTestGtFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_CROSS_FLAG", "TestCrossFlag", new EntityPropertyTestCrossFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_TYPE_GT", "TestTypeGt", new EntityPropertyTestTypeGtSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_TYPE_CROSS", "TestTypeCross", new EntityPropertyTestTypeCrossSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_SIGNIFICANCE_LV_GT", "TestSignificanceLvGt", new EntityPropertyTestSignificanceLvGtSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_SIGNIFICANCE_LV_CROSS", "TestSignificanceLvCross", new EntityPropertyTestSignificanceLvCrossSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TDefaultEnvBase> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TDefaultEnvBase)entity, value);
        }

        public class EntityPropertyLanguageSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.Language = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyNoanswerDenominatorFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.NoanswerDenominatorFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyVisibleUnfitFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.VisibleUnfitFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoanswerUnfitFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.NoanswerUnfitFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyWeightbackFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.WeightbackFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyCellJoincellJoinFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.CellJoincellJoinFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyChartDirectionGtFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.ChartDirectionGtFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyChartDirectionCrossFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.ChartDirectionCrossFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoanswerTargetFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.NoanswerTargetFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyNoanswerAxisFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.NoanswerAxisFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUnfitTargetFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.UnfitTargetFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyUnfitAxisFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.UnfitAxisFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTotalnumFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TotalnumFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRateDiffColorMinus5Setupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.RateDiffColorMinus5 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRateDiffColorMinus10Setupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.RateDiffColorMinus10 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRateDiffColorPlus5Setupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.RateDiffColorPlus5 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRateDiffColorPlus10Setupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.RateDiffColorPlus10 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGraphTypeSaSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeSa = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeSaMatrixSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeSaMatrix = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeMaSimpleSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeMaSimple = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeMaCrossSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeMaCross = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeMaMatrixSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeMaMatrix = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeNRateSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeNRate = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyGraphTypeNRankingSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.GraphTypeNRanking = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySetExecuteFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.SetExecuteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleAllSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleAll = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTitleAxisAllSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleAxisAll = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTitleNoanswerSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleNoanswer = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTitleUnfitSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleUnfit = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyTitleBeforeWbSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleBeforeWb = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFlagStatisticsParameterSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagStatisticsParameter = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleStatisticsParameterSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleStatisticsParameter = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFlagTotalSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagTotal = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleTotalSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleTotal = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpSumSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpSum = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFlagAvrSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagAvr = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleAvrSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleAvr = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpAvrSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpAvr = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFlagSdSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagSd = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleSdSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleSd = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpSdSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpSd = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFlagMinSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagMin = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleMinSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleMin = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpMinSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpMin = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFlagMaxSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagMax = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleMaxSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleMax = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpMaxSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpMax = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyFlagMedianSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.FlagMedian = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTitleMedianSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TitleMedian = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyDpMedianSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpMedian = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpWeightSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpWeight = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyDpWeightAvrSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.DpWeightAvr = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestGtFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TestGtFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestCrossFlagSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TestCrossFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestTypeGtSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TestTypeGt = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestTypeCrossSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TestTypeCross = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestSignificanceLvGtSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TestSignificanceLvGt = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestSignificanceLvCrossSetupper : EntityPropertySetupper<TDefaultEnvBase> {
            public void Setup(TDefaultEnvBase entity, Object value) { entity.TestSignificanceLvCross = (value != null) ? (int?)value : null; }
        }
    }
}
