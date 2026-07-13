
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
    public abstract class AbstractBsTOutputSubCrossCQ : AbstractConditionQuery {

        public AbstractBsTOutputSubCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SUB_CROSS"; }
        public override String getTableSqlName() { return "T_OUTPUT_SUB_CROSS"; }

        public void SetOutputSubCrossId_Equal(decimal? v) { regOutputSubCrossId(CK_EQ, v); }
        public void SetOutputSubCrossId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCrossId(CK_NES, v);
        }
        public void SetOutputSubCrossId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCrossId(CK_GT, v);
        }
        public void SetOutputSubCrossId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCrossId(CK_LT, v);
        }
        public void SetOutputSubCrossId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCrossId(CK_GE, v);
        }
        public void SetOutputSubCrossId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputSubCrossId(CK_LE, v);
        }
        public void SetOutputSubCrossId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputSubCrossId(), "OUTPUT_SUB_CROSS_ID");
        }
        public void SetOutputSubCrossId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputSubCrossId(), "OUTPUT_SUB_CROSS_ID");
        }
        public void SetOutputSubCrossId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCrossId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputSubCrossId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputSubCrossId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputSubCrossId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputSubCrossId(), "OUTPUT_SUB_CROSS_ID");
        }
        protected abstract ConditionValue getCValueOutputSubCrossId();

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

        public void SetOutputType_Equal(int? v) { regOutputType(CK_EQ, v); }
        public void SetOutputType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_NES, v);
        }
        public void SetOutputType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_GT, v);
        }
        public void SetOutputType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_LT, v);
        }
        public void SetOutputType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_GE, v);
        }
        public void SetOutputType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regOutputType(CK_LE, v);
        }
        public void SetOutputType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueOutputType(), "OUTPUT_TYPE");
        }
        public void SetOutputType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueOutputType(), "OUTPUT_TYPE");
        }
        protected void regOutputType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputType(), "OUTPUT_TYPE");
        }
        protected abstract ConditionValue getCValueOutputType();

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
        public void SetPageSettingTableType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingTableType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPageSettingTableType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingTableType(CK_ISNN, DUMMY_OBJECT);
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

        public void SetLevel2pluscolor_Equal(int? v) { regLevel2pluscolor(CK_EQ, v); }
        public void SetLevel2pluscolor_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2pluscolor(CK_NES, v);
        }
        public void SetLevel2pluscolor_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2pluscolor(CK_GT, v);
        }
        public void SetLevel2pluscolor_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2pluscolor(CK_LT, v);
        }
        public void SetLevel2pluscolor_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2pluscolor(CK_GE, v);
        }
        public void SetLevel2pluscolor_LessEqual(int? v) {
            WhereSetterFlag = true;
            regLevel2pluscolor(CK_LE, v);
        }
        public void SetLevel2pluscolor_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLevel2pluscolor(), "LEVEL2PLUSCOLOR");
        }
        public void SetLevel2pluscolor_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLevel2pluscolor(), "LEVEL2PLUSCOLOR");
        }
        public void SetLevel2pluscolor_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2pluscolor(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLevel2pluscolor_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2pluscolor(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLevel2pluscolor(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLevel2pluscolor(), "LEVEL2PLUSCOLOR");
        }
        protected abstract ConditionValue getCValueLevel2pluscolor();

        public void SetLevel1pluscolor_Equal(int? v) { regLevel1pluscolor(CK_EQ, v); }
        public void SetLevel1pluscolor_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1pluscolor(CK_NES, v);
        }
        public void SetLevel1pluscolor_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1pluscolor(CK_GT, v);
        }
        public void SetLevel1pluscolor_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1pluscolor(CK_LT, v);
        }
        public void SetLevel1pluscolor_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1pluscolor(CK_GE, v);
        }
        public void SetLevel1pluscolor_LessEqual(int? v) {
            WhereSetterFlag = true;
            regLevel1pluscolor(CK_LE, v);
        }
        public void SetLevel1pluscolor_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLevel1pluscolor(), "LEVEL1PLUSCOLOR");
        }
        public void SetLevel1pluscolor_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLevel1pluscolor(), "LEVEL1PLUSCOLOR");
        }
        public void SetLevel1pluscolor_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1pluscolor(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLevel1pluscolor_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1pluscolor(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLevel1pluscolor(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLevel1pluscolor(), "LEVEL1PLUSCOLOR");
        }
        protected abstract ConditionValue getCValueLevel1pluscolor();

        public void SetLevel1minuscolor_Equal(int? v) { regLevel1minuscolor(CK_EQ, v); }
        public void SetLevel1minuscolor_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1minuscolor(CK_NES, v);
        }
        public void SetLevel1minuscolor_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1minuscolor(CK_GT, v);
        }
        public void SetLevel1minuscolor_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1minuscolor(CK_LT, v);
        }
        public void SetLevel1minuscolor_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1minuscolor(CK_GE, v);
        }
        public void SetLevel1minuscolor_LessEqual(int? v) {
            WhereSetterFlag = true;
            regLevel1minuscolor(CK_LE, v);
        }
        public void SetLevel1minuscolor_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLevel1minuscolor(), "LEVEL1MINUSCOLOR");
        }
        public void SetLevel1minuscolor_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLevel1minuscolor(), "LEVEL1MINUSCOLOR");
        }
        public void SetLevel1minuscolor_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1minuscolor(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLevel1minuscolor_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1minuscolor(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLevel1minuscolor(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLevel1minuscolor(), "LEVEL1MINUSCOLOR");
        }
        protected abstract ConditionValue getCValueLevel1minuscolor();

        public void SetLevel2minuscolor_Equal(int? v) { regLevel2minuscolor(CK_EQ, v); }
        public void SetLevel2minuscolor_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2minuscolor(CK_NES, v);
        }
        public void SetLevel2minuscolor_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2minuscolor(CK_GT, v);
        }
        public void SetLevel2minuscolor_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2minuscolor(CK_LT, v);
        }
        public void SetLevel2minuscolor_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2minuscolor(CK_GE, v);
        }
        public void SetLevel2minuscolor_LessEqual(int? v) {
            WhereSetterFlag = true;
            regLevel2minuscolor(CK_LE, v);
        }
        public void SetLevel2minuscolor_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLevel2minuscolor(), "LEVEL2MINUSCOLOR");
        }
        public void SetLevel2minuscolor_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLevel2minuscolor(), "LEVEL2MINUSCOLOR");
        }
        public void SetLevel2minuscolor_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2minuscolor(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLevel2minuscolor_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2minuscolor(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLevel2minuscolor(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLevel2minuscolor(), "LEVEL2MINUSCOLOR");
        }
        protected abstract ConditionValue getCValueLevel2minuscolor();

        public void SetLevel2percent_Equal(int? v) { regLevel2percent(CK_EQ, v); }
        public void SetLevel2percent_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2percent(CK_NES, v);
        }
        public void SetLevel2percent_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2percent(CK_GT, v);
        }
        public void SetLevel2percent_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2percent(CK_LT, v);
        }
        public void SetLevel2percent_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2percent(CK_GE, v);
        }
        public void SetLevel2percent_LessEqual(int? v) {
            WhereSetterFlag = true;
            regLevel2percent(CK_LE, v);
        }
        public void SetLevel2percent_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLevel2percent(), "LEVEL2PERCENT");
        }
        public void SetLevel2percent_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLevel2percent(), "LEVEL2PERCENT");
        }
        public void SetLevel2percent_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2percent(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLevel2percent_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel2percent(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLevel2percent(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLevel2percent(), "LEVEL2PERCENT");
        }
        protected abstract ConditionValue getCValueLevel2percent();

        public void SetLevel1percent_Equal(int? v) { regLevel1percent(CK_EQ, v); }
        public void SetLevel1percent_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1percent(CK_NES, v);
        }
        public void SetLevel1percent_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1percent(CK_GT, v);
        }
        public void SetLevel1percent_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1percent(CK_LT, v);
        }
        public void SetLevel1percent_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1percent(CK_GE, v);
        }
        public void SetLevel1percent_LessEqual(int? v) {
            WhereSetterFlag = true;
            regLevel1percent(CK_LE, v);
        }
        public void SetLevel1percent_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLevel1percent(), "LEVEL1PERCENT");
        }
        public void SetLevel1percent_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLevel1percent(), "LEVEL1PERCENT");
        }
        public void SetLevel1percent_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1percent(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLevel1percent_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLevel1percent(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLevel1percent(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLevel1percent(), "LEVEL1PERCENT");
        }
        protected abstract ConditionValue getCValueLevel1percent();

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
        public SSQFunction<TOutputSubCrossCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSubCrossCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSubCrossCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSubCrossCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSubCrossCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSubCrossCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSubCrossCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSubCrossCB>(delegate(String function, SubQuery<TOutputSubCrossCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSubCrossCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSubCrossCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSubCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_SUB_CROSS_ID", "OUTPUT_SUB_CROSS_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSubCrossCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
