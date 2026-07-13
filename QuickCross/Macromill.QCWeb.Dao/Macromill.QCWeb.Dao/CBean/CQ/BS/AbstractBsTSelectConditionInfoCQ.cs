
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
    public abstract class AbstractBsTSelectConditionInfoCQ : AbstractConditionQuery {

        public AbstractBsTSelectConditionInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_SELECT_CONDITION_INFO"; }
        public override String getTableSqlName() { return "T_SELECT_CONDITION_INFO"; }

        public void SetSelectNo_Equal(long? v) { regSelectNo(CK_EQ, v); }
        public void SetSelectNo_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectNo(CK_NES, v);
        }
        public void SetSelectNo_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectNo(CK_GT, v);
        }
        public void SetSelectNo_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectNo(CK_LT, v);
        }
        public void SetSelectNo_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectNo(CK_GE, v);
        }
        public void SetSelectNo_LessEqual(long? v) {
            WhereSetterFlag = true;
            regSelectNo(CK_LE, v);
        }
        public void SetSelectNo_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueSelectNo(), "SELECT_NO");
        }
        public void SetSelectNo_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueSelectNo(), "SELECT_NO");
        }
        public void SetSelectNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSelectNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSelectNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSelectNo(), "SELECT_NO");
        }
        protected abstract ConditionValue getCValueSelectNo();

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
        public void ExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
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

        public void SetQuestionNo_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQuestionNo_Equal(fRES(v));
        }
        protected void DoSetQuestionNo_Equal(String v) { regQuestionNo(CK_EQ, v); }
        public void SetQuestionNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQuestionNo_NotEqual(fRES(v));
        }
        protected void DoSetQuestionNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_NES, v);
        }
        public void SetQuestionNo_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_GT, fRES(v));
        }
        public void SetQuestionNo_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_LT, fRES(v));
        }
        public void SetQuestionNo_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_GE, fRES(v));
        }
        public void SetQuestionNo_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_LE, fRES(v));
        }
        public void SetQuestionNo_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQuestionNo(), "QUESTION_NO");
        }
        public void SetQuestionNo_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQuestionNo(), "QUESTION_NO");
        }
        public void SetQuestionNo_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQuestionNo_LikeSearch(v, cLSOP());
        }
        public void SetQuestionNo_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQuestionNo(), "QUESTION_NO", option);
        }
        public void SetQuestionNo_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQuestionNo(), "QUESTION_NO", option);
        }
        public void SetQuestionNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQuestionNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQuestionNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQuestionNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQuestionNo(), "QUESTION_NO");
        }
        protected abstract ConditionValue getCValueQuestionNo();

        public void SetChildQuestionNo_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetChildQuestionNo_Equal(fRES(v));
        }
        protected void DoSetChildQuestionNo_Equal(String v) { regChildQuestionNo(CK_EQ, v); }
        public void SetChildQuestionNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetChildQuestionNo_NotEqual(fRES(v));
        }
        protected void DoSetChildQuestionNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_NES, v);
        }
        public void SetChildQuestionNo_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_GT, fRES(v));
        }
        public void SetChildQuestionNo_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_LT, fRES(v));
        }
        public void SetChildQuestionNo_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_GE, fRES(v));
        }
        public void SetChildQuestionNo_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_LE, fRES(v));
        }
        public void SetChildQuestionNo_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueChildQuestionNo(), "CHILD_QUESTION_NO");
        }
        public void SetChildQuestionNo_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueChildQuestionNo(), "CHILD_QUESTION_NO");
        }
        public void SetChildQuestionNo_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetChildQuestionNo_LikeSearch(v, cLSOP());
        }
        public void SetChildQuestionNo_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueChildQuestionNo(), "CHILD_QUESTION_NO", option);
        }
        public void SetChildQuestionNo_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueChildQuestionNo(), "CHILD_QUESTION_NO", option);
        }
        public void SetChildQuestionNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetChildQuestionNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChildQuestionNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regChildQuestionNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChildQuestionNo(), "CHILD_QUESTION_NO");
        }
        protected abstract ConditionValue getCValueChildQuestionNo();

        public void SetSelectCondition_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSelectCondition_Equal(fRES(v));
        }
        protected void DoSetSelectCondition_Equal(String v) { regSelectCondition(CK_EQ, v); }
        public void SetSelectCondition_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSelectCondition_NotEqual(fRES(v));
        }
        protected void DoSetSelectCondition_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_NES, v);
        }
        public void SetSelectCondition_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_GT, fRES(v));
        }
        public void SetSelectCondition_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_LT, fRES(v));
        }
        public void SetSelectCondition_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_GE, fRES(v));
        }
        public void SetSelectCondition_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_LE, fRES(v));
        }
        public void SetSelectCondition_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSelectCondition(), "SELECT_CONDITION");
        }
        public void SetSelectCondition_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSelectCondition(), "SELECT_CONDITION");
        }
        public void SetSelectCondition_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSelectCondition_LikeSearch(v, cLSOP());
        }
        public void SetSelectCondition_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSelectCondition(), "SELECT_CONDITION", option);
        }
        public void SetSelectCondition_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSelectCondition(), "SELECT_CONDITION", option);
        }
        public void SetSelectCondition_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSelectCondition_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectCondition(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSelectCondition(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSelectCondition(), "SELECT_CONDITION");
        }
        protected abstract ConditionValue getCValueSelectCondition();

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
