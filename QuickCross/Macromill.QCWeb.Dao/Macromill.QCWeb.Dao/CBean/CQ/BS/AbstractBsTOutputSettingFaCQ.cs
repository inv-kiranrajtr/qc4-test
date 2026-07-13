
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
    public abstract class AbstractBsTOutputSettingFaCQ : AbstractConditionQuery {

        public AbstractBsTOutputSettingFaCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SETTING_FA"; }
        public override String getTableSqlName() { return "T_OUTPUT_SETTING_FA"; }

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

        public void SetPageSettingFlag_Equal(int? v) { regPageSettingFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of pageSettingFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingFlag(CK_EQ, int.Parse(code));
        }
        public void SetPageSettingFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of pageSettingFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingFlag(CK_NES, int.Parse(code));
        }
        public void SetPageSettingFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingFlag(), "PAGE_SETTING_FLAG");
        }
        public void SetPageSettingFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingFlag(), "PAGE_SETTING_FLAG");
        }
        protected void regPageSettingFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingFlag(), "PAGE_SETTING_FLAG");
        }
        protected abstract ConditionValue getCValuePageSettingFlag();

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

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSettingFaCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSettingFaCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSettingFaCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSettingFaCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSettingFaCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSettingFaCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSettingFaCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSettingFaCB>(delegate(String function, SubQuery<TOutputSettingFaCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSettingFaCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSettingFaCB>", subQuery);
            TOutputSettingFaCB cb = new TOutputSettingFaCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSettingFaCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSettingFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingFaCB>", subQuery);
            TOutputSettingFaCB cb = new TOutputSettingFaCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSettingFaCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
