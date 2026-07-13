
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
    public abstract class AbstractBsTCrossScenarioTargetCQ : AbstractConditionQuery {

        public AbstractBsTCrossScenarioTargetCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_CROSS_SCENARIO_TARGET"; }
        public override String getTableSqlName() { return "T_CROSS_SCENARIO_TARGET"; }

        public void SetCrossScenarioTargetId_Equal(decimal? v) { regCrossScenarioTargetId(CK_EQ, v); }
        public void SetCrossScenarioTargetId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_NES, v);
        }
        public void SetCrossScenarioTargetId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_GT, v);
        }
        public void SetCrossScenarioTargetId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_LT, v);
        }
        public void SetCrossScenarioTargetId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_GE, v);
        }
        public void SetCrossScenarioTargetId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCrossScenarioTargetId(CK_LE, v);
        }
        public void SetCrossScenarioTargetId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCrossScenarioTargetId(), "CROSS_SCENARIO_TARGET_ID");
        }
        public void SetCrossScenarioTargetId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCrossScenarioTargetId(), "CROSS_SCENARIO_TARGET_ID");
        }
        public void ExistsTColorSetInfoCrossList(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList(cb.Query());
            registerExistsSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_ExistsSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery);
        public void ExistsTCrossScenarioItemList(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_ExistsSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery);
        public void NotExistsTColorSetInfoCrossList(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_NotExistsSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery);
        public void NotExistsTCrossScenarioItemList(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_NotExistsSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery);
        public void InScopeTColorSetInfoCrossList(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_InScopeSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery);
        public void InScopeTCrossScenarioItemList(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery);
        public void NotInScopeTColorSetInfoCrossList(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_NotInScopeSubQuery_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery);
        public void NotInScopeTCrossScenarioItemList(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery);
        public void xsderiveTColorSetInfoCrossList(String function, SubQuery<TColorSetInfoCrossCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepCrossScenarioTargetId_SpecifyDerivedReferrer_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery);
        public void xsderiveTCrossScenarioItemList(String function, SubQuery<TCrossScenarioItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepCrossScenarioTargetId_SpecifyDerivedReferrer_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery);

        public QDRFunction<TColorSetInfoCrossCB> DerivedTColorSetInfoCrossList() {
            return xcreateQDRFunctionTColorSetInfoCrossList();
        }
        protected QDRFunction<TColorSetInfoCrossCB> xcreateQDRFunctionTColorSetInfoCrossList() {
            return new QDRFunction<TColorSetInfoCrossCB>(delegate(String function, SubQuery<TColorSetInfoCrossCB> subQuery, String operand, Object value) {
                xqderiveTColorSetInfoCrossList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTColorSetInfoCrossList(String function, SubQuery<TColorSetInfoCrossCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossList(TColorSetInfoCrossCQ subQuery);
        public abstract String keepCrossScenarioTargetId_QueryDerivedReferrer_TColorSetInfoCrossListParameter(Object parameterValue);

        public QDRFunction<TCrossScenarioItemCB> DerivedTCrossScenarioItemList() {
            return xcreateQDRFunctionTCrossScenarioItemList();
        }
        protected QDRFunction<TCrossScenarioItemCB> xcreateQDRFunctionTCrossScenarioItemList() {
            return new QDRFunction<TCrossScenarioItemCB>(delegate(String function, SubQuery<TCrossScenarioItemCB> subQuery, String operand, Object value) {
                xqderiveTCrossScenarioItemList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTCrossScenarioItemList(String function, SubQuery<TCrossScenarioItemCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemList(TCrossScenarioItemCQ subQuery);
        public abstract String keepCrossScenarioTargetId_QueryDerivedReferrer_TCrossScenarioItemListParameter(Object parameterValue);
        public void SetCrossScenarioTargetId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCrossScenarioTargetId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCrossScenarioTargetId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossScenarioTargetId(), "CROSS_SCENARIO_TARGET_ID");
        }
        protected abstract ConditionValue getCValueCrossScenarioTargetId();

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
        public void InScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        public void NotInScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        protected void regScenarioTotalizationId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        protected abstract ConditionValue getCValueScenarioTotalizationId();

        public void SetScenariosetNo_Equal(int? v) { regScenariosetNo(CK_EQ, v); }
        public void SetScenariosetNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenariosetNo(CK_NES, v);
        }
        public void SetScenariosetNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenariosetNo(CK_GT, v);
        }
        public void SetScenariosetNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenariosetNo(CK_LT, v);
        }
        public void SetScenariosetNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenariosetNo(CK_GE, v);
        }
        public void SetScenariosetNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regScenariosetNo(CK_LE, v);
        }
        public void SetScenariosetNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueScenariosetNo(), "SCENARIOSET_NO");
        }
        public void SetScenariosetNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueScenariosetNo(), "SCENARIOSET_NO");
        }
        protected void regScenariosetNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenariosetNo(), "SCENARIOSET_NO");
        }
        protected abstract ConditionValue getCValueScenariosetNo();

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

        public void SetScItemId_Equal(decimal? v) { regScItemId(CK_EQ, v); }
        public void SetScItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScItemId(CK_NES, v);
        }
        public void SetScItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScItemId(CK_GT, v);
        }
        public void SetScItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScItemId(CK_LT, v);
        }
        public void SetScItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScItemId(CK_GE, v);
        }
        public void SetScItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regScItemId(CK_LE, v);
        }
        public void SetScItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueScItemId(), "SC_ITEM_ID");
        }
        public void SetScItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueScItemId(), "SC_ITEM_ID");
        }
        protected void regScItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScItemId(), "SC_ITEM_ID");
        }
        protected abstract ConditionValue getCValueScItemId();

        public void SetViewName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewName_Equal(fRES(v));
        }
        protected void DoSetViewName_Equal(String v) { regViewName(CK_EQ, v); }
        public void SetViewName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewName_NotEqual(fRES(v));
        }
        protected void DoSetViewName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewName(CK_NES, v);
        }
        public void SetViewName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewName(CK_GT, fRES(v));
        }
        public void SetViewName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewName(CK_LT, fRES(v));
        }
        public void SetViewName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewName(CK_GE, fRES(v));
        }
        public void SetViewName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewName(CK_LE, fRES(v));
        }
        public void SetViewName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueViewName(), "VIEW_NAME");
        }
        public void SetViewName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueViewName(), "VIEW_NAME");
        }
        public void SetViewName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetViewName_LikeSearch(v, cLSOP());
        }
        public void SetViewName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueViewName(), "VIEW_NAME", option);
        }
        public void SetViewName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueViewName(), "VIEW_NAME", option);
        }
        protected void regViewName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueViewName(), "VIEW_NAME");
        }
        protected abstract ConditionValue getCValueViewName();

        public void SetGraphType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphType_Equal(fRES(v));
        }
        protected void DoSetGraphType_Equal(String v) { regGraphType(CK_EQ, v); }
        public void SetGraphType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphType_NotEqual(fRES(v));
        }
        protected void DoSetGraphType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_NES, v);
        }
        public void SetGraphType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_GT, fRES(v));
        }
        public void SetGraphType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_LT, fRES(v));
        }
        public void SetGraphType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_GE, fRES(v));
        }
        public void SetGraphType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_LE, fRES(v));
        }
        public void SetGraphType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphType(), "GRAPH_TYPE");
        }
        public void SetGraphType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphType(), "GRAPH_TYPE");
        }
        public void SetGraphType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphType_LikeSearch(v, cLSOP());
        }
        public void SetGraphType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphType(), "GRAPH_TYPE", option);
        }
        public void SetGraphType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphType(), "GRAPH_TYPE", option);
        }
        public void SetGraphType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGraphType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGraphType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphType(), "GRAPH_TYPE");
        }
        protected abstract ConditionValue getCValueGraphType();

        public void SetReportType_Equal(int? v) { regReportType(CK_EQ, v); }
        public void SetReportType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_NES, v);
        }
        public void SetReportType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_GT, v);
        }
        public void SetReportType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_LT, v);
        }
        public void SetReportType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_GE, v);
        }
        public void SetReportType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regReportType(CK_LE, v);
        }
        public void SetReportType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueReportType(), "REPORT_TYPE");
        }
        public void SetReportType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueReportType(), "REPORT_TYPE");
        }
        protected void regReportType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportType(), "REPORT_TYPE");
        }
        protected abstract ConditionValue getCValueReportType();

        public void SetViewItemString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewItemString_Equal(fRES(v));
        }
        protected void DoSetViewItemString_Equal(String v) { regViewItemString(CK_EQ, v); }
        public void SetViewItemString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewItemString_NotEqual(fRES(v));
        }
        protected void DoSetViewItemString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_NES, v);
        }
        public void SetViewItemString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_GT, fRES(v));
        }
        public void SetViewItemString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_LT, fRES(v));
        }
        public void SetViewItemString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_GE, fRES(v));
        }
        public void SetViewItemString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_LE, fRES(v));
        }
        public void SetViewItemString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueViewItemString(), "VIEW_ITEM_STRING");
        }
        public void SetViewItemString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueViewItemString(), "VIEW_ITEM_STRING");
        }
        public void SetViewItemString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetViewItemString_LikeSearch(v, cLSOP());
        }
        public void SetViewItemString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueViewItemString(), "VIEW_ITEM_STRING", option);
        }
        public void SetViewItemString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueViewItemString(), "VIEW_ITEM_STRING", option);
        }
        public void SetViewItemString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetViewItemString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regViewItemString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueViewItemString(), "VIEW_ITEM_STRING");
        }
        protected abstract ConditionValue getCValueViewItemString();

        public void SetScenarioComment_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioComment_Equal(fRES(v));
        }
        protected void DoSetScenarioComment_Equal(String v) { regScenarioComment(CK_EQ, v); }
        public void SetScenarioComment_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioComment_NotEqual(fRES(v));
        }
        protected void DoSetScenarioComment_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_NES, v);
        }
        public void SetScenarioComment_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_GT, fRES(v));
        }
        public void SetScenarioComment_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_LT, fRES(v));
        }
        public void SetScenarioComment_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_GE, fRES(v));
        }
        public void SetScenarioComment_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_LE, fRES(v));
        }
        public void SetScenarioComment_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueScenarioComment(), "SCENARIO_COMMENT");
        }
        public void SetScenarioComment_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueScenarioComment(), "SCENARIO_COMMENT");
        }
        public void SetScenarioComment_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetScenarioComment_LikeSearch(v, cLSOP());
        }
        public void SetScenarioComment_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueScenarioComment(), "SCENARIO_COMMENT", option);
        }
        public void SetScenarioComment_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueScenarioComment(), "SCENARIO_COMMENT", option);
        }
        public void SetScenarioComment_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_ISN, DUMMY_OBJECT);
        }
        public void SetScenarioComment_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regScenarioComment(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioComment(), "SCENARIO_COMMENT");
        }
        protected abstract ConditionValue getCValueScenarioComment();

        public void SetPolylineFlag_Equal(int? v) { regPolylineFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of polylineFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPolylineFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPolylineFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of polylineFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPolylineFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPolylineFlag(CK_EQ, int.Parse(code));
        }
        public void SetPolylineFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of polylineFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPolylineFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPolylineFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of polylineFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPolylineFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPolylineFlag(CK_NES, int.Parse(code));
        }
        public void SetPolylineFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePolylineFlag(), "POLYLINE_FLAG");
        }
        public void SetPolylineFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePolylineFlag(), "POLYLINE_FLAG");
        }
        protected void regPolylineFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePolylineFlag(), "POLYLINE_FLAG");
        }
        protected abstract ConditionValue getCValuePolylineFlag();

        public void SetGraphTypeReport_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeReport_Equal(fRES(v));
        }
        protected void DoSetGraphTypeReport_Equal(String v) { regGraphTypeReport(CK_EQ, v); }
        public void SetGraphTypeReport_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeReport_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeReport_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_NES, v);
        }
        public void SetGraphTypeReport_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_GT, fRES(v));
        }
        public void SetGraphTypeReport_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_LT, fRES(v));
        }
        public void SetGraphTypeReport_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_GE, fRES(v));
        }
        public void SetGraphTypeReport_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_LE, fRES(v));
        }
        public void SetGraphTypeReport_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT");
        }
        public void SetGraphTypeReport_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT");
        }
        public void SetGraphTypeReport_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeReport_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeReport_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT", option);
        }
        public void SetGraphTypeReport_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT", option);
        }
        public void SetGraphTypeReport_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGraphTypeReport_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGraphTypeReport(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT");
        }
        protected abstract ConditionValue getCValueGraphTypeReport();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TCrossScenarioTargetCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TCrossScenarioTargetCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TCrossScenarioTargetCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TCrossScenarioTargetCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TCrossScenarioTargetCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TCrossScenarioTargetCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TCrossScenarioTargetCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TCrossScenarioTargetCB>(delegate(String function, SubQuery<TCrossScenarioTargetCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TCrossScenarioTargetCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TCrossScenarioTargetCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TCrossScenarioTargetCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
