
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
    public abstract class AbstractBsTFaScenarioItemCQ : AbstractConditionQuery {

        public AbstractBsTFaScenarioItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_FA_SCENARIO_ITEM"; }
        public override String getTableSqlName() { return "T_FA_SCENARIO_ITEM"; }

        public void SetFaScenarioItemId_Equal(decimal? v) { regFaScenarioItemId(CK_EQ, v); }
        public void SetFaScenarioItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioItemId(CK_NES, v);
        }
        public void SetFaScenarioItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioItemId(CK_GT, v);
        }
        public void SetFaScenarioItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioItemId(CK_LT, v);
        }
        public void SetFaScenarioItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioItemId(CK_GE, v);
        }
        public void SetFaScenarioItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regFaScenarioItemId(CK_LE, v);
        }
        public void SetFaScenarioItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueFaScenarioItemId(), "FA_SCENARIO_ITEM_ID");
        }
        public void SetFaScenarioItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueFaScenarioItemId(), "FA_SCENARIO_ITEM_ID");
        }
        public void SetFaScenarioItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFaScenarioItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFaScenarioItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFaScenarioItemId(), "FA_SCENARIO_ITEM_ID");
        }
        protected abstract ConditionValue getCValueFaScenarioItemId();

        public void SetFaScenarioHeaderId_Equal(decimal? v) { regFaScenarioHeaderId(CK_EQ, v); }
        public void SetFaScenarioHeaderId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioHeaderId(CK_NES, v);
        }
        public void SetFaScenarioHeaderId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioHeaderId(CK_GT, v);
        }
        public void SetFaScenarioHeaderId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioHeaderId(CK_LT, v);
        }
        public void SetFaScenarioHeaderId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioHeaderId(CK_GE, v);
        }
        public void SetFaScenarioHeaderId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regFaScenarioHeaderId(CK_LE, v);
        }
        public void SetFaScenarioHeaderId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueFaScenarioHeaderId(), "FA_SCENARIO_HEADER_ID");
        }
        public void SetFaScenarioHeaderId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueFaScenarioHeaderId(), "FA_SCENARIO_HEADER_ID");
        }
        public void InScopeTFaScenarioHeader(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioHeader(cb.Query());
            registerInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioHeader(TFaScenarioHeaderCQ subQuery);
        public void NotInScopeTFaScenarioHeader(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeader(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioHeader(TFaScenarioHeaderCQ subQuery);
        protected void regFaScenarioHeaderId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFaScenarioHeaderId(), "FA_SCENARIO_HEADER_ID");
        }
        protected abstract ConditionValue getCValueFaScenarioHeaderId();

        public void SetFaTargetItemId_Equal(decimal? v) { regFaTargetItemId(CK_EQ, v); }
        public void SetFaTargetItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaTargetItemId(CK_NES, v);
        }
        public void SetFaTargetItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaTargetItemId(CK_GT, v);
        }
        public void SetFaTargetItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaTargetItemId(CK_LT, v);
        }
        public void SetFaTargetItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaTargetItemId(CK_GE, v);
        }
        public void SetFaTargetItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regFaTargetItemId(CK_LE, v);
        }
        public void SetFaTargetItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueFaTargetItemId(), "FA_TARGET_ITEM_ID");
        }
        public void SetFaTargetItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueFaTargetItemId(), "FA_TARGET_ITEM_ID");
        }
        public void InScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaTargetItemId_InScopeSubQuery_TItemInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "FA_TARGET_ITEM_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepFaTargetItemId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaTargetItemId_NotInScopeSubQuery_TItemInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "FA_TARGET_ITEM_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepFaTargetItemId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        protected void regFaTargetItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFaTargetItemId(), "FA_TARGET_ITEM_ID");
        }
        protected abstract ConditionValue getCValueFaTargetItemId();

        public void SetTitleString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleString_Equal(fRES(v));
        }
        protected void DoSetTitleString_Equal(String v) { regTitleString(CK_EQ, v); }
        public void SetTitleString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleString_NotEqual(fRES(v));
        }
        protected void DoSetTitleString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_NES, v);
        }
        public void SetTitleString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_GT, fRES(v));
        }
        public void SetTitleString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_LT, fRES(v));
        }
        public void SetTitleString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_GE, fRES(v));
        }
        public void SetTitleString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_LE, fRES(v));
        }
        public void SetTitleString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleString(), "TITLE_STRING");
        }
        public void SetTitleString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleString(), "TITLE_STRING");
        }
        public void SetTitleString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleString_LikeSearch(v, cLSOP());
        }
        public void SetTitleString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleString(), "TITLE_STRING", option);
        }
        public void SetTitleString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleString(), "TITLE_STRING", option);
        }
        public void SetTitleString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleString(), "TITLE_STRING");
        }
        protected abstract ConditionValue getCValueTitleString();

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
        public SSQFunction<TFaScenarioItemCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TFaScenarioItemCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TFaScenarioItemCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TFaScenarioItemCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TFaScenarioItemCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TFaScenarioItemCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TFaScenarioItemCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TFaScenarioItemCB>(delegate(String function, SubQuery<TFaScenarioItemCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TFaScenarioItemCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TFaScenarioItemCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "FA_SCENARIO_ITEM_ID", "FA_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TFaScenarioItemCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
