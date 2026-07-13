
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
    public abstract class AbstractBsTFaScenarioHeaderCQ : AbstractConditionQuery {

        public AbstractBsTFaScenarioHeaderCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_FA_SCENARIO_HEADER"; }
        public override String getTableSqlName() { return "T_FA_SCENARIO_HEADER"; }

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
        public void ExistsTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_ExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void ExistsTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_ExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void NotExistsTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_NotExistsSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void NotExistsTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_NotExistsSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void InScopeTFaScenarioItem(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem(cb.Query());
            registerInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_Scenario_Header_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItem(TFaScenarioItemCQ subQuery);
        public void InScopeTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_InScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void InScopeTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_InScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void NotInScopeTFaScenarioItem(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_Scenario_Header_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItem(TFaScenarioItemCQ subQuery);
        public void NotInScopeTFaListAddItemList(SubQuery<TFaListAddItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void NotInScopeTFaScenarioItemList(SubQuery<TFaScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_NotInScopeSubQuery_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public void xsderiveTFaListAddItemList(String function, SubQuery<TFaListAddItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaListAddItemCB>", subQuery);
            TFaListAddItemCB cb = new TFaListAddItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public void xsderiveTFaScenarioItemList(String function, SubQuery<TFaScenarioItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioItemCB>", subQuery);
            TFaScenarioItemCB cb = new TFaScenarioItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepFaScenarioHeaderId_SpecifyDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery);

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
            String subQueryPropertyName = keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemList(TFaListAddItemCQ subQuery);
        public abstract String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaListAddItemListParameter(Object parameterValue);

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
            String subQueryPropertyName = keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemList(TFaScenarioItemCQ subQuery);
        public abstract String keepFaScenarioHeaderId_QueryDerivedReferrer_TFaScenarioItemListParameter(Object parameterValue);
        public void SetFaScenarioHeaderId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioHeaderId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFaScenarioHeaderId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFaScenarioHeaderId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFaScenarioHeaderId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFaScenarioHeaderId(), "FA_SCENARIO_HEADER_ID");
        }
        protected abstract ConditionValue getCValueFaScenarioHeaderId();

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

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TFaScenarioHeaderCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TFaScenarioHeaderCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TFaScenarioHeaderCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TFaScenarioHeaderCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TFaScenarioHeaderCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TFaScenarioHeaderCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TFaScenarioHeaderCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TFaScenarioHeaderCB>(delegate(String function, SubQuery<TFaScenarioHeaderCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TFaScenarioHeaderCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TFaScenarioHeaderCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TFaScenarioHeaderCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaScenarioHeaderCB>", subQuery);
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "FA_SCENARIO_HEADER_ID", "FA_SCENARIO_HEADER_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TFaScenarioHeaderCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
