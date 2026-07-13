
using System;
using System.Collections.Generic;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CKey;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public abstract class AbstractBsTGtMatrixChildCQ : AbstractConditionQuery {

        public AbstractBsTGtMatrixChildCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_GT_MATRIX_CHILD"; }
        public override String getTableSqlName() { return "T_GT_MATRIX_CHILD"; }

        public void SetGtMatrixChildid_Equal(decimal? v) { regGtMatrixChildid(CK_EQ, v); }
        public void SetGtMatrixChildid_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixChildid(CK_NES, v);
        }
        public void SetGtMatrixChildid_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixChildid(CK_GT, v);
        }
        public void SetGtMatrixChildid_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixChildid(CK_LT, v);
        }
        public void SetGtMatrixChildid_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixChildid(CK_GE, v);
        }
        public void SetGtMatrixChildid_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regGtMatrixChildid(CK_LE, v);
        }
        public void SetGtMatrixChildid_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueGtMatrixChildid(), "GT_MATRIX_CHILDID");
        }
        public void SetGtMatrixChildid_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueGtMatrixChildid(), "GT_MATRIX_CHILDID");
        }
        public void SetGtMatrixChildid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixChildid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGtMatrixChildid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixChildid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGtMatrixChildid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGtMatrixChildid(), "GT_MATRIX_CHILDID");
        }
        protected abstract ConditionValue getCValueGtMatrixChildid();

        public void SetGtMatrixInfoId_Equal(decimal? v) { regGtMatrixInfoId(CK_EQ, v); }
        public void SetGtMatrixInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_NES, v);
        }
        public void SetGtMatrixInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_GT, v);
        }
        public void SetGtMatrixInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_LT, v);
        }
        public void SetGtMatrixInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_GE, v);
        }
        public void SetGtMatrixInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regGtMatrixInfoId(CK_LE, v);
        }
        public void SetGtMatrixInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueGtMatrixInfoId(), "GT_MATRIX_INFO_ID");
        }
        public void SetGtMatrixInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueGtMatrixInfoId(), "GT_MATRIX_INFO_ID");
        }
        public void InScopeTGtMatrixInfo(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixInfo(TGtMatrixInfoCQ subQuery);
        public void NotInScopeTGtMatrixInfo(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixInfo(TGtMatrixInfoCQ subQuery);
        protected void regGtMatrixInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGtMatrixInfoId(), "GT_MATRIX_INFO_ID");
        }
        protected abstract ConditionValue getCValueGtMatrixInfoId();

        public void SetChildItemId_Equal(decimal? v) { regChildItemId(CK_EQ, v); }
        public void SetChildItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemId(CK_NES, v);
        }
        public void SetChildItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemId(CK_GT, v);
        }
        public void SetChildItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemId(CK_LT, v);
        }
        public void SetChildItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemId(CK_GE, v);
        }
        public void SetChildItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regChildItemId(CK_LE, v);
        }
        public void SetChildItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueChildItemId(), "CHILD_ITEM_ID");
        }
        public void SetChildItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueChildItemId(), "CHILD_ITEM_ID");
        }
        public void InScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepChildItemId_InScopeSubQuery_TItemInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CHILD_ITEM_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepChildItemId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepChildItemId_NotInScopeSubQuery_TItemInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CHILD_ITEM_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepChildItemId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        protected void regChildItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChildItemId(), "CHILD_ITEM_ID");
        }
        protected abstract ConditionValue getCValueChildItemId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TGtMatrixChildCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TGtMatrixChildCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TGtMatrixChildCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TGtMatrixChildCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TGtMatrixChildCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TGtMatrixChildCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TGtMatrixChildCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TGtMatrixChildCB>(delegate(String function, SubQuery<TGtMatrixChildCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TGtMatrixChildCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TGtMatrixChildCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "GT_MATRIX_CHILDID", "GT_MATRIX_CHILDID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TGtMatrixChildCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
