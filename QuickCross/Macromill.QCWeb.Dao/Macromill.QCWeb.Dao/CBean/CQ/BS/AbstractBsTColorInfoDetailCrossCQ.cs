
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
    public abstract class AbstractBsTColorInfoDetailCrossCQ : AbstractConditionQuery {

        public AbstractBsTColorInfoDetailCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_COLOR_INFO_DETAIL_CROSS"; }
        public override String getTableSqlName() { return "T_COLOR_INFO_DETAIL_CROSS"; }

        public void SetColorInfoDetailCrossId_Equal(decimal? v) { regColorInfoDetailCrossId(CK_EQ, v); }
        public void SetColorInfoDetailCrossId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailCrossId(CK_NES, v);
        }
        public void SetColorInfoDetailCrossId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailCrossId(CK_GT, v);
        }
        public void SetColorInfoDetailCrossId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailCrossId(CK_LT, v);
        }
        public void SetColorInfoDetailCrossId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailCrossId(CK_GE, v);
        }
        public void SetColorInfoDetailCrossId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regColorInfoDetailCrossId(CK_LE, v);
        }
        public void SetColorInfoDetailCrossId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueColorInfoDetailCrossId(), "COLOR_INFO_DETAIL_CROSS_ID");
        }
        public void SetColorInfoDetailCrossId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueColorInfoDetailCrossId(), "COLOR_INFO_DETAIL_CROSS_ID");
        }
        public void SetColorInfoDetailCrossId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailCrossId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColorInfoDetailCrossId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorInfoDetailCrossId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColorInfoDetailCrossId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorInfoDetailCrossId(), "COLOR_INFO_DETAIL_CROSS_ID");
        }
        protected abstract ConditionValue getCValueColorInfoDetailCrossId();

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

        public void SetColorSetInfoCrossId_Equal(decimal? v) { regColorSetInfoCrossId(CK_EQ, v); }
        public void SetColorSetInfoCrossId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoCrossId(CK_NES, v);
        }
        public void SetColorSetInfoCrossId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoCrossId(CK_GT, v);
        }
        public void SetColorSetInfoCrossId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoCrossId(CK_LT, v);
        }
        public void SetColorSetInfoCrossId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoCrossId(CK_GE, v);
        }
        public void SetColorSetInfoCrossId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regColorSetInfoCrossId(CK_LE, v);
        }
        public void SetColorSetInfoCrossId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueColorSetInfoCrossId(), "COLOR_SET_INFO_CROSS_ID");
        }
        public void SetColorSetInfoCrossId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueColorSetInfoCrossId(), "COLOR_SET_INFO_CROSS_ID");
        }
        public void InScopeTColorSetInfoCross(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCross(cb.Query());
            registerInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_InScopeSubQuery_TColorSetInfoCross(TColorSetInfoCrossCQ subQuery);
        public void NotInScopeTColorSetInfoCross(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCross(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_NotInScopeSubQuery_TColorSetInfoCross(TColorSetInfoCrossCQ subQuery);
        protected void regColorSetInfoCrossId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorSetInfoCrossId(), "COLOR_SET_INFO_CROSS_ID");
        }
        protected abstract ConditionValue getCValueColorSetInfoCrossId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TColorInfoDetailCrossCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TColorInfoDetailCrossCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TColorInfoDetailCrossCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TColorInfoDetailCrossCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TColorInfoDetailCrossCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TColorInfoDetailCrossCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TColorInfoDetailCrossCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TColorInfoDetailCrossCB>(delegate(String function, SubQuery<TColorInfoDetailCrossCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TColorInfoDetailCrossCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TColorInfoDetailCrossCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TColorInfoDetailCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "COLOR_INFO_DETAIL_CROSS_ID", "COLOR_INFO_DETAIL_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TColorInfoDetailCrossCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
