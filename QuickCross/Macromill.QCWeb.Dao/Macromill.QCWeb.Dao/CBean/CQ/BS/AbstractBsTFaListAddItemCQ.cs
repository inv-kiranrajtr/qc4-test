
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
    public abstract class AbstractBsTFaListAddItemCQ : AbstractConditionQuery {

        public AbstractBsTFaListAddItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_FA_LIST_ADD_ITEM"; }
        public override String getTableSqlName() { return "T_FA_LIST_ADD_ITEM"; }

        public void SetFaListAddItemId_Equal(decimal? v) { regFaListAddItemId(CK_EQ, v); }
        public void SetFaListAddItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaListAddItemId(CK_NES, v);
        }
        public void SetFaListAddItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaListAddItemId(CK_GT, v);
        }
        public void SetFaListAddItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaListAddItemId(CK_LT, v);
        }
        public void SetFaListAddItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaListAddItemId(CK_GE, v);
        }
        public void SetFaListAddItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regFaListAddItemId(CK_LE, v);
        }
        public void SetFaListAddItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueFaListAddItemId(), "FA_LIST_ADD_ITEM_ID");
        }
        public void SetFaListAddItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueFaListAddItemId(), "FA_LIST_ADD_ITEM_ID");
        }
        public void SetFaListAddItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaListAddItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFaListAddItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaListAddItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFaListAddItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFaListAddItemId(), "FA_LIST_ADD_ITEM_ID");
        }
        protected abstract ConditionValue getCValueFaListAddItemId();

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
        public void InScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TItemInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TItemInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        protected void regItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueItemInfoId();

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

        public void SetLv2title_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLv2title_Equal(fRES(v));
        }
        protected void DoSetLv2title_Equal(String v) { regLv2title(CK_EQ, v); }
        public void SetLv2title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLv2title_NotEqual(fRES(v));
        }
        protected void DoSetLv2title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_NES, v);
        }
        public void SetLv2title_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_GT, fRES(v));
        }
        public void SetLv2title_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_LT, fRES(v));
        }
        public void SetLv2title_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_GE, fRES(v));
        }
        public void SetLv2title_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_LE, fRES(v));
        }
        public void SetLv2title_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLv2title(), "LV2TITLE");
        }
        public void SetLv2title_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLv2title(), "LV2TITLE");
        }
        public void SetLv2title_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLv2title_LikeSearch(v, cLSOP());
        }
        public void SetLv2title_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLv2title(), "LV2TITLE", option);
        }
        public void SetLv2title_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLv2title(), "LV2TITLE", option);
        }
        public void SetLv2title_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLv2title_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv2title(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLv2title(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLv2title(), "LV2TITLE");
        }
        protected abstract ConditionValue getCValueLv2title();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TFaListAddItemCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TFaListAddItemCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TFaListAddItemCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TFaListAddItemCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TFaListAddItemCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TFaListAddItemCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TFaListAddItemCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TFaListAddItemCB>(delegate(String function, SubQuery<TFaListAddItemCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TFaListAddItemCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TFaListAddItemCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "FA_LIST_ADD_ITEM_ID", "FA_LIST_ADD_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TFaListAddItemCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
