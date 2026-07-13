
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
    public abstract class AbstractBsTDefaultEnvColorDtlCCQ : AbstractConditionQuery {

        public AbstractBsTDefaultEnvColorDtlCCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_DTL_C"; }
        public override String getTableSqlName() { return "T_DEFAULT_ENV_COLOR_DTL_C"; }

        public void SetDefEnvColorDtlCId_Equal(int? v) { regDefEnvColorDtlCId(CK_EQ, v); }
        public void SetDefEnvColorDtlCId_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlCId(CK_NES, v);
        }
        public void SetDefEnvColorDtlCId_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlCId(CK_GT, v);
        }
        public void SetDefEnvColorDtlCId_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlCId(CK_LT, v);
        }
        public void SetDefEnvColorDtlCId_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlCId(CK_GE, v);
        }
        public void SetDefEnvColorDtlCId_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDefEnvColorDtlCId(CK_LE, v);
        }
        public void SetDefEnvColorDtlCId_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDefEnvColorDtlCId(), "DEF_ENV_COLOR_DTL_C_ID");
        }
        public void SetDefEnvColorDtlCId_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDefEnvColorDtlCId(), "DEF_ENV_COLOR_DTL_C_ID");
        }
        public void SetDefEnvColorDtlCId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlCId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDefEnvColorDtlCId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlCId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDefEnvColorDtlCId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDefEnvColorDtlCId(), "DEF_ENV_COLOR_DTL_C_ID");
        }
        protected abstract ConditionValue getCValueDefEnvColorDtlCId();

        public void SetDefEnvColorInfoCId_Equal(int? v) { regDefEnvColorInfoCId(CK_EQ, v); }
        public void SetDefEnvColorInfoCId_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoCId(CK_NES, v);
        }
        public void SetDefEnvColorInfoCId_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoCId(CK_GT, v);
        }
        public void SetDefEnvColorInfoCId_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoCId(CK_LT, v);
        }
        public void SetDefEnvColorInfoCId_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoCId(CK_GE, v);
        }
        public void SetDefEnvColorInfoCId_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDefEnvColorInfoCId(CK_LE, v);
        }
        public void SetDefEnvColorInfoCId_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDefEnvColorInfoCId(), "DEF_ENV_COLOR_INFO_C_ID");
        }
        public void SetDefEnvColorInfoCId_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDefEnvColorInfoCId(), "DEF_ENV_COLOR_INFO_C_ID");
        }
        public void InScopeTDefaultEnvColorInfoC(SubQuery<TDefaultEnvColorInfoCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCCB>", subQuery);
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoC(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorInfoC(TDefaultEnvColorInfoCCQ subQuery);
        public void NotInScopeTDefaultEnvColorInfoC(SubQuery<TDefaultEnvColorInfoCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCCB>", subQuery);
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoC(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorInfoC(TDefaultEnvColorInfoCCQ subQuery);
        protected void regDefEnvColorInfoCId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDefEnvColorInfoCId(), "DEF_ENV_COLOR_INFO_C_ID");
        }
        protected abstract ConditionValue getCValueDefEnvColorInfoCId();

        public void SetGraphColorNo_Equal(int? v) { regGraphColorNo(CK_EQ, v); }
        public void SetGraphColorNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphColorNo(CK_NES, v);
        }
        public void SetGraphColorNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphColorNo(CK_GT, v);
        }
        public void SetGraphColorNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphColorNo(CK_LT, v);
        }
        public void SetGraphColorNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphColorNo(CK_GE, v);
        }
        public void SetGraphColorNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regGraphColorNo(CK_LE, v);
        }
        public void SetGraphColorNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueGraphColorNo(), "GRAPH_COLOR_NO");
        }
        public void SetGraphColorNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueGraphColorNo(), "GRAPH_COLOR_NO");
        }
        protected void regGraphColorNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphColorNo(), "GRAPH_COLOR_NO");
        }
        protected abstract ConditionValue getCValueGraphColorNo();

        public void SetColorCode_Equal(int? v) { regColorCode(CK_EQ, v); }
        public void SetColorCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorCode(CK_NES, v);
        }
        public void SetColorCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorCode(CK_GT, v);
        }
        public void SetColorCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorCode(CK_LT, v);
        }
        public void SetColorCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorCode(CK_GE, v);
        }
        public void SetColorCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regColorCode(CK_LE, v);
        }
        public void SetColorCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueColorCode(), "COLOR_CODE");
        }
        public void SetColorCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueColorCode(), "COLOR_CODE");
        }
        protected void regColorCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorCode(), "COLOR_CODE");
        }
        protected abstract ConditionValue getCValueColorCode();

        public void SetPatternCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPatternCode_Equal(fRES(v));
        }
        protected void DoSetPatternCode_Equal(String v) { regPatternCode(CK_EQ, v); }
        public void SetPatternCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPatternCode_NotEqual(fRES(v));
        }
        protected void DoSetPatternCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_NES, v);
        }
        public void SetPatternCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_GT, fRES(v));
        }
        public void SetPatternCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_LT, fRES(v));
        }
        public void SetPatternCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_GE, fRES(v));
        }
        public void SetPatternCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_LE, fRES(v));
        }
        public void SetPatternCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValuePatternCode(), "PATTERN_CODE");
        }
        public void SetPatternCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValuePatternCode(), "PATTERN_CODE");
        }
        public void SetPatternCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetPatternCode_LikeSearch(v, cLSOP());
        }
        public void SetPatternCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValuePatternCode(), "PATTERN_CODE", option);
        }
        public void SetPatternCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValuePatternCode(), "PATTERN_CODE", option);
        }
        public void SetPatternCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPatternCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPatternCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPatternCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePatternCode(), "PATTERN_CODE");
        }
        protected abstract ConditionValue getCValuePatternCode();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDefaultEnvColorDtlCCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDefaultEnvColorDtlCCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDefaultEnvColorDtlCCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDefaultEnvColorDtlCCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDefaultEnvColorDtlCCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDefaultEnvColorDtlCCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDefaultEnvColorDtlCCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDefaultEnvColorDtlCCB>(delegate(String function, SubQuery<TDefaultEnvColorDtlCCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDefaultEnvColorDtlCCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDefaultEnvColorDtlCCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDefaultEnvColorDtlCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_DTL_C_ID", "DEF_ENV_COLOR_DTL_C_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDefaultEnvColorDtlCCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
