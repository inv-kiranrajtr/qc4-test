
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
    public abstract class AbstractBsTNoticeCQ : AbstractConditionQuery {

        public AbstractBsTNoticeCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_NOTICE"; }
        public override String getTableSqlName() { return "T_NOTICE"; }

        public void SetNoticeId_Equal(decimal? v) { regNoticeId(CK_EQ, v); }
        public void SetNoticeId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeId(CK_NES, v);
        }
        public void SetNoticeId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeId(CK_GT, v);
        }
        public void SetNoticeId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeId(CK_LT, v);
        }
        public void SetNoticeId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeId(CK_GE, v);
        }
        public void SetNoticeId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regNoticeId(CK_LE, v);
        }
        public void SetNoticeId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueNoticeId(), "NOTICE_ID");
        }
        public void SetNoticeId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueNoticeId(), "NOTICE_ID");
        }
        public void SetNoticeId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNoticeId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNoticeId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoticeId(), "NOTICE_ID");
        }
        protected abstract ConditionValue getCValueNoticeId();

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

        public void SetUserId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetUserId_Equal(fRES(v));
        }
        protected void DoSetUserId_Equal(String v) { regUserId(CK_EQ, v); }
        public void SetUserId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetUserId_NotEqual(fRES(v));
        }
        protected void DoSetUserId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUserId(CK_NES, v);
        }
        public void SetUserId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUserId(CK_GT, fRES(v));
        }
        public void SetUserId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUserId(CK_LT, fRES(v));
        }
        public void SetUserId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUserId(CK_GE, fRES(v));
        }
        public void SetUserId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUserId(CK_LE, fRES(v));
        }
        public void SetUserId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueUserId(), "USER_ID");
        }
        public void SetUserId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueUserId(), "USER_ID");
        }
        public void SetUserId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetUserId_LikeSearch(v, cLSOP());
        }
        public void SetUserId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueUserId(), "USER_ID", option);
        }
        public void SetUserId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueUserId(), "USER_ID", option);
        }
        protected void regUserId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUserId(), "USER_ID");
        }
        protected abstract ConditionValue getCValueUserId();

        public void SetDeleteFlag_Equal(int? v) { regDeleteFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of deleteFlag as equal. { = }
        /// はい: 削除を示す
        /// </summary>
        public void SetDeleteFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.True.Code;
            regDeleteFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteFlag as equal. { = }
        /// いいえ: 未削除を示す
        /// </summary>
        public void SetDeleteFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.False.Code;
            regDeleteFlag(CK_EQ, int.Parse(code));
        }
        public void SetDeleteFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of deleteFlag as notEqual. { &lt;&gt; }
        /// はい: 削除を示す
        /// </summary>
        public void SetDeleteFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.True.Code;
            regDeleteFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteFlag as notEqual. { &lt;&gt; }
        /// いいえ: 未削除を示す
        /// </summary>
        public void SetDeleteFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.False.Code;
            regDeleteFlag(CK_NES, int.Parse(code));
        }
        public void SetDeleteFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDeleteFlag(), "DELETE_FLAG");
        }
        public void SetDeleteFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDeleteFlag(), "DELETE_FLAG");
        }
        protected void regDeleteFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteFlag(), "DELETE_FLAG");
        }
        protected abstract ConditionValue getCValueDeleteFlag();

        public void SetNoticeInfo_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNoticeInfo_Equal(fRES(v));
        }
        protected void DoSetNoticeInfo_Equal(String v) { regNoticeInfo(CK_EQ, v); }
        public void SetNoticeInfo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNoticeInfo_NotEqual(fRES(v));
        }
        protected void DoSetNoticeInfo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeInfo(CK_NES, v);
        }
        public void SetNoticeInfo_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeInfo(CK_GT, fRES(v));
        }
        public void SetNoticeInfo_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeInfo(CK_LT, fRES(v));
        }
        public void SetNoticeInfo_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeInfo(CK_GE, fRES(v));
        }
        public void SetNoticeInfo_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeInfo(CK_LE, fRES(v));
        }
        public void SetNoticeInfo_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNoticeInfo(), "NOTICE_INFO");
        }
        public void SetNoticeInfo_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNoticeInfo(), "NOTICE_INFO");
        }
        public void SetNoticeInfo_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNoticeInfo_LikeSearch(v, cLSOP());
        }
        public void SetNoticeInfo_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNoticeInfo(), "NOTICE_INFO", option);
        }
        public void SetNoticeInfo_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNoticeInfo(), "NOTICE_INFO", option);
        }
        protected void regNoticeInfo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoticeInfo(), "NOTICE_INFO");
        }
        protected abstract ConditionValue getCValueNoticeInfo();

        public void SetNoticeType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNoticeType_Equal(fRES(v));
        }
        protected void DoSetNoticeType_Equal(String v) { regNoticeType(CK_EQ, v); }
        public void SetNoticeType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNoticeType_NotEqual(fRES(v));
        }
        protected void DoSetNoticeType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_NES, v);
        }
        public void SetNoticeType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_GT, fRES(v));
        }
        public void SetNoticeType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_LT, fRES(v));
        }
        public void SetNoticeType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_GE, fRES(v));
        }
        public void SetNoticeType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_LE, fRES(v));
        }
        public void SetNoticeType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNoticeType(), "NOTICE_TYPE");
        }
        public void SetNoticeType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNoticeType(), "NOTICE_TYPE");
        }
        public void SetNoticeType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNoticeType_LikeSearch(v, cLSOP());
        }
        public void SetNoticeType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNoticeType(), "NOTICE_TYPE", option);
        }
        public void SetNoticeType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNoticeType(), "NOTICE_TYPE", option);
        }
        public void SetNoticeType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNoticeType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoticeType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNoticeType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoticeType(), "NOTICE_TYPE");
        }
        protected abstract ConditionValue getCValueNoticeType();

        public void SetLinkUrl_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLinkUrl_Equal(fRES(v));
        }
        protected void DoSetLinkUrl_Equal(String v) { regLinkUrl(CK_EQ, v); }
        public void SetLinkUrl_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLinkUrl_NotEqual(fRES(v));
        }
        protected void DoSetLinkUrl_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_NES, v);
        }
        public void SetLinkUrl_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_GT, fRES(v));
        }
        public void SetLinkUrl_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_LT, fRES(v));
        }
        public void SetLinkUrl_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_GE, fRES(v));
        }
        public void SetLinkUrl_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_LE, fRES(v));
        }
        public void SetLinkUrl_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLinkUrl(), "LINK_URL");
        }
        public void SetLinkUrl_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLinkUrl(), "LINK_URL");
        }
        public void SetLinkUrl_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLinkUrl_LikeSearch(v, cLSOP());
        }
        public void SetLinkUrl_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLinkUrl(), "LINK_URL", option);
        }
        public void SetLinkUrl_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLinkUrl(), "LINK_URL", option);
        }
        public void SetLinkUrl_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLinkUrl_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLinkUrl(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLinkUrl(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLinkUrl(), "LINK_URL");
        }
        protected abstract ConditionValue getCValueLinkUrl();

        public void SetExpirationStartdate_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_EQ, v);
        }
        public void SetExpirationStartdate_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_GT, v);
        }
        public void SetExpirationStartdate_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_LT, v);
        }
        public void SetExpirationStartdate_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_GE, v);
        }
        public void SetExpirationStartdate_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_LE, v);
        }
        public void SetExpirationStartdate_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueExpirationStartdate(), "EXPIRATION_STARTDATE", option);
        }
        public void SetExpirationStartdate_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetExpirationStartdate_FromTo(from, to, new DateFromToOption());
        }
        public void SetExpirationStartdate_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExpirationStartdate_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationStartdate(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExpirationStartdate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExpirationStartdate(), "EXPIRATION_STARTDATE");
        }
        protected abstract ConditionValue getCValueExpirationStartdate();

        public void SetExpirationEnddate_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_EQ, v);
        }
        public void SetExpirationEnddate_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_GT, v);
        }
        public void SetExpirationEnddate_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_LT, v);
        }
        public void SetExpirationEnddate_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_GE, v);
        }
        public void SetExpirationEnddate_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_LE, v);
        }
        public void SetExpirationEnddate_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueExpirationEnddate(), "EXPIRATION_ENDDATE", option);
        }
        public void SetExpirationEnddate_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetExpirationEnddate_FromTo(from, to, new DateFromToOption());
        }
        public void SetExpirationEnddate_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExpirationEnddate_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpirationEnddate(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExpirationEnddate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExpirationEnddate(), "EXPIRATION_ENDDATE");
        }
        protected abstract ConditionValue getCValueExpirationEnddate();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TNoticeCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TNoticeCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TNoticeCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TNoticeCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TNoticeCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TNoticeCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TNoticeCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TNoticeCB>(delegate(String function, SubQuery<TNoticeCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TNoticeCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TNoticeCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TNoticeCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TNoticeCB>", subQuery);
            TNoticeCB cb = new TNoticeCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "NOTICE_ID", "NOTICE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TNoticeCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
