
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

    public class TScenarioTotalizationDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TScenarioTotalization);

        private static readonly TScenarioTotalizationDbm _instance = new TScenarioTotalizationDbm();
        private TScenarioTotalizationDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TScenarioTotalizationDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_SCENARIO_TOTALIZATION"; } }
        public override String TablePropertyName { get { return "TScenarioTotalization"; } }
        public override String TableSqlName { get { return "T_SCENARIO_TOTALIZATION"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnScenarioTotalizationId;
        protected ColumnInfo _columnQcwebid;
        protected ColumnInfo _columnScenarioType;
        protected ColumnInfo _columnScenarioName;
        protected ColumnInfo _columnConditionDiv;
        protected ColumnInfo _columnFilterFlag;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnWeightbackFlag;
        protected ColumnInfo _columnWeightbackCode;
        protected ColumnInfo _columnTotalnumFlag;
        protected ColumnInfo _columnGraphOutputFlag;
        protected ColumnInfo _columnPieChartChoiceFlag;
        protected ColumnInfo _columnMinimumRate;
        protected ColumnInfo _columnAxisNoanswerOnoff;
        protected ColumnInfo _columnTargetNoanswerOnoff;
        protected ColumnInfo _columnPolylineOnoff;
        protected ColumnInfo _columnMarkingN;
        protected ColumnInfo _columnRankingFlag;
        protected ColumnInfo _columnRateFlag;
        protected ColumnInfo _columnRate1Flag;
        protected ColumnInfo _columnRate1Sign;
        protected ColumnInfo _columnRate1Range;
        protected ColumnInfo _columnRate1Backcolor1;
        protected ColumnInfo _columnRate1Backcolor2;
        protected ColumnInfo _columnRate2Flag;
        protected ColumnInfo _columnRate2Sign;
        protected ColumnInfo _columnRate2Range;
        protected ColumnInfo _columnRate2Backcolor1;
        protected ColumnInfo _columnRate2Backcolor2;
        protected ColumnInfo _columnLastUpdateUser;
        protected ColumnInfo _columnLastUpdateDatetime;
        protected ColumnInfo _columnTestFlag;
        protected ColumnInfo _columnTestType;
        protected ColumnInfo _columnTestSignificanceLv;

        public ColumnInfo ColumnScenarioTotalizationId { get { return _columnScenarioTotalizationId; } }
        public ColumnInfo ColumnQcwebid { get { return _columnQcwebid; } }
        public ColumnInfo ColumnScenarioType { get { return _columnScenarioType; } }
        public ColumnInfo ColumnScenarioName { get { return _columnScenarioName; } }
        public ColumnInfo ColumnConditionDiv { get { return _columnConditionDiv; } }
        public ColumnInfo ColumnFilterFlag { get { return _columnFilterFlag; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnWeightbackFlag { get { return _columnWeightbackFlag; } }
        public ColumnInfo ColumnWeightbackCode { get { return _columnWeightbackCode; } }
        public ColumnInfo ColumnTotalnumFlag { get { return _columnTotalnumFlag; } }
        public ColumnInfo ColumnGraphOutputFlag { get { return _columnGraphOutputFlag; } }
        public ColumnInfo ColumnPieChartChoiceFlag { get { return _columnPieChartChoiceFlag; } }
        public ColumnInfo ColumnMinimumRate { get { return _columnMinimumRate; } }
        public ColumnInfo ColumnAxisNoanswerOnoff { get { return _columnAxisNoanswerOnoff; } }
        public ColumnInfo ColumnTargetNoanswerOnoff { get { return _columnTargetNoanswerOnoff; } }
        public ColumnInfo ColumnPolylineOnoff { get { return _columnPolylineOnoff; } }
        public ColumnInfo ColumnMarkingN { get { return _columnMarkingN; } }
        public ColumnInfo ColumnRankingFlag { get { return _columnRankingFlag; } }
        public ColumnInfo ColumnRateFlag { get { return _columnRateFlag; } }
        public ColumnInfo ColumnRate1Flag { get { return _columnRate1Flag; } }
        public ColumnInfo ColumnRate1Sign { get { return _columnRate1Sign; } }
        public ColumnInfo ColumnRate1Range { get { return _columnRate1Range; } }
        public ColumnInfo ColumnRate1Backcolor1 { get { return _columnRate1Backcolor1; } }
        public ColumnInfo ColumnRate1Backcolor2 { get { return _columnRate1Backcolor2; } }
        public ColumnInfo ColumnRate2Flag { get { return _columnRate2Flag; } }
        public ColumnInfo ColumnRate2Sign { get { return _columnRate2Sign; } }
        public ColumnInfo ColumnRate2Range { get { return _columnRate2Range; } }
        public ColumnInfo ColumnRate2Backcolor1 { get { return _columnRate2Backcolor1; } }
        public ColumnInfo ColumnRate2Backcolor2 { get { return _columnRate2Backcolor2; } }
        public ColumnInfo ColumnLastUpdateUser { get { return _columnLastUpdateUser; } }
        public ColumnInfo ColumnLastUpdateDatetime { get { return _columnLastUpdateDatetime; } }
        public ColumnInfo ColumnTestFlag { get { return _columnTestFlag; } }
        public ColumnInfo ColumnTestType { get { return _columnTestType; } }
        public ColumnInfo ColumnTestSignificanceLv { get { return _columnTestSignificanceLv; } }

        protected void InitializeColumnInfo() {
            _columnScenarioTotalizationId = cci("SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", null, null, true, "ScenarioTotalizationId", typeof(decimal?), true, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TGtScenarioItem,TCrossScenarioTarget,TFaScenarioHeader,TScenarioQuerylist,TCategoryOutputEdit,TGtMatrixInfo", "TCategoryOutputEditList,TCrossScenarioTargetList,TFaScenarioHeaderList,TGtMatrixInfoList,TGtScenarioItemList,TScenarioQuerylistList,TItemInfoList");
            _columnQcwebid = cci("QCWEBID", "QCWEBID", null, null, true, "Qcwebid", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, "TQcwebSurveyInfo,TDefaultEnv", null);
            _columnScenarioType = cci("SCENARIO_TYPE", "SCENARIO_TYPE", null, null, true, "ScenarioType", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnScenarioName = cci("SCENARIO_NAME", "SCENARIO_NAME", null, null, true, "ScenarioName", typeof(String), false, "NVARCHAR2", 50, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnConditionDiv = cci("CONDITION_DIV", "CONDITION_DIV", null, null, true, "ConditionDiv", typeof(String), false, "VARCHAR2", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFilterFlag = cci("FILTER_FLAG", "FILTER_FLAG", null, null, true, "FilterFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(int?), false, "NUMBER", 5, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackFlag = cci("WEIGHTBACK_FLAG", "WEIGHTBACK_FLAG", null, null, true, "WeightbackFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnWeightbackCode = cci("WEIGHTBACK_CODE", "WEIGHTBACK_CODE", null, null, false, "WeightbackCode", typeof(decimal?), false, "NUMBER", 27, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTotalnumFlag = cci("TOTALNUM_FLAG", "TOTALNUM_FLAG", null, null, true, "TotalnumFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnGraphOutputFlag = cci("GRAPH_OUTPUT_FLAG", "GRAPH_OUTPUT_FLAG", null, null, true, "GraphOutputFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPieChartChoiceFlag = cci("PIE_CHART_CHOICE_FLAG", "PIE_CHART_CHOICE_FLAG", null, null, false, "PieChartChoiceFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMinimumRate = cci("MINIMUM_RATE", "MINIMUM_RATE", null, null, false, "MinimumRate", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAxisNoanswerOnoff = cci("AXIS_NOANSWER_ONOFF", "AXIS_NOANSWER_ONOFF", null, null, false, "AxisNoanswerOnoff", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTargetNoanswerOnoff = cci("TARGET_NOANSWER_ONOFF", "TARGET_NOANSWER_ONOFF", null, null, false, "TargetNoanswerOnoff", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPolylineOnoff = cci("POLYLINE_ONOFF", "POLYLINE_ONOFF", null, null, false, "PolylineOnoff", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarkingN = cci("MARKING_N", "MARKING_N", null, null, false, "MarkingN", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRankingFlag = cci("RANKING_FLAG", "RANKING_FLAG", null, null, false, "RankingFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRateFlag = cci("RATE_FLAG", "RATE_FLAG", null, null, false, "RateFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate1Flag = cci("RATE1_FLAG", "RATE1_FLAG", null, null, false, "Rate1Flag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate1Sign = cci("RATE1_SIGN", "RATE1_SIGN", null, null, false, "Rate1Sign", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate1Range = cci("RATE1_RANGE", "RATE1_RANGE", null, null, false, "Rate1Range", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate1Backcolor1 = cci("RATE1_BACKCOLOR1", "RATE1_BACKCOLOR1", null, null, false, "Rate1Backcolor1", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate1Backcolor2 = cci("RATE1_BACKCOLOR2", "RATE1_BACKCOLOR2", null, null, false, "Rate1Backcolor2", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate2Flag = cci("RATE2_FLAG", "RATE2_FLAG", null, null, false, "Rate2Flag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate2Sign = cci("RATE2_SIGN", "RATE2_SIGN", null, null, false, "Rate2Sign", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate2Range = cci("RATE2_RANGE", "RATE2_RANGE", null, null, false, "Rate2Range", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate2Backcolor1 = cci("RATE2_BACKCOLOR1", "RATE2_BACKCOLOR1", null, null, false, "Rate2Backcolor1", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnRate2Backcolor2 = cci("RATE2_BACKCOLOR2", "RATE2_BACKCOLOR2", null, null, false, "Rate2Backcolor2", typeof(int?), false, "NUMBER", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateUser = cci("LAST_UPDATE_USER", "LAST_UPDATE_USER", null, null, false, "LastUpdateUser", typeof(String), false, "VARCHAR2", 1000, 0, true, OptimisticLockType.NONE, null, null, null);
            _columnLastUpdateDatetime = cci("LAST_UPDATE_DATETIME", "LAST_UPDATE_DATETIME", null, null, false, "LastUpdateDatetime", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, true, OptimisticLockType.NONE, null, null, null);
            _columnTestFlag = cci("TEST_FLAG", "TEST_FLAG", null, null, false, "TestFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestType = cci("TEST_TYPE", "TEST_TYPE", null, null, false, "TestType", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnTestSignificanceLv = cci("TEST_SIGNIFICANCE_LV", "TEST_SIGNIFICANCE_LV", null, null, false, "TestSignificanceLv", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnScenarioTotalizationId);
            _columnInfoList.add(ColumnQcwebid);
            _columnInfoList.add(ColumnScenarioType);
            _columnInfoList.add(ColumnScenarioName);
            _columnInfoList.add(ColumnConditionDiv);
            _columnInfoList.add(ColumnFilterFlag);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnWeightbackFlag);
            _columnInfoList.add(ColumnWeightbackCode);
            _columnInfoList.add(ColumnTotalnumFlag);
            _columnInfoList.add(ColumnGraphOutputFlag);
            _columnInfoList.add(ColumnPieChartChoiceFlag);
            _columnInfoList.add(ColumnMinimumRate);
            _columnInfoList.add(ColumnAxisNoanswerOnoff);
            _columnInfoList.add(ColumnTargetNoanswerOnoff);
            _columnInfoList.add(ColumnPolylineOnoff);
            _columnInfoList.add(ColumnMarkingN);
            _columnInfoList.add(ColumnRankingFlag);
            _columnInfoList.add(ColumnRateFlag);
            _columnInfoList.add(ColumnRate1Flag);
            _columnInfoList.add(ColumnRate1Sign);
            _columnInfoList.add(ColumnRate1Range);
            _columnInfoList.add(ColumnRate1Backcolor1);
            _columnInfoList.add(ColumnRate1Backcolor2);
            _columnInfoList.add(ColumnRate2Flag);
            _columnInfoList.add(ColumnRate2Sign);
            _columnInfoList.add(ColumnRate2Range);
            _columnInfoList.add(ColumnRate2Backcolor1);
            _columnInfoList.add(ColumnRate2Backcolor2);
            _columnInfoList.add(ColumnLastUpdateUser);
            _columnInfoList.add(ColumnLastUpdateDatetime);
            _columnInfoList.add(ColumnTestFlag);
            _columnInfoList.add(ColumnTestType);
            _columnInfoList.add(ColumnTestSignificanceLv);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnScenarioTotalizationId);
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
        public ForeignInfo ForeignTQcwebSurveyInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TQcwebSurveyInfoDbm.GetInstance().ColumnQcwebid);
            return cfi("TQcwebSurveyInfo", this, TQcwebSurveyInfoDbm.GetInstance(), map, 0, false, false);
        }}
        public ForeignInfo ForeignTGtScenarioItem { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TGtScenarioItemDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TGtScenarioItem", this, TGtScenarioItemDbm.GetInstance(), map, 1, true, false);
        }}
        public ForeignInfo ForeignTCrossScenarioTarget { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TCrossScenarioTargetDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TCrossScenarioTarget", this, TCrossScenarioTargetDbm.GetInstance(), map, 2, true, false);
        }}
        public ForeignInfo ForeignTFaScenarioHeader { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TFaScenarioHeaderDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TFaScenarioHeader", this, TFaScenarioHeaderDbm.GetInstance(), map, 3, true, false);
        }}
        public ForeignInfo ForeignTScenarioQuerylist { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TScenarioQuerylistDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TScenarioQuerylist", this, TScenarioQuerylistDbm.GetInstance(), map, 4, true, false);
        }}
        public ForeignInfo ForeignTCategoryOutputEdit { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TCategoryOutputEditDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TCategoryOutputEdit", this, TCategoryOutputEditDbm.GetInstance(), map, 5, true, false);
        }}
        public ForeignInfo ForeignTGtMatrixInfo { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TGtMatrixInfoDbm.GetInstance().ColumnScenarioTotalizationId);
            return cfi("TGtMatrixInfo", this, TGtMatrixInfoDbm.GetInstance(), map, 6, true, false);
        }}
        public ForeignInfo ForeignTDefaultEnv { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnQcwebid, TDefaultEnvDbm.GetInstance().ColumnQcwebid);
            return cfi("TDefaultEnv", this, TDefaultEnvDbm.GetInstance(), map, 7, false, false);
        }}


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------
        public ReferrerInfo ReferrerTCategoryOutputEditList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TCategoryOutputEditDbm.GetInstance().ColumnScenarioTotalizationId);
            return cri("TCategoryOutputEditList", this, TCategoryOutputEditDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTCrossScenarioTargetList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TCrossScenarioTargetDbm.GetInstance().ColumnScenarioTotalizationId);
            return cri("TCrossScenarioTargetList", this, TCrossScenarioTargetDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTFaScenarioHeaderList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TFaScenarioHeaderDbm.GetInstance().ColumnScenarioTotalizationId);
            return cri("TFaScenarioHeaderList", this, TFaScenarioHeaderDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTGtMatrixInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TGtMatrixInfoDbm.GetInstance().ColumnScenarioTotalizationId);
            return cri("TGtMatrixInfoList", this, TGtMatrixInfoDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTGtScenarioItemList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TGtScenarioItemDbm.GetInstance().ColumnScenarioTotalizationId);
            return cri("TGtScenarioItemList", this, TGtScenarioItemDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTScenarioQuerylistList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TScenarioQuerylistDbm.GetInstance().ColumnScenarioTotalizationId);
            return cri("TScenarioQuerylistList", this, TScenarioQuerylistDbm.GetInstance(), map, false);
        }}
        public ReferrerInfo ReferrerTItemInfoList { get {
            Map<ColumnInfo, ColumnInfo> map = new LinkedHashMap<ColumnInfo, ColumnInfo>();
            map.put(ColumnScenarioTotalizationId, TItemInfoDbm.GetInstance().ColumnCategoryEditId);
            return cri("TItemInfoList", this, TItemInfoDbm.GetInstance(), map, false);
        }}

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasSequence { get { return true; } }
        public override String SequenceName { get { return "T_Scenario_Totalization_SEQ_01"; } }
        public override String SequenceNextValSql { get { return "select T_Scenario_Totalization_SEQ_01.nextval from dual"; } }
        public override int? SequenceIncrementSize { get { return 1; } }
        public override int? SequenceCacheSize { get { return null; } }
        public override bool HasCommonColumn { get { return true; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_SCENARIO_TOTALIZATION";
        public static readonly String TABLE_PROPERTY_NAME = "TScenarioTotalization";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SCENARIO_TOTALIZATION_ID = "SCENARIO_TOTALIZATION_ID";
        public static readonly String DB_NAME_QCWEBID = "QCWEBID";
        public static readonly String DB_NAME_SCENARIO_TYPE = "SCENARIO_TYPE";
        public static readonly String DB_NAME_SCENARIO_NAME = "SCENARIO_NAME";
        public static readonly String DB_NAME_CONDITION_DIV = "CONDITION_DIV";
        public static readonly String DB_NAME_FILTER_FLAG = "FILTER_FLAG";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_WEIGHTBACK_FLAG = "WEIGHTBACK_FLAG";
        public static readonly String DB_NAME_WEIGHTBACK_CODE = "WEIGHTBACK_CODE";
        public static readonly String DB_NAME_TOTALNUM_FLAG = "TOTALNUM_FLAG";
        public static readonly String DB_NAME_GRAPH_OUTPUT_FLAG = "GRAPH_OUTPUT_FLAG";
        public static readonly String DB_NAME_PIE_CHART_CHOICE_FLAG = "PIE_CHART_CHOICE_FLAG";
        public static readonly String DB_NAME_MINIMUM_RATE = "MINIMUM_RATE";
        public static readonly String DB_NAME_AXIS_NOANSWER_ONOFF = "AXIS_NOANSWER_ONOFF";
        public static readonly String DB_NAME_TARGET_NOANSWER_ONOFF = "TARGET_NOANSWER_ONOFF";
        public static readonly String DB_NAME_POLYLINE_ONOFF = "POLYLINE_ONOFF";
        public static readonly String DB_NAME_MARKING_N = "MARKING_N";
        public static readonly String DB_NAME_RANKING_FLAG = "RANKING_FLAG";
        public static readonly String DB_NAME_RATE_FLAG = "RATE_FLAG";
        public static readonly String DB_NAME_RATE1_FLAG = "RATE1_FLAG";
        public static readonly String DB_NAME_RATE1_SIGN = "RATE1_SIGN";
        public static readonly String DB_NAME_RATE1_RANGE = "RATE1_RANGE";
        public static readonly String DB_NAME_RATE1_BACKCOLOR1 = "RATE1_BACKCOLOR1";
        public static readonly String DB_NAME_RATE1_BACKCOLOR2 = "RATE1_BACKCOLOR2";
        public static readonly String DB_NAME_RATE2_FLAG = "RATE2_FLAG";
        public static readonly String DB_NAME_RATE2_SIGN = "RATE2_SIGN";
        public static readonly String DB_NAME_RATE2_RANGE = "RATE2_RANGE";
        public static readonly String DB_NAME_RATE2_BACKCOLOR1 = "RATE2_BACKCOLOR1";
        public static readonly String DB_NAME_RATE2_BACKCOLOR2 = "RATE2_BACKCOLOR2";
        public static readonly String DB_NAME_LAST_UPDATE_USER = "LAST_UPDATE_USER";
        public static readonly String DB_NAME_LAST_UPDATE_DATETIME = "LAST_UPDATE_DATETIME";
        public static readonly String DB_NAME_TEST_FLAG = "TEST_FLAG";
        public static readonly String DB_NAME_TEST_TYPE = "TEST_TYPE";
        public static readonly String DB_NAME_TEST_SIGNIFICANCE_LV = "TEST_SIGNIFICANCE_LV";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SCENARIO_TOTALIZATION_ID = "ScenarioTotalizationId";
        public static readonly String PROPERTY_NAME_QCWEBID = "Qcwebid";
        public static readonly String PROPERTY_NAME_SCENARIO_TYPE = "ScenarioType";
        public static readonly String PROPERTY_NAME_SCENARIO_NAME = "ScenarioName";
        public static readonly String PROPERTY_NAME_CONDITION_DIV = "ConditionDiv";
        public static readonly String PROPERTY_NAME_FILTER_FLAG = "FilterFlag";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_FLAG = "WeightbackFlag";
        public static readonly String PROPERTY_NAME_WEIGHTBACK_CODE = "WeightbackCode";
        public static readonly String PROPERTY_NAME_TOTALNUM_FLAG = "TotalnumFlag";
        public static readonly String PROPERTY_NAME_GRAPH_OUTPUT_FLAG = "GraphOutputFlag";
        public static readonly String PROPERTY_NAME_PIE_CHART_CHOICE_FLAG = "PieChartChoiceFlag";
        public static readonly String PROPERTY_NAME_MINIMUM_RATE = "MinimumRate";
        public static readonly String PROPERTY_NAME_AXIS_NOANSWER_ONOFF = "AxisNoanswerOnoff";
        public static readonly String PROPERTY_NAME_TARGET_NOANSWER_ONOFF = "TargetNoanswerOnoff";
        public static readonly String PROPERTY_NAME_POLYLINE_ONOFF = "PolylineOnoff";
        public static readonly String PROPERTY_NAME_MARKING_N = "MarkingN";
        public static readonly String PROPERTY_NAME_RANKING_FLAG = "RankingFlag";
        public static readonly String PROPERTY_NAME_RATE_FLAG = "RateFlag";
        public static readonly String PROPERTY_NAME_RATE1_FLAG = "Rate1Flag";
        public static readonly String PROPERTY_NAME_RATE1_SIGN = "Rate1Sign";
        public static readonly String PROPERTY_NAME_RATE1_RANGE = "Rate1Range";
        public static readonly String PROPERTY_NAME_RATE1_BACKCOLOR1 = "Rate1Backcolor1";
        public static readonly String PROPERTY_NAME_RATE1_BACKCOLOR2 = "Rate1Backcolor2";
        public static readonly String PROPERTY_NAME_RATE2_FLAG = "Rate2Flag";
        public static readonly String PROPERTY_NAME_RATE2_SIGN = "Rate2Sign";
        public static readonly String PROPERTY_NAME_RATE2_RANGE = "Rate2Range";
        public static readonly String PROPERTY_NAME_RATE2_BACKCOLOR1 = "Rate2Backcolor1";
        public static readonly String PROPERTY_NAME_RATE2_BACKCOLOR2 = "Rate2Backcolor2";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_USER = "LastUpdateUser";
        public static readonly String PROPERTY_NAME_LAST_UPDATE_DATETIME = "LastUpdateDatetime";
        public static readonly String PROPERTY_NAME_TEST_FLAG = "TestFlag";
        public static readonly String PROPERTY_NAME_TEST_TYPE = "TestType";
        public static readonly String PROPERTY_NAME_TEST_SIGNIFICANCE_LV = "TestSignificanceLv";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        public static readonly String FOREIGN_PROPERTY_NAME_TQcwebSurveyInfo = "TQcwebSurveyInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TGtScenarioItem = "TGtScenarioItem";
        public static readonly String FOREIGN_PROPERTY_NAME_TCrossScenarioTarget = "TCrossScenarioTarget";
        public static readonly String FOREIGN_PROPERTY_NAME_TFaScenarioHeader = "TFaScenarioHeader";
        public static readonly String FOREIGN_PROPERTY_NAME_TScenarioQuerylist = "TScenarioQuerylist";
        public static readonly String FOREIGN_PROPERTY_NAME_TCategoryOutputEdit = "TCategoryOutputEdit";
        public static readonly String FOREIGN_PROPERTY_NAME_TGtMatrixInfo = "TGtMatrixInfo";
        public static readonly String FOREIGN_PROPERTY_NAME_TDefaultEnv = "TDefaultEnv";
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------
        public static readonly String REFERRER_PROPERTY_NAME_TCategoryOutputEditList = "TCategoryOutputEditList";
        public static readonly String REFERRER_PROPERTY_NAME_TCrossScenarioTargetList = "TCrossScenarioTargetList";
        public static readonly String REFERRER_PROPERTY_NAME_TFaScenarioHeaderList = "TFaScenarioHeaderList";
        public static readonly String REFERRER_PROPERTY_NAME_TGtMatrixInfoList = "TGtMatrixInfoList";
        public static readonly String REFERRER_PROPERTY_NAME_TGtScenarioItemList = "TGtScenarioItemList";
        public static readonly String REFERRER_PROPERTY_NAME_TScenarioQuerylistList = "TScenarioQuerylistList";
        public static readonly String REFERRER_PROPERTY_NAME_TItemInfoList = "TItemInfoList";

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TScenarioTotalizationDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), PROPERTY_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(DB_NAME_QCWEBID.ToLower(), PROPERTY_NAME_QCWEBID);
                map.put(DB_NAME_SCENARIO_TYPE.ToLower(), PROPERTY_NAME_SCENARIO_TYPE);
                map.put(DB_NAME_SCENARIO_NAME.ToLower(), PROPERTY_NAME_SCENARIO_NAME);
                map.put(DB_NAME_CONDITION_DIV.ToLower(), PROPERTY_NAME_CONDITION_DIV);
                map.put(DB_NAME_FILTER_FLAG.ToLower(), PROPERTY_NAME_FILTER_FLAG);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_WEIGHTBACK_FLAG.ToLower(), PROPERTY_NAME_WEIGHTBACK_FLAG);
                map.put(DB_NAME_WEIGHTBACK_CODE.ToLower(), PROPERTY_NAME_WEIGHTBACK_CODE);
                map.put(DB_NAME_TOTALNUM_FLAG.ToLower(), PROPERTY_NAME_TOTALNUM_FLAG);
                map.put(DB_NAME_GRAPH_OUTPUT_FLAG.ToLower(), PROPERTY_NAME_GRAPH_OUTPUT_FLAG);
                map.put(DB_NAME_PIE_CHART_CHOICE_FLAG.ToLower(), PROPERTY_NAME_PIE_CHART_CHOICE_FLAG);
                map.put(DB_NAME_MINIMUM_RATE.ToLower(), PROPERTY_NAME_MINIMUM_RATE);
                map.put(DB_NAME_AXIS_NOANSWER_ONOFF.ToLower(), PROPERTY_NAME_AXIS_NOANSWER_ONOFF);
                map.put(DB_NAME_TARGET_NOANSWER_ONOFF.ToLower(), PROPERTY_NAME_TARGET_NOANSWER_ONOFF);
                map.put(DB_NAME_POLYLINE_ONOFF.ToLower(), PROPERTY_NAME_POLYLINE_ONOFF);
                map.put(DB_NAME_MARKING_N.ToLower(), PROPERTY_NAME_MARKING_N);
                map.put(DB_NAME_RANKING_FLAG.ToLower(), PROPERTY_NAME_RANKING_FLAG);
                map.put(DB_NAME_RATE_FLAG.ToLower(), PROPERTY_NAME_RATE_FLAG);
                map.put(DB_NAME_RATE1_FLAG.ToLower(), PROPERTY_NAME_RATE1_FLAG);
                map.put(DB_NAME_RATE1_SIGN.ToLower(), PROPERTY_NAME_RATE1_SIGN);
                map.put(DB_NAME_RATE1_RANGE.ToLower(), PROPERTY_NAME_RATE1_RANGE);
                map.put(DB_NAME_RATE1_BACKCOLOR1.ToLower(), PROPERTY_NAME_RATE1_BACKCOLOR1);
                map.put(DB_NAME_RATE1_BACKCOLOR2.ToLower(), PROPERTY_NAME_RATE1_BACKCOLOR2);
                map.put(DB_NAME_RATE2_FLAG.ToLower(), PROPERTY_NAME_RATE2_FLAG);
                map.put(DB_NAME_RATE2_SIGN.ToLower(), PROPERTY_NAME_RATE2_SIGN);
                map.put(DB_NAME_RATE2_RANGE.ToLower(), PROPERTY_NAME_RATE2_RANGE);
                map.put(DB_NAME_RATE2_BACKCOLOR1.ToLower(), PROPERTY_NAME_RATE2_BACKCOLOR1);
                map.put(DB_NAME_RATE2_BACKCOLOR2.ToLower(), PROPERTY_NAME_RATE2_BACKCOLOR2);
                map.put(DB_NAME_LAST_UPDATE_USER.ToLower(), PROPERTY_NAME_LAST_UPDATE_USER);
                map.put(DB_NAME_LAST_UPDATE_DATETIME.ToLower(), PROPERTY_NAME_LAST_UPDATE_DATETIME);
                map.put(DB_NAME_TEST_FLAG.ToLower(), PROPERTY_NAME_TEST_FLAG);
                map.put(DB_NAME_TEST_TYPE.ToLower(), PROPERTY_NAME_TEST_TYPE);
                map.put(DB_NAME_TEST_SIGNIFICANCE_LV.ToLower(), PROPERTY_NAME_TEST_SIGNIFICANCE_LV);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SCENARIO_TOTALIZATION_ID.ToLower(), DB_NAME_SCENARIO_TOTALIZATION_ID);
                map.put(PROPERTY_NAME_QCWEBID.ToLower(), DB_NAME_QCWEBID);
                map.put(PROPERTY_NAME_SCENARIO_TYPE.ToLower(), DB_NAME_SCENARIO_TYPE);
                map.put(PROPERTY_NAME_SCENARIO_NAME.ToLower(), DB_NAME_SCENARIO_NAME);
                map.put(PROPERTY_NAME_CONDITION_DIV.ToLower(), DB_NAME_CONDITION_DIV);
                map.put(PROPERTY_NAME_FILTER_FLAG.ToLower(), DB_NAME_FILTER_FLAG);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_WEIGHTBACK_FLAG.ToLower(), DB_NAME_WEIGHTBACK_FLAG);
                map.put(PROPERTY_NAME_WEIGHTBACK_CODE.ToLower(), DB_NAME_WEIGHTBACK_CODE);
                map.put(PROPERTY_NAME_TOTALNUM_FLAG.ToLower(), DB_NAME_TOTALNUM_FLAG);
                map.put(PROPERTY_NAME_GRAPH_OUTPUT_FLAG.ToLower(), DB_NAME_GRAPH_OUTPUT_FLAG);
                map.put(PROPERTY_NAME_PIE_CHART_CHOICE_FLAG.ToLower(), DB_NAME_PIE_CHART_CHOICE_FLAG);
                map.put(PROPERTY_NAME_MINIMUM_RATE.ToLower(), DB_NAME_MINIMUM_RATE);
                map.put(PROPERTY_NAME_AXIS_NOANSWER_ONOFF.ToLower(), DB_NAME_AXIS_NOANSWER_ONOFF);
                map.put(PROPERTY_NAME_TARGET_NOANSWER_ONOFF.ToLower(), DB_NAME_TARGET_NOANSWER_ONOFF);
                map.put(PROPERTY_NAME_POLYLINE_ONOFF.ToLower(), DB_NAME_POLYLINE_ONOFF);
                map.put(PROPERTY_NAME_MARKING_N.ToLower(), DB_NAME_MARKING_N);
                map.put(PROPERTY_NAME_RANKING_FLAG.ToLower(), DB_NAME_RANKING_FLAG);
                map.put(PROPERTY_NAME_RATE_FLAG.ToLower(), DB_NAME_RATE_FLAG);
                map.put(PROPERTY_NAME_RATE1_FLAG.ToLower(), DB_NAME_RATE1_FLAG);
                map.put(PROPERTY_NAME_RATE1_SIGN.ToLower(), DB_NAME_RATE1_SIGN);
                map.put(PROPERTY_NAME_RATE1_RANGE.ToLower(), DB_NAME_RATE1_RANGE);
                map.put(PROPERTY_NAME_RATE1_BACKCOLOR1.ToLower(), DB_NAME_RATE1_BACKCOLOR1);
                map.put(PROPERTY_NAME_RATE1_BACKCOLOR2.ToLower(), DB_NAME_RATE1_BACKCOLOR2);
                map.put(PROPERTY_NAME_RATE2_FLAG.ToLower(), DB_NAME_RATE2_FLAG);
                map.put(PROPERTY_NAME_RATE2_SIGN.ToLower(), DB_NAME_RATE2_SIGN);
                map.put(PROPERTY_NAME_RATE2_RANGE.ToLower(), DB_NAME_RATE2_RANGE);
                map.put(PROPERTY_NAME_RATE2_BACKCOLOR1.ToLower(), DB_NAME_RATE2_BACKCOLOR1);
                map.put(PROPERTY_NAME_RATE2_BACKCOLOR2.ToLower(), DB_NAME_RATE2_BACKCOLOR2);
                map.put(PROPERTY_NAME_LAST_UPDATE_USER.ToLower(), DB_NAME_LAST_UPDATE_USER);
                map.put(PROPERTY_NAME_LAST_UPDATE_DATETIME.ToLower(), DB_NAME_LAST_UPDATE_DATETIME);
                map.put(PROPERTY_NAME_TEST_FLAG.ToLower(), DB_NAME_TEST_FLAG);
                map.put(PROPERTY_NAME_TEST_TYPE.ToLower(), DB_NAME_TEST_TYPE);
                map.put(PROPERTY_NAME_TEST_SIGNIFICANCE_LV.ToLower(), DB_NAME_TEST_SIGNIFICANCE_LV);
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
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TScenarioTotalization"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TScenarioTotalizationDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TScenarioTotalizationCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TScenarioTotalizationBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TScenarioTotalization NewMyEntity() { return new TScenarioTotalization(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TScenarioTotalizationCB NewMyConditionBean() { return new TScenarioTotalizationCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TScenarioTotalization>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TScenarioTotalization>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SCENARIO_TOTALIZATION_ID", "ScenarioTotalizationId", new EntityPropertyScenarioTotalizationIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("QCWEBID", "Qcwebid", new EntityPropertyQcwebidSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_TYPE", "ScenarioType", new EntityPropertyScenarioTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SCENARIO_NAME", "ScenarioName", new EntityPropertyScenarioNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CONDITION_DIV", "ConditionDiv", new EntityPropertyConditionDivSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FILTER_FLAG", "FilterFlag", new EntityPropertyFilterFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_FLAG", "WeightbackFlag", new EntityPropertyWeightbackFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("WEIGHTBACK_CODE", "WeightbackCode", new EntityPropertyWeightbackCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TOTALNUM_FLAG", "TotalnumFlag", new EntityPropertyTotalnumFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("GRAPH_OUTPUT_FLAG", "GraphOutputFlag", new EntityPropertyGraphOutputFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PIE_CHART_CHOICE_FLAG", "PieChartChoiceFlag", new EntityPropertyPieChartChoiceFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MINIMUM_RATE", "MinimumRate", new EntityPropertyMinimumRateSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AXIS_NOANSWER_ONOFF", "AxisNoanswerOnoff", new EntityPropertyAxisNoanswerOnoffSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TARGET_NOANSWER_ONOFF", "TargetNoanswerOnoff", new EntityPropertyTargetNoanswerOnoffSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("POLYLINE_ONOFF", "PolylineOnoff", new EntityPropertyPolylineOnoffSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARKING_N", "MarkingN", new EntityPropertyMarkingNSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RANKING_FLAG", "RankingFlag", new EntityPropertyRankingFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE_FLAG", "RateFlag", new EntityPropertyRateFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE1_FLAG", "Rate1Flag", new EntityPropertyRate1FlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE1_SIGN", "Rate1Sign", new EntityPropertyRate1SignSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE1_RANGE", "Rate1Range", new EntityPropertyRate1RangeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE1_BACKCOLOR1", "Rate1Backcolor1", new EntityPropertyRate1Backcolor1Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE1_BACKCOLOR2", "Rate1Backcolor2", new EntityPropertyRate1Backcolor2Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE2_FLAG", "Rate2Flag", new EntityPropertyRate2FlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE2_SIGN", "Rate2Sign", new EntityPropertyRate2SignSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE2_RANGE", "Rate2Range", new EntityPropertyRate2RangeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE2_BACKCOLOR1", "Rate2Backcolor1", new EntityPropertyRate2Backcolor1Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("RATE2_BACKCOLOR2", "Rate2Backcolor2", new EntityPropertyRate2Backcolor2Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_USER", "LastUpdateUser", new EntityPropertyLastUpdateUserSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("LAST_UPDATE_DATETIME", "LastUpdateDatetime", new EntityPropertyLastUpdateDatetimeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_FLAG", "TestFlag", new EntityPropertyTestFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_TYPE", "TestType", new EntityPropertyTestTypeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("TEST_SIGNIFICANCE_LV", "TestSignificanceLv", new EntityPropertyTestSignificanceLvSetupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TScenarioTotalization> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TScenarioTotalization)entity, value);
        }

        public class EntityPropertyScenarioTotalizationIdSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.ScenarioTotalizationId = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyQcwebidSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Qcwebid = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyScenarioTypeSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.ScenarioType = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyScenarioNameSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.ScenarioName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyConditionDivSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.ConditionDiv = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFilterFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.FilterFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.SortNo = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyWeightbackFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.WeightbackFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyWeightbackCodeSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.WeightbackCode = (value != null) ? (decimal?)value : null; }
        }
        public class EntityPropertyTotalnumFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.TotalnumFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyGraphOutputFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.GraphOutputFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPieChartChoiceFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.PieChartChoiceFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMinimumRateSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.MinimumRate = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAxisNoanswerOnoffSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.AxisNoanswerOnoff = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTargetNoanswerOnoffSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.TargetNoanswerOnoff = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyPolylineOnoffSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.PolylineOnoff = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyMarkingNSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.MarkingN = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyRankingFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.RankingFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRateFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.RateFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate1FlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate1Flag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate1SignSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate1Sign = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate1RangeSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate1Range = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyRate1Backcolor1Setupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate1Backcolor1 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate1Backcolor2Setupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate1Backcolor2 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate2FlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate2Flag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate2SignSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate2Sign = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate2RangeSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate2Range = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyRate2Backcolor1Setupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate2Backcolor1 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyRate2Backcolor2Setupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.Rate2Backcolor2 = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyLastUpdateUserSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.LastUpdateUser = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyLastUpdateDatetimeSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.LastUpdateDatetime = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertyTestFlagSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.TestFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestTypeSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.TestType = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyTestSignificanceLvSetupper : EntityPropertySetupper<TScenarioTotalization> {
            public void Setup(TScenarioTotalization entity, Object value) { entity.TestSignificanceLv = (value != null) ? (int?)value : null; }
        }
    }
}
