
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
    public abstract class AbstractBsTOutputSettingReportCQ : AbstractConditionQuery {

        public AbstractBsTOutputSettingReportCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SETTING_REPORT"; }
        public override String getTableSqlName() { return "T_OUTPUT_SETTING_REPORT"; }

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
        public void ExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void SetQcwebid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQcwebid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetFileTypeExcelFlag_Equal(int? v) { regFileTypeExcelFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of fileTypeExcelFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetFileTypeExcelFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFileTypeExcelFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of fileTypeExcelFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFileTypeExcelFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFileTypeExcelFlag(CK_EQ, int.Parse(code));
        }
        public void SetFileTypeExcelFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileTypeExcelFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of fileTypeExcelFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetFileTypeExcelFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFileTypeExcelFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of fileTypeExcelFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFileTypeExcelFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFileTypeExcelFlag(CK_NES, int.Parse(code));
        }
        public void SetFileTypeExcelFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFileTypeExcelFlag(), "FILE_TYPE_EXCEL_FLAG");
        }
        public void SetFileTypeExcelFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFileTypeExcelFlag(), "FILE_TYPE_EXCEL_FLAG");
        }
        protected void regFileTypeExcelFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFileTypeExcelFlag(), "FILE_TYPE_EXCEL_FLAG");
        }
        protected abstract ConditionValue getCValueFileTypeExcelFlag();

        public void SetFileTypePpFlag_Equal(int? v) { regFileTypePpFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of fileTypePpFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetFileTypePpFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFileTypePpFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of fileTypePpFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFileTypePpFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFileTypePpFlag(CK_EQ, int.Parse(code));
        }
        public void SetFileTypePpFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileTypePpFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of fileTypePpFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetFileTypePpFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFileTypePpFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of fileTypePpFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFileTypePpFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFileTypePpFlag(CK_NES, int.Parse(code));
        }
        public void SetFileTypePpFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFileTypePpFlag(), "FILE_TYPE_PP_FLAG");
        }
        public void SetFileTypePpFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFileTypePpFlag(), "FILE_TYPE_PP_FLAG");
        }
        protected void regFileTypePpFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFileTypePpFlag(), "FILE_TYPE_PP_FLAG");
        }
        protected abstract ConditionValue getCValueFileTypePpFlag();

        public void SetFileTypePdfFlag_Equal(int? v) { regFileTypePdfFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of fileTypePdfFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetFileTypePdfFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFileTypePdfFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of fileTypePdfFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFileTypePdfFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFileTypePdfFlag(CK_EQ, int.Parse(code));
        }
        public void SetFileTypePdfFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFileTypePdfFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of fileTypePdfFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetFileTypePdfFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regFileTypePdfFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of fileTypePdfFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetFileTypePdfFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regFileTypePdfFlag(CK_NES, int.Parse(code));
        }
        public void SetFileTypePdfFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueFileTypePdfFlag(), "FILE_TYPE_PDF_FLAG");
        }
        public void SetFileTypePdfFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueFileTypePdfFlag(), "FILE_TYPE_PDF_FLAG");
        }
        protected void regFileTypePdfFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFileTypePdfFlag(), "FILE_TYPE_PDF_FLAG");
        }
        protected abstract ConditionValue getCValueFileTypePdfFlag();

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

        public void SetGraphOutputFlag_Equal(int? v) { regGraphOutputFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of graphOutputFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetGraphOutputFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regGraphOutputFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of graphOutputFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetGraphOutputFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regGraphOutputFlag(CK_EQ, int.Parse(code));
        }
        public void SetGraphOutputFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGraphOutputFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of graphOutputFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetGraphOutputFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regGraphOutputFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of graphOutputFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetGraphOutputFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regGraphOutputFlag(CK_NES, int.Parse(code));
        }
        public void SetGraphOutputFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueGraphOutputFlag(), "GRAPH_OUTPUT_FLAG");
        }
        public void SetGraphOutputFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueGraphOutputFlag(), "GRAPH_OUTPUT_FLAG");
        }
        protected void regGraphOutputFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGraphOutputFlag(), "GRAPH_OUTPUT_FLAG");
        }
        protected abstract ConditionValue getCValueGraphOutputFlag();

        public void SetAscFlag_Equal(int? v) { regAscFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of ascFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetAscFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regAscFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of ascFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetAscFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regAscFlag(CK_EQ, int.Parse(code));
        }
        public void SetAscFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAscFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of ascFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetAscFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regAscFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of ascFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetAscFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regAscFlag(CK_NES, int.Parse(code));
        }
        public void SetAscFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueAscFlag(), "ASC_FLAG");
        }
        public void SetAscFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueAscFlag(), "ASC_FLAG");
        }
        protected void regAscFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAscFlag(), "ASC_FLAG");
        }
        protected abstract ConditionValue getCValueAscFlag();

        public void SetCommentVisibleFlag_Equal(int? v) { regCommentVisibleFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of commentVisibleFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCommentVisibleFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCommentVisibleFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of commentVisibleFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCommentVisibleFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCommentVisibleFlag(CK_EQ, int.Parse(code));
        }
        public void SetCommentVisibleFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCommentVisibleFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of commentVisibleFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCommentVisibleFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCommentVisibleFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of commentVisibleFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCommentVisibleFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCommentVisibleFlag(CK_NES, int.Parse(code));
        }
        public void SetCommentVisibleFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCommentVisibleFlag(), "COMMENT_VISIBLE_FLAG");
        }
        public void SetCommentVisibleFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCommentVisibleFlag(), "COMMENT_VISIBLE_FLAG");
        }
        protected void regCommentVisibleFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCommentVisibleFlag(), "COMMENT_VISIBLE_FLAG");
        }
        protected abstract ConditionValue getCValueCommentVisibleFlag();

        public void SetSurveyReportFlag_Equal(int? v) { regSurveyReportFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of surveyReportFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetSurveyReportFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSurveyReportFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of surveyReportFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSurveyReportFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSurveyReportFlag(CK_EQ, int.Parse(code));
        }
        public void SetSurveyReportFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyReportFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of surveyReportFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetSurveyReportFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSurveyReportFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of surveyReportFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSurveyReportFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSurveyReportFlag(CK_NES, int.Parse(code));
        }
        public void SetSurveyReportFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSurveyReportFlag(), "SURVEY_REPORT_FLAG");
        }
        public void SetSurveyReportFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSurveyReportFlag(), "SURVEY_REPORT_FLAG");
        }
        protected void regSurveyReportFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyReportFlag(), "SURVEY_REPORT_FLAG");
        }
        protected abstract ConditionValue getCValueSurveyReportFlag();

        public void SetOutputTemplateId_Equal(decimal? v) { regOutputTemplateId(CK_EQ, v); }
        public void SetOutputTemplateId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateId(CK_NES, v);
        }
        public void SetOutputTemplateId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateId(CK_GT, v);
        }
        public void SetOutputTemplateId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateId(CK_LT, v);
        }
        public void SetOutputTemplateId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateId(CK_GE, v);
        }
        public void SetOutputTemplateId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputTemplateId(CK_LE, v);
        }
        public void SetOutputTemplateId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputTemplateId(), "OUTPUT_TEMPLATE_ID");
        }
        public void SetOutputTemplateId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputTemplateId(), "OUTPUT_TEMPLATE_ID");
        }
        public void SetOutputTemplateId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputTemplateId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputTemplateId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputTemplateId(), "OUTPUT_TEMPLATE_ID");
        }
        protected abstract ConditionValue getCValueOutputTemplateId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSettingReportCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSettingReportCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSettingReportCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSettingReportCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSettingReportCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSettingReportCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSettingReportCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSettingReportCB>(delegate(String function, SubQuery<TOutputSettingReportCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSettingReportCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSettingReportCB>", subQuery);
            TOutputSettingReportCB cb = new TOutputSettingReportCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSettingReportCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSettingReportCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingReportCB>", subQuery);
            TOutputSettingReportCB cb = new TOutputSettingReportCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSettingReportCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
