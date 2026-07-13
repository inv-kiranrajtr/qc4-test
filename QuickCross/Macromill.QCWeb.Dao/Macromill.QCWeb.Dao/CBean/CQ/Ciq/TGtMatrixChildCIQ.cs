
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
    public class TGtMatrixChildCIQ : AbstractBsTGtMatrixChildCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTGtMatrixChildCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TGtMatrixChildCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTGtMatrixChildCQ myCQ)
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


        protected override ConditionValue getCValueGtMatrixChildid() {
            return _myCQ.GtMatrixChildid;
        }


        protected override ConditionValue getCValueGtMatrixInfoId() {
            return _myCQ.GtMatrixInfoId;
        }


        public override String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo(TGtMatrixInfoCQ subQuery) {
            return _myCQ.keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo(subQuery);
        }

        public override String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo(TGtMatrixInfoCQ subQuery) {
            return _myCQ.keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo(subQuery);
        }

        protected override ConditionValue getCValueChildItemId() {
            return _myCQ.ChildItemId;
        }


        public override String keepChildItemId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            return _myCQ.keepChildItemId_InScopeSubQuery_TItemInfo(subQuery);
        }

        public override String keepChildItemId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery) {
            return _myCQ.keepChildItemId_NotInScopeSubQuery_TItemInfo(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TGtMatrixChildCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TGtMatrixChildCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
