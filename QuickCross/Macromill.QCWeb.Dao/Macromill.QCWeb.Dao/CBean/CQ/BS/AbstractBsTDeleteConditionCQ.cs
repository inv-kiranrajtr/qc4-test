
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
    public abstract class AbstractBsTDeleteConditionCQ : AbstractConditionQuery {

        public AbstractBsTDeleteConditionCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DELETE_CONDITION"; }
        public override String getTableSqlName() { return "T_DELETE_CONDITION"; }

        public void SetDeleteConditionId_Equal(decimal? v) { regDeleteConditionId(CK_EQ, v); }
        public void SetDeleteConditionId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteConditionId(CK_NES, v);
        }
        public void SetDeleteConditionId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteConditionId(CK_GT, v);
        }
        public void SetDeleteConditionId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteConditionId(CK_LT, v);
        }
        public void SetDeleteConditionId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteConditionId(CK_GE, v);
        }
        public void SetDeleteConditionId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDeleteConditionId(CK_LE, v);
        }
        public void SetDeleteConditionId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDeleteConditionId(), "DELETE_CONDITION_ID");
        }
        public void SetDeleteConditionId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDeleteConditionId(), "DELETE_CONDITION_ID");
        }
        public void SetDeleteConditionId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteConditionId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDeleteConditionId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteConditionId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDeleteConditionId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteConditionId(), "DELETE_CONDITION_ID");
        }
        protected abstract ConditionValue getCValueDeleteConditionId();

        public void SetSortNo_Equal(int? v) { regSortNo(CK_EQ, v); }
        public void SetSortNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_NES, v);
        }
        public void SetSortNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GT, v);
        }
        public void SetSortNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_LT, v);
        }
        public void SetSortNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GE, v);
        }
        public void SetSortNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortNo(CK_LE, v);
        }
        public void SetSortNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        public void SetSortNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        protected void regSortNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortNo(), "SORT_NO");
        }
        protected abstract ConditionValue getCValueSortNo();

        public void SetItemId_Equal(decimal? v) { regItemId(CK_EQ, v); }
        public void SetItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemId(CK_NES, v);
        }
        public void SetItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemId(CK_GT, v);
        }
        public void SetItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemId(CK_LT, v);
        }
        public void SetItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemId(CK_GE, v);
        }
        public void SetItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regItemId(CK_LE, v);
        }
        public void SetItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueItemId(), "ITEM_ID");
        }
        public void SetItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueItemId(), "ITEM_ID");
        }
        protected void regItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemId(), "ITEM_ID");
        }
        protected abstract ConditionValue getCValueItemId();

        public void SetOperationCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(fRES(v));
        }
        protected void DoSetOperationCode_Equal(String v) { regOperationCode(CK_EQ, v); }
        public void SetOperationCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(fRES(v));
        }
        protected void DoSetOperationCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_NES, v);
        }
        public void SetOperationCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_GT, fRES(v));
        }
        public void SetOperationCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_LT, fRES(v));
        }
        public void SetOperationCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_GE, fRES(v));
        }
        public void SetOperationCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_LE, fRES(v));
        }
        public void SetOperationCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOperationCode(), "OPERATION_CODE");
        }
        public void SetOperationCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOperationCode(), "OPERATION_CODE");
        }
        public void SetOperationCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOperationCode_LikeSearch(v, cLSOP());
        }
        public void SetOperationCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOperationCode(), "OPERATION_CODE", option);
        }
        public void SetOperationCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOperationCode(), "OPERATION_CODE", option);
        }
        protected void regOperationCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOperationCode(), "OPERATION_CODE");
        }
        protected abstract ConditionValue getCValueOperationCode();

        public void SetOperationValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationValue_Equal(fRES(v));
        }
        protected void DoSetOperationValue_Equal(String v) { regOperationValue(CK_EQ, v); }
        public void SetOperationValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationValue_NotEqual(fRES(v));
        }
        protected void DoSetOperationValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_NES, v);
        }
        public void SetOperationValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_GT, fRES(v));
        }
        public void SetOperationValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_LT, fRES(v));
        }
        public void SetOperationValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_GE, fRES(v));
        }
        public void SetOperationValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_LE, fRES(v));
        }
        public void SetOperationValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOperationValue(), "OPERATION_VALUE");
        }
        public void SetOperationValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOperationValue(), "OPERATION_VALUE");
        }
        public void SetOperationValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOperationValue_LikeSearch(v, cLSOP());
        }
        public void SetOperationValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOperationValue(), "OPERATION_VALUE", option);
        }
        public void SetOperationValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOperationValue(), "OPERATION_VALUE", option);
        }
        public void SetOperationValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOperationValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOperationValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOperationValue(), "OPERATION_VALUE");
        }
        protected abstract ConditionValue getCValueOperationValue();

        public void SetDataEditId_Equal(decimal? v) { regDataEditId(CK_EQ, v); }
        public void SetDataEditId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_NES, v);
        }
        public void SetDataEditId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_GT, v);
        }
        public void SetDataEditId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_LT, v);
        }
        public void SetDataEditId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_GE, v);
        }
        public void SetDataEditId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDataEditId(CK_LE, v);
        }
        public void SetDataEditId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDataEditId(), "DATA_EDIT_ID");
        }
        public void SetDataEditId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDataEditId(), "DATA_EDIT_ID");
        }
        public void InScopeTDeleteData(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDeleteData(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDeleteData(TDeleteDataCQ subQuery);
        public void NotInScopeTDeleteData(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDeleteData(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDeleteData(TDeleteDataCQ subQuery);
        protected void regDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataEditId(), "DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDataEditId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDeleteConditionCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDeleteConditionCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDeleteConditionCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDeleteConditionCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDeleteConditionCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDeleteConditionCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDeleteConditionCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDeleteConditionCB>(delegate(String function, SubQuery<TDeleteConditionCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDeleteConditionCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDeleteConditionCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDeleteConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DELETE_CONDITION_ID", "DELETE_CONDITION_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDeleteConditionCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
