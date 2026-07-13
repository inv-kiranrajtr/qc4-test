
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
    public abstract class AbstractBsTCategoryOutputDetailCQ : AbstractConditionQuery {

        public AbstractBsTCategoryOutputDetailCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_CATEGORY_OUTPUT_DETAIL"; }
        public override String getTableSqlName() { return "T_CATEGORY_OUTPUT_DETAIL"; }

        public void SetCategoryOutputEditDetailId_Equal(decimal? v) { regCategoryOutputEditDetailId(CK_EQ, v); }
        public void SetCategoryOutputEditDetailId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditDetailId(CK_NES, v);
        }
        public void SetCategoryOutputEditDetailId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditDetailId(CK_GT, v);
        }
        public void SetCategoryOutputEditDetailId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditDetailId(CK_LT, v);
        }
        public void SetCategoryOutputEditDetailId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditDetailId(CK_GE, v);
        }
        public void SetCategoryOutputEditDetailId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCategoryOutputEditDetailId(CK_LE, v);
        }
        public void SetCategoryOutputEditDetailId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCategoryOutputEditDetailId(), "CATEGORY_OUTPUT_EDIT_DETAIL_ID");
        }
        public void SetCategoryOutputEditDetailId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCategoryOutputEditDetailId(), "CATEGORY_OUTPUT_EDIT_DETAIL_ID");
        }
        public void SetCategoryOutputEditDetailId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditDetailId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCategoryOutputEditDetailId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditDetailId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCategoryOutputEditDetailId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryOutputEditDetailId(), "CATEGORY_OUTPUT_EDIT_DETAIL_ID");
        }
        protected abstract ConditionValue getCValueCategoryOutputEditDetailId();

        public void SetCategoryOutputEditId_Equal(decimal? v) { regCategoryOutputEditId(CK_EQ, v); }
        public void SetCategoryOutputEditId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_NES, v);
        }
        public void SetCategoryOutputEditId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_GT, v);
        }
        public void SetCategoryOutputEditId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_LT, v);
        }
        public void SetCategoryOutputEditId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_GE, v);
        }
        public void SetCategoryOutputEditId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCategoryOutputEditId(CK_LE, v);
        }
        public void SetCategoryOutputEditId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCategoryOutputEditId(), "CATEGORY_OUTPUT_EDIT_ID");
        }
        public void SetCategoryOutputEditId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCategoryOutputEditId(), "CATEGORY_OUTPUT_EDIT_ID");
        }
        public void InScopeTCategoryOutputEdit(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputEdit(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputEdit(TCategoryOutputEditCQ subQuery);
        public void NotInScopeTCategoryOutputEdit(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEdit(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputEdit(TCategoryOutputEditCQ subQuery);
        protected void regCategoryOutputEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryOutputEditId(), "CATEGORY_OUTPUT_EDIT_ID");
        }
        protected abstract ConditionValue getCValueCategoryOutputEditId();

        public void SetOldCategoryNo_Equal(int? v) { regOldCategoryNo(CK_EQ, v); }
        public void SetOldCategoryNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldCategoryNo(CK_NES, v);
        }
        public void SetOldCategoryNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldCategoryNo(CK_GT, v);
        }
        public void SetOldCategoryNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldCategoryNo(CK_LT, v);
        }
        public void SetOldCategoryNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldCategoryNo(CK_GE, v);
        }
        public void SetOldCategoryNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regOldCategoryNo(CK_LE, v);
        }
        public void SetOldCategoryNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueOldCategoryNo(), "OLD_CATEGORY_NO");
        }
        public void SetOldCategoryNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueOldCategoryNo(), "OLD_CATEGORY_NO");
        }
        protected void regOldCategoryNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOldCategoryNo(), "OLD_CATEGORY_NO");
        }
        protected abstract ConditionValue getCValueOldCategoryNo();

        public void SetNewCategoryNo_Equal(int? v) { regNewCategoryNo(CK_EQ, v); }
        public void SetNewCategoryNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_NES, v);
        }
        public void SetNewCategoryNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_GT, v);
        }
        public void SetNewCategoryNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_LT, v);
        }
        public void SetNewCategoryNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_GE, v);
        }
        public void SetNewCategoryNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regNewCategoryNo(CK_LE, v);
        }
        public void SetNewCategoryNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNewCategoryNo(), "NEW_CATEGORY_NO");
        }
        public void SetNewCategoryNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNewCategoryNo(), "NEW_CATEGORY_NO");
        }
        protected void regNewCategoryNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewCategoryNo(), "NEW_CATEGORY_NO");
        }
        protected abstract ConditionValue getCValueNewCategoryNo();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TCategoryOutputDetailCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TCategoryOutputDetailCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TCategoryOutputDetailCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TCategoryOutputDetailCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TCategoryOutputDetailCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TCategoryOutputDetailCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TCategoryOutputDetailCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TCategoryOutputDetailCB>(delegate(String function, SubQuery<TCategoryOutputDetailCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TCategoryOutputDetailCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TCategoryOutputDetailCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_DETAIL_ID", "CATEGORY_OUTPUT_EDIT_DETAIL_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TCategoryOutputDetailCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
