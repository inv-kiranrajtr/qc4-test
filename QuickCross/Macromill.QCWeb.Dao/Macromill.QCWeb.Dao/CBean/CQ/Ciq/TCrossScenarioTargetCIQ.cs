
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
    public class TCrossScenarioTargetCIQ : AbstractBsTCrossScenarioTargetCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTCrossScenarioTargetCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCrossScenarioTargetCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTCrossScenarioTargetCQ myCQ)
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


        protected override ConditionValue getCValueCrossScenarioTargetId() {
            return _myCQ.CrossScenarioTargetId;
        }


        public override String keepCrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList(subQuery);
        }

        public override String keepCrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList(subQuery);
        }

        public override String keepCrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList(subQuery);
        }

        public override String keepCrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList(subQuery);
        }

        public override String keepCrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList(subQuery);
        }

        public override String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList(subQuery);
        }

        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList(subQuery);
        }

        public override String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            return _myCQ.keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList(subQuery);
        }
        public override String keepCrossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameter(Object parameterValue) {
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

        protected override ConditionValue getCValueScenariosetNo() {
            return _myCQ.ScenariosetNo;
        }


        protected override ConditionValue getCValueSortNo() {
            return _myCQ.SortNo;
        }


        protected override ConditionValue getCValueScItemId() {
            return _myCQ.ScItemId;
        }


        protected override ConditionValue getCValueViewName() {
            return _myCQ.ViewName;
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


        protected override ConditionValue getCValuePolylineFlag() {
            return _myCQ.PolylineFlag;
        }


        protected override ConditionValue getCValueGraphTypeReport() {
            return _myCQ.GraphTypeReport;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TCrossScenarioTargetCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TCrossScenarioTargetCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
