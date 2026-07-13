
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
    public abstract class AbstractBsTDefaultEnvColorInfoCCQ : AbstractConditionQuery {

        public AbstractBsTDefaultEnvColorInfoCCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_INFO_C"; }
        public override String getTableSqlName() { return "T_DEFAULT_ENV_COLOR_INFO_C"; }

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
        public void ExistsTDefaultEnvColorDtlCList(SubQuery<TDefaultEnvColorDtlCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_ExistsSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery);
        public void NotExistsTDefaultEnvColorDtlCList(SubQuery<TDefaultEnvColorDtlCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_NotExistsSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery);
        public void InScopeTDefaultEnvColorDtlCList(SubQuery<TDefaultEnvColorDtlCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_InScopeSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery);
        public void NotInScopeTDefaultEnvColorDtlCList(SubQuery<TDefaultEnvColorDtlCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_NotInScopeSubQuery_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery);
        public void xsderiveTDefaultEnvColorDtlCList(String function, SubQuery<TDefaultEnvColorDtlCCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDefEnvColorInfoCId_SpecifyDerivedReferrer_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery);

        public QDRFunction<TDefaultEnvColorDtlCCB> DerivedTDefaultEnvColorDtlCList() {
            return xcreateQDRFunctionTDefaultEnvColorDtlCList();
        }
        protected QDRFunction<TDefaultEnvColorDtlCCB> xcreateQDRFunctionTDefaultEnvColorDtlCList() {
            return new QDRFunction<TDefaultEnvColorDtlCCB>(delegate(String function, SubQuery<TDefaultEnvColorDtlCCB> subQuery, String operand, Object value) {
                xqderiveTDefaultEnvColorDtlCList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDefaultEnvColorDtlCList(String function, SubQuery<TDefaultEnvColorDtlCCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDefaultEnvColorDtlCCB>", subQuery);
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCList(TDefaultEnvColorDtlCCQ subQuery);
        public abstract String keepDefEnvColorInfoCId_QueryDerivedReferrer_TDefaultEnvColorDtlCListParameter(Object parameterValue);
        public void SetDefEnvColorInfoCId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoCId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDefEnvColorInfoCId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDefEnvColorInfoCId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDefEnvColorInfoCId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDefEnvColorInfoCId(), "DEF_ENV_COLOR_INFO_C_ID");
        }
        protected abstract ConditionValue getCValueDefEnvColorInfoCId();

        public void SetLanguage_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLanguage_Equal(fRES(v));
        }
        protected void DoSetLanguage_Equal(String v) { regLanguage(CK_EQ, v); }
        public void SetLanguage_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLanguage_NotEqual(fRES(v));
        }
        protected void DoSetLanguage_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_NES, v);
        }
        public void SetLanguage_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_GT, fRES(v));
        }
        public void SetLanguage_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_LT, fRES(v));
        }
        public void SetLanguage_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_GE, fRES(v));
        }
        public void SetLanguage_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLanguage(CK_LE, fRES(v));
        }
        public void SetLanguage_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLanguage(), "LANGUAGE");
        }
        public void SetLanguage_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLanguage(), "LANGUAGE");
        }
        public void SetLanguage_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLanguage_LikeSearch(v, cLSOP());
        }
        public void SetLanguage_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLanguage(), "LANGUAGE", option);
        }
        public void SetLanguage_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLanguage(), "LANGUAGE", option);
        }
        public void InScopeTDefaultEnvBase(SubQuery<TDefaultEnvBaseCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvBaseCB>", subQuery);
            TDefaultEnvBaseCB cb = new TDefaultEnvBaseCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepLanguage_InScopeSubQuery_TDefaultEnvBase(cb.Query());
            registerInScopeSubQuery(cb.Query(), "LANGUAGE", "LANGUAGE", subQueryPropertyName);
        }
        public abstract String keepLanguage_InScopeSubQuery_TDefaultEnvBase(TDefaultEnvBaseCQ subQuery);
        public void NotInScopeTDefaultEnvBase(SubQuery<TDefaultEnvBaseCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvBaseCB>", subQuery);
            TDefaultEnvBaseCB cb = new TDefaultEnvBaseCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepLanguage_NotInScopeSubQuery_TDefaultEnvBase(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "LANGUAGE", "LANGUAGE", subQueryPropertyName);
        }
        public abstract String keepLanguage_NotInScopeSubQuery_TDefaultEnvBase(TDefaultEnvBaseCQ subQuery);
        protected void regLanguage(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLanguage(), "LANGUAGE");
        }
        protected abstract ConditionValue getCValueLanguage();

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
        public SSQFunction<TDefaultEnvColorInfoCCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDefaultEnvColorInfoCCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDefaultEnvColorInfoCCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDefaultEnvColorInfoCCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDefaultEnvColorInfoCCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDefaultEnvColorInfoCCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDefaultEnvColorInfoCCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDefaultEnvColorInfoCCB>(delegate(String function, SubQuery<TDefaultEnvColorInfoCCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDefaultEnvColorInfoCCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCCB>", subQuery);
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDefaultEnvColorInfoCCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDefaultEnvColorInfoCCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDefaultEnvColorInfoCCB>", subQuery);
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DEF_ENV_COLOR_INFO_C_ID", "DEF_ENV_COLOR_INFO_C_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDefaultEnvColorInfoCCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
