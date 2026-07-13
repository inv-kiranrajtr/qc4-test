
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
    public abstract class AbstractBsTGtScenarioItemCQ : AbstractConditionQuery {

        public AbstractBsTGtScenarioItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_GT_SCENARIO_ITEM"; }
        public override String getTableSqlName() { return "T_GT_SCENARIO_ITEM"; }

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
        public void ExistsTColorSetInfoGtList(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList(cb.Query());
            registerExistsSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepGtScenarioItemId_ExistsSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery);
        public void NotExistsTColorSetInfoGtList(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepGtScenarioItemId_NotExistsSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery);
        public void InScopeTColorSetInfoGtList(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepGtScenarioItemId_InScopeSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery);
        public void NotInScopeTColorSetInfoGtList(SubQuery<TColorSetInfoGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepGtScenarioItemId_NotInScopeSubQuery_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery);
        public void xsderiveTColorSetInfoGtList(String function, SubQuery<TColorSetInfoGtCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepGtScenarioItemId_SpecifyDerivedReferrer_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery);

        public QDRFunction<TColorSetInfoGtCB> DerivedTColorSetInfoGtList() {
            return xcreateQDRFunctionTColorSetInfoGtList();
        }
        protected QDRFunction<TColorSetInfoGtCB> xcreateQDRFunctionTColorSetInfoGtList() {
            return new QDRFunction<TColorSetInfoGtCB>(delegate(String function, SubQuery<TColorSetInfoGtCB> subQuery, String operand, Object value) {
                xqderiveTColorSetInfoGtList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTColorSetInfoGtList(String function, SubQuery<TColorSetInfoGtCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TColorSetInfoGtCB>", subQuery);
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtList(TColorSetInfoGtCQ subQuery);
        public abstract String keepGtScenarioItemId_QueryDerivedReferrer_TColorSetInfoGtListParameter(Object parameterValue);
        public void SetGtScenarioItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtScenarioItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGtScenarioItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGtScenarioItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGtScenarioItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGtScenarioItemId(), "GT_SCENARIO_ITEM_ID");
        }
        protected abstract ConditionValue getCValueGtScenarioItemId();

        public void SetScenarioTotalizationId_Equal(decimal? v) { regScenarioTotalizationId(CK_EQ, v); }
        public void SetScenarioTotalizationId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_NES, v);
        }
        public void SetScenarioTotalizationId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_GT, v);
        }
        public void SetScenarioTotalizationId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_LT, v);
        }
        public void SetScenarioTotalizationId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_GE, v);
        }
        public void SetScenarioTotalizationId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regScenarioTotalizationId(CK_LE, v);
        }
        public void SetScenarioTotalizationId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        public void SetScenarioTotalizationId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        public void InScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        public void NotInScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        protected void regScenarioTotalizationId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        protected abstract ConditionValue getCValueScenarioTotalizationId();

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

        public void SetItemInfoId_Equal(decimal? v) { regItemInfoId(CK_EQ, v); }
        public void SetItemInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_NES, v);
        }
        public void SetItemInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_GT, v);
        }
        public void SetItemInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_LT, v);
        }
        public void SetItemInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_GE, v);
        }
        public void SetItemInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regItemInfoId(CK_LE, v);
        }
        public void SetItemInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        public void SetItemInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        public void InScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TItemInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TItemInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        protected void regItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueItemInfoId();

        public void SetScenarioName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioName_Equal(fRES(v));
        }
        protected void DoSetScenarioName_Equal(String v) { regScenarioName(CK_EQ, v); }
        public void SetScenarioName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetScenarioName_NotEqual(fRES(v));
        }
        protected void DoSetScenarioName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_NES, v);
        }
        public void SetScenarioName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_GT, fRES(v));
        }
        public void SetScenarioName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_LT, fRES(v));
        }
        public void SetScenarioName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_GE, fRES(v));
        }
        public void SetScenarioName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioName(CK_LE, fRES(v));
        }
        public void SetScenarioName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueScenarioName(), "SCENARIO_NAME");
        }
        public void SetScenarioName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueScenarioName(), "SCENARIO_NAME");
        }
        public void SetScenarioName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetScenarioName_LikeSearch(v, cLSOP());
        }
        public void SetScenarioName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueScenarioName(), "SCENARIO_NAME", option);
        }
        public void SetScenarioName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueScenarioName(), "SCENARIO_NAME", option);
        }
        protected void regScenarioName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioName(), "SCENARIO_NAME");
        }
        protected abstract ConditionValue getCValueScenarioName();

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

        public void SetViewItemString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewItemString_Equal(fRES(v));
        }
        protected void DoSetViewItemString_Equal(String v) { regViewItemString(CK_EQ, v); }
        public void SetViewItemString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetViewItemString_NotEqual(fRES(v));
        }
        protected void DoSetViewItemString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_NES, v);
        }
        public void SetViewItemString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_GT, fRES(v));
        }
        public void SetViewItemString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_LT, fRES(v));
        }
        public void SetViewItemString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_GE, fRES(v));
        }
        public void SetViewItemString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_LE, fRES(v));
        }
        public void SetViewItemString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueViewItemString(), "VIEW_ITEM_STRING");
        }
        public void SetViewItemString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueViewItemString(), "VIEW_ITEM_STRING");
        }
        public void SetViewItemString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetViewItemString_LikeSearch(v, cLSOP());
        }
        public void SetViewItemString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueViewItemString(), "VIEW_ITEM_STRING", option);
        }
        public void SetViewItemString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueViewItemString(), "VIEW_ITEM_STRING", option);
        }
        public void SetViewItemString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetViewItemString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regViewItemString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regViewItemString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueViewItemString(), "VIEW_ITEM_STRING");
        }
        protected abstract ConditionValue getCValueViewItemString();

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

        public void SetSurveyType_Equal(int? v) { regSurveyType(CK_EQ, v); }
        public void SetSurveyType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyType(CK_NES, v);
        }
        public void SetSurveyType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyType(CK_GT, v);
        }
        public void SetSurveyType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyType(CK_LT, v);
        }
        public void SetSurveyType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyType(CK_GE, v);
        }
        public void SetSurveyType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSurveyType(CK_LE, v);
        }
        public void SetSurveyType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSurveyType(), "SURVEY_TYPE");
        }
        public void SetSurveyType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSurveyType(), "SURVEY_TYPE");
        }
        public void SetSurveyType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSurveyType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSurveyType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyType(), "SURVEY_TYPE");
        }
        protected abstract ConditionValue getCValueSurveyType();

        public void SetGraphTypeReport_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeReport_Equal(fRES(v));
        }
        protected void DoSetGraphTypeReport_Equal(String v) { regGraphTypeReport(CK_EQ, v); }
        public void SetGraphTypeReport_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGraphTypeReport_NotEqual(fRES(v));
        }
        protected void DoSetGraphTypeReport_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_NES, v);
        }
        public void SetGraphTypeReport_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_GT, fRES(v));
        }
        public void SetGraphTypeReport_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_LT, fRES(v));
        }
        public void SetGraphTypeReport_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_GE, fRES(v));
        }
        public void SetGraphTypeReport_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_LE, fRES(v));
        }
        public void SetGraphTypeReport_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT");
        }
        public void SetGraphTypeReport_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT");
        }
        public void SetGraphTypeReport_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGraphTypeReport_LikeSearch(v, cLSOP());
        }
        public void SetGraphTypeReport_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT", option);
        }
        public void SetGraphTypeReport_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT", option);
        }
        public void SetGraphTypeReport_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_ISN, DUMMY_OBJECT);
        }
        public void SetGraphTypeReport_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphTypeReport(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regGraphTypeReport(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphTypeReport(), "GRAPH_TYPE_REPORT");
        }
        protected abstract ConditionValue getCValueGraphTypeReport();

        public void SetTestTargetType_Equal(int? v) { regTestTargetType(CK_EQ, v); }
        public void SetTestTargetType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTargetType(CK_NES, v);
        }
        public void SetTestTargetType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTargetType(CK_GT, v);
        }
        public void SetTestTargetType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTargetType(CK_LT, v);
        }
        public void SetTestTargetType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTargetType(CK_GE, v);
        }
        public void SetTestTargetType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTestTargetType(CK_LE, v);
        }
        public void SetTestTargetType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTestTargetType(), "TEST_TARGET_TYPE");
        }
        public void SetTestTargetType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTestTargetType(), "TEST_TARGET_TYPE");
        }
        public void SetTestTargetType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTargetType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTestTargetType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTestTargetType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTestTargetType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTestTargetType(), "TEST_TARGET_TYPE");
        }
        protected abstract ConditionValue getCValueTestTargetType();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TGtScenarioItemCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TGtScenarioItemCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TGtScenarioItemCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TGtScenarioItemCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TGtScenarioItemCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TGtScenarioItemCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TGtScenarioItemCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TGtScenarioItemCB>(delegate(String function, SubQuery<TGtScenarioItemCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TGtScenarioItemCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TGtScenarioItemCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TGtScenarioItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TGtScenarioItemCB>", subQuery);
            TGtScenarioItemCB cb = new TGtScenarioItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "GT_SCENARIO_ITEM_ID", "GT_SCENARIO_ITEM_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TGtScenarioItemCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
