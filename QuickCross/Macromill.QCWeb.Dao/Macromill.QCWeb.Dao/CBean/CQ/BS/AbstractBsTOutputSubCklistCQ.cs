
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
    public abstract class AbstractBsTOutputSubCklistCQ : AbstractConditionQuery {

        public AbstractBsTOutputSubCklistCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SUB_CKLIST"; }
        public override String getTableSqlName() { return "T_OUTPUT_SUB_CKLIST"; }

        public void SetOutputSubCklistId_Equal(decimal? v) { regOutputSubCklistId(CK_EQ, v); }
        public void SetOutputSubCklistId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCklistId(CK_NES, v);
        }
        public void SetOutputSubCklistId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCklistId(CK_GT, v);
        }
        public void SetOutputSubCklistId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCklistId(CK_LT, v);
        }
        public void SetOutputSubCklistId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCklistId(CK_GE, v);
        }
        public void SetOutputSubCklistId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputSubCklistId(CK_LE, v);
        }
        public void SetOutputSubCklistId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputSubCklistId(), "OUTPUT_SUB_CKLIST_ID");
        }
        public void SetOutputSubCklistId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputSubCklistId(), "OUTPUT_SUB_CKLIST_ID");
        }
        public void SetOutputSubCklistId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCklistId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputSubCklistId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCklistId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputSubCklistId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputSubCklistId(), "OUTPUT_SUB_CKLIST_ID");
        }
        protected abstract ConditionValue getCValueOutputSubCklistId();

        public void SetOutputCommonId_Equal(decimal? v) { regOutputCommonId(CK_EQ, v); }
        public void SetOutputCommonId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_NES, v);
        }
        public void SetOutputCommonId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GT, v);
        }
        public void SetOutputCommonId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_LT, v);
        }
        public void SetOutputCommonId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GE, v);
        }
        public void SetOutputCommonId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputCommonId(CK_LE, v);
        }
        public void SetOutputCommonId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void SetOutputCommonId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void InScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputCommon(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        public void NotInScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        protected void regOutputCommonId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        protected abstract ConditionValue getCValueOutputCommonId();

        public void SetTotalCount_Equal(long? v) { regTotalCount(CK_EQ, v); }
        public void SetTotalCount_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalCount(CK_NES, v);
        }
        public void SetTotalCount_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalCount(CK_GT, v);
        }
        public void SetTotalCount_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalCount(CK_LT, v);
        }
        public void SetTotalCount_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalCount(CK_GE, v);
        }
        public void SetTotalCount_LessEqual(long? v) {
            WhereSetterFlag = true;
            regTotalCount(CK_LE, v);
        }
        public void SetTotalCount_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueTotalCount(), "TOTAL_COUNT");
        }
        public void SetTotalCount_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueTotalCount(), "TOTAL_COUNT");
        }
        protected void regTotalCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTotalCount(), "TOTAL_COUNT");
        }
        protected abstract ConditionValue getCValueTotalCount();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSubCklistCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSubCklistCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSubCklistCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSubCklistCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSubCklistCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSubCklistCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSubCklistCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSubCklistCB>(delegate(String function, SubQuery<TOutputSubCklistCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSubCklistCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSubCklistCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSubCklistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_SUB_CKLIST_ID", "OUTPUT_SUB_CKLIST_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSubCklistCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
