
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
    public class TFaScenarioHeaderCIQ : AbstractBsTFaScenarioHeaderCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTFaScenarioHeaderCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TFaScenarioHeaderCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTFaScenarioHeaderCQ myCQ)
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


        protected override ConditionValue getCValueFaScenarioHeaderId() {
            return _myCQ.FaScenarioHeaderId;
        }


        public override String keepFaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepFaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new SystemException("ExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepFaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepFaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new SystemException("NotExistsSubQuery at inline() is unsupported! Sorry!");
            // _myCQ.keepFaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem(TFaScenarioItemCQ subQuery) {
            return _myCQ.keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem(subQuery);
        }

        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            return _myCQ.keepFaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            return _myCQ.keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem(TFaScenarioItemCQ subQuery) {
            return _myCQ.keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem(subQuery);
        }

        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            return _myCQ.keepFaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList(subQuery);
        }

        public override String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            return _myCQ.keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList(subQuery);
        }
        public override String keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Specify)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameter(Object parameterValue) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery) {
            throw new UnsupportedOperationException("(Query)DerivedReferrer at inline() is unsupported! Sorry!");
        }
        public override String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameter(Object parameterValue) {
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

        protected override ConditionValue getCValueScenarioComment() {
            return _myCQ.ScenarioComment;
        }


        protected override ConditionValue getCValueViewName() {
            return _myCQ.ViewName;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TFaScenarioHeaderCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TFaScenarioHeaderCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
