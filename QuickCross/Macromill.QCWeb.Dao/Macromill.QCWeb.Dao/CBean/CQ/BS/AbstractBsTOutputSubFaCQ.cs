
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
    public abstract class AbstractBsTOutputSubFaCQ : AbstractConditionQuery {

        public AbstractBsTOutputSubFaCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SUB_FA"; }
        public override String getTableSqlName() { return "T_OUTPUT_SUB_FA"; }

        public void SetOutputSubFaId_Equal(decimal? v) { regOutputSubFaId(CK_EQ, v); }
        public void SetOutputSubFaId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubFaId(CK_NES, v);
        }
        public void SetOutputSubFaId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubFaId(CK_GT, v);
        }
        public void SetOutputSubFaId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubFaId(CK_LT, v);
        }
        public void SetOutputSubFaId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubFaId(CK_GE, v);
        }
        public void SetOutputSubFaId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputSubFaId(CK_LE, v);
        }
        public void SetOutputSubFaId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputSubFaId(), "OUTPUT_SUB_FA_ID");
        }
        public void SetOutputSubFaId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputSubFaId(), "OUTPUT_SUB_FA_ID");
        }
        public void SetOutputSubFaId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubFaId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputSubFaId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubFaId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputSubFaId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputSubFaId(), "OUTPUT_SUB_FA_ID");
        }
        protected abstract ConditionValue getCValueOutputSubFaId();

        public void SetOutputCommonId_Equal(decimal? v) { regOutputCommonId(CK_EQ, v); }
        public void SetOutputCommonId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_NES, v);
        }
        public void SetOutputCommonId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GT, v);
        }
        public void SetOutputCommonId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_LT, v);
        }
        public void SetOutputCommonId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GE, v);
        }
        public void SetOutputCommonId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputCommonId(CK_LE, v);
        }
        public void SetOutputCommonId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void SetOutputCommonId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void InScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputCommon(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        public void NotInScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        protected void regOutputCommonId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        protected abstract ConditionValue getCValueOutputCommonId();

        public void SetPageSettingPaperSize_Equal(int? v) { regPageSettingPaperSize(CK_EQ, v); }
        public void SetPageSettingPaperSize_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_NES, v);
        }
        public void SetPageSettingPaperSize_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_GT, v);
        }
        public void SetPageSettingPaperSize_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_LT, v);
        }
        public void SetPageSettingPaperSize_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_GE, v);
        }
        public void SetPageSettingPaperSize_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPageSettingPaperSize(CK_LE, v);
        }
        public void SetPageSettingPaperSize_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingPaperSize(), "PAGE_SETTING_PAPER_SIZE");
        }
        public void SetPageSettingPaperSize_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingPaperSize(), "PAGE_SETTING_PAPER_SIZE");
        }
        public void SetPageSettingPaperSize_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPageSettingPaperSize_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPageSettingPaperSize(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingPaperSize(), "PAGE_SETTING_PAPER_SIZE");
        }
        protected abstract ConditionValue getCValuePageSettingPaperSize();

        public void SetPageSettingPaperOrientation_Equal(int? v) { regPageSettingPaperOrientation(CK_EQ, v); }
        public void SetPageSettingPaperOrientation_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_NES, v);
        }
        public void SetPageSettingPaperOrientation_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_GT, v);
        }
        public void SetPageSettingPaperOrientation_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_LT, v);
        }
        public void SetPageSettingPaperOrientation_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_GE, v);
        }
        public void SetPageSettingPaperOrientation_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPageSettingPaperOrientation(CK_LE, v);
        }
        public void SetPageSettingPaperOrientation_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingPaperOrientation(), "PAGE_SETTING_PAPER_ORIENTATION");
        }
        public void SetPageSettingPaperOrientation_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingPaperOrientation(), "PAGE_SETTING_PAPER_ORIENTATION");
        }
        public void SetPageSettingPaperOrientation_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPageSettingPaperOrientation_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPageSettingPaperOrientation(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingPaperOrientation(), "PAGE_SETTING_PAPER_ORIENTATION");
        }
        protected abstract ConditionValue getCValuePageSettingPaperOrientation();

        public void SetFilteringExpression_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFilteringExpression_Equal(fRES(v));
        }
        protected void DoSetFilteringExpression_Equal(String v) { regFilteringExpression(CK_EQ, v); }
        public void SetFilteringExpression_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFilteringExpression_NotEqual(fRES(v));
        }
        protected void DoSetFilteringExpression_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_NES, v);
        }
        public void SetFilteringExpression_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_GT, fRES(v));
        }
        public void SetFilteringExpression_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_LT, fRES(v));
        }
        public void SetFilteringExpression_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_GE, fRES(v));
        }
        public void SetFilteringExpression_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_LE, fRES(v));
        }
        public void SetFilteringExpression_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFilteringExpression(), "FILTERING_EXPRESSION");
        }
        public void SetFilteringExpression_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFilteringExpression(), "FILTERING_EXPRESSION");
        }
        public void SetFilteringExpression_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFilteringExpression_LikeSearch(v, cLSOP());
        }
        public void SetFilteringExpression_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFilteringExpression(), "FILTERING_EXPRESSION", option);
        }
        public void SetFilteringExpression_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFilteringExpression(), "FILTERING_EXPRESSION", option);
        }
        public void SetFilteringExpression_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFilteringExpression_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFilteringExpression(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFilteringExpression(), "FILTERING_EXPRESSION");
        }
        protected abstract ConditionValue getCValueFilteringExpression();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSubFaCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSubFaCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSubFaCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSubFaCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSubFaCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSubFaCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSubFaCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSubFaCB>(delegate(String function, SubQuery<TOutputSubFaCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSubFaCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSubFaCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSubFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_SUB_FA_ID", "OUTPUT_SUB_FA_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSubFaCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
