
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
    public abstract class AbstractBsTReportsetCQ : AbstractConditionQuery {

        public AbstractBsTReportsetCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_REPORTSET"; }
        public override String getTableSqlName() { return "T_REPORTSET"; }

        public void SetReportsetId_Equal(decimal? v) { regReportsetId(CK_EQ, v); }
        public void SetReportsetId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_NES, v);
        }
        public void SetReportsetId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_GT, v);
        }
        public void SetReportsetId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_LT, v);
        }
        public void SetReportsetId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_GE, v);
        }
        public void SetReportsetId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regReportsetId(CK_LE, v);
        }
        public void SetReportsetId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueReportsetId(), "REPORTSET_ID");
        }
        public void SetReportsetId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueReportsetId(), "REPORTSET_ID");
        }
        public void ExistsTReportList(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_ExistsSubQuery_TReportList(cb.Query());
            registerExistsSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_ExistsSubQuery_TReportList(TReportCQ subQuery);
        public void NotExistsTReportList(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_NotExistsSubQuery_TReportList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_NotExistsSubQuery_TReportList(TReportCQ subQuery);
        public void InScopeTReport(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_InScopeSubQuery_TReport(cb.Query());
            registerInScopeSubQuery(cb.Query(), "REPORTSET_ID", "Reportset_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_InScopeSubQuery_TReport(TReportCQ subQuery);
        public void InScopeTReportList(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_InScopeSubQuery_TReportList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_InScopeSubQuery_TReportList(TReportCQ subQuery);
        public void NotInScopeTReport(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_NotInScopeSubQuery_TReport(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "REPORTSET_ID", "Reportset_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_NotInScopeSubQuery_TReport(TReportCQ subQuery);
        public void NotInScopeTReportList(SubQuery<TReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_NotInScopeSubQuery_TReportList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepReportsetId_NotInScopeSubQuery_TReportList(TReportCQ subQuery);
        public void xsderiveTReportList(String function, SubQuery<TReportCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_SpecifyDerivedReferrer_TReportList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepReportsetId_SpecifyDerivedReferrer_TReportList(TReportCQ subQuery);

        public QDRFunction<TReportCB> DerivedTReportList() {
            return xcreateQDRFunctionTReportList();
        }
        protected QDRFunction<TReportCB> xcreateQDRFunctionTReportList() {
            return new QDRFunction<TReportCB>(delegate(String function, SubQuery<TReportCB> subQuery, String operand, Object value) {
                xqderiveTReportList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTReportList(String function, SubQuery<TReportCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TReportCB>", subQuery);
            TReportCB cb = new TReportCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepReportsetId_QueryDerivedReferrer_TReportList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepReportsetId_QueryDerivedReferrer_TReportListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepReportsetId_QueryDerivedReferrer_TReportList(TReportCQ subQuery);
        public abstract String keepReportsetId_QueryDerivedReferrer_TReportListParameter(Object parameterValue);
        public void SetReportsetId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetReportsetId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regReportsetId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportsetId(), "REPORTSET_ID");
        }
        protected abstract ConditionValue getCValueReportsetId();

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

        public void SetReportsetName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetReportsetName_Equal(fRES(v));
        }
        protected void DoSetReportsetName_Equal(String v) { regReportsetName(CK_EQ, v); }
        public void SetReportsetName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetReportsetName_NotEqual(fRES(v));
        }
        protected void DoSetReportsetName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_NES, v);
        }
        public void SetReportsetName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_GT, fRES(v));
        }
        public void SetReportsetName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_LT, fRES(v));
        }
        public void SetReportsetName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_GE, fRES(v));
        }
        public void SetReportsetName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_LE, fRES(v));
        }
        public void SetReportsetName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueReportsetName(), "REPORTSET_NAME");
        }
        public void SetReportsetName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueReportsetName(), "REPORTSET_NAME");
        }
        public void SetReportsetName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetReportsetName_LikeSearch(v, cLSOP());
        }
        public void SetReportsetName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueReportsetName(), "REPORTSET_NAME", option);
        }
        public void SetReportsetName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueReportsetName(), "REPORTSET_NAME", option);
        }
        public void SetReportsetName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetReportsetName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportsetName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regReportsetName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportsetName(), "REPORTSET_NAME");
        }
        protected abstract ConditionValue getCValueReportsetName();

        public void SetSortNo_Equal(int? v) { regSortNo(CK_EQ, v); }
        public void SetSortNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_NES, v);
        }
        public void SetSortNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GT, v);
        }
        public void SetSortNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_LT, v);
        }
        public void SetSortNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GE, v);
        }
        public void SetSortNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortNo(CK_LE, v);
        }
        public void SetSortNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        public void SetSortNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        protected void regSortNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortNo(), "SORT_NO");
        }
        protected abstract ConditionValue getCValueSortNo();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TReportsetCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TReportsetCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TReportsetCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TReportsetCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TReportsetCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TReportsetCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TReportsetCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TReportsetCB>(delegate(String function, SubQuery<TReportsetCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TReportsetCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TReportsetCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TReportsetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TReportsetCB>", subQuery);
            TReportsetCB cb = new TReportsetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "REPORTSET_ID", "REPORTSET_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TReportsetCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
