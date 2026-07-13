
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
    public abstract class AbstractBsTColorSetInfoCrossCQ : AbstractConditionQuery {

        public AbstractBsTColorSetInfoCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_COLOR_SET_INFO_CROSS"; }
        public override String getTableSqlName() { return "T_COLOR_SET_INFO_CROSS"; }

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
        public void ExistsTColorInfoDetailCrossList(SubQuery<TColorInfoDetailCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList(cb.Query());
            registerExistsSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_ExistsSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery);
        public void NotExistsTColorInfoDetailCrossList(SubQuery<TColorInfoDetailCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_NotExistsSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery);
        public void InScopeTColorInfoDetailCrossList(SubQuery<TColorInfoDetailCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_InScopeSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery);
        public void NotInScopeTColorInfoDetailCrossList(SubQuery<TColorInfoDetailCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_NotInScopeSubQuery_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery);
        public void xsderiveTColorInfoDetailCrossList(String function, SubQuery<TColorInfoDetailCrossCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepColorSetInfoCrossId_SpecifyDerivedReferrer_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery);

        public QDRFunction<TColorInfoDetailCrossCB> DerivedTColorInfoDetailCrossList() {
            return xcreateQDRFunctionTColorInfoDetailCrossList();
        }
        protected QDRFunction<TColorInfoDetailCrossCB> xcreateQDRFunctionTColorInfoDetailCrossList() {
            return new QDRFunction<TColorInfoDetailCrossCB>(delegate(String function, SubQuery<TColorInfoDetailCrossCB> subQuery, String operand, Object value) {
                xqderiveTColorInfoDetailCrossList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTColorInfoDetailCrossList(String function, SubQuery<TColorInfoDetailCrossCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TColorInfoDetailCrossCB>", subQuery);
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossList(TColorInfoDetailCrossCQ subQuery);
        public abstract String keepColorSetInfoCrossId_QueryDerivedReferrer_TColorInfoDetailCrossListParameter(Object parameterValue);
        public void SetColorSetInfoCrossId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoCrossId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColorSetInfoCrossId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoCrossId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColorSetInfoCrossId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorSetInfoCrossId(), "COLOR_SET_INFO_CROSS_ID");
        }
        protected abstract ConditionValue getCValueColorSetInfoCrossId();

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

        public void SetCrossScenarioTargetId_Equal(decimal? v) { regCrossScenarioTargetId(CK_EQ, v); }
        public void SetCrossScenarioTargetId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_NES, v);
        }
        public void SetCrossScenarioTargetId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_GT, v);
        }
        public void SetCrossScenarioTargetId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_LT, v);
        }
        public void SetCrossScenarioTargetId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioTargetId(CK_GE, v);
        }
        public void SetCrossScenarioTargetId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCrossScenarioTargetId(CK_LE, v);
        }
        public void SetCrossScenarioTargetId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCrossScenarioTargetId(), "CROSS_SCENARIO_TARGET_ID");
        }
        public void SetCrossScenarioTargetId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCrossScenarioTargetId(), "CROSS_SCENARIO_TARGET_ID");
        }
        public void InScopeTCrossScenarioTarget(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_InScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery);
        public void NotInScopeTCrossScenarioTarget(SubQuery<TCrossScenarioTargetCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioTargetCB>", subQuery);
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_TARGET_ID", "CROSS_SCENARIO_TARGET_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioTargetId_NotInScopeSubQuery_TCrossScenarioTarget(TCrossScenarioTargetCQ subQuery);
        protected void regCrossScenarioTargetId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossScenarioTargetId(), "CROSS_SCENARIO_TARGET_ID");
        }
        protected abstract ConditionValue getCValueCrossScenarioTargetId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TColorSetInfoCrossCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TColorSetInfoCrossCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TColorSetInfoCrossCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TColorSetInfoCrossCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TColorSetInfoCrossCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TColorSetInfoCrossCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TColorSetInfoCrossCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TColorSetInfoCrossCB>(delegate(String function, SubQuery<TColorSetInfoCrossCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TColorSetInfoCrossCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TColorSetInfoCrossCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TColorSetInfoCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoCrossCB>", subQuery);
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_CROSS_ID", "COLOR_SET_INFO_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TColorSetInfoCrossCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
