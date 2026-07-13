
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
    public abstract class AbstractBsTDefaultEnvCQ : AbstractConditionQuery {

        public AbstractBsTDefaultEnvCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DEFAULT_ENV"; }
        public override String getTableSqlName() { return "T_DEFAULT_ENV"; }

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
        public void ExistsTDefaultEnvColorInfoList(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TDefaultEnvColorInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery);
        public void ExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void ExistsTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void NotExistsTDefaultEnvColorInfoList(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery);
        public void NotExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void InScopeTDefaultEnvColorInfoList(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TDefaultEnvColorInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void NotInScopeTDefaultEnvColorInfoList(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTScenarioTotalizationList(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public void xsderiveTDefaultEnvColorInfoList(String function, SubQuery<TDefaultEnvColorInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery);
        public void xsderiveTScenarioTotalizationList(String function, SubQuery<TScenarioTotalizationCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);

        public QDRFunction<TDefaultEnvColorInfoCB> DerivedTDefaultEnvColorInfoList() {
            return xcreateQDRFunctionTDefaultEnvColorInfoList();
        }
        protected QDRFunction<TDefaultEnvColorInfoCB> xcreateQDRFunctionTDefaultEnvColorInfoList() {
            return new QDRFunction<TDefaultEnvColorInfoCB>(delegate(String function, SubQuery<TDefaultEnvColorInfoCB> subQuery, String operand, Object value) {
                xqderiveTDefaultEnvColorInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDefaultEnvColorInfoList(String function, SubQuery<TDefaultEnvColorInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoList(TDefaultEnvColorInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TDefaultEnvColorInfoListParameter(Object parameterValue);

        public QDRFunction<TScenarioTotalizationCB> DerivedTScenarioTotalizationList() {
            return xcreateQDRFunctionTScenarioTotalizationList();
        }
        protected QDRFunction<TScenarioTotalizationCB> xcreateQDRFunctionTScenarioTotalizationList() {
            return new QDRFunction<TScenarioTotalizationCB>(delegate(String function, SubQuery<TScenarioTotalizationCB> subQuery, String operand, Object value) {
                xqderiveTScenarioTotalizationList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTScenarioTotalizationList(String function, SubQuery<TScenarioTotalizationCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationList(TScenarioTotalizationCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TScenarioTotalizationListParameter(Object parameterValue);
        public void SetQcwebid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQcwebid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetNoanswerDenominatorFlag_Equal(int? v) { regNoanswerDenominatorFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of noanswerDenominatorFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerDenominatorFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerDenominatorFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerDenominatorFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerDenominatorFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerDenominatorFlag(CK_EQ, int.Parse(code));
        }
        public void SetNoanswerDenominatorFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerDenominatorFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of noanswerDenominatorFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerDenominatorFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerDenominatorFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerDenominatorFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerDenominatorFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerDenominatorFlag(CK_NES, int.Parse(code));
        }
        public void SetNoanswerDenominatorFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNoanswerDenominatorFlag(), "NOANSWER_DENOMINATOR_FLAG");
        }
        public void SetNoanswerDenominatorFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNoanswerDenominatorFlag(), "NOANSWER_DENOMINATOR_FLAG");
        }
        protected void regNoanswerDenominatorFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoanswerDenominatorFlag(), "NOANSWER_DENOMINATOR_FLAG");
        }
        protected abstract ConditionValue getCValueNoanswerDenominatorFlag();

        public void SetVisibleUnfitFlag_Equal(int? v) { regVisibleUnfitFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of visibleUnfitFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetVisibleUnfitFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regVisibleUnfitFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of visibleUnfitFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetVisibleUnfitFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regVisibleUnfitFlag(CK_EQ, int.Parse(code));
        }
        public void SetVisibleUnfitFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regVisibleUnfitFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of visibleUnfitFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetVisibleUnfitFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regVisibleUnfitFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of visibleUnfitFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetVisibleUnfitFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regVisibleUnfitFlag(CK_NES, int.Parse(code));
        }
        public void SetVisibleUnfitFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueVisibleUnfitFlag(), "VISIBLE_UNFIT_FLAG");
        }
        public void SetVisibleUnfitFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueVisibleUnfitFlag(), "VISIBLE_UNFIT_FLAG");
        }
        protected void regVisibleUnfitFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueVisibleUnfitFlag(), "VISIBLE_UNFIT_FLAG");
        }
        protected abstract ConditionValue getCValueVisibleUnfitFlag();

        public void SetNoanswerUnfitFlag_Equal(int? v) { regNoanswerUnfitFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of noanswerUnfitFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerUnfitFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerUnfitFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerUnfitFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerUnfitFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerUnfitFlag(CK_EQ, int.Parse(code));
        }
        public void SetNoanswerUnfitFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerUnfitFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of noanswerUnfitFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerUnfitFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerUnfitFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerUnfitFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerUnfitFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerUnfitFlag(CK_NES, int.Parse(code));
        }
        public void SetNoanswerUnfitFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNoanswerUnfitFlag(), "NOANSWER_UNFIT_FLAG");
        }
        public void SetNoanswerUnfitFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNoanswerUnfitFlag(), "NOANSWER_UNFIT_FLAG");
        }
        protected void regNoanswerUnfitFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoanswerUnfitFlag(), "NOANSWER_UNFIT_FLAG");
        }
        protected abstract ConditionValue getCValueNoanswerUnfitFlag();

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

        public void SetCellJoincellJoinFlag_Equal(int? v) { regCellJoincellJoinFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of cellJoincellJoinFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCellJoincellJoinFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCellJoincellJoinFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of cellJoincellJoinFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCellJoincellJoinFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCellJoincellJoinFlag(CK_EQ, int.Parse(code));
        }
        public void SetCellJoincellJoinFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellJoincellJoinFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of cellJoincellJoinFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCellJoincellJoinFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCellJoincellJoinFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of cellJoincellJoinFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCellJoincellJoinFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCellJoincellJoinFlag(CK_NES, int.Parse(code));
        }
        public void SetCellJoincellJoinFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCellJoincellJoinFlag(), "CELL_JOINCELL_JOIN_FLAG");
        }
        public void SetCellJoincellJoinFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCellJoincellJoinFlag(), "CELL_JOINCELL_JOIN_FLAG");
        }
        protected void regCellJoincellJoinFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCellJoincellJoinFlag(), "CELL_JOINCELL_JOIN_FLAG");
        }
        protected abstract ConditionValue getCValueCellJoincellJoinFlag();

        public void SetChartDirectionGtFlag_Equal(int? v) { regChartDirectionGtFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of chartDirectionGtFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetChartDirectionGtFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regChartDirectionGtFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of chartDirectionGtFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetChartDirectionGtFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regChartDirectionGtFlag(CK_EQ, int.Parse(code));
        }
        public void SetChartDirectionGtFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChartDirectionGtFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of chartDirectionGtFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetChartDirectionGtFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regChartDirectionGtFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of chartDirectionGtFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetChartDirectionGtFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regChartDirectionGtFlag(CK_NES, int.Parse(code));
        }
        public void SetChartDirectionGtFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueChartDirectionGtFlag(), "CHART_DIRECTION_GT_FLAG");
        }
        public void SetChartDirectionGtFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueChartDirectionGtFlag(), "CHART_DIRECTION_GT_FLAG");
        }
        protected void regChartDirectionGtFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChartDirectionGtFlag(), "CHART_DIRECTION_GT_FLAG");
        }
        protected abstract ConditionValue getCValueChartDirectionGtFlag();

        public void SetChartDirectionCrossFlag_Equal(int? v) { regChartDirectionCrossFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of chartDirectionCrossFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetChartDirectionCrossFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regChartDirectionCrossFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of chartDirectionCrossFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetChartDirectionCrossFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regChartDirectionCrossFlag(CK_EQ, int.Parse(code));
        }
        public void SetChartDirectionCrossFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChartDirectionCrossFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of chartDirectionCrossFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetChartDirectionCrossFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regChartDirectionCrossFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of chartDirectionCrossFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetChartDirectionCrossFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regChartDirectionCrossFlag(CK_NES, int.Parse(code));
        }
        public void SetChartDirectionCrossFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueChartDirectionCrossFlag(), "CHART_DIRECTION_CROSS_FLAG");
        }
        public void SetChartDirectionCrossFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueChartDirectionCrossFlag(), "CHART_DIRECTION_CROSS_FLAG");
        }
        protected void regChartDirectionCrossFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChartDirectionCrossFlag(), "CHART_DIRECTION_CROSS_FLAG");
        }
        protected abstract ConditionValue getCValueChartDirectionCrossFlag();

        public void SetNoanswerTargetFlag_Equal(int? v) { regNoanswerTargetFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of noanswerTargetFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerTargetFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerTargetFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerTargetFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerTargetFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerTargetFlag(CK_EQ, int.Parse(code));
        }
        public void SetNoanswerTargetFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerTargetFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of noanswerTargetFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerTargetFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerTargetFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerTargetFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerTargetFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerTargetFlag(CK_NES, int.Parse(code));
        }
        public void SetNoanswerTargetFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNoanswerTargetFlag(), "NOANSWER_TARGET_FLAG");
        }
        public void SetNoanswerTargetFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNoanswerTargetFlag(), "NOANSWER_TARGET_FLAG");
        }
        protected void regNoanswerTargetFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoanswerTargetFlag(), "NOANSWER_TARGET_FLAG");
        }
        protected abstract ConditionValue getCValueNoanswerTargetFlag();

        public void SetNoanswerAxisFlag_Equal(int? v) { regNoanswerAxisFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of noanswerAxisFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerAxisFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerAxisFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerAxisFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerAxisFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerAxisFlag(CK_EQ, int.Parse(code));
        }
        public void SetNoanswerAxisFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerAxisFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of noanswerAxisFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerAxisFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerAxisFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerAxisFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerAxisFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerAxisFlag(CK_NES, int.Parse(code));
        }
        public void SetNoanswerAxisFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNoanswerAxisFlag(), "NOANSWER_AXIS_FLAG");
        }
        public void SetNoanswerAxisFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNoanswerAxisFlag(), "NOANSWER_AXIS_FLAG");
        }
        protected void regNoanswerAxisFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoanswerAxisFlag(), "NOANSWER_AXIS_FLAG");
        }
        protected abstract ConditionValue getCValueNoanswerAxisFlag();

        public void SetUnfitTargetFlag_Equal(int? v) { regUnfitTargetFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of unfitTargetFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetUnfitTargetFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUnfitTargetFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of unfitTargetFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUnfitTargetFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUnfitTargetFlag(CK_EQ, int.Parse(code));
        }
        public void SetUnfitTargetFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnfitTargetFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of unfitTargetFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetUnfitTargetFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUnfitTargetFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of unfitTargetFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUnfitTargetFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUnfitTargetFlag(CK_NES, int.Parse(code));
        }
        public void SetUnfitTargetFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUnfitTargetFlag(), "UNFIT_TARGET_FLAG");
        }
        public void SetUnfitTargetFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUnfitTargetFlag(), "UNFIT_TARGET_FLAG");
        }
        protected void regUnfitTargetFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUnfitTargetFlag(), "UNFIT_TARGET_FLAG");
        }
        protected abstract ConditionValue getCValueUnfitTargetFlag();

        public void SetUnfitAxisFlag_Equal(int? v) { regUnfitAxisFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of unfitAxisFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetUnfitAxisFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUnfitAxisFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of unfitAxisFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUnfitAxisFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUnfitAxisFlag(CK_EQ, int.Parse(code));
        }
        public void SetUnfitAxisFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnfitAxisFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of unfitAxisFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetUnfitAxisFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUnfitAxisFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of unfitAxisFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUnfitAxisFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUnfitAxisFlag(CK_NES, int.Parse(code));
        }
        public void SetUnfitAxisFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUnfitAxisFlag(), "UNFIT_AXIS_FLAG");
        }
        public void SetUnfitAxisFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUnfitAxisFlag(), "UNFIT_AXIS_FLAG");
        }
        protected void regUnfitAxisFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUnfitAxisFlag(), "UNFIT_AXIS_FLAG");
        }
        protected abstract ConditionValue getCValueUnfitAxisFlag();

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

        public void SetRateDiffColorMinus5_Equal(int? v) { regRateDiffColorMinus5(CK_EQ, v); }
        public void SetRateDiffColorMinus5_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus5(CK_NES, v);
        }
        public void SetRateDiffColorMinus5_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus5(CK_GT, v);
        }
        public void SetRateDiffColorMinus5_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus5(CK_LT, v);
        }
        public void SetRateDiffColorMinus5_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus5(CK_GE, v);
        }
        public void SetRateDiffColorMinus5_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRateDiffColorMinus5(CK_LE, v);
        }
        public void SetRateDiffColorMinus5_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRateDiffColorMinus5(), "RATE_DIFF_COLOR_MINUS5");
        }
        public void SetRateDiffColorMinus5_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRateDiffColorMinus5(), "RATE_DIFF_COLOR_MINUS5");
        }
        protected void regRateDiffColorMinus5(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRateDiffColorMinus5(), "RATE_DIFF_COLOR_MINUS5");
        }
        protected abstract ConditionValue getCValueRateDiffColorMinus5();

        public void SetRateDiffColorMinus10_Equal(int? v) { regRateDiffColorMinus10(CK_EQ, v); }
        public void SetRateDiffColorMinus10_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus10(CK_NES, v);
        }
        public void SetRateDiffColorMinus10_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus10(CK_GT, v);
        }
        public void SetRateDiffColorMinus10_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus10(CK_LT, v);
        }
        public void SetRateDiffColorMinus10_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorMinus10(CK_GE, v);
        }
        public void SetRateDiffColorMinus10_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRateDiffColorMinus10(CK_LE, v);
        }
        public void SetRateDiffColorMinus10_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRateDiffColorMinus10(), "RATE_DIFF_COLOR_MINUS10");
        }
        public void SetRateDiffColorMinus10_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRateDiffColorMinus10(), "RATE_DIFF_COLOR_MINUS10");
        }
        protected void regRateDiffColorMinus10(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRateDiffColorMinus10(), "RATE_DIFF_COLOR_MINUS10");
        }
        protected abstract ConditionValue getCValueRateDiffColorMinus10();

        public void SetRateDiffColorPlus5_Equal(int? v) { regRateDiffColorPlus5(CK_EQ, v); }
        public void SetRateDiffColorPlus5_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus5(CK_NES, v);
        }
        public void SetRateDiffColorPlus5_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus5(CK_GT, v);
        }
        public void SetRateDiffColorPlus5_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus5(CK_LT, v);
        }
        public void SetRateDiffColorPlus5_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus5(CK_GE, v);
        }
        public void SetRateDiffColorPlus5_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRateDiffColorPlus5(CK_LE, v);
        }
        public void SetRateDiffColorPlus5_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRateDiffColorPlus5(), "RATE_DIFF_COLOR_PLUS5");
        }
        public void SetRateDiffColorPlus5_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRateDiffColorPlus5(), "RATE_DIFF_COLOR_PLUS5");
        }
        protected void regRateDiffColorPlus5(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRateDiffColorPlus5(), "RATE_DIFF_COLOR_PLUS5");
        }
        protected abstract ConditionValue getCValueRateDiffColorPlus5();

        public void SetRateDiffColorPlus10_Equal(int? v) { regRateDiffColorPlus10(CK_EQ, v); }
        public void SetRateDiffColorPlus10_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus10(CK_NES, v);
        }
        public void SetRateDiffColorPlus10_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus10(CK_GT, v);
        }
        public void SetRateDiffColorPlus10_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus10(CK_LT, v);
        }
        public void SetRateDiffColorPlus10_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRateDiffColorPlus10(CK_GE, v);
        }
        public void SetRateDiffColorPlus10_LessEqual(int? v) {
            WhereSetterFlag = true;
            regRateDiffColorPlus10(CK_LE, v);
        }
        public void SetRateDiffColorPlus10_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueRateDiffColorPlus10(), "RATE_DIFF_COLOR_PLUS10");
        }
        public void SetRateDiffColorPlus10_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueRateDiffColorPlus10(), "RATE_DIFF_COLOR_PLUS10");
        }
        protected void regRateDiffColorPlus10(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRateDiffColorPlus10(), "RATE_DIFF_COLOR_PLUS10");
        }
        protected abstract ConditionValue getCValueRateDiffColorPlus10();

        public void SetGraphTypeSa_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeSa_Equal(fRES(v));
        }
        protected void DoSetGraphTypeSa_Equal(String v) { regGraphTypeSa(CK_EQ, v); }
        public void SetGraphTypeSa_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeSa_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeSa_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSa(CK_NES, v);
        }
        public void SetGraphTypeSa_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSa(CK_GT, fRES(v));
        }
        public void SetGraphTypeSa_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSa(CK_LT, fRES(v));
        }
        public void SetGraphTypeSa_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSa(CK_GE, fRES(v));
        }
        public void SetGraphTypeSa_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSa(CK_LE, fRES(v));
        }
        public void SetGraphTypeSa_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeSa(), "GRAPH_TYPE_SA");
        }
        public void SetGraphTypeSa_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeSa(), "GRAPH_TYPE_SA");
        }
        public void SetGraphTypeSa_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeSa_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeSa_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeSa(), "GRAPH_TYPE_SA", option);
        }
        public void SetGraphTypeSa_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeSa(), "GRAPH_TYPE_SA", option);
        }
        protected void regGraphTypeSa(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeSa(), "GRAPH_TYPE_SA");
        }
        protected abstract ConditionValue getCValueGraphTypeSa();

        public void SetGraphTypeSaMatrix_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeSaMatrix_Equal(fRES(v));
        }
        protected void DoSetGraphTypeSaMatrix_Equal(String v) { regGraphTypeSaMatrix(CK_EQ, v); }
        public void SetGraphTypeSaMatrix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeSaMatrix_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeSaMatrix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSaMatrix(CK_NES, v);
        }
        public void SetGraphTypeSaMatrix_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSaMatrix(CK_GT, fRES(v));
        }
        public void SetGraphTypeSaMatrix_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSaMatrix(CK_LT, fRES(v));
        }
        public void SetGraphTypeSaMatrix_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSaMatrix(CK_GE, fRES(v));
        }
        public void SetGraphTypeSaMatrix_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeSaMatrix(CK_LE, fRES(v));
        }
        public void SetGraphTypeSaMatrix_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeSaMatrix(), "GRAPH_TYPE_SA_MATRIX");
        }
        public void SetGraphTypeSaMatrix_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeSaMatrix(), "GRAPH_TYPE_SA_MATRIX");
        }
        public void SetGraphTypeSaMatrix_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeSaMatrix_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeSaMatrix_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeSaMatrix(), "GRAPH_TYPE_SA_MATRIX", option);
        }
        public void SetGraphTypeSaMatrix_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeSaMatrix(), "GRAPH_TYPE_SA_MATRIX", option);
        }
        protected void regGraphTypeSaMatrix(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeSaMatrix(), "GRAPH_TYPE_SA_MATRIX");
        }
        protected abstract ConditionValue getCValueGraphTypeSaMatrix();

        public void SetGraphTypeMaSimple_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeMaSimple_Equal(fRES(v));
        }
        protected void DoSetGraphTypeMaSimple_Equal(String v) { regGraphTypeMaSimple(CK_EQ, v); }
        public void SetGraphTypeMaSimple_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeMaSimple_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeMaSimple_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaSimple(CK_NES, v);
        }
        public void SetGraphTypeMaSimple_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaSimple(CK_GT, fRES(v));
        }
        public void SetGraphTypeMaSimple_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaSimple(CK_LT, fRES(v));
        }
        public void SetGraphTypeMaSimple_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaSimple(CK_GE, fRES(v));
        }
        public void SetGraphTypeMaSimple_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaSimple(CK_LE, fRES(v));
        }
        public void SetGraphTypeMaSimple_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeMaSimple(), "GRAPH_TYPE_MA_SIMPLE");
        }
        public void SetGraphTypeMaSimple_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeMaSimple(), "GRAPH_TYPE_MA_SIMPLE");
        }
        public void SetGraphTypeMaSimple_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeMaSimple_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeMaSimple_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeMaSimple(), "GRAPH_TYPE_MA_SIMPLE", option);
        }
        public void SetGraphTypeMaSimple_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeMaSimple(), "GRAPH_TYPE_MA_SIMPLE", option);
        }
        protected void regGraphTypeMaSimple(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeMaSimple(), "GRAPH_TYPE_MA_SIMPLE");
        }
        protected abstract ConditionValue getCValueGraphTypeMaSimple();

        public void SetGraphTypeMaCross_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeMaCross_Equal(fRES(v));
        }
        protected void DoSetGraphTypeMaCross_Equal(String v) { regGraphTypeMaCross(CK_EQ, v); }
        public void SetGraphTypeMaCross_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeMaCross_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeMaCross_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaCross(CK_NES, v);
        }
        public void SetGraphTypeMaCross_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaCross(CK_GT, fRES(v));
        }
        public void SetGraphTypeMaCross_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaCross(CK_LT, fRES(v));
        }
        public void SetGraphTypeMaCross_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaCross(CK_GE, fRES(v));
        }
        public void SetGraphTypeMaCross_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaCross(CK_LE, fRES(v));
        }
        public void SetGraphTypeMaCross_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeMaCross(), "GRAPH_TYPE_MA_CROSS");
        }
        public void SetGraphTypeMaCross_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeMaCross(), "GRAPH_TYPE_MA_CROSS");
        }
        public void SetGraphTypeMaCross_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeMaCross_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeMaCross_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeMaCross(), "GRAPH_TYPE_MA_CROSS", option);
        }
        public void SetGraphTypeMaCross_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeMaCross(), "GRAPH_TYPE_MA_CROSS", option);
        }
        protected void regGraphTypeMaCross(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeMaCross(), "GRAPH_TYPE_MA_CROSS");
        }
        protected abstract ConditionValue getCValueGraphTypeMaCross();

        public void SetGraphTypeMaMatrix_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeMaMatrix_Equal(fRES(v));
        }
        protected void DoSetGraphTypeMaMatrix_Equal(String v) { regGraphTypeMaMatrix(CK_EQ, v); }
        public void SetGraphTypeMaMatrix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeMaMatrix_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeMaMatrix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaMatrix(CK_NES, v);
        }
        public void SetGraphTypeMaMatrix_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaMatrix(CK_GT, fRES(v));
        }
        public void SetGraphTypeMaMatrix_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaMatrix(CK_LT, fRES(v));
        }
        public void SetGraphTypeMaMatrix_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaMatrix(CK_GE, fRES(v));
        }
        public void SetGraphTypeMaMatrix_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeMaMatrix(CK_LE, fRES(v));
        }
        public void SetGraphTypeMaMatrix_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeMaMatrix(), "GRAPH_TYPE_MA_MATRIX");
        }
        public void SetGraphTypeMaMatrix_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeMaMatrix(), "GRAPH_TYPE_MA_MATRIX");
        }
        public void SetGraphTypeMaMatrix_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeMaMatrix_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeMaMatrix_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeMaMatrix(), "GRAPH_TYPE_MA_MATRIX", option);
        }
        public void SetGraphTypeMaMatrix_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeMaMatrix(), "GRAPH_TYPE_MA_MATRIX", option);
        }
        protected void regGraphTypeMaMatrix(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeMaMatrix(), "GRAPH_TYPE_MA_MATRIX");
        }
        protected abstract ConditionValue getCValueGraphTypeMaMatrix();

        public void SetGraphTypeNRate_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeNRate_Equal(fRES(v));
        }
        protected void DoSetGraphTypeNRate_Equal(String v) { regGraphTypeNRate(CK_EQ, v); }
        public void SetGraphTypeNRate_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeNRate_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeNRate_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRate(CK_NES, v);
        }
        public void SetGraphTypeNRate_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRate(CK_GT, fRES(v));
        }
        public void SetGraphTypeNRate_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRate(CK_LT, fRES(v));
        }
        public void SetGraphTypeNRate_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRate(CK_GE, fRES(v));
        }
        public void SetGraphTypeNRate_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRate(CK_LE, fRES(v));
        }
        public void SetGraphTypeNRate_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeNRate(), "GRAPH_TYPE_N_RATE");
        }
        public void SetGraphTypeNRate_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeNRate(), "GRAPH_TYPE_N_RATE");
        }
        public void SetGraphTypeNRate_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeNRate_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeNRate_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeNRate(), "GRAPH_TYPE_N_RATE", option);
        }
        public void SetGraphTypeNRate_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeNRate(), "GRAPH_TYPE_N_RATE", option);
        }
        protected void regGraphTypeNRate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeNRate(), "GRAPH_TYPE_N_RATE");
        }
        protected abstract ConditionValue getCValueGraphTypeNRate();

        public void SetGraphTypeNRanking_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeNRanking_Equal(fRES(v));
        }
        protected void DoSetGraphTypeNRanking_Equal(String v) { regGraphTypeNRanking(CK_EQ, v); }
        public void SetGraphTypeNRanking_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeNRanking_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeNRanking_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRanking(CK_NES, v);
        }
        public void SetGraphTypeNRanking_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRanking(CK_GT, fRES(v));
        }
        public void SetGraphTypeNRanking_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRanking(CK_LT, fRES(v));
        }
        public void SetGraphTypeNRanking_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRanking(CK_GE, fRES(v));
        }
        public void SetGraphTypeNRanking_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeNRanking(CK_LE, fRES(v));
        }
        public void SetGraphTypeNRanking_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeNRanking(), "GRAPH_TYPE_N_RANKING");
        }
        public void SetGraphTypeNRanking_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeNRanking(), "GRAPH_TYPE_N_RANKING");
        }
        public void SetGraphTypeNRanking_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeNRanking_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeNRanking_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeNRanking(), "GRAPH_TYPE_N_RANKING", option);
        }
        public void SetGraphTypeNRanking_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeNRanking(), "GRAPH_TYPE_N_RANKING", option);
        }
        protected void regGraphTypeNRanking(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeNRanking(), "GRAPH_TYPE_N_RANKING");
        }
        protected abstract ConditionValue getCValueGraphTypeNRanking();

        public void SetSetExecuteFlag_Equal(int? v) { regSetExecuteFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of setExecuteFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetSetExecuteFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSetExecuteFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of setExecuteFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSetExecuteFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSetExecuteFlag(CK_EQ, int.Parse(code));
        }
        public void SetSetExecuteFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSetExecuteFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of setExecuteFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetSetExecuteFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSetExecuteFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of setExecuteFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSetExecuteFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSetExecuteFlag(CK_NES, int.Parse(code));
        }
        public void SetSetExecuteFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSetExecuteFlag(), "SET_EXECUTE_FLAG");
        }
        public void SetSetExecuteFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSetExecuteFlag(), "SET_EXECUTE_FLAG");
        }
        protected void regSetExecuteFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSetExecuteFlag(), "SET_EXECUTE_FLAG");
        }
        protected abstract ConditionValue getCValueSetExecuteFlag();

        public void SetTitleAll_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleAll_Equal(fRES(v));
        }
        protected void DoSetTitleAll_Equal(String v) { regTitleAll(CK_EQ, v); }
        public void SetTitleAll_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleAll_NotEqual(fRES(v));
        }
        protected void DoSetTitleAll_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_NES, v);
        }
        public void SetTitleAll_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_GT, fRES(v));
        }
        public void SetTitleAll_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_LT, fRES(v));
        }
        public void SetTitleAll_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_GE, fRES(v));
        }
        public void SetTitleAll_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_LE, fRES(v));
        }
        public void SetTitleAll_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleAll(), "TITLE_ALL");
        }
        public void SetTitleAll_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleAll(), "TITLE_ALL");
        }
        public void SetTitleAll_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleAll_LikeSearch(v, cLSOP());
        }
        public void SetTitleAll_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleAll(), "TITLE_ALL", option);
        }
        public void SetTitleAll_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleAll(), "TITLE_ALL", option);
        }
        public void SetTitleAll_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleAll_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAll(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleAll(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleAll(), "TITLE_ALL");
        }
        protected abstract ConditionValue getCValueTitleAll();

        public void SetTitleAxisAll_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleAxisAll_Equal(fRES(v));
        }
        protected void DoSetTitleAxisAll_Equal(String v) { regTitleAxisAll(CK_EQ, v); }
        public void SetTitleAxisAll_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleAxisAll_NotEqual(fRES(v));
        }
        protected void DoSetTitleAxisAll_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_NES, v);
        }
        public void SetTitleAxisAll_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_GT, fRES(v));
        }
        public void SetTitleAxisAll_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_LT, fRES(v));
        }
        public void SetTitleAxisAll_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_GE, fRES(v));
        }
        public void SetTitleAxisAll_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_LE, fRES(v));
        }
        public void SetTitleAxisAll_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleAxisAll(), "TITLE_AXIS_ALL");
        }
        public void SetTitleAxisAll_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleAxisAll(), "TITLE_AXIS_ALL");
        }
        public void SetTitleAxisAll_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleAxisAll_LikeSearch(v, cLSOP());
        }
        public void SetTitleAxisAll_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleAxisAll(), "TITLE_AXIS_ALL", option);
        }
        public void SetTitleAxisAll_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleAxisAll(), "TITLE_AXIS_ALL", option);
        }
        public void SetTitleAxisAll_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleAxisAll_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAxisAll(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleAxisAll(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleAxisAll(), "TITLE_AXIS_ALL");
        }
        protected abstract ConditionValue getCValueTitleAxisAll();

        public void SetTitleNoanswer_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleNoanswer_Equal(fRES(v));
        }
        protected void DoSetTitleNoanswer_Equal(String v) { regTitleNoanswer(CK_EQ, v); }
        public void SetTitleNoanswer_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleNoanswer_NotEqual(fRES(v));
        }
        protected void DoSetTitleNoanswer_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_NES, v);
        }
        public void SetTitleNoanswer_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_GT, fRES(v));
        }
        public void SetTitleNoanswer_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_LT, fRES(v));
        }
        public void SetTitleNoanswer_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_GE, fRES(v));
        }
        public void SetTitleNoanswer_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_LE, fRES(v));
        }
        public void SetTitleNoanswer_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleNoanswer(), "TITLE_NOANSWER");
        }
        public void SetTitleNoanswer_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleNoanswer(), "TITLE_NOANSWER");
        }
        public void SetTitleNoanswer_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleNoanswer_LikeSearch(v, cLSOP());
        }
        public void SetTitleNoanswer_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleNoanswer(), "TITLE_NOANSWER", option);
        }
        public void SetTitleNoanswer_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleNoanswer(), "TITLE_NOANSWER", option);
        }
        public void SetTitleNoanswer_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleNoanswer_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleNoanswer(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleNoanswer(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleNoanswer(), "TITLE_NOANSWER");
        }
        protected abstract ConditionValue getCValueTitleNoanswer();

        public void SetTitleUnfit_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleUnfit_Equal(fRES(v));
        }
        protected void DoSetTitleUnfit_Equal(String v) { regTitleUnfit(CK_EQ, v); }
        public void SetTitleUnfit_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleUnfit_NotEqual(fRES(v));
        }
        protected void DoSetTitleUnfit_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_NES, v);
        }
        public void SetTitleUnfit_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_GT, fRES(v));
        }
        public void SetTitleUnfit_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_LT, fRES(v));
        }
        public void SetTitleUnfit_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_GE, fRES(v));
        }
        public void SetTitleUnfit_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_LE, fRES(v));
        }
        public void SetTitleUnfit_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleUnfit(), "TITLE_UNFIT");
        }
        public void SetTitleUnfit_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleUnfit(), "TITLE_UNFIT");
        }
        public void SetTitleUnfit_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleUnfit_LikeSearch(v, cLSOP());
        }
        public void SetTitleUnfit_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleUnfit(), "TITLE_UNFIT", option);
        }
        public void SetTitleUnfit_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleUnfit(), "TITLE_UNFIT", option);
        }
        public void SetTitleUnfit_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleUnfit_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleUnfit(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleUnfit(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleUnfit(), "TITLE_UNFIT");
        }
        protected abstract ConditionValue getCValueTitleUnfit();

        public void SetTitleBeforeWb_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleBeforeWb_Equal(fRES(v));
        }
        protected void DoSetTitleBeforeWb_Equal(String v) { regTitleBeforeWb(CK_EQ, v); }
        public void SetTitleBeforeWb_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleBeforeWb_NotEqual(fRES(v));
        }
        protected void DoSetTitleBeforeWb_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_NES, v);
        }
        public void SetTitleBeforeWb_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_GT, fRES(v));
        }
        public void SetTitleBeforeWb_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_LT, fRES(v));
        }
        public void SetTitleBeforeWb_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_GE, fRES(v));
        }
        public void SetTitleBeforeWb_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_LE, fRES(v));
        }
        public void SetTitleBeforeWb_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleBeforeWb(), "TITLE_BEFORE_WB");
        }
        public void SetTitleBeforeWb_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleBeforeWb(), "TITLE_BEFORE_WB");
        }
        public void SetTitleBeforeWb_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleBeforeWb_LikeSearch(v, cLSOP());
        }
        public void SetTitleBeforeWb_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleBeforeWb(), "TITLE_BEFORE_WB", option);
        }
        public void SetTitleBeforeWb_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleBeforeWb(), "TITLE_BEFORE_WB", option);
        }
        public void SetTitleBeforeWb_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleBeforeWb_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleBeforeWb(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleBeforeWb(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleBeforeWb(), "TITLE_BEFORE_WB");
        }
        protected abstract ConditionValue getCValueTitleBeforeWb();

        public void SetFlagStatisticsParameter_Equal(int? v) { regFlagStatisticsParameter(CK_EQ, v); }
        public void SetFlagStatisticsParameter_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagStatisticsParameter(CK_NES, v);
        }
        public void SetFlagStatisticsParameter_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagStatisticsParameter(CK_GT, v);
        }
        public void SetFlagStatisticsParameter_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagStatisticsParameter(CK_LT, v);
        }
        public void SetFlagStatisticsParameter_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagStatisticsParameter(CK_GE, v);
        }
        public void SetFlagStatisticsParameter_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagStatisticsParameter(CK_LE, v);
        }
        public void SetFlagStatisticsParameter_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagStatisticsParameter(), "FLAG_STATISTICS_PARAMETER");
        }
        public void SetFlagStatisticsParameter_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagStatisticsParameter(), "FLAG_STATISTICS_PARAMETER");
        }
        public void SetFlagStatisticsParameter_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagStatisticsParameter(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagStatisticsParameter_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagStatisticsParameter(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagStatisticsParameter(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagStatisticsParameter(), "FLAG_STATISTICS_PARAMETER");
        }
        protected abstract ConditionValue getCValueFlagStatisticsParameter();

        public void SetTitleStatisticsParameter_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleStatisticsParameter_Equal(fRES(v));
        }
        protected void DoSetTitleStatisticsParameter_Equal(String v) { regTitleStatisticsParameter(CK_EQ, v); }
        public void SetTitleStatisticsParameter_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleStatisticsParameter_NotEqual(fRES(v));
        }
        protected void DoSetTitleStatisticsParameter_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_NES, v);
        }
        public void SetTitleStatisticsParameter_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_GT, fRES(v));
        }
        public void SetTitleStatisticsParameter_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_LT, fRES(v));
        }
        public void SetTitleStatisticsParameter_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_GE, fRES(v));
        }
        public void SetTitleStatisticsParameter_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_LE, fRES(v));
        }
        public void SetTitleStatisticsParameter_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleStatisticsParameter(), "TITLE_STATISTICS_PARAMETER");
        }
        public void SetTitleStatisticsParameter_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleStatisticsParameter(), "TITLE_STATISTICS_PARAMETER");
        }
        public void SetTitleStatisticsParameter_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleStatisticsParameter_LikeSearch(v, cLSOP());
        }
        public void SetTitleStatisticsParameter_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleStatisticsParameter(), "TITLE_STATISTICS_PARAMETER", option);
        }
        public void SetTitleStatisticsParameter_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleStatisticsParameter(), "TITLE_STATISTICS_PARAMETER", option);
        }
        public void SetTitleStatisticsParameter_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleStatisticsParameter_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleStatisticsParameter(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleStatisticsParameter(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleStatisticsParameter(), "TITLE_STATISTICS_PARAMETER");
        }
        protected abstract ConditionValue getCValueTitleStatisticsParameter();

        public void SetFlagTotal_Equal(int? v) { regFlagTotal(CK_EQ, v); }
        public void SetFlagTotal_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagTotal(CK_NES, v);
        }
        public void SetFlagTotal_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagTotal(CK_GT, v);
        }
        public void SetFlagTotal_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagTotal(CK_LT, v);
        }
        public void SetFlagTotal_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagTotal(CK_GE, v);
        }
        public void SetFlagTotal_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagTotal(CK_LE, v);
        }
        public void SetFlagTotal_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagTotal(), "FLAG_TOTAL");
        }
        public void SetFlagTotal_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagTotal(), "FLAG_TOTAL");
        }
        public void SetFlagTotal_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagTotal(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagTotal_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagTotal(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagTotal(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagTotal(), "FLAG_TOTAL");
        }
        protected abstract ConditionValue getCValueFlagTotal();

        public void SetTitleTotal_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleTotal_Equal(fRES(v));
        }
        protected void DoSetTitleTotal_Equal(String v) { regTitleTotal(CK_EQ, v); }
        public void SetTitleTotal_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleTotal_NotEqual(fRES(v));
        }
        protected void DoSetTitleTotal_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_NES, v);
        }
        public void SetTitleTotal_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_GT, fRES(v));
        }
        public void SetTitleTotal_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_LT, fRES(v));
        }
        public void SetTitleTotal_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_GE, fRES(v));
        }
        public void SetTitleTotal_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_LE, fRES(v));
        }
        public void SetTitleTotal_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleTotal(), "TITLE_TOTAL");
        }
        public void SetTitleTotal_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleTotal(), "TITLE_TOTAL");
        }
        public void SetTitleTotal_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleTotal_LikeSearch(v, cLSOP());
        }
        public void SetTitleTotal_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleTotal(), "TITLE_TOTAL", option);
        }
        public void SetTitleTotal_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleTotal(), "TITLE_TOTAL", option);
        }
        public void SetTitleTotal_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleTotal_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleTotal(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleTotal(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleTotal(), "TITLE_TOTAL");
        }
        protected abstract ConditionValue getCValueTitleTotal();

        public void SetDpSum_Equal(int? v) { regDpSum(CK_EQ, v); }
        public void SetDpSum_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSum(CK_NES, v);
        }
        public void SetDpSum_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSum(CK_GT, v);
        }
        public void SetDpSum_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSum(CK_LT, v);
        }
        public void SetDpSum_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSum(CK_GE, v);
        }
        public void SetDpSum_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpSum(CK_LE, v);
        }
        public void SetDpSum_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpSum(), "DP_SUM");
        }
        public void SetDpSum_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpSum(), "DP_SUM");
        }
        public void SetDpSum_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSum(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpSum_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSum(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpSum(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpSum(), "DP_SUM");
        }
        protected abstract ConditionValue getCValueDpSum();

        public void SetFlagAvr_Equal(int? v) { regFlagAvr(CK_EQ, v); }
        public void SetFlagAvr_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagAvr(CK_NES, v);
        }
        public void SetFlagAvr_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagAvr(CK_GT, v);
        }
        public void SetFlagAvr_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagAvr(CK_LT, v);
        }
        public void SetFlagAvr_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagAvr(CK_GE, v);
        }
        public void SetFlagAvr_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagAvr(CK_LE, v);
        }
        public void SetFlagAvr_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagAvr(), "FLAG_AVR");
        }
        public void SetFlagAvr_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagAvr(), "FLAG_AVR");
        }
        public void SetFlagAvr_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagAvr(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagAvr_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagAvr(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagAvr(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagAvr(), "FLAG_AVR");
        }
        protected abstract ConditionValue getCValueFlagAvr();

        public void SetTitleAvr_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleAvr_Equal(fRES(v));
        }
        protected void DoSetTitleAvr_Equal(String v) { regTitleAvr(CK_EQ, v); }
        public void SetTitleAvr_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleAvr_NotEqual(fRES(v));
        }
        protected void DoSetTitleAvr_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_NES, v);
        }
        public void SetTitleAvr_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_GT, fRES(v));
        }
        public void SetTitleAvr_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_LT, fRES(v));
        }
        public void SetTitleAvr_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_GE, fRES(v));
        }
        public void SetTitleAvr_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_LE, fRES(v));
        }
        public void SetTitleAvr_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleAvr(), "TITLE_AVR");
        }
        public void SetTitleAvr_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleAvr(), "TITLE_AVR");
        }
        public void SetTitleAvr_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleAvr_LikeSearch(v, cLSOP());
        }
        public void SetTitleAvr_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleAvr(), "TITLE_AVR", option);
        }
        public void SetTitleAvr_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleAvr(), "TITLE_AVR", option);
        }
        public void SetTitleAvr_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleAvr_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleAvr(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleAvr(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleAvr(), "TITLE_AVR");
        }
        protected abstract ConditionValue getCValueTitleAvr();

        public void SetDpAvr_Equal(int? v) { regDpAvr(CK_EQ, v); }
        public void SetDpAvr_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAvr(CK_NES, v);
        }
        public void SetDpAvr_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAvr(CK_GT, v);
        }
        public void SetDpAvr_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAvr(CK_LT, v);
        }
        public void SetDpAvr_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAvr(CK_GE, v);
        }
        public void SetDpAvr_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpAvr(CK_LE, v);
        }
        public void SetDpAvr_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpAvr(), "DP_AVR");
        }
        public void SetDpAvr_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpAvr(), "DP_AVR");
        }
        public void SetDpAvr_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAvr(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpAvr_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAvr(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpAvr(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpAvr(), "DP_AVR");
        }
        protected abstract ConditionValue getCValueDpAvr();

        public void SetFlagSd_Equal(int? v) { regFlagSd(CK_EQ, v); }
        public void SetFlagSd_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagSd(CK_NES, v);
        }
        public void SetFlagSd_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagSd(CK_GT, v);
        }
        public void SetFlagSd_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagSd(CK_LT, v);
        }
        public void SetFlagSd_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagSd(CK_GE, v);
        }
        public void SetFlagSd_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagSd(CK_LE, v);
        }
        public void SetFlagSd_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagSd(), "FLAG_SD");
        }
        public void SetFlagSd_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagSd(), "FLAG_SD");
        }
        public void SetFlagSd_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagSd(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagSd_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagSd(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagSd(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagSd(), "FLAG_SD");
        }
        protected abstract ConditionValue getCValueFlagSd();

        public void SetTitleSd_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleSd_Equal(fRES(v));
        }
        protected void DoSetTitleSd_Equal(String v) { regTitleSd(CK_EQ, v); }
        public void SetTitleSd_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleSd_NotEqual(fRES(v));
        }
        protected void DoSetTitleSd_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_NES, v);
        }
        public void SetTitleSd_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_GT, fRES(v));
        }
        public void SetTitleSd_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_LT, fRES(v));
        }
        public void SetTitleSd_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_GE, fRES(v));
        }
        public void SetTitleSd_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_LE, fRES(v));
        }
        public void SetTitleSd_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleSd(), "TITLE_SD");
        }
        public void SetTitleSd_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleSd(), "TITLE_SD");
        }
        public void SetTitleSd_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleSd_LikeSearch(v, cLSOP());
        }
        public void SetTitleSd_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleSd(), "TITLE_SD", option);
        }
        public void SetTitleSd_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleSd(), "TITLE_SD", option);
        }
        public void SetTitleSd_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleSd_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleSd(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleSd(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleSd(), "TITLE_SD");
        }
        protected abstract ConditionValue getCValueTitleSd();

        public void SetDpSd_Equal(int? v) { regDpSd(CK_EQ, v); }
        public void SetDpSd_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSd(CK_NES, v);
        }
        public void SetDpSd_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSd(CK_GT, v);
        }
        public void SetDpSd_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSd(CK_LT, v);
        }
        public void SetDpSd_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSd(CK_GE, v);
        }
        public void SetDpSd_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpSd(CK_LE, v);
        }
        public void SetDpSd_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpSd(), "DP_SD");
        }
        public void SetDpSd_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpSd(), "DP_SD");
        }
        public void SetDpSd_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSd(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpSd_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpSd(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpSd(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpSd(), "DP_SD");
        }
        protected abstract ConditionValue getCValueDpSd();

        public void SetFlagMin_Equal(int? v) { regFlagMin(CK_EQ, v); }
        public void SetFlagMin_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMin(CK_NES, v);
        }
        public void SetFlagMin_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMin(CK_GT, v);
        }
        public void SetFlagMin_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMin(CK_LT, v);
        }
        public void SetFlagMin_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMin(CK_GE, v);
        }
        public void SetFlagMin_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagMin(CK_LE, v);
        }
        public void SetFlagMin_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagMin(), "FLAG_MIN");
        }
        public void SetFlagMin_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagMin(), "FLAG_MIN");
        }
        public void SetFlagMin_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMin(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagMin_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMin(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagMin(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagMin(), "FLAG_MIN");
        }
        protected abstract ConditionValue getCValueFlagMin();

        public void SetTitleMin_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleMin_Equal(fRES(v));
        }
        protected void DoSetTitleMin_Equal(String v) { regTitleMin(CK_EQ, v); }
        public void SetTitleMin_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleMin_NotEqual(fRES(v));
        }
        protected void DoSetTitleMin_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_NES, v);
        }
        public void SetTitleMin_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_GT, fRES(v));
        }
        public void SetTitleMin_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_LT, fRES(v));
        }
        public void SetTitleMin_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_GE, fRES(v));
        }
        public void SetTitleMin_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_LE, fRES(v));
        }
        public void SetTitleMin_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleMin(), "TITLE_MIN");
        }
        public void SetTitleMin_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleMin(), "TITLE_MIN");
        }
        public void SetTitleMin_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleMin_LikeSearch(v, cLSOP());
        }
        public void SetTitleMin_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleMin(), "TITLE_MIN", option);
        }
        public void SetTitleMin_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleMin(), "TITLE_MIN", option);
        }
        public void SetTitleMin_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleMin_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMin(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleMin(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleMin(), "TITLE_MIN");
        }
        protected abstract ConditionValue getCValueTitleMin();

        public void SetDpMin_Equal(int? v) { regDpMin(CK_EQ, v); }
        public void SetDpMin_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMin(CK_NES, v);
        }
        public void SetDpMin_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMin(CK_GT, v);
        }
        public void SetDpMin_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMin(CK_LT, v);
        }
        public void SetDpMin_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMin(CK_GE, v);
        }
        public void SetDpMin_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpMin(CK_LE, v);
        }
        public void SetDpMin_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpMin(), "DP_MIN");
        }
        public void SetDpMin_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpMin(), "DP_MIN");
        }
        public void SetDpMin_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMin(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpMin_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMin(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpMin(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpMin(), "DP_MIN");
        }
        protected abstract ConditionValue getCValueDpMin();

        public void SetFlagMax_Equal(int? v) { regFlagMax(CK_EQ, v); }
        public void SetFlagMax_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMax(CK_NES, v);
        }
        public void SetFlagMax_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMax(CK_GT, v);
        }
        public void SetFlagMax_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMax(CK_LT, v);
        }
        public void SetFlagMax_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMax(CK_GE, v);
        }
        public void SetFlagMax_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagMax(CK_LE, v);
        }
        public void SetFlagMax_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagMax(), "FLAG_MAX");
        }
        public void SetFlagMax_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagMax(), "FLAG_MAX");
        }
        public void SetFlagMax_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMax(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagMax_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMax(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagMax(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagMax(), "FLAG_MAX");
        }
        protected abstract ConditionValue getCValueFlagMax();

        public void SetTitleMax_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleMax_Equal(fRES(v));
        }
        protected void DoSetTitleMax_Equal(String v) { regTitleMax(CK_EQ, v); }
        public void SetTitleMax_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleMax_NotEqual(fRES(v));
        }
        protected void DoSetTitleMax_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_NES, v);
        }
        public void SetTitleMax_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_GT, fRES(v));
        }
        public void SetTitleMax_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_LT, fRES(v));
        }
        public void SetTitleMax_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_GE, fRES(v));
        }
        public void SetTitleMax_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_LE, fRES(v));
        }
        public void SetTitleMax_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleMax(), "TITLE_MAX");
        }
        public void SetTitleMax_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleMax(), "TITLE_MAX");
        }
        public void SetTitleMax_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleMax_LikeSearch(v, cLSOP());
        }
        public void SetTitleMax_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleMax(), "TITLE_MAX", option);
        }
        public void SetTitleMax_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleMax(), "TITLE_MAX", option);
        }
        public void SetTitleMax_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleMax_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMax(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleMax(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleMax(), "TITLE_MAX");
        }
        protected abstract ConditionValue getCValueTitleMax();

        public void SetDpMax_Equal(int? v) { regDpMax(CK_EQ, v); }
        public void SetDpMax_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMax(CK_NES, v);
        }
        public void SetDpMax_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMax(CK_GT, v);
        }
        public void SetDpMax_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMax(CK_LT, v);
        }
        public void SetDpMax_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMax(CK_GE, v);
        }
        public void SetDpMax_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpMax(CK_LE, v);
        }
        public void SetDpMax_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpMax(), "DP_MAX");
        }
        public void SetDpMax_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpMax(), "DP_MAX");
        }
        public void SetDpMax_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMax(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpMax_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMax(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpMax(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpMax(), "DP_MAX");
        }
        protected abstract ConditionValue getCValueDpMax();

        public void SetFlagMedian_Equal(int? v) { regFlagMedian(CK_EQ, v); }
        public void SetFlagMedian_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMedian(CK_NES, v);
        }
        public void SetFlagMedian_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMedian(CK_GT, v);
        }
        public void SetFlagMedian_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMedian(CK_LT, v);
        }
        public void SetFlagMedian_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMedian(CK_GE, v);
        }
        public void SetFlagMedian_LessEqual(int? v) {
            WhereSetterFlag = true;
            regFlagMedian(CK_LE, v);
        }
        public void SetFlagMedian_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFlagMedian(), "FLAG_MEDIAN");
        }
        public void SetFlagMedian_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFlagMedian(), "FLAG_MEDIAN");
        }
        public void SetFlagMedian_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMedian(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFlagMedian_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFlagMedian(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFlagMedian(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFlagMedian(), "FLAG_MEDIAN");
        }
        protected abstract ConditionValue getCValueFlagMedian();

        public void SetTitleMedian_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleMedian_Equal(fRES(v));
        }
        protected void DoSetTitleMedian_Equal(String v) { regTitleMedian(CK_EQ, v); }
        public void SetTitleMedian_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleMedian_NotEqual(fRES(v));
        }
        protected void DoSetTitleMedian_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_NES, v);
        }
        public void SetTitleMedian_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_GT, fRES(v));
        }
        public void SetTitleMedian_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_LT, fRES(v));
        }
        public void SetTitleMedian_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_GE, fRES(v));
        }
        public void SetTitleMedian_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_LE, fRES(v));
        }
        public void SetTitleMedian_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleMedian(), "TITLE_MEDIAN");
        }
        public void SetTitleMedian_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleMedian(), "TITLE_MEDIAN");
        }
        public void SetTitleMedian_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleMedian_LikeSearch(v, cLSOP());
        }
        public void SetTitleMedian_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleMedian(), "TITLE_MEDIAN", option);
        }
        public void SetTitleMedian_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleMedian(), "TITLE_MEDIAN", option);
        }
        public void SetTitleMedian_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleMedian_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleMedian(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleMedian(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleMedian(), "TITLE_MEDIAN");
        }
        protected abstract ConditionValue getCValueTitleMedian();

        public void SetDpMedian_Equal(int? v) { regDpMedian(CK_EQ, v); }
        public void SetDpMedian_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMedian(CK_NES, v);
        }
        public void SetDpMedian_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMedian(CK_GT, v);
        }
        public void SetDpMedian_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMedian(CK_LT, v);
        }
        public void SetDpMedian_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMedian(CK_GE, v);
        }
        public void SetDpMedian_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpMedian(CK_LE, v);
        }
        public void SetDpMedian_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpMedian(), "DP_MEDIAN");
        }
        public void SetDpMedian_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpMedian(), "DP_MEDIAN");
        }
        public void SetDpMedian_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMedian(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpMedian_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpMedian(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpMedian(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpMedian(), "DP_MEDIAN");
        }
        protected abstract ConditionValue getCValueDpMedian();

        public void SetDpWeight_Equal(int? v) { regDpWeight(CK_EQ, v); }
        public void SetDpWeight_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeight(CK_NES, v);
        }
        public void SetDpWeight_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeight(CK_GT, v);
        }
        public void SetDpWeight_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeight(CK_LT, v);
        }
        public void SetDpWeight_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeight(CK_GE, v);
        }
        public void SetDpWeight_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpWeight(CK_LE, v);
        }
        public void SetDpWeight_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpWeight(), "DP_WEIGHT");
        }
        public void SetDpWeight_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpWeight(), "DP_WEIGHT");
        }
        public void SetDpWeight_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeight(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpWeight_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeight(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpWeight(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpWeight(), "DP_WEIGHT");
        }
        protected abstract ConditionValue getCValueDpWeight();

        public void SetDpWeightAvr_Equal(int? v) { regDpWeightAvr(CK_EQ, v); }
        public void SetDpWeightAvr_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightAvr(CK_NES, v);
        }
        public void SetDpWeightAvr_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightAvr(CK_GT, v);
        }
        public void SetDpWeightAvr_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightAvr(CK_LT, v);
        }
        public void SetDpWeightAvr_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightAvr(CK_GE, v);
        }
        public void SetDpWeightAvr_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpWeightAvr(CK_LE, v);
        }
        public void SetDpWeightAvr_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpWeightAvr(), "DP_WEIGHT_AVR");
        }
        public void SetDpWeightAvr_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpWeightAvr(), "DP_WEIGHT_AVR");
        }
        public void SetDpWeightAvr_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightAvr(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpWeightAvr_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightAvr(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpWeightAvr(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpWeightAvr(), "DP_WEIGHT_AVR");
        }
        protected abstract ConditionValue getCValueDpWeightAvr();

        public void SetExcelType_Equal(int? v) { regExcelType(CK_EQ, v); }
        public void SetExcelType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelType(CK_NES, v);
        }
        public void SetExcelType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelType(CK_GT, v);
        }
        public void SetExcelType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelType(CK_LT, v);
        }
        public void SetExcelType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelType(CK_GE, v);
        }
        public void SetExcelType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regExcelType(CK_LE, v);
        }
        public void SetExcelType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueExcelType(), "EXCEL_TYPE");
        }
        public void SetExcelType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueExcelType(), "EXCEL_TYPE");
        }
        public void SetExcelType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExcelType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExcelType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExcelType(), "EXCEL_TYPE");
        }
        protected abstract ConditionValue getCValueExcelType();

        public void SetPpType_Equal(int? v) { regPpType(CK_EQ, v); }
        public void SetPpType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPpType(CK_NES, v);
        }
        public void SetPpType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPpType(CK_GT, v);
        }
        public void SetPpType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPpType(CK_LT, v);
        }
        public void SetPpType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPpType(CK_GE, v);
        }
        public void SetPpType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPpType(CK_LE, v);
        }
        public void SetPpType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePpType(), "PP_TYPE");
        }
        public void SetPpType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePpType(), "PP_TYPE");
        }
        public void SetPpType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPpType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPpType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPpType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPpType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePpType(), "PP_TYPE");
        }
        protected abstract ConditionValue getCValuePpType();

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

        public void SetTestGtFlag_Equal(int? v) { regTestGtFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of testGtFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestGtFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestGtFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testGtFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestGtFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestGtFlag(CK_EQ, int.Parse(code));
        }
        public void SetTestGtFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestGtFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of testGtFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestGtFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestGtFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testGtFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestGtFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestGtFlag(CK_NES, int.Parse(code));
        }
        public void SetTestGtFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestGtFlag(), "TEST_GT_FLAG");
        }
        public void SetTestGtFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestGtFlag(), "TEST_GT_FLAG");
        }
        public void SetTestGtFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestGtFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestGtFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestGtFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestGtFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestGtFlag(), "TEST_GT_FLAG");
        }
        protected abstract ConditionValue getCValueTestGtFlag();

        public void SetTestCrossFlag_Equal(int? v) { regTestCrossFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of testCrossFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestCrossFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestCrossFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testCrossFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestCrossFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestCrossFlag(CK_EQ, int.Parse(code));
        }
        public void SetTestCrossFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestCrossFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of testCrossFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestCrossFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestCrossFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testCrossFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestCrossFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestCrossFlag(CK_NES, int.Parse(code));
        }
        public void SetTestCrossFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestCrossFlag(), "TEST_CROSS_FLAG");
        }
        public void SetTestCrossFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestCrossFlag(), "TEST_CROSS_FLAG");
        }
        public void SetTestCrossFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestCrossFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestCrossFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestCrossFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestCrossFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestCrossFlag(), "TEST_CROSS_FLAG");
        }
        protected abstract ConditionValue getCValueTestCrossFlag();

        public void SetTestTypeGt_Equal(int? v) { regTestTypeGt(CK_EQ, v); }
        public void SetTestTypeGt_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeGt(CK_NES, v);
        }
        public void SetTestTypeGt_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeGt(CK_GT, v);
        }
        public void SetTestTypeGt_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeGt(CK_LT, v);
        }
        public void SetTestTypeGt_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeGt(CK_GE, v);
        }
        public void SetTestTypeGt_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestTypeGt(CK_LE, v);
        }
        public void SetTestTypeGt_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestTypeGt(), "TEST_TYPE_GT");
        }
        public void SetTestTypeGt_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestTypeGt(), "TEST_TYPE_GT");
        }
        public void SetTestTypeGt_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeGt(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestTypeGt_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeGt(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestTypeGt(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestTypeGt(), "TEST_TYPE_GT");
        }
        protected abstract ConditionValue getCValueTestTypeGt();

        public void SetTestTypeCross_Equal(int? v) { regTestTypeCross(CK_EQ, v); }
        public void SetTestTypeCross_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeCross(CK_NES, v);
        }
        public void SetTestTypeCross_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeCross(CK_GT, v);
        }
        public void SetTestTypeCross_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeCross(CK_LT, v);
        }
        public void SetTestTypeCross_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeCross(CK_GE, v);
        }
        public void SetTestTypeCross_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestTypeCross(CK_LE, v);
        }
        public void SetTestTypeCross_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestTypeCross(), "TEST_TYPE_CROSS");
        }
        public void SetTestTypeCross_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestTypeCross(), "TEST_TYPE_CROSS");
        }
        public void SetTestTypeCross_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeCross(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestTypeCross_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTypeCross(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestTypeCross(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestTypeCross(), "TEST_TYPE_CROSS");
        }
        protected abstract ConditionValue getCValueTestTypeCross();

        public void SetTestSignificanceLvGt_Equal(int? v) { regTestSignificanceLvGt(CK_EQ, v); }
        public void SetTestSignificanceLvGt_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvGt(CK_NES, v);
        }
        public void SetTestSignificanceLvGt_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvGt(CK_GT, v);
        }
        public void SetTestSignificanceLvGt_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvGt(CK_LT, v);
        }
        public void SetTestSignificanceLvGt_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvGt(CK_GE, v);
        }
        public void SetTestSignificanceLvGt_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestSignificanceLvGt(CK_LE, v);
        }
        public void SetTestSignificanceLvGt_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestSignificanceLvGt(), "TEST_SIGNIFICANCE_LV_GT");
        }
        public void SetTestSignificanceLvGt_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestSignificanceLvGt(), "TEST_SIGNIFICANCE_LV_GT");
        }
        public void SetTestSignificanceLvGt_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvGt(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestSignificanceLvGt_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvGt(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestSignificanceLvGt(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestSignificanceLvGt(), "TEST_SIGNIFICANCE_LV_GT");
        }
        protected abstract ConditionValue getCValueTestSignificanceLvGt();

        public void SetTestSignificanceLvCross_Equal(int? v) { regTestSignificanceLvCross(CK_EQ, v); }
        public void SetTestSignificanceLvCross_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvCross(CK_NES, v);
        }
        public void SetTestSignificanceLvCross_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvCross(CK_GT, v);
        }
        public void SetTestSignificanceLvCross_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvCross(CK_LT, v);
        }
        public void SetTestSignificanceLvCross_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvCross(CK_GE, v);
        }
        public void SetTestSignificanceLvCross_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestSignificanceLvCross(CK_LE, v);
        }
        public void SetTestSignificanceLvCross_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestSignificanceLvCross(), "TEST_SIGNIFICANCE_LV_CROSS");
        }
        public void SetTestSignificanceLvCross_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestSignificanceLvCross(), "TEST_SIGNIFICANCE_LV_CROSS");
        }
        public void SetTestSignificanceLvCross_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvCross(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestSignificanceLvCross_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestSignificanceLvCross(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestSignificanceLvCross(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestSignificanceLvCross(), "TEST_SIGNIFICANCE_LV_CROSS");
        }
        protected abstract ConditionValue getCValueTestSignificanceLvCross();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDefaultEnvCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDefaultEnvCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDefaultEnvCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDefaultEnvCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDefaultEnvCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDefaultEnvCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDefaultEnvCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDefaultEnvCB>(delegate(String function, SubQuery<TDefaultEnvCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDefaultEnvCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDefaultEnvCB>", subQuery);
            TDefaultEnvCB cb = new TDefaultEnvCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDefaultEnvCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDefaultEnvCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvCB>", subQuery);
            TDefaultEnvCB cb = new TDefaultEnvCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDefaultEnvCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
