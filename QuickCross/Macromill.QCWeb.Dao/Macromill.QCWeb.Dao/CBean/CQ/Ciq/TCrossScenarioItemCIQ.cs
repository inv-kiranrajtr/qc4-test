
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
    public class TCrossScenarioItemCIQ : AbstractBsTCrossScenarioItemCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTCrossScenarioItemCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCrossScenarioItemCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTCrossScenarioItemCQ myCQ)
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


        protected override ConditionValue getCValueCrossScenarioItemId() {
            return _myCQ.CrossScenarioItemId;
        }


        public override String keepCrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList(subQuery);
        }

        public override String keepCrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList(subQuery);
        }

        public override String keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList(TPolylineCategoryListCQ subQuery) {
            return _myCQ.keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList(subQuery);
        }

        public override String keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            return _myCQ.keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList(subQuery);
        }

        public override String keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList(TPolylineCategoryListCQ subQuery) {
            return _myCQ.keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList(subQuery);
        }

        public override String keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            return _myCQ.keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList(subQuery);
        }
        public override String keepCrossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }

        protected override ConditionValue getCValueCrossScenarioTargetId() {
            return _myCQ.CrossScenarioTargetId;
        }


        public override String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(subQuery);
        }

        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(subQuery);
        }

        protected override ConditionValue getCValueSortNo() {
            return _myCQ.SortNo;
        }


        protected override ConditionValue getCValueAxis1ItemId() {
            return _myCQ.Axis1ItemId;
        }


        protected override ConditionValue getCValueAxis2ItemId() {
            return _myCQ.Axis2ItemId;
        }


        protected override ConditionValue getCValueViewItemName() {
            return _myCQ.ViewItemName;
        }


        protected override ConditionValue getCValueGraphType() {
            return _myCQ.GraphType;
        }


        protected override ConditionValue getCValueReportType() {
            return _myCQ.ReportType;
        }


        protected override ConditionValue getCValueTitleString() {
            return _myCQ.TitleString;
        }


        protected override ConditionValue getCValueScenarioComment() {
            return _myCQ.ScenarioComment;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TCrossScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TCrossScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
