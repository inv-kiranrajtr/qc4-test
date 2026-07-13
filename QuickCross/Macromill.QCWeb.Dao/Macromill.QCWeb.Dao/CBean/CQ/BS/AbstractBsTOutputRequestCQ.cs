
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
    public abstract class AbstractBsTOutputRequestCQ : AbstractConditionQuery {

        public AbstractBsTOutputRequestCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_REQUEST"; }
        public override String getTableSqlName() { return "T_OUTPUT_REQUEST"; }

        public void SetOutputRequestId_Equal(decimal? v) { regOutputRequestId(CK_EQ, v); }
        public void SetOutputRequestId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_NES, v);
        }
        public void SetOutputRequestId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_GT, v);
        }
        public void SetOutputRequestId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_LT, v);
        }
        public void SetOutputRequestId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_GE, v);
        }
        public void SetOutputRequestId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputRequestId(CK_LE, v);
        }
        public void SetOutputRequestId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputRequestId(), "OUTPUT_REQUEST_ID");
        }
        public void SetOutputRequestId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputRequestId(), "OUTPUT_REQUEST_ID");
        }
        public void ExistsTOutputCommonList(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_ExistsSubQuery_TOutputCommonList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_ExistsSubQuery_TOutputCommonList(TOutputCommonCQ subQuery);
        public void NotExistsTOutputCommonList(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_NotExistsSubQuery_TOutputCommonList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_NotExistsSubQuery_TOutputCommonList(TOutputCommonCQ subQuery);
        public void InScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_InScopeSubQuery_TOutputCommon(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "Output_Request_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        public void InScopeTOutputCommonList(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_InScopeSubQuery_TOutputCommonList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_InScopeSubQuery_TOutputCommonList(TOutputCommonCQ subQuery);
        public void NotInScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_NotInScopeSubQuery_TOutputCommon(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "Output_Request_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        public void NotInScopeTOutputCommonList(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_NotInScopeSubQuery_TOutputCommonList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_NotInScopeSubQuery_TOutputCommonList(TOutputCommonCQ subQuery);
        public void xsderiveTOutputCommonList(String function, SubQuery<TOutputCommonCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_SpecifyDerivedReferrer_TOutputCommonList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputRequestId_SpecifyDerivedReferrer_TOutputCommonList(TOutputCommonCQ subQuery);

        public QDRFunction<TOutputCommonCB> DerivedTOutputCommonList() {
            return xcreateQDRFunctionTOutputCommonList();
        }
        protected QDRFunction<TOutputCommonCB> xcreateQDRFunctionTOutputCommonList() {
            return new QDRFunction<TOutputCommonCB>(delegate(String function, SubQuery<TOutputCommonCB> subQuery, String operand, Object value) {
                xqderiveTOutputCommonList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputCommonList(String function, SubQuery<TOutputCommonCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_QueryDerivedReferrer_TOutputCommonList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputRequestId_QueryDerivedReferrer_TOutputCommonListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputRequestId_QueryDerivedReferrer_TOutputCommonList(TOutputCommonCQ subQuery);
        public abstract String keepOutputRequestId_QueryDerivedReferrer_TOutputCommonListParameter(Object parameterValue);
        public void SetOutputRequestId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputRequestId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputRequestId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputRequestId(), "OUTPUT_REQUEST_ID");
        }
        protected abstract ConditionValue getCValueOutputRequestId();

        public void SetRequestServerCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetRequestServerCode_Equal(fRES(v));
        }
        protected void DoSetRequestServerCode_Equal(String v) { regRequestServerCode(CK_EQ, v); }
        public void SetRequestServerCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetRequestServerCode_NotEqual(fRES(v));
        }
        protected void DoSetRequestServerCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestServerCode(CK_NES, v);
        }
        public void SetRequestServerCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestServerCode(CK_GT, fRES(v));
        }
        public void SetRequestServerCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestServerCode(CK_LT, fRES(v));
        }
        public void SetRequestServerCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestServerCode(CK_GE, fRES(v));
        }
        public void SetRequestServerCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestServerCode(CK_LE, fRES(v));
        }
        public void SetRequestServerCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueRequestServerCode(), "REQUEST_SERVER_CODE");
        }
        public void SetRequestServerCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueRequestServerCode(), "REQUEST_SERVER_CODE");
        }
        public void SetRequestServerCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetRequestServerCode_LikeSearch(v, cLSOP());
        }
        public void SetRequestServerCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueRequestServerCode(), "REQUEST_SERVER_CODE", option);
        }
        public void SetRequestServerCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueRequestServerCode(), "REQUEST_SERVER_CODE", option);
        }
        protected void regRequestServerCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRequestServerCode(), "REQUEST_SERVER_CODE");
        }
        protected abstract ConditionValue getCValueRequestServerCode();

        public void SetRequestUserId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetRequestUserId_Equal(fRES(v));
        }
        protected void DoSetRequestUserId_Equal(String v) { regRequestUserId(CK_EQ, v); }
        public void SetRequestUserId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetRequestUserId_NotEqual(fRES(v));
        }
        protected void DoSetRequestUserId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestUserId(CK_NES, v);
        }
        public void SetRequestUserId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestUserId(CK_GT, fRES(v));
        }
        public void SetRequestUserId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestUserId(CK_LT, fRES(v));
        }
        public void SetRequestUserId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestUserId(CK_GE, fRES(v));
        }
        public void SetRequestUserId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestUserId(CK_LE, fRES(v));
        }
        public void SetRequestUserId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueRequestUserId(), "REQUEST_USER_ID");
        }
        public void SetRequestUserId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueRequestUserId(), "REQUEST_USER_ID");
        }
        public void SetRequestUserId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetRequestUserId_LikeSearch(v, cLSOP());
        }
        public void SetRequestUserId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueRequestUserId(), "REQUEST_USER_ID", option);
        }
        public void SetRequestUserId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueRequestUserId(), "REQUEST_USER_ID", option);
        }
        protected void regRequestUserId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRequestUserId(), "REQUEST_USER_ID");
        }
        protected abstract ConditionValue getCValueRequestUserId();

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

        public void SetLastDownloadUserid_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastDownloadUserid_Equal(fRES(v));
        }
        protected void DoSetLastDownloadUserid_Equal(String v) { regLastDownloadUserid(CK_EQ, v); }
        public void SetLastDownloadUserid_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastDownloadUserid_NotEqual(fRES(v));
        }
        protected void DoSetLastDownloadUserid_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_NES, v);
        }
        public void SetLastDownloadUserid_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_GT, fRES(v));
        }
        public void SetLastDownloadUserid_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_LT, fRES(v));
        }
        public void SetLastDownloadUserid_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_GE, fRES(v));
        }
        public void SetLastDownloadUserid_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_LE, fRES(v));
        }
        public void SetLastDownloadUserid_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLastDownloadUserid(), "LAST_DOWNLOAD_USERID");
        }
        public void SetLastDownloadUserid_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLastDownloadUserid(), "LAST_DOWNLOAD_USERID");
        }
        public void SetLastDownloadUserid_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastDownloadUserid_LikeSearch(v, cLSOP());
        }
        public void SetLastDownloadUserid_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLastDownloadUserid(), "LAST_DOWNLOAD_USERID", option);
        }
        public void SetLastDownloadUserid_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLastDownloadUserid(), "LAST_DOWNLOAD_USERID", option);
        }
        public void SetLastDownloadUserid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastDownloadUserid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadUserid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastDownloadUserid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastDownloadUserid(), "LAST_DOWNLOAD_USERID");
        }
        protected abstract ConditionValue getCValueLastDownloadUserid();

        public void SetRequestDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_EQ, v);
        }
        public void SetRequestDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_GT, v);
        }
        public void SetRequestDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_LT, v);
        }
        public void SetRequestDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_GE, v);
        }
        public void SetRequestDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRequestDatetime(CK_LE, v);
        }
        public void SetRequestDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueRequestDatetime(), "REQUEST_DATETIME", option);
        }
        public void SetRequestDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetRequestDatetime_FromTo(from, to, new DateFromToOption());
        }
        protected void regRequestDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRequestDatetime(), "REQUEST_DATETIME");
        }
        protected abstract ConditionValue getCValueRequestDatetime();

        public void SetDownloadPath_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDownloadPath_Equal(fRES(v));
        }
        protected void DoSetDownloadPath_Equal(String v) { regDownloadPath(CK_EQ, v); }
        public void SetDownloadPath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDownloadPath_NotEqual(fRES(v));
        }
        protected void DoSetDownloadPath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_NES, v);
        }
        public void SetDownloadPath_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_GT, fRES(v));
        }
        public void SetDownloadPath_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_LT, fRES(v));
        }
        public void SetDownloadPath_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_GE, fRES(v));
        }
        public void SetDownloadPath_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_LE, fRES(v));
        }
        public void SetDownloadPath_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueDownloadPath(), "DOWNLOAD_PATH");
        }
        public void SetDownloadPath_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueDownloadPath(), "DOWNLOAD_PATH");
        }
        public void SetDownloadPath_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDownloadPath_LikeSearch(v, cLSOP());
        }
        public void SetDownloadPath_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueDownloadPath(), "DOWNLOAD_PATH", option);
        }
        public void SetDownloadPath_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueDownloadPath(), "DOWNLOAD_PATH", option);
        }
        public void SetDownloadPath_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDownloadPath_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDownloadPath(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDownloadPath(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDownloadPath(), "DOWNLOAD_PATH");
        }
        protected abstract ConditionValue getCValueDownloadPath();

        public void SetProcServerCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetProcServerCode_Equal(fRES(v));
        }
        protected void DoSetProcServerCode_Equal(String v) { regProcServerCode(CK_EQ, v); }
        public void SetProcServerCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetProcServerCode_NotEqual(fRES(v));
        }
        protected void DoSetProcServerCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_NES, v);
        }
        public void SetProcServerCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_GT, fRES(v));
        }
        public void SetProcServerCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_LT, fRES(v));
        }
        public void SetProcServerCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_GE, fRES(v));
        }
        public void SetProcServerCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_LE, fRES(v));
        }
        public void SetProcServerCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueProcServerCode(), "PROC_SERVER_CODE");
        }
        public void SetProcServerCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueProcServerCode(), "PROC_SERVER_CODE");
        }
        public void SetProcServerCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetProcServerCode_LikeSearch(v, cLSOP());
        }
        public void SetProcServerCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueProcServerCode(), "PROC_SERVER_CODE", option);
        }
        public void SetProcServerCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueProcServerCode(), "PROC_SERVER_CODE", option);
        }
        public void SetProcServerCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetProcServerCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcServerCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regProcServerCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcServerCode(), "PROC_SERVER_CODE");
        }
        protected abstract ConditionValue getCValueProcServerCode();

        public void SetStatusCode_Equal(int? v) { regStatusCode(CK_EQ, v); }
        public void SetStatusCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_NES, v);
        }
        public void SetStatusCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_GT, v);
        }
        public void SetStatusCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_LT, v);
        }
        public void SetStatusCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_GE, v);
        }
        public void SetStatusCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regStatusCode(CK_LE, v);
        }
        public void SetStatusCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueStatusCode(), "STATUS_CODE");
        }
        public void SetStatusCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueStatusCode(), "STATUS_CODE");
        }
        protected void regStatusCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueStatusCode(), "STATUS_CODE");
        }
        protected abstract ConditionValue getCValueStatusCode();

        public void SetDescription_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDescription_Equal(fRES(v));
        }
        protected void DoSetDescription_Equal(String v) { regDescription(CK_EQ, v); }
        public void SetDescription_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDescription_NotEqual(fRES(v));
        }
        protected void DoSetDescription_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_NES, v);
        }
        public void SetDescription_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_GT, fRES(v));
        }
        public void SetDescription_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_LT, fRES(v));
        }
        public void SetDescription_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_GE, fRES(v));
        }
        public void SetDescription_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_LE, fRES(v));
        }
        public void SetDescription_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueDescription(), "DESCRIPTION");
        }
        public void SetDescription_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueDescription(), "DESCRIPTION");
        }
        public void SetDescription_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDescription_LikeSearch(v, cLSOP());
        }
        public void SetDescription_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueDescription(), "DESCRIPTION", option);
        }
        public void SetDescription_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueDescription(), "DESCRIPTION", option);
        }
        public void SetDescription_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDescription_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDescription(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDescription(), "DESCRIPTION");
        }
        protected abstract ConditionValue getCValueDescription();

        public void SetEndDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_EQ, v);
        }
        public void SetEndDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_GT, v);
        }
        public void SetEndDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_LT, v);
        }
        public void SetEndDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_GE, v);
        }
        public void SetEndDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_LE, v);
        }
        public void SetEndDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueEndDatetime(), "END_DATETIME", option);
        }
        public void SetEndDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetEndDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetEndDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetEndDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEndDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regEndDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEndDatetime(), "END_DATETIME");
        }
        protected abstract ConditionValue getCValueEndDatetime();

        public void SetLastDownloadDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_EQ, v);
        }
        public void SetLastDownloadDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_GT, v);
        }
        public void SetLastDownloadDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_LT, v);
        }
        public void SetLastDownloadDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_GE, v);
        }
        public void SetLastDownloadDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_LE, v);
        }
        public void SetLastDownloadDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueLastDownloadDatetime(), "LAST_DOWNLOAD_DATETIME", option);
        }
        public void SetLastDownloadDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastDownloadDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetLastDownloadDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastDownloadDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastDownloadDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastDownloadDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastDownloadDatetime(), "LAST_DOWNLOAD_DATETIME");
        }
        protected abstract ConditionValue getCValueLastDownloadDatetime();

        public void SetExcelbookType_Equal(int? v) { regExcelbookType(CK_EQ, v); }
        /// <summary>
        /// Set the value of EXL2003 of excelbookType as equal. { = }
        /// 2003形式: 2003形式を示す
        /// </summary>
        public void SetExcelbookType_Equal_EXL2003() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ExcelbookType.EXL2003.Code;
            regExcelbookType(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of EXL2007 of excelbookType as equal. { = }
        /// 2007形式: 2007形式を示す
        /// </summary>
        public void SetExcelbookType_Equal_EXL2007() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ExcelbookType.EXL2007.Code;
            regExcelbookType(CK_EQ, int.Parse(code));
        }
        public void SetExcelbookType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookType(CK_NES, v);
        }
        /// <summary>
        /// Set the value of EXL2003 of excelbookType as notEqual. { &lt;&gt; }
        /// 2003形式: 2003形式を示す
        /// </summary>
        public void SetExcelbookType_NotEqual_EXL2003() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ExcelbookType.EXL2003.Code;
            regExcelbookType(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of EXL2007 of excelbookType as notEqual. { &lt;&gt; }
        /// 2007形式: 2007形式を示す
        /// </summary>
        public void SetExcelbookType_NotEqual_EXL2007() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.ExcelbookType.EXL2007.Code;
            regExcelbookType(CK_NES, int.Parse(code));
        }
        public void SetExcelbookType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueExcelbookType(), "EXCELBOOK_TYPE");
        }
        public void SetExcelbookType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueExcelbookType(), "EXCELBOOK_TYPE");
        }
        public void SetExcelbookType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExcelbookType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExcelbookType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExcelbookType(), "EXCELBOOK_TYPE");
        }
        protected abstract ConditionValue getCValueExcelbookType();

        public void SetNumericAnswerViewCode_Equal(int? v) { regNumericAnswerViewCode(CK_EQ, v); }
        public void SetNumericAnswerViewCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumericAnswerViewCode(CK_NES, v);
        }
        public void SetNumericAnswerViewCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumericAnswerViewCode(CK_GT, v);
        }
        public void SetNumericAnswerViewCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumericAnswerViewCode(CK_LT, v);
        }
        public void SetNumericAnswerViewCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumericAnswerViewCode(CK_GE, v);
        }
        public void SetNumericAnswerViewCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regNumericAnswerViewCode(CK_LE, v);
        }
        public void SetNumericAnswerViewCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNumericAnswerViewCode(), "NUMERIC_ANSWER_VIEW_CODE");
        }
        public void SetNumericAnswerViewCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNumericAnswerViewCode(), "NUMERIC_ANSWER_VIEW_CODE");
        }
        public void SetNumericAnswerViewCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumericAnswerViewCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNumericAnswerViewCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumericAnswerViewCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNumericAnswerViewCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNumericAnswerViewCode(), "NUMERIC_ANSWER_VIEW_CODE");
        }
        protected abstract ConditionValue getCValueNumericAnswerViewCode();

        public void SetDpTotal_Equal(int? v) { regDpTotal(CK_EQ, v); }
        public void SetDpTotal_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpTotal(CK_NES, v);
        }
        public void SetDpTotal_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpTotal(CK_GT, v);
        }
        public void SetDpTotal_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpTotal(CK_LT, v);
        }
        public void SetDpTotal_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpTotal(CK_GE, v);
        }
        public void SetDpTotal_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpTotal(CK_LE, v);
        }
        public void SetDpTotal_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpTotal(), "DP_TOTAL");
        }
        public void SetDpTotal_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpTotal(), "DP_TOTAL");
        }
        public void SetDpTotal_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpTotal(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpTotal_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpTotal(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpTotal(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpTotal(), "DP_TOTAL");
        }
        protected abstract ConditionValue getCValueDpTotal();

        public void SetDpAverage_Equal(int? v) { regDpAverage(CK_EQ, v); }
        public void SetDpAverage_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAverage(CK_NES, v);
        }
        public void SetDpAverage_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAverage(CK_GT, v);
        }
        public void SetDpAverage_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAverage(CK_LT, v);
        }
        public void SetDpAverage_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAverage(CK_GE, v);
        }
        public void SetDpAverage_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpAverage(CK_LE, v);
        }
        public void SetDpAverage_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpAverage(), "DP_AVERAGE");
        }
        public void SetDpAverage_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpAverage(), "DP_AVERAGE");
        }
        public void SetDpAverage_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAverage(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpAverage_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpAverage(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpAverage(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpAverage(), "DP_AVERAGE");
        }
        protected abstract ConditionValue getCValueDpAverage();

        public void SetDpStandardDiv_Equal(int? v) { regDpStandardDiv(CK_EQ, v); }
        public void SetDpStandardDiv_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpStandardDiv(CK_NES, v);
        }
        public void SetDpStandardDiv_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpStandardDiv(CK_GT, v);
        }
        public void SetDpStandardDiv_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpStandardDiv(CK_LT, v);
        }
        public void SetDpStandardDiv_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpStandardDiv(CK_GE, v);
        }
        public void SetDpStandardDiv_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpStandardDiv(CK_LE, v);
        }
        public void SetDpStandardDiv_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpStandardDiv(), "DP_STANDARD_DIV");
        }
        public void SetDpStandardDiv_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpStandardDiv(), "DP_STANDARD_DIV");
        }
        public void SetDpStandardDiv_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpStandardDiv(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpStandardDiv_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpStandardDiv(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpStandardDiv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpStandardDiv(), "DP_STANDARD_DIV");
        }
        protected abstract ConditionValue getCValueDpStandardDiv();

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

        public void SetDpWeightavr_Equal(int? v) { regDpWeightavr(CK_EQ, v); }
        public void SetDpWeightavr_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightavr(CK_NES, v);
        }
        public void SetDpWeightavr_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightavr(CK_GT, v);
        }
        public void SetDpWeightavr_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightavr(CK_LT, v);
        }
        public void SetDpWeightavr_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightavr(CK_GE, v);
        }
        public void SetDpWeightavr_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDpWeightavr(CK_LE, v);
        }
        public void SetDpWeightavr_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDpWeightavr(), "DP_WEIGHTAVR");
        }
        public void SetDpWeightavr_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDpWeightavr(), "DP_WEIGHTAVR");
        }
        public void SetDpWeightavr_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightavr(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDpWeightavr_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDpWeightavr(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDpWeightavr(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDpWeightavr(), "DP_WEIGHTAVR");
        }
        protected abstract ConditionValue getCValueDpWeightavr();

        public void SetProcWeight_Equal(int? v) { regProcWeight(CK_EQ, v); }
        public void SetProcWeight_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcWeight(CK_NES, v);
        }
        public void SetProcWeight_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcWeight(CK_GT, v);
        }
        public void SetProcWeight_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcWeight(CK_LT, v);
        }
        public void SetProcWeight_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcWeight(CK_GE, v);
        }
        public void SetProcWeight_LessEqual(int? v) {
            WhereSetterFlag = true;
            regProcWeight(CK_LE, v);
        }
        public void SetProcWeight_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueProcWeight(), "PROC_WEIGHT");
        }
        public void SetProcWeight_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueProcWeight(), "PROC_WEIGHT");
        }
        public void SetProcWeight_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcWeight(CK_ISN, DUMMY_OBJECT);
        }
        public void SetProcWeight_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcWeight(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regProcWeight(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcWeight(), "PROC_WEIGHT");
        }
        protected abstract ConditionValue getCValueProcWeight();

        public void SetOutputReportsetInfoId_Equal(decimal? v) { regOutputReportsetInfoId(CK_EQ, v); }
        public void SetOutputReportsetInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_NES, v);
        }
        public void SetOutputReportsetInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_GT, v);
        }
        public void SetOutputReportsetInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_LT, v);
        }
        public void SetOutputReportsetInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_GE, v);
        }
        public void SetOutputReportsetInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputReportsetInfoId(CK_LE, v);
        }
        public void SetOutputReportsetInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputReportsetInfoId(), "OUTPUT_REPORTSET_INFO_ID");
        }
        public void SetOutputReportsetInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputReportsetInfoId(), "OUTPUT_REPORTSET_INFO_ID");
        }
        public void InScopeTOutputReportsetInfo(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_InScopeSubQuery_TOutputReportsetInfo(TOutputReportsetInfoCQ subQuery);
        public void NotInScopeTOutputReportsetInfo(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputReportsetInfo(TOutputReportsetInfoCQ subQuery);
        public void SetOutputReportsetInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputReportsetInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputReportsetInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputReportsetInfoId(), "OUTPUT_REPORTSET_INFO_ID");
        }
        protected abstract ConditionValue getCValueOutputReportsetInfoId();

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

        public void SetViewSurveyName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewSurveyName_Equal(fRES(v));
        }
        protected void DoSetViewSurveyName_Equal(String v) { regViewSurveyName(CK_EQ, v); }
        public void SetViewSurveyName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewSurveyName_NotEqual(fRES(v));
        }
        protected void DoSetViewSurveyName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_NES, v);
        }
        public void SetViewSurveyName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_GT, fRES(v));
        }
        public void SetViewSurveyName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_LT, fRES(v));
        }
        public void SetViewSurveyName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_GE, fRES(v));
        }
        public void SetViewSurveyName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_LE, fRES(v));
        }
        public void SetViewSurveyName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueViewSurveyName(), "VIEW_SURVEY_NAME");
        }
        public void SetViewSurveyName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueViewSurveyName(), "VIEW_SURVEY_NAME");
        }
        public void SetViewSurveyName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetViewSurveyName_LikeSearch(v, cLSOP());
        }
        public void SetViewSurveyName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueViewSurveyName(), "VIEW_SURVEY_NAME", option);
        }
        public void SetViewSurveyName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueViewSurveyName(), "VIEW_SURVEY_NAME", option);
        }
        public void SetViewSurveyName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetViewSurveyName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewSurveyName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regViewSurveyName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueViewSurveyName(), "VIEW_SURVEY_NAME");
        }
        protected abstract ConditionValue getCValueViewSurveyName();

        public void SetLanguage_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLanguage_Equal(fRES(v));
        }
        protected void DoSetLanguage_Equal(String v) { regLanguage(CK_EQ, v); }
        public void SetLanguage_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLanguage_NotEqual(fRES(v));
        }
        protected void DoSetLanguage_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_NES, v);
        }
        public void SetLanguage_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_GT, fRES(v));
        }
        public void SetLanguage_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_LT, fRES(v));
        }
        public void SetLanguage_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_GE, fRES(v));
        }
        public void SetLanguage_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_LE, fRES(v));
        }
        public void SetLanguage_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLanguage(), "LANGUAGE");
        }
        public void SetLanguage_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLanguage(), "LANGUAGE");
        }
        public void SetLanguage_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLanguage_LikeSearch(v, cLSOP());
        }
        public void SetLanguage_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLanguage(), "LANGUAGE", option);
        }
        public void SetLanguage_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLanguage(), "LANGUAGE", option);
        }
        protected void regLanguage(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLanguage(), "LANGUAGE");
        }
        protected abstract ConditionValue getCValueLanguage();

        public void SetShowZeroNaIvCode_Equal(int? v) { regShowZeroNaIvCode(CK_EQ, v); }
        public void SetShowZeroNaIvCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regShowZeroNaIvCode(CK_NES, v);
        }
        public void SetShowZeroNaIvCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regShowZeroNaIvCode(CK_GT, v);
        }
        public void SetShowZeroNaIvCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regShowZeroNaIvCode(CK_LT, v);
        }
        public void SetShowZeroNaIvCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regShowZeroNaIvCode(CK_GE, v);
        }
        public void SetShowZeroNaIvCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regShowZeroNaIvCode(CK_LE, v);
        }
        public void SetShowZeroNaIvCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueShowZeroNaIvCode(), "SHOW_ZERO_NA_IV_CODE");
        }
        public void SetShowZeroNaIvCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueShowZeroNaIvCode(), "SHOW_ZERO_NA_IV_CODE");
        }
        public void SetShowZeroNaIvCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regShowZeroNaIvCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetShowZeroNaIvCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regShowZeroNaIvCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regShowZeroNaIvCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueShowZeroNaIvCode(), "SHOW_ZERO_NA_IV_CODE");
        }
        protected abstract ConditionValue getCValueShowZeroNaIvCode();

        public void SetMergeAxisCellsFlag_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeAxisCellsFlag_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of True of mergeAxisCellsFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetMergeAxisCellsFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeAxisCellsFlag_Equal(CDef.Flag.True.Code);
        }
        /// <summary>
        /// Set the value of False of mergeAxisCellsFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetMergeAxisCellsFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeAxisCellsFlag_Equal(CDef.Flag.False.Code);
        }
        protected void DoSetMergeAxisCellsFlag_Equal(String v) { regMergeAxisCellsFlag(CK_EQ, v); }
        public void SetMergeAxisCellsFlag_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeAxisCellsFlag_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of True of mergeAxisCellsFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetMergeAxisCellsFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeAxisCellsFlag_NotEqual(CDef.Flag.True.Code);
        }
        /// <summary>
        /// Set the value of False of mergeAxisCellsFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetMergeAxisCellsFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeAxisCellsFlag_NotEqual(CDef.Flag.False.Code);
        }
        protected void DoSetMergeAxisCellsFlag_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeAxisCellsFlag(CK_NES, v);
        }
        public void SetMergeAxisCellsFlag_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMergeAxisCellsFlag(), "MERGE_AXIS_CELLS_FLAG");
        }
        public void SetMergeAxisCellsFlag_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMergeAxisCellsFlag(), "MERGE_AXIS_CELLS_FLAG");
        }
        public void SetMergeAxisCellsFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeAxisCellsFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMergeAxisCellsFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeAxisCellsFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMergeAxisCellsFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMergeAxisCellsFlag(), "MERGE_AXIS_CELLS_FLAG");
        }
        protected abstract ConditionValue getCValueMergeAxisCellsFlag();

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
        public void SetScenarioName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetScenarioName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regScenarioName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioName(), "SCENARIO_NAME");
        }
        protected abstract ConditionValue getCValueScenarioName();

        public void SetStartDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_EQ, v);
        }
        public void SetStartDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_GT, v);
        }
        public void SetStartDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_LT, v);
        }
        public void SetStartDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_GE, v);
        }
        public void SetStartDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_LE, v);
        }
        public void SetStartDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueStartDatetime(), "START_DATETIME", option);
        }
        public void SetStartDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetStartDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetStartDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetStartDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStartDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regStartDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueStartDatetime(), "START_DATETIME");
        }
        protected abstract ConditionValue getCValueStartDatetime();

        public void SetTestLogFlag_Equal(int? v) { regTestLogFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of testLogFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestLogFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestLogFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testLogFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestLogFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestLogFlag(CK_EQ, int.Parse(code));
        }
        public void SetTestLogFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestLogFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of testLogFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTestLogFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTestLogFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of testLogFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTestLogFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTestLogFlag(CK_NES, int.Parse(code));
        }
        public void SetTestLogFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestLogFlag(), "TEST_LOG_FLAG");
        }
        public void SetTestLogFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestLogFlag(), "TEST_LOG_FLAG");
        }
        public void SetTestLogFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestLogFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestLogFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestLogFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestLogFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestLogFlag(), "TEST_LOG_FLAG");
        }
        protected abstract ConditionValue getCValueTestLogFlag();

        public void SetTsvFileSizeGt_Equal(long? v) { regTsvFileSizeGt(CK_EQ, v); }
        public void SetTsvFileSizeGt_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeGt(CK_NES, v);
        }
        public void SetTsvFileSizeGt_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeGt(CK_GT, v);
        }
        public void SetTsvFileSizeGt_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeGt(CK_LT, v);
        }
        public void SetTsvFileSizeGt_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeGt(CK_GE, v);
        }
        public void SetTsvFileSizeGt_LessEqual(long? v) {
            WhereSetterFlag = true;
            regTsvFileSizeGt(CK_LE, v);
        }
        public void SetTsvFileSizeGt_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueTsvFileSizeGt(), "TSV_FILE_SIZE_GT");
        }
        public void SetTsvFileSizeGt_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueTsvFileSizeGt(), "TSV_FILE_SIZE_GT");
        }
        protected void regTsvFileSizeGt(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTsvFileSizeGt(), "TSV_FILE_SIZE_GT");
        }
        protected abstract ConditionValue getCValueTsvFileSizeGt();

        public void SetTsvFileSizeCross_Equal(long? v) { regTsvFileSizeCross(CK_EQ, v); }
        public void SetTsvFileSizeCross_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeCross(CK_NES, v);
        }
        public void SetTsvFileSizeCross_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeCross(CK_GT, v);
        }
        public void SetTsvFileSizeCross_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeCross(CK_LT, v);
        }
        public void SetTsvFileSizeCross_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeCross(CK_GE, v);
        }
        public void SetTsvFileSizeCross_LessEqual(long? v) {
            WhereSetterFlag = true;
            regTsvFileSizeCross(CK_LE, v);
        }
        public void SetTsvFileSizeCross_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueTsvFileSizeCross(), "TSV_FILE_SIZE_CROSS");
        }
        public void SetTsvFileSizeCross_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueTsvFileSizeCross(), "TSV_FILE_SIZE_CROSS");
        }
        protected void regTsvFileSizeCross(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTsvFileSizeCross(), "TSV_FILE_SIZE_CROSS");
        }
        protected abstract ConditionValue getCValueTsvFileSizeCross();

        public void SetTsvFileSizeFa_Equal(long? v) { regTsvFileSizeFa(CK_EQ, v); }
        public void SetTsvFileSizeFa_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeFa(CK_NES, v);
        }
        public void SetTsvFileSizeFa_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeFa(CK_GT, v);
        }
        public void SetTsvFileSizeFa_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeFa(CK_LT, v);
        }
        public void SetTsvFileSizeFa_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeFa(CK_GE, v);
        }
        public void SetTsvFileSizeFa_LessEqual(long? v) {
            WhereSetterFlag = true;
            regTsvFileSizeFa(CK_LE, v);
        }
        public void SetTsvFileSizeFa_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueTsvFileSizeFa(), "TSV_FILE_SIZE_FA");
        }
        public void SetTsvFileSizeFa_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueTsvFileSizeFa(), "TSV_FILE_SIZE_FA");
        }
        protected void regTsvFileSizeFa(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTsvFileSizeFa(), "TSV_FILE_SIZE_FA");
        }
        protected abstract ConditionValue getCValueTsvFileSizeFa();

        public void SetTsvFileSizeDataOutput_Equal(long? v) { regTsvFileSizeDataOutput(CK_EQ, v); }
        public void SetTsvFileSizeDataOutput_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeDataOutput(CK_NES, v);
        }
        public void SetTsvFileSizeDataOutput_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeDataOutput(CK_GT, v);
        }
        public void SetTsvFileSizeDataOutput_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeDataOutput(CK_LT, v);
        }
        public void SetTsvFileSizeDataOutput_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFileSizeDataOutput(CK_GE, v);
        }
        public void SetTsvFileSizeDataOutput_LessEqual(long? v) {
            WhereSetterFlag = true;
            regTsvFileSizeDataOutput(CK_LE, v);
        }
        public void SetTsvFileSizeDataOutput_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueTsvFileSizeDataOutput(), "TSV_FILE_SIZE_DATA_OUTPUT");
        }
        public void SetTsvFileSizeDataOutput_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueTsvFileSizeDataOutput(), "TSV_FILE_SIZE_DATA_OUTPUT");
        }
        protected void regTsvFileSizeDataOutput(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTsvFileSizeDataOutput(), "TSV_FILE_SIZE_DATA_OUTPUT");
        }
        protected abstract ConditionValue getCValueTsvFileSizeDataOutput();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputRequestCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputRequestCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputRequestCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputRequestCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputRequestCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputRequestCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputRequestCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputRequestCB>(delegate(String function, SubQuery<TOutputRequestCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputRequestCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputRequestCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputRequestCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
