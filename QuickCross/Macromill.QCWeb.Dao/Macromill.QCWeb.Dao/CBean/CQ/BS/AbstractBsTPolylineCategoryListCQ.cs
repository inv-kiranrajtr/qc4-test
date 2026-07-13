
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
    public abstract class AbstractBsTPolylineCategoryListCQ : AbstractConditionQuery {

        public AbstractBsTPolylineCategoryListCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_POLYLINE_CATEGORY_LIST"; }
        public override String getTableSqlName() { return "T_POLYLINE_CATEGORY_LIST"; }

        public void SetPolylineCategoryListId_Equal(decimal? v) { regPolylineCategoryListId(CK_EQ, v); }
        public void SetPolylineCategoryListId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineCategoryListId(CK_NES, v);
        }
        public void SetPolylineCategoryListId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineCategoryListId(CK_GT, v);
        }
        public void SetPolylineCategoryListId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineCategoryListId(CK_LT, v);
        }
        public void SetPolylineCategoryListId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineCategoryListId(CK_GE, v);
        }
        public void SetPolylineCategoryListId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regPolylineCategoryListId(CK_LE, v);
        }
        public void SetPolylineCategoryListId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValuePolylineCategoryListId(), "POLYLINE_CATEGORY_LIST_ID");
        }
        public void SetPolylineCategoryListId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValuePolylineCategoryListId(), "POLYLINE_CATEGORY_LIST_ID");
        }
        public void SetPolylineCategoryListId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineCategoryListId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPolylineCategoryListId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPolylineCategoryListId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPolylineCategoryListId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePolylineCategoryListId(), "POLYLINE_CATEGORY_LIST_ID");
        }
        protected abstract ConditionValue getCValuePolylineCategoryListId();

        public void SetCrossScenarioItemId_Equal(decimal? v) { regCrossScenarioItemId(CK_EQ, v); }
        public void SetCrossScenarioItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioItemId(CK_NES, v);
        }
        public void SetCrossScenarioItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioItemId(CK_GT, v);
        }
        public void SetCrossScenarioItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioItemId(CK_LT, v);
        }
        public void SetCrossScenarioItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioItemId(CK_GE, v);
        }
        public void SetCrossScenarioItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCrossScenarioItemId(CK_LE, v);
        }
        public void SetCrossScenarioItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCrossScenarioItemId(), "CROSS_SCENARIO_ITEM_ID");
        }
        public void SetCrossScenarioItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCrossScenarioItemId(), "CROSS_SCENARIO_ITEM_ID");
        }
        public void InScopeTCrossScenarioItem(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_InScopeSubQuery_TCrossScenarioItem(TCrossScenarioItemCQ subQuery);
        public void NotInScopeTCrossScenarioItem(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_NotInScopeSubQuery_TCrossScenarioItem(TCrossScenarioItemCQ subQuery);
        protected void regCrossScenarioItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossScenarioItemId(), "CROSS_SCENARIO_ITEM_ID");
        }
        protected abstract ConditionValue getCValueCrossScenarioItemId();

        public void SetAxisCategoryNo_Equal(int? v) { regAxisCategoryNo(CK_EQ, v); }
        public void SetAxisCategoryNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisCategoryNo(CK_NES, v);
        }
        public void SetAxisCategoryNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisCategoryNo(CK_GT, v);
        }
        public void SetAxisCategoryNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisCategoryNo(CK_LT, v);
        }
        public void SetAxisCategoryNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisCategoryNo(CK_GE, v);
        }
        public void SetAxisCategoryNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regAxisCategoryNo(CK_LE, v);
        }
        public void SetAxisCategoryNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueAxisCategoryNo(), "AXIS_CATEGORY_NO");
        }
        public void SetAxisCategoryNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueAxisCategoryNo(), "AXIS_CATEGORY_NO");
        }
        public void SetAxisCategoryNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisCategoryNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAxisCategoryNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxisCategoryNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAxisCategoryNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAxisCategoryNo(), "AXIS_CATEGORY_NO");
        }
        protected abstract ConditionValue getCValueAxisCategoryNo();

        public void SetAxis2CategoryNo_Equal(int? v) { regAxis2CategoryNo(CK_EQ, v); }
        public void SetAxis2CategoryNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2CategoryNo(CK_NES, v);
        }
        public void SetAxis2CategoryNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2CategoryNo(CK_GT, v);
        }
        public void SetAxis2CategoryNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2CategoryNo(CK_LT, v);
        }
        public void SetAxis2CategoryNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2CategoryNo(CK_GE, v);
        }
        public void SetAxis2CategoryNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regAxis2CategoryNo(CK_LE, v);
        }
        public void SetAxis2CategoryNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueAxis2CategoryNo(), "AXIS2_CATEGORY_NO");
        }
        public void SetAxis2CategoryNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueAxis2CategoryNo(), "AXIS2_CATEGORY_NO");
        }
        public void SetAxis2CategoryNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2CategoryNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAxis2CategoryNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2CategoryNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAxis2CategoryNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAxis2CategoryNo(), "AXIS2_CATEGORY_NO");
        }
        protected abstract ConditionValue getCValueAxis2CategoryNo();

        public void SetArrayNoSingular_Equal(int? v) { regArrayNoSingular(CK_EQ, v); }
        public void SetArrayNoSingular_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoSingular(CK_NES, v);
        }
        public void SetArrayNoSingular_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoSingular(CK_GT, v);
        }
        public void SetArrayNoSingular_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoSingular(CK_LT, v);
        }
        public void SetArrayNoSingular_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoSingular(CK_GE, v);
        }
        public void SetArrayNoSingular_LessEqual(int? v) {
            WhereSetterFlag = true;
            regArrayNoSingular(CK_LE, v);
        }
        public void SetArrayNoSingular_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueArrayNoSingular(), "ARRAY_NO_SINGULAR");
        }
        public void SetArrayNoSingular_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueArrayNoSingular(), "ARRAY_NO_SINGULAR");
        }
        public void SetArrayNoSingular_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoSingular(CK_ISN, DUMMY_OBJECT);
        }
        public void SetArrayNoSingular_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoSingular(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regArrayNoSingular(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueArrayNoSingular(), "ARRAY_NO_SINGULAR");
        }
        protected abstract ConditionValue getCValueArrayNoSingular();

        public void SetArrayNoPlural_Equal(int? v) { regArrayNoPlural(CK_EQ, v); }
        public void SetArrayNoPlural_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoPlural(CK_NES, v);
        }
        public void SetArrayNoPlural_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoPlural(CK_GT, v);
        }
        public void SetArrayNoPlural_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoPlural(CK_LT, v);
        }
        public void SetArrayNoPlural_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoPlural(CK_GE, v);
        }
        public void SetArrayNoPlural_LessEqual(int? v) {
            WhereSetterFlag = true;
            regArrayNoPlural(CK_LE, v);
        }
        public void SetArrayNoPlural_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueArrayNoPlural(), "ARRAY_NO_PLURAL");
        }
        public void SetArrayNoPlural_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueArrayNoPlural(), "ARRAY_NO_PLURAL");
        }
        public void SetArrayNoPlural_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoPlural(CK_ISN, DUMMY_OBJECT);
        }
        public void SetArrayNoPlural_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArrayNoPlural(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regArrayNoPlural(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueArrayNoPlural(), "ARRAY_NO_PLURAL");
        }
        protected abstract ConditionValue getCValueArrayNoPlural();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TPolylineCategoryListCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TPolylineCategoryListCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TPolylineCategoryListCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TPolylineCategoryListCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TPolylineCategoryListCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TPolylineCategoryListCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TPolylineCategoryListCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TPolylineCategoryListCB>(delegate(String function, SubQuery<TPolylineCategoryListCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TPolylineCategoryListCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TPolylineCategoryListCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "POLYLINE_CATEGORY_LIST_ID", "POLYLINE_CATEGORY_LIST_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TPolylineCategoryListCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
