
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
    public class TScenarioTotalizationCIQ : AbstractBsTScenarioTotalizationCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTScenarioTotalizationCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TScenarioTotalizationCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTScenarioTotalizationCQ myCQ)
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


        protected override ConditionValue getCValueScenarioTotalizationId() {
            return _myCQ.ScenarioTotalizationId;
        }


        public override String keepScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList(subQuery);
        }

        public override String keepScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList(subQuery);
        }

        public override String keepScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList(subQuery);
        }

        public override String keepScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepScenarioTotalizationId_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_ExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepScenarioTotalizationId_NotExistsSubQuery_TItemInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepScenarioTotalizationId_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TItemInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList(subQuery);
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueQcwebid() {
            return _myCQ.Qcwebid;
        }


        public override String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        public override String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery) {
            return _myCQ.keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(subQuery);
        }

        protected override ConditionValue getCValueScenarioType() {
            return _myCQ.ScenarioType;
        }


        protected override ConditionValue getCValueScenarioName() {
            return _myCQ.ScenarioName;
        }


        protected override ConditionValue getCValueConditionDiv() {
            return _myCQ.ConditionDiv;
        }


        protected override ConditionValue getCValueFilterFlag() {
            return _myCQ.FilterFlag;
        }


        protected override ConditionValue getCValueSortNo() {
            return _myCQ.SortNo;
        }


        protected override ConditionValue getCValueWeightbackFlag() {
            return _myCQ.WeightbackFlag;
        }


        protected override ConditionValue getCValueWeightbackCode() {
            return _myCQ.WeightbackCode;
        }


        protected override ConditionValue getCValueTotalnumFlag() {
            return _myCQ.TotalnumFlag;
        }


        protected override ConditionValue getCValueGraphOutputFlag() {
            return _myCQ.GraphOutputFlag;
        }


        protected override ConditionValue getCValuePieChartChoiceFlag() {
            return _myCQ.PieChartChoiceFlag;
        }


        protected override ConditionValue getCValueMinimumRate() {
            return _myCQ.MinimumRate;
        }


        protected override ConditionValue getCValueAxisNoanswerOnoff() {
            return _myCQ.AxisNoanswerOnoff;
        }


        protected override ConditionValue getCValueTargetNoanswerOnoff() {
            return _myCQ.TargetNoanswerOnoff;
        }


        protected override ConditionValue getCValuePolylineOnoff() {
            return _myCQ.PolylineOnoff;
        }


        protected override ConditionValue getCValueMarkingN() {
            return _myCQ.MarkingN;
        }


        protected override ConditionValue getCValueRankingFlag() {
            return _myCQ.RankingFlag;
        }


        protected override ConditionValue getCValueRateFlag() {
            return _myCQ.RateFlag;
        }


        protected override ConditionValue getCValueRate1Flag() {
            return _myCQ.Rate1Flag;
        }


        protected override ConditionValue getCValueRate1Sign() {
            return _myCQ.Rate1Sign;
        }


        protected override ConditionValue getCValueRate1Range() {
            return _myCQ.Rate1Range;
        }


        protected override ConditionValue getCValueRate1Backcolor1() {
            return _myCQ.Rate1Backcolor1;
        }


        protected override ConditionValue getCValueRate1Backcolor2() {
            return _myCQ.Rate1Backcolor2;
        }


        protected override ConditionValue getCValueRate2Flag() {
            return _myCQ.Rate2Flag;
        }


        protected override ConditionValue getCValueRate2Sign() {
            return _myCQ.Rate2Sign;
        }


        protected override ConditionValue getCValueRate2Range() {
            return _myCQ.Rate2Range;
        }


        protected override ConditionValue getCValueRate2Backcolor1() {
            return _myCQ.Rate2Backcolor1;
        }


        protected override ConditionValue getCValueRate2Backcolor2() {
            return _myCQ.Rate2Backcolor2;
        }


        protected override ConditionValue getCValueLastUpdateUser() {
            return _myCQ.LastUpdateUser;
        }


        protected override ConditionValue getCValueLastUpdateDatetime() {
            return _myCQ.LastUpdateDatetime;
        }


        protected override ConditionValue getCValueTestFlag() {
            return _myCQ.TestFlag;
        }


        protected override ConditionValue getCValueTestType() {
            return _myCQ.TestType;
        }


        protected override ConditionValue getCValueTestSignificanceLv() {
            return _myCQ.TestSignificanceLv;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TScenarioTotalizationCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TScenarioTotalizationCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
