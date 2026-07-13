
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
    public abstract class AbstractBsTCrossScenarioItemCQ : AbstractConditionQuery {

        public AbstractBsTCrossScenarioItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_CROSS_SCENARIO_ITEM"; }
        public override String getTableSqlName() { return "T_CROSS_SCENARIO_ITEM"; }

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
        public void ExistsTPolylineCategoryListList(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList(cb.Query());
            registerExistsSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_ExistsSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery);
        public void NotExistsTPolylineCategoryListList(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_NotExistsSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery);
        public void InScopeTPolylineCategoryList(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "Cross_Scenario_Item_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryList(TPolylineCategoryListCQ subQuery);
        public void InScopeTPolylineCategoryListList(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_InScopeSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery);
        public void NotInScopeTPolylineCategoryList(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "Cross_Scenario_Item_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryList(TPolylineCategoryListCQ subQuery);
        public void NotInScopeTPolylineCategoryListList(SubQuery<TPolylineCategoryListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepCrossScenarioItemId_NotInScopeSubQuery_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery);
        public void xsderiveTPolylineCategoryListList(String function, SubQuery<TPolylineCategoryListCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepCrossScenarioItemId_SpecifyDerivedReferrer_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery);

        public QDRFunction<TPolylineCategoryListCB> DerivedTPolylineCategoryListList() {
            return xcreateQDRFunctionTPolylineCategoryListList();
        }
        protected QDRFunction<TPolylineCategoryListCB> xcreateQDRFunctionTPolylineCategoryListList() {
            return new QDRFunction<TPolylineCategoryListCB>(delegate(String function, SubQuery<TPolylineCategoryListCB> subQuery, String operand, Object value) {
                xqderiveTPolylineCategoryListList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTPolylineCategoryListList(String function, SubQuery<TPolylineCategoryListCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TPolylineCategoryListCB>", subQuery);
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListList(TPolylineCategoryListCQ subQuery);
        public abstract String keepCrossScenarioItemId_QueryDerivedReferrer_TPolylineCategoryListListParameter(Object parameterValue);
        public void SetCrossScenarioItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCrossScenarioItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossScenarioItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCrossScenarioItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossScenarioItemId(), "CROSS_SCENARIO_ITEM_ID");
        }
        protected abstract ConditionValue getCValueCrossScenarioItemId();

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

        public void SetSortNo_Equal(int? v) { regSortNo(CK_EQ, v); }
        public void SetSortNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_NES, v);
        }
        public void SetSortNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GT, v);
        }
        public void SetSortNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_LT, v);
        }
        public void SetSortNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GE, v);
        }
        public void SetSortNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortNo(CK_LE, v);
        }
        public void SetSortNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        public void SetSortNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        protected void regSortNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortNo(), "SORT_NO");
        }
        protected abstract ConditionValue getCValueSortNo();

        public void SetAxis1ItemId_Equal(decimal? v) { regAxis1ItemId(CK_EQ, v); }
        public void SetAxis1ItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis1ItemId(CK_NES, v);
        }
        public void SetAxis1ItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis1ItemId(CK_GT, v);
        }
        public void SetAxis1ItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis1ItemId(CK_LT, v);
        }
        public void SetAxis1ItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis1ItemId(CK_GE, v);
        }
        public void SetAxis1ItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regAxis1ItemId(CK_LE, v);
        }
        public void SetAxis1ItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueAxis1ItemId(), "AXIS1_ITEM_ID");
        }
        public void SetAxis1ItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueAxis1ItemId(), "AXIS1_ITEM_ID");
        }
        public void SetAxis1ItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis1ItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAxis1ItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis1ItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAxis1ItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAxis1ItemId(), "AXIS1_ITEM_ID");
        }
        protected abstract ConditionValue getCValueAxis1ItemId();

        public void SetAxis2ItemId_Equal(decimal? v) { regAxis2ItemId(CK_EQ, v); }
        public void SetAxis2ItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2ItemId(CK_NES, v);
        }
        public void SetAxis2ItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2ItemId(CK_GT, v);
        }
        public void SetAxis2ItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2ItemId(CK_LT, v);
        }
        public void SetAxis2ItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2ItemId(CK_GE, v);
        }
        public void SetAxis2ItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regAxis2ItemId(CK_LE, v);
        }
        public void SetAxis2ItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueAxis2ItemId(), "AXIS2_ITEM_ID");
        }
        public void SetAxis2ItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueAxis2ItemId(), "AXIS2_ITEM_ID");
        }
        public void SetAxis2ItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2ItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAxis2ItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAxis2ItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAxis2ItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAxis2ItemId(), "AXIS2_ITEM_ID");
        }
        protected abstract ConditionValue getCValueAxis2ItemId();

        public void SetViewItemName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewItemName_Equal(fRES(v));
        }
        protected void DoSetViewItemName_Equal(String v) { regViewItemName(CK_EQ, v); }
        public void SetViewItemName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewItemName_NotEqual(fRES(v));
        }
        protected void DoSetViewItemName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_NES, v);
        }
        public void SetViewItemName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_GT, fRES(v));
        }
        public void SetViewItemName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_LT, fRES(v));
        }
        public void SetViewItemName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_GE, fRES(v));
        }
        public void SetViewItemName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_LE, fRES(v));
        }
        public void SetViewItemName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueViewItemName(), "VIEW_ITEM_NAME");
        }
        public void SetViewItemName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueViewItemName(), "VIEW_ITEM_NAME");
        }
        public void SetViewItemName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetViewItemName_LikeSearch(v, cLSOP());
        }
        public void SetViewItemName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueViewItemName(), "VIEW_ITEM_NAME", option);
        }
        public void SetViewItemName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueViewItemName(), "VIEW_ITEM_NAME", option);
        }
        public void SetViewItemName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetViewItemName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regViewItemName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueViewItemName(), "VIEW_ITEM_NAME");
        }
        protected abstract ConditionValue getCValueViewItemName();

        public void SetGraphType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphType_Equal(fRES(v));
        }
        protected void DoSetGraphType_Equal(String v) { regGraphType(CK_EQ, v); }
        public void SetGraphType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphType_NotEqual(fRES(v));
        }
        protected void DoSetGraphType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_NES, v);
        }
        public void SetGraphType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_GT, fRES(v));
        }
        public void SetGraphType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_LT, fRES(v));
        }
        public void SetGraphType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_GE, fRES(v));
        }
        public void SetGraphType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_LE, fRES(v));
        }
        public void SetGraphType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphType(), "GRAPH_TYPE");
        }
        public void SetGraphType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphType(), "GRAPH_TYPE");
        }
        public void SetGraphType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphType_LikeSearch(v, cLSOP());
        }
        public void SetGraphType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphType(), "GRAPH_TYPE", option);
        }
        public void SetGraphType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphType(), "GRAPH_TYPE", option);
        }
        public void SetGraphType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGraphType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGraphType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphType(), "GRAPH_TYPE");
        }
        protected abstract ConditionValue getCValueGraphType();

        public void SetReportType_Equal(int? v) { regReportType(CK_EQ, v); }
        public void SetReportType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_NES, v);
        }
        public void SetReportType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_GT, v);
        }
        public void SetReportType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_LT, v);
        }
        public void SetReportType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportType(CK_GE, v);
        }
        public void SetReportType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regReportType(CK_LE, v);
        }
        public void SetReportType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueReportType(), "REPORT_TYPE");
        }
        public void SetReportType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueReportType(), "REPORT_TYPE");
        }
        protected void regReportType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportType(), "REPORT_TYPE");
        }
        protected abstract ConditionValue getCValueReportType();

        public void SetTitleString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleString_Equal(fRES(v));
        }
        protected void DoSetTitleString_Equal(String v) { regTitleString(CK_EQ, v); }
        public void SetTitleString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTitleString_NotEqual(fRES(v));
        }
        protected void DoSetTitleString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_NES, v);
        }
        public void SetTitleString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_GT, fRES(v));
        }
        public void SetTitleString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_LT, fRES(v));
        }
        public void SetTitleString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_GE, fRES(v));
        }
        public void SetTitleString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_LE, fRES(v));
        }
        public void SetTitleString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTitleString(), "TITLE_STRING");
        }
        public void SetTitleString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTitleString(), "TITLE_STRING");
        }
        public void SetTitleString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTitleString_LikeSearch(v, cLSOP());
        }
        public void SetTitleString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTitleString(), "TITLE_STRING", option);
        }
        public void SetTitleString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTitleString(), "TITLE_STRING", option);
        }
        public void SetTitleString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTitleString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTitleString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTitleString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTitleString(), "TITLE_STRING");
        }
        protected abstract ConditionValue getCValueTitleString();

        public void SetScenarioComment_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioComment_Equal(fRES(v));
        }
        protected void DoSetScenarioComment_Equal(String v) { regScenarioComment(CK_EQ, v); }
        public void SetScenarioComment_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioComment_NotEqual(fRES(v));
        }
        protected void DoSetScenarioComment_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_NES, v);
        }
        public void SetScenarioComment_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_GT, fRES(v));
        }
        public void SetScenarioComment_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_LT, fRES(v));
        }
        public void SetScenarioComment_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_GE, fRES(v));
        }
        public void SetScenarioComment_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_LE, fRES(v));
        }
        public void SetScenarioComment_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueScenarioComment(), "SCENARIO_COMMENT");
        }
        public void SetScenarioComment_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueScenarioComment(), "SCENARIO_COMMENT");
        }
        public void SetScenarioComment_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetScenarioComment_LikeSearch(v, cLSOP());
        }
        public void SetScenarioComment_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueScenarioComment(), "SCENARIO_COMMENT", option);
        }
        public void SetScenarioComment_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueScenarioComment(), "SCENARIO_COMMENT", option);
        }
        public void SetScenarioComment_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_ISN, DUMMY_OBJECT);
        }
        public void SetScenarioComment_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioComment(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regScenarioComment(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioComment(), "SCENARIO_COMMENT");
        }
        protected abstract ConditionValue getCValueScenarioComment();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TCrossScenarioItemCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TCrossScenarioItemCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TCrossScenarioItemCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TCrossScenarioItemCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TCrossScenarioItemCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TCrossScenarioItemCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TCrossScenarioItemCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TCrossScenarioItemCB>(delegate(String function, SubQuery<TCrossScenarioItemCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TCrossScenarioItemCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TCrossScenarioItemCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TCrossScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCrossScenarioItemCB>", subQuery);
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "CROSS_SCENARIO_ITEM_ID", "CROSS_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TCrossScenarioItemCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
