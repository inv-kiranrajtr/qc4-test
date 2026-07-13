
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
    public abstract class AbstractBsTColorInfoDetailGtCQ : AbstractConditionQuery {

        public AbstractBsTColorInfoDetailGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_COLOR_INFO_DETAIL_GT"; }
        public override String getTableSqlName() { return "T_COLOR_INFO_DETAIL_GT"; }

        public void SetColorInfoDetailGtId_Equal(decimal? v) { regColorInfoDetailGtId(CK_EQ, v); }
        public void SetColorInfoDetailGtId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailGtId(CK_NES, v);
        }
        public void SetColorInfoDetailGtId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailGtId(CK_GT, v);
        }
        public void SetColorInfoDetailGtId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailGtId(CK_LT, v);
        }
        public void SetColorInfoDetailGtId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailGtId(CK_GE, v);
        }
        public void SetColorInfoDetailGtId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regColorInfoDetailGtId(CK_LE, v);
        }
        public void SetColorInfoDetailGtId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueColorInfoDetailGtId(), "COLOR_INFO_DETAIL_GT_ID");
        }
        public void SetColorInfoDetailGtId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueColorInfoDetailGtId(), "COLOR_INFO_DETAIL_GT_ID");
        }
        public void SetColorInfoDetailGtId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailGtId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColorInfoDetailGtId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailGtId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColorInfoDetailGtId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorInfoDetailGtId(), "COLOR_INFO_DETAIL_GT_ID");
        }
        protected abstract ConditionValue getCValueColorInfoDetailGtId();

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

        public void SetColorSetInfoGtId_Equal(decimal? v) { regColorSetInfoGtId(CK_EQ, v); }
        public void SetColorSetInfoGtId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoGtId(CK_NES, v);
        }
        public void SetColorSetInfoGtId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoGtId(CK_GT, v);
        }
        public void SetColorSetInfoGtId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoGtId(CK_LT, v);
        }
        public void SetColorSetInfoGtId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoGtId(CK_GE, v);
        }
        public void SetColorSetInfoGtId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regColorSetInfoGtId(CK_LE, v);
        }
        public void SetColorSetInfoGtId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueColorSetInfoGtId(), "COLOR_SET_INFO_GT_ID");
        }
        public void SetColorSetInfoGtId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueColorSetInfoGtId(), "COLOR_SET_INFO_GT_ID");
        }
        public void InScopeTColorSetInfoGt(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt(cb.Query());
            registerInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoGtId_InScopeSubQuery_TColorSetInfoGt(TColorSetInfoGtCQ subQuery);
        public void NotInScopeTColorSetInfoGt(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoGtId_NotInScopeSubQuery_TColorSetInfoGt(TColorSetInfoGtCQ subQuery);
        protected void regColorSetInfoGtId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorSetInfoGtId(), "COLOR_SET_INFO_GT_ID");
        }
        protected abstract ConditionValue getCValueColorSetInfoGtId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TColorInfoDetailGtCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TColorInfoDetailGtCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TColorInfoDetailGtCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TColorInfoDetailGtCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TColorInfoDetailGtCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TColorInfoDetailGtCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TColorInfoDetailGtCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TColorInfoDetailGtCB>(delegate(String function, SubQuery<TColorInfoDetailGtCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TColorInfoDetailGtCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TColorInfoDetailGtCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TColorInfoDetailGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "COLOR_INFO_DETAIL_GT_ID", "COLOR_INFO_DETAIL_GT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TColorInfoDetailGtCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
