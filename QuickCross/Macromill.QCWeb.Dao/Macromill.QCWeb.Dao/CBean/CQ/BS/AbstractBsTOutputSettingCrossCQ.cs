
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
    public abstract class AbstractBsTOutputSettingCrossCQ : AbstractConditionQuery {

        public AbstractBsTOutputSettingCrossCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SETTING_CROSS"; }
        public override String getTableSqlName() { return "T_OUTPUT_SETTING_CROSS"; }

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

        public void SetCrossNpFlag_Equal(int? v) { regCrossNpFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of crossNpFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCrossNpFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCrossNpFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of crossNpFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCrossNpFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCrossNpFlag(CK_EQ, int.Parse(code));
        }
        public void SetCrossNpFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossNpFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of crossNpFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCrossNpFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCrossNpFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of crossNpFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCrossNpFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCrossNpFlag(CK_NES, int.Parse(code));
        }
        public void SetCrossNpFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCrossNpFlag(), "CROSS_NP_FLAG");
        }
        public void SetCrossNpFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCrossNpFlag(), "CROSS_NP_FLAG");
        }
        protected void regCrossNpFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossNpFlag(), "CROSS_NP_FLAG");
        }
        protected abstract ConditionValue getCValueCrossNpFlag();

        public void SetCrossNFlag_Equal(int? v) { regCrossNFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of crossNFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCrossNFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCrossNFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of crossNFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCrossNFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCrossNFlag(CK_EQ, int.Parse(code));
        }
        public void SetCrossNFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossNFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of crossNFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCrossNFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCrossNFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of crossNFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCrossNFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCrossNFlag(CK_NES, int.Parse(code));
        }
        public void SetCrossNFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCrossNFlag(), "CROSS_N_FLAG");
        }
        public void SetCrossNFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCrossNFlag(), "CROSS_N_FLAG");
        }
        protected void regCrossNFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossNFlag(), "CROSS_N_FLAG");
        }
        protected abstract ConditionValue getCValueCrossNFlag();

        public void SetCrossPFlag_Equal(int? v) { regCrossPFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of crossPFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCrossPFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCrossPFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of crossPFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCrossPFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCrossPFlag(CK_EQ, int.Parse(code));
        }
        public void SetCrossPFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCrossPFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of crossPFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCrossPFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCrossPFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of crossPFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCrossPFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCrossPFlag(CK_NES, int.Parse(code));
        }
        public void SetCrossPFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCrossPFlag(), "CROSS_P_FLAG");
        }
        public void SetCrossPFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCrossPFlag(), "CROSS_P_FLAG");
        }
        protected void regCrossPFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCrossPFlag(), "CROSS_P_FLAG");
        }
        protected abstract ConditionValue getCValueCrossPFlag();

        public void SetPageSettingNpFlag_Equal(int? v) { regPageSettingNpFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of pageSettingNpFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingNpFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingNpFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingNpFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingNpFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingNpFlag(CK_EQ, int.Parse(code));
        }
        public void SetPageSettingNpFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingNpFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of pageSettingNpFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingNpFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingNpFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingNpFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingNpFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingNpFlag(CK_NES, int.Parse(code));
        }
        public void SetPageSettingNpFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingNpFlag(), "PAGE_SETTING_NP_FLAG");
        }
        public void SetPageSettingNpFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingNpFlag(), "PAGE_SETTING_NP_FLAG");
        }
        protected void regPageSettingNpFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingNpFlag(), "PAGE_SETTING_NP_FLAG");
        }
        protected abstract ConditionValue getCValuePageSettingNpFlag();

        public void SetPageSettingNFlag_Equal(int? v) { regPageSettingNFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of pageSettingNFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingNFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingNFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingNFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingNFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingNFlag(CK_EQ, int.Parse(code));
        }
        public void SetPageSettingNFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingNFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of pageSettingNFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingNFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingNFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingNFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingNFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingNFlag(CK_NES, int.Parse(code));
        }
        public void SetPageSettingNFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingNFlag(), "PAGE_SETTING_N_FLAG");
        }
        public void SetPageSettingNFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingNFlag(), "PAGE_SETTING_N_FLAG");
        }
        protected void regPageSettingNFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingNFlag(), "PAGE_SETTING_N_FLAG");
        }
        protected abstract ConditionValue getCValuePageSettingNFlag();

        public void SetPageSettingPFlag_Equal(int? v) { regPageSettingPFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of pageSettingPFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingPFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingPFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingPFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingPFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingPFlag(CK_EQ, int.Parse(code));
        }
        public void SetPageSettingPFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPageSettingPFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of pageSettingPFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPageSettingPFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPageSettingPFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of pageSettingPFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPageSettingPFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPageSettingPFlag(CK_NES, int.Parse(code));
        }
        public void SetPageSettingPFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePageSettingPFlag(), "PAGE_SETTING_P_FLAG");
        }
        public void SetPageSettingPFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePageSettingPFlag(), "PAGE_SETTING_P_FLAG");
        }
        protected void regPageSettingPFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePageSettingPFlag(), "PAGE_SETTING_P_FLAG");
        }
        protected abstract ConditionValue getCValuePageSettingPFlag();

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

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSettingCrossCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSettingCrossCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSettingCrossCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSettingCrossCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSettingCrossCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSettingCrossCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSettingCrossCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSettingCrossCB>(delegate(String function, SubQuery<TOutputSettingCrossCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSettingCrossCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSettingCrossCB>", subQuery);
            TOutputSettingCrossCB cb = new TOutputSettingCrossCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSettingCrossCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSettingCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCrossCB>", subQuery);
            TOutputSettingCrossCB cb = new TOutputSettingCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSettingCrossCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
