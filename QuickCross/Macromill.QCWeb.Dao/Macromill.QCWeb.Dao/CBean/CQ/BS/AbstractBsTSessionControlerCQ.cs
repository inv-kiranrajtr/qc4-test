
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
    public abstract class AbstractBsTSessionControlerCQ : AbstractConditionQuery {

        public AbstractBsTSessionControlerCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_SESSION_CONTROLER"; }
        public override String getTableSqlName() { return "T_SESSION_CONTROLER"; }

        public void SetSessionId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSessionId_Equal(fRES(v));
        }
        protected void DoSetSessionId_Equal(String v) { regSessionId(CK_EQ, v); }
        public void SetSessionId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSessionId_NotEqual(fRES(v));
        }
        protected void DoSetSessionId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_NES, v);
        }
        public void SetSessionId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_GT, fRES(v));
        }
        public void SetSessionId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_LT, fRES(v));
        }
        public void SetSessionId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_GE, fRES(v));
        }
        public void SetSessionId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_LE, fRES(v));
        }
        public void SetSessionId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSessionId(), "SESSION_ID");
        }
        public void SetSessionId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSessionId(), "SESSION_ID");
        }
        public void SetSessionId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSessionId_LikeSearch(v, cLSOP());
        }
        public void SetSessionId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSessionId(), "SESSION_ID", option);
        }
        public void SetSessionId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSessionId(), "SESSION_ID", option);
        }
        public void SetSessionId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSessionId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSessionId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSessionId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSessionId(), "SESSION_ID");
        }
        protected abstract ConditionValue getCValueSessionId();

        public void SetGuid_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGuid_Equal(fRES(v));
        }
        protected void DoSetGuid_Equal(String v) { regGuid(CK_EQ, v); }
        public void SetGuid_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGuid_NotEqual(fRES(v));
        }
        protected void DoSetGuid_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_NES, v);
        }
        public void SetGuid_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_GT, fRES(v));
        }
        public void SetGuid_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_LT, fRES(v));
        }
        public void SetGuid_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_GE, fRES(v));
        }
        public void SetGuid_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_LE, fRES(v));
        }
        public void SetGuid_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGuid(), "GUID");
        }
        public void SetGuid_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGuid(), "GUID");
        }
        public void SetGuid_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGuid_LikeSearch(v, cLSOP());
        }
        public void SetGuid_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGuid(), "GUID", option);
        }
        public void SetGuid_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGuid(), "GUID", option);
        }
        public void SetGuid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGuid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGuid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGuid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGuid(), "GUID");
        }
        protected abstract ConditionValue getCValueGuid();

        public void SetProcessServerCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetProcessServerCode_Equal(fRES(v));
        }
        protected void DoSetProcessServerCode_Equal(String v) { regProcessServerCode(CK_EQ, v); }
        public void SetProcessServerCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetProcessServerCode_NotEqual(fRES(v));
        }
        protected void DoSetProcessServerCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessServerCode(CK_NES, v);
        }
        public void SetProcessServerCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessServerCode(CK_GT, fRES(v));
        }
        public void SetProcessServerCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessServerCode(CK_LT, fRES(v));
        }
        public void SetProcessServerCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessServerCode(CK_GE, fRES(v));
        }
        public void SetProcessServerCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessServerCode(CK_LE, fRES(v));
        }
        public void SetProcessServerCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueProcessServerCode(), "PROCESS_SERVER_CODE");
        }
        public void SetProcessServerCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueProcessServerCode(), "PROCESS_SERVER_CODE");
        }
        public void SetProcessServerCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetProcessServerCode_LikeSearch(v, cLSOP());
        }
        public void SetProcessServerCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueProcessServerCode(), "PROCESS_SERVER_CODE", option);
        }
        public void SetProcessServerCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueProcessServerCode(), "PROCESS_SERVER_CODE", option);
        }
        protected void regProcessServerCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcessServerCode(), "PROCESS_SERVER_CODE");
        }
        protected abstract ConditionValue getCValueProcessServerCode();

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

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
