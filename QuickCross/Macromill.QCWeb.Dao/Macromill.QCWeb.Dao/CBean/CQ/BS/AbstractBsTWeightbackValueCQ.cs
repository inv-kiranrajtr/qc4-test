
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
    public abstract class AbstractBsTWeightbackValueCQ : AbstractConditionQuery {

        public AbstractBsTWeightbackValueCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_WEIGHTBACK_VALUE"; }
        public override String getTableSqlName() { return "T_WEIGHTBACK_VALUE"; }

        public void SetWeightbackValueId_Equal(decimal? v) { regWeightbackValueId(CK_EQ, v); }
        public void SetWeightbackValueId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValueId(CK_NES, v);
        }
        public void SetWeightbackValueId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValueId(CK_GT, v);
        }
        public void SetWeightbackValueId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValueId(CK_LT, v);
        }
        public void SetWeightbackValueId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValueId(CK_GE, v);
        }
        public void SetWeightbackValueId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regWeightbackValueId(CK_LE, v);
        }
        public void SetWeightbackValueId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueWeightbackValueId(), "WEIGHTBACK_VALUE_ID");
        }
        public void SetWeightbackValueId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueWeightbackValueId(), "WEIGHTBACK_VALUE_ID");
        }
        public void SetWeightbackValueId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValueId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetWeightbackValueId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValueId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regWeightbackValueId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackValueId(), "WEIGHTBACK_VALUE_ID");
        }
        protected abstract ConditionValue getCValueWeightbackValueId();

        public void SetWeightbackItemNo_Equal(decimal? v) { regWeightbackItemNo(CK_EQ, v); }
        public void SetWeightbackItemNo_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemNo(CK_NES, v);
        }
        public void SetWeightbackItemNo_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemNo(CK_GT, v);
        }
        public void SetWeightbackItemNo_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemNo(CK_LT, v);
        }
        public void SetWeightbackItemNo_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackItemNo(CK_GE, v);
        }
        public void SetWeightbackItemNo_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regWeightbackItemNo(CK_LE, v);
        }
        public void SetWeightbackItemNo_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueWeightbackItemNo(), "WEIGHTBACK_ITEM_NO");
        }
        public void SetWeightbackItemNo_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueWeightbackItemNo(), "WEIGHTBACK_ITEM_NO");
        }
        protected void regWeightbackItemNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackItemNo(), "WEIGHTBACK_ITEM_NO");
        }
        protected abstract ConditionValue getCValueWeightbackItemNo();

        public void SetPercentValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPercentValue_Equal(fRES(v));
        }
        protected void DoSetPercentValue_Equal(String v) { regPercentValue(CK_EQ, v); }
        public void SetPercentValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPercentValue_NotEqual(fRES(v));
        }
        protected void DoSetPercentValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_NES, v);
        }
        public void SetPercentValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_GT, fRES(v));
        }
        public void SetPercentValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_LT, fRES(v));
        }
        public void SetPercentValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_GE, fRES(v));
        }
        public void SetPercentValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_LE, fRES(v));
        }
        public void SetPercentValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValuePercentValue(), "PERCENT_VALUE");
        }
        public void SetPercentValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValuePercentValue(), "PERCENT_VALUE");
        }
        public void SetPercentValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetPercentValue_LikeSearch(v, cLSOP());
        }
        public void SetPercentValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValuePercentValue(), "PERCENT_VALUE", option);
        }
        public void SetPercentValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValuePercentValue(), "PERCENT_VALUE", option);
        }
        public void SetPercentValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPercentValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPercentValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPercentValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePercentValue(), "PERCENT_VALUE");
        }
        protected abstract ConditionValue getCValuePercentValue();

        public void SetParameterNValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetParameterNValue_Equal(fRES(v));
        }
        protected void DoSetParameterNValue_Equal(String v) { regParameterNValue(CK_EQ, v); }
        public void SetParameterNValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetParameterNValue_NotEqual(fRES(v));
        }
        protected void DoSetParameterNValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_NES, v);
        }
        public void SetParameterNValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_GT, fRES(v));
        }
        public void SetParameterNValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_LT, fRES(v));
        }
        public void SetParameterNValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_GE, fRES(v));
        }
        public void SetParameterNValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_LE, fRES(v));
        }
        public void SetParameterNValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueParameterNValue(), "PARAMETER_N_VALUE");
        }
        public void SetParameterNValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueParameterNValue(), "PARAMETER_N_VALUE");
        }
        public void SetParameterNValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetParameterNValue_LikeSearch(v, cLSOP());
        }
        public void SetParameterNValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueParameterNValue(), "PARAMETER_N_VALUE", option);
        }
        public void SetParameterNValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueParameterNValue(), "PARAMETER_N_VALUE", option);
        }
        public void SetParameterNValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetParameterNValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regParameterNValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regParameterNValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueParameterNValue(), "PARAMETER_N_VALUE");
        }
        protected abstract ConditionValue getCValueParameterNValue();

        public void SetWeightbackValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetWeightbackValue_Equal(fRES(v));
        }
        protected void DoSetWeightbackValue_Equal(String v) { regWeightbackValue(CK_EQ, v); }
        public void SetWeightbackValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetWeightbackValue_NotEqual(fRES(v));
        }
        protected void DoSetWeightbackValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_NES, v);
        }
        public void SetWeightbackValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_GT, fRES(v));
        }
        public void SetWeightbackValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_LT, fRES(v));
        }
        public void SetWeightbackValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_GE, fRES(v));
        }
        public void SetWeightbackValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_LE, fRES(v));
        }
        public void SetWeightbackValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueWeightbackValue(), "WEIGHTBACK_VALUE");
        }
        public void SetWeightbackValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueWeightbackValue(), "WEIGHTBACK_VALUE");
        }
        public void SetWeightbackValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetWeightbackValue_LikeSearch(v, cLSOP());
        }
        public void SetWeightbackValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueWeightbackValue(), "WEIGHTBACK_VALUE", option);
        }
        public void SetWeightbackValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueWeightbackValue(), "WEIGHTBACK_VALUE", option);
        }
        public void SetWeightbackValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetWeightbackValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regWeightbackValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackValue(), "WEIGHTBACK_VALUE");
        }
        protected abstract ConditionValue getCValueWeightbackValue();

        public void SetWeightbackId_Equal(decimal? v) { regWeightbackId(CK_EQ, v); }
        public void SetWeightbackId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_NES, v);
        }
        public void SetWeightbackId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_GT, v);
        }
        public void SetWeightbackId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_LT, v);
        }
        public void SetWeightbackId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightbackId(CK_GE, v);
        }
        public void SetWeightbackId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regWeightbackId(CK_LE, v);
        }
        public void SetWeightbackId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueWeightbackId(), "WEIGHTBACK_ID");
        }
        public void SetWeightbackId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueWeightbackId(), "WEIGHTBACK_ID");
        }
        public void InScopeTWeightback(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_InScopeSubQuery_TWeightback(cb.Query());
            registerInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_InScopeSubQuery_TWeightback(TWeightbackCQ subQuery);
        public void NotInScopeTWeightback(SubQuery<TWeightbackCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackCB>", subQuery);
            TWeightbackCB cb = new TWeightbackCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepWeightbackId_NotInScopeSubQuery_TWeightback(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "WEIGHTBACK_ID", "WEIGHTBACK_ID", subQueryPropertyName);
        }
        public abstract String keepWeightbackId_NotInScopeSubQuery_TWeightback(TWeightbackCQ subQuery);
        protected void regWeightbackId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightbackId(), "WEIGHTBACK_ID");
        }
        protected abstract ConditionValue getCValueWeightbackId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TWeightbackValueCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TWeightbackValueCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TWeightbackValueCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TWeightbackValueCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TWeightbackValueCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TWeightbackValueCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TWeightbackValueCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TWeightbackValueCB>(delegate(String function, SubQuery<TWeightbackValueCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TWeightbackValueCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TWeightbackValueCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TWeightbackValueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TWeightbackValueCB>", subQuery);
            TWeightbackValueCB cb = new TWeightbackValueCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "WEIGHTBACK_VALUE_ID", "WEIGHTBACK_VALUE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TWeightbackValueCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
