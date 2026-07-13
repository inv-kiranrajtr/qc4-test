
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
    public class TPolylineCategoryListCIQ : AbstractBsTPolylineCategoryListCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTPolylineCategoryListCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TPolylineCategoryListCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTPolylineCategoryListCQ myCQ)
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


        protected override ConditionValue getCValuePolylineCategoryListId() {
            return _myCQ.PolylineCategoryListId;
        }


        protected override ConditionValue getCValueCrossScenarioItemId() {
            return _myCQ.CrossScenarioItemId;
        }


        public override String keepCrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem(TCrossScenarioItemCQ subQuery) {
            return _myCQ.keepCrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem(subQuery);
        }

        public override String keepCrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem(TCrossScenarioItemCQ subQuery) {
            return _myCQ.keepCrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem(subQuery);
        }

        protected override ConditionValue getCValueAxisCategoryNo() {
            return _myCQ.AxisCategoryNo;
        }


        protected override ConditionValue getCValueAxis2CategoryNo() {
            return _myCQ.Axis2CategoryNo;
        }


        protected override ConditionValue getCValueArrayNoSingular() {
            return _myCQ.ArrayNoSingular;
        }


        protected override ConditionValue getCValueArrayNoPlural() {
            return _myCQ.ArrayNoPlural;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TPolylineCategoryListCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TPolylineCategoryListCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
