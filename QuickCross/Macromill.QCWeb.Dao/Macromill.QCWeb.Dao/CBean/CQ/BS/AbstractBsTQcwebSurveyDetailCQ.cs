
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
    public abstract class AbstractBsTQcwebSurveyDetailCQ : AbstractConditionQuery {

        public AbstractBsTQcwebSurveyDetailCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_QCWEB_SURVEY_DETAIL"; }
        public override String getTableSqlName() { return "T_QCWEB_SURVEY_DETAIL"; }

        public void SetQcwebDetailId_Equal(decimal? v) { regQcwebDetailId(CK_EQ, v); }
        public void SetQcwebDetailId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebDetailId(CK_NES, v);
        }
        public void SetQcwebDetailId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebDetailId(CK_GT, v);
        }
        public void SetQcwebDetailId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebDetailId(CK_LT, v);
        }
        public void SetQcwebDetailId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebDetailId(CK_GE, v);
        }
        public void SetQcwebDetailId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regQcwebDetailId(CK_LE, v);
        }
        public void SetQcwebDetailId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueQcwebDetailId(), "QCWEB_DETAIL_ID");
        }
        public void SetQcwebDetailId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueQcwebDetailId(), "QCWEB_DETAIL_ID");
        }
        public void SetQcwebDetailId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebDetailId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQcwebDetailId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebDetailId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQcwebDetailId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebDetailId(), "QCWEB_DETAIL_ID");
        }
        protected abstract ConditionValue getCValueQcwebDetailId();

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

        public void SetSurveyNo_Equal(int? v) { regSurveyNo(CK_EQ, v); }
        public void SetSurveyNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNo(CK_NES, v);
        }
        public void SetSurveyNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNo(CK_GT, v);
        }
        public void SetSurveyNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNo(CK_LT, v);
        }
        public void SetSurveyNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNo(CK_GE, v);
        }
        public void SetSurveyNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSurveyNo(CK_LE, v);
        }
        public void SetSurveyNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSurveyNo(), "SURVEY_NO");
        }
        public void SetSurveyNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSurveyNo(), "SURVEY_NO");
        }
        public void SetSurveyNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSurveyNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSurveyNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyNo(), "SURVEY_NO");
        }
        protected abstract ConditionValue getCValueSurveyNo();

        public void SetSurveyName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyName_Equal(fRES(v));
        }
        protected void DoSetSurveyName_Equal(String v) { regSurveyName(CK_EQ, v); }
        public void SetSurveyName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyName_NotEqual(fRES(v));
        }
        protected void DoSetSurveyName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_NES, v);
        }
        public void SetSurveyName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_GT, fRES(v));
        }
        public void SetSurveyName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_LT, fRES(v));
        }
        public void SetSurveyName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_GE, fRES(v));
        }
        public void SetSurveyName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_LE, fRES(v));
        }
        public void SetSurveyName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSurveyName(), "SURVEY_NAME");
        }
        public void SetSurveyName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSurveyName(), "SURVEY_NAME");
        }
        public void SetSurveyName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSurveyName_LikeSearch(v, cLSOP());
        }
        public void SetSurveyName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSurveyName(), "SURVEY_NAME", option);
        }
        public void SetSurveyName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSurveyName(), "SURVEY_NAME", option);
        }
        public void SetSurveyName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSurveyName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSurveyName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyName(), "SURVEY_NAME");
        }
        protected abstract ConditionValue getCValueSurveyName();

        public void SetQc3uniqueId_Equal(decimal? v) { regQc3uniqueId(CK_EQ, v); }
        public void SetQc3uniqueId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQc3uniqueId(CK_NES, v);
        }
        public void SetQc3uniqueId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQc3uniqueId(CK_GT, v);
        }
        public void SetQc3uniqueId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQc3uniqueId(CK_LT, v);
        }
        public void SetQc3uniqueId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQc3uniqueId(CK_GE, v);
        }
        public void SetQc3uniqueId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regQc3uniqueId(CK_LE, v);
        }
        public void SetQc3uniqueId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueQc3uniqueId(), "QC3UNIQUE_ID");
        }
        public void SetQc3uniqueId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueQc3uniqueId(), "QC3UNIQUE_ID");
        }
        public void SetQc3uniqueId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQc3uniqueId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQc3uniqueId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQc3uniqueId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQc3uniqueId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQc3uniqueId(), "QC3UNIQUE_ID");
        }
        protected abstract ConditionValue getCValueQc3uniqueId();

        public void SetSurveyMethod_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyMethod_Equal(fRES(v));
        }
        protected void DoSetSurveyMethod_Equal(String v) { regSurveyMethod(CK_EQ, v); }
        public void SetSurveyMethod_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyMethod_NotEqual(fRES(v));
        }
        protected void DoSetSurveyMethod_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_NES, v);
        }
        public void SetSurveyMethod_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_GT, fRES(v));
        }
        public void SetSurveyMethod_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_LT, fRES(v));
        }
        public void SetSurveyMethod_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_GE, fRES(v));
        }
        public void SetSurveyMethod_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_LE, fRES(v));
        }
        public void SetSurveyMethod_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSurveyMethod(), "SURVEY_METHOD");
        }
        public void SetSurveyMethod_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSurveyMethod(), "SURVEY_METHOD");
        }
        public void SetSurveyMethod_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSurveyMethod_LikeSearch(v, cLSOP());
        }
        public void SetSurveyMethod_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSurveyMethod(), "SURVEY_METHOD", option);
        }
        public void SetSurveyMethod_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSurveyMethod(), "SURVEY_METHOD", option);
        }
        public void SetSurveyMethod_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSurveyMethod_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyMethod(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSurveyMethod(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyMethod(), "SURVEY_METHOD");
        }
        protected abstract ConditionValue getCValueSurveyMethod();

        public void SetServiceType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetServiceType_Equal(fRES(v));
        }
        protected void DoSetServiceType_Equal(String v) { regServiceType(CK_EQ, v); }
        public void SetServiceType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetServiceType_NotEqual(fRES(v));
        }
        protected void DoSetServiceType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_NES, v);
        }
        public void SetServiceType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_GT, fRES(v));
        }
        public void SetServiceType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_LT, fRES(v));
        }
        public void SetServiceType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_GE, fRES(v));
        }
        public void SetServiceType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_LE, fRES(v));
        }
        public void SetServiceType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueServiceType(), "SERVICE_TYPE");
        }
        public void SetServiceType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueServiceType(), "SERVICE_TYPE");
        }
        public void SetServiceType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetServiceType_LikeSearch(v, cLSOP());
        }
        public void SetServiceType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueServiceType(), "SERVICE_TYPE", option);
        }
        public void SetServiceType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueServiceType(), "SERVICE_TYPE", option);
        }
        public void SetServiceType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetServiceType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regServiceType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regServiceType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueServiceType(), "SERVICE_TYPE");
        }
        protected abstract ConditionValue getCValueServiceType();

        public void SetSurveyDate_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDate_Equal(fRES(v));
        }
        protected void DoSetSurveyDate_Equal(String v) { regSurveyDate(CK_EQ, v); }
        public void SetSurveyDate_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSurveyDate_NotEqual(fRES(v));
        }
        protected void DoSetSurveyDate_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_NES, v);
        }
        public void SetSurveyDate_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_GT, fRES(v));
        }
        public void SetSurveyDate_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_LT, fRES(v));
        }
        public void SetSurveyDate_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_GE, fRES(v));
        }
        public void SetSurveyDate_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_LE, fRES(v));
        }
        public void SetSurveyDate_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSurveyDate(), "SURVEY_DATE");
        }
        public void SetSurveyDate_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSurveyDate(), "SURVEY_DATE");
        }
        public void SetSurveyDate_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSurveyDate_LikeSearch(v, cLSOP());
        }
        public void SetSurveyDate_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSurveyDate(), "SURVEY_DATE", option);
        }
        public void SetSurveyDate_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSurveyDate(), "SURVEY_DATE", option);
        }
        public void SetSurveyDate_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSurveyDate_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyDate(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSurveyDate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyDate(), "SURVEY_DATE");
        }
        protected abstract ConditionValue getCValueSurveyDate();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TQcwebSurveyDetailCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TQcwebSurveyDetailCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TQcwebSurveyDetailCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TQcwebSurveyDetailCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TQcwebSurveyDetailCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TQcwebSurveyDetailCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TQcwebSurveyDetailCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TQcwebSurveyDetailCB>(delegate(String function, SubQuery<TQcwebSurveyDetailCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TQcwebSurveyDetailCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TQcwebSurveyDetailCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TQcwebSurveyDetailCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyDetailCB>", subQuery);
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEB_DETAIL_ID", "QCWEB_DETAIL_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TQcwebSurveyDetailCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
