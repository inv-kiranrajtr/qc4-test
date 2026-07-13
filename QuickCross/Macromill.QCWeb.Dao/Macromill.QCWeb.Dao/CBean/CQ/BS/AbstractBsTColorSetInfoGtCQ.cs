
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
    public abstract class AbstractBsTColorSetInfoGtCQ : AbstractConditionQuery {

        public AbstractBsTColorSetInfoGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_COLOR_SET_INFO_GT"; }
        public override String getTableSqlName() { return "T_COLOR_SET_INFO_GT"; }

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
        public void ExistsTColorInfoDetailGtList(SubQuery<TColorInfoDetailGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList(cb.Query());
            registerExistsSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoGtId_ExistsSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery);
        public void NotExistsTColorInfoDetailGtList(SubQuery<TColorInfoDetailGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoGtId_NotExistsSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery);
        public void InScopeTColorInfoDetailGtList(SubQuery<TColorInfoDetailGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoGtId_InScopeSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery);
        public void NotInScopeTColorInfoDetailGtList(SubQuery<TColorInfoDetailGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepColorSetInfoGtId_NotInScopeSubQuery_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery);
        public void xsderiveTColorInfoDetailGtList(String function, SubQuery<TColorInfoDetailGtCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepColorSetInfoGtId_SpecifyDerivedReferrer_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery);

        public QDRFunction<TColorInfoDetailGtCB> DerivedTColorInfoDetailGtList() {
            return xcreateQDRFunctionTColorInfoDetailGtList();
        }
        protected QDRFunction<TColorInfoDetailGtCB> xcreateQDRFunctionTColorInfoDetailGtList() {
            return new QDRFunction<TColorInfoDetailGtCB>(delegate(String function, SubQuery<TColorInfoDetailGtCB> subQuery, String operand, Object value) {
                xqderiveTColorInfoDetailGtList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTColorInfoDetailGtList(String function, SubQuery<TColorInfoDetailGtCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TColorInfoDetailGtCB>", subQuery);
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtList(TColorInfoDetailGtCQ subQuery);
        public abstract String keepColorSetInfoGtId_QueryDerivedReferrer_TColorInfoDetailGtListParameter(Object parameterValue);
        public void SetColorSetInfoGtId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoGtId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetColorSetInfoGtId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regColorSetInfoGtId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regColorSetInfoGtId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueColorSetInfoGtId(), "COLOR_SET_INFO_GT_ID");
        }
        protected abstract ConditionValue getCValueColorSetInfoGtId();

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

        public void SetGtScenarioItemId_Equal(decimal? v) { regGtScenarioItemId(CK_EQ, v); }
        public void SetGtScenarioItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtScenarioItemId(CK_NES, v);
        }
        public void SetGtScenarioItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtScenarioItemId(CK_GT, v);
        }
        public void SetGtScenarioItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtScenarioItemId(CK_LT, v);
        }
        public void SetGtScenarioItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtScenarioItemId(CK_GE, v);
        }
        public void SetGtScenarioItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regGtScenarioItemId(CK_LE, v);
        }
        public void SetGtScenarioItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueGtScenarioItemId(), "GT_SCENARIO_ITEM_ID");
        }
        public void SetGtScenarioItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueGtScenarioItemId(), "GT_SCENARIO_ITEM_ID");
        }
        public void InScopeTGtScenarioItem(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_InScopeSubQuery_TGtScenarioItem(cb.Query());
            registerInScopeSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepGtScenarioItemId_InScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery);
        public void NotInScopeTGtScenarioItem(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepGtScenarioItemId_NotInScopeSubQuery_TGtScenarioItem(TGtScenarioItemCQ subQuery);
        protected void regGtScenarioItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGtScenarioItemId(), "GT_SCENARIO_ITEM_ID");
        }
        protected abstract ConditionValue getCValueGtScenarioItemId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TColorSetInfoGtCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TColorSetInfoGtCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TColorSetInfoGtCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TColorSetInfoGtCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TColorSetInfoGtCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TColorSetInfoGtCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TColorSetInfoGtCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TColorSetInfoGtCB>(delegate(String function, SubQuery<TColorSetInfoGtCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TColorSetInfoGtCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TColorSetInfoGtCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "COLOR_SET_INFO_GT_ID", "COLOR_SET_INFO_GT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TColorSetInfoGtCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
