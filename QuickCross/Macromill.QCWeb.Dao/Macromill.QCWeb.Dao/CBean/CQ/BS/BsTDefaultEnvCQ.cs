
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTDefaultEnvCQ : AbstractBsTDefaultEnvCQ {

        protected TDefaultEnvCIQ _inlineQuery;

        public BsTDefaultEnvCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TDefaultEnvCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TDefaultEnvCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TDefaultEnvCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TDefaultEnvCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _qcwebid;
        public ConditionValue Qcwebid {
            get { if (_qcwebid == null) { _qcwebid = new ConditionValue(); } return _qcwebid; }
        }
        protected override ConditionValue getCValueQcwebid() { return this.Qcwebid; }


        protected Map<String, TDefaultEnvColorInfoCQ> _qcwebid_ExistsSubQuery_TDefaultEnvColorInfoListMap;
        public Map<String, TDefaultEnvColorInfoCQ> Qcwebid_ExistsSubQuery_TDefaultEnvColorInfoList { get { return _qcwebid_ExistsSubQuery_TDefaultEnvColorInfoListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TDefaultEnvColorInfoListMap == null) { _qcwebid_ExistsSubQuery_TDefaultEnvColorInfoListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TDefaultEnvColorInfoListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TDefaultEnvColorInfoListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TDefaultEnvColorInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_ExistsSubQuery_TScenarioTotalizationList { get { return _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_ExistsSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_ExistsSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_ExistsSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_ExistsSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCQ> _qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoListMap;
        public Map<String, TDefaultEnvColorInfoCQ> Qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList { get { return _qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoListMap == null) { _qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_NotExistsSubQuery_TScenarioTotalizationList { get { return _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_NotExistsSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_NotExistsSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCQ> _qcwebid_InScopeSubQuery_TDefaultEnvColorInfoListMap;
        public Map<String, TDefaultEnvColorInfoCQ> Qcwebid_InScopeSubQuery_TDefaultEnvColorInfoList { get { return _qcwebid_InScopeSubQuery_TDefaultEnvColorInfoListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TDefaultEnvColorInfoListMap == null) { _qcwebid_InScopeSubQuery_TDefaultEnvColorInfoListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TDefaultEnvColorInfoListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TDefaultEnvColorInfoListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TDefaultEnvColorInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_InScopeSubQuery_TScenarioTotalizationList { get { return _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_InScopeSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_InScopeSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_InScopeSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_InScopeSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCQ> _qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoListMap;
        public Map<String, TDefaultEnvColorInfoCQ> Qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList { get { return _qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoListMap == null) { _qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList." + key;
        }

        protected Map<String, TQcwebSurveyInfoCQ> _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap;
        public Map<String, TQcwebSurveyInfoCQ> Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne { get { return _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap == null) { _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap = new LinkedHashMap<String, TQcwebSurveyInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOneMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_NotInScopeSubQuery_TScenarioTotalizationList { get { return _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap == null) { _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_NotInScopeSubQuery_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_NotInScopeSubQuery_TScenarioTotalizationList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCQ> _qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoListMap;
        public Map<String, TDefaultEnvColorInfoCQ> Qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoList { get { return _qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoListMap == null) { _qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoList." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList { get { return _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap; }}
        public override String keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap == null) { _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList." + key;
        }

        protected Map<String, TDefaultEnvColorInfoCQ> _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListMap;
        public Map<String, TDefaultEnvColorInfoCQ> Qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoList { get { return _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListMap == null) { _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListMap = new LinkedHashMap<String, TDefaultEnvColorInfoCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameter { get { return _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameter." + key;
        }

        protected Map<String, TScenarioTotalizationCQ> _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap;
        public Map<String, TScenarioTotalizationCQ> Qcwebid_QueryDerivedReferrer_TScenarioTotalizationList { get { return _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            if (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap == null) { _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap = new LinkedHashMap<String, TScenarioTotalizationCQ>(); }
            String key = "subQueryMapKey" + (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListMap.put(key, subQuery); return "Qcwebid_QueryDerivedReferrer_TScenarioTotalizationList." + key;
        }
        protected Map<String, Object> _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap;
        public Map<String, Object> Qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter { get { return _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap; } }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(Object parameterValue) {
            if (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap == null) { _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap = new LinkedHashMap<String, Object>(); }
            String key = "subQueryParameterKey" + (_qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap.size() + 1);
            _qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameterMap.put(key, parameterValue); return "Qcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter." + key;
        }

        public BsTDefaultEnvCQ AddOrderBy_Qcwebid_Asc() { regOBA("QCWEBID");return this; }
        public BsTDefaultEnvCQ AddOrderBy_Qcwebid_Desc() { regOBD("QCWEBID");return this; }

        protected ConditionValue _noanswerDenominatorFlag;
        public ConditionValue NoanswerDenominatorFlag {
            get { if (_noanswerDenominatorFlag == null) { _noanswerDenominatorFlag = new ConditionValue(); } return _noanswerDenominatorFlag; }
        }
        protected override ConditionValue getCValueNoanswerDenominatorFlag() { return this.NoanswerDenominatorFlag; }


        public BsTDefaultEnvCQ AddOrderBy_NoanswerDenominatorFlag_Asc() { regOBA("NOANSWER_DENOMINATOR_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_NoanswerDenominatorFlag_Desc() { regOBD("NOANSWER_DENOMINATOR_FLAG");return this; }

        protected ConditionValue _visibleUnfitFlag;
        public ConditionValue VisibleUnfitFlag {
            get { if (_visibleUnfitFlag == null) { _visibleUnfitFlag = new ConditionValue(); } return _visibleUnfitFlag; }
        }
        protected override ConditionValue getCValueVisibleUnfitFlag() { return this.VisibleUnfitFlag; }


        public BsTDefaultEnvCQ AddOrderBy_VisibleUnfitFlag_Asc() { regOBA("VISIBLE_UNFIT_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_VisibleUnfitFlag_Desc() { regOBD("VISIBLE_UNFIT_FLAG");return this; }

        protected ConditionValue _noanswerUnfitFlag;
        public ConditionValue NoanswerUnfitFlag {
            get { if (_noanswerUnfitFlag == null) { _noanswerUnfitFlag = new ConditionValue(); } return _noanswerUnfitFlag; }
        }
        protected override ConditionValue getCValueNoanswerUnfitFlag() { return this.NoanswerUnfitFlag; }


        public BsTDefaultEnvCQ AddOrderBy_NoanswerUnfitFlag_Asc() { regOBA("NOANSWER_UNFIT_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_NoanswerUnfitFlag_Desc() { regOBD("NOANSWER_UNFIT_FLAG");return this; }

        protected ConditionValue _weightbackFlag;
        public ConditionValue WeightbackFlag {
            get { if (_weightbackFlag == null) { _weightbackFlag = new ConditionValue(); } return _weightbackFlag; }
        }
        protected override ConditionValue getCValueWeightbackFlag() { return this.WeightbackFlag; }


        public BsTDefaultEnvCQ AddOrderBy_WeightbackFlag_Asc() { regOBA("WEIGHTBACK_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_WeightbackFlag_Desc() { regOBD("WEIGHTBACK_FLAG");return this; }

        protected ConditionValue _cellJoincellJoinFlag;
        public ConditionValue CellJoincellJoinFlag {
            get { if (_cellJoincellJoinFlag == null) { _cellJoincellJoinFlag = new ConditionValue(); } return _cellJoincellJoinFlag; }
        }
        protected override ConditionValue getCValueCellJoincellJoinFlag() { return this.CellJoincellJoinFlag; }


        public BsTDefaultEnvCQ AddOrderBy_CellJoincellJoinFlag_Asc() { regOBA("CELL_JOINCELL_JOIN_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_CellJoincellJoinFlag_Desc() { regOBD("CELL_JOINCELL_JOIN_FLAG");return this; }

        protected ConditionValue _chartDirectionGtFlag;
        public ConditionValue ChartDirectionGtFlag {
            get { if (_chartDirectionGtFlag == null) { _chartDirectionGtFlag = new ConditionValue(); } return _chartDirectionGtFlag; }
        }
        protected override ConditionValue getCValueChartDirectionGtFlag() { return this.ChartDirectionGtFlag; }


        public BsTDefaultEnvCQ AddOrderBy_ChartDirectionGtFlag_Asc() { regOBA("CHART_DIRECTION_GT_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_ChartDirectionGtFlag_Desc() { regOBD("CHART_DIRECTION_GT_FLAG");return this; }

        protected ConditionValue _chartDirectionCrossFlag;
        public ConditionValue ChartDirectionCrossFlag {
            get { if (_chartDirectionCrossFlag == null) { _chartDirectionCrossFlag = new ConditionValue(); } return _chartDirectionCrossFlag; }
        }
        protected override ConditionValue getCValueChartDirectionCrossFlag() { return this.ChartDirectionCrossFlag; }


        public BsTDefaultEnvCQ AddOrderBy_ChartDirectionCrossFlag_Asc() { regOBA("CHART_DIRECTION_CROSS_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_ChartDirectionCrossFlag_Desc() { regOBD("CHART_DIRECTION_CROSS_FLAG");return this; }

        protected ConditionValue _noanswerTargetFlag;
        public ConditionValue NoanswerTargetFlag {
            get { if (_noanswerTargetFlag == null) { _noanswerTargetFlag = new ConditionValue(); } return _noanswerTargetFlag; }
        }
        protected override ConditionValue getCValueNoanswerTargetFlag() { return this.NoanswerTargetFlag; }


        public BsTDefaultEnvCQ AddOrderBy_NoanswerTargetFlag_Asc() { regOBA("NOANSWER_TARGET_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_NoanswerTargetFlag_Desc() { regOBD("NOANSWER_TARGET_FLAG");return this; }

        protected ConditionValue _noanswerAxisFlag;
        public ConditionValue NoanswerAxisFlag {
            get { if (_noanswerAxisFlag == null) { _noanswerAxisFlag = new ConditionValue(); } return _noanswerAxisFlag; }
        }
        protected override ConditionValue getCValueNoanswerAxisFlag() { return this.NoanswerAxisFlag; }


        public BsTDefaultEnvCQ AddOrderBy_NoanswerAxisFlag_Asc() { regOBA("NOANSWER_AXIS_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_NoanswerAxisFlag_Desc() { regOBD("NOANSWER_AXIS_FLAG");return this; }

        protected ConditionValue _unfitTargetFlag;
        public ConditionValue UnfitTargetFlag {
            get { if (_unfitTargetFlag == null) { _unfitTargetFlag = new ConditionValue(); } return _unfitTargetFlag; }
        }
        protected override ConditionValue getCValueUnfitTargetFlag() { return this.UnfitTargetFlag; }


        public BsTDefaultEnvCQ AddOrderBy_UnfitTargetFlag_Asc() { regOBA("UNFIT_TARGET_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_UnfitTargetFlag_Desc() { regOBD("UNFIT_TARGET_FLAG");return this; }

        protected ConditionValue _unfitAxisFlag;
        public ConditionValue UnfitAxisFlag {
            get { if (_unfitAxisFlag == null) { _unfitAxisFlag = new ConditionValue(); } return _unfitAxisFlag; }
        }
        protected override ConditionValue getCValueUnfitAxisFlag() { return this.UnfitAxisFlag; }


        public BsTDefaultEnvCQ AddOrderBy_UnfitAxisFlag_Asc() { regOBA("UNFIT_AXIS_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_UnfitAxisFlag_Desc() { regOBD("UNFIT_AXIS_FLAG");return this; }

        protected ConditionValue _totalnumFlag;
        public ConditionValue TotalnumFlag {
            get { if (_totalnumFlag == null) { _totalnumFlag = new ConditionValue(); } return _totalnumFlag; }
        }
        protected override ConditionValue getCValueTotalnumFlag() { return this.TotalnumFlag; }


        public BsTDefaultEnvCQ AddOrderBy_TotalnumFlag_Asc() { regOBA("TOTALNUM_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TotalnumFlag_Desc() { regOBD("TOTALNUM_FLAG");return this; }

        protected ConditionValue _rateDiffColorMinus5;
        public ConditionValue RateDiffColorMinus5 {
            get { if (_rateDiffColorMinus5 == null) { _rateDiffColorMinus5 = new ConditionValue(); } return _rateDiffColorMinus5; }
        }
        protected override ConditionValue getCValueRateDiffColorMinus5() { return this.RateDiffColorMinus5; }


        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorMinus5_Asc() { regOBA("RATE_DIFF_COLOR_MINUS5");return this; }
        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorMinus5_Desc() { regOBD("RATE_DIFF_COLOR_MINUS5");return this; }

        protected ConditionValue _rateDiffColorMinus10;
        public ConditionValue RateDiffColorMinus10 {
            get { if (_rateDiffColorMinus10 == null) { _rateDiffColorMinus10 = new ConditionValue(); } return _rateDiffColorMinus10; }
        }
        protected override ConditionValue getCValueRateDiffColorMinus10() { return this.RateDiffColorMinus10; }


        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorMinus10_Asc() { regOBA("RATE_DIFF_COLOR_MINUS10");return this; }
        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorMinus10_Desc() { regOBD("RATE_DIFF_COLOR_MINUS10");return this; }

        protected ConditionValue _rateDiffColorPlus5;
        public ConditionValue RateDiffColorPlus5 {
            get { if (_rateDiffColorPlus5 == null) { _rateDiffColorPlus5 = new ConditionValue(); } return _rateDiffColorPlus5; }
        }
        protected override ConditionValue getCValueRateDiffColorPlus5() { return this.RateDiffColorPlus5; }


        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorPlus5_Asc() { regOBA("RATE_DIFF_COLOR_PLUS5");return this; }
        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorPlus5_Desc() { regOBD("RATE_DIFF_COLOR_PLUS5");return this; }

        protected ConditionValue _rateDiffColorPlus10;
        public ConditionValue RateDiffColorPlus10 {
            get { if (_rateDiffColorPlus10 == null) { _rateDiffColorPlus10 = new ConditionValue(); } return _rateDiffColorPlus10; }
        }
        protected override ConditionValue getCValueRateDiffColorPlus10() { return this.RateDiffColorPlus10; }


        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorPlus10_Asc() { regOBA("RATE_DIFF_COLOR_PLUS10");return this; }
        public BsTDefaultEnvCQ AddOrderBy_RateDiffColorPlus10_Desc() { regOBD("RATE_DIFF_COLOR_PLUS10");return this; }

        protected ConditionValue _graphTypeSa;
        public ConditionValue GraphTypeSa {
            get { if (_graphTypeSa == null) { _graphTypeSa = new ConditionValue(); } return _graphTypeSa; }
        }
        protected override ConditionValue getCValueGraphTypeSa() { return this.GraphTypeSa; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeSa_Asc() { regOBA("GRAPH_TYPE_SA");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeSa_Desc() { regOBD("GRAPH_TYPE_SA");return this; }

        protected ConditionValue _graphTypeSaMatrix;
        public ConditionValue GraphTypeSaMatrix {
            get { if (_graphTypeSaMatrix == null) { _graphTypeSaMatrix = new ConditionValue(); } return _graphTypeSaMatrix; }
        }
        protected override ConditionValue getCValueGraphTypeSaMatrix() { return this.GraphTypeSaMatrix; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeSaMatrix_Asc() { regOBA("GRAPH_TYPE_SA_MATRIX");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeSaMatrix_Desc() { regOBD("GRAPH_TYPE_SA_MATRIX");return this; }

        protected ConditionValue _graphTypeMaSimple;
        public ConditionValue GraphTypeMaSimple {
            get { if (_graphTypeMaSimple == null) { _graphTypeMaSimple = new ConditionValue(); } return _graphTypeMaSimple; }
        }
        protected override ConditionValue getCValueGraphTypeMaSimple() { return this.GraphTypeMaSimple; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeMaSimple_Asc() { regOBA("GRAPH_TYPE_MA_SIMPLE");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeMaSimple_Desc() { regOBD("GRAPH_TYPE_MA_SIMPLE");return this; }

        protected ConditionValue _graphTypeMaCross;
        public ConditionValue GraphTypeMaCross {
            get { if (_graphTypeMaCross == null) { _graphTypeMaCross = new ConditionValue(); } return _graphTypeMaCross; }
        }
        protected override ConditionValue getCValueGraphTypeMaCross() { return this.GraphTypeMaCross; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeMaCross_Asc() { regOBA("GRAPH_TYPE_MA_CROSS");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeMaCross_Desc() { regOBD("GRAPH_TYPE_MA_CROSS");return this; }

        protected ConditionValue _graphTypeMaMatrix;
        public ConditionValue GraphTypeMaMatrix {
            get { if (_graphTypeMaMatrix == null) { _graphTypeMaMatrix = new ConditionValue(); } return _graphTypeMaMatrix; }
        }
        protected override ConditionValue getCValueGraphTypeMaMatrix() { return this.GraphTypeMaMatrix; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeMaMatrix_Asc() { regOBA("GRAPH_TYPE_MA_MATRIX");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeMaMatrix_Desc() { regOBD("GRAPH_TYPE_MA_MATRIX");return this; }

        protected ConditionValue _graphTypeNRate;
        public ConditionValue GraphTypeNRate {
            get { if (_graphTypeNRate == null) { _graphTypeNRate = new ConditionValue(); } return _graphTypeNRate; }
        }
        protected override ConditionValue getCValueGraphTypeNRate() { return this.GraphTypeNRate; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeNRate_Asc() { regOBA("GRAPH_TYPE_N_RATE");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeNRate_Desc() { regOBD("GRAPH_TYPE_N_RATE");return this; }

        protected ConditionValue _graphTypeNRanking;
        public ConditionValue GraphTypeNRanking {
            get { if (_graphTypeNRanking == null) { _graphTypeNRanking = new ConditionValue(); } return _graphTypeNRanking; }
        }
        protected override ConditionValue getCValueGraphTypeNRanking() { return this.GraphTypeNRanking; }


        public BsTDefaultEnvCQ AddOrderBy_GraphTypeNRanking_Asc() { regOBA("GRAPH_TYPE_N_RANKING");return this; }
        public BsTDefaultEnvCQ AddOrderBy_GraphTypeNRanking_Desc() { regOBD("GRAPH_TYPE_N_RANKING");return this; }

        protected ConditionValue _setExecuteFlag;
        public ConditionValue SetExecuteFlag {
            get { if (_setExecuteFlag == null) { _setExecuteFlag = new ConditionValue(); } return _setExecuteFlag; }
        }
        protected override ConditionValue getCValueSetExecuteFlag() { return this.SetExecuteFlag; }


        public BsTDefaultEnvCQ AddOrderBy_SetExecuteFlag_Asc() { regOBA("SET_EXECUTE_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_SetExecuteFlag_Desc() { regOBD("SET_EXECUTE_FLAG");return this; }

        protected ConditionValue _titleAll;
        public ConditionValue TitleAll {
            get { if (_titleAll == null) { _titleAll = new ConditionValue(); } return _titleAll; }
        }
        protected override ConditionValue getCValueTitleAll() { return this.TitleAll; }


        public BsTDefaultEnvCQ AddOrderBy_TitleAll_Asc() { regOBA("TITLE_ALL");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleAll_Desc() { regOBD("TITLE_ALL");return this; }

        protected ConditionValue _titleAxisAll;
        public ConditionValue TitleAxisAll {
            get { if (_titleAxisAll == null) { _titleAxisAll = new ConditionValue(); } return _titleAxisAll; }
        }
        protected override ConditionValue getCValueTitleAxisAll() { return this.TitleAxisAll; }


        public BsTDefaultEnvCQ AddOrderBy_TitleAxisAll_Asc() { regOBA("TITLE_AXIS_ALL");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleAxisAll_Desc() { regOBD("TITLE_AXIS_ALL");return this; }

        protected ConditionValue _titleNoanswer;
        public ConditionValue TitleNoanswer {
            get { if (_titleNoanswer == null) { _titleNoanswer = new ConditionValue(); } return _titleNoanswer; }
        }
        protected override ConditionValue getCValueTitleNoanswer() { return this.TitleNoanswer; }


        public BsTDefaultEnvCQ AddOrderBy_TitleNoanswer_Asc() { regOBA("TITLE_NOANSWER");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleNoanswer_Desc() { regOBD("TITLE_NOANSWER");return this; }

        protected ConditionValue _titleUnfit;
        public ConditionValue TitleUnfit {
            get { if (_titleUnfit == null) { _titleUnfit = new ConditionValue(); } return _titleUnfit; }
        }
        protected override ConditionValue getCValueTitleUnfit() { return this.TitleUnfit; }


        public BsTDefaultEnvCQ AddOrderBy_TitleUnfit_Asc() { regOBA("TITLE_UNFIT");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleUnfit_Desc() { regOBD("TITLE_UNFIT");return this; }

        protected ConditionValue _titleBeforeWb;
        public ConditionValue TitleBeforeWb {
            get { if (_titleBeforeWb == null) { _titleBeforeWb = new ConditionValue(); } return _titleBeforeWb; }
        }
        protected override ConditionValue getCValueTitleBeforeWb() { return this.TitleBeforeWb; }


        public BsTDefaultEnvCQ AddOrderBy_TitleBeforeWb_Asc() { regOBA("TITLE_BEFORE_WB");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleBeforeWb_Desc() { regOBD("TITLE_BEFORE_WB");return this; }

        protected ConditionValue _flagStatisticsParameter;
        public ConditionValue FlagStatisticsParameter {
            get { if (_flagStatisticsParameter == null) { _flagStatisticsParameter = new ConditionValue(); } return _flagStatisticsParameter; }
        }
        protected override ConditionValue getCValueFlagStatisticsParameter() { return this.FlagStatisticsParameter; }


        public BsTDefaultEnvCQ AddOrderBy_FlagStatisticsParameter_Asc() { regOBA("FLAG_STATISTICS_PARAMETER");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagStatisticsParameter_Desc() { regOBD("FLAG_STATISTICS_PARAMETER");return this; }

        protected ConditionValue _titleStatisticsParameter;
        public ConditionValue TitleStatisticsParameter {
            get { if (_titleStatisticsParameter == null) { _titleStatisticsParameter = new ConditionValue(); } return _titleStatisticsParameter; }
        }
        protected override ConditionValue getCValueTitleStatisticsParameter() { return this.TitleStatisticsParameter; }


        public BsTDefaultEnvCQ AddOrderBy_TitleStatisticsParameter_Asc() { regOBA("TITLE_STATISTICS_PARAMETER");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleStatisticsParameter_Desc() { regOBD("TITLE_STATISTICS_PARAMETER");return this; }

        protected ConditionValue _flagTotal;
        public ConditionValue FlagTotal {
            get { if (_flagTotal == null) { _flagTotal = new ConditionValue(); } return _flagTotal; }
        }
        protected override ConditionValue getCValueFlagTotal() { return this.FlagTotal; }


        public BsTDefaultEnvCQ AddOrderBy_FlagTotal_Asc() { regOBA("FLAG_TOTAL");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagTotal_Desc() { regOBD("FLAG_TOTAL");return this; }

        protected ConditionValue _titleTotal;
        public ConditionValue TitleTotal {
            get { if (_titleTotal == null) { _titleTotal = new ConditionValue(); } return _titleTotal; }
        }
        protected override ConditionValue getCValueTitleTotal() { return this.TitleTotal; }


        public BsTDefaultEnvCQ AddOrderBy_TitleTotal_Asc() { regOBA("TITLE_TOTAL");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleTotal_Desc() { regOBD("TITLE_TOTAL");return this; }

        protected ConditionValue _dpSum;
        public ConditionValue DpSum {
            get { if (_dpSum == null) { _dpSum = new ConditionValue(); } return _dpSum; }
        }
        protected override ConditionValue getCValueDpSum() { return this.DpSum; }


        public BsTDefaultEnvCQ AddOrderBy_DpSum_Asc() { regOBA("DP_SUM");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpSum_Desc() { regOBD("DP_SUM");return this; }

        protected ConditionValue _flagAvr;
        public ConditionValue FlagAvr {
            get { if (_flagAvr == null) { _flagAvr = new ConditionValue(); } return _flagAvr; }
        }
        protected override ConditionValue getCValueFlagAvr() { return this.FlagAvr; }


        public BsTDefaultEnvCQ AddOrderBy_FlagAvr_Asc() { regOBA("FLAG_AVR");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagAvr_Desc() { regOBD("FLAG_AVR");return this; }

        protected ConditionValue _titleAvr;
        public ConditionValue TitleAvr {
            get { if (_titleAvr == null) { _titleAvr = new ConditionValue(); } return _titleAvr; }
        }
        protected override ConditionValue getCValueTitleAvr() { return this.TitleAvr; }


        public BsTDefaultEnvCQ AddOrderBy_TitleAvr_Asc() { regOBA("TITLE_AVR");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleAvr_Desc() { regOBD("TITLE_AVR");return this; }

        protected ConditionValue _dpAvr;
        public ConditionValue DpAvr {
            get { if (_dpAvr == null) { _dpAvr = new ConditionValue(); } return _dpAvr; }
        }
        protected override ConditionValue getCValueDpAvr() { return this.DpAvr; }


        public BsTDefaultEnvCQ AddOrderBy_DpAvr_Asc() { regOBA("DP_AVR");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpAvr_Desc() { regOBD("DP_AVR");return this; }

        protected ConditionValue _flagSd;
        public ConditionValue FlagSd {
            get { if (_flagSd == null) { _flagSd = new ConditionValue(); } return _flagSd; }
        }
        protected override ConditionValue getCValueFlagSd() { return this.FlagSd; }


        public BsTDefaultEnvCQ AddOrderBy_FlagSd_Asc() { regOBA("FLAG_SD");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagSd_Desc() { regOBD("FLAG_SD");return this; }

        protected ConditionValue _titleSd;
        public ConditionValue TitleSd {
            get { if (_titleSd == null) { _titleSd = new ConditionValue(); } return _titleSd; }
        }
        protected override ConditionValue getCValueTitleSd() { return this.TitleSd; }


        public BsTDefaultEnvCQ AddOrderBy_TitleSd_Asc() { regOBA("TITLE_SD");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleSd_Desc() { regOBD("TITLE_SD");return this; }

        protected ConditionValue _dpSd;
        public ConditionValue DpSd {
            get { if (_dpSd == null) { _dpSd = new ConditionValue(); } return _dpSd; }
        }
        protected override ConditionValue getCValueDpSd() { return this.DpSd; }


        public BsTDefaultEnvCQ AddOrderBy_DpSd_Asc() { regOBA("DP_SD");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpSd_Desc() { regOBD("DP_SD");return this; }

        protected ConditionValue _flagMin;
        public ConditionValue FlagMin {
            get { if (_flagMin == null) { _flagMin = new ConditionValue(); } return _flagMin; }
        }
        protected override ConditionValue getCValueFlagMin() { return this.FlagMin; }


        public BsTDefaultEnvCQ AddOrderBy_FlagMin_Asc() { regOBA("FLAG_MIN");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagMin_Desc() { regOBD("FLAG_MIN");return this; }

        protected ConditionValue _titleMin;
        public ConditionValue TitleMin {
            get { if (_titleMin == null) { _titleMin = new ConditionValue(); } return _titleMin; }
        }
        protected override ConditionValue getCValueTitleMin() { return this.TitleMin; }


        public BsTDefaultEnvCQ AddOrderBy_TitleMin_Asc() { regOBA("TITLE_MIN");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleMin_Desc() { regOBD("TITLE_MIN");return this; }

        protected ConditionValue _dpMin;
        public ConditionValue DpMin {
            get { if (_dpMin == null) { _dpMin = new ConditionValue(); } return _dpMin; }
        }
        protected override ConditionValue getCValueDpMin() { return this.DpMin; }


        public BsTDefaultEnvCQ AddOrderBy_DpMin_Asc() { regOBA("DP_MIN");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpMin_Desc() { regOBD("DP_MIN");return this; }

        protected ConditionValue _flagMax;
        public ConditionValue FlagMax {
            get { if (_flagMax == null) { _flagMax = new ConditionValue(); } return _flagMax; }
        }
        protected override ConditionValue getCValueFlagMax() { return this.FlagMax; }


        public BsTDefaultEnvCQ AddOrderBy_FlagMax_Asc() { regOBA("FLAG_MAX");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagMax_Desc() { regOBD("FLAG_MAX");return this; }

        protected ConditionValue _titleMax;
        public ConditionValue TitleMax {
            get { if (_titleMax == null) { _titleMax = new ConditionValue(); } return _titleMax; }
        }
        protected override ConditionValue getCValueTitleMax() { return this.TitleMax; }


        public BsTDefaultEnvCQ AddOrderBy_TitleMax_Asc() { regOBA("TITLE_MAX");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleMax_Desc() { regOBD("TITLE_MAX");return this; }

        protected ConditionValue _dpMax;
        public ConditionValue DpMax {
            get { if (_dpMax == null) { _dpMax = new ConditionValue(); } return _dpMax; }
        }
        protected override ConditionValue getCValueDpMax() { return this.DpMax; }


        public BsTDefaultEnvCQ AddOrderBy_DpMax_Asc() { regOBA("DP_MAX");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpMax_Desc() { regOBD("DP_MAX");return this; }

        protected ConditionValue _flagMedian;
        public ConditionValue FlagMedian {
            get { if (_flagMedian == null) { _flagMedian = new ConditionValue(); } return _flagMedian; }
        }
        protected override ConditionValue getCValueFlagMedian() { return this.FlagMedian; }


        public BsTDefaultEnvCQ AddOrderBy_FlagMedian_Asc() { regOBA("FLAG_MEDIAN");return this; }
        public BsTDefaultEnvCQ AddOrderBy_FlagMedian_Desc() { regOBD("FLAG_MEDIAN");return this; }

        protected ConditionValue _titleMedian;
        public ConditionValue TitleMedian {
            get { if (_titleMedian == null) { _titleMedian = new ConditionValue(); } return _titleMedian; }
        }
        protected override ConditionValue getCValueTitleMedian() { return this.TitleMedian; }


        public BsTDefaultEnvCQ AddOrderBy_TitleMedian_Asc() { regOBA("TITLE_MEDIAN");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TitleMedian_Desc() { regOBD("TITLE_MEDIAN");return this; }

        protected ConditionValue _dpMedian;
        public ConditionValue DpMedian {
            get { if (_dpMedian == null) { _dpMedian = new ConditionValue(); } return _dpMedian; }
        }
        protected override ConditionValue getCValueDpMedian() { return this.DpMedian; }


        public BsTDefaultEnvCQ AddOrderBy_DpMedian_Asc() { regOBA("DP_MEDIAN");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpMedian_Desc() { regOBD("DP_MEDIAN");return this; }

        protected ConditionValue _dpWeight;
        public ConditionValue DpWeight {
            get { if (_dpWeight == null) { _dpWeight = new ConditionValue(); } return _dpWeight; }
        }
        protected override ConditionValue getCValueDpWeight() { return this.DpWeight; }


        public BsTDefaultEnvCQ AddOrderBy_DpWeight_Asc() { regOBA("DP_WEIGHT");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpWeight_Desc() { regOBD("DP_WEIGHT");return this; }

        protected ConditionValue _dpWeightAvr;
        public ConditionValue DpWeightAvr {
            get { if (_dpWeightAvr == null) { _dpWeightAvr = new ConditionValue(); } return _dpWeightAvr; }
        }
        protected override ConditionValue getCValueDpWeightAvr() { return this.DpWeightAvr; }


        public BsTDefaultEnvCQ AddOrderBy_DpWeightAvr_Asc() { regOBA("DP_WEIGHT_AVR");return this; }
        public BsTDefaultEnvCQ AddOrderBy_DpWeightAvr_Desc() { regOBD("DP_WEIGHT_AVR");return this; }

        protected ConditionValue _excelType;
        public ConditionValue ExcelType {
            get { if (_excelType == null) { _excelType = new ConditionValue(); } return _excelType; }
        }
        protected override ConditionValue getCValueExcelType() { return this.ExcelType; }


        public BsTDefaultEnvCQ AddOrderBy_ExcelType_Asc() { regOBA("EXCEL_TYPE");return this; }
        public BsTDefaultEnvCQ AddOrderBy_ExcelType_Desc() { regOBD("EXCEL_TYPE");return this; }

        protected ConditionValue _ppType;
        public ConditionValue PpType {
            get { if (_ppType == null) { _ppType = new ConditionValue(); } return _ppType; }
        }
        protected override ConditionValue getCValuePpType() { return this.PpType; }


        public BsTDefaultEnvCQ AddOrderBy_PpType_Asc() { regOBA("PP_TYPE");return this; }
        public BsTDefaultEnvCQ AddOrderBy_PpType_Desc() { regOBD("PP_TYPE");return this; }

        protected ConditionValue _lastUpdateUser;
        public ConditionValue LastUpdateUser {
            get { if (_lastUpdateUser == null) { _lastUpdateUser = new ConditionValue(); } return _lastUpdateUser; }
        }
        protected override ConditionValue getCValueLastUpdateUser() { return this.LastUpdateUser; }


        public BsTDefaultEnvCQ AddOrderBy_LastUpdateUser_Asc() { regOBA("LAST_UPDATE_USER");return this; }
        public BsTDefaultEnvCQ AddOrderBy_LastUpdateUser_Desc() { regOBD("LAST_UPDATE_USER");return this; }

        protected ConditionValue _lastUpdateDatetime;
        public ConditionValue LastUpdateDatetime {
            get { if (_lastUpdateDatetime == null) { _lastUpdateDatetime = new ConditionValue(); } return _lastUpdateDatetime; }
        }
        protected override ConditionValue getCValueLastUpdateDatetime() { return this.LastUpdateDatetime; }


        public BsTDefaultEnvCQ AddOrderBy_LastUpdateDatetime_Asc() { regOBA("LAST_UPDATE_DATETIME");return this; }
        public BsTDefaultEnvCQ AddOrderBy_LastUpdateDatetime_Desc() { regOBD("LAST_UPDATE_DATETIME");return this; }

        protected ConditionValue _testGtFlag;
        public ConditionValue TestGtFlag {
            get { if (_testGtFlag == null) { _testGtFlag = new ConditionValue(); } return _testGtFlag; }
        }
        protected override ConditionValue getCValueTestGtFlag() { return this.TestGtFlag; }


        public BsTDefaultEnvCQ AddOrderBy_TestGtFlag_Asc() { regOBA("TEST_GT_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TestGtFlag_Desc() { regOBD("TEST_GT_FLAG");return this; }

        protected ConditionValue _testCrossFlag;
        public ConditionValue TestCrossFlag {
            get { if (_testCrossFlag == null) { _testCrossFlag = new ConditionValue(); } return _testCrossFlag; }
        }
        protected override ConditionValue getCValueTestCrossFlag() { return this.TestCrossFlag; }


        public BsTDefaultEnvCQ AddOrderBy_TestCrossFlag_Asc() { regOBA("TEST_CROSS_FLAG");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TestCrossFlag_Desc() { regOBD("TEST_CROSS_FLAG");return this; }

        protected ConditionValue _testTypeGt;
        public ConditionValue TestTypeGt {
            get { if (_testTypeGt == null) { _testTypeGt = new ConditionValue(); } return _testTypeGt; }
        }
        protected override ConditionValue getCValueTestTypeGt() { return this.TestTypeGt; }


        public BsTDefaultEnvCQ AddOrderBy_TestTypeGt_Asc() { regOBA("TEST_TYPE_GT");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TestTypeGt_Desc() { regOBD("TEST_TYPE_GT");return this; }

        protected ConditionValue _testTypeCross;
        public ConditionValue TestTypeCross {
            get { if (_testTypeCross == null) { _testTypeCross = new ConditionValue(); } return _testTypeCross; }
        }
        protected override ConditionValue getCValueTestTypeCross() { return this.TestTypeCross; }


        public BsTDefaultEnvCQ AddOrderBy_TestTypeCross_Asc() { regOBA("TEST_TYPE_CROSS");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TestTypeCross_Desc() { regOBD("TEST_TYPE_CROSS");return this; }

        protected ConditionValue _testSignificanceLvGt;
        public ConditionValue TestSignificanceLvGt {
            get { if (_testSignificanceLvGt == null) { _testSignificanceLvGt = new ConditionValue(); } return _testSignificanceLvGt; }
        }
        protected override ConditionValue getCValueTestSignificanceLvGt() { return this.TestSignificanceLvGt; }


        public BsTDefaultEnvCQ AddOrderBy_TestSignificanceLvGt_Asc() { regOBA("TEST_SIGNIFICANCE_LV_GT");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TestSignificanceLvGt_Desc() { regOBD("TEST_SIGNIFICANCE_LV_GT");return this; }

        protected ConditionValue _testSignificanceLvCross;
        public ConditionValue TestSignificanceLvCross {
            get { if (_testSignificanceLvCross == null) { _testSignificanceLvCross = new ConditionValue(); } return _testSignificanceLvCross; }
        }
        protected override ConditionValue getCValueTestSignificanceLvCross() { return this.TestSignificanceLvCross; }


        public BsTDefaultEnvCQ AddOrderBy_TestSignificanceLvCross_Asc() { regOBA("TEST_SIGNIFICANCE_LV_CROSS");return this; }
        public BsTDefaultEnvCQ AddOrderBy_TestSignificanceLvCross_Desc() { regOBD("TEST_SIGNIFICANCE_LV_CROSS");return this; }

        public BsTDefaultEnvCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTDefaultEnvCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            TDefaultEnvCQ baseQuery = (TDefaultEnvCQ)baseQueryAsSuper;
            TDefaultEnvCQ unionQuery = (TDefaultEnvCQ)unionQueryAsSuper;
            if (baseQuery.hasConditionQueryTQcwebSurveyInfoAsOne()) {
                unionQuery.QueryTQcwebSurveyInfoAsOne().reflectRelationOnUnionQuery(baseQuery.QueryTQcwebSurveyInfoAsOne(), unionQuery.QueryTQcwebSurveyInfoAsOne());
            }

        }
    


        protected TQcwebSurveyInfoCQ _conditionQueryTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoCQ ConditionQueryTQcwebSurveyInfoAsOne {
            get {
                if (_conditionQueryTQcwebSurveyInfoAsOne == null) {
                    _conditionQueryTQcwebSurveyInfoAsOne = createQueryTQcwebSurveyInfoAsOne();
                    xsetupOuterJoin_TQcwebSurveyInfoAsOne();
                }
                return _conditionQueryTQcwebSurveyInfoAsOne;
            }
        }
        public TQcwebSurveyInfoCQ QueryTQcwebSurveyInfoAsOne() { return this.ConditionQueryTQcwebSurveyInfoAsOne; }
        protected TQcwebSurveyInfoCQ createQueryTQcwebSurveyInfoAsOne() {
            String nrp = resolveNextRelationPathTQcwebSurveyInfoAsOne();
            String jan = resolveJoinAliasName(nrp, xgetNextNestLevel());
            TQcwebSurveyInfoCQ cq = new TQcwebSurveyInfoCQ(this, xgetSqlClause(), jan, xgetNextNestLevel());
            cq.xsetForeignPropertyName("tQcwebSurveyInfoAsOne"); cq.xsetRelationPath(nrp); return cq;
        }
        public void xsetupOuterJoin_TQcwebSurveyInfoAsOne() {
            TQcwebSurveyInfoCQ cq = ConditionQueryTQcwebSurveyInfoAsOne;
            Map<String, String> joinOnMap = new LinkedHashMap<String, String>();
            joinOnMap.put("QCWEBID", "QCWebID");
            registerOuterJoin(cq, joinOnMap);
        }
        protected String resolveNextRelationPathTQcwebSurveyInfoAsOne() {
            return resolveNextRelationPath("T_DEFAULT_ENV", "tQcwebSurveyInfoAsOne");
        }
        public bool hasConditionQueryTQcwebSurveyInfoAsOne() {
            return _conditionQueryTQcwebSurveyInfoAsOne != null;
        }

	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TDefaultEnvCQ> _scalarSubQueryMap;
	    public Map<String, TDefaultEnvCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TDefaultEnvCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TDefaultEnvCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TDefaultEnvCQ> _myselfInScopeSubQueryMap;
        public Map<String, TDefaultEnvCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TDefaultEnvCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TDefaultEnvCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
