
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
    public abstract class AbstractBsTScenarioTotalizationCQ : AbstractConditionQuery {

        public AbstractBsTScenarioTotalizationCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_SCENARIO_TOTALIZATION"; }
        public override String getTableSqlName() { return "T_SCENARIO_TOTALIZATION"; }

        public void SetScenarioTotalizationId_Equal(decimal? v) { regScenarioTotalizationId(CK_EQ, v); }
        public void SetScenarioTotalizationId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_NES, v);
        }
        public void SetScenarioTotalizationId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_GT, v);
        }
        public void SetScenarioTotalizationId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_LT, v);
        }
        public void SetScenarioTotalizationId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_GE, v);
        }
        public void SetScenarioTotalizationId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regScenarioTotalizationId(CK_LE, v);
        }
        public void SetScenarioTotalizationId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        public void SetScenarioTotalizationId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        public void ExistsTCategoryOutputEditList(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery);
        public void ExistsTCrossScenarioTargetList(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery);
        public void ExistsTFaScenarioHeaderList(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery);
        public void ExistsTGtMatrixInfoList(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery);
        public void ExistsTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void ExistsTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void ExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_ExistsSubQuery_TItemInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "CATEGORY_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotExistsTCategoryOutputEditList(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery);
        public void NotExistsTCrossScenarioTargetList(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery);
        public void NotExistsTFaScenarioHeaderList(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery);
        public void NotExistsTGtMatrixInfoList(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery);
        public void NotExistsTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void NotExistsTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void NotExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotExistsSubQuery_TItemInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "CATEGORY_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void InScopeTGtScenarioItem(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery);
        public void InScopeTCategoryOutputEditList(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery);
        public void InScopeTCrossScenarioTargetList(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery);
        public void InScopeTFaScenarioHeaderList(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery);
        public void InScopeTGtMatrixInfoList(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery);
        public void InScopeTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void InScopeTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void InScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TItemInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "CATEGORY_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotInScopeTGtScenarioItem(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "Scenario_Totalization_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery);
        public void NotInScopeTCategoryOutputEditList(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery);
        public void NotInScopeTCrossScenarioTargetList(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery);
        public void NotInScopeTFaScenarioHeaderList(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery);
        public void NotInScopeTGtMatrixInfoList(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery);
        public void NotInScopeTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void NotInScopeTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void NotInScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "CATEGORY_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void xsderiveTCategoryOutputEditList(String function, SubQuery<TCategoryOutputEditCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery);
        public void xsderiveTCrossScenarioTargetList(String function, SubQuery<TCrossScenarioTargetCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery);
        public void xsderiveTFaScenarioHeaderList(String function, SubQuery<TFaScenarioHeaderCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery);
        public void xsderiveTGtMatrixInfoList(String function, SubQuery<TGtMatrixInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery);
        public void xsderiveTGtScenarioItemList(String function, SubQuery<TGtScenarioItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void xsderiveTScenarioQuerylistList(String function, SubQuery<TScenarioQuerylistCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void xsderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "CATEGORY_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepScenarioTotalizationId_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);

        public QDRFunction<TCategoryOutputEditCB> DerivedTCategoryOutputEditList() {
            return xcreateQDRFunctionTCategoryOutputEditList();
        }
        protected QDRFunction<TCategoryOutputEditCB> xcreateQDRFunctionTCategoryOutputEditList() {
            return new QDRFunction<TCategoryOutputEditCB>(delegate(String function, SubQuery<TCategoryOutputEditCB> subQuery, String operand, Object value) {
                xqderiveTCategoryOutputEditList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTCategoryOutputEditList(String function, SubQuery<TCategoryOutputEditCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditList(TCategoryOutputEditCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TCategoryOutputEditListParameter(Object parameterValue);

        public QDRFunction<TCrossScenarioTargetCB> DerivedTCrossScenarioTargetList() {
            return xcreateQDRFunctionTCrossScenarioTargetList();
        }
        protected QDRFunction<TCrossScenarioTargetCB> xcreateQDRFunctionTCrossScenarioTargetList() {
            return new QDRFunction<TCrossScenarioTargetCB>(delegate(String function, SubQuery<TCrossScenarioTargetCB> subQuery, String operand, Object value) {
                xqderiveTCrossScenarioTargetList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTCrossScenarioTargetList(String function, SubQuery<TCrossScenarioTargetCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetList(TCrossScenarioTargetCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TCrossScenarioTargetListParameter(Object parameterValue);

        public QDRFunction<TFaScenarioHeaderCB> DerivedTFaScenarioHeaderList() {
            return xcreateQDRFunctionTFaScenarioHeaderList();
        }
        protected QDRFunction<TFaScenarioHeaderCB> xcreateQDRFunctionTFaScenarioHeaderList() {
            return new QDRFunction<TFaScenarioHeaderCB>(delegate(String function, SubQuery<TFaScenarioHeaderCB> subQuery, String operand, Object value) {
                xqderiveTFaScenarioHeaderList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTFaScenarioHeaderList(String function, SubQuery<TFaScenarioHeaderCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderList(TFaScenarioHeaderCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TFaScenarioHeaderListParameter(Object parameterValue);

        public QDRFunction<TGtMatrixInfoCB> DerivedTGtMatrixInfoList() {
            return xcreateQDRFunctionTGtMatrixInfoList();
        }
        protected QDRFunction<TGtMatrixInfoCB> xcreateQDRFunctionTGtMatrixInfoList() {
            return new QDRFunction<TGtMatrixInfoCB>(delegate(String function, SubQuery<TGtMatrixInfoCB> subQuery, String operand, Object value) {
                xqderiveTGtMatrixInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTGtMatrixInfoList(String function, SubQuery<TGtMatrixInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoList(TGtMatrixInfoCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TGtMatrixInfoListParameter(Object parameterValue);

        public QDRFunction<TGtScenarioItemCB> DerivedTGtScenarioItemList() {
            return xcreateQDRFunctionTGtScenarioItemList();
        }
        protected QDRFunction<TGtScenarioItemCB> xcreateQDRFunctionTGtScenarioItemList() {
            return new QDRFunction<TGtScenarioItemCB>(delegate(String function, SubQuery<TGtScenarioItemCB> subQuery, String operand, Object value) {
                xqderiveTGtScenarioItemList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTGtScenarioItemList(String function, SubQuery<TGtScenarioItemCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TGtScenarioItemListParameter(Object parameterValue);

        public QDRFunction<TScenarioQuerylistCB> DerivedTScenarioQuerylistList() {
            return xcreateQDRFunctionTScenarioQuerylistList();
        }
        protected QDRFunction<TScenarioQuerylistCB> xcreateQDRFunctionTScenarioQuerylistList() {
            return new QDRFunction<TScenarioQuerylistCB>(delegate(String function, SubQuery<TScenarioQuerylistCB> subQuery, String operand, Object value) {
                xqderiveTScenarioQuerylistList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTScenarioQuerylistList(String function, SubQuery<TScenarioQuerylistCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TScenarioQuerylistListParameter(Object parameterValue);

        public QDRFunction<TItemInfoCB> DerivedTItemInfoList() {
            return xcreateQDRFunctionTItemInfoList();
        }
        protected QDRFunction<TItemInfoCB> xcreateQDRFunctionTItemInfoList() {
            return new QDRFunction<TItemInfoCB>(delegate(String function, SubQuery<TItemInfoCB> subQuery, String operand, Object value) {
                xqderiveTItemInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SCENARIO_TOTALIZATION_ID", "CATEGORY_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);
        public abstract String keepScenarioTotalizationId_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue);
        public void SetScenarioTotalizationId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetScenarioTotalizationId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regScenarioTotalizationId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        protected abstract ConditionValue getCValueScenarioTotalizationId();

        public void SetQcwebid_Equal(decimal? v) { regQcwebid(CK_EQ, v); }
        public void SetQcwebid_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_NES, v);
        }
        public void SetQcwebid_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_GT, v);
        }
        public void SetQcwebid_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_LT, v);
        }
        public void SetQcwebid_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_GE, v);
        }
        public void SetQcwebid_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regQcwebid(CK_LE, v);
        }
        public void SetQcwebid_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueQcwebid(), "QCWEBID");
        }
        public void SetQcwebid_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueQcwebid(), "QCWEBID");
        }
        public void InScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetScenarioType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of GT of scenarioType as equal. { = }
        /// GT: GTシナリオを示す
        /// </summary>
        public void SetScenarioType_Equal_GT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_Equal(CDef.ScenarioType.GT.Code);
        }
        /// <summary>
        /// Set the value of CROSS of scenarioType as equal. { = }
        /// CROSS: クロスシナリオを示す
        /// </summary>
        public void SetScenarioType_Equal_CROSS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_Equal(CDef.ScenarioType.CROSS.Code);
        }
        /// <summary>
        /// Set the value of FA of scenarioType as equal. { = }
        /// FA: FAシナリオを示す
        /// </summary>
        public void SetScenarioType_Equal_FA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_Equal(CDef.ScenarioType.FA.Code);
        }
        protected void DoSetScenarioType_Equal(String v) { regScenarioType(CK_EQ, v); }
        public void SetScenarioType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of GT of scenarioType as notEqual. { &lt;&gt; }
        /// GT: GTシナリオを示す
        /// </summary>
        public void SetScenarioType_NotEqual_GT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_NotEqual(CDef.ScenarioType.GT.Code);
        }
        /// <summary>
        /// Set the value of CROSS of scenarioType as notEqual. { &lt;&gt; }
        /// CROSS: クロスシナリオを示す
        /// </summary>
        public void SetScenarioType_NotEqual_CROSS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_NotEqual(CDef.ScenarioType.CROSS.Code);
        }
        /// <summary>
        /// Set the value of FA of scenarioType as notEqual. { &lt;&gt; }
        /// FA: FAシナリオを示す
        /// </summary>
        public void SetScenarioType_NotEqual_FA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioType_NotEqual(CDef.ScenarioType.FA.Code);
        }
        protected void DoSetScenarioType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioType(CK_NES, v);
        }
        public void SetScenarioType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueScenarioType(), "SCENARIO_TYPE");
        }
        public void SetScenarioType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueScenarioType(), "SCENARIO_TYPE");
        }
        protected void regScenarioType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioType(), "SCENARIO_TYPE");
        }
        protected abstract ConditionValue getCValueScenarioType();

        public void SetScenarioName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioName_Equal(fRES(v));
        }
        protected void DoSetScenarioName_Equal(String v) { regScenarioName(CK_EQ, v); }
        public void SetScenarioName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioName_NotEqual(fRES(v));
        }
        protected void DoSetScenarioName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_NES, v);
        }
        public void SetScenarioName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_GT, fRES(v));
        }
        public void SetScenarioName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_LT, fRES(v));
        }
        public void SetScenarioName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_GE, fRES(v));
        }
        public void SetScenarioName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_LE, fRES(v));
        }
        public void SetScenarioName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueScenarioName(), "SCENARIO_NAME");
        }
        public void SetScenarioName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueScenarioName(), "SCENARIO_NAME");
        }
        public void SetScenarioName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetScenarioName_LikeSearch(v, cLSOP());
        }
        public void SetScenarioName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueScenarioName(), "SCENARIO_NAME", option);
        }
        public void SetScenarioName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueScenarioName(), "SCENARIO_NAME", option);
        }
        protected void regScenarioName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioName(), "SCENARIO_NAME");
        }
        protected abstract ConditionValue getCValueScenarioName();

        public void SetConditionDiv_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of AND of conditionDiv as equal. { = }
        /// &: ANDを示す
        /// </summary>
        public void SetConditionDiv_Equal_AND() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_Equal(CDef.ConditionDiv.AND.Code);
        }
        /// <summary>
        /// Set the value of OR of conditionDiv as equal. { = }
        /// |: ORを示す
        /// </summary>
        public void SetConditionDiv_Equal_OR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_Equal(CDef.ConditionDiv.OR.Code);
        }
        protected void DoSetConditionDiv_Equal(String v) { regConditionDiv(CK_EQ, v); }
        public void SetConditionDiv_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of AND of conditionDiv as notEqual. { &lt;&gt; }
        /// &: ANDを示す
        /// </summary>
        public void SetConditionDiv_NotEqual_AND() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_NotEqual(CDef.ConditionDiv.AND.Code);
        }
        /// <summary>
        /// Set the value of OR of conditionDiv as notEqual. { &lt;&gt; }
        /// |: ORを示す
        /// </summary>
        public void SetConditionDiv_NotEqual_OR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_NotEqual(CDef.ConditionDiv.OR.Code);
        }
        protected void DoSetConditionDiv_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionDiv(CK_NES, v);
        }
        public void SetConditionDiv_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueConditionDiv(), "CONDITION_DIV");
        }
        public void SetConditionDiv_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueConditionDiv(), "CONDITION_DIV");
        }
        protected void regConditionDiv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionDiv(), "CONDITION_DIV");
        }
        protected abstract ConditionValue getCValueConditionDiv();

        public void SetFilterFlag_Equal(int? v) { regFilterFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of filterFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetFilterFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFilterFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of filterFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFilterFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFilterFlag(CK_EQ, int.Parse(code));
        }
        public void SetFilterFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilterFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of filterFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetFilterFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFilterFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of filterFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFilterFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFilterFlag(CK_NES, int.Parse(code));
        }
        public void SetFilterFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFilterFlag(), "FILTER_FLAG");
        }
        public void SetFilterFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFilterFlag(), "FILTER_FLAG");
        }
        protected void regFilterFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFilterFlag(), "FILTER_FLAG");
        }
        protected abstract ConditionValue getCValueFilterFlag();

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

        public void SetWeightbackFlag_Equal(int? v) { regWeightbackFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of weightbackFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetWeightbackFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regWeightbackFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of weightbackFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetWeightbackFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regWeightbackFlag(CK_EQ, int.Parse(code));
        }
        public void SetWeightbackFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of weightbackFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetWeightbackFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regWeightbackFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of weightbackFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetWeightbackFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regWeightbackFlag(CK_NES, int.Parse(code));
        }
        public void SetWeightbackFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueWeightbackFlag(), "WEIGHTBACK_FLAG");
        }
        public void SetWeightbackFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueWeightbackFlag(), "WEIGHTBACK_FLAG");
        }
        protected void regWeightbackFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackFlag(), "WEIGHTBACK_FLAG");
        }
        protected abstract ConditionValue getCValueWeightbackFlag();

        public void SetWeightbackCode_Equal(decimal? v) { regWeightbackCode(CK_EQ, v); }
        public void SetWeightbackCode_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackCode(CK_NES, v);
        }
        public void SetWeightbackCode_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackCode(CK_GT, v);
        }
        public void SetWeightbackCode_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackCode(CK_LT, v);
        }
        public void SetWeightbackCode_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackCode(CK_GE, v);
        }
        public void SetWeightbackCode_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regWeightbackCode(CK_LE, v);
        }
        public void SetWeightbackCode_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueWeightbackCode(), "WEIGHTBACK_CODE");
        }
        public void SetWeightbackCode_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueWeightbackCode(), "WEIGHTBACK_CODE");
        }
        public void SetWeightbackCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetWeightbackCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regWeightbackCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackCode(), "WEIGHTBACK_CODE");
        }
        protected abstract ConditionValue getCValueWeightbackCode();

        public void SetTotalnumFlag_Equal(int? v) { regTotalnumFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of totalnumFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTotalnumFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTotalnumFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of totalnumFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTotalnumFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTotalnumFlag(CK_EQ, int.Parse(code));
        }
        public void SetTotalnumFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalnumFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of totalnumFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTotalnumFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTotalnumFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of totalnumFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTotalnumFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTotalnumFlag(CK_NES, int.Parse(code));
        }
        public void SetTotalnumFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTotalnumFlag(), "TOTALNUM_FLAG");
        }
        public void SetTotalnumFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTotalnumFlag(), "TOTALNUM_FLAG");
        }
        protected void regTotalnumFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTotalnumFlag(), "TOTALNUM_FLAG");
        }
        protected abstract ConditionValue getCValueTotalnumFlag();

        public void SetGraphOutputFlag_Equal(int? v) { regGraphOutputFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of graphOutputFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetGraphOutputFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regGraphOutputFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of graphOutputFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetGraphOutputFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regGraphOutputFlag(CK_EQ, int.Parse(code));
        }
        public void SetGraphOutputFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphOutputFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of graphOutputFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetGraphOutputFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regGraphOutputFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of graphOutputFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetGraphOutputFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regGraphOutputFlag(CK_NES, int.Parse(code));
        }
        public void SetGraphOutputFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueGraphOutputFlag(), "GRAPH_OUTPUT_FLAG");
        }
        public void SetGraphOutputFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueGraphOutputFlag(), "GRAPH_OUTPUT_FLAG");
        }
        protected void regGraphOutputFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphOutputFlag(), "GRAPH_OUTPUT_FLAG");
        }
        protected abstract ConditionValue getCValueGraphOutputFlag();

        public void SetPieChartChoiceFlag_Equal(int? v) { regPieChartChoiceFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of pieChartChoiceFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPieChartChoiceFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPieChartChoiceFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pieChartChoiceFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPieChartChoiceFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPieChartChoiceFlag(CK_EQ, int.Parse(code));
        }
        public void SetPieChartChoiceFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPieChartChoiceFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of pieChartChoiceFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPieChartChoiceFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPieChartChoiceFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pieChartChoiceFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPieChartChoiceFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPieChartChoiceFlag(CK_NES, int.Parse(code));
        }
        public void SetPieChartChoiceFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePieChartChoiceFlag(), "PIE_CHART_CHOICE_FLAG");
        }
        public void SetPieChartChoiceFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePieChartChoiceFlag(), "PIE_CHART_CHOICE_FLAG");
        }
        public void SetPieChartChoiceFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPieChartChoiceFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPieChartChoiceFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPieChartChoiceFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPieChartChoiceFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePieChartChoiceFlag(), "PIE_CHART_CHOICE_FLAG");
        }
        protected abstract ConditionValue getCValuePieChartChoiceFlag();

        public void SetMinimumRate_Equal(int? v) { regMinimumRate(CK_EQ, v); }
        public void SetMinimumRate_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMinimumRate(CK_NES, v);
        }
        public void SetMinimumRate_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMinimumRate(CK_GT, v);
        }
        public void SetMinimumRate_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMinimumRate(CK_LT, v);
        }
        public void SetMinimumRate_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMinimumRate(CK_GE, v);
        }
        public void SetMinimumRate_LessEqual(int? v) {
            WhereSetterFlag = true;
            regMinimumRate(CK_LE, v);
        }
        public void SetMinimumRate_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMinimumRate(), "MINIMUM_RATE");
        }
        public void SetMinimumRate_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMinimumRate(), "MINIMUM_RATE");
        }
        public void SetMinimumRate_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMinimumRate(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMinimumRate_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMinimumRate(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMinimumRate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMinimumRate(), "MINIMUM_RATE");
        }
        protected abstract ConditionValue getCValueMinimumRate();

        public void SetAxisNoanswerOnoff_Equal(int? v) { regAxisNoanswerOnoff(CK_EQ, v); }
        public void SetAxisNoanswerOnoff_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisNoanswerOnoff(CK_NES, v);
        }
        public void SetAxisNoanswerOnoff_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisNoanswerOnoff(CK_GT, v);
        }
        public void SetAxisNoanswerOnoff_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisNoanswerOnoff(CK_LT, v);
        }
        public void SetAxisNoanswerOnoff_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisNoanswerOnoff(CK_GE, v);
        }
        public void SetAxisNoanswerOnoff_LessEqual(int? v) {
            WhereSetterFlag = true;
            regAxisNoanswerOnoff(CK_LE, v);
        }
        public void SetAxisNoanswerOnoff_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueAxisNoanswerOnoff(), "AXIS_NOANSWER_ONOFF");
        }
        public void SetAxisNoanswerOnoff_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueAxisNoanswerOnoff(), "AXIS_NOANSWER_ONOFF");
        }
        public void SetAxisNoanswerOnoff_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisNoanswerOnoff(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAxisNoanswerOnoff_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisNoanswerOnoff(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAxisNoanswerOnoff(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAxisNoanswerOnoff(), "AXIS_NOANSWER_ONOFF");
        }
        protected abstract ConditionValue getCValueAxisNoanswerOnoff();

        public void SetTargetNoanswerOnoff_Equal(int? v) { regTargetNoanswerOnoff(CK_EQ, v); }
        public void SetTargetNoanswerOnoff_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetNoanswerOnoff(CK_NES, v);
        }
        public void SetTargetNoanswerOnoff_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetNoanswerOnoff(CK_GT, v);
        }
        public void SetTargetNoanswerOnoff_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetNoanswerOnoff(CK_LT, v);
        }
        public void SetTargetNoanswerOnoff_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetNoanswerOnoff(CK_GE, v);
        }
        public void SetTargetNoanswerOnoff_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTargetNoanswerOnoff(CK_LE, v);
        }
        public void SetTargetNoanswerOnoff_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTargetNoanswerOnoff(), "TARGET_NOANSWER_ONOFF");
        }
        public void SetTargetNoanswerOnoff_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTargetNoanswerOnoff(), "TARGET_NOANSWER_ONOFF");
        }
        public void SetTargetNoanswerOnoff_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetNoanswerOnoff(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTargetNoanswerOnoff_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetNoanswerOnoff(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTargetNoanswerOnoff(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTargetNoanswerOnoff(), "TARGET_NOANSWER_ONOFF");
        }
        protected abstract ConditionValue getCValueTargetNoanswerOnoff();

        public void SetPolylineOnoff_Equal(int? v) { regPolylineOnoff(CK_EQ, v); }
        public void SetPolylineOnoff_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineOnoff(CK_NES, v);
        }
        public void SetPolylineOnoff_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineOnoff(CK_GT, v);
        }
        public void SetPolylineOnoff_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineOnoff(CK_LT, v);
        }
        public void SetPolylineOnoff_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineOnoff(CK_GE, v);
        }
        public void SetPolylineOnoff_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPolylineOnoff(CK_LE, v);
        }
        public void SetPolylineOnoff_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePolylineOnoff(), "POLYLINE_ONOFF");
        }
        public void SetPolylineOnoff_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePolylineOnoff(), "POLYLINE_ONOFF");
        }
        public void SetPolylineOnoff_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineOnoff(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPolylineOnoff_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineOnoff(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPolylineOnoff(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePolylineOnoff(), "POLYLINE_ONOFF");
        }
        protected abstract ConditionValue getCValuePolylineOnoff();

        public void SetMarkingN_Equal(long? v) { regMarkingN(CK_EQ, v); }
        public void SetMarkingN_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingN(CK_NES, v);
        }
        public void SetMarkingN_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingN(CK_GT, v);
        }
        public void SetMarkingN_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingN(CK_LT, v);
        }
        public void SetMarkingN_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingN(CK_GE, v);
        }
        public void SetMarkingN_LessEqual(long? v) {
            WhereSetterFlag = true;
            regMarkingN(CK_LE, v);
        }
        public void SetMarkingN_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueMarkingN(), "MARKING_N");
        }
        public void SetMarkingN_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueMarkingN(), "MARKING_N");
        }
        public void SetMarkingN_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingN(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMarkingN_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingN(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMarkingN(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMarkingN(), "MARKING_N");
        }
        protected abstract ConditionValue getCValueMarkingN();

        public void SetRankingFlag_Equal(int? v) { regRankingFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of rankingFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetRankingFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRankingFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rankingFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRankingFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRankingFlag(CK_EQ, int.Parse(code));
        }
        public void SetRankingFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRankingFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of rankingFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetRankingFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRankingFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rankingFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRankingFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRankingFlag(CK_NES, int.Parse(code));
        }
        public void SetRankingFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRankingFlag(), "RANKING_FLAG");
        }
        public void SetRankingFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRankingFlag(), "RANKING_FLAG");
        }
        public void SetRankingFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRankingFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRankingFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRankingFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRankingFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRankingFlag(), "RANKING_FLAG");
        }
        protected abstract ConditionValue getCValueRankingFlag();

        public void SetRateFlag_Equal(int? v) { regRateFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of rateFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetRateFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRateFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rateFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRateFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRateFlag(CK_EQ, int.Parse(code));
        }
        public void SetRateFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of rateFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetRateFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRateFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rateFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRateFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRateFlag(CK_NES, int.Parse(code));
        }
        public void SetRateFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRateFlag(), "RATE_FLAG");
        }
        public void SetRateFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRateFlag(), "RATE_FLAG");
        }
        public void SetRateFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRateFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRateFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRateFlag(), "RATE_FLAG");
        }
        protected abstract ConditionValue getCValueRateFlag();

        public void SetRate1Flag_Equal(int? v) { regRate1Flag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of rate1Flag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetRate1Flag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRate1Flag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rate1Flag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRate1Flag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRate1Flag(CK_EQ, int.Parse(code));
        }
        public void SetRate1Flag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Flag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of rate1Flag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetRate1Flag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRate1Flag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rate1Flag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRate1Flag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRate1Flag(CK_NES, int.Parse(code));
        }
        public void SetRate1Flag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate1Flag(), "RATE1_FLAG");
        }
        public void SetRate1Flag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate1Flag(), "RATE1_FLAG");
        }
        public void SetRate1Flag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Flag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate1Flag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Flag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate1Flag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate1Flag(), "RATE1_FLAG");
        }
        protected abstract ConditionValue getCValueRate1Flag();

        public void SetRate1Sign_Equal(int? v) { regRate1Sign(CK_EQ, v); }
        /// <summary>
        /// Set the value of PlusAndMinus of rate1Sign as equal. { = }
        /// ±: プラス・マイナスを示す
        /// </summary>
        public void SetRate1Sign_Equal_PlusAndMinus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.PlusAndMinus.Code;
            regRate1Sign(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Plus of rate1Sign as equal. { = }
        /// +: プラスを示す
        /// </summary>
        public void SetRate1Sign_Equal_Plus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Plus.Code;
            regRate1Sign(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Minus of rate1Sign as equal. { = }
        /// -: マイナスを示す
        /// </summary>
        public void SetRate1Sign_Equal_Minus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Minus.Code;
            regRate1Sign(CK_EQ, int.Parse(code));
        }
        public void SetRate1Sign_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Sign(CK_NES, v);
        }
        /// <summary>
        /// Set the value of PlusAndMinus of rate1Sign as notEqual. { &lt;&gt; }
        /// ±: プラス・マイナスを示す
        /// </summary>
        public void SetRate1Sign_NotEqual_PlusAndMinus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.PlusAndMinus.Code;
            regRate1Sign(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Plus of rate1Sign as notEqual. { &lt;&gt; }
        /// +: プラスを示す
        /// </summary>
        public void SetRate1Sign_NotEqual_Plus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Plus.Code;
            regRate1Sign(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Minus of rate1Sign as notEqual. { &lt;&gt; }
        /// -: マイナスを示す
        /// </summary>
        public void SetRate1Sign_NotEqual_Minus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Minus.Code;
            regRate1Sign(CK_NES, int.Parse(code));
        }
        public void SetRate1Sign_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate1Sign(), "RATE1_SIGN");
        }
        public void SetRate1Sign_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate1Sign(), "RATE1_SIGN");
        }
        public void SetRate1Sign_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Sign(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate1Sign_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Sign(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate1Sign(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate1Sign(), "RATE1_SIGN");
        }
        protected abstract ConditionValue getCValueRate1Sign();

        public void SetRate1Range_Equal(long? v) { regRate1Range(CK_EQ, v); }
        public void SetRate1Range_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Range(CK_NES, v);
        }
        public void SetRate1Range_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Range(CK_GT, v);
        }
        public void SetRate1Range_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Range(CK_LT, v);
        }
        public void SetRate1Range_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Range(CK_GE, v);
        }
        public void SetRate1Range_LessEqual(long? v) {
            WhereSetterFlag = true;
            regRate1Range(CK_LE, v);
        }
        public void SetRate1Range_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueRate1Range(), "RATE1_RANGE");
        }
        public void SetRate1Range_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueRate1Range(), "RATE1_RANGE");
        }
        public void SetRate1Range_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Range(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate1Range_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Range(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate1Range(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate1Range(), "RATE1_RANGE");
        }
        protected abstract ConditionValue getCValueRate1Range();

        public void SetRate1Backcolor1_Equal(int? v) { regRate1Backcolor1(CK_EQ, v); }
        public void SetRate1Backcolor1_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor1(CK_NES, v);
        }
        public void SetRate1Backcolor1_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor1(CK_GT, v);
        }
        public void SetRate1Backcolor1_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor1(CK_LT, v);
        }
        public void SetRate1Backcolor1_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor1(CK_GE, v);
        }
        public void SetRate1Backcolor1_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRate1Backcolor1(CK_LE, v);
        }
        public void SetRate1Backcolor1_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate1Backcolor1(), "RATE1_BACKCOLOR1");
        }
        public void SetRate1Backcolor1_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate1Backcolor1(), "RATE1_BACKCOLOR1");
        }
        public void SetRate1Backcolor1_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor1(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate1Backcolor1_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor1(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate1Backcolor1(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate1Backcolor1(), "RATE1_BACKCOLOR1");
        }
        protected abstract ConditionValue getCValueRate1Backcolor1();

        public void SetRate1Backcolor2_Equal(int? v) { regRate1Backcolor2(CK_EQ, v); }
        public void SetRate1Backcolor2_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor2(CK_NES, v);
        }
        public void SetRate1Backcolor2_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor2(CK_GT, v);
        }
        public void SetRate1Backcolor2_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor2(CK_LT, v);
        }
        public void SetRate1Backcolor2_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor2(CK_GE, v);
        }
        public void SetRate1Backcolor2_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRate1Backcolor2(CK_LE, v);
        }
        public void SetRate1Backcolor2_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate1Backcolor2(), "RATE1_BACKCOLOR2");
        }
        public void SetRate1Backcolor2_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate1Backcolor2(), "RATE1_BACKCOLOR2");
        }
        public void SetRate1Backcolor2_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor2(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate1Backcolor2_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate1Backcolor2(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate1Backcolor2(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate1Backcolor2(), "RATE1_BACKCOLOR2");
        }
        protected abstract ConditionValue getCValueRate1Backcolor2();

        public void SetRate2Flag_Equal(int? v) { regRate2Flag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of rate2Flag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetRate2Flag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRate2Flag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rate2Flag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRate2Flag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRate2Flag(CK_EQ, int.Parse(code));
        }
        public void SetRate2Flag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Flag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of rate2Flag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetRate2Flag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regRate2Flag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of rate2Flag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetRate2Flag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regRate2Flag(CK_NES, int.Parse(code));
        }
        public void SetRate2Flag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate2Flag(), "RATE2_FLAG");
        }
        public void SetRate2Flag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate2Flag(), "RATE2_FLAG");
        }
        public void SetRate2Flag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Flag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate2Flag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Flag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate2Flag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate2Flag(), "RATE2_FLAG");
        }
        protected abstract ConditionValue getCValueRate2Flag();

        public void SetRate2Sign_Equal(int? v) { regRate2Sign(CK_EQ, v); }
        /// <summary>
        /// Set the value of PlusAndMinus of rate2Sign as equal. { = }
        /// ±: プラス・マイナスを示す
        /// </summary>
        public void SetRate2Sign_Equal_PlusAndMinus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.PlusAndMinus.Code;
            regRate2Sign(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Plus of rate2Sign as equal. { = }
        /// +: プラスを示す
        /// </summary>
        public void SetRate2Sign_Equal_Plus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Plus.Code;
            regRate2Sign(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Minus of rate2Sign as equal. { = }
        /// -: マイナスを示す
        /// </summary>
        public void SetRate2Sign_Equal_Minus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Minus.Code;
            regRate2Sign(CK_EQ, int.Parse(code));
        }
        public void SetRate2Sign_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Sign(CK_NES, v);
        }
        /// <summary>
        /// Set the value of PlusAndMinus of rate2Sign as notEqual. { &lt;&gt; }
        /// ±: プラス・マイナスを示す
        /// </summary>
        public void SetRate2Sign_NotEqual_PlusAndMinus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.PlusAndMinus.Code;
            regRate2Sign(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Plus of rate2Sign as notEqual. { &lt;&gt; }
        /// +: プラスを示す
        /// </summary>
        public void SetRate2Sign_NotEqual_Plus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Plus.Code;
            regRate2Sign(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Minus of rate2Sign as notEqual. { &lt;&gt; }
        /// -: マイナスを示す
        /// </summary>
        public void SetRate2Sign_NotEqual_Minus() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.RateSign.Minus.Code;
            regRate2Sign(CK_NES, int.Parse(code));
        }
        public void SetRate2Sign_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate2Sign(), "RATE2_SIGN");
        }
        public void SetRate2Sign_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate2Sign(), "RATE2_SIGN");
        }
        public void SetRate2Sign_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Sign(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate2Sign_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Sign(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate2Sign(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate2Sign(), "RATE2_SIGN");
        }
        protected abstract ConditionValue getCValueRate2Sign();

        public void SetRate2Range_Equal(long? v) { regRate2Range(CK_EQ, v); }
        public void SetRate2Range_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Range(CK_NES, v);
        }
        public void SetRate2Range_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Range(CK_GT, v);
        }
        public void SetRate2Range_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Range(CK_LT, v);
        }
        public void SetRate2Range_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Range(CK_GE, v);
        }
        public void SetRate2Range_LessEqual(long? v) {
            WhereSetterFlag = true;
            regRate2Range(CK_LE, v);
        }
        public void SetRate2Range_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueRate2Range(), "RATE2_RANGE");
        }
        public void SetRate2Range_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueRate2Range(), "RATE2_RANGE");
        }
        public void SetRate2Range_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Range(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate2Range_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Range(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate2Range(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate2Range(), "RATE2_RANGE");
        }
        protected abstract ConditionValue getCValueRate2Range();

        public void SetRate2Backcolor1_Equal(int? v) { regRate2Backcolor1(CK_EQ, v); }
        public void SetRate2Backcolor1_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor1(CK_NES, v);
        }
        public void SetRate2Backcolor1_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor1(CK_GT, v);
        }
        public void SetRate2Backcolor1_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor1(CK_LT, v);
        }
        public void SetRate2Backcolor1_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor1(CK_GE, v);
        }
        public void SetRate2Backcolor1_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRate2Backcolor1(CK_LE, v);
        }
        public void SetRate2Backcolor1_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate2Backcolor1(), "RATE2_BACKCOLOR1");
        }
        public void SetRate2Backcolor1_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate2Backcolor1(), "RATE2_BACKCOLOR1");
        }
        public void SetRate2Backcolor1_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor1(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate2Backcolor1_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor1(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate2Backcolor1(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate2Backcolor1(), "RATE2_BACKCOLOR1");
        }
        protected abstract ConditionValue getCValueRate2Backcolor1();

        public void SetRate2Backcolor2_Equal(int? v) { regRate2Backcolor2(CK_EQ, v); }
        public void SetRate2Backcolor2_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor2(CK_NES, v);
        }
        public void SetRate2Backcolor2_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor2(CK_GT, v);
        }
        public void SetRate2Backcolor2_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor2(CK_LT, v);
        }
        public void SetRate2Backcolor2_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor2(CK_GE, v);
        }
        public void SetRate2Backcolor2_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRate2Backcolor2(CK_LE, v);
        }
        public void SetRate2Backcolor2_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRate2Backcolor2(), "RATE2_BACKCOLOR2");
        }
        public void SetRate2Backcolor2_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRate2Backcolor2(), "RATE2_BACKCOLOR2");
        }
        public void SetRate2Backcolor2_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor2(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRate2Backcolor2_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRate2Backcolor2(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRate2Backcolor2(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRate2Backcolor2(), "RATE2_BACKCOLOR2");
        }
        protected abstract ConditionValue getCValueRate2Backcolor2();

        public void SetLastUpdateUser_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastUpdateUser_Equal(fRES(v));
        }
        protected void DoSetLastUpdateUser_Equal(String v) { regLastUpdateUser(CK_EQ, v); }
        public void SetLastUpdateUser_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastUpdateUser_NotEqual(fRES(v));
        }
        protected void DoSetLastUpdateUser_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_NES, v);
        }
        public void SetLastUpdateUser_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_GT, fRES(v));
        }
        public void SetLastUpdateUser_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_LT, fRES(v));
        }
        public void SetLastUpdateUser_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_GE, fRES(v));
        }
        public void SetLastUpdateUser_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_LE, fRES(v));
        }
        public void SetLastUpdateUser_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        public void SetLastUpdateUser_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        public void SetLastUpdateUser_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastUpdateUser_LikeSearch(v, cLSOP());
        }
        public void SetLastUpdateUser_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLastUpdateUser(), "LAST_UPDATE_USER", option);
        }
        public void SetLastUpdateUser_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLastUpdateUser(), "LAST_UPDATE_USER", option);
        }
        public void SetLastUpdateUser_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastUpdateUser_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastUpdateUser(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        protected abstract ConditionValue getCValueLastUpdateUser();

        public void SetLastUpdateDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_EQ, v);
        }
        public void SetLastUpdateDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_GT, v);
        }
        public void SetLastUpdateDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_LT, v);
        }
        public void SetLastUpdateDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_GE, v);
        }
        public void SetLastUpdateDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_LE, v);
        }
        public void SetLastUpdateDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueLastUpdateDatetime(), "LAST_UPDATE_DATETIME", option);
        }
        public void SetLastUpdateDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastUpdateDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetLastUpdateDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastUpdateDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastUpdateDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastUpdateDatetime(), "LAST_UPDATE_DATETIME");
        }
        protected abstract ConditionValue getCValueLastUpdateDatetime();

        public void SetTestFlag_Equal(int? v) { regTestFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of testFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestFlag(CK_EQ, int.Parse(code));
        }
        public void SetTestFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of testFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestFlag(CK_NES, int.Parse(code));
        }
        public void SetTestFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestFlag(), "TEST_FLAG");
        }
        public void SetTestFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestFlag(), "TEST_FLAG");
        }
        public void SetTestFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestFlag(), "TEST_FLAG");
        }
        protected abstract ConditionValue getCValueTestFlag();

        public void SetTestType_Equal(int? v) { regTestType(CK_EQ, v); }
        public void SetTestType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestType(CK_NES, v);
        }
        public void SetTestType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestType(CK_GT, v);
        }
        public void SetTestType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestType(CK_LT, v);
        }
        public void SetTestType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestType(CK_GE, v);
        }
        public void SetTestType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestType(CK_LE, v);
        }
        public void SetTestType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestType(), "TEST_TYPE");
        }
        public void SetTestType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestType(), "TEST_TYPE");
        }
        public void SetTestType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestType(), "TEST_TYPE");
        }
        protected abstract ConditionValue getCValueTestType();

        public void SetTestSignificanceLv_Equal(int? v) { regTestSignificanceLv(CK_EQ, v); }
        public void SetTestSignificanceLv_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLv(CK_NES, v);
        }
        public void SetTestSignificanceLv_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLv(CK_GT, v);
        }
        public void SetTestSignificanceLv_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLv(CK_LT, v);
        }
        public void SetTestSignificanceLv_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLv(CK_GE, v);
        }
        public void SetTestSignificanceLv_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestSignificanceLv(CK_LE, v);
        }
        public void SetTestSignificanceLv_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestSignificanceLv(), "TEST_SIGNIFICANCE_LV");
        }
        public void SetTestSignificanceLv_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestSignificanceLv(), "TEST_SIGNIFICANCE_LV");
        }
        public void SetTestSignificanceLv_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLv(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestSignificanceLv_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLv(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestSignificanceLv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestSignificanceLv(), "TEST_SIGNIFICANCE_LV");
        }
        protected abstract ConditionValue getCValueTestSignificanceLv();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TScenarioTotalizationCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TScenarioTotalizationCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TScenarioTotalizationCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TScenarioTotalizationCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TScenarioTotalizationCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TScenarioTotalizationCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TScenarioTotalizationCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TScenarioTotalizationCB>(delegate(String function, SubQuery<TScenarioTotalizationCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TScenarioTotalizationCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TScenarioTotalizationCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TScenarioTotalizationCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
