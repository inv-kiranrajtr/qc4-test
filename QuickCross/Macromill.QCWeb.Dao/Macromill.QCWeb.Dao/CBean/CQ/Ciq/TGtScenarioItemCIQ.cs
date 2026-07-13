
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
    public class TGtScenarioItemCIQ : AbstractBsTGtScenarioItemCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTGtScenarioItemCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TGtScenarioItemCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTGtScenarioItemCQ myCQ)
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


        protected override ConditionValue getCValueGtScenarioItemId() {
            return _myCQ.GtScenarioItemId;
        }


        public override String keepGtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepGtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList(subQuery);
        }

        public override String keepGtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepGtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList(subQuery);
        }

        public override String keepGtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            return _myCQ.keepGtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList(subQuery);
        }

        public override String keepGtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            return _myCQ.keepGtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList(subQuery);
        }
        public override String keepGtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueScenarioTotalizationId() {
            return _myCQ.ScenarioTotalizationId;
        }


        public override String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(subQuery);
        }

        public override String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery) {
            return _myCQ.keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(subQuery);
        }

        protected override ConditionValue getCValueSortNo() {
            return _myCQ.SortNo;
        }


        protected override ConditionValue getCValueItemInfoId() {
            return _myCQ.ItemInfoId;
        }


        public override String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TItemInfo(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TItemInfo(subQuery);
        }

        protected override ConditionValue getCValueScenarioName() {
            return _myCQ.ScenarioName;
        }


        protected override ConditionValue getCValueGraphType() {
            return _myCQ.GraphType;
        }


        protected override ConditionValue getCValueReportType() {
            return _myCQ.ReportType;
        }


        protected override ConditionValue getCValueViewItemString() {
            return _myCQ.ViewItemString;
        }


        protected override ConditionValue getCValueScenarioComment() {
            return _myCQ.ScenarioComment;
        }


        protected override ConditionValue getCValueSurveyType() {
            return _myCQ.SurveyType;
        }


        protected override ConditionValue getCValueGraphTypeReport() {
            return _myCQ.GraphTypeReport;
        }


        protected override ConditionValue getCValueTestTargetType() {
            return _myCQ.TestTargetType;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TGtScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TGtScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
