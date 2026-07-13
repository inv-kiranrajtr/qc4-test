
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
    public class TReportCIQ : AbstractBsTReportCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTReportCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TReportCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTReportCQ myCQ)
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


        protected override ConditionValue getCValueReportId() {
            return _myCQ.ReportId;
        }


        public override String keepReportId_ExistsSubQuery_TReportChildList(TReportChildCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepReportId_ExistsSubQuery_TReportChildList(subQuery);
        }

        public override String keepReportId_NotExistsSubQuery_TReportChildList(TReportChildCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepReportId_NotExistsSubQuery_TReportChildList(subQuery);
        }

        public override String keepReportId_InScopeSubQuery_TReportChild(TReportChildCQ subQuery) {
            return _myCQ.keepReportId_InScopeSubQuery_TReportChild(subQuery);
        }

        public override String keepReportId_InScopeSubQuery_TReportChildList(TReportChildCQ subQuery) {
            return _myCQ.keepReportId_InScopeSubQuery_TReportChildList(subQuery);
        }

        public override String keepReportId_NotInScopeSubQuery_TReportChild(TReportChildCQ subQuery) {
            return _myCQ.keepReportId_NotInScopeSubQuery_TReportChild(subQuery);
        }

        public override String keepReportId_NotInScopeSubQuery_TReportChildList(TReportChildCQ subQuery) {
            return _myCQ.keepReportId_NotInScopeSubQuery_TReportChildList(subQuery);
        }
        public override String keepReportId_SpecifyDerivedReferrer_TReportChildList(TReportChildCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepReportId_QueryDerivedReferrer_TReportChildList(TReportChildCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepReportId_QueryDerivedReferrer_TReportChildListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueReportsetId() {
            return _myCQ.ReportsetId;
        }


        public override String keepReportsetId_InScopeSubQuery_TReportset(TReportsetCQ subQuery) {
            return _myCQ.keepReportsetId_InScopeSubQuery_TReportset(subQuery);
        }

        public override String keepReportsetId_NotInScopeSubQuery_TReportset(TReportsetCQ subQuery) {
            return _myCQ.keepReportsetId_NotInScopeSubQuery_TReportset(subQuery);
        }

        protected override ConditionValue getCValueTargetScenarioItemId() {
            return _myCQ.TargetScenarioItemId;
        }


        protected override ConditionValue getCValueSortNo() {
            return _myCQ.SortNo;
        }


        protected override ConditionValue getCValueChildDiv() {
            return _myCQ.ChildDiv;
        }


        protected override ConditionValue getCValueScenarioType() {
            return _myCQ.ScenarioType;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TReportCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TReportCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
