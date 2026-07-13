
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
    public class TColorSetInfoGtCIQ : AbstractBsTColorSetInfoGtCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTColorSetInfoGtCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorSetInfoGtCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTColorSetInfoGtCQ myCQ)
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


        protected override ConditionValue getCValueColorSetInfoGtId() {
            return _myCQ.ColorSetInfoGtId;
        }


        public override String keepColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList(subQuery);
        }

        public override String keepColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList(subQuery);
        }

        public override String keepColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            return _myCQ.keepColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList(subQuery);
        }

        public override String keepColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            return _myCQ.keepColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList(subQuery);
        }
        public override String keepColorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueTypeCode() {
            return _myCQ.TypeCode;
        }


        protected override ConditionValue getCValueGradationType() {
            return _myCQ.GradationType;
        }


        protected override ConditionValue getCValueGtScenarioItemId() {
            return _myCQ.GtScenarioItemId;
        }


        public override String keepGtScenarioItemId_InScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepGtScenarioItemId_InScopeSubQuery_TGtScenarioItem(subQuery);
        }

        public override String keepGtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery) {
            return _myCQ.keepGtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TColorSetInfoGtCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TColorSetInfoGtCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
