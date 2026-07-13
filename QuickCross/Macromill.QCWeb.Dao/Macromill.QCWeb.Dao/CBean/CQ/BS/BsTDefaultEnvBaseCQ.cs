
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDefaultEnvBaseCQ : AbstractBsTDefaultEnvBaseCQ {

        protected TDefaultEnvBaseCIQ _inlineQuery;

        public BsTDefaultEnvBaseCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDefaultEnvBaseCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDefaultEnvBaseCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDefaultEnvBaseCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDefaultEnvBaseCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _language;
        public ConditionValue Language {
            get { if (_language == null) { _language = new ConditionValue(); } return _language; }
        }
        protected override ConditionValue getCValueLanguage() { return this.Language; }


        protected Map<String, TDefaultEnvColorInfoCCQ> _language_ExistsSubQuery_TDefaultEnvColorInfoCListMap;
        public Map<String, TDefaultEnvColorInfoCCQ> Language_ExistsSubQuery_TDefaultEnvColorInfoCList { get { return _language_ExistsSubQuery_TDefaultEnvColorInfoCListMap; }}
        public override String keepLanguage_ExistsSubQuery_TDefaultEnvColorInfoCList(TDefaultEnvColorInfoCCQ subQuery) {
            if (_language_ExistsSubQuery_TDefaultEnvColorInfoCListMap == null) { _language_ExistsSubQuery_TDefaultEnvColorInfoCListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_language_ExistsSubQuery_TDefaultEnvColorInfoCListMap.size() + 1);
            _language_ExistsSubQuery_TDefaultEnvColorInfoCListMap.put(key, subQuery); return "Language_ExistsSubQuery_TDefaultEnvColorInfoCList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCCQ> _language_NotExistsSubQuery_TDefaultEnvColorInfoCListMap;
        public Map<String, TDefaultEnvColorInfoCCQ> Language_NotExistsSubQuery_TDefaultEnvColorInfoCList { get { return _language_NotExistsSubQuery_TDefaultEnvColorInfoCListMap; }}
        public override String keepLanguage_NotExistsSubQuery_TDefaultEnvColorInfoCList(TDefaultEnvColorInfoCCQ subQuery) {
            if (_language_NotExistsSubQuery_TDefaultEnvColorInfoCListMap == null) { _language_NotExistsSubQuery_TDefaultEnvColorInfoCListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_language_NotExistsSubQuery_TDefaultEnvColorInfoCListMap.size() + 1);
            _language_NotExistsSubQuery_TDefaultEnvColorInfoCListMap.put(key, subQuery); return "Language_NotExistsSubQuery_TDefaultEnvColorInfoCList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCCQ> _language_InScopeSubQuery_TDefaultEnvColorInfoCListMap;
        public Map<String, TDefaultEnvColorInfoCCQ> Language_InScopeSubQuery_TDefaultEnvColorInfoCList { get { return _language_InScopeSubQuery_TDefaultEnvColorInfoCListMap; }}
        public override String keepLanguage_InScopeSubQuery_TDefaultEnvColorInfoCList(TDefaultEnvColorInfoCCQ subQuery) {
            if (_language_InScopeSubQuery_TDefaultEnvColorInfoCListMap == null) { _language_InScopeSubQuery_TDefaultEnvColorInfoCListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_language_InScopeSubQuery_TDefaultEnvColorInfoCListMap.size() + 1);
            _language_InScopeSubQuery_TDefaultEnvColorInfoCListMap.put(key, subQuery); return "Language_InScopeSubQuery_TDefaultEnvColorInfoCList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCCQ> _language_NotInScopeSubQuery_TDefaultEnvColorInfoCListMap;
        public Map<String, TDefaultEnvColorInfoCCQ> Language_NotInScopeSubQuery_TDefaultEnvColorInfoCList { get { return _language_NotInScopeSubQuery_TDefaultEnvColorInfoCListMap; }}
        public override String keepLanguage_NotInScopeSubQuery_TDefaultEnvColorInfoCList(TDefaultEnvColorInfoCCQ subQuery) {
            if (_language_NotInScopeSubQuery_TDefaultEnvColorInfoCListMap == null) { _language_NotInScopeSubQuery_TDefaultEnvColorInfoCListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_language_NotInScopeSubQuery_TDefaultEnvColorInfoCListMap.size() + 1);
            _language_NotInScopeSubQuery_TDefaultEnvColorInfoCListMap.put(key, subQuery); return "Language_NotInScopeSubQuery_TDefaultEnvColorInfoCList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCCQ> _language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCListMap;
        public Map<String, TDefaultEnvColorInfoCCQ> Language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCList { get { return _language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCListMap; }}
        public override String keepLanguage_SpecifyDerivedReferrer_TDefaultEnvColorInfoCList(TDefaultEnvColorInfoCCQ subQuery) {
            if (_language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCListMap == null) { _language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCListMap.size() + 1);
           _language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCListMap.put(key, subQuery); return "Language_SpecifyDerivedReferrer_TDefaultEnvColorInfoCList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCCQ> _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListMap;
        public Map<String, TDefaultEnvColorInfoCCQ> Language_QueryDerivedReferrer_TDefaultEnvColorInfoCList { get { return _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListMap; } }
        public override String keepLanguage_QueryDerivedReferrer_TDefaultEnvColorInfoCList(TDefaultEnvColorInfoCCQ subQuery) {
            if (_language_QueryDerivedReferrer_TDefaultEnvColorInfoCListMap == null) { _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCCQ>(); }
            String key = "subQueryMapKey" + (_language_QueryDerivedReferrer_TDefaultEnvColorInfoCListMap.size() + 1);
            _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListMap.put(key, subQuery); return "Language_QueryDerivedReferrer_TDefaultEnvColorInfoCList." + key;
        }
        protected Map<String, Object> _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameterMap;
        public Map<String, Object> Language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameter { get { return _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameterMap; } }
        public override String keepLanguage_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameter(Object parameterValue) {
            if (_language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameterMap == null) { _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameterMap.size() + 1);
            _language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameterMap.put(key, parameterValue); return "Language_QueryDerivedReferrer_TDefaultEnvColorInfoCListParameter." + key;
        }

        public BsTDefaultEnvBaseCQ AddOrderBy_Language_Asc() { regOBA("LANGUAGE");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_Language_Desc() { regOBD("LANGUAGE");return this; }

        protected ConditionValue _noanswerDenominatorFlag;
        public ConditionValue NoanswerDenominatorFlag {
            get { if (_noanswerDenominatorFlag == null) { _noanswerDenominatorFlag = new ConditionValue(); } return _noanswerDenominatorFlag; }
        }
        protected override ConditionValue getCValueNoanswerDenominatorFlag() { return this.NoanswerDenominatorFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerDenominatorFlag_Asc() { regOBA("NOANSWER_DENOMINATOR_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerDenominatorFlag_Desc() { regOBD("NOANSWER_DENOMINATOR_FLAG");return this; }

        protected ConditionValue _visibleUnfitFlag;
        public ConditionValue VisibleUnfitFlag {
            get { if (_visibleUnfitFlag == null) { _visibleUnfitFlag = new ConditionValue(); } return _visibleUnfitFlag; }
        }
        protected override ConditionValue getCValueVisibleUnfitFlag() { return this.VisibleUnfitFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_VisibleUnfitFlag_Asc() { regOBA("VISIBLE_UNFIT_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_VisibleUnfitFlag_Desc() { regOBD("VISIBLE_UNFIT_FLAG");return this; }

        protected ConditionValue _noanswerUnfitFlag;
        public ConditionValue NoanswerUnfitFlag {
            get { if (_noanswerUnfitFlag == null) { _noanswerUnfitFlag = new ConditionValue(); } return _noanswerUnfitFlag; }
        }
        protected override ConditionValue getCValueNoanswerUnfitFlag() { return this.NoanswerUnfitFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerUnfitFlag_Asc() { regOBA("NOANSWER_UNFIT_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerUnfitFlag_Desc() { regOBD("NOANSWER_UNFIT_FLAG");return this; }

        protected ConditionValue _weightbackFlag;
        public ConditionValue WeightbackFlag {
            get { if (_weightbackFlag == null) { _weightbackFlag = new ConditionValue(); } return _weightbackFlag; }
        }
        protected override ConditionValue getCValueWeightbackFlag() { return this.WeightbackFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_WeightbackFlag_Asc() { regOBA("WEIGHTBACK_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_WeightbackFlag_Desc() { regOBD("WEIGHTBACK_FLAG");return this; }

        protected ConditionValue _cellJoincellJoinFlag;
        public ConditionValue CellJoincellJoinFlag {
            get { if (_cellJoincellJoinFlag == null) { _cellJoincellJoinFlag = new ConditionValue(); } return _cellJoincellJoinFlag; }
        }
        protected override ConditionValue getCValueCellJoincellJoinFlag() { return this.CellJoincellJoinFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_CellJoincellJoinFlag_Asc() { regOBA("CELL_JOINCELL_JOIN_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_CellJoincellJoinFlag_Desc() { regOBD("CELL_JOINCELL_JOIN_FLAG");return this; }

        protected ConditionValue _chartDirectionGtFlag;
        public ConditionValue ChartDirectionGtFlag {
            get { if (_chartDirectionGtFlag == null) { _chartDirectionGtFlag = new ConditionValue(); } return _chartDirectionGtFlag; }
        }
        protected override ConditionValue getCValueChartDirectionGtFlag() { return this.ChartDirectionGtFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_ChartDirectionGtFlag_Asc() { regOBA("CHART_DIRECTION_GT_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_ChartDirectionGtFlag_Desc() { regOBD("CHART_DIRECTION_GT_FLAG");return this; }

        protected ConditionValue _chartDirectionCrossFlag;
        public ConditionValue ChartDirectionCrossFlag {
            get { if (_chartDirectionCrossFlag == null) { _chartDirectionCrossFlag = new ConditionValue(); } return _chartDirectionCrossFlag; }
        }
        protected override ConditionValue getCValueChartDirectionCrossFlag() { return this.ChartDirectionCrossFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_ChartDirectionCrossFlag_Asc() { regOBA("CHART_DIRECTION_CROSS_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_ChartDirectionCrossFlag_Desc() { regOBD("CHART_DIRECTION_CROSS_FLAG");return this; }

        protected ConditionValue _noanswerTargetFlag;
        public ConditionValue NoanswerTargetFlag {
            get { if (_noanswerTargetFlag == null) { _noanswerTargetFlag = new ConditionValue(); } return _noanswerTargetFlag; }
        }
        protected override ConditionValue getCValueNoanswerTargetFlag() { return this.NoanswerTargetFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerTargetFlag_Asc() { regOBA("NOANSWER_TARGET_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerTargetFlag_Desc() { regOBD("NOANSWER_TARGET_FLAG");return this; }

        protected ConditionValue _noanswerAxisFlag;
        public ConditionValue NoanswerAxisFlag {
            get { if (_noanswerAxisFlag == null) { _noanswerAxisFlag = new ConditionValue(); } return _noanswerAxisFlag; }
        }
        protected override ConditionValue getCValueNoanswerAxisFlag() { return this.NoanswerAxisFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerAxisFlag_Asc() { regOBA("NOANSWER_AXIS_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_NoanswerAxisFlag_Desc() { regOBD("NOANSWER_AXIS_FLAG");return this; }

        protected ConditionValue _unfitTargetFlag;
        public ConditionValue UnfitTargetFlag {
            get { if (_unfitTargetFlag == null) { _unfitTargetFlag = new ConditionValue(); } return _unfitTargetFlag; }
        }
        protected override ConditionValue getCValueUnfitTargetFlag() { return this.UnfitTargetFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_UnfitTargetFlag_Asc() { regOBA("UNFIT_TARGET_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_UnfitTargetFlag_Desc() { regOBD("UNFIT_TARGET_FLAG");return this; }

        protected ConditionValue _unfitAxisFlag;
        public ConditionValue UnfitAxisFlag {
            get { if (_unfitAxisFlag == null) { _unfitAxisFlag = new ConditionValue(); } return _unfitAxisFlag; }
        }
        protected override ConditionValue getCValueUnfitAxisFlag() { return this.UnfitAxisFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_UnfitAxisFlag_Asc() { regOBA("UNFIT_AXIS_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_UnfitAxisFlag_Desc() { regOBD("UNFIT_AXIS_FLAG");return this; }

        protected ConditionValue _totalnumFlag;
        public ConditionValue TotalnumFlag {
            get { if (_totalnumFlag == null) { _totalnumFlag = new ConditionValue(); } return _totalnumFlag; }
        }
        protected override ConditionValue getCValueTotalnumFlag() { return this.TotalnumFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TotalnumFlag_Asc() { regOBA("TOTALNUM_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TotalnumFlag_Desc() { regOBD("TOTALNUM_FLAG");return this; }

        protected ConditionValue _rateDiffColorMinus5;
        public ConditionValue RateDiffColorMinus5 {
            get { if (_rateDiffColorMinus5 == null) { _rateDiffColorMinus5 = new ConditionValue(); } return _rateDiffColorMinus5; }
        }
        protected override ConditionValue getCValueRateDiffColorMinus5() { return this.RateDiffColorMinus5; }


        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorMinus5_Asc() { regOBA("RATE_DIFF_COLOR_MINUS5");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorMinus5_Desc() { regOBD("RATE_DIFF_COLOR_MINUS5");return this; }

        protected ConditionValue _rateDiffColorMinus10;
        public ConditionValue RateDiffColorMinus10 {
            get { if (_rateDiffColorMinus10 == null) { _rateDiffColorMinus10 = new ConditionValue(); } return _rateDiffColorMinus10; }
        }
        protected override ConditionValue getCValueRateDiffColorMinus10() { return this.RateDiffColorMinus10; }


        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorMinus10_Asc() { regOBA("RATE_DIFF_COLOR_MINUS10");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorMinus10_Desc() { regOBD("RATE_DIFF_COLOR_MINUS10");return this; }

        protected ConditionValue _rateDiffColorPlus5;
        public ConditionValue RateDiffColorPlus5 {
            get { if (_rateDiffColorPlus5 == null) { _rateDiffColorPlus5 = new ConditionValue(); } return _rateDiffColorPlus5; }
        }
        protected override ConditionValue getCValueRateDiffColorPlus5() { return this.RateDiffColorPlus5; }


        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorPlus5_Asc() { regOBA("RATE_DIFF_COLOR_PLUS5");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorPlus5_Desc() { regOBD("RATE_DIFF_COLOR_PLUS5");return this; }

        protected ConditionValue _rateDiffColorPlus10;
        public ConditionValue RateDiffColorPlus10 {
            get { if (_rateDiffColorPlus10 == null) { _rateDiffColorPlus10 = new ConditionValue(); } return _rateDiffColorPlus10; }
        }
        protected override ConditionValue getCValueRateDiffColorPlus10() { return this.RateDiffColorPlus10; }


        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorPlus10_Asc() { regOBA("RATE_DIFF_COLOR_PLUS10");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_RateDiffColorPlus10_Desc() { regOBD("RATE_DIFF_COLOR_PLUS10");return this; }

        protected ConditionValue _graphTypeSa;
        public ConditionValue GraphTypeSa {
            get { if (_graphTypeSa == null) { _graphTypeSa = new ConditionValue(); } return _graphTypeSa; }
        }
        protected override ConditionValue getCValueGraphTypeSa() { return this.GraphTypeSa; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeSa_Asc() { regOBA("GRAPH_TYPE_SA");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeSa_Desc() { regOBD("GRAPH_TYPE_SA");return this; }

        protected ConditionValue _graphTypeSaMatrix;
        public ConditionValue GraphTypeSaMatrix {
            get { if (_graphTypeSaMatrix == null) { _graphTypeSaMatrix = new ConditionValue(); } return _graphTypeSaMatrix; }
        }
        protected override ConditionValue getCValueGraphTypeSaMatrix() { return this.GraphTypeSaMatrix; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeSaMatrix_Asc() { regOBA("GRAPH_TYPE_SA_MATRIX");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeSaMatrix_Desc() { regOBD("GRAPH_TYPE_SA_MATRIX");return this; }

        protected ConditionValue _graphTypeMaSimple;
        public ConditionValue GraphTypeMaSimple {
            get { if (_graphTypeMaSimple == null) { _graphTypeMaSimple = new ConditionValue(); } return _graphTypeMaSimple; }
        }
        protected override ConditionValue getCValueGraphTypeMaSimple() { return this.GraphTypeMaSimple; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeMaSimple_Asc() { regOBA("GRAPH_TYPE_MA_SIMPLE");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeMaSimple_Desc() { regOBD("GRAPH_TYPE_MA_SIMPLE");return this; }

        protected ConditionValue _graphTypeMaCross;
        public ConditionValue GraphTypeMaCross {
            get { if (_graphTypeMaCross == null) { _graphTypeMaCross = new ConditionValue(); } return _graphTypeMaCross; }
        }
        protected override ConditionValue getCValueGraphTypeMaCross() { return this.GraphTypeMaCross; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeMaCross_Asc() { regOBA("GRAPH_TYPE_MA_CROSS");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeMaCross_Desc() { regOBD("GRAPH_TYPE_MA_CROSS");return this; }

        protected ConditionValue _graphTypeMaMatrix;
        public ConditionValue GraphTypeMaMatrix {
            get { if (_graphTypeMaMatrix == null) { _graphTypeMaMatrix = new ConditionValue(); } return _graphTypeMaMatrix; }
        }
        protected override ConditionValue getCValueGraphTypeMaMatrix() { return this.GraphTypeMaMatrix; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeMaMatrix_Asc() { regOBA("GRAPH_TYPE_MA_MATRIX");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeMaMatrix_Desc() { regOBD("GRAPH_TYPE_MA_MATRIX");return this; }

        protected ConditionValue _graphTypeNRate;
        public ConditionValue GraphTypeNRate {
            get { if (_graphTypeNRate == null) { _graphTypeNRate = new ConditionValue(); } return _graphTypeNRate; }
        }
        protected override ConditionValue getCValueGraphTypeNRate() { return this.GraphTypeNRate; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeNRate_Asc() { regOBA("GRAPH_TYPE_N_RATE");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeNRate_Desc() { regOBD("GRAPH_TYPE_N_RATE");return this; }

        protected ConditionValue _graphTypeNRanking;
        public ConditionValue GraphTypeNRanking {
            get { if (_graphTypeNRanking == null) { _graphTypeNRanking = new ConditionValue(); } return _graphTypeNRanking; }
        }
        protected override ConditionValue getCValueGraphTypeNRanking() { return this.GraphTypeNRanking; }


        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeNRanking_Asc() { regOBA("GRAPH_TYPE_N_RANKING");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_GraphTypeNRanking_Desc() { regOBD("GRAPH_TYPE_N_RANKING");return this; }

        protected ConditionValue _setExecuteFlag;
        public ConditionValue SetExecuteFlag {
            get { if (_setExecuteFlag == null) { _setExecuteFlag = new ConditionValue(); } return _setExecuteFlag; }
        }
        protected override ConditionValue getCValueSetExecuteFlag() { return this.SetExecuteFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_SetExecuteFlag_Asc() { regOBA("SET_EXECUTE_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_SetExecuteFlag_Desc() { regOBD("SET_EXECUTE_FLAG");return this; }

        protected ConditionValue _titleAll;
        public ConditionValue TitleAll {
            get { if (_titleAll == null) { _titleAll = new ConditionValue(); } return _titleAll; }
        }
        protected override ConditionValue getCValueTitleAll() { return this.TitleAll; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleAll_Asc() { regOBA("TITLE_ALL");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleAll_Desc() { regOBD("TITLE_ALL");return this; }

        protected ConditionValue _titleAxisAll;
        public ConditionValue TitleAxisAll {
            get { if (_titleAxisAll == null) { _titleAxisAll = new ConditionValue(); } return _titleAxisAll; }
        }
        protected override ConditionValue getCValueTitleAxisAll() { return this.TitleAxisAll; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleAxisAll_Asc() { regOBA("TITLE_AXIS_ALL");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleAxisAll_Desc() { regOBD("TITLE_AXIS_ALL");return this; }

        protected ConditionValue _titleNoanswer;
        public ConditionValue TitleNoanswer {
            get { if (_titleNoanswer == null) { _titleNoanswer = new ConditionValue(); } return _titleNoanswer; }
        }
        protected override ConditionValue getCValueTitleNoanswer() { return this.TitleNoanswer; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleNoanswer_Asc() { regOBA("TITLE_NOANSWER");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleNoanswer_Desc() { regOBD("TITLE_NOANSWER");return this; }

        protected ConditionValue _titleUnfit;
        public ConditionValue TitleUnfit {
            get { if (_titleUnfit == null) { _titleUnfit = new ConditionValue(); } return _titleUnfit; }
        }
        protected override ConditionValue getCValueTitleUnfit() { return this.TitleUnfit; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleUnfit_Asc() { regOBA("TITLE_UNFIT");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleUnfit_Desc() { regOBD("TITLE_UNFIT");return this; }

        protected ConditionValue _titleBeforeWb;
        public ConditionValue TitleBeforeWb {
            get { if (_titleBeforeWb == null) { _titleBeforeWb = new ConditionValue(); } return _titleBeforeWb; }
        }
        protected override ConditionValue getCValueTitleBeforeWb() { return this.TitleBeforeWb; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleBeforeWb_Asc() { regOBA("TITLE_BEFORE_WB");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleBeforeWb_Desc() { regOBD("TITLE_BEFORE_WB");return this; }

        protected ConditionValue _flagStatisticsParameter;
        public ConditionValue FlagStatisticsParameter {
            get { if (_flagStatisticsParameter == null) { _flagStatisticsParameter = new ConditionValue(); } return _flagStatisticsParameter; }
        }
        protected override ConditionValue getCValueFlagStatisticsParameter() { return this.FlagStatisticsParameter; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagStatisticsParameter_Asc() { regOBA("FLAG_STATISTICS_PARAMETER");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagStatisticsParameter_Desc() { regOBD("FLAG_STATISTICS_PARAMETER");return this; }

        protected ConditionValue _titleStatisticsParameter;
        public ConditionValue TitleStatisticsParameter {
            get { if (_titleStatisticsParameter == null) { _titleStatisticsParameter = new ConditionValue(); } return _titleStatisticsParameter; }
        }
        protected override ConditionValue getCValueTitleStatisticsParameter() { return this.TitleStatisticsParameter; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleStatisticsParameter_Asc() { regOBA("TITLE_STATISTICS_PARAMETER");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleStatisticsParameter_Desc() { regOBD("TITLE_STATISTICS_PARAMETER");return this; }

        protected ConditionValue _flagTotal;
        public ConditionValue FlagTotal {
            get { if (_flagTotal == null) { _flagTotal = new ConditionValue(); } return _flagTotal; }
        }
        protected override ConditionValue getCValueFlagTotal() { return this.FlagTotal; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagTotal_Asc() { regOBA("FLAG_TOTAL");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagTotal_Desc() { regOBD("FLAG_TOTAL");return this; }

        protected ConditionValue _titleTotal;
        public ConditionValue TitleTotal {
            get { if (_titleTotal == null) { _titleTotal = new ConditionValue(); } return _titleTotal; }
        }
        protected override ConditionValue getCValueTitleTotal() { return this.TitleTotal; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleTotal_Asc() { regOBA("TITLE_TOTAL");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleTotal_Desc() { regOBD("TITLE_TOTAL");return this; }

        protected ConditionValue _dpSum;
        public ConditionValue DpSum {
            get { if (_dpSum == null) { _dpSum = new ConditionValue(); } return _dpSum; }
        }
        protected override ConditionValue getCValueDpSum() { return this.DpSum; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpSum_Asc() { regOBA("DP_SUM");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpSum_Desc() { regOBD("DP_SUM");return this; }

        protected ConditionValue _flagAvr;
        public ConditionValue FlagAvr {
            get { if (_flagAvr == null) { _flagAvr = new ConditionValue(); } return _flagAvr; }
        }
        protected override ConditionValue getCValueFlagAvr() { return this.FlagAvr; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagAvr_Asc() { regOBA("FLAG_AVR");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagAvr_Desc() { regOBD("FLAG_AVR");return this; }

        protected ConditionValue _titleAvr;
        public ConditionValue TitleAvr {
            get { if (_titleAvr == null) { _titleAvr = new ConditionValue(); } return _titleAvr; }
        }
        protected override ConditionValue getCValueTitleAvr() { return this.TitleAvr; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleAvr_Asc() { regOBA("TITLE_AVR");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleAvr_Desc() { regOBD("TITLE_AVR");return this; }

        protected ConditionValue _dpAvr;
        public ConditionValue DpAvr {
            get { if (_dpAvr == null) { _dpAvr = new ConditionValue(); } return _dpAvr; }
        }
        protected override ConditionValue getCValueDpAvr() { return this.DpAvr; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpAvr_Asc() { regOBA("DP_AVR");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpAvr_Desc() { regOBD("DP_AVR");return this; }

        protected ConditionValue _flagSd;
        public ConditionValue FlagSd {
            get { if (_flagSd == null) { _flagSd = new ConditionValue(); } return _flagSd; }
        }
        protected override ConditionValue getCValueFlagSd() { return this.FlagSd; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagSd_Asc() { regOBA("FLAG_SD");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagSd_Desc() { regOBD("FLAG_SD");return this; }

        protected ConditionValue _titleSd;
        public ConditionValue TitleSd {
            get { if (_titleSd == null) { _titleSd = new ConditionValue(); } return _titleSd; }
        }
        protected override ConditionValue getCValueTitleSd() { return this.TitleSd; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleSd_Asc() { regOBA("TITLE_SD");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleSd_Desc() { regOBD("TITLE_SD");return this; }

        protected ConditionValue _dpSd;
        public ConditionValue DpSd {
            get { if (_dpSd == null) { _dpSd = new ConditionValue(); } return _dpSd; }
        }
        protected override ConditionValue getCValueDpSd() { return this.DpSd; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpSd_Asc() { regOBA("DP_SD");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpSd_Desc() { regOBD("DP_SD");return this; }

        protected ConditionValue _flagMin;
        public ConditionValue FlagMin {
            get { if (_flagMin == null) { _flagMin = new ConditionValue(); } return _flagMin; }
        }
        protected override ConditionValue getCValueFlagMin() { return this.FlagMin; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagMin_Asc() { regOBA("FLAG_MIN");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagMin_Desc() { regOBD("FLAG_MIN");return this; }

        protected ConditionValue _titleMin;
        public ConditionValue TitleMin {
            get { if (_titleMin == null) { _titleMin = new ConditionValue(); } return _titleMin; }
        }
        protected override ConditionValue getCValueTitleMin() { return this.TitleMin; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleMin_Asc() { regOBA("TITLE_MIN");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleMin_Desc() { regOBD("TITLE_MIN");return this; }

        protected ConditionValue _dpMin;
        public ConditionValue DpMin {
            get { if (_dpMin == null) { _dpMin = new ConditionValue(); } return _dpMin; }
        }
        protected override ConditionValue getCValueDpMin() { return this.DpMin; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpMin_Asc() { regOBA("DP_MIN");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpMin_Desc() { regOBD("DP_MIN");return this; }

        protected ConditionValue _flagMax;
        public ConditionValue FlagMax {
            get { if (_flagMax == null) { _flagMax = new ConditionValue(); } return _flagMax; }
        }
        protected override ConditionValue getCValueFlagMax() { return this.FlagMax; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagMax_Asc() { regOBA("FLAG_MAX");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagMax_Desc() { regOBD("FLAG_MAX");return this; }

        protected ConditionValue _titleMax;
        public ConditionValue TitleMax {
            get { if (_titleMax == null) { _titleMax = new ConditionValue(); } return _titleMax; }
        }
        protected override ConditionValue getCValueTitleMax() { return this.TitleMax; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleMax_Asc() { regOBA("TITLE_MAX");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleMax_Desc() { regOBD("TITLE_MAX");return this; }

        protected ConditionValue _dpMax;
        public ConditionValue DpMax {
            get { if (_dpMax == null) { _dpMax = new ConditionValue(); } return _dpMax; }
        }
        protected override ConditionValue getCValueDpMax() { return this.DpMax; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpMax_Asc() { regOBA("DP_MAX");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpMax_Desc() { regOBD("DP_MAX");return this; }

        protected ConditionValue _flagMedian;
        public ConditionValue FlagMedian {
            get { if (_flagMedian == null) { _flagMedian = new ConditionValue(); } return _flagMedian; }
        }
        protected override ConditionValue getCValueFlagMedian() { return this.FlagMedian; }


        public BsTDefaultEnvBaseCQ AddOrderBy_FlagMedian_Asc() { regOBA("FLAG_MEDIAN");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_FlagMedian_Desc() { regOBD("FLAG_MEDIAN");return this; }

        protected ConditionValue _titleMedian;
        public ConditionValue TitleMedian {
            get { if (_titleMedian == null) { _titleMedian = new ConditionValue(); } return _titleMedian; }
        }
        protected override ConditionValue getCValueTitleMedian() { return this.TitleMedian; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TitleMedian_Asc() { regOBA("TITLE_MEDIAN");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TitleMedian_Desc() { regOBD("TITLE_MEDIAN");return this; }

        protected ConditionValue _dpMedian;
        public ConditionValue DpMedian {
            get { if (_dpMedian == null) { _dpMedian = new ConditionValue(); } return _dpMedian; }
        }
        protected override ConditionValue getCValueDpMedian() { return this.DpMedian; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpMedian_Asc() { regOBA("DP_MEDIAN");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpMedian_Desc() { regOBD("DP_MEDIAN");return this; }

        protected ConditionValue _dpWeight;
        public ConditionValue DpWeight {
            get { if (_dpWeight == null) { _dpWeight = new ConditionValue(); } return _dpWeight; }
        }
        protected override ConditionValue getCValueDpWeight() { return this.DpWeight; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpWeight_Asc() { regOBA("DP_WEIGHT");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpWeight_Desc() { regOBD("DP_WEIGHT");return this; }

        protected ConditionValue _dpWeightAvr;
        public ConditionValue DpWeightAvr {
            get { if (_dpWeightAvr == null) { _dpWeightAvr = new ConditionValue(); } return _dpWeightAvr; }
        }
        protected override ConditionValue getCValueDpWeightAvr() { return this.DpWeightAvr; }


        public BsTDefaultEnvBaseCQ AddOrderBy_DpWeightAvr_Asc() { regOBA("DP_WEIGHT_AVR");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_DpWeightAvr_Desc() { regOBD("DP_WEIGHT_AVR");return this; }

        protected ConditionValue _testGtFlag;
        public ConditionValue TestGtFlag {
            get { if (_testGtFlag == null) { _testGtFlag = new ConditionValue(); } return _testGtFlag; }
        }
        protected override ConditionValue getCValueTestGtFlag() { return this.TestGtFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TestGtFlag_Asc() { regOBA("TEST_GT_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TestGtFlag_Desc() { regOBD("TEST_GT_FLAG");return this; }

        protected ConditionValue _testCrossFlag;
        public ConditionValue TestCrossFlag {
            get { if (_testCrossFlag == null) { _testCrossFlag = new ConditionValue(); } return _testCrossFlag; }
        }
        protected override ConditionValue getCValueTestCrossFlag() { return this.TestCrossFlag; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TestCrossFlag_Asc() { regOBA("TEST_CROSS_FLAG");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TestCrossFlag_Desc() { regOBD("TEST_CROSS_FLAG");return this; }

        protected ConditionValue _testTypeGt;
        public ConditionValue TestTypeGt {
            get { if (_testTypeGt == null) { _testTypeGt = new ConditionValue(); } return _testTypeGt; }
        }
        protected override ConditionValue getCValueTestTypeGt() { return this.TestTypeGt; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TestTypeGt_Asc() { regOBA("TEST_TYPE_GT");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TestTypeGt_Desc() { regOBD("TEST_TYPE_GT");return this; }

        protected ConditionValue _testTypeCross;
        public ConditionValue TestTypeCross {
            get { if (_testTypeCross == null) { _testTypeCross = new ConditionValue(); } return _testTypeCross; }
        }
        protected override ConditionValue getCValueTestTypeCross() { return this.TestTypeCross; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TestTypeCross_Asc() { regOBA("TEST_TYPE_CROSS");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TestTypeCross_Desc() { regOBD("TEST_TYPE_CROSS");return this; }

        protected ConditionValue _testSignificanceLvGt;
        public ConditionValue TestSignificanceLvGt {
            get { if (_testSignificanceLvGt == null) { _testSignificanceLvGt = new ConditionValue(); } return _testSignificanceLvGt; }
        }
        protected override ConditionValue getCValueTestSignificanceLvGt() { return this.TestSignificanceLvGt; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TestSignificanceLvGt_Asc() { regOBA("TEST_SIGNIFICANCE_LV_GT");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TestSignificanceLvGt_Desc() { regOBD("TEST_SIGNIFICANCE_LV_GT");return this; }

        protected ConditionValue _testSignificanceLvCross;
        public ConditionValue TestSignificanceLvCross {
            get { if (_testSignificanceLvCross == null) { _testSignificanceLvCross = new ConditionValue(); } return _testSignificanceLvCross; }
        }
        protected override ConditionValue getCValueTestSignificanceLvCross() { return this.TestSignificanceLvCross; }


        public BsTDefaultEnvBaseCQ AddOrderBy_TestSignificanceLvCross_Asc() { regOBA("TEST_SIGNIFICANCE_LV_CROSS");return this; }
        public BsTDefaultEnvBaseCQ AddOrderBy_TestSignificanceLvCross_Desc() { regOBD("TEST_SIGNIFICANCE_LV_CROSS");return this; }

        public BsTDefaultEnvBaseCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDefaultEnvBaseCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDefaultEnvBaseCQ> _scalarSubQueryMap;
	    public Map<String, TDefaultEnvBaseCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDefaultEnvBaseCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDefaultEnvBaseCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDefaultEnvBaseCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDefaultEnvBaseCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDefaultEnvBaseCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDefaultEnvBaseCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
