
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
    public abstract class AbstractBsTReportCQ : AbstractConditionQuery {

        public AbstractBsTReportCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_REPORT"; }
        public override String getTableSqlName() { return "T_REPORT"; }

        public void SetReportId_Equal(decimal? v) { regReportId(CK_EQ, v); }
        public void SetReportId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportId(CK_NES, v);
        }
        public void SetReportId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportId(CK_GT, v);
        }
        public void SetReportId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportId(CK_LT, v);
        }
        public void SetReportId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportId(CK_GE, v);
        }
        public void SetReportId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regReportId(CK_LE, v);
        }
        public void SetReportId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueReportId(), "REPORT_ID");
        }
        public void SetReportId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueReportId(), "REPORT_ID");
        }
        public void ExistsTReportChildList(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_ExistsSubQuery_TReportChildList(cb.Query());
            registerExistsSubQuery(cb.Query(), "REPORT_ID", "PARENT_REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepReportId_ExistsSubQuery_TReportChildList(TReportChildCQ subQuery);
        public void NotExistsTReportChildList(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_NotExistsSubQuery_TReportChildList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "REPORT_ID", "PARENT_REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepReportId_NotExistsSubQuery_TReportChildList(TReportChildCQ subQuery);
        public void InScopeTReportChild(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_InScopeSubQuery_TReportChild(cb.Query());
            registerInScopeSubQuery(cb.Query(), "REPORT_ID", "Parent_Report_ID", subQueryPropertyName);
        }
        public abstract String keepReportId_InScopeSubQuery_TReportChild(TReportChildCQ subQuery);
        public void InScopeTReportChildList(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_InScopeSubQuery_TReportChildList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "REPORT_ID", "PARENT_REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepReportId_InScopeSubQuery_TReportChildList(TReportChildCQ subQuery);
        public void NotInScopeTReportChild(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_NotInScopeSubQuery_TReportChild(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "REPORT_ID", "Parent_Report_ID", subQueryPropertyName);
        }
        public abstract String keepReportId_NotInScopeSubQuery_TReportChild(TReportChildCQ subQuery);
        public void NotInScopeTReportChildList(SubQuery<TReportChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_NotInScopeSubQuery_TReportChildList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "REPORT_ID", "PARENT_REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepReportId_NotInScopeSubQuery_TReportChildList(TReportChildCQ subQuery);
        public void xsderiveTReportChildList(String function, SubQuery<TReportChildCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_SpecifyDerivedReferrer_TReportChildList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "REPORT_ID", "PARENT_REPORT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepReportId_SpecifyDerivedReferrer_TReportChildList(TReportChildCQ subQuery);

        public QDRFunction<TReportChildCB> DerivedTReportChildList() {
            return xcreateQDRFunctionTReportChildList();
        }
        protected QDRFunction<TReportChildCB> xcreateQDRFunctionTReportChildList() {
            return new QDRFunction<TReportChildCB>(delegate(String function, SubQuery<TReportChildCB> subQuery, String operand, Object value) {
                xqderiveTReportChildList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTReportChildList(String function, SubQuery<TReportChildCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TReportChildCB>", subQuery);
            TReportChildCB cb = new TReportChildCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportId_QueryDerivedReferrer_TReportChildList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepReportId_QueryDerivedReferrer_TReportChildListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "REPORT_ID", "PARENT_REPORT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepReportId_QueryDerivedReferrer_TReportChildList(TReportChildCQ subQuery);
        public abstract String keepReportId_QueryDerivedReferrer_TReportChildListParameter(Object parameterValue);
        public void SetReportId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetReportId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regReportId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportId(), "REPORT_ID");
        }
        protected abstract ConditionValue getCValueReportId();

        public void SetReportsetId_Equal(decimal? v) { regReportsetId(CK_EQ, v); }
        public void SetReportsetId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_NES, v);
        }
        public void SetReportsetId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_GT, v);
        }
        public void SetReportsetId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_LT, v);
        }
        public void SetReportsetId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_GE, v);
        }
        public void SetReportsetId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regReportsetId(CK_LE, v);
        }
        public void SetReportsetId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueReportsetId(), "REPORTSET_ID");
        }
        public void SetReportsetId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueReportsetId(), "REPORTSET_ID");
        }
        public void InScopeTReportset(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_InScopeSubQuery_TReportset(cb.Query());
            registerInScopeSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_InScopeSubQuery_TReportset(TReportsetCQ subQuery);
        public void NotInScopeTReportset(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_NotInScopeSubQuery_TReportset(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_NotInScopeSubQuery_TReportset(TReportsetCQ subQuery);
        protected void regReportsetId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportsetId(), "REPORTSET_ID");
        }
        protected abstract ConditionValue getCValueReportsetId();

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

        public void SetChildDiv_Equal(int? v) { regChildDiv(CK_EQ, v); }
        public void SetChildDiv_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildDiv(CK_NES, v);
        }
        public void SetChildDiv_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildDiv(CK_GT, v);
        }
        public void SetChildDiv_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildDiv(CK_LT, v);
        }
        public void SetChildDiv_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildDiv(CK_GE, v);
        }
        public void SetChildDiv_LessEqual(int? v) {
            WhereSetterFlag = true;
            regChildDiv(CK_LE, v);
        }
        public void SetChildDiv_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueChildDiv(), "CHILD_DIV");
        }
        public void SetChildDiv_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueChildDiv(), "CHILD_DIV");
        }
        protected void regChildDiv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChildDiv(), "CHILD_DIV");
        }
        protected abstract ConditionValue getCValueChildDiv();

        public void SetScenarioType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_Equal(fRES(v));
        }
        protected void DoSetScenarioType_Equal(String v) { regScenarioType(CK_EQ, v); }
        public void SetScenarioType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_NotEqual(fRES(v));
        }
        protected void DoSetScenarioType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioType(CK_NES, v);
        }
        public void SetScenarioType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioType(CK_GT, fRES(v));
        }
        public void SetScenarioType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioType(CK_LT, fRES(v));
        }
        public void SetScenarioType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioType(CK_GE, fRES(v));
        }
        public void SetScenarioType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioType(CK_LE, fRES(v));
        }
        public void SetScenarioType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueScenarioType(), "SCENARIO_TYPE");
        }
        public void SetScenarioType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueScenarioType(), "SCENARIO_TYPE");
        }
        public void SetScenarioType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetScenarioType_LikeSearch(v, cLSOP());
        }
        public void SetScenarioType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueScenarioType(), "SCENARIO_TYPE", option);
        }
        public void SetScenarioType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueScenarioType(), "SCENARIO_TYPE", option);
        }
        protected void regScenarioType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioType(), "SCENARIO_TYPE");
        }
        protected abstract ConditionValue getCValueScenarioType();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TReportCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TReportCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TReportCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TReportCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TReportCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TReportCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TReportCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TReportCB>(delegate(String function, SubQuery<TReportCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TReportCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TReportCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "REPORT_ID", "REPORT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TReportCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
