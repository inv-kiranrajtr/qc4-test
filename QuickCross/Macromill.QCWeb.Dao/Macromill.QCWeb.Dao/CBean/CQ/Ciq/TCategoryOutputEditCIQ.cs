
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
    public class TCategoryOutputEditCIQ : AbstractBsTCategoryOutputEditCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTCategoryOutputEditCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCategoryOutputEditCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTCategoryOutputEditCQ myCQ)
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


        protected override ConditionValue getCValueCategoryOutputEditId() {
            return _myCQ.CategoryOutputEditId;
        }


        public override String keepCategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList(subQuery);
        }

        public override String keepCategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepCategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList(subQuery);
        }

        public override String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail(TCategoryOutputDetailCQ subQuery) {
            return _myCQ.keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail(subQuery);
        }

        public override String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            return _myCQ.keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList(subQuery);
        }

        public override String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail(TCategoryOutputDetailCQ subQuery) {
            return _myCQ.keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail(subQuery);
        }

        public override String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            return _myCQ.keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList(subQuery);
        }
        public override String keepCategoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameter(Object parameterValue) {
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

        protected override ConditionValue getCValueOldItemId() {
            return _myCQ.OldItemId;
        }


        protected override ConditionValue getCValueNewItemId() {
            return _myCQ.NewItemId;
        }


        protected override ConditionValue getCValueTopFlag() {
            return _myCQ.TopFlag;
        }


        protected override ConditionValue getCValueTopCount() {
            return _myCQ.TopCount;
        }


        protected override ConditionValue getCValueTopName() {
            return _myCQ.TopName;
        }


        protected override ConditionValue getCValueBottomFlag() {
            return _myCQ.BottomFlag;
        }


        protected override ConditionValue getCValueBottomCount() {
            return _myCQ.BottomCount;
        }


        protected override ConditionValue getCValueBottomName() {
            return _myCQ.BottomName;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TCategoryOutputEditCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TCategoryOutputEditCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
