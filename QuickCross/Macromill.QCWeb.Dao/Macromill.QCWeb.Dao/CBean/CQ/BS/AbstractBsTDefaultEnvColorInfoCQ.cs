
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
    public abstract class AbstractBsTDefaultEnvColorInfoCQ : AbstractConditionQuery {

        public AbstractBsTDefaultEnvColorInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_INFO"; }
        public override String getTableSqlName() { return "T_DEFAULT_ENV_COLOR_INFO"; }

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
        public void ExistsTDefaultEnvColorDtlList(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_ExistsSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery);
        public void NotExistsTDefaultEnvColorDtlList(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_NotExistsSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery);
        public void InScopeTDefaultEnvColorDtl(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "Def_Env_Color_Info_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtl(TDefaultEnvColorDtlCQ subQuery);
        public void InScopeTDefaultEnvColorDtlList(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_InScopeSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery);
        public void NotInScopeTDefaultEnvColorDtl(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "Def_Env_Color_Info_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtl(TDefaultEnvColorDtlCQ subQuery);
        public void NotInScopeTDefaultEnvColorDtlList(SubQuery<TDefaultEnvColorDtlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_NotInScopeSubQuery_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery);
        public void xsderiveTDefaultEnvColorDtlList(String function, SubQuery<TDefaultEnvColorDtlCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDefEnvColorInfoId_SpecifyDerivedReferrer_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery);

        public QDRFunction<TDefaultEnvColorDtlCB> DerivedTDefaultEnvColorDtlList() {
            return xcreateQDRFunctionTDefaultEnvColorDtlList();
        }
        protected QDRFunction<TDefaultEnvColorDtlCB> xcreateQDRFunctionTDefaultEnvColorDtlList() {
            return new QDRFunction<TDefaultEnvColorDtlCB>(delegate(String function, SubQuery<TDefaultEnvColorDtlCB> subQuery, String operand, Object value) {
                xqderiveTDefaultEnvColorDtlList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDefaultEnvColorDtlList(String function, SubQuery<TDefaultEnvColorDtlCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCB>", subQuery);
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlList(TDefaultEnvColorDtlCQ subQuery);
        public abstract String keepDefEnvColorInfoId_QueryDerivedReferrer_TDefaultEnvColorDtlListParameter(Object parameterValue);
        public void SetDefEnvColorInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDefEnvColorInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDefEnvColorInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDefEnvColorInfoId(), "DEF_ENV_COLOR_INFO_ID");
        }
        protected abstract ConditionValue getCValueDefEnvColorInfoId();

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
        public void InScopeTDefaultEnv(SubQuery<TDefaultEnvCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvCB>", subQuery);
            TDefaultEnvCB cb = new TDefaultEnvCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TDefaultEnv(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TDefaultEnv(TDefaultEnvCQ subQuery);
        public void NotInScopeTDefaultEnv(SubQuery<TDefaultEnvCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvCB>", subQuery);
            TDefaultEnvCB cb = new TDefaultEnvCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TDefaultEnv(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TDefaultEnv(TDefaultEnvCQ subQuery);
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetTypeCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTypeCode_Equal(fRES(v));
        }
        protected void DoSetTypeCode_Equal(String v) { regTypeCode(CK_EQ, v); }
        public void SetTypeCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTypeCode_NotEqual(fRES(v));
        }
        protected void DoSetTypeCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeCode(CK_NES, v);
        }
        public void SetTypeCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeCode(CK_GT, fRES(v));
        }
        public void SetTypeCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeCode(CK_LT, fRES(v));
        }
        public void SetTypeCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeCode(CK_GE, fRES(v));
        }
        public void SetTypeCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeCode(CK_LE, fRES(v));
        }
        public void SetTypeCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTypeCode(), "TYPE_CODE");
        }
        public void SetTypeCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTypeCode(), "TYPE_CODE");
        }
        public void SetTypeCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTypeCode_LikeSearch(v, cLSOP());
        }
        public void SetTypeCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTypeCode(), "TYPE_CODE", option);
        }
        public void SetTypeCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTypeCode(), "TYPE_CODE", option);
        }
        protected void regTypeCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTypeCode(), "TYPE_CODE");
        }
        protected abstract ConditionValue getCValueTypeCode();

        public void SetGradationType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGradationType_Equal(fRES(v));
        }
        protected void DoSetGradationType_Equal(String v) { regGradationType(CK_EQ, v); }
        public void SetGradationType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGradationType_NotEqual(fRES(v));
        }
        protected void DoSetGradationType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGradationType(CK_NES, v);
        }
        public void SetGradationType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGradationType(CK_GT, fRES(v));
        }
        public void SetGradationType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGradationType(CK_LT, fRES(v));
        }
        public void SetGradationType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGradationType(CK_GE, fRES(v));
        }
        public void SetGradationType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGradationType(CK_LE, fRES(v));
        }
        public void SetGradationType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGradationType(), "GRADATION_TYPE");
        }
        public void SetGradationType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGradationType(), "GRADATION_TYPE");
        }
        public void SetGradationType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGradationType_LikeSearch(v, cLSOP());
        }
        public void SetGradationType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGradationType(), "GRADATION_TYPE", option);
        }
        public void SetGradationType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGradationType(), "GRADATION_TYPE", option);
        }
        protected void regGradationType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGradationType(), "GRADATION_TYPE");
        }
        protected abstract ConditionValue getCValueGradationType();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDefaultEnvColorInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDefaultEnvColorInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDefaultEnvColorInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDefaultEnvColorInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDefaultEnvColorInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDefaultEnvColorInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDefaultEnvColorInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDefaultEnvColorInfoCB>(delegate(String function, SubQuery<TDefaultEnvColorInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDefaultEnvColorInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDefaultEnvColorInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDefaultEnvColorInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCB>", subQuery);
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_ID", "DEF_ENV_COLOR_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDefaultEnvColorInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
