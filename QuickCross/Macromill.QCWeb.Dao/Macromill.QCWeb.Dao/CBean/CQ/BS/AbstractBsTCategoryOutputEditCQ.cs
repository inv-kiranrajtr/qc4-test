
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
    public abstract class AbstractBsTCategoryOutputEditCQ : AbstractConditionQuery {

        public AbstractBsTCategoryOutputEditCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_CATEGORY_OUTPUT_EDIT"; }
        public override String getTableSqlName() { return "T_CATEGORY_OUTPUT_EDIT"; }

        public void SetCategoryOutputEditId_Equal(decimal? v) { regCategoryOutputEditId(CK_EQ, v); }
        public void SetCategoryOutputEditId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_NES, v);
        }
        public void SetCategoryOutputEditId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_GT, v);
        }
        public void SetCategoryOutputEditId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_LT, v);
        }
        public void SetCategoryOutputEditId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_GE, v);
        }
        public void SetCategoryOutputEditId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCategoryOutputEditId(CK_LE, v);
        }
        public void SetCategoryOutputEditId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCategoryOutputEditId(), "CATEGORY_OUTPUT_EDIT_ID");
        }
        public void SetCategoryOutputEditId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCategoryOutputEditId(), "CATEGORY_OUTPUT_EDIT_ID");
        }
        public void ExistsTCategoryOutputDetailList(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList(cb.Query());
            registerExistsSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_ExistsSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery);
        public void NotExistsTCategoryOutputDetailList(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_NotExistsSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery);
        public void InScopeTCategoryOutputDetail(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "Category_Output_Edit_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetail(TCategoryOutputDetailCQ subQuery);
        public void InScopeTCategoryOutputDetailList(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_InScopeSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery);
        public void NotInScopeTCategoryOutputDetail(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "Category_Output_Edit_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetail(TCategoryOutputDetailCQ subQuery);
        public void NotInScopeTCategoryOutputDetailList(SubQuery<TCategoryOutputDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryOutputEditId_NotInScopeSubQuery_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery);
        public void xsderiveTCategoryOutputDetailList(String function, SubQuery<TCategoryOutputDetailCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepCategoryOutputEditId_SpecifyDerivedReferrer_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery);

        public QDRFunction<TCategoryOutputDetailCB> DerivedTCategoryOutputDetailList() {
            return xcreateQDRFunctionTCategoryOutputDetailList();
        }
        protected QDRFunction<TCategoryOutputDetailCB> xcreateQDRFunctionTCategoryOutputDetailList() {
            return new QDRFunction<TCategoryOutputDetailCB>(delegate(String function, SubQuery<TCategoryOutputDetailCB> subQuery, String operand, Object value) {
                xqderiveTCategoryOutputDetailList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTCategoryOutputDetailList(String function, SubQuery<TCategoryOutputDetailCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TCategoryOutputDetailCB>", subQuery);
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailList(TCategoryOutputDetailCQ subQuery);
        public abstract String keepCategoryOutputEditId_QueryDerivedReferrer_TCategoryOutputDetailListParameter(Object parameterValue);
        public void SetCategoryOutputEditId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCategoryOutputEditId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryOutputEditId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCategoryOutputEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryOutputEditId(), "CATEGORY_OUTPUT_EDIT_ID");
        }
        protected abstract ConditionValue getCValueCategoryOutputEditId();

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

        public void SetOldItemId_Equal(decimal? v) { regOldItemId(CK_EQ, v); }
        public void SetOldItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldItemId(CK_NES, v);
        }
        public void SetOldItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldItemId(CK_GT, v);
        }
        public void SetOldItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldItemId(CK_LT, v);
        }
        public void SetOldItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOldItemId(CK_GE, v);
        }
        public void SetOldItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOldItemId(CK_LE, v);
        }
        public void SetOldItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOldItemId(), "OLD_ITEM_ID");
        }
        public void SetOldItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOldItemId(), "OLD_ITEM_ID");
        }
        protected void regOldItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOldItemId(), "OLD_ITEM_ID");
        }
        protected abstract ConditionValue getCValueOldItemId();

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

        public void SetTopFlag_Equal(int? v) { regTopFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of topFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTopFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTopFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of topFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTopFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTopFlag(CK_EQ, int.Parse(code));
        }
        public void SetTopFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of topFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTopFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTopFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of topFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTopFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTopFlag(CK_NES, int.Parse(code));
        }
        public void SetTopFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTopFlag(), "TOP_FLAG");
        }
        public void SetTopFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTopFlag(), "TOP_FLAG");
        }
        protected void regTopFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTopFlag(), "TOP_FLAG");
        }
        protected abstract ConditionValue getCValueTopFlag();

        public void SetTopCount_Equal(int? v) { regTopCount(CK_EQ, v); }
        public void SetTopCount_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopCount(CK_NES, v);
        }
        public void SetTopCount_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopCount(CK_GT, v);
        }
        public void SetTopCount_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopCount(CK_LT, v);
        }
        public void SetTopCount_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopCount(CK_GE, v);
        }
        public void SetTopCount_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTopCount(CK_LE, v);
        }
        public void SetTopCount_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTopCount(), "TOP_COUNT");
        }
        public void SetTopCount_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTopCount(), "TOP_COUNT");
        }
        public void SetTopCount_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopCount(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTopCount_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopCount(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTopCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTopCount(), "TOP_COUNT");
        }
        protected abstract ConditionValue getCValueTopCount();

        public void SetTopName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTopName_Equal(fRES(v));
        }
        protected void DoSetTopName_Equal(String v) { regTopName(CK_EQ, v); }
        public void SetTopName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTopName_NotEqual(fRES(v));
        }
        protected void DoSetTopName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_NES, v);
        }
        public void SetTopName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_GT, fRES(v));
        }
        public void SetTopName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_LT, fRES(v));
        }
        public void SetTopName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_GE, fRES(v));
        }
        public void SetTopName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_LE, fRES(v));
        }
        public void SetTopName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTopName(), "TOP_NAME");
        }
        public void SetTopName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTopName(), "TOP_NAME");
        }
        public void SetTopName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTopName_LikeSearch(v, cLSOP());
        }
        public void SetTopName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTopName(), "TOP_NAME", option);
        }
        public void SetTopName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTopName(), "TOP_NAME", option);
        }
        public void SetTopName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTopName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTopName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTopName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTopName(), "TOP_NAME");
        }
        protected abstract ConditionValue getCValueTopName();

        public void SetBottomFlag_Equal(int? v) { regBottomFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of bottomFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetBottomFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regBottomFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of bottomFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetBottomFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regBottomFlag(CK_EQ, int.Parse(code));
        }
        public void SetBottomFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of bottomFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetBottomFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regBottomFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of bottomFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetBottomFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regBottomFlag(CK_NES, int.Parse(code));
        }
        public void SetBottomFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueBottomFlag(), "BOTTOM_FLAG");
        }
        public void SetBottomFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueBottomFlag(), "BOTTOM_FLAG");
        }
        protected void regBottomFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBottomFlag(), "BOTTOM_FLAG");
        }
        protected abstract ConditionValue getCValueBottomFlag();

        public void SetBottomCount_Equal(int? v) { regBottomCount(CK_EQ, v); }
        public void SetBottomCount_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomCount(CK_NES, v);
        }
        public void SetBottomCount_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomCount(CK_GT, v);
        }
        public void SetBottomCount_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomCount(CK_LT, v);
        }
        public void SetBottomCount_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomCount(CK_GE, v);
        }
        public void SetBottomCount_LessEqual(int? v) {
            WhereSetterFlag = true;
            regBottomCount(CK_LE, v);
        }
        public void SetBottomCount_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueBottomCount(), "BOTTOM_COUNT");
        }
        public void SetBottomCount_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueBottomCount(), "BOTTOM_COUNT");
        }
        public void SetBottomCount_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomCount(CK_ISN, DUMMY_OBJECT);
        }
        public void SetBottomCount_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomCount(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regBottomCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBottomCount(), "BOTTOM_COUNT");
        }
        protected abstract ConditionValue getCValueBottomCount();

        public void SetBottomName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetBottomName_Equal(fRES(v));
        }
        protected void DoSetBottomName_Equal(String v) { regBottomName(CK_EQ, v); }
        public void SetBottomName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetBottomName_NotEqual(fRES(v));
        }
        protected void DoSetBottomName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_NES, v);
        }
        public void SetBottomName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_GT, fRES(v));
        }
        public void SetBottomName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_LT, fRES(v));
        }
        public void SetBottomName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_GE, fRES(v));
        }
        public void SetBottomName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_LE, fRES(v));
        }
        public void SetBottomName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueBottomName(), "BOTTOM_NAME");
        }
        public void SetBottomName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueBottomName(), "BOTTOM_NAME");
        }
        public void SetBottomName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetBottomName_LikeSearch(v, cLSOP());
        }
        public void SetBottomName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueBottomName(), "BOTTOM_NAME", option);
        }
        public void SetBottomName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueBottomName(), "BOTTOM_NAME", option);
        }
        public void SetBottomName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetBottomName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regBottomName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBottomName(), "BOTTOM_NAME");
        }
        protected abstract ConditionValue getCValueBottomName();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TCategoryOutputEditCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TCategoryOutputEditCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TCategoryOutputEditCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TCategoryOutputEditCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TCategoryOutputEditCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TCategoryOutputEditCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TCategoryOutputEditCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TCategoryOutputEditCB>(delegate(String function, SubQuery<TCategoryOutputEditCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TCategoryOutputEditCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TCategoryOutputEditCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TCategoryOutputEditCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryOutputEditCB>", subQuery);
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "CATEGORY_OUTPUT_EDIT_ID", "CATEGORY_OUTPUT_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TCategoryOutputEditCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
