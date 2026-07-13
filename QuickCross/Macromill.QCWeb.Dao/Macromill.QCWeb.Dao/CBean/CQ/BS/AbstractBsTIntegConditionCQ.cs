
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
    public abstract class AbstractBsTIntegConditionCQ : AbstractConditionQuery {

        public AbstractBsTIntegConditionCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_INTEG_CONDITION"; }
        public override String getTableSqlName() { return "T_INTEG_CONDITION"; }

        public void SetIntegConditionId_Equal(decimal? v) { regIntegConditionId(CK_EQ, v); }
        public void SetIntegConditionId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIntegConditionId(CK_NES, v);
        }
        public void SetIntegConditionId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIntegConditionId(CK_GT, v);
        }
        public void SetIntegConditionId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIntegConditionId(CK_LT, v);
        }
        public void SetIntegConditionId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIntegConditionId(CK_GE, v);
        }
        public void SetIntegConditionId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regIntegConditionId(CK_LE, v);
        }
        public void SetIntegConditionId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueIntegConditionId(), "INTEG_CONDITION_ID");
        }
        public void SetIntegConditionId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueIntegConditionId(), "INTEG_CONDITION_ID");
        }
        public void SetIntegConditionId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIntegConditionId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetIntegConditionId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIntegConditionId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regIntegConditionId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueIntegConditionId(), "INTEG_CONDITION_ID");
        }
        protected abstract ConditionValue getCValueIntegConditionId();

        public void SetConditionNo_Equal(int? v) { regConditionNo(CK_EQ, v); }
        public void SetConditionNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionNo(CK_NES, v);
        }
        public void SetConditionNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionNo(CK_GT, v);
        }
        public void SetConditionNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionNo(CK_LT, v);
        }
        public void SetConditionNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionNo(CK_GE, v);
        }
        public void SetConditionNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regConditionNo(CK_LE, v);
        }
        public void SetConditionNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueConditionNo(), "CONDITION_NO");
        }
        public void SetConditionNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueConditionNo(), "CONDITION_NO");
        }
        protected void regConditionNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionNo(), "CONDITION_NO");
        }
        protected abstract ConditionValue getCValueConditionNo();

        public void SetSrcItemId_Equal(decimal? v) { regSrcItemId(CK_EQ, v); }
        public void SetSrcItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_NES, v);
        }
        public void SetSrcItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_GT, v);
        }
        public void SetSrcItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_LT, v);
        }
        public void SetSrcItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_GE, v);
        }
        public void SetSrcItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regSrcItemId(CK_LE, v);
        }
        public void SetSrcItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueSrcItemId(), "SRC_ITEM_ID");
        }
        public void SetSrcItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueSrcItemId(), "SRC_ITEM_ID");
        }
        protected void regSrcItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSrcItemId(), "SRC_ITEM_ID");
        }
        protected abstract ConditionValue getCValueSrcItemId();

        public void SetSourceItemNo_Equal(int? v) { regSourceItemNo(CK_EQ, v); }
        public void SetSourceItemNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSourceItemNo(CK_NES, v);
        }
        public void SetSourceItemNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSourceItemNo(CK_GT, v);
        }
        public void SetSourceItemNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSourceItemNo(CK_LT, v);
        }
        public void SetSourceItemNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSourceItemNo(CK_GE, v);
        }
        public void SetSourceItemNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSourceItemNo(CK_LE, v);
        }
        public void SetSourceItemNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSourceItemNo(), "SOURCE_ITEM_NO");
        }
        public void SetSourceItemNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSourceItemNo(), "SOURCE_ITEM_NO");
        }
        protected void regSourceItemNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSourceItemNo(), "SOURCE_ITEM_NO");
        }
        protected abstract ConditionValue getCValueSourceItemNo();

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
        public void SetOperationCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOperationCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOperationCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOperationCode(), "OPERATION_CODE");
        }
        protected abstract ConditionValue getCValueOperationCode();

        public void SetConditionString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionString_Equal(fRES(v));
        }
        protected void DoSetConditionString_Equal(String v) { regConditionString(CK_EQ, v); }
        public void SetConditionString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionString_NotEqual(fRES(v));
        }
        protected void DoSetConditionString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_NES, v);
        }
        public void SetConditionString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_GT, fRES(v));
        }
        public void SetConditionString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_LT, fRES(v));
        }
        public void SetConditionString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_GE, fRES(v));
        }
        public void SetConditionString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_LE, fRES(v));
        }
        public void SetConditionString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueConditionString(), "CONDITION_STRING");
        }
        public void SetConditionString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueConditionString(), "CONDITION_STRING");
        }
        public void SetConditionString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetConditionString_LikeSearch(v, cLSOP());
        }
        public void SetConditionString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueConditionString(), "CONDITION_STRING", option);
        }
        public void SetConditionString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueConditionString(), "CONDITION_STRING", option);
        }
        public void SetConditionString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetConditionString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regConditionString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionString(), "CONDITION_STRING");
        }
        protected abstract ConditionValue getCValueConditionString();

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
        public void InScopeTDataProcessNewItem(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataProcessNewItem(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery);
        public void NotInScopeTDataProcessNewItem(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery);
        protected void regDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataEditId(), "DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDataEditId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TIntegConditionCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TIntegConditionCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TIntegConditionCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TIntegConditionCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TIntegConditionCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TIntegConditionCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TIntegConditionCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TIntegConditionCB>(delegate(String function, SubQuery<TIntegConditionCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TIntegConditionCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TIntegConditionCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TIntegConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "INTEG_CONDITION_ID", "INTEG_CONDITION_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TIntegConditionCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
