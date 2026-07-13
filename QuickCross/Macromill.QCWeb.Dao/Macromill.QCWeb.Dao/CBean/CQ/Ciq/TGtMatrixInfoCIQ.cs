
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
    public class TGtMatrixInfoCIQ : AbstractBsTGtMatrixInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTGtMatrixInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TGtMatrixInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTGtMatrixInfoCQ myCQ)
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


        protected override ConditionValue getCValueGtMatrixInfoId() {
            return _myCQ.GtMatrixInfoId;
        }


        public override String keepGtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepGtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList(subQuery);
        }

        public override String keepGtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepGtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList(subQuery);
        }

        public override String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChild(TGtMatrixChildCQ subQuery) {
            return _myCQ.keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChild(subQuery);
        }

        public override String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            return _myCQ.keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList(subQuery);
        }

        public override String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild(TGtMatrixChildCQ subQuery) {
            return _myCQ.keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild(subQuery);
        }

        public override String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            return _myCQ.keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList(subQuery);
        }
        public override String keepGtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(Object parameterValue) {
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

        protected override ConditionValue getCValueBaseItemId() {
            return _myCQ.BaseItemId;
        }


        protected override ConditionValue getCValueNewItemId() {
            return _myCQ.NewItemId;
        }


        protected override ConditionValue getCValueTotalizationType() {
            return _myCQ.TotalizationType;
        }


        protected override ConditionValue getCValueLv1title() {
            return _myCQ.Lv1title;
        }


        protected override ConditionValue getCValueItemName() {
            return _myCQ.ItemName;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TGtMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TGtMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
