
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
    public abstract class AbstractBsTGtMatrixInfoCQ : AbstractConditionQuery {

        public AbstractBsTGtMatrixInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_GT_MATRIX_INFO"; }
        public override String getTableSqlName() { return "T_GT_MATRIX_INFO"; }

        public void SetGtMatrixInfoId_Equal(decimal? v) { regGtMatrixInfoId(CK_EQ, v); }
        public void SetGtMatrixInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_NES, v);
        }
        public void SetGtMatrixInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_GT, v);
        }
        public void SetGtMatrixInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_LT, v);
        }
        public void SetGtMatrixInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_GE, v);
        }
        public void SetGtMatrixInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regGtMatrixInfoId(CK_LE, v);
        }
        public void SetGtMatrixInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueGtMatrixInfoId(), "GT_MATRIX_INFO_ID");
        }
        public void SetGtMatrixInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueGtMatrixInfoId(), "GT_MATRIX_INFO_ID");
        }
        public void ExistsTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList(cb.Query());
            registerExistsSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_ExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void NotExistsTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_NotExistsSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void InScopeTGtMatrixChild(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChild(cb.Query());
            registerInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_Matrix_Info_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChild(TGtMatrixChildCQ subQuery);
        public void InScopeTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_InScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void NotInScopeTGtMatrixChild(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_Matrix_Info_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChild(TGtMatrixChildCQ subQuery);
        public void NotInScopeTGtMatrixChildList(SubQuery<TGtMatrixChildCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepGtMatrixInfoId_NotInScopeSubQuery_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public void xsderiveTGtMatrixChildList(String function, SubQuery<TGtMatrixChildCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixChildCB>", subQuery);
            TGtMatrixChildCB cb = new TGtMatrixChildCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepGtMatrixInfoId_SpecifyDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery);

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
            String subQueryPropertyName = keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildList(TGtMatrixChildCQ subQuery);
        public abstract String keepGtMatrixInfoId_QueryDerivedReferrer_TGtMatrixChildListParameter(Object parameterValue);
        public void SetGtMatrixInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGtMatrixInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtMatrixInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGtMatrixInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGtMatrixInfoId(), "GT_MATRIX_INFO_ID");
        }
        protected abstract ConditionValue getCValueGtMatrixInfoId();

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

        public void SetBaseItemId_Equal(decimal? v) { regBaseItemId(CK_EQ, v); }
        public void SetBaseItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseItemId(CK_NES, v);
        }
        public void SetBaseItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseItemId(CK_GT, v);
        }
        public void SetBaseItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseItemId(CK_LT, v);
        }
        public void SetBaseItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseItemId(CK_GE, v);
        }
        public void SetBaseItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regBaseItemId(CK_LE, v);
        }
        public void SetBaseItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueBaseItemId(), "BASE_ITEM_ID");
        }
        public void SetBaseItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueBaseItemId(), "BASE_ITEM_ID");
        }
        protected void regBaseItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBaseItemId(), "BASE_ITEM_ID");
        }
        protected abstract ConditionValue getCValueBaseItemId();

        public void SetNewItemId_Equal(decimal? v) { regNewItemId(CK_EQ, v); }
        public void SetNewItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_NES, v);
        }
        public void SetNewItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_GT, v);
        }
        public void SetNewItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_LT, v);
        }
        public void SetNewItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_GE, v);
        }
        public void SetNewItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regNewItemId(CK_LE, v);
        }
        public void SetNewItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueNewItemId(), "NEW_ITEM_ID");
        }
        public void SetNewItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueNewItemId(), "NEW_ITEM_ID");
        }
        protected void regNewItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewItemId(), "NEW_ITEM_ID");
        }
        protected abstract ConditionValue getCValueNewItemId();

        public void SetTotalizationType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTotalizationType_Equal(fRES(v));
        }
        protected void DoSetTotalizationType_Equal(String v) { regTotalizationType(CK_EQ, v); }
        public void SetTotalizationType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTotalizationType_NotEqual(fRES(v));
        }
        protected void DoSetTotalizationType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalizationType(CK_NES, v);
        }
        public void SetTotalizationType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalizationType(CK_GT, fRES(v));
        }
        public void SetTotalizationType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalizationType(CK_LT, fRES(v));
        }
        public void SetTotalizationType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalizationType(CK_GE, fRES(v));
        }
        public void SetTotalizationType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTotalizationType(CK_LE, fRES(v));
        }
        public void SetTotalizationType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTotalizationType(), "TOTALIZATION_TYPE");
        }
        public void SetTotalizationType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTotalizationType(), "TOTALIZATION_TYPE");
        }
        public void SetTotalizationType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTotalizationType_LikeSearch(v, cLSOP());
        }
        public void SetTotalizationType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTotalizationType(), "TOTALIZATION_TYPE", option);
        }
        public void SetTotalizationType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTotalizationType(), "TOTALIZATION_TYPE", option);
        }
        protected void regTotalizationType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTotalizationType(), "TOTALIZATION_TYPE");
        }
        protected abstract ConditionValue getCValueTotalizationType();

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
        public void SetItemName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetItemName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regItemName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemName(), "ITEM_NAME");
        }
        protected abstract ConditionValue getCValueItemName();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TGtMatrixInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TGtMatrixInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TGtMatrixInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TGtMatrixInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TGtMatrixInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TGtMatrixInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TGtMatrixInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TGtMatrixInfoCB>(delegate(String function, SubQuery<TGtMatrixInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TGtMatrixInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TGtMatrixInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TGtMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtMatrixInfoCB>", subQuery);
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "GT_MATRIX_INFO_ID", "GT_MATRIX_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TGtMatrixInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
