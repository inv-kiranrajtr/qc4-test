
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
    public abstract class AbstractBsTWeightbackCQ : AbstractConditionQuery {

        public AbstractBsTWeightbackCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_WEIGHTBACK"; }
        public override String getTableSqlName() { return "T_WEIGHTBACK"; }

        public void SetWeightbackId_Equal(decimal? v) { regWeightbackId(CK_EQ, v); }
        public void SetWeightbackId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_NES, v);
        }
        public void SetWeightbackId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_GT, v);
        }
        public void SetWeightbackId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_LT, v);
        }
        public void SetWeightbackId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_GE, v);
        }
        public void SetWeightbackId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regWeightbackId(CK_LE, v);
        }
        public void SetWeightbackId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueWeightbackId(), "WEIGHTBACK_ID");
        }
        public void SetWeightbackId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueWeightbackId(), "WEIGHTBACK_ID");
        }
        public void ExistsTWeightbackValueList(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_ExistsSubQuery_TWeightbackValueList(cb.Query());
            registerExistsSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_ExistsSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery);
        public void NotExistsTWeightbackValueList(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_NotExistsSubQuery_TWeightbackValueList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_NotExistsSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery);
        public void InScopeTWeightbackValue(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_InScopeSubQuery_TWeightbackValue(cb.Query());
            registerInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "Weightback_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_InScopeSubQuery_TWeightbackValue(TWeightbackValueCQ subQuery);
        public void InScopeTWeightbackValueList(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_InScopeSubQuery_TWeightbackValueList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_InScopeSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery);
        public void NotInScopeTWeightbackValue(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_NotInScopeSubQuery_TWeightbackValue(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "Weightback_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_NotInScopeSubQuery_TWeightbackValue(TWeightbackValueCQ subQuery);
        public void NotInScopeTWeightbackValueList(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_NotInScopeSubQuery_TWeightbackValueList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_NotInScopeSubQuery_TWeightbackValueList(TWeightbackValueCQ subQuery);
        public void xsderiveTWeightbackValueList(String function, SubQuery<TWeightbackValueCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_SpecifyDerivedReferrer_TWeightbackValueList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepWeightbackId_SpecifyDerivedReferrer_TWeightbackValueList(TWeightbackValueCQ subQuery);

        public QDRFunction<TWeightbackValueCB> DerivedTWeightbackValueList() {
            return xcreateQDRFunctionTWeightbackValueList();
        }
        protected QDRFunction<TWeightbackValueCB> xcreateQDRFunctionTWeightbackValueList() {
            return new QDRFunction<TWeightbackValueCB>(delegate(String function, SubQuery<TWeightbackValueCB> subQuery, String operand, Object value) {
                xqderiveTWeightbackValueList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTWeightbackValueList(String function, SubQuery<TWeightbackValueCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_QueryDerivedReferrer_TWeightbackValueList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepWeightbackId_QueryDerivedReferrer_TWeightbackValueListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepWeightbackId_QueryDerivedReferrer_TWeightbackValueList(TWeightbackValueCQ subQuery);
        public abstract String keepWeightbackId_QueryDerivedReferrer_TWeightbackValueListParameter(Object parameterValue);
        public void SetWeightbackId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetWeightbackId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regWeightbackId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackId(), "WEIGHTBACK_ID");
        }
        protected abstract ConditionValue getCValueWeightbackId();

        public void SetWeightbackItemId_Equal(decimal? v) { regWeightbackItemId(CK_EQ, v); }
        public void SetWeightbackItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemId(CK_NES, v);
        }
        public void SetWeightbackItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemId(CK_GT, v);
        }
        public void SetWeightbackItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemId(CK_LT, v);
        }
        public void SetWeightbackItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemId(CK_GE, v);
        }
        public void SetWeightbackItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regWeightbackItemId(CK_LE, v);
        }
        public void SetWeightbackItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueWeightbackItemId(), "WEIGHTBACK_ITEM_ID");
        }
        public void SetWeightbackItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueWeightbackItemId(), "WEIGHTBACK_ITEM_ID");
        }
        protected void regWeightbackItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackItemId(), "WEIGHTBACK_ITEM_ID");
        }
        protected abstract ConditionValue getCValueWeightbackItemId();

        public void SetAssistCalcFlag_Equal(int? v) { regAssistCalcFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of assistCalcFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetAssistCalcFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regAssistCalcFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of assistCalcFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetAssistCalcFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regAssistCalcFlag(CK_EQ, int.Parse(code));
        }
        public void SetAssistCalcFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAssistCalcFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of assistCalcFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetAssistCalcFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regAssistCalcFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of assistCalcFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetAssistCalcFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regAssistCalcFlag(CK_NES, int.Parse(code));
        }
        public void SetAssistCalcFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueAssistCalcFlag(), "ASSIST_CALC_FLAG");
        }
        public void SetAssistCalcFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueAssistCalcFlag(), "ASSIST_CALC_FLAG");
        }
        protected void regAssistCalcFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAssistCalcFlag(), "ASSIST_CALC_FLAG");
        }
        protected abstract ConditionValue getCValueAssistCalcFlag();

        public void SetAssistCalcType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAssistCalcType_Equal(fRES(v));
        }
        protected void DoSetAssistCalcType_Equal(String v) { regAssistCalcType(CK_EQ, v); }
        public void SetAssistCalcType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAssistCalcType_NotEqual(fRES(v));
        }
        protected void DoSetAssistCalcType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAssistCalcType(CK_NES, v);
        }
        public void SetAssistCalcType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAssistCalcType(CK_GT, fRES(v));
        }
        public void SetAssistCalcType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAssistCalcType(CK_LT, fRES(v));
        }
        public void SetAssistCalcType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAssistCalcType(CK_GE, fRES(v));
        }
        public void SetAssistCalcType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAssistCalcType(CK_LE, fRES(v));
        }
        public void SetAssistCalcType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueAssistCalcType(), "ASSIST_CALC_TYPE");
        }
        public void SetAssistCalcType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueAssistCalcType(), "ASSIST_CALC_TYPE");
        }
        public void SetAssistCalcType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetAssistCalcType_LikeSearch(v, cLSOP());
        }
        public void SetAssistCalcType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueAssistCalcType(), "ASSIST_CALC_TYPE", option);
        }
        public void SetAssistCalcType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueAssistCalcType(), "ASSIST_CALC_TYPE", option);
        }
        protected void regAssistCalcType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAssistCalcType(), "ASSIST_CALC_TYPE");
        }
        protected abstract ConditionValue getCValueAssistCalcType();

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

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TWeightbackCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TWeightbackCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TWeightbackCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TWeightbackCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TWeightbackCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TWeightbackCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TWeightbackCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TWeightbackCB>(delegate(String function, SubQuery<TWeightbackCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TWeightbackCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TWeightbackCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TWeightbackCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
