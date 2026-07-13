
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
    public abstract class AbstractBsTItemInfoCQ : AbstractConditionQuery {

        public AbstractBsTItemInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_ITEM_INFO"; }
        public override String getTableSqlName() { return "T_ITEM_INFO"; }

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
        public void ExistsTCategoryInfoList(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TCategoryInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery);
        public void ExistsTMatrixInfoByItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery);
        public void ExistsTMatrixInfoByChildItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery);
        public void ExistsTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TScenarioQuerylistList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void ExistsTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TGtScenarioItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void ExistsTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TFaScenarioItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "FA_Target_Item_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void ExistsTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TFaListAddItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void ExistsTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_ExistsSubQuery_TGtMatrixChildList(cb.Query());
            registerExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_ExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void NotExistsTCategoryInfoList(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TCategoryInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery);
        public void NotExistsTMatrixInfoByItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery);
        public void NotExistsTMatrixInfoByChildItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery);
        public void NotExistsTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TScenarioQuerylistList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void NotExistsTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TGtScenarioItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void NotExistsTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TFaScenarioItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "FA_Target_Item_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void NotExistsTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TFaListAddItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void NotExistsTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotExistsSubQuery_TGtMatrixChildList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void InScopeTMatrixInfo(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TMatrixInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "Child_Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TMatrixInfo(TMatrixInfoCQ subQuery);
        public void InScopeTCategoryInfoList(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TCategoryInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery);
        public void InScopeTMatrixInfoByItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery);
        public void InScopeTMatrixInfoByChildItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery);
        public void InScopeTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TScenarioQuerylistList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void InScopeTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TGtScenarioItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void InScopeTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TFaScenarioItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "FA_Target_Item_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void InScopeTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TFaListAddItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void InScopeTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TGtMatrixChildList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void NotInScopeTMatrixInfo(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TMatrixInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "Child_Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TMatrixInfo(TMatrixInfoCQ subQuery);
        public void NotInScopeTCategoryInfoList(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TCategoryInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TCategoryInfoList(TCategoryInfoCQ subQuery);
        public void NotInScopeTMatrixInfoByItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery);
        public void NotInScopeTMatrixInfoByChildItemInfoIdList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery);
        public void NotInScopeTScenarioQuerylistList(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void NotInScopeTGtScenarioItemList(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TGtScenarioItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void NotInScopeTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TFaScenarioItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "FA_Target_Item_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void NotInScopeTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TFaListAddItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void NotInScopeTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TGtMatrixChildList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void xsderiveTCategoryInfoList(String function, SubQuery<TCategoryInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TCategoryInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TCategoryInfoList(TCategoryInfoCQ subQuery);
        public void xsderiveTMatrixInfoByItemInfoIdList(String function, SubQuery<TMatrixInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery);
        public void xsderiveTMatrixInfoByChildItemInfoIdList(String function, SubQuery<TMatrixInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery);
        public void xsderiveTScenarioQuerylistList(String function, SubQuery<TScenarioQuerylistCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public void xsderiveTGtScenarioItemList(String function, SubQuery<TGtScenarioItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TGtScenarioItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public void xsderiveTFaScenarioItemList(String function, SubQuery<TFaScenarioItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TFaScenarioItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "FA_Target_Item_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void xsderiveTFaListAddItemList(String function, SubQuery<TFaListAddItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TFaListAddItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void xsderiveTGtMatrixChildList(String function, SubQuery<TGtMatrixChildCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepItemInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery);

        public QDRFunction<TCategoryInfoCB> DerivedTCategoryInfoList() {
            return xcreateQDRFunctionTCategoryInfoList();
        }
        protected QDRFunction<TCategoryInfoCB> xcreateQDRFunctionTCategoryInfoList() {
            return new QDRFunction<TCategoryInfoCB>(delegate(String function, SubQuery<TCategoryInfoCB> subQuery, String operand, Object value) {
                xqderiveTCategoryInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTCategoryInfoList(String function, SubQuery<TCategoryInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TCategoryInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TCategoryInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TCategoryInfoList(TCategoryInfoCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TCategoryInfoListParameter(Object parameterValue);

        public QDRFunction<TMatrixInfoCB> DerivedTMatrixInfoByItemInfoIdList() {
            return xcreateQDRFunctionTMatrixInfoByItemInfoIdList();
        }
        protected QDRFunction<TMatrixInfoCB> xcreateQDRFunctionTMatrixInfoByItemInfoIdList() {
            return new QDRFunction<TMatrixInfoCB>(delegate(String function, SubQuery<TMatrixInfoCB> subQuery, String operand, Object value) {
                xqderiveTMatrixInfoByItemInfoIdList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTMatrixInfoByItemInfoIdList(String function, SubQuery<TMatrixInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdList(TMatrixInfoCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByItemInfoIdListParameter(Object parameterValue);

        public QDRFunction<TMatrixInfoCB> DerivedTMatrixInfoByChildItemInfoIdList() {
            return xcreateQDRFunctionTMatrixInfoByChildItemInfoIdList();
        }
        protected QDRFunction<TMatrixInfoCB> xcreateQDRFunctionTMatrixInfoByChildItemInfoIdList() {
            return new QDRFunction<TMatrixInfoCB>(delegate(String function, SubQuery<TMatrixInfoCB> subQuery, String operand, Object value) {
                xqderiveTMatrixInfoByChildItemInfoIdList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTMatrixInfoByChildItemInfoIdList(String function, SubQuery<TMatrixInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdList(TMatrixInfoCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TMatrixInfoByChildItemInfoIdListParameter(Object parameterValue);

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
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistList(TScenarioQuerylistCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TScenarioQuerylistListParameter(Object parameterValue);

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
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemList(TGtScenarioItemCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TGtScenarioItemListParameter(Object parameterValue);

        public QDRFunction<TFaScenarioItemCB> DerivedTFaScenarioItemList() {
            return xcreateQDRFunctionTFaScenarioItemList();
        }
        protected QDRFunction<TFaScenarioItemCB> xcreateQDRFunctionTFaScenarioItemList() {
            return new QDRFunction<TFaScenarioItemCB>(delegate(String function, SubQuery<TFaScenarioItemCB> subQuery, String operand, Object value) {
                xqderiveTFaScenarioItemList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTFaScenarioItemList(String function, SubQuery<TFaScenarioItemCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "FA_Target_Item_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TFaScenarioItemListParameter(Object parameterValue);

        public QDRFunction<TFaListAddItemCB> DerivedTFaListAddItemList() {
            return xcreateQDRFunctionTFaListAddItemList();
        }
        protected QDRFunction<TFaListAddItemCB> xcreateQDRFunctionTFaListAddItemList() {
            return new QDRFunction<TFaListAddItemCB>(delegate(String function, SubQuery<TFaListAddItemCB> subQuery, String operand, Object value) {
                xqderiveTFaListAddItemList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTFaListAddItemList(String function, SubQuery<TFaListAddItemCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TFaListAddItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TFaListAddItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TFaListAddItemListParameter(Object parameterValue);

        public QDRFunction<TGtMatrixChildCB> DerivedTGtMatrixChildList() {
            return xcreateQDRFunctionTGtMatrixChildList();
        }
        protected QDRFunction<TGtMatrixChildCB> xcreateQDRFunctionTGtMatrixChildList() {
            return new QDRFunction<TGtMatrixChildCB>(delegate(String function, SubQuery<TGtMatrixChildCB> subQuery, String operand, Object value) {
                xqderiveTGtMatrixChildList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTGtMatrixChildList(String function, SubQuery<TGtMatrixChildCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "ITEM_INFO_ID", "CHILD_ITEM_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public abstract String keepItemInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(Object parameterValue);
        public void SetItemInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetItemInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueItemInfoId();

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

        public void SetItemName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemName_Equal(fRES(v));
        }
        protected void DoSetItemName_Equal(String v) { regItemName(CK_EQ, v); }
        public void SetItemName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemName_NotEqual(fRES(v));
        }
        protected void DoSetItemName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_NES, v);
        }
        public void SetItemName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_GT, fRES(v));
        }
        public void SetItemName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_LT, fRES(v));
        }
        public void SetItemName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_GE, fRES(v));
        }
        public void SetItemName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_LE, fRES(v));
        }
        public void SetItemName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueItemName(), "ITEM_NAME");
        }
        public void SetItemName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueItemName(), "ITEM_NAME");
        }
        public void SetItemName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetItemName_LikeSearch(v, cLSOP());
        }
        public void SetItemName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueItemName(), "ITEM_NAME", option);
        }
        public void SetItemName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueItemName(), "ITEM_NAME", option);
        }
        protected void regItemName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemName(), "ITEM_NAME");
        }
        protected abstract ConditionValue getCValueItemName();

        public void SetSourceDiv_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of Original of sourceDiv as equal. { = }
        /// Original: QC3から取り込まれたオリジナルデータを示す
        /// </summary>
        public void SetSourceDiv_Equal_Original() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_Equal(CDef.SourceDiv.Original.Code);
        }
        /// <summary>
        /// Set the value of DataEdit of sourceDiv as equal. { = }
        /// 加工データ: データ加工で作成されたデータを示す
        /// </summary>
        public void SetSourceDiv_Equal_DataEdit() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_Equal(CDef.SourceDiv.DataEdit.Code);
        }
        /// <summary>
        /// Set the value of ScenarioDataEdit of sourceDiv as equal. { = }
        /// シナリオ加工データ: シナリオ内の操作で作成されたデータを示す
        /// </summary>
        public void SetSourceDiv_Equal_ScenarioDataEdit() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_Equal(CDef.SourceDiv.ScenarioDataEdit.Code);
        }
        protected void DoSetSourceDiv_Equal(String v) { regSourceDiv(CK_EQ, v); }
        public void SetSourceDiv_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of Original of sourceDiv as notEqual. { &lt;&gt; }
        /// Original: QC3から取り込まれたオリジナルデータを示す
        /// </summary>
        public void SetSourceDiv_NotEqual_Original() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_NotEqual(CDef.SourceDiv.Original.Code);
        }
        /// <summary>
        /// Set the value of DataEdit of sourceDiv as notEqual. { &lt;&gt; }
        /// 加工データ: データ加工で作成されたデータを示す
        /// </summary>
        public void SetSourceDiv_NotEqual_DataEdit() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_NotEqual(CDef.SourceDiv.DataEdit.Code);
        }
        /// <summary>
        /// Set the value of ScenarioDataEdit of sourceDiv as notEqual. { &lt;&gt; }
        /// シナリオ加工データ: シナリオ内の操作で作成されたデータを示す
        /// </summary>
        public void SetSourceDiv_NotEqual_ScenarioDataEdit() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSourceDiv_NotEqual(CDef.SourceDiv.ScenarioDataEdit.Code);
        }
        protected void DoSetSourceDiv_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSourceDiv(CK_NES, v);
        }
        public void SetSourceDiv_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSourceDiv(), "SOURCE_DIV");
        }
        public void SetSourceDiv_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSourceDiv(), "SOURCE_DIV");
        }
        protected void regSourceDiv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSourceDiv(), "SOURCE_DIV");
        }
        protected abstract ConditionValue getCValueSourceDiv();

        public void SetItemno_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemno_Equal(fRES(v));
        }
        protected void DoSetItemno_Equal(String v) { regItemno(CK_EQ, v); }
        public void SetItemno_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemno_NotEqual(fRES(v));
        }
        protected void DoSetItemno_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_NES, v);
        }
        public void SetItemno_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_GT, fRES(v));
        }
        public void SetItemno_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_LT, fRES(v));
        }
        public void SetItemno_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_GE, fRES(v));
        }
        public void SetItemno_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_LE, fRES(v));
        }
        public void SetItemno_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueItemno(), "ITEMNO");
        }
        public void SetItemno_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueItemno(), "ITEMNO");
        }
        public void SetItemno_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetItemno_LikeSearch(v, cLSOP());
        }
        public void SetItemno_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueItemno(), "ITEMNO", option);
        }
        public void SetItemno_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueItemno(), "ITEMNO", option);
        }
        public void SetItemno_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_ISN, DUMMY_OBJECT);
        }
        public void SetItemno_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemno(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regItemno(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemno(), "ITEMNO");
        }
        protected abstract ConditionValue getCValueItemno();

        public void SetItemType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of SAR of itemType as equal. { = }
        /// SAR: SARを示す
        /// </summary>
        public void SetItemType_Equal_SAR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.SAR.Code);
        }
        /// <summary>
        /// Set the value of SAS of itemType as equal. { = }
        /// SAS: SASを示す
        /// </summary>
        public void SetItemType_Equal_SAS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.SAS.Code);
        }
        /// <summary>
        /// Set the value of SAP of itemType as equal. { = }
        /// SAP: SAPを示す
        /// </summary>
        public void SetItemType_Equal_SAP() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.SAP.Code);
        }
        /// <summary>
        /// Set the value of MAC of itemType as equal. { = }
        /// MAC: MACを示す
        /// </summary>
        public void SetItemType_Equal_MAC() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.MAC.Code);
        }
        /// <summary>
        /// Set the value of MTS of itemType as equal. { = }
        /// MTS: MTSを示す
        /// </summary>
        public void SetItemType_Equal_MTS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.MTS.Code);
        }
        /// <summary>
        /// Set the value of MTM of itemType as equal. { = }
        /// MTM: MTSを示す
        /// </summary>
        public void SetItemType_Equal_MTM() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.MTM.Code);
        }
        /// <summary>
        /// Set the value of MTT of itemType as equal. { = }
        /// MTT: MTTを示す
        /// </summary>
        public void SetItemType_Equal_MTT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.MTT.Code);
        }
        /// <summary>
        /// Set the value of RNK of itemType as equal. { = }
        /// RNK: RNKを示す
        /// </summary>
        public void SetItemType_Equal_RNK() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.RNK.Code);
        }
        /// <summary>
        /// Set the value of RAT of itemType as equal. { = }
        /// RAT: RATを示す
        /// </summary>
        public void SetItemType_Equal_RAT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.RAT.Code);
        }
        /// <summary>
        /// Set the value of FAS of itemType as equal. { = }
        /// FAS: FASを示す
        /// </summary>
        public void SetItemType_Equal_FAS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.FAS.Code);
        }
        /// <summary>
        /// Set the value of FAL of itemType as equal. { = }
        /// FAL: FALを示す
        /// </summary>
        public void SetItemType_Equal_FAL() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_Equal(CDef.ItemType.FAL.Code);
        }
        protected void DoSetItemType_Equal(String v) { regItemType(CK_EQ, v); }
        public void SetItemType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of SAR of itemType as notEqual. { &lt;&gt; }
        /// SAR: SARを示す
        /// </summary>
        public void SetItemType_NotEqual_SAR() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.SAR.Code);
        }
        /// <summary>
        /// Set the value of SAS of itemType as notEqual. { &lt;&gt; }
        /// SAS: SASを示す
        /// </summary>
        public void SetItemType_NotEqual_SAS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.SAS.Code);
        }
        /// <summary>
        /// Set the value of SAP of itemType as notEqual. { &lt;&gt; }
        /// SAP: SAPを示す
        /// </summary>
        public void SetItemType_NotEqual_SAP() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.SAP.Code);
        }
        /// <summary>
        /// Set the value of MAC of itemType as notEqual. { &lt;&gt; }
        /// MAC: MACを示す
        /// </summary>
        public void SetItemType_NotEqual_MAC() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.MAC.Code);
        }
        /// <summary>
        /// Set the value of MTS of itemType as notEqual. { &lt;&gt; }
        /// MTS: MTSを示す
        /// </summary>
        public void SetItemType_NotEqual_MTS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.MTS.Code);
        }
        /// <summary>
        /// Set the value of MTM of itemType as notEqual. { &lt;&gt; }
        /// MTM: MTSを示す
        /// </summary>
        public void SetItemType_NotEqual_MTM() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.MTM.Code);
        }
        /// <summary>
        /// Set the value of MTT of itemType as notEqual. { &lt;&gt; }
        /// MTT: MTTを示す
        /// </summary>
        public void SetItemType_NotEqual_MTT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.MTT.Code);
        }
        /// <summary>
        /// Set the value of RNK of itemType as notEqual. { &lt;&gt; }
        /// RNK: RNKを示す
        /// </summary>
        public void SetItemType_NotEqual_RNK() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.RNK.Code);
        }
        /// <summary>
        /// Set the value of RAT of itemType as notEqual. { &lt;&gt; }
        /// RAT: RATを示す
        /// </summary>
        public void SetItemType_NotEqual_RAT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.RAT.Code);
        }
        /// <summary>
        /// Set the value of FAS of itemType as notEqual. { &lt;&gt; }
        /// FAS: FASを示す
        /// </summary>
        public void SetItemType_NotEqual_FAS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.FAS.Code);
        }
        /// <summary>
        /// Set the value of FAL of itemType as notEqual. { &lt;&gt; }
        /// FAL: FALを示す
        /// </summary>
        public void SetItemType_NotEqual_FAL() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetItemType_NotEqual(CDef.ItemType.FAL.Code);
        }
        protected void DoSetItemType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemType(CK_NES, v);
        }
        public void SetItemType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueItemType(), "ITEM_TYPE");
        }
        public void SetItemType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueItemType(), "ITEM_TYPE");
        }
        public void SetItemType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetItemType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regItemType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemType(), "ITEM_TYPE");
        }
        protected abstract ConditionValue getCValueItemType();

        public void SetAnswerType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of SA of answerType as equal. { = }
        /// SA: SAを示す
        /// </summary>
        public void SetAnswerType_Equal_SA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_Equal(CDef.AnswerType.SA.Code);
        }
        /// <summary>
        /// Set the value of MA of answerType as equal. { = }
        /// MA: MAを示す
        /// </summary>
        public void SetAnswerType_Equal_MA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_Equal(CDef.AnswerType.MA.Code);
        }
        /// <summary>
        /// Set the value of N of answerType as equal. { = }
        /// N: Nを示す
        /// </summary>
        public void SetAnswerType_Equal_N() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_Equal(CDef.AnswerType.N.Code);
        }
        /// <summary>
        /// Set the value of FA of answerType as equal. { = }
        /// FA: FAを示す
        /// </summary>
        public void SetAnswerType_Equal_FA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_Equal(CDef.AnswerType.FA.Code);
        }
        /// <summary>
        /// Set the value of D of answerType as equal. { = }
        /// D: Dを示す
        /// </summary>
        public void SetAnswerType_Equal_D() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_Equal(CDef.AnswerType.D.Code);
        }
        protected void DoSetAnswerType_Equal(String v) { regAnswerType(CK_EQ, v); }
        public void SetAnswerType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of SA of answerType as notEqual. { &lt;&gt; }
        /// SA: SAを示す
        /// </summary>
        public void SetAnswerType_NotEqual_SA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_NotEqual(CDef.AnswerType.SA.Code);
        }
        /// <summary>
        /// Set the value of MA of answerType as notEqual. { &lt;&gt; }
        /// MA: MAを示す
        /// </summary>
        public void SetAnswerType_NotEqual_MA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_NotEqual(CDef.AnswerType.MA.Code);
        }
        /// <summary>
        /// Set the value of N of answerType as notEqual. { &lt;&gt; }
        /// N: Nを示す
        /// </summary>
        public void SetAnswerType_NotEqual_N() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_NotEqual(CDef.AnswerType.N.Code);
        }
        /// <summary>
        /// Set the value of FA of answerType as notEqual. { &lt;&gt; }
        /// FA: FAを示す
        /// </summary>
        public void SetAnswerType_NotEqual_FA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_NotEqual(CDef.AnswerType.FA.Code);
        }
        /// <summary>
        /// Set the value of D of answerType as notEqual. { &lt;&gt; }
        /// D: Dを示す
        /// </summary>
        public void SetAnswerType_NotEqual_D() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAnswerType_NotEqual(CDef.AnswerType.D.Code);
        }
        protected void DoSetAnswerType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerType(CK_NES, v);
        }
        public void SetAnswerType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueAnswerType(), "ANSWER_TYPE");
        }
        public void SetAnswerType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueAnswerType(), "ANSWER_TYPE");
        }
        protected void regAnswerType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAnswerType(), "ANSWER_TYPE");
        }
        protected abstract ConditionValue getCValueAnswerType();

        public void SetSortNumber_Equal(int? v) { regSortNumber(CK_EQ, v); }
        public void SetSortNumber_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNumber(CK_NES, v);
        }
        public void SetSortNumber_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNumber(CK_GT, v);
        }
        public void SetSortNumber_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNumber(CK_LT, v);
        }
        public void SetSortNumber_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNumber(CK_GE, v);
        }
        public void SetSortNumber_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortNumber(CK_LE, v);
        }
        public void SetSortNumber_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortNumber(), "SORT_NUMBER");
        }
        public void SetSortNumber_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortNumber(), "SORT_NUMBER");
        }
        protected void regSortNumber(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortNumber(), "SORT_NUMBER");
        }
        protected abstract ConditionValue getCValueSortNumber();

        public void SetMatrixDiv_Equal(int? v) { regMatrixDiv(CK_EQ, v); }
        /// <summary>
        /// Set the value of NormalItem of matrixDiv as equal. { = }
        /// 通常アイテム: 通常アイテムを示す
        /// </summary>
        public void SetMatrixDiv_Equal_NormalItem() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.NormalItem.Code;
            regMatrixDiv(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of MatrixParent of matrixDiv as equal. { = }
        /// 親アイテム: 親アイテムを示す
        /// </summary>
        public void SetMatrixDiv_Equal_MatrixParent() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.MatrixParent.Code;
            regMatrixDiv(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of FirstChild of matrixDiv as equal. { = }
        /// 子マトリックス（親作成元アイテム）: 子マトリックス（親作成元アイテム）を示す
        /// </summary>
        public void SetMatrixDiv_Equal_FirstChild() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.FirstChild.Code;
            regMatrixDiv(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of MatrixChild of matrixDiv as equal. { = }
        /// 子マトリックス（通常子アイテム）: 子マトリックス（通常子アイテム）を示す
        /// </summary>
        public void SetMatrixDiv_Equal_MatrixChild() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.MatrixChild.Code;
            regMatrixDiv(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of SubFA of matrixDiv as equal. { = }
        /// 子マトリックス（付加FA）: 子マトリックス（付加FA）を示す
        /// </summary>
        public void SetMatrixDiv_Equal_SubFA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.SubFA.Code;
            regMatrixDiv(CK_EQ, int.Parse(code));
        }
        public void SetMatrixDiv_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMatrixDiv(CK_NES, v);
        }
        /// <summary>
        /// Set the value of NormalItem of matrixDiv as notEqual. { &lt;&gt; }
        /// 通常アイテム: 通常アイテムを示す
        /// </summary>
        public void SetMatrixDiv_NotEqual_NormalItem() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.NormalItem.Code;
            regMatrixDiv(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of MatrixParent of matrixDiv as notEqual. { &lt;&gt; }
        /// 親アイテム: 親アイテムを示す
        /// </summary>
        public void SetMatrixDiv_NotEqual_MatrixParent() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.MatrixParent.Code;
            regMatrixDiv(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of FirstChild of matrixDiv as notEqual. { &lt;&gt; }
        /// 子マトリックス（親作成元アイテム）: 子マトリックス（親作成元アイテム）を示す
        /// </summary>
        public void SetMatrixDiv_NotEqual_FirstChild() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.FirstChild.Code;
            regMatrixDiv(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of MatrixChild of matrixDiv as notEqual. { &lt;&gt; }
        /// 子マトリックス（通常子アイテム）: 子マトリックス（通常子アイテム）を示す
        /// </summary>
        public void SetMatrixDiv_NotEqual_MatrixChild() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.MatrixChild.Code;
            regMatrixDiv(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of SubFA of matrixDiv as notEqual. { &lt;&gt; }
        /// 子マトリックス（付加FA）: 子マトリックス（付加FA）を示す
        /// </summary>
        public void SetMatrixDiv_NotEqual_SubFA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.MatrixType.SubFA.Code;
            regMatrixDiv(CK_NES, int.Parse(code));
        }
        public void SetMatrixDiv_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMatrixDiv(), "MATRIX_DIV");
        }
        public void SetMatrixDiv_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMatrixDiv(), "MATRIX_DIV");
        }
        protected void regMatrixDiv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMatrixDiv(), "MATRIX_DIV");
        }
        protected abstract ConditionValue getCValueMatrixDiv();

        public void SetLv1title_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLv1title_Equal(fRES(v));
        }
        protected void DoSetLv1title_Equal(String v) { regLv1title(CK_EQ, v); }
        public void SetLv1title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLv1title_NotEqual(fRES(v));
        }
        protected void DoSetLv1title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_NES, v);
        }
        public void SetLv1title_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_GT, fRES(v));
        }
        public void SetLv1title_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_LT, fRES(v));
        }
        public void SetLv1title_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_GE, fRES(v));
        }
        public void SetLv1title_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_LE, fRES(v));
        }
        public void SetLv1title_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLv1title(), "LV1TITLE");
        }
        public void SetLv1title_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLv1title(), "LV1TITLE");
        }
        public void SetLv1title_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLv1title_LikeSearch(v, cLSOP());
        }
        public void SetLv1title_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLv1title(), "LV1TITLE", option);
        }
        public void SetLv1title_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLv1title(), "LV1TITLE", option);
        }
        public void SetLv1title_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLv1title_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLv1title(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLv1title(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLv1title(), "LV1TITLE");
        }
        protected abstract ConditionValue getCValueLv1title();

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

        public void SetOriginalLv1title_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalLv1title_Equal(fRES(v));
        }
        protected void DoSetOriginalLv1title_Equal(String v) { regOriginalLv1title(CK_EQ, v); }
        public void SetOriginalLv1title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalLv1title_NotEqual(fRES(v));
        }
        protected void DoSetOriginalLv1title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_NES, v);
        }
        public void SetOriginalLv1title_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_GT, fRES(v));
        }
        public void SetOriginalLv1title_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_LT, fRES(v));
        }
        public void SetOriginalLv1title_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_GE, fRES(v));
        }
        public void SetOriginalLv1title_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_LE, fRES(v));
        }
        public void SetOriginalLv1title_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOriginalLv1title(), "ORIGINAL_LV1TITLE");
        }
        public void SetOriginalLv1title_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOriginalLv1title(), "ORIGINAL_LV1TITLE");
        }
        public void SetOriginalLv1title_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOriginalLv1title_LikeSearch(v, cLSOP());
        }
        public void SetOriginalLv1title_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOriginalLv1title(), "ORIGINAL_LV1TITLE", option);
        }
        public void SetOriginalLv1title_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOriginalLv1title(), "ORIGINAL_LV1TITLE", option);
        }
        public void SetOriginalLv1title_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOriginalLv1title_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv1title(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOriginalLv1title(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOriginalLv1title(), "ORIGINAL_LV1TITLE");
        }
        protected abstract ConditionValue getCValueOriginalLv1title();

        public void SetOriginalLv2title_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalLv2title_Equal(fRES(v));
        }
        protected void DoSetOriginalLv2title_Equal(String v) { regOriginalLv2title(CK_EQ, v); }
        public void SetOriginalLv2title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalLv2title_NotEqual(fRES(v));
        }
        protected void DoSetOriginalLv2title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_NES, v);
        }
        public void SetOriginalLv2title_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_GT, fRES(v));
        }
        public void SetOriginalLv2title_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_LT, fRES(v));
        }
        public void SetOriginalLv2title_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_GE, fRES(v));
        }
        public void SetOriginalLv2title_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_LE, fRES(v));
        }
        public void SetOriginalLv2title_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOriginalLv2title(), "ORIGINAL_LV2TITLE");
        }
        public void SetOriginalLv2title_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOriginalLv2title(), "ORIGINAL_LV2TITLE");
        }
        public void SetOriginalLv2title_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOriginalLv2title_LikeSearch(v, cLSOP());
        }
        public void SetOriginalLv2title_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOriginalLv2title(), "ORIGINAL_LV2TITLE", option);
        }
        public void SetOriginalLv2title_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOriginalLv2title(), "ORIGINAL_LV2TITLE", option);
        }
        public void SetOriginalLv2title_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOriginalLv2title_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalLv2title(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOriginalLv2title(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOriginalLv2title(), "ORIGINAL_LV2TITLE");
        }
        protected abstract ConditionValue getCValueOriginalLv2title();

        public void SetTableName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTableName_Equal(fRES(v));
        }
        protected void DoSetTableName_Equal(String v) { regTableName(CK_EQ, v); }
        public void SetTableName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTableName_NotEqual(fRES(v));
        }
        protected void DoSetTableName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_NES, v);
        }
        public void SetTableName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_GT, fRES(v));
        }
        public void SetTableName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_LT, fRES(v));
        }
        public void SetTableName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_GE, fRES(v));
        }
        public void SetTableName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_LE, fRES(v));
        }
        public void SetTableName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTableName(), "TABLE_NAME");
        }
        public void SetTableName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTableName(), "TABLE_NAME");
        }
        public void SetTableName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTableName_LikeSearch(v, cLSOP());
        }
        public void SetTableName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTableName(), "TABLE_NAME", option);
        }
        public void SetTableName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTableName(), "TABLE_NAME", option);
        }
        public void SetTableName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTableName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTableName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTableName(), "TABLE_NAME");
        }
        protected abstract ConditionValue getCValueTableName();

        public void SetColumnName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetColumnName_Equal(fRES(v));
        }
        protected void DoSetColumnName_Equal(String v) { regColumnName(CK_EQ, v); }
        public void SetColumnName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetColumnName_NotEqual(fRES(v));
        }
        protected void DoSetColumnName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_NES, v);
        }
        public void SetColumnName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_GT, fRES(v));
        }
        public void SetColumnName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_LT, fRES(v));
        }
        public void SetColumnName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_GE, fRES(v));
        }
        public void SetColumnName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_LE, fRES(v));
        }
        public void SetColumnName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueColumnName(), "COLUMN_NAME");
        }
        public void SetColumnName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueColumnName(), "COLUMN_NAME");
        }
        public void SetColumnName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetColumnName_LikeSearch(v, cLSOP());
        }
        public void SetColumnName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueColumnName(), "COLUMN_NAME", option);
        }
        public void SetColumnName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueColumnName(), "COLUMN_NAME", option);
        }
        public void SetColumnName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColumnName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColumnName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColumnName(), "COLUMN_NAME");
        }
        protected abstract ConditionValue getCValueColumnName();

        public void SetCategoryEditId_Equal(decimal? v) { regCategoryEditId(CK_EQ, v); }
        public void SetCategoryEditId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryEditId(CK_NES, v);
        }
        public void SetCategoryEditId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryEditId(CK_GT, v);
        }
        public void SetCategoryEditId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryEditId(CK_LT, v);
        }
        public void SetCategoryEditId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryEditId(CK_GE, v);
        }
        public void SetCategoryEditId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCategoryEditId(CK_LE, v);
        }
        public void SetCategoryEditId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCategoryEditId(), "CATEGORY_EDIT_ID");
        }
        public void SetCategoryEditId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCategoryEditId(), "CATEGORY_EDIT_ID");
        }
        public void InScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryEditId_InScopeSubQuery_TScenarioTotalization(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CATEGORY_EDIT_ID", "Scenario_Totalization_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryEditId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        public void NotInScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryEditId_NotInScopeSubQuery_TScenarioTotalization(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CATEGORY_EDIT_ID", "Scenario_Totalization_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryEditId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        public void SetCategoryEditId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryEditId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCategoryEditId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryEditId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCategoryEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryEditId(), "CATEGORY_EDIT_ID");
        }
        protected abstract ConditionValue getCValueCategoryEditId();

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
        public void InScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataEditList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void NotInScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataEditList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void SetDataEditId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDataEditId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataEditId(), "DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDataEditId();

        public void SetStatus_Equal(int? v) { regStatus(CK_EQ, v); }
        /// <summary>
        /// Set the value of Invalid of status as equal. { = }
        /// 無効(論理削除): 無効を示す
        /// </summary>
        public void SetStatus_Equal_Invalid() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ItemStatus.Invalid.Code;
            regStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Effective of status as equal. { = }
        /// 有効: 有効を示す
        /// </summary>
        public void SetStatus_Equal_Effective() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ItemStatus.Effective.Code;
            regStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Temporary of status as equal. { = }
        /// 仮登録: 仮登録を示す
        /// </summary>
        public void SetStatus_Equal_Temporary() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ItemStatus.Temporary.Code;
            regStatus(CK_EQ, int.Parse(code));
        }
        public void SetStatus_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatus(CK_NES, v);
        }
        /// <summary>
        /// Set the value of Invalid of status as notEqual. { &lt;&gt; }
        /// 無効(論理削除): 無効を示す
        /// </summary>
        public void SetStatus_NotEqual_Invalid() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ItemStatus.Invalid.Code;
            regStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Effective of status as notEqual. { &lt;&gt; }
        /// 有効: 有効を示す
        /// </summary>
        public void SetStatus_NotEqual_Effective() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ItemStatus.Effective.Code;
            regStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of Temporary of status as notEqual. { &lt;&gt; }
        /// 仮登録: 仮登録を示す
        /// </summary>
        public void SetStatus_NotEqual_Temporary() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ItemStatus.Temporary.Code;
            regStatus(CK_NES, int.Parse(code));
        }
        public void SetStatus_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueStatus(), "STATUS");
        }
        public void SetStatus_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueStatus(), "STATUS");
        }
        protected void regStatus(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueStatus(), "STATUS");
        }
        protected abstract ConditionValue getCValueStatus();

        public void SetTableNameOrg_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTableNameOrg_Equal(fRES(v));
        }
        protected void DoSetTableNameOrg_Equal(String v) { regTableNameOrg(CK_EQ, v); }
        public void SetTableNameOrg_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTableNameOrg_NotEqual(fRES(v));
        }
        protected void DoSetTableNameOrg_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_NES, v);
        }
        public void SetTableNameOrg_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_GT, fRES(v));
        }
        public void SetTableNameOrg_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_LT, fRES(v));
        }
        public void SetTableNameOrg_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_GE, fRES(v));
        }
        public void SetTableNameOrg_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_LE, fRES(v));
        }
        public void SetTableNameOrg_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTableNameOrg(), "TABLE_NAME_ORG");
        }
        public void SetTableNameOrg_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTableNameOrg(), "TABLE_NAME_ORG");
        }
        public void SetTableNameOrg_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTableNameOrg_LikeSearch(v, cLSOP());
        }
        public void SetTableNameOrg_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTableNameOrg(), "TABLE_NAME_ORG", option);
        }
        public void SetTableNameOrg_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTableNameOrg(), "TABLE_NAME_ORG", option);
        }
        public void SetTableNameOrg_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTableNameOrg_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNameOrg(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTableNameOrg(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTableNameOrg(), "TABLE_NAME_ORG");
        }
        protected abstract ConditionValue getCValueTableNameOrg();

        public void SetColumnNameOrg_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetColumnNameOrg_Equal(fRES(v));
        }
        protected void DoSetColumnNameOrg_Equal(String v) { regColumnNameOrg(CK_EQ, v); }
        public void SetColumnNameOrg_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetColumnNameOrg_NotEqual(fRES(v));
        }
        protected void DoSetColumnNameOrg_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_NES, v);
        }
        public void SetColumnNameOrg_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_GT, fRES(v));
        }
        public void SetColumnNameOrg_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_LT, fRES(v));
        }
        public void SetColumnNameOrg_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_GE, fRES(v));
        }
        public void SetColumnNameOrg_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_LE, fRES(v));
        }
        public void SetColumnNameOrg_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueColumnNameOrg(), "COLUMN_NAME_ORG");
        }
        public void SetColumnNameOrg_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueColumnNameOrg(), "COLUMN_NAME_ORG");
        }
        public void SetColumnNameOrg_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetColumnNameOrg_LikeSearch(v, cLSOP());
        }
        public void SetColumnNameOrg_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueColumnNameOrg(), "COLUMN_NAME_ORG", option);
        }
        public void SetColumnNameOrg_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueColumnNameOrg(), "COLUMN_NAME_ORG", option);
        }
        public void SetColumnNameOrg_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColumnNameOrg_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNameOrg(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColumnNameOrg(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColumnNameOrg(), "COLUMN_NAME_ORG");
        }
        protected abstract ConditionValue getCValueColumnNameOrg();

        public void SetCompelItemChangeFlag_Equal(int? v) { regCompelItemChangeFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of compelItemChangeFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCompelItemChangeFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCompelItemChangeFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of compelItemChangeFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCompelItemChangeFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCompelItemChangeFlag(CK_EQ, int.Parse(code));
        }
        public void SetCompelItemChangeFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCompelItemChangeFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of compelItemChangeFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCompelItemChangeFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCompelItemChangeFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of compelItemChangeFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCompelItemChangeFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCompelItemChangeFlag(CK_NES, int.Parse(code));
        }
        public void SetCompelItemChangeFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCompelItemChangeFlag(), "COMPEL_ITEM_CHANGE_FLAG");
        }
        public void SetCompelItemChangeFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCompelItemChangeFlag(), "COMPEL_ITEM_CHANGE_FLAG");
        }
        protected void regCompelItemChangeFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCompelItemChangeFlag(), "COMPEL_ITEM_CHANGE_FLAG");
        }
        protected abstract ConditionValue getCValueCompelItemChangeFlag();

        public void SetSortFlag_Equal(int? v) { regSortFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of sortFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetSortFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSortFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of sortFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSortFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSortFlag(CK_EQ, int.Parse(code));
        }
        public void SetSortFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of sortFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetSortFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSortFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of sortFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSortFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSortFlag(CK_NES, int.Parse(code));
        }
        public void SetSortFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortFlag(), "SORT_FLAG");
        }
        public void SetSortFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortFlag(), "SORT_FLAG");
        }
        protected void regSortFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortFlag(), "SORT_FLAG");
        }
        protected abstract ConditionValue getCValueSortFlag();

        public void SetSortRange_Equal(int? v) { regSortRange(CK_EQ, v); }
        public void SetSortRange_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRange(CK_NES, v);
        }
        public void SetSortRange_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRange(CK_GT, v);
        }
        public void SetSortRange_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRange(CK_LT, v);
        }
        public void SetSortRange_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRange(CK_GE, v);
        }
        public void SetSortRange_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortRange(CK_LE, v);
        }
        public void SetSortRange_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortRange(), "SORT_RANGE");
        }
        public void SetSortRange_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortRange(), "SORT_RANGE");
        }
        public void SetSortRange_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRange(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSortRange_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRange(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSortRange(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortRange(), "SORT_RANGE");
        }
        protected abstract ConditionValue getCValueSortRange();

        public void SetMultivariateFlag_Equal(int? v) { regMultivariateFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of multivariateFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetMultivariateFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regMultivariateFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of multivariateFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetMultivariateFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regMultivariateFlag(CK_EQ, int.Parse(code));
        }
        public void SetMultivariateFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMultivariateFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of multivariateFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetMultivariateFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regMultivariateFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of multivariateFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetMultivariateFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regMultivariateFlag(CK_NES, int.Parse(code));
        }
        public void SetMultivariateFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMultivariateFlag(), "MULTIVARIATE_FLAG");
        }
        public void SetMultivariateFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMultivariateFlag(), "MULTIVARIATE_FLAG");
        }
        protected void regMultivariateFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMultivariateFlag(), "MULTIVARIATE_FLAG");
        }
        protected abstract ConditionValue getCValueMultivariateFlag();

        public void SetTableNo_Equal(int? v) { regTableNo(CK_EQ, v); }
        public void SetTableNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_NES, v);
        }
        public void SetTableNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_GT, v);
        }
        public void SetTableNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_LT, v);
        }
        public void SetTableNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_GE, v);
        }
        public void SetTableNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTableNo(CK_LE, v);
        }
        public void SetTableNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTableNo(), "TABLE_NO");
        }
        public void SetTableNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTableNo(), "TABLE_NO");
        }
        public void SetTableNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTableNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTableNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTableNo(), "TABLE_NO");
        }
        protected abstract ConditionValue getCValueTableNo();

        public void SetColumnNo_Equal(int? v) { regColumnNo(CK_EQ, v); }
        public void SetColumnNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNo(CK_NES, v);
        }
        public void SetColumnNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNo(CK_GT, v);
        }
        public void SetColumnNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNo(CK_LT, v);
        }
        public void SetColumnNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNo(CK_GE, v);
        }
        public void SetColumnNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regColumnNo(CK_LE, v);
        }
        public void SetColumnNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueColumnNo(), "COLUMN_NO");
        }
        public void SetColumnNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueColumnNo(), "COLUMN_NO");
        }
        public void SetColumnNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColumnNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColumnNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColumnNo(), "COLUMN_NO");
        }
        protected abstract ConditionValue getCValueColumnNo();

        public void SetTableNoOrg_Equal(int? v) { regTableNoOrg(CK_EQ, v); }
        public void SetTableNoOrg_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNoOrg(CK_NES, v);
        }
        public void SetTableNoOrg_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNoOrg(CK_GT, v);
        }
        public void SetTableNoOrg_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNoOrg(CK_LT, v);
        }
        public void SetTableNoOrg_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNoOrg(CK_GE, v);
        }
        public void SetTableNoOrg_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTableNoOrg(CK_LE, v);
        }
        public void SetTableNoOrg_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTableNoOrg(), "TABLE_NO_ORG");
        }
        public void SetTableNoOrg_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTableNoOrg(), "TABLE_NO_ORG");
        }
        public void SetTableNoOrg_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNoOrg(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTableNoOrg_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNoOrg(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTableNoOrg(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTableNoOrg(), "TABLE_NO_ORG");
        }
        protected abstract ConditionValue getCValueTableNoOrg();

        public void SetColumnNoOrg_Equal(int? v) { regColumnNoOrg(CK_EQ, v); }
        public void SetColumnNoOrg_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNoOrg(CK_NES, v);
        }
        public void SetColumnNoOrg_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNoOrg(CK_GT, v);
        }
        public void SetColumnNoOrg_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNoOrg(CK_LT, v);
        }
        public void SetColumnNoOrg_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNoOrg(CK_GE, v);
        }
        public void SetColumnNoOrg_LessEqual(int? v) {
            WhereSetterFlag = true;
            regColumnNoOrg(CK_LE, v);
        }
        public void SetColumnNoOrg_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueColumnNoOrg(), "COLUMN_NO_ORG");
        }
        public void SetColumnNoOrg_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueColumnNoOrg(), "COLUMN_NO_ORG");
        }
        public void SetColumnNoOrg_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNoOrg(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColumnNoOrg_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColumnNoOrg(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColumnNoOrg(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColumnNoOrg(), "COLUMN_NO_ORG");
        }
        protected abstract ConditionValue getCValueColumnNoOrg();

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

        public void SetNewAtQc3Flag_Equal(int? v) { regNewAtQc3Flag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of newAtQc3Flag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetNewAtQc3Flag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNewAtQc3Flag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of newAtQc3Flag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNewAtQc3Flag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNewAtQc3Flag(CK_EQ, int.Parse(code));
        }
        public void SetNewAtQc3Flag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewAtQc3Flag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of newAtQc3Flag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetNewAtQc3Flag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNewAtQc3Flag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of newAtQc3Flag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNewAtQc3Flag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNewAtQc3Flag(CK_NES, int.Parse(code));
        }
        public void SetNewAtQc3Flag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNewAtQc3Flag(), "NEW_AT_QC3_FLAG");
        }
        public void SetNewAtQc3Flag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNewAtQc3Flag(), "NEW_AT_QC3_FLAG");
        }
        protected void regNewAtQc3Flag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewAtQc3Flag(), "NEW_AT_QC3_FLAG");
        }
        protected abstract ConditionValue getCValueNewAtQc3Flag();

        public void SetSortRangeOrg_Equal(int? v) { regSortRangeOrg(CK_EQ, v); }
        public void SetSortRangeOrg_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRangeOrg(CK_NES, v);
        }
        public void SetSortRangeOrg_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRangeOrg(CK_GT, v);
        }
        public void SetSortRangeOrg_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRangeOrg(CK_LT, v);
        }
        public void SetSortRangeOrg_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRangeOrg(CK_GE, v);
        }
        public void SetSortRangeOrg_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortRangeOrg(CK_LE, v);
        }
        public void SetSortRangeOrg_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortRangeOrg(), "SORT_RANGE_ORG");
        }
        public void SetSortRangeOrg_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortRangeOrg(), "SORT_RANGE_ORG");
        }
        public void SetSortRangeOrg_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRangeOrg(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSortRangeOrg_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortRangeOrg(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSortRangeOrg(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortRangeOrg(), "SORT_RANGE_ORG");
        }
        protected abstract ConditionValue getCValueSortRangeOrg();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TItemInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TItemInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TItemInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TItemInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TItemInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TItemInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TItemInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TItemInfoCB>(delegate(String function, SubQuery<TItemInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TItemInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TItemInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TItemInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
