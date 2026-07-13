
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
    public abstract class AbstractBsTAllocationCellInfoCQ : AbstractConditionQuery {

        public AbstractBsTAllocationCellInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_ALLOCATION_CELL_INFO"; }
        public override String getTableSqlName() { return "T_ALLOCATION_CELL_INFO"; }

        public void SetAllocationCellId_Equal(long? v) { regAllocationCellId(CK_EQ, v); }
        public void SetAllocationCellId_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAllocationCellId(CK_NES, v);
        }
        public void SetAllocationCellId_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAllocationCellId(CK_GT, v);
        }
        public void SetAllocationCellId_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAllocationCellId(CK_LT, v);
        }
        public void SetAllocationCellId_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAllocationCellId(CK_GE, v);
        }
        public void SetAllocationCellId_LessEqual(long? v) {
            WhereSetterFlag = true;
            regAllocationCellId(CK_LE, v);
        }
        public void SetAllocationCellId_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueAllocationCellId(), "ALLOCATION_CELL_ID");
        }
        public void SetAllocationCellId_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueAllocationCellId(), "ALLOCATION_CELL_ID");
        }
        public void SetAllocationCellId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAllocationCellId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAllocationCellId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAllocationCellId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAllocationCellId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAllocationCellId(), "ALLOCATION_CELL_ID");
        }
        protected abstract ConditionValue getCValueAllocationCellId();

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

        public void SetCellNo_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCellNo_Equal(fRES(v));
        }
        protected void DoSetCellNo_Equal(String v) { regCellNo(CK_EQ, v); }
        public void SetCellNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCellNo_NotEqual(fRES(v));
        }
        protected void DoSetCellNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_NES, v);
        }
        public void SetCellNo_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_GT, fRES(v));
        }
        public void SetCellNo_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_LT, fRES(v));
        }
        public void SetCellNo_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_GE, fRES(v));
        }
        public void SetCellNo_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_LE, fRES(v));
        }
        public void SetCellNo_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCellNo(), "CELL_NO");
        }
        public void SetCellNo_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCellNo(), "CELL_NO");
        }
        public void SetCellNo_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCellNo_LikeSearch(v, cLSOP());
        }
        public void SetCellNo_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCellNo(), "CELL_NO", option);
        }
        public void SetCellNo_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCellNo(), "CELL_NO", option);
        }
        public void SetCellNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCellNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCellNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCellNo(), "CELL_NO");
        }
        protected abstract ConditionValue getCValueCellNo();

        public void SetCellName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCellName_Equal(fRES(v));
        }
        protected void DoSetCellName_Equal(String v) { regCellName(CK_EQ, v); }
        public void SetCellName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCellName_NotEqual(fRES(v));
        }
        protected void DoSetCellName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_NES, v);
        }
        public void SetCellName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_GT, fRES(v));
        }
        public void SetCellName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_LT, fRES(v));
        }
        public void SetCellName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_GE, fRES(v));
        }
        public void SetCellName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_LE, fRES(v));
        }
        public void SetCellName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCellName(), "CELL_NAME");
        }
        public void SetCellName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCellName(), "CELL_NAME");
        }
        public void SetCellName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCellName_LikeSearch(v, cLSOP());
        }
        public void SetCellName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCellName(), "CELL_NAME", option);
        }
        public void SetCellName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCellName(), "CELL_NAME", option);
        }
        public void SetCellName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCellName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCellName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCellName(), "CELL_NAME");
        }
        protected abstract ConditionValue getCValueCellName();

        public void SetExpectationSampleCount_Equal(decimal? v) { regExpectationSampleCount(CK_EQ, v); }
        public void SetExpectationSampleCount_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpectationSampleCount(CK_NES, v);
        }
        public void SetExpectationSampleCount_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpectationSampleCount(CK_GT, v);
        }
        public void SetExpectationSampleCount_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpectationSampleCount(CK_LT, v);
        }
        public void SetExpectationSampleCount_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpectationSampleCount(CK_GE, v);
        }
        public void SetExpectationSampleCount_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regExpectationSampleCount(CK_LE, v);
        }
        public void SetExpectationSampleCount_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueExpectationSampleCount(), "EXPECTATION_SAMPLE_COUNT");
        }
        public void SetExpectationSampleCount_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueExpectationSampleCount(), "EXPECTATION_SAMPLE_COUNT");
        }
        public void SetExpectationSampleCount_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpectationSampleCount(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExpectationSampleCount_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExpectationSampleCount(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExpectationSampleCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExpectationSampleCount(), "EXPECTATION_SAMPLE_COUNT");
        }
        protected abstract ConditionValue getCValueExpectationSampleCount();

        public void SetValidSampleCount_Equal(decimal? v) { regValidSampleCount(CK_EQ, v); }
        public void SetValidSampleCount_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regValidSampleCount(CK_NES, v);
        }
        public void SetValidSampleCount_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regValidSampleCount(CK_GT, v);
        }
        public void SetValidSampleCount_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regValidSampleCount(CK_LT, v);
        }
        public void SetValidSampleCount_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regValidSampleCount(CK_GE, v);
        }
        public void SetValidSampleCount_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regValidSampleCount(CK_LE, v);
        }
        public void SetValidSampleCount_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueValidSampleCount(), "VALID_SAMPLE_COUNT");
        }
        public void SetValidSampleCount_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueValidSampleCount(), "VALID_SAMPLE_COUNT");
        }
        public void SetValidSampleCount_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regValidSampleCount(CK_ISN, DUMMY_OBJECT);
        }
        public void SetValidSampleCount_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regValidSampleCount(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regValidSampleCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueValidSampleCount(), "VALID_SAMPLE_COUNT");
        }
        protected abstract ConditionValue getCValueValidSampleCount();

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
