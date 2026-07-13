
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
    public abstract class AbstractBsTMatrixInfoCQ : AbstractConditionQuery {

        public AbstractBsTMatrixInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_MATRIX_INFO"; }
        public override String getTableSqlName() { return "T_MATRIX_INFO"; }

        public void SetMatrixInfoId_Equal(decimal? v) { regMatrixInfoId(CK_EQ, v); }
        public void SetMatrixInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixInfoId(CK_NES, v);
        }
        public void SetMatrixInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixInfoId(CK_GT, v);
        }
        public void SetMatrixInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixInfoId(CK_LT, v);
        }
        public void SetMatrixInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixInfoId(CK_GE, v);
        }
        public void SetMatrixInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regMatrixInfoId(CK_LE, v);
        }
        public void SetMatrixInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueMatrixInfoId(), "MATRIX_INFO_ID");
        }
        public void SetMatrixInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueMatrixInfoId(), "MATRIX_INFO_ID");
        }
        public void SetMatrixInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMatrixInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMatrixInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMatrixInfoId(), "MATRIX_INFO_ID");
        }
        protected abstract ConditionValue getCValueMatrixInfoId();

        public void SetItemInfoId_Equal(decimal? v) { regItemInfoId(CK_EQ, v); }
        public void SetItemInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_NES, v);
        }
        public void SetItemInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_GT, v);
        }
        public void SetItemInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_LT, v);
        }
        public void SetItemInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_GE, v);
        }
        public void SetItemInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regItemInfoId(CK_LE, v);
        }
        public void SetItemInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        public void SetItemInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        public void InScopeTItemInfoByItemInfoId(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TItemInfoByItemInfoId(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfoByItemInfoId(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TItemInfoByItemInfoId(TItemInfoCQ subQuery);
        protected void regItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueItemInfoId();

        public void SetChildItemInfoId_Equal(decimal? v) { regChildItemInfoId(CK_EQ, v); }
        public void SetChildItemInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemInfoId(CK_NES, v);
        }
        public void SetChildItemInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemInfoId(CK_GT, v);
        }
        public void SetChildItemInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemInfoId(CK_LT, v);
        }
        public void SetChildItemInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildItemInfoId(CK_GE, v);
        }
        public void SetChildItemInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regChildItemInfoId(CK_LE, v);
        }
        public void SetChildItemInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueChildItemInfoId(), "CHILD_ITEM_INFO_ID");
        }
        public void SetChildItemInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueChildItemInfoId(), "CHILD_ITEM_INFO_ID");
        }
        public void InScopeTItemInfoByChildItemInfoId(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CHILD_ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepChildItemInfoId_InScopeSubQuery_TItemInfoByChildItemInfoId(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfoByChildItemInfoId(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CHILD_ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepChildItemInfoId_NotInScopeSubQuery_TItemInfoByChildItemInfoId(TItemInfoCQ subQuery);
        protected void regChildItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChildItemInfoId(), "CHILD_ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueChildItemInfoId();

        public void SetAddFaItemInfoId_Equal(decimal? v) { regAddFaItemInfoId(CK_EQ, v); }
        public void SetAddFaItemInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaItemInfoId(CK_NES, v);
        }
        public void SetAddFaItemInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaItemInfoId(CK_GT, v);
        }
        public void SetAddFaItemInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaItemInfoId(CK_LT, v);
        }
        public void SetAddFaItemInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaItemInfoId(CK_GE, v);
        }
        public void SetAddFaItemInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regAddFaItemInfoId(CK_LE, v);
        }
        public void SetAddFaItemInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueAddFaItemInfoId(), "ADD_FA_ITEM_INFO_ID");
        }
        public void SetAddFaItemInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueAddFaItemInfoId(), "ADD_FA_ITEM_INFO_ID");
        }
        public void SetAddFaItemInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaItemInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAddFaItemInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaItemInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAddFaItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAddFaItemInfoId(), "ADD_FA_ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueAddFaItemInfoId();

        public void SetAddFaCategoryInfoId_Equal(decimal? v) { regAddFaCategoryInfoId(CK_EQ, v); }
        public void SetAddFaCategoryInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaCategoryInfoId(CK_NES, v);
        }
        public void SetAddFaCategoryInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaCategoryInfoId(CK_GT, v);
        }
        public void SetAddFaCategoryInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaCategoryInfoId(CK_LT, v);
        }
        public void SetAddFaCategoryInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaCategoryInfoId(CK_GE, v);
        }
        public void SetAddFaCategoryInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regAddFaCategoryInfoId(CK_LE, v);
        }
        public void SetAddFaCategoryInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueAddFaCategoryInfoId(), "ADD_FA_CATEGORY_INFO_ID");
        }
        public void SetAddFaCategoryInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueAddFaCategoryInfoId(), "ADD_FA_CATEGORY_INFO_ID");
        }
        public void InScopeTCategoryInfo(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepAddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ADD_FA_CATEGORY_INFO_ID", "CATEGORY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepAddFaCategoryInfoId_InScopeSubQuery_TCategoryInfo(TCategoryInfoCQ subQuery);
        public void NotInScopeTCategoryInfo(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepAddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ADD_FA_CATEGORY_INFO_ID", "CATEGORY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepAddFaCategoryInfoId_NotInScopeSubQuery_TCategoryInfo(TCategoryInfoCQ subQuery);
        public void SetAddFaCategoryInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaCategoryInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAddFaCategoryInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddFaCategoryInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAddFaCategoryInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAddFaCategoryInfoId(), "ADD_FA_CATEGORY_INFO_ID");
        }
        protected abstract ConditionValue getCValueAddFaCategoryInfoId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TMatrixInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TMatrixInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TMatrixInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TMatrixInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TMatrixInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TMatrixInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TMatrixInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TMatrixInfoCB>(delegate(String function, SubQuery<TMatrixInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TMatrixInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TMatrixInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "MATRIX_INFO_ID", "MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TMatrixInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
