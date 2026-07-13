
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CKey;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ.BS;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.CQ.Ciq {

    [System.Serializable]
    public class TDefaultEnvCIQ : AbstractBsTDefaultEnvCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTDefaultEnvCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTDefaultEnvCQ myCQ)
            : base(childQuery, sqlClause, aliasName, nestLevel) {
            _myCQ = myCQ;
            _foreignPropertyName = _myCQ.xgetForeignPropertyName();// Accept foreign property name.
            _relationPath = _myCQ.xgetRelationPath();// Accept relation path.
        }

        // ===================================================================================
        //                                                             Override about Register
        //                                                             =======================
        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            throw new UnsupportedOperationException("InlineQuery must not need UNION method: " + baseQueryAsSuper + " : " + unionQueryAsSuper);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue, String colName) {
            regIQ(key, value, cvalue, colName);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue
                                                                        , String colName, ConditionOption option) {
            regIQ(key, value, cvalue, colName, option);
        }
    
        protected override void registerWhereClause(String whereClause) {
            registerInlineWhereClause(whereClause);
        }
    
        protected override String getInScopeSubQueryRealColumnName(String columnName) {
            if (_onClause) {
                throw new UnsupportedOperationException("InScopeSubQuery of on-clause is unsupported");
            }
            return _onClause ? xgetAliasName() + "." + columnName : columnName;
        }
    
        protected override void registerExistsSubQuery(ConditionQuery subQuery
                                     , String columnName, String relatedColumnName, String propertyName) {
            throw new UnsupportedOperationException("Sorry! ExistsSubQuery at inline view is unsupported. So please use InScopeSubQyery.");
        }


        protected override ConditionValue getCValueQcwebid() {
            return _myCQ.Qcwebid;
        }


        public override String keepQcwebid_ExistsSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TDefaultEnvColorInfoList(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TDefaultEnvColorInfoList(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(subQuery);
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueNoanswerDenominatorFlag() {
            return _myCQ.NoanswerDenominatorFlag;
        }


        protected override ConditionValue getCValueVisibleUnfitFlag() {
            return _myCQ.VisibleUnfitFlag;
        }


        protected override ConditionValue getCValueNoanswerUnfitFlag() {
            return _myCQ.NoanswerUnfitFlag;
        }


        protected override ConditionValue getCValueWeightbackFlag() {
            return _myCQ.WeightbackFlag;
        }


        protected override ConditionValue getCValueCellJoincellJoinFlag() {
            return _myCQ.CellJoincellJoinFlag;
        }


        protected override ConditionValue getCValueChartDirectionGtFlag() {
            return _myCQ.ChartDirectionGtFlag;
        }


        protected override ConditionValue getCValueChartDirectionCrossFlag() {
            return _myCQ.ChartDirectionCrossFlag;
        }


        protected override ConditionValue getCValueNoanswerTargetFlag() {
            return _myCQ.NoanswerTargetFlag;
        }


        protected override ConditionValue getCValueNoanswerAxisFlag() {
            return _myCQ.NoanswerAxisFlag;
        }


        protected override ConditionValue getCValueUnfitTargetFlag() {
            return _myCQ.UnfitTargetFlag;
        }


        protected override ConditionValue getCValueUnfitAxisFlag() {
            return _myCQ.UnfitAxisFlag;
        }


        protected override ConditionValue getCValueTotalnumFlag() {
            return _myCQ.TotalnumFlag;
        }


        protected override ConditionValue getCValueRateDiffColorMinus5() {
            return _myCQ.RateDiffColorMinus5;
        }


        protected override ConditionValue getCValueRateDiffColorMinus10() {
            return _myCQ.RateDiffColorMinus10;
        }


        protected override ConditionValue getCValueRateDiffColorPlus5() {
            return _myCQ.RateDiffColorPlus5;
        }


        protected override ConditionValue getCValueRateDiffColorPlus10() {
            return _myCQ.RateDiffColorPlus10;
        }


        protected override ConditionValue getCValueGraphTypeSa() {
            return _myCQ.GraphTypeSa;
        }


        protected override ConditionValue getCValueGraphTypeSaMatrix() {
            return _myCQ.GraphTypeSaMatrix;
        }


        protected override ConditionValue getCValueGraphTypeMaSimple() {
            return _myCQ.GraphTypeMaSimple;
        }


        protected override ConditionValue getCValueGraphTypeMaCross() {
            return _myCQ.GraphTypeMaCross;
        }


        protected override ConditionValue getCValueGraphTypeMaMatrix() {
            return _myCQ.GraphTypeMaMatrix;
        }


        protected override ConditionValue getCValueGraphTypeNRate() {
            return _myCQ.GraphTypeNRate;
        }


        protected override ConditionValue getCValueGraphTypeNRanking() {
            return _myCQ.GraphTypeNRanking;
        }


        protected override ConditionValue getCValueSetExecuteFlag() {
            return _myCQ.SetExecuteFlag;
        }


        protected override ConditionValue getCValueTitleAll() {
            return _myCQ.TitleAll;
        }


        protected override ConditionValue getCValueTitleAxisAll() {
            return _myCQ.TitleAxisAll;
        }


        protected override ConditionValue getCValueTitleNoanswer() {
            return _myCQ.TitleNoanswer;
        }


        protected override ConditionValue getCValueTitleUnfit() {
            return _myCQ.TitleUnfit;
        }


        protected override ConditionValue getCValueTitleBeforeWb() {
            return _myCQ.TitleBeforeWb;
        }


        protected override ConditionValue getCValueFlagStatisticsParameter() {
            return _myCQ.FlagStatisticsParameter;
        }


        protected override ConditionValue getCValueTitleStatisticsParameter() {
            return _myCQ.TitleStatisticsParameter;
        }


        protected override ConditionValue getCValueFlagTotal() {
            return _myCQ.FlagTotal;
        }


        protected override ConditionValue getCValueTitleTotal() {
            return _myCQ.TitleTotal;
        }


        protected override ConditionValue getCValueDpSum() {
            return _myCQ.DpSum;
        }


        protected override ConditionValue getCValueFlagAvr() {
            return _myCQ.FlagAvr;
        }


        protected override ConditionValue getCValueTitleAvr() {
            return _myCQ.TitleAvr;
        }


        protected override ConditionValue getCValueDpAvr() {
            return _myCQ.DpAvr;
        }


        protected override ConditionValue getCValueFlagSd() {
            return _myCQ.FlagSd;
        }


        protected override ConditionValue getCValueTitleSd() {
            return _myCQ.TitleSd;
        }


        protected override ConditionValue getCValueDpSd() {
            return _myCQ.DpSd;
        }


        protected override ConditionValue getCValueFlagMin() {
            return _myCQ.FlagMin;
        }


        protected override ConditionValue getCValueTitleMin() {
            return _myCQ.TitleMin;
        }


        protected override ConditionValue getCValueDpMin() {
            return _myCQ.DpMin;
        }


        protected override ConditionValue getCValueFlagMax() {
            return _myCQ.FlagMax;
        }


        protected override ConditionValue getCValueTitleMax() {
            return _myCQ.TitleMax;
        }


        protected override ConditionValue getCValueDpMax() {
            return _myCQ.DpMax;
        }


        protected override ConditionValue getCValueFlagMedian() {
            return _myCQ.FlagMedian;
        }


        protected override ConditionValue getCValueTitleMedian() {
            return _myCQ.TitleMedian;
        }


        protected override ConditionValue getCValueDpMedian() {
            return _myCQ.DpMedian;
        }


        protected override ConditionValue getCValueDpWeight() {
            return _myCQ.DpWeight;
        }


        protected override ConditionValue getCValueDpWeightAvr() {
            return _myCQ.DpWeightAvr;
        }


        protected override ConditionValue getCValueExcelType() {
            return _myCQ.ExcelType;
        }


        protected override ConditionValue getCValuePpType() {
            return _myCQ.PpType;
        }


        protected override ConditionValue getCValueLastUpdateUser() {
            return _myCQ.LastUpdateUser;
        }


        protected override ConditionValue getCValueLastUpdateDatetime() {
            return _myCQ.LastUpdateDatetime;
        }


        protected override ConditionValue getCValueTestGtFlag() {
            return _myCQ.TestGtFlag;
        }


        protected override ConditionValue getCValueTestCrossFlag() {
            return _myCQ.TestCrossFlag;
        }


        protected override ConditionValue getCValueTestTypeGt() {
            return _myCQ.TestTypeGt;
        }


        protected override ConditionValue getCValueTestTypeCross() {
            return _myCQ.TestTypeCross;
        }


        protected override ConditionValue getCValueTestSignificanceLvGt() {
            return _myCQ.TestSignificanceLvGt;
        }


        protected override ConditionValue getCValueTestSignificanceLvCross() {
            return _myCQ.TestSignificanceLvCross;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TDefaultEnvCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TDefaultEnvCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
