
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
    public abstract class AbstractBsTDefaultEnvColorDtlCQ : AbstractConditionQuery {

        public AbstractBsTDefaultEnvColorDtlCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_DTL"; }
        public override String getTableSqlName() { return "T_DEFAULT_ENV_COLOR_DTL"; }

        public void SetDefEnvColorDtlId_Equal(decimal? v) { regDefEnvColorDtlId(CK_EQ, v); }
        public void SetDefEnvColorDtlId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlId(CK_NES, v);
        }
        public void SetDefEnvColorDtlId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlId(CK_GT, v);
        }
        public void SetDefEnvColorDtlId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlId(CK_LT, v);
        }
        public void SetDefEnvColorDtlId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlId(CK_GE, v);
        }
        public void SetDefEnvColorDtlId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDefEnvColorDtlId(CK_LE, v);
        }
        public void SetDefEnvColorDtlId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDefEnvColorDtlId(), "DEF_ENV_COLOR_DTL_ID");
        }
        public void SetDefEnvColorDtlId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDefEnvColorDtlId(), "DEF_ENV_COLOR_DTL_ID");
        }
        public void SetDefEnvColorDtlId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDefEnvColorDtlId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorDtlId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDefEnvColorDtlId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDefEnvColorDtlId(), "DEF_ENV_COLOR_DTL_ID");
        }
        protected abstract ConditionValue getCValueDefEnvColorDtlId();

        public void SetDefEnvColorInfoId_Equal(decimal? v) { regDefEnvColorInfoId(CK_EQ, v); }
        public void SetDefEnvColorInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoId(CK_NES, v);
        }
        public void SetDefEnvColorInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoId(CK_GT, v);
        }
        public void SetDefEnvColorInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoId(CK_LT, v);
        }
        public void SetDefEnvColorInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoId(CK_GE, v);
        }
        public void SetDefEnvColorInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDefEnvColorInfoId(CK_LE, v);
        }
        public void SetDefEnvColorInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDefEnvColorInfoId(), "DEF_ENV_COLOR_INFO_ID");
        }
        public void SetDefEnvColorInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDefEnvColorInfoId(), "DEF_ENV_COLOR_INFO_ID");
        }
        public void InScopeTDefaultEnvColorInfo(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorInfo(TDefaultEnvColorInfoCQ subQuery);
        public void NotInScopeTDefaultEnvColorInfo(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorInfo(TDefaultEnvColorInfoCQ subQuery);
        protected void regDefEnvColorInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDefEnvColorInfoId(), "DEF_ENV_COLOR_INFO_ID");
        }
        protected abstract ConditionValue getCValueDefEnvColorInfoId();

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
        public SSQFunction<TDefaultEnvColorDtlCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDefaultEnvColorDtlCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDefaultEnvColorDtlCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDefaultEnvColorDtlCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDefaultEnvColorDtlCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDefaultEnvColorDtlCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDefaultEnvColorDtlCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDefaultEnvColorDtlCB>(delegate(String function, SubQuery<TDefaultEnvColorDtlCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDefaultEnvColorDtlCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDefaultEnvColorDtlCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_DTL_ID", "DEF_ENV_COLOR_DTL_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDefaultEnvColorDtlCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
