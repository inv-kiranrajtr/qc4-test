
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
    public abstract class AbstractBsTOutputSettingCQ : AbstractConditionQuery {

        public AbstractBsTOutputSettingCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_SETTING"; }
        public override String getTableSqlName() { return "T_OUTPUT_SETTING"; }

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
        public void ExistsTOutputHistoryItemList(SubQuery<TOutputHistoryItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputHistoryItemCB>", subQuery);
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TOutputHistoryItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery);
        public void ExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTOutputHistoryItemList(SubQuery<TOutputHistoryItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputHistoryItemCB>", subQuery);
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TOutputHistoryItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery);
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
        public void InScopeTOutputHistoryItemList(SubQuery<TOutputHistoryItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputHistoryItemCB>", subQuery);
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TOutputHistoryItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery);
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
        public void NotInScopeTOutputHistoryItemList(SubQuery<TOutputHistoryItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputHistoryItemCB>", subQuery);
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TOutputHistoryItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void xsderiveTOutputHistoryItemList(String function, SubQuery<TOutputHistoryItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputHistoryItemCB>", subQuery);
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TOutputHistoryItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery);

        public QDRFunction<TOutputHistoryItemCB> DerivedTOutputHistoryItemList() {
            return xcreateQDRFunctionTOutputHistoryItemList();
        }
        protected QDRFunction<TOutputHistoryItemCB> xcreateQDRFunctionTOutputHistoryItemList() {
            return new QDRFunction<TOutputHistoryItemCB>(delegate(String function, SubQuery<TOutputHistoryItemCB> subQuery, String operand, Object value) {
                xqderiveTOutputHistoryItemList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputHistoryItemList(String function, SubQuery<TOutputHistoryItemCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputHistoryItemCB>", subQuery);
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemList(TOutputHistoryItemCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TOutputHistoryItemListParameter(Object parameterValue);
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

        public void SetOutputFileType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputFileType_Equal(fRES(v));
        }
        protected void DoSetOutputFileType_Equal(String v) { regOutputFileType(CK_EQ, v); }
        public void SetOutputFileType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputFileType_NotEqual(fRES(v));
        }
        protected void DoSetOutputFileType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileType(CK_NES, v);
        }
        public void SetOutputFileType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileType(CK_GT, fRES(v));
        }
        public void SetOutputFileType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileType(CK_LT, fRES(v));
        }
        public void SetOutputFileType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileType(CK_GE, fRES(v));
        }
        public void SetOutputFileType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileType(CK_LE, fRES(v));
        }
        public void SetOutputFileType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOutputFileType(), "OUTPUT_FILE_TYPE");
        }
        public void SetOutputFileType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOutputFileType(), "OUTPUT_FILE_TYPE");
        }
        public void SetOutputFileType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOutputFileType_LikeSearch(v, cLSOP());
        }
        public void SetOutputFileType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOutputFileType(), "OUTPUT_FILE_TYPE", option);
        }
        public void SetOutputFileType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOutputFileType(), "OUTPUT_FILE_TYPE", option);
        }
        protected void regOutputFileType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputFileType(), "OUTPUT_FILE_TYPE");
        }
        protected abstract ConditionValue getCValueOutputFileType();

        public void SetPartitionFlag_Equal(int? v) { regPartitionFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of partitionFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetPartitionFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPartitionFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of partitionFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPartitionFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPartitionFlag(CK_EQ, int.Parse(code));
        }
        public void SetPartitionFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPartitionFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of partitionFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetPartitionFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regPartitionFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of partitionFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetPartitionFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regPartitionFlag(CK_NES, int.Parse(code));
        }
        public void SetPartitionFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePartitionFlag(), "PARTITION_FLAG");
        }
        public void SetPartitionFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePartitionFlag(), "PARTITION_FLAG");
        }
        protected void regPartitionFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePartitionFlag(), "PARTITION_FLAG");
        }
        protected abstract ConditionValue getCValuePartitionFlag();

        public void SetLayoutFlag_Equal(int? v) { regLayoutFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of layoutFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetLayoutFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regLayoutFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of layoutFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetLayoutFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regLayoutFlag(CK_EQ, int.Parse(code));
        }
        public void SetLayoutFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLayoutFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of layoutFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetLayoutFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regLayoutFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of layoutFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetLayoutFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regLayoutFlag(CK_NES, int.Parse(code));
        }
        public void SetLayoutFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLayoutFlag(), "LAYOUT_FLAG");
        }
        public void SetLayoutFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLayoutFlag(), "LAYOUT_FLAG");
        }
        protected void regLayoutFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLayoutFlag(), "LAYOUT_FLAG");
        }
        protected abstract ConditionValue getCValueLayoutFlag();

        public void SetOutputType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputType_Equal(fRES(v));
        }
        protected void DoSetOutputType_Equal(String v) { regOutputType(CK_EQ, v); }
        public void SetOutputType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputType_NotEqual(fRES(v));
        }
        protected void DoSetOutputType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_NES, v);
        }
        public void SetOutputType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_GT, fRES(v));
        }
        public void SetOutputType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_LT, fRES(v));
        }
        public void SetOutputType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_GE, fRES(v));
        }
        public void SetOutputType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_LE, fRES(v));
        }
        public void SetOutputType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOutputType(), "OUTPUT_TYPE");
        }
        public void SetOutputType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOutputType(), "OUTPUT_TYPE");
        }
        public void SetOutputType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOutputType_LikeSearch(v, cLSOP());
        }
        public void SetOutputType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOutputType(), "OUTPUT_TYPE", option);
        }
        public void SetOutputType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOutputType(), "OUTPUT_TYPE", option);
        }
        public void SetOutputType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputType(), "OUTPUT_TYPE");
        }
        protected abstract ConditionValue getCValueOutputType();

        public void SetNoAnswerChar_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNoAnswerChar_Equal(fRES(v));
        }
        protected void DoSetNoAnswerChar_Equal(String v) { regNoAnswerChar(CK_EQ, v); }
        public void SetNoAnswerChar_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNoAnswerChar_NotEqual(fRES(v));
        }
        protected void DoSetNoAnswerChar_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoAnswerChar(CK_NES, v);
        }
        public void SetNoAnswerChar_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoAnswerChar(CK_GT, fRES(v));
        }
        public void SetNoAnswerChar_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoAnswerChar(CK_LT, fRES(v));
        }
        public void SetNoAnswerChar_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoAnswerChar(CK_GE, fRES(v));
        }
        public void SetNoAnswerChar_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoAnswerChar(CK_LE, fRES(v));
        }
        public void SetNoAnswerChar_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNoAnswerChar(), "NO_ANSWER_CHAR");
        }
        public void SetNoAnswerChar_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNoAnswerChar(), "NO_ANSWER_CHAR");
        }
        public void SetNoAnswerChar_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNoAnswerChar_LikeSearch(v, cLSOP());
        }
        public void SetNoAnswerChar_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNoAnswerChar(), "NO_ANSWER_CHAR", option);
        }
        public void SetNoAnswerChar_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNoAnswerChar(), "NO_ANSWER_CHAR", option);
        }
        protected void regNoAnswerChar(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoAnswerChar(), "NO_ANSWER_CHAR");
        }
        protected abstract ConditionValue getCValueNoAnswerChar();

        public void SetUnmacthChar_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetUnmacthChar_Equal(fRES(v));
        }
        protected void DoSetUnmacthChar_Equal(String v) { regUnmacthChar(CK_EQ, v); }
        public void SetUnmacthChar_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetUnmacthChar_NotEqual(fRES(v));
        }
        protected void DoSetUnmacthChar_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmacthChar(CK_NES, v);
        }
        public void SetUnmacthChar_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmacthChar(CK_GT, fRES(v));
        }
        public void SetUnmacthChar_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmacthChar(CK_LT, fRES(v));
        }
        public void SetUnmacthChar_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmacthChar(CK_GE, fRES(v));
        }
        public void SetUnmacthChar_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmacthChar(CK_LE, fRES(v));
        }
        public void SetUnmacthChar_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueUnmacthChar(), "UNMACTH_CHAR");
        }
        public void SetUnmacthChar_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueUnmacthChar(), "UNMACTH_CHAR");
        }
        public void SetUnmacthChar_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetUnmacthChar_LikeSearch(v, cLSOP());
        }
        public void SetUnmacthChar_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueUnmacthChar(), "UNMACTH_CHAR", option);
        }
        public void SetUnmacthChar_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueUnmacthChar(), "UNMACTH_CHAR", option);
        }
        protected void regUnmacthChar(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUnmacthChar(), "UNMACTH_CHAR");
        }
        protected abstract ConditionValue getCValueUnmacthChar();

        public void SetMultiItemType_Equal(int? v) { regMultiItemType(CK_EQ, v); }
        public void SetMultiItemType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMultiItemType(CK_NES, v);
        }
        public void SetMultiItemType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMultiItemType(CK_GT, v);
        }
        public void SetMultiItemType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMultiItemType(CK_LT, v);
        }
        public void SetMultiItemType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMultiItemType(CK_GE, v);
        }
        public void SetMultiItemType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regMultiItemType(CK_LE, v);
        }
        public void SetMultiItemType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMultiItemType(), "MULTI_ITEM_TYPE");
        }
        public void SetMultiItemType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMultiItemType(), "MULTI_ITEM_TYPE");
        }
        protected void regMultiItemType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMultiItemType(), "MULTI_ITEM_TYPE");
        }
        protected abstract ConditionValue getCValueMultiItemType();

        public void SetNumberType_Equal(int? v) { regNumberType(CK_EQ, v); }
        public void SetNumberType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumberType(CK_NES, v);
        }
        public void SetNumberType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumberType(CK_GT, v);
        }
        public void SetNumberType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumberType(CK_LT, v);
        }
        public void SetNumberType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNumberType(CK_GE, v);
        }
        public void SetNumberType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regNumberType(CK_LE, v);
        }
        public void SetNumberType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNumberType(), "NUMBER_TYPE");
        }
        public void SetNumberType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNumberType(), "NUMBER_TYPE");
        }
        protected void regNumberType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNumberType(), "NUMBER_TYPE");
        }
        protected abstract ConditionValue getCValueNumberType();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputSettingCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputSettingCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputSettingCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputSettingCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputSettingCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputSettingCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputSettingCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputSettingCB>(delegate(String function, SubQuery<TOutputSettingCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputSettingCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputSettingCB>", subQuery);
            TOutputSettingCB cb = new TOutputSettingCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputSettingCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputSettingCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSettingCB>", subQuery);
            TOutputSettingCB cb = new TOutputSettingCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputSettingCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
