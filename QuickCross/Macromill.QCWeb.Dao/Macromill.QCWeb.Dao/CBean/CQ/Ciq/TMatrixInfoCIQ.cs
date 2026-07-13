
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
    public class TMatrixInfoCIQ : AbstractBsTMatrixInfoCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTMatrixInfoCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TMatrixInfoCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTMatrixInfoCQ myCQ)
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


        protected override ConditionValue getCValueMatrixInfoId() {
            return _myCQ.MatrixInfoId;
        }


        protected override ConditionValue getCValueItemInfoId() {
            return _myCQ.ItemInfoId;
        }


        public override String keepItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId(TItemInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId(subQuery);
        }

        public override String keepItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId(TItemInfoCQ subQuery) {
            return _myCQ.keepItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId(subQuery);
        }

        protected override ConditionValue getCValueChildItemInfoId() {
            return _myCQ.ChildItemInfoId;
        }


        public override String keepChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId(TItemInfoCQ subQuery) {
            return _myCQ.keepChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId(subQuery);
        }

        public override String keepChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId(TItemInfoCQ subQuery) {
            return _myCQ.keepChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId(subQuery);
        }

        protected override ConditionValue getCValueAddFaItemInfoId() {
            return _myCQ.AddFaItemInfoId;
        }


        protected override ConditionValue getCValueAddFaCategoryInfoId() {
            return _myCQ.AddFaCategoryInfoId;
        }


        public override String keepAddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo(TCategoryInfoCQ subQuery) {
            return _myCQ.keepAddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo(subQuery);
        }

        public override String keepAddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo(TCategoryInfoCQ subQuery) {
            return _myCQ.keepAddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo(subQuery);
        }

        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TMatrixInfoCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
