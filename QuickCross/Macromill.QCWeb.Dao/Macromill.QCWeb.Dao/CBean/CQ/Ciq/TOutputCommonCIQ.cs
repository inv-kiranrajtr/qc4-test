
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
    public class TOutputCommonCIQ : AbstractBsTOutputCommonCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTOutputCommonCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputCommonCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTOutputCommonCQ myCQ)
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


        protected override ConditionValue getCValueOutputCommonId() {
            return _myCQ.OutputCommonId;
        }


        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_ExistsSubQuery_TOutputSubCklistList(subQuery);
        }

        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_ExistsSubQuery_TOutputSubCrossList(subQuery);
        }

        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_ExistsSubQuery_TOutputSubFaList(subQuery);
        }

        public override String keepOutputCommonId_ExistsSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_ExistsSubQuery_TOutputSubGtList(subQuery);
        }

        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_NotExistsSubQuery_TOutputSubCklistList(subQuery);
        }

        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_NotExistsSubQuery_TOutputSubCrossList(subQuery);
        }

        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_NotExistsSubQuery_TOutputSubFaList(subQuery);
        }

        public override String keepOutputCommonId_NotExistsSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepOutputCommonId_NotExistsSubQuery_TOutputSubGtList(subQuery);
        }

        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubGt(TOutputSubGtCQ subQuery) {
            return _myCQ.keepOutputCommonId_InScopeSubQuery_TOutputSubGt(subQuery);
        }

        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            return _myCQ.keepOutputCommonId_InScopeSubQuery_TOutputSubCklistList(subQuery);
        }

        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            return _myCQ.keepOutputCommonId_InScopeSubQuery_TOutputSubCrossList(subQuery);
        }

        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            return _myCQ.keepOutputCommonId_InScopeSubQuery_TOutputSubFaList(subQuery);
        }

        public override String keepOutputCommonId_InScopeSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            return _myCQ.keepOutputCommonId_InScopeSubQuery_TOutputSubGtList(subQuery);
        }

        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubGt(TOutputSubGtCQ subQuery) {
            return _myCQ.keepOutputCommonId_NotInScopeSubQuery_TOutputSubGt(subQuery);
        }

        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            return _myCQ.keepOutputCommonId_NotInScopeSubQuery_TOutputSubCklistList(subQuery);
        }

        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            return _myCQ.keepOutputCommonId_NotInScopeSubQuery_TOutputSubCrossList(subQuery);
        }

        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            return _myCQ.keepOutputCommonId_NotInScopeSubQuery_TOutputSubFaList(subQuery);
        }

        public override String keepOutputCommonId_NotInScopeSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            return _myCQ.keepOutputCommonId_NotInScopeSubQuery_TOutputSubGtList(subQuery);
        }
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistList(TOutputSubCklistCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossList(TOutputSubCrossCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaList(TOutputSubFaCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtList(TOutputSubGtCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueOrderCount() {
            return _myCQ.OrderCount;
        }


        protected override ConditionValue getCValueTsvFilePath() {
            return _myCQ.TsvFilePath;
        }


        protected override ConditionValue getCValueExcelbookNamePrefix() {
            return _myCQ.ExcelbookNamePrefix;
        }


        protected override ConditionValue getCValueProcessStartDatetime() {
            return _myCQ.ProcessStartDatetime;
        }


        protected override ConditionValue getCValueProcessForecastEndDatetime() {
            return _myCQ.ProcessForecastEndDatetime;
        }


        protected override ConditionValue getCValueProcessEndDatetime() {
            return _myCQ.ProcessEndDatetime;
        }


        protected override ConditionValue getCValueStatusCode() {
            return _myCQ.StatusCode;
        }


        protected override ConditionValue getCValueDescription() {
            return _myCQ.Description;
        }


        protected override ConditionValue getCValueOutputType() {
            return _myCQ.OutputType;
        }


        protected override ConditionValue getCValueOutputRequestId() {
            return _myCQ.OutputRequestId;
        }


        public override String keepOutputRequestId_InScopeSubQuery_TOutputRequest(TOutputRequestCQ subQuery) {
            return _myCQ.keepOutputRequestId_InScopeSubQuery_TOutputRequest(subQuery);
        }

        public override String keepOutputRequestId_NotInScopeSubQuery_TOutputRequest(TOutputRequestCQ subQuery) {
            return _myCQ.keepOutputRequestId_NotInScopeSubQuery_TOutputRequest(subQuery);
        }

        protected override ConditionValue getCValueWbSettingCode() {
            return _myCQ.WbSettingCode;
        }


        protected override ConditionValue getCValueNoanswerVisibleCode() {
            return _myCQ.NoanswerVisibleCode;
        }


        protected override ConditionValue getCValueUnmatchVisibleCode() {
            return _myCQ.UnmatchVisibleCode;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TOutputCommonCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TOutputCommonCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
