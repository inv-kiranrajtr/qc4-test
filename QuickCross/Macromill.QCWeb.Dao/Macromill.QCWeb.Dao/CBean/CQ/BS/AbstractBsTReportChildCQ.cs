
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
    public abstract class AbstractBsTReportChildCQ : AbstractConditionQuery {

        public AbstractBsTReportChildCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_REPORT_CHILD"; }
        public override String getTableSqlName() { return "T_REPORT_CHILD"; }

        public void SetReportChildId_Equal(decimal? v) { regReportChildId(CK_EQ, v); }
        public void SetReportChildId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportChildId(CK_NES, v);
        }
        public void SetReportChildId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportChildId(CK_GT, v);
        }
        public void SetReportChildId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportChildId(CK_LT, v);
        }
        public void SetReportChildId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportChildId(CK_GE, v);
        }
        public void SetReportChildId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regReportChildId(CK_LE, v);
        }
        public void SetReportChildId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueReportChildId(), "REPORT_CHILD_ID");
        }
        public void SetReportChildId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueReportChildId(), "REPORT_CHILD_ID");
        }
        public void SetReportChildId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportChildId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetReportChildId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportChildId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regReportChildId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportChildId(), "REPORT_CHILD_ID");
        }
        protected abstract ConditionValue getCValueReportChildId();

        public void SetParentReportId_Equal(decimal? v) { regParentReportId(CK_EQ, v); }
        public void SetParentReportId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParentReportId(CK_NES, v);
        }
        public void SetParentReportId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParentReportId(CK_GT, v);
        }
        public void SetParentReportId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParentReportId(CK_LT, v);
        }
        public void SetParentReportId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParentReportId(CK_GE, v);
        }
        public void SetParentReportId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regParentReportId(CK_LE, v);
        }
        public void SetParentReportId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueParentReportId(), "PARENT_REPORT_ID");
        }
        public void SetParentReportId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueParentReportId(), "PARENT_REPORT_ID");
        }
        public void InScopeTReport(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepParentReportId_InScopeSubQuery_TReport(cb.Query());
            registerInScopeSubQuery(cb.Query(), "PARENT_REPORT_ID", "REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepParentReportId_InScopeSubQuery_TReport(TReportCQ subQuery);
        public void NotInScopeTReport(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepParentReportId_NotInScopeSubQuery_TReport(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "PARENT_REPORT_ID", "REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepParentReportId_NotInScopeSubQuery_TReport(TReportCQ subQuery);
        protected void regParentReportId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueParentReportId(), "PARENT_REPORT_ID");
        }
        protected abstract ConditionValue getCValueParentReportId();

        public void SetTargetScenarioItemId_Equal(decimal? v) { regTargetScenarioItemId(CK_EQ, v); }
        public void SetTargetScenarioItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetScenarioItemId(CK_NES, v);
        }
        public void SetTargetScenarioItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetScenarioItemId(CK_GT, v);
        }
        public void SetTargetScenarioItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetScenarioItemId(CK_LT, v);
        }
        public void SetTargetScenarioItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetScenarioItemId(CK_GE, v);
        }
        public void SetTargetScenarioItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regTargetScenarioItemId(CK_LE, v);
        }
        public void SetTargetScenarioItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueTargetScenarioItemId(), "TARGET_SCENARIO_ITEM_ID");
        }
        public void SetTargetScenarioItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueTargetScenarioItemId(), "TARGET_SCENARIO_ITEM_ID");
        }
        protected void regTargetScenarioItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTargetScenarioItemId(), "TARGET_SCENARIO_ITEM_ID");
        }
        protected abstract ConditionValue getCValueTargetScenarioItemId();

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

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TReportChildCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TReportChildCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TReportChildCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TReportChildCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TReportChildCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TReportChildCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TReportChildCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TReportChildCB>(delegate(String function, SubQuery<TReportChildCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TReportChildCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TReportChildCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "REPORT_CHILD_ID", "REPORT_CHILD_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TReportChildCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
