
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
    public abstract class AbstractBsTOutputSubGtCQ : AbstractConditionQuery {

        public AbstractBsTOutputSubGtCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SUB_GT"; }
        public override String getTableSqlName() { return "T_OUTPUT_SUB_GT"; }

        public void SetOutputSubGtId_Equal(decimal? v) { regOutputSubGtId(CK_EQ, v); }
        public void SetOutputSubGtId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubGtId(CK_NES, v);
        }
        public void SetOutputSubGtId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubGtId(CK_GT, v);
        }
        public void SetOutputSubGtId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubGtId(CK_LT, v);
        }
        public void SetOutputSubGtId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubGtId(CK_GE, v);
        }
        public void SetOutputSubGtId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputSubGtId(CK_LE, v);
        }
        public void SetOutputSubGtId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputSubGtId(), "OUTPUT_SUB_GT_ID");
        }
        public void SetOutputSubGtId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputSubGtId(), "OUTPUT_SUB_GT_ID");
        }
        public void SetOutputSubGtId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubGtId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputSubGtId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubGtId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputSubGtId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputSubGtId(), "OUTPUT_SUB_GT_ID");
        }
        protected abstract ConditionValue getCValueOutputSubGtId();

        public void SetOutputCommonId_Equal(decimal? v) { regOutputCommonId(CK_EQ, v); }
        public void SetOutputCommonId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_NES, v);
        }
        public void SetOutputCommonId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GT, v);
        }
        public void SetOutputCommonId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_LT, v);
        }
        public void SetOutputCommonId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GE, v);
        }
        public void SetOutputCommonId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputCommonId(CK_LE, v);
        }
        public void SetOutputCommonId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void SetOutputCommonId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void InScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputCommon(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        public void NotInScopeTOutputCommon(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputCommon(TOutputCommonCQ subQuery);
        protected void regOutputCommonId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        protected abstract ConditionValue getCValueOutputCommonId();

        public void SetOutputTableType_Equal(int? v) { regOutputTableType(CK_EQ, v); }
        public void SetOutputTableType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableType(CK_NES, v);
        }
        public void SetOutputTableType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableType(CK_GT, v);
        }
        public void SetOutputTableType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableType(CK_LT, v);
        }
        public void SetOutputTableType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableType(CK_GE, v);
        }
        public void SetOutputTableType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regOutputTableType(CK_LE, v);
        }
        public void SetOutputTableType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueOutputTableType(), "OUTPUT_TABLE_TYPE");
        }
        public void SetOutputTableType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueOutputTableType(), "OUTPUT_TABLE_TYPE");
        }
        protected void regOutputTableType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputTableType(), "OUTPUT_TABLE_TYPE");
        }
        protected abstract ConditionValue getCValueOutputTableType();

        public void SetOutputTableOrientation_Equal(int? v) { regOutputTableOrientation(CK_EQ, v); }
        public void SetOutputTableOrientation_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableOrientation(CK_NES, v);
        }
        public void SetOutputTableOrientation_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableOrientation(CK_GT, v);
        }
        public void SetOutputTableOrientation_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableOrientation(CK_LT, v);
        }
        public void SetOutputTableOrientation_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTableOrientation(CK_GE, v);
        }
        public void SetOutputTableOrientation_LessEqual(int? v) {
            WhereSetterFlag = true;
            regOutputTableOrientation(CK_LE, v);
        }
        public void SetOutputTableOrientation_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueOutputTableOrientation(), "OUTPUT_TABLE_ORIENTATION");
        }
        public void SetOutputTableOrientation_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueOutputTableOrientation(), "OUTPUT_TABLE_ORIENTATION");
        }
        protected void regOutputTableOrientation(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputTableOrientation(), "OUTPUT_TABLE_ORIENTATION");
        }
        protected abstract ConditionValue getCValueOutputTableOrientation();

        public void SetPageSettingTableType_Equal(int? v) { regPageSettingTableType(CK_EQ, v); }
        public void SetPageSettingTableType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingTableType(CK_NES, v);
        }
        public void SetPageSettingTableType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingTableType(CK_GT, v);
        }
        public void SetPageSettingTableType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingTableType(CK_LT, v);
        }
        public void SetPageSettingTableType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingTableType(CK_GE, v);
        }
        public void SetPageSettingTableType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPageSettingTableType(CK_LE, v);
        }
        public void SetPageSettingTableType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingTableType(), "PAGE_SETTING_TABLE_TYPE");
        }
        public void SetPageSettingTableType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingTableType(), "PAGE_SETTING_TABLE_TYPE");
        }
        protected void regPageSettingTableType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingTableType(), "PAGE_SETTING_TABLE_TYPE");
        }
        protected abstract ConditionValue getCValuePageSettingTableType();

        public void SetPageSettingPaperSize_Equal(int? v) { regPageSettingPaperSize(CK_EQ, v); }
        public void SetPageSettingPaperSize_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_NES, v);
        }
        public void SetPageSettingPaperSize_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_GT, v);
        }
        public void SetPageSettingPaperSize_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_LT, v);
        }
        public void SetPageSettingPaperSize_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_GE, v);
        }
        public void SetPageSettingPaperSize_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPageSettingPaperSize(CK_LE, v);
        }
        public void SetPageSettingPaperSize_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingPaperSize(), "PAGE_SETTING_PAPER_SIZE");
        }
        public void SetPageSettingPaperSize_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingPaperSize(), "PAGE_SETTING_PAPER_SIZE");
        }
        public void SetPageSettingPaperSize_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPageSettingPaperSize_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperSize(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPageSettingPaperSize(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingPaperSize(), "PAGE_SETTING_PAPER_SIZE");
        }
        protected abstract ConditionValue getCValuePageSettingPaperSize();

        public void SetPageSettingPaperOrientation_Equal(int? v) { regPageSettingPaperOrientation(CK_EQ, v); }
        public void SetPageSettingPaperOrientation_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_NES, v);
        }
        public void SetPageSettingPaperOrientation_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_GT, v);
        }
        public void SetPageSettingPaperOrientation_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_LT, v);
        }
        public void SetPageSettingPaperOrientation_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_GE, v);
        }
        public void SetPageSettingPaperOrientation_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPageSettingPaperOrientation(CK_LE, v);
        }
        public void SetPageSettingPaperOrientation_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingPaperOrientation(), "PAGE_SETTING_PAPER_ORIENTATION");
        }
        public void SetPageSettingPaperOrientation_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingPaperOrientation(), "PAGE_SETTING_PAPER_ORIENTATION");
        }
        public void SetPageSettingPaperOrientation_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPageSettingPaperOrientation_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPaperOrientation(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPageSettingPaperOrientation(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingPaperOrientation(), "PAGE_SETTING_PAPER_ORIENTATION");
        }
        protected abstract ConditionValue getCValuePageSettingPaperOrientation();

        public void SetMarkingLevel_Equal(int? v) { regMarkingLevel(CK_EQ, v); }
        public void SetMarkingLevel_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingLevel(CK_NES, v);
        }
        public void SetMarkingLevel_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingLevel(CK_GT, v);
        }
        public void SetMarkingLevel_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingLevel(CK_LT, v);
        }
        public void SetMarkingLevel_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingLevel(CK_GE, v);
        }
        public void SetMarkingLevel_LessEqual(int? v) {
            WhereSetterFlag = true;
            regMarkingLevel(CK_LE, v);
        }
        public void SetMarkingLevel_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMarkingLevel(), "MARKING_LEVEL");
        }
        public void SetMarkingLevel_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMarkingLevel(), "MARKING_LEVEL");
        }
        public void SetMarkingLevel_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingLevel(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMarkingLevel_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingLevel(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMarkingLevel(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMarkingLevel(), "MARKING_LEVEL");
        }
        protected abstract ConditionValue getCValueMarkingLevel();

        public void SetMarkingMinParameter_Equal(long? v) { regMarkingMinParameter(CK_EQ, v); }
        public void SetMarkingMinParameter_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingMinParameter(CK_NES, v);
        }
        public void SetMarkingMinParameter_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingMinParameter(CK_GT, v);
        }
        public void SetMarkingMinParameter_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingMinParameter(CK_LT, v);
        }
        public void SetMarkingMinParameter_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingMinParameter(CK_GE, v);
        }
        public void SetMarkingMinParameter_LessEqual(long? v) {
            WhereSetterFlag = true;
            regMarkingMinParameter(CK_LE, v);
        }
        public void SetMarkingMinParameter_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueMarkingMinParameter(), "MARKING_MIN_PARAMETER");
        }
        public void SetMarkingMinParameter_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueMarkingMinParameter(), "MARKING_MIN_PARAMETER");
        }
        public void SetMarkingMinParameter_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingMinParameter(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMarkingMinParameter_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingMinParameter(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMarkingMinParameter(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMarkingMinParameter(), "MARKING_MIN_PARAMETER");
        }
        protected abstract ConditionValue getCValueMarkingMinParameter();

        public void SetMarkingCode_Equal(int? v) { regMarkingCode(CK_EQ, v); }
        public void SetMarkingCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingCode(CK_NES, v);
        }
        public void SetMarkingCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingCode(CK_GT, v);
        }
        public void SetMarkingCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingCode(CK_LT, v);
        }
        public void SetMarkingCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingCode(CK_GE, v);
        }
        public void SetMarkingCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regMarkingCode(CK_LE, v);
        }
        public void SetMarkingCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMarkingCode(), "MARKING_CODE");
        }
        public void SetMarkingCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMarkingCode(), "MARKING_CODE");
        }
        public void SetMarkingCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMarkingCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarkingCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMarkingCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMarkingCode(), "MARKING_CODE");
        }
        protected abstract ConditionValue getCValueMarkingCode();

        public void SetFilteringExpression_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFilteringExpression_Equal(fRES(v));
        }
        protected void DoSetFilteringExpression_Equal(String v) { regFilteringExpression(CK_EQ, v); }
        public void SetFilteringExpression_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFilteringExpression_NotEqual(fRES(v));
        }
        protected void DoSetFilteringExpression_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_NES, v);
        }
        public void SetFilteringExpression_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_GT, fRES(v));
        }
        public void SetFilteringExpression_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_LT, fRES(v));
        }
        public void SetFilteringExpression_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_GE, fRES(v));
        }
        public void SetFilteringExpression_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_LE, fRES(v));
        }
        public void SetFilteringExpression_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFilteringExpression(), "FILTERING_EXPRESSION");
        }
        public void SetFilteringExpression_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFilteringExpression(), "FILTERING_EXPRESSION");
        }
        public void SetFilteringExpression_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFilteringExpression_LikeSearch(v, cLSOP());
        }
        public void SetFilteringExpression_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFilteringExpression(), "FILTERING_EXPRESSION", option);
        }
        public void SetFilteringExpression_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFilteringExpression(), "FILTERING_EXPRESSION", option);
        }
        public void SetFilteringExpression_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFilteringExpression_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFilteringExpression(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFilteringExpression(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFilteringExpression(), "FILTERING_EXPRESSION");
        }
        protected abstract ConditionValue getCValueFilteringExpression();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSubGtCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSubGtCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSubGtCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSubGtCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSubGtCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSubGtCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSubGtCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSubGtCB>(delegate(String function, SubQuery<TOutputSubGtCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSubGtCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSubGtCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_SUB_GT_ID", "OUTPUT_SUB_GT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSubGtCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
