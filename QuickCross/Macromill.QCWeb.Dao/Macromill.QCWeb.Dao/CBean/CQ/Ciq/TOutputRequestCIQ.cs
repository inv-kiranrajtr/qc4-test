
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
    public class TOutputRequestCIQ : AbstractBsTOutputRequestCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputRequestCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputRequestCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputRequestCQ myCQ)
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


        protected override ConditionValue getCValueOutputRequestId() {
            return _myCQ.OutputRequestId;
        }


        public override String keepOutputRequestId_ExistsSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputRequestId_ExistsSubQuery_TOutputCommonList(subQuery);
        }

        public override String keepOutputRequestId_NotExistsSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputRequestId_NotExistsSubQuery_TOutputCommonList(subQuery);
        }

        public override String keepOutputRequestId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            return _myCQ.keepOutputRequestId_InScopeSubQuery_TOutputCommon(subQuery);
        }

        public override String keepOutputRequestId_InScopeSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            return _myCQ.keepOutputRequestId_InScopeSubQuery_TOutputCommonList(subQuery);
        }

        public override String keepOutputRequestId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery) {
            return _myCQ.keepOutputRequestId_NotInScopeSubQuery_TOutputCommon(subQuery);
        }

        public override String keepOutputRequestId_NotInScopeSubQuery_TOutputCommonList(TOutputCommonCQ subQuery) {
            return _myCQ.keepOutputRequestId_NotInScopeSubQuery_TOutputCommonList(subQuery);
        }
        public override String keepOutputRequestId_SpecifyDerivedReferrer_TOutputCommonList(TOutputCommonCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputRequestId_QueryDerivedReferrer_TOutputCommonList(TOutputCommonCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputRequestId_QueryDerivedReferrer_TOutputCommonListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueRequestServerCode() {
            return _myCQ.RequestServerCode;
        }


        protected override ConditionValue getCValueRequestUserId() {
            return _myCQ.RequestUserId;
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

        protected override ConditionValue getCValueLastDownloadUserid() {
            return _myCQ.LastDownloadUserid;
        }


        protected override ConditionValue getCValueRequestDatetime() {
            return _myCQ.RequestDatetime;
        }


        protected override ConditionValue getCValueDownloadPath() {
            return _myCQ.DownloadPath;
        }


        protected override ConditionValue getCValueProcServerCode() {
            return _myCQ.ProcServerCode;
        }


        protected override ConditionValue getCValueStatusCode() {
            return _myCQ.StatusCode;
        }


        protected override ConditionValue getCValueDescription() {
            return _myCQ.Description;
        }


        protected override ConditionValue getCValueEndDatetime() {
            return _myCQ.EndDatetime;
        }


        protected override ConditionValue getCValueLastDownloadDatetime() {
            return _myCQ.LastDownloadDatetime;
        }


        protected override ConditionValue getCValueExcelbookType() {
            return _myCQ.ExcelbookType;
        }


        protected override ConditionValue getCValueNumericAnswerViewCode() {
            return _myCQ.NumericAnswerViewCode;
        }


        protected override ConditionValue getCValueDpTotal() {
            return _myCQ.DpTotal;
        }


        protected override ConditionValue getCValueDpAverage() {
            return _myCQ.DpAverage;
        }


        protected override ConditionValue getCValueDpStandardDiv() {
            return _myCQ.DpStandardDiv;
        }


        protected override ConditionValue getCValueDpMin() {
            return _myCQ.DpMin;
        }


        protected override ConditionValue getCValueDpMax() {
            return _myCQ.DpMax;
        }


        protected override ConditionValue getCValueDpMedian() {
            return _myCQ.DpMedian;
        }


        protected override ConditionValue getCValueDpWeight() {
            return _myCQ.DpWeight;
        }


        protected override ConditionValue getCValueDpWeightavr() {
            return _myCQ.DpWeightavr;
        }


        protected override ConditionValue getCValueProcWeight() {
            return _myCQ.ProcWeight;
        }


        protected override ConditionValue getCValueOutputReportsetInfoId() {
            return _myCQ.OutputReportsetInfoId;
        }


        public override String keepOutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo(TOutputReportsetInfoCQ subQuery) {
            return _myCQ.keepOutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo(subQuery);
        }

        public override String keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo(TOutputReportsetInfoCQ subQuery) {
            return _myCQ.keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo(subQuery);
        }

        protected override ConditionValue getCValueDeleteFlag() {
            return _myCQ.DeleteFlag;
        }


        protected override ConditionValue getCValueViewSurveyName() {
            return _myCQ.ViewSurveyName;
        }


        protected override ConditionValue getCValueLanguage() {
            return _myCQ.Language;
        }


        protected override ConditionValue getCValueShowZeroNaIvCode() {
            return _myCQ.ShowZeroNaIvCode;
        }


        protected override ConditionValue getCValueMergeAxisCellsFlag() {
            return _myCQ.MergeAxisCellsFlag;
        }


        protected override ConditionValue getCValueScenarioName() {
            return _myCQ.ScenarioName;
        }


        protected override ConditionValue getCValueStartDatetime() {
            return _myCQ.StartDatetime;
        }


        protected override ConditionValue getCValueTestLogFlag() {
            return _myCQ.TestLogFlag;
        }


        protected override ConditionValue getCValueTsvFileSizeGt() {
            return _myCQ.TsvFileSizeGt;
        }


        protected override ConditionValue getCValueTsvFileSizeCross() {
            return _myCQ.TsvFileSizeCross;
        }


        protected override ConditionValue getCValueTsvFileSizeFa() {
            return _myCQ.TsvFileSizeFa;
        }


        protected override ConditionValue getCValueTsvFileSizeDataOutput() {
            return _myCQ.TsvFileSizeDataOutput;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputRequestCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputRequestCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
