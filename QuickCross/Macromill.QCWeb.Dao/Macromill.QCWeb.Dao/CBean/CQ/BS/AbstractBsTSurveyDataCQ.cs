
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
    public abstract class AbstractBsTSurveyDataCQ : AbstractConditionQuery {

        public AbstractBsTSurveyDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_SURVEY_DATA"; }
        public override String getTableSqlName() { return "T_SURVEY_DATA"; }

        public void SetSampleId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSampleId_Equal(fRES(v));
        }
        protected void DoSetSampleId_Equal(String v) { regSampleId(CK_EQ, v); }
        public void SetSampleId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSampleId_NotEqual(fRES(v));
        }
        protected void DoSetSampleId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_NES, v);
        }
        public void SetSampleId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_GT, fRES(v));
        }
        public void SetSampleId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_LT, fRES(v));
        }
        public void SetSampleId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_GE, fRES(v));
        }
        public void SetSampleId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_LE, fRES(v));
        }
        public void SetSampleId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSampleId(), "SAMPLE_ID");
        }
        public void SetSampleId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSampleId(), "SAMPLE_ID");
        }
        public void SetSampleId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSampleId_LikeSearch(v, cLSOP());
        }
        public void SetSampleId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSampleId(), "SAMPLE_ID", option);
        }
        public void SetSampleId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSampleId(), "SAMPLE_ID", option);
        }
        public void SetSampleId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSampleId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSampleId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSampleId(), "SAMPLE_ID");
        }
        protected abstract ConditionValue getCValueSampleId();

        public void SetMergeCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeCode_Equal(fRES(v));
        }
        protected void DoSetMergeCode_Equal(String v) { regMergeCode(CK_EQ, v); }
        public void SetMergeCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMergeCode_NotEqual(fRES(v));
        }
        protected void DoSetMergeCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_NES, v);
        }
        public void SetMergeCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_GT, fRES(v));
        }
        public void SetMergeCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_LT, fRES(v));
        }
        public void SetMergeCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_GE, fRES(v));
        }
        public void SetMergeCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_LE, fRES(v));
        }
        public void SetMergeCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMergeCode(), "MERGE_CODE");
        }
        public void SetMergeCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMergeCode(), "MERGE_CODE");
        }
        public void SetMergeCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetMergeCode_LikeSearch(v, cLSOP());
        }
        public void SetMergeCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueMergeCode(), "MERGE_CODE", option);
        }
        public void SetMergeCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueMergeCode(), "MERGE_CODE", option);
        }
        public void SetMergeCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMergeCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMergeCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMergeCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMergeCode(), "MERGE_CODE");
        }
        protected abstract ConditionValue getCValueMergeCode();

        public void SetSortNo_Equal(long? v) { regSortNo(CK_EQ, v); }
        public void SetSortNo_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_NES, v);
        }
        public void SetSortNo_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GT, v);
        }
        public void SetSortNo_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_LT, v);
        }
        public void SetSortNo_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GE, v);
        }
        public void SetSortNo_LessEqual(long? v) {
            WhereSetterFlag = true;
            regSortNo(CK_LE, v);
        }
        public void SetSortNo_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueSortNo(), "SORT_NO");
        }
        public void SetSortNo_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueSortNo(), "SORT_NO");
        }
        protected void regSortNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortNo(), "SORT_NO");
        }
        protected abstract ConditionValue getCValueSortNo();

        public void SetDeleteFlag_Equal(int? v) { regDeleteFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of deleteFlag as equal. { = }
        /// はい: 削除を示す
        /// </summary>
        public void SetDeleteFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.True.Code;
            regDeleteFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteFlag as equal. { = }
        /// いいえ: 未削除を示す
        /// </summary>
        public void SetDeleteFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.False.Code;
            regDeleteFlag(CK_EQ, int.Parse(code));
        }
        public void SetDeleteFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of deleteFlag as notEqual. { &lt;&gt; }
        /// はい: 削除を示す
        /// </summary>
        public void SetDeleteFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.True.Code;
            regDeleteFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteFlag as notEqual. { &lt;&gt; }
        /// いいえ: 未削除を示す
        /// </summary>
        public void SetDeleteFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteFlag.False.Code;
            regDeleteFlag(CK_NES, int.Parse(code));
        }
        public void SetDeleteFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDeleteFlag(), "DELETE_FLAG");
        }
        public void SetDeleteFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDeleteFlag(), "DELETE_FLAG");
        }
        public void SetDeleteFlag_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteFlag(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDeleteFlag_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteFlag(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDeleteFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteFlag(), "DELETE_FLAG");
        }
        protected abstract ConditionValue getCValueDeleteFlag();

        public void SetAnswerDate_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_EQ, v);
        }
        public void SetAnswerDate_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_GT, v);
        }
        public void SetAnswerDate_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_LT, v);
        }
        public void SetAnswerDate_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_GE, v);
        }
        public void SetAnswerDate_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_LE, v);
        }
        public void SetAnswerDate_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueAnswerDate(), "ANSWER_DATE", option);
        }
        public void SetAnswerDate_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetAnswerDate_FromTo(from, to, new DateFromToOption());
        }
        public void SetAnswerDate_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAnswerDate_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAnswerDate(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAnswerDate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAnswerDate(), "ANSWER_DATE");
        }
        protected abstract ConditionValue getCValueAnswerDate();

        public void SetSex_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSex_Equal(fRES(v));
        }
        protected void DoSetSex_Equal(String v) { regSex(CK_EQ, v); }
        public void SetSex_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSex_NotEqual(fRES(v));
        }
        protected void DoSetSex_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_NES, v);
        }
        public void SetSex_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_GT, fRES(v));
        }
        public void SetSex_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_LT, fRES(v));
        }
        public void SetSex_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_GE, fRES(v));
        }
        public void SetSex_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_LE, fRES(v));
        }
        public void SetSex_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSex(), "SEX");
        }
        public void SetSex_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSex(), "SEX");
        }
        public void SetSex_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSex_LikeSearch(v, cLSOP());
        }
        public void SetSex_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSex(), "SEX", option);
        }
        public void SetSex_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSex(), "SEX", option);
        }
        public void SetSex_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSex_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSex(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSex(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSex(), "SEX");
        }
        protected abstract ConditionValue getCValueSex();

        public void SetAge_Equal(int? v) { regAge(CK_EQ, v); }
        public void SetAge_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAge(CK_NES, v);
        }
        public void SetAge_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAge(CK_GT, v);
        }
        public void SetAge_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAge(CK_LT, v);
        }
        public void SetAge_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAge(CK_GE, v);
        }
        public void SetAge_LessEqual(int? v) {
            WhereSetterFlag = true;
            regAge(CK_LE, v);
        }
        public void SetAge_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueAge(), "AGE");
        }
        public void SetAge_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueAge(), "AGE");
        }
        public void SetAge_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAge(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAge_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAge(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAge(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAge(), "AGE");
        }
        protected abstract ConditionValue getCValueAge();

        public void SetAgeId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAgeId_Equal(fRES(v));
        }
        protected void DoSetAgeId_Equal(String v) { regAgeId(CK_EQ, v); }
        public void SetAgeId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAgeId_NotEqual(fRES(v));
        }
        protected void DoSetAgeId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_NES, v);
        }
        public void SetAgeId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_GT, fRES(v));
        }
        public void SetAgeId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_LT, fRES(v));
        }
        public void SetAgeId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_GE, fRES(v));
        }
        public void SetAgeId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_LE, fRES(v));
        }
        public void SetAgeId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueAgeId(), "AGE_ID");
        }
        public void SetAgeId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueAgeId(), "AGE_ID");
        }
        public void SetAgeId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetAgeId_LikeSearch(v, cLSOP());
        }
        public void SetAgeId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueAgeId(), "AGE_ID", option);
        }
        public void SetAgeId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueAgeId(), "AGE_ID", option);
        }
        public void SetAgeId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetAgeId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAgeId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regAgeId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAgeId(), "AGE_ID");
        }
        protected abstract ConditionValue getCValueAgeId();

        public void SetPrefecture_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPrefecture_Equal(fRES(v));
        }
        protected void DoSetPrefecture_Equal(String v) { regPrefecture(CK_EQ, v); }
        public void SetPrefecture_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPrefecture_NotEqual(fRES(v));
        }
        protected void DoSetPrefecture_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_NES, v);
        }
        public void SetPrefecture_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_GT, fRES(v));
        }
        public void SetPrefecture_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_LT, fRES(v));
        }
        public void SetPrefecture_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_GE, fRES(v));
        }
        public void SetPrefecture_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_LE, fRES(v));
        }
        public void SetPrefecture_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValuePrefecture(), "PREFECTURE");
        }
        public void SetPrefecture_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValuePrefecture(), "PREFECTURE");
        }
        public void SetPrefecture_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetPrefecture_LikeSearch(v, cLSOP());
        }
        public void SetPrefecture_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValuePrefecture(), "PREFECTURE", option);
        }
        public void SetPrefecture_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValuePrefecture(), "PREFECTURE", option);
        }
        public void SetPrefecture_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPrefecture_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPrefecture(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPrefecture(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePrefecture(), "PREFECTURE");
        }
        protected abstract ConditionValue getCValuePrefecture();

        public void SetArea_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetArea_Equal(fRES(v));
        }
        protected void DoSetArea_Equal(String v) { regArea(CK_EQ, v); }
        public void SetArea_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetArea_NotEqual(fRES(v));
        }
        protected void DoSetArea_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_NES, v);
        }
        public void SetArea_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_GT, fRES(v));
        }
        public void SetArea_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_LT, fRES(v));
        }
        public void SetArea_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_GE, fRES(v));
        }
        public void SetArea_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_LE, fRES(v));
        }
        public void SetArea_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueArea(), "AREA");
        }
        public void SetArea_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueArea(), "AREA");
        }
        public void SetArea_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetArea_LikeSearch(v, cLSOP());
        }
        public void SetArea_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueArea(), "AREA", option);
        }
        public void SetArea_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueArea(), "AREA", option);
        }
        public void SetArea_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_ISN, DUMMY_OBJECT);
        }
        public void SetArea_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regArea(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regArea(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueArea(), "AREA");
        }
        protected abstract ConditionValue getCValueArea();

        public void SetMarried_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMarried_Equal(fRES(v));
        }
        protected void DoSetMarried_Equal(String v) { regMarried(CK_EQ, v); }
        public void SetMarried_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMarried_NotEqual(fRES(v));
        }
        protected void DoSetMarried_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_NES, v);
        }
        public void SetMarried_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_GT, fRES(v));
        }
        public void SetMarried_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_LT, fRES(v));
        }
        public void SetMarried_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_GE, fRES(v));
        }
        public void SetMarried_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_LE, fRES(v));
        }
        public void SetMarried_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMarried(), "MARRIED");
        }
        public void SetMarried_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMarried(), "MARRIED");
        }
        public void SetMarried_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetMarried_LikeSearch(v, cLSOP());
        }
        public void SetMarried_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueMarried(), "MARRIED", option);
        }
        public void SetMarried_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueMarried(), "MARRIED", option);
        }
        public void SetMarried_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMarried_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMarried(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMarried(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMarried(), "MARRIED");
        }
        protected abstract ConditionValue getCValueMarried();

        public void SetChild_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetChild_Equal(fRES(v));
        }
        protected void DoSetChild_Equal(String v) { regChild(CK_EQ, v); }
        public void SetChild_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetChild_NotEqual(fRES(v));
        }
        protected void DoSetChild_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_NES, v);
        }
        public void SetChild_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_GT, fRES(v));
        }
        public void SetChild_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_LT, fRES(v));
        }
        public void SetChild_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_GE, fRES(v));
        }
        public void SetChild_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_LE, fRES(v));
        }
        public void SetChild_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueChild(), "CHILD");
        }
        public void SetChild_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueChild(), "CHILD");
        }
        public void SetChild_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetChild_LikeSearch(v, cLSOP());
        }
        public void SetChild_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueChild(), "CHILD", option);
        }
        public void SetChild_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueChild(), "CHILD", option);
        }
        public void SetChild_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_ISN, DUMMY_OBJECT);
        }
        public void SetChild_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regChild(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regChild(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueChild(), "CHILD");
        }
        protected abstract ConditionValue getCValueChild();

        public void SetHincome_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetHincome_Equal(fRES(v));
        }
        protected void DoSetHincome_Equal(String v) { regHincome(CK_EQ, v); }
        public void SetHincome_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetHincome_NotEqual(fRES(v));
        }
        protected void DoSetHincome_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_NES, v);
        }
        public void SetHincome_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_GT, fRES(v));
        }
        public void SetHincome_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_LT, fRES(v));
        }
        public void SetHincome_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_GE, fRES(v));
        }
        public void SetHincome_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_LE, fRES(v));
        }
        public void SetHincome_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueHincome(), "HINCOME");
        }
        public void SetHincome_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueHincome(), "HINCOME");
        }
        public void SetHincome_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetHincome_LikeSearch(v, cLSOP());
        }
        public void SetHincome_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueHincome(), "HINCOME", option);
        }
        public void SetHincome_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueHincome(), "HINCOME", option);
        }
        public void SetHincome_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_ISN, DUMMY_OBJECT);
        }
        public void SetHincome_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regHincome(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regHincome(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueHincome(), "HINCOME");
        }
        protected abstract ConditionValue getCValueHincome();

        public void SetPincome_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPincome_Equal(fRES(v));
        }
        protected void DoSetPincome_Equal(String v) { regPincome(CK_EQ, v); }
        public void SetPincome_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPincome_NotEqual(fRES(v));
        }
        protected void DoSetPincome_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_NES, v);
        }
        public void SetPincome_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_GT, fRES(v));
        }
        public void SetPincome_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_LT, fRES(v));
        }
        public void SetPincome_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_GE, fRES(v));
        }
        public void SetPincome_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_LE, fRES(v));
        }
        public void SetPincome_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValuePincome(), "PINCOME");
        }
        public void SetPincome_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValuePincome(), "PINCOME");
        }
        public void SetPincome_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetPincome_LikeSearch(v, cLSOP());
        }
        public void SetPincome_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValuePincome(), "PINCOME", option);
        }
        public void SetPincome_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValuePincome(), "PINCOME", option);
        }
        public void SetPincome_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPincome_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPincome(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPincome(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePincome(), "PINCOME");
        }
        protected abstract ConditionValue getCValuePincome();

        public void SetJob_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetJob_Equal(fRES(v));
        }
        protected void DoSetJob_Equal(String v) { regJob(CK_EQ, v); }
        public void SetJob_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetJob_NotEqual(fRES(v));
        }
        protected void DoSetJob_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_NES, v);
        }
        public void SetJob_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_GT, fRES(v));
        }
        public void SetJob_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_LT, fRES(v));
        }
        public void SetJob_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_GE, fRES(v));
        }
        public void SetJob_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_LE, fRES(v));
        }
        public void SetJob_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueJob(), "JOB");
        }
        public void SetJob_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueJob(), "JOB");
        }
        public void SetJob_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetJob_LikeSearch(v, cLSOP());
        }
        public void SetJob_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueJob(), "JOB", option);
        }
        public void SetJob_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueJob(), "JOB", option);
        }
        public void SetJob_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_ISN, DUMMY_OBJECT);
        }
        public void SetJob_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regJob(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regJob(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueJob(), "JOB");
        }
        protected abstract ConditionValue getCValueJob();

        public void SetStudent_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetStudent_Equal(fRES(v));
        }
        protected void DoSetStudent_Equal(String v) { regStudent(CK_EQ, v); }
        public void SetStudent_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetStudent_NotEqual(fRES(v));
        }
        protected void DoSetStudent_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_NES, v);
        }
        public void SetStudent_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_GT, fRES(v));
        }
        public void SetStudent_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_LT, fRES(v));
        }
        public void SetStudent_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_GE, fRES(v));
        }
        public void SetStudent_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_LE, fRES(v));
        }
        public void SetStudent_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueStudent(), "STUDENT");
        }
        public void SetStudent_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueStudent(), "STUDENT");
        }
        public void SetStudent_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetStudent_LikeSearch(v, cLSOP());
        }
        public void SetStudent_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueStudent(), "STUDENT", option);
        }
        public void SetStudent_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueStudent(), "STUDENT", option);
        }
        public void SetStudent_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_ISN, DUMMY_OBJECT);
        }
        public void SetStudent_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStudent(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regStudent(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueStudent(), "STUDENT");
        }
        protected abstract ConditionValue getCValueStudent();

        public void SetCell_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCell_Equal(fRES(v));
        }
        protected void DoSetCell_Equal(String v) { regCell(CK_EQ, v); }
        public void SetCell_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCell_NotEqual(fRES(v));
        }
        protected void DoSetCell_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_NES, v);
        }
        public void SetCell_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_GT, fRES(v));
        }
        public void SetCell_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_LT, fRES(v));
        }
        public void SetCell_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_GE, fRES(v));
        }
        public void SetCell_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_LE, fRES(v));
        }
        public void SetCell_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCell(), "CELL");
        }
        public void SetCell_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCell(), "CELL");
        }
        public void SetCell_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCell_LikeSearch(v, cLSOP());
        }
        public void SetCell_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCell(), "CELL", option);
        }
        public void SetCell_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCell(), "CELL", option);
        }
        public void SetCell_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCell_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCell(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCell(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCell(), "CELL");
        }
        protected abstract ConditionValue getCValueCell();

        public void SetCellName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCellName_Equal(fRES(v));
        }
        protected void DoSetCellName_Equal(String v) { regCellName(CK_EQ, v); }
        public void SetCellName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCellName_NotEqual(fRES(v));
        }
        protected void DoSetCellName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_NES, v);
        }
        public void SetCellName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_GT, fRES(v));
        }
        public void SetCellName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_LT, fRES(v));
        }
        public void SetCellName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_GE, fRES(v));
        }
        public void SetCellName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_LE, fRES(v));
        }
        public void SetCellName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCellName(), "CELL_NAME");
        }
        public void SetCellName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCellName(), "CELL_NAME");
        }
        public void SetCellName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCellName_LikeSearch(v, cLSOP());
        }
        public void SetCellName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCellName(), "CELL_NAME", option);
        }
        public void SetCellName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCellName(), "CELL_NAME", option);
        }
        public void SetCellName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCellName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCellName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCellName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCellName(), "CELL_NAME");
        }
        protected abstract ConditionValue getCValueCellName();

        public void SetQ0001_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0001_Equal(fRES(v));
        }
        protected void DoSetQ0001_Equal(String v) { regQ0001(CK_EQ, v); }
        public void SetQ0001_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0001_NotEqual(fRES(v));
        }
        protected void DoSetQ0001_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_NES, v);
        }
        public void SetQ0001_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_GT, fRES(v));
        }
        public void SetQ0001_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_LT, fRES(v));
        }
        public void SetQ0001_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_GE, fRES(v));
        }
        public void SetQ0001_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_LE, fRES(v));
        }
        public void SetQ0001_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0001(), "Q0001");
        }
        public void SetQ0001_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0001(), "Q0001");
        }
        public void SetQ0001_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0001_LikeSearch(v, cLSOP());
        }
        public void SetQ0001_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0001(), "Q0001", option);
        }
        public void SetQ0001_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0001(), "Q0001", option);
        }
        public void SetQ0001_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0001_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0001(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0001(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0001(), "Q0001");
        }
        protected abstract ConditionValue getCValueQ0001();

        public void SetQ0002_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0002_Equal(fRES(v));
        }
        protected void DoSetQ0002_Equal(String v) { regQ0002(CK_EQ, v); }
        public void SetQ0002_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0002_NotEqual(fRES(v));
        }
        protected void DoSetQ0002_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_NES, v);
        }
        public void SetQ0002_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_GT, fRES(v));
        }
        public void SetQ0002_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_LT, fRES(v));
        }
        public void SetQ0002_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_GE, fRES(v));
        }
        public void SetQ0002_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_LE, fRES(v));
        }
        public void SetQ0002_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0002(), "Q0002");
        }
        public void SetQ0002_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0002(), "Q0002");
        }
        public void SetQ0002_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0002_LikeSearch(v, cLSOP());
        }
        public void SetQ0002_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0002(), "Q0002", option);
        }
        public void SetQ0002_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0002(), "Q0002", option);
        }
        public void SetQ0002_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0002_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0002(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0002(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0002(), "Q0002");
        }
        protected abstract ConditionValue getCValueQ0002();

        public void SetQ0003_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0003_Equal(fRES(v));
        }
        protected void DoSetQ0003_Equal(String v) { regQ0003(CK_EQ, v); }
        public void SetQ0003_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0003_NotEqual(fRES(v));
        }
        protected void DoSetQ0003_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_NES, v);
        }
        public void SetQ0003_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_GT, fRES(v));
        }
        public void SetQ0003_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_LT, fRES(v));
        }
        public void SetQ0003_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_GE, fRES(v));
        }
        public void SetQ0003_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_LE, fRES(v));
        }
        public void SetQ0003_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0003(), "Q0003");
        }
        public void SetQ0003_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0003(), "Q0003");
        }
        public void SetQ0003_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0003_LikeSearch(v, cLSOP());
        }
        public void SetQ0003_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0003(), "Q0003", option);
        }
        public void SetQ0003_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0003(), "Q0003", option);
        }
        public void SetQ0003_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0003_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0003(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0003(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0003(), "Q0003");
        }
        protected abstract ConditionValue getCValueQ0003();

        public void SetQ0004_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0004_Equal(fRES(v));
        }
        protected void DoSetQ0004_Equal(String v) { regQ0004(CK_EQ, v); }
        public void SetQ0004_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0004_NotEqual(fRES(v));
        }
        protected void DoSetQ0004_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_NES, v);
        }
        public void SetQ0004_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_GT, fRES(v));
        }
        public void SetQ0004_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_LT, fRES(v));
        }
        public void SetQ0004_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_GE, fRES(v));
        }
        public void SetQ0004_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_LE, fRES(v));
        }
        public void SetQ0004_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0004(), "Q0004");
        }
        public void SetQ0004_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0004(), "Q0004");
        }
        public void SetQ0004_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0004_LikeSearch(v, cLSOP());
        }
        public void SetQ0004_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0004(), "Q0004", option);
        }
        public void SetQ0004_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0004(), "Q0004", option);
        }
        public void SetQ0004_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0004_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0004(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0004(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0004(), "Q0004");
        }
        protected abstract ConditionValue getCValueQ0004();

        public void SetQ0005_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0005_Equal(fRES(v));
        }
        protected void DoSetQ0005_Equal(String v) { regQ0005(CK_EQ, v); }
        public void SetQ0005_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0005_NotEqual(fRES(v));
        }
        protected void DoSetQ0005_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_NES, v);
        }
        public void SetQ0005_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_GT, fRES(v));
        }
        public void SetQ0005_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_LT, fRES(v));
        }
        public void SetQ0005_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_GE, fRES(v));
        }
        public void SetQ0005_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_LE, fRES(v));
        }
        public void SetQ0005_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0005(), "Q0005");
        }
        public void SetQ0005_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0005(), "Q0005");
        }
        public void SetQ0005_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0005_LikeSearch(v, cLSOP());
        }
        public void SetQ0005_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0005(), "Q0005", option);
        }
        public void SetQ0005_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0005(), "Q0005", option);
        }
        public void SetQ0005_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0005_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0005(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0005(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0005(), "Q0005");
        }
        protected abstract ConditionValue getCValueQ0005();

        public void SetQ0006_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0006_Equal(fRES(v));
        }
        protected void DoSetQ0006_Equal(String v) { regQ0006(CK_EQ, v); }
        public void SetQ0006_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0006_NotEqual(fRES(v));
        }
        protected void DoSetQ0006_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_NES, v);
        }
        public void SetQ0006_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_GT, fRES(v));
        }
        public void SetQ0006_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_LT, fRES(v));
        }
        public void SetQ0006_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_GE, fRES(v));
        }
        public void SetQ0006_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_LE, fRES(v));
        }
        public void SetQ0006_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0006(), "Q0006");
        }
        public void SetQ0006_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0006(), "Q0006");
        }
        public void SetQ0006_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0006_LikeSearch(v, cLSOP());
        }
        public void SetQ0006_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0006(), "Q0006", option);
        }
        public void SetQ0006_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0006(), "Q0006", option);
        }
        public void SetQ0006_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0006_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0006(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0006(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0006(), "Q0006");
        }
        protected abstract ConditionValue getCValueQ0006();

        public void SetQ0007_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0007_Equal(fRES(v));
        }
        protected void DoSetQ0007_Equal(String v) { regQ0007(CK_EQ, v); }
        public void SetQ0007_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0007_NotEqual(fRES(v));
        }
        protected void DoSetQ0007_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_NES, v);
        }
        public void SetQ0007_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_GT, fRES(v));
        }
        public void SetQ0007_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_LT, fRES(v));
        }
        public void SetQ0007_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_GE, fRES(v));
        }
        public void SetQ0007_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_LE, fRES(v));
        }
        public void SetQ0007_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0007(), "Q0007");
        }
        public void SetQ0007_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0007(), "Q0007");
        }
        public void SetQ0007_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0007_LikeSearch(v, cLSOP());
        }
        public void SetQ0007_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0007(), "Q0007", option);
        }
        public void SetQ0007_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0007(), "Q0007", option);
        }
        public void SetQ0007_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0007_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0007(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0007(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0007(), "Q0007");
        }
        protected abstract ConditionValue getCValueQ0007();

        public void SetQ0008_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0008_Equal(fRES(v));
        }
        protected void DoSetQ0008_Equal(String v) { regQ0008(CK_EQ, v); }
        public void SetQ0008_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0008_NotEqual(fRES(v));
        }
        protected void DoSetQ0008_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_NES, v);
        }
        public void SetQ0008_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_GT, fRES(v));
        }
        public void SetQ0008_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_LT, fRES(v));
        }
        public void SetQ0008_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_GE, fRES(v));
        }
        public void SetQ0008_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_LE, fRES(v));
        }
        public void SetQ0008_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0008(), "Q0008");
        }
        public void SetQ0008_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0008(), "Q0008");
        }
        public void SetQ0008_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0008_LikeSearch(v, cLSOP());
        }
        public void SetQ0008_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0008(), "Q0008", option);
        }
        public void SetQ0008_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0008(), "Q0008", option);
        }
        public void SetQ0008_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0008_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0008(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0008(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0008(), "Q0008");
        }
        protected abstract ConditionValue getCValueQ0008();

        public void SetQ0009_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0009_Equal(fRES(v));
        }
        protected void DoSetQ0009_Equal(String v) { regQ0009(CK_EQ, v); }
        public void SetQ0009_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0009_NotEqual(fRES(v));
        }
        protected void DoSetQ0009_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_NES, v);
        }
        public void SetQ0009_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_GT, fRES(v));
        }
        public void SetQ0009_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_LT, fRES(v));
        }
        public void SetQ0009_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_GE, fRES(v));
        }
        public void SetQ0009_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_LE, fRES(v));
        }
        public void SetQ0009_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0009(), "Q0009");
        }
        public void SetQ0009_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0009(), "Q0009");
        }
        public void SetQ0009_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0009_LikeSearch(v, cLSOP());
        }
        public void SetQ0009_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0009(), "Q0009", option);
        }
        public void SetQ0009_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0009(), "Q0009", option);
        }
        public void SetQ0009_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0009_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0009(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0009(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0009(), "Q0009");
        }
        protected abstract ConditionValue getCValueQ0009();

        public void SetQ0010_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0010_Equal(fRES(v));
        }
        protected void DoSetQ0010_Equal(String v) { regQ0010(CK_EQ, v); }
        public void SetQ0010_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0010_NotEqual(fRES(v));
        }
        protected void DoSetQ0010_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_NES, v);
        }
        public void SetQ0010_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_GT, fRES(v));
        }
        public void SetQ0010_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_LT, fRES(v));
        }
        public void SetQ0010_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_GE, fRES(v));
        }
        public void SetQ0010_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_LE, fRES(v));
        }
        public void SetQ0010_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0010(), "Q0010");
        }
        public void SetQ0010_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0010(), "Q0010");
        }
        public void SetQ0010_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0010_LikeSearch(v, cLSOP());
        }
        public void SetQ0010_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0010(), "Q0010", option);
        }
        public void SetQ0010_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0010(), "Q0010", option);
        }
        public void SetQ0010_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0010_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0010(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0010(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0010(), "Q0010");
        }
        protected abstract ConditionValue getCValueQ0010();

        public void SetQ0011_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0011_Equal(fRES(v));
        }
        protected void DoSetQ0011_Equal(String v) { regQ0011(CK_EQ, v); }
        public void SetQ0011_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0011_NotEqual(fRES(v));
        }
        protected void DoSetQ0011_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_NES, v);
        }
        public void SetQ0011_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_GT, fRES(v));
        }
        public void SetQ0011_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_LT, fRES(v));
        }
        public void SetQ0011_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_GE, fRES(v));
        }
        public void SetQ0011_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_LE, fRES(v));
        }
        public void SetQ0011_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0011(), "Q0011");
        }
        public void SetQ0011_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0011(), "Q0011");
        }
        public void SetQ0011_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0011_LikeSearch(v, cLSOP());
        }
        public void SetQ0011_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0011(), "Q0011", option);
        }
        public void SetQ0011_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0011(), "Q0011", option);
        }
        public void SetQ0011_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0011_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0011(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0011(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0011(), "Q0011");
        }
        protected abstract ConditionValue getCValueQ0011();

        public void SetQ0012_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0012_Equal(fRES(v));
        }
        protected void DoSetQ0012_Equal(String v) { regQ0012(CK_EQ, v); }
        public void SetQ0012_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0012_NotEqual(fRES(v));
        }
        protected void DoSetQ0012_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_NES, v);
        }
        public void SetQ0012_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_GT, fRES(v));
        }
        public void SetQ0012_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_LT, fRES(v));
        }
        public void SetQ0012_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_GE, fRES(v));
        }
        public void SetQ0012_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_LE, fRES(v));
        }
        public void SetQ0012_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0012(), "Q0012");
        }
        public void SetQ0012_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0012(), "Q0012");
        }
        public void SetQ0012_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0012_LikeSearch(v, cLSOP());
        }
        public void SetQ0012_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0012(), "Q0012", option);
        }
        public void SetQ0012_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0012(), "Q0012", option);
        }
        public void SetQ0012_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0012_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0012(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0012(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0012(), "Q0012");
        }
        protected abstract ConditionValue getCValueQ0012();

        public void SetQ0013_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0013_Equal(fRES(v));
        }
        protected void DoSetQ0013_Equal(String v) { regQ0013(CK_EQ, v); }
        public void SetQ0013_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0013_NotEqual(fRES(v));
        }
        protected void DoSetQ0013_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_NES, v);
        }
        public void SetQ0013_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_GT, fRES(v));
        }
        public void SetQ0013_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_LT, fRES(v));
        }
        public void SetQ0013_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_GE, fRES(v));
        }
        public void SetQ0013_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_LE, fRES(v));
        }
        public void SetQ0013_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0013(), "Q0013");
        }
        public void SetQ0013_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0013(), "Q0013");
        }
        public void SetQ0013_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0013_LikeSearch(v, cLSOP());
        }
        public void SetQ0013_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0013(), "Q0013", option);
        }
        public void SetQ0013_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0013(), "Q0013", option);
        }
        public void SetQ0013_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0013_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0013(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0013(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0013(), "Q0013");
        }
        protected abstract ConditionValue getCValueQ0013();

        public void SetQ0014_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0014_Equal(fRES(v));
        }
        protected void DoSetQ0014_Equal(String v) { regQ0014(CK_EQ, v); }
        public void SetQ0014_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0014_NotEqual(fRES(v));
        }
        protected void DoSetQ0014_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_NES, v);
        }
        public void SetQ0014_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_GT, fRES(v));
        }
        public void SetQ0014_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_LT, fRES(v));
        }
        public void SetQ0014_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_GE, fRES(v));
        }
        public void SetQ0014_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_LE, fRES(v));
        }
        public void SetQ0014_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0014(), "Q0014");
        }
        public void SetQ0014_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0014(), "Q0014");
        }
        public void SetQ0014_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0014_LikeSearch(v, cLSOP());
        }
        public void SetQ0014_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0014(), "Q0014", option);
        }
        public void SetQ0014_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0014(), "Q0014", option);
        }
        public void SetQ0014_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0014_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0014(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0014(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0014(), "Q0014");
        }
        protected abstract ConditionValue getCValueQ0014();

        public void SetQ0015_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0015_Equal(fRES(v));
        }
        protected void DoSetQ0015_Equal(String v) { regQ0015(CK_EQ, v); }
        public void SetQ0015_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0015_NotEqual(fRES(v));
        }
        protected void DoSetQ0015_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_NES, v);
        }
        public void SetQ0015_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_GT, fRES(v));
        }
        public void SetQ0015_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_LT, fRES(v));
        }
        public void SetQ0015_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_GE, fRES(v));
        }
        public void SetQ0015_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_LE, fRES(v));
        }
        public void SetQ0015_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0015(), "Q0015");
        }
        public void SetQ0015_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0015(), "Q0015");
        }
        public void SetQ0015_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0015_LikeSearch(v, cLSOP());
        }
        public void SetQ0015_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0015(), "Q0015", option);
        }
        public void SetQ0015_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0015(), "Q0015", option);
        }
        public void SetQ0015_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0015_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0015(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0015(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0015(), "Q0015");
        }
        protected abstract ConditionValue getCValueQ0015();

        public void SetQ0016_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0016_Equal(fRES(v));
        }
        protected void DoSetQ0016_Equal(String v) { regQ0016(CK_EQ, v); }
        public void SetQ0016_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0016_NotEqual(fRES(v));
        }
        protected void DoSetQ0016_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_NES, v);
        }
        public void SetQ0016_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_GT, fRES(v));
        }
        public void SetQ0016_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_LT, fRES(v));
        }
        public void SetQ0016_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_GE, fRES(v));
        }
        public void SetQ0016_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_LE, fRES(v));
        }
        public void SetQ0016_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0016(), "Q0016");
        }
        public void SetQ0016_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0016(), "Q0016");
        }
        public void SetQ0016_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0016_LikeSearch(v, cLSOP());
        }
        public void SetQ0016_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0016(), "Q0016", option);
        }
        public void SetQ0016_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0016(), "Q0016", option);
        }
        public void SetQ0016_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0016_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0016(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0016(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0016(), "Q0016");
        }
        protected abstract ConditionValue getCValueQ0016();

        public void SetQ0017_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0017_Equal(fRES(v));
        }
        protected void DoSetQ0017_Equal(String v) { regQ0017(CK_EQ, v); }
        public void SetQ0017_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0017_NotEqual(fRES(v));
        }
        protected void DoSetQ0017_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_NES, v);
        }
        public void SetQ0017_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_GT, fRES(v));
        }
        public void SetQ0017_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_LT, fRES(v));
        }
        public void SetQ0017_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_GE, fRES(v));
        }
        public void SetQ0017_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_LE, fRES(v));
        }
        public void SetQ0017_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0017(), "Q0017");
        }
        public void SetQ0017_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0017(), "Q0017");
        }
        public void SetQ0017_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0017_LikeSearch(v, cLSOP());
        }
        public void SetQ0017_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0017(), "Q0017", option);
        }
        public void SetQ0017_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0017(), "Q0017", option);
        }
        public void SetQ0017_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0017_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0017(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0017(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0017(), "Q0017");
        }
        protected abstract ConditionValue getCValueQ0017();

        public void SetQ0018_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0018_Equal(fRES(v));
        }
        protected void DoSetQ0018_Equal(String v) { regQ0018(CK_EQ, v); }
        public void SetQ0018_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0018_NotEqual(fRES(v));
        }
        protected void DoSetQ0018_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_NES, v);
        }
        public void SetQ0018_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_GT, fRES(v));
        }
        public void SetQ0018_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_LT, fRES(v));
        }
        public void SetQ0018_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_GE, fRES(v));
        }
        public void SetQ0018_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_LE, fRES(v));
        }
        public void SetQ0018_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0018(), "Q0018");
        }
        public void SetQ0018_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0018(), "Q0018");
        }
        public void SetQ0018_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0018_LikeSearch(v, cLSOP());
        }
        public void SetQ0018_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0018(), "Q0018", option);
        }
        public void SetQ0018_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0018(), "Q0018", option);
        }
        public void SetQ0018_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0018_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0018(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0018(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0018(), "Q0018");
        }
        protected abstract ConditionValue getCValueQ0018();

        public void SetQ0019_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0019_Equal(fRES(v));
        }
        protected void DoSetQ0019_Equal(String v) { regQ0019(CK_EQ, v); }
        public void SetQ0019_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0019_NotEqual(fRES(v));
        }
        protected void DoSetQ0019_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_NES, v);
        }
        public void SetQ0019_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_GT, fRES(v));
        }
        public void SetQ0019_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_LT, fRES(v));
        }
        public void SetQ0019_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_GE, fRES(v));
        }
        public void SetQ0019_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_LE, fRES(v));
        }
        public void SetQ0019_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0019(), "Q0019");
        }
        public void SetQ0019_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0019(), "Q0019");
        }
        public void SetQ0019_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0019_LikeSearch(v, cLSOP());
        }
        public void SetQ0019_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0019(), "Q0019", option);
        }
        public void SetQ0019_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0019(), "Q0019", option);
        }
        public void SetQ0019_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0019_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0019(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0019(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0019(), "Q0019");
        }
        protected abstract ConditionValue getCValueQ0019();

        public void SetQ0020_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0020_Equal(fRES(v));
        }
        protected void DoSetQ0020_Equal(String v) { regQ0020(CK_EQ, v); }
        public void SetQ0020_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0020_NotEqual(fRES(v));
        }
        protected void DoSetQ0020_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_NES, v);
        }
        public void SetQ0020_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_GT, fRES(v));
        }
        public void SetQ0020_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_LT, fRES(v));
        }
        public void SetQ0020_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_GE, fRES(v));
        }
        public void SetQ0020_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_LE, fRES(v));
        }
        public void SetQ0020_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0020(), "Q0020");
        }
        public void SetQ0020_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0020(), "Q0020");
        }
        public void SetQ0020_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0020_LikeSearch(v, cLSOP());
        }
        public void SetQ0020_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0020(), "Q0020", option);
        }
        public void SetQ0020_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0020(), "Q0020", option);
        }
        public void SetQ0020_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0020_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0020(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0020(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0020(), "Q0020");
        }
        protected abstract ConditionValue getCValueQ0020();

        public void SetQ0021_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0021_Equal(fRES(v));
        }
        protected void DoSetQ0021_Equal(String v) { regQ0021(CK_EQ, v); }
        public void SetQ0021_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0021_NotEqual(fRES(v));
        }
        protected void DoSetQ0021_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_NES, v);
        }
        public void SetQ0021_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_GT, fRES(v));
        }
        public void SetQ0021_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_LT, fRES(v));
        }
        public void SetQ0021_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_GE, fRES(v));
        }
        public void SetQ0021_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_LE, fRES(v));
        }
        public void SetQ0021_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0021(), "Q0021");
        }
        public void SetQ0021_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0021(), "Q0021");
        }
        public void SetQ0021_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0021_LikeSearch(v, cLSOP());
        }
        public void SetQ0021_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0021(), "Q0021", option);
        }
        public void SetQ0021_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0021(), "Q0021", option);
        }
        public void SetQ0021_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0021_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0021(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0021(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0021(), "Q0021");
        }
        protected abstract ConditionValue getCValueQ0021();

        public void SetQ0022_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0022_Equal(fRES(v));
        }
        protected void DoSetQ0022_Equal(String v) { regQ0022(CK_EQ, v); }
        public void SetQ0022_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0022_NotEqual(fRES(v));
        }
        protected void DoSetQ0022_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_NES, v);
        }
        public void SetQ0022_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_GT, fRES(v));
        }
        public void SetQ0022_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_LT, fRES(v));
        }
        public void SetQ0022_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_GE, fRES(v));
        }
        public void SetQ0022_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_LE, fRES(v));
        }
        public void SetQ0022_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0022(), "Q0022");
        }
        public void SetQ0022_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0022(), "Q0022");
        }
        public void SetQ0022_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0022_LikeSearch(v, cLSOP());
        }
        public void SetQ0022_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0022(), "Q0022", option);
        }
        public void SetQ0022_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0022(), "Q0022", option);
        }
        public void SetQ0022_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0022_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0022(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0022(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0022(), "Q0022");
        }
        protected abstract ConditionValue getCValueQ0022();

        public void SetQ0023_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0023_Equal(fRES(v));
        }
        protected void DoSetQ0023_Equal(String v) { regQ0023(CK_EQ, v); }
        public void SetQ0023_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0023_NotEqual(fRES(v));
        }
        protected void DoSetQ0023_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_NES, v);
        }
        public void SetQ0023_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_GT, fRES(v));
        }
        public void SetQ0023_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_LT, fRES(v));
        }
        public void SetQ0023_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_GE, fRES(v));
        }
        public void SetQ0023_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_LE, fRES(v));
        }
        public void SetQ0023_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0023(), "Q0023");
        }
        public void SetQ0023_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0023(), "Q0023");
        }
        public void SetQ0023_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0023_LikeSearch(v, cLSOP());
        }
        public void SetQ0023_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0023(), "Q0023", option);
        }
        public void SetQ0023_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0023(), "Q0023", option);
        }
        public void SetQ0023_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0023_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0023(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0023(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0023(), "Q0023");
        }
        protected abstract ConditionValue getCValueQ0023();

        public void SetQ0024_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0024_Equal(fRES(v));
        }
        protected void DoSetQ0024_Equal(String v) { regQ0024(CK_EQ, v); }
        public void SetQ0024_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0024_NotEqual(fRES(v));
        }
        protected void DoSetQ0024_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_NES, v);
        }
        public void SetQ0024_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_GT, fRES(v));
        }
        public void SetQ0024_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_LT, fRES(v));
        }
        public void SetQ0024_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_GE, fRES(v));
        }
        public void SetQ0024_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_LE, fRES(v));
        }
        public void SetQ0024_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0024(), "Q0024");
        }
        public void SetQ0024_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0024(), "Q0024");
        }
        public void SetQ0024_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0024_LikeSearch(v, cLSOP());
        }
        public void SetQ0024_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0024(), "Q0024", option);
        }
        public void SetQ0024_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0024(), "Q0024", option);
        }
        public void SetQ0024_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0024_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0024(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0024(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0024(), "Q0024");
        }
        protected abstract ConditionValue getCValueQ0024();

        public void SetQ0025_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0025_Equal(fRES(v));
        }
        protected void DoSetQ0025_Equal(String v) { regQ0025(CK_EQ, v); }
        public void SetQ0025_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0025_NotEqual(fRES(v));
        }
        protected void DoSetQ0025_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_NES, v);
        }
        public void SetQ0025_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_GT, fRES(v));
        }
        public void SetQ0025_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_LT, fRES(v));
        }
        public void SetQ0025_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_GE, fRES(v));
        }
        public void SetQ0025_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_LE, fRES(v));
        }
        public void SetQ0025_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0025(), "Q0025");
        }
        public void SetQ0025_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0025(), "Q0025");
        }
        public void SetQ0025_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0025_LikeSearch(v, cLSOP());
        }
        public void SetQ0025_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0025(), "Q0025", option);
        }
        public void SetQ0025_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0025(), "Q0025", option);
        }
        public void SetQ0025_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0025_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0025(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0025(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0025(), "Q0025");
        }
        protected abstract ConditionValue getCValueQ0025();

        public void SetQ0026_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0026_Equal(fRES(v));
        }
        protected void DoSetQ0026_Equal(String v) { regQ0026(CK_EQ, v); }
        public void SetQ0026_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0026_NotEqual(fRES(v));
        }
        protected void DoSetQ0026_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_NES, v);
        }
        public void SetQ0026_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_GT, fRES(v));
        }
        public void SetQ0026_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_LT, fRES(v));
        }
        public void SetQ0026_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_GE, fRES(v));
        }
        public void SetQ0026_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_LE, fRES(v));
        }
        public void SetQ0026_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0026(), "Q0026");
        }
        public void SetQ0026_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0026(), "Q0026");
        }
        public void SetQ0026_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0026_LikeSearch(v, cLSOP());
        }
        public void SetQ0026_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0026(), "Q0026", option);
        }
        public void SetQ0026_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0026(), "Q0026", option);
        }
        public void SetQ0026_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0026_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0026(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0026(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0026(), "Q0026");
        }
        protected abstract ConditionValue getCValueQ0026();

        public void SetQ0027_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0027_Equal(fRES(v));
        }
        protected void DoSetQ0027_Equal(String v) { regQ0027(CK_EQ, v); }
        public void SetQ0027_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0027_NotEqual(fRES(v));
        }
        protected void DoSetQ0027_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_NES, v);
        }
        public void SetQ0027_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_GT, fRES(v));
        }
        public void SetQ0027_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_LT, fRES(v));
        }
        public void SetQ0027_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_GE, fRES(v));
        }
        public void SetQ0027_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_LE, fRES(v));
        }
        public void SetQ0027_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0027(), "Q0027");
        }
        public void SetQ0027_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0027(), "Q0027");
        }
        public void SetQ0027_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0027_LikeSearch(v, cLSOP());
        }
        public void SetQ0027_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0027(), "Q0027", option);
        }
        public void SetQ0027_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0027(), "Q0027", option);
        }
        public void SetQ0027_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0027_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0027(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0027(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0027(), "Q0027");
        }
        protected abstract ConditionValue getCValueQ0027();

        public void SetQ0028_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0028_Equal(fRES(v));
        }
        protected void DoSetQ0028_Equal(String v) { regQ0028(CK_EQ, v); }
        public void SetQ0028_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0028_NotEqual(fRES(v));
        }
        protected void DoSetQ0028_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_NES, v);
        }
        public void SetQ0028_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_GT, fRES(v));
        }
        public void SetQ0028_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_LT, fRES(v));
        }
        public void SetQ0028_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_GE, fRES(v));
        }
        public void SetQ0028_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_LE, fRES(v));
        }
        public void SetQ0028_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0028(), "Q0028");
        }
        public void SetQ0028_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0028(), "Q0028");
        }
        public void SetQ0028_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0028_LikeSearch(v, cLSOP());
        }
        public void SetQ0028_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0028(), "Q0028", option);
        }
        public void SetQ0028_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0028(), "Q0028", option);
        }
        public void SetQ0028_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0028_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0028(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0028(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0028(), "Q0028");
        }
        protected abstract ConditionValue getCValueQ0028();

        public void SetQ0029_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0029_Equal(fRES(v));
        }
        protected void DoSetQ0029_Equal(String v) { regQ0029(CK_EQ, v); }
        public void SetQ0029_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0029_NotEqual(fRES(v));
        }
        protected void DoSetQ0029_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_NES, v);
        }
        public void SetQ0029_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_GT, fRES(v));
        }
        public void SetQ0029_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_LT, fRES(v));
        }
        public void SetQ0029_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_GE, fRES(v));
        }
        public void SetQ0029_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_LE, fRES(v));
        }
        public void SetQ0029_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0029(), "Q0029");
        }
        public void SetQ0029_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0029(), "Q0029");
        }
        public void SetQ0029_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0029_LikeSearch(v, cLSOP());
        }
        public void SetQ0029_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0029(), "Q0029", option);
        }
        public void SetQ0029_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0029(), "Q0029", option);
        }
        public void SetQ0029_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0029_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0029(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0029(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0029(), "Q0029");
        }
        protected abstract ConditionValue getCValueQ0029();

        public void SetQ0030_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0030_Equal(fRES(v));
        }
        protected void DoSetQ0030_Equal(String v) { regQ0030(CK_EQ, v); }
        public void SetQ0030_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0030_NotEqual(fRES(v));
        }
        protected void DoSetQ0030_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_NES, v);
        }
        public void SetQ0030_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_GT, fRES(v));
        }
        public void SetQ0030_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_LT, fRES(v));
        }
        public void SetQ0030_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_GE, fRES(v));
        }
        public void SetQ0030_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_LE, fRES(v));
        }
        public void SetQ0030_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0030(), "Q0030");
        }
        public void SetQ0030_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0030(), "Q0030");
        }
        public void SetQ0030_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0030_LikeSearch(v, cLSOP());
        }
        public void SetQ0030_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0030(), "Q0030", option);
        }
        public void SetQ0030_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0030(), "Q0030", option);
        }
        public void SetQ0030_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0030_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0030(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0030(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0030(), "Q0030");
        }
        protected abstract ConditionValue getCValueQ0030();

        public void SetQ0031_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0031_Equal(fRES(v));
        }
        protected void DoSetQ0031_Equal(String v) { regQ0031(CK_EQ, v); }
        public void SetQ0031_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0031_NotEqual(fRES(v));
        }
        protected void DoSetQ0031_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_NES, v);
        }
        public void SetQ0031_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_GT, fRES(v));
        }
        public void SetQ0031_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_LT, fRES(v));
        }
        public void SetQ0031_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_GE, fRES(v));
        }
        public void SetQ0031_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_LE, fRES(v));
        }
        public void SetQ0031_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0031(), "Q0031");
        }
        public void SetQ0031_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0031(), "Q0031");
        }
        public void SetQ0031_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0031_LikeSearch(v, cLSOP());
        }
        public void SetQ0031_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0031(), "Q0031", option);
        }
        public void SetQ0031_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0031(), "Q0031", option);
        }
        public void SetQ0031_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0031_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0031(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0031(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0031(), "Q0031");
        }
        protected abstract ConditionValue getCValueQ0031();

        public void SetQ0032_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0032_Equal(fRES(v));
        }
        protected void DoSetQ0032_Equal(String v) { regQ0032(CK_EQ, v); }
        public void SetQ0032_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0032_NotEqual(fRES(v));
        }
        protected void DoSetQ0032_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_NES, v);
        }
        public void SetQ0032_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_GT, fRES(v));
        }
        public void SetQ0032_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_LT, fRES(v));
        }
        public void SetQ0032_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_GE, fRES(v));
        }
        public void SetQ0032_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_LE, fRES(v));
        }
        public void SetQ0032_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0032(), "Q0032");
        }
        public void SetQ0032_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0032(), "Q0032");
        }
        public void SetQ0032_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0032_LikeSearch(v, cLSOP());
        }
        public void SetQ0032_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0032(), "Q0032", option);
        }
        public void SetQ0032_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0032(), "Q0032", option);
        }
        public void SetQ0032_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0032_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0032(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0032(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0032(), "Q0032");
        }
        protected abstract ConditionValue getCValueQ0032();

        public void SetQ0033_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0033_Equal(fRES(v));
        }
        protected void DoSetQ0033_Equal(String v) { regQ0033(CK_EQ, v); }
        public void SetQ0033_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0033_NotEqual(fRES(v));
        }
        protected void DoSetQ0033_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_NES, v);
        }
        public void SetQ0033_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_GT, fRES(v));
        }
        public void SetQ0033_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_LT, fRES(v));
        }
        public void SetQ0033_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_GE, fRES(v));
        }
        public void SetQ0033_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_LE, fRES(v));
        }
        public void SetQ0033_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0033(), "Q0033");
        }
        public void SetQ0033_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0033(), "Q0033");
        }
        public void SetQ0033_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0033_LikeSearch(v, cLSOP());
        }
        public void SetQ0033_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0033(), "Q0033", option);
        }
        public void SetQ0033_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0033(), "Q0033", option);
        }
        public void SetQ0033_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0033_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0033(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0033(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0033(), "Q0033");
        }
        protected abstract ConditionValue getCValueQ0033();

        public void SetQ0034_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0034_Equal(fRES(v));
        }
        protected void DoSetQ0034_Equal(String v) { regQ0034(CK_EQ, v); }
        public void SetQ0034_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0034_NotEqual(fRES(v));
        }
        protected void DoSetQ0034_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_NES, v);
        }
        public void SetQ0034_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_GT, fRES(v));
        }
        public void SetQ0034_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_LT, fRES(v));
        }
        public void SetQ0034_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_GE, fRES(v));
        }
        public void SetQ0034_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_LE, fRES(v));
        }
        public void SetQ0034_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0034(), "Q0034");
        }
        public void SetQ0034_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0034(), "Q0034");
        }
        public void SetQ0034_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0034_LikeSearch(v, cLSOP());
        }
        public void SetQ0034_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0034(), "Q0034", option);
        }
        public void SetQ0034_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0034(), "Q0034", option);
        }
        public void SetQ0034_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0034_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0034(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0034(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0034(), "Q0034");
        }
        protected abstract ConditionValue getCValueQ0034();

        public void SetQ0035_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0035_Equal(fRES(v));
        }
        protected void DoSetQ0035_Equal(String v) { regQ0035(CK_EQ, v); }
        public void SetQ0035_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0035_NotEqual(fRES(v));
        }
        protected void DoSetQ0035_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_NES, v);
        }
        public void SetQ0035_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_GT, fRES(v));
        }
        public void SetQ0035_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_LT, fRES(v));
        }
        public void SetQ0035_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_GE, fRES(v));
        }
        public void SetQ0035_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_LE, fRES(v));
        }
        public void SetQ0035_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0035(), "Q0035");
        }
        public void SetQ0035_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0035(), "Q0035");
        }
        public void SetQ0035_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0035_LikeSearch(v, cLSOP());
        }
        public void SetQ0035_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0035(), "Q0035", option);
        }
        public void SetQ0035_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0035(), "Q0035", option);
        }
        public void SetQ0035_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0035_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0035(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0035(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0035(), "Q0035");
        }
        protected abstract ConditionValue getCValueQ0035();

        public void SetQ0036_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0036_Equal(fRES(v));
        }
        protected void DoSetQ0036_Equal(String v) { regQ0036(CK_EQ, v); }
        public void SetQ0036_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0036_NotEqual(fRES(v));
        }
        protected void DoSetQ0036_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_NES, v);
        }
        public void SetQ0036_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_GT, fRES(v));
        }
        public void SetQ0036_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_LT, fRES(v));
        }
        public void SetQ0036_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_GE, fRES(v));
        }
        public void SetQ0036_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_LE, fRES(v));
        }
        public void SetQ0036_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0036(), "Q0036");
        }
        public void SetQ0036_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0036(), "Q0036");
        }
        public void SetQ0036_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0036_LikeSearch(v, cLSOP());
        }
        public void SetQ0036_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0036(), "Q0036", option);
        }
        public void SetQ0036_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0036(), "Q0036", option);
        }
        public void SetQ0036_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0036_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0036(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0036(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0036(), "Q0036");
        }
        protected abstract ConditionValue getCValueQ0036();

        public void SetQ0037_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0037_Equal(fRES(v));
        }
        protected void DoSetQ0037_Equal(String v) { regQ0037(CK_EQ, v); }
        public void SetQ0037_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0037_NotEqual(fRES(v));
        }
        protected void DoSetQ0037_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_NES, v);
        }
        public void SetQ0037_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_GT, fRES(v));
        }
        public void SetQ0037_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_LT, fRES(v));
        }
        public void SetQ0037_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_GE, fRES(v));
        }
        public void SetQ0037_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_LE, fRES(v));
        }
        public void SetQ0037_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0037(), "Q0037");
        }
        public void SetQ0037_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0037(), "Q0037");
        }
        public void SetQ0037_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0037_LikeSearch(v, cLSOP());
        }
        public void SetQ0037_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0037(), "Q0037", option);
        }
        public void SetQ0037_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0037(), "Q0037", option);
        }
        public void SetQ0037_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0037_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0037(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0037(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0037(), "Q0037");
        }
        protected abstract ConditionValue getCValueQ0037();

        public void SetQ0038_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0038_Equal(fRES(v));
        }
        protected void DoSetQ0038_Equal(String v) { regQ0038(CK_EQ, v); }
        public void SetQ0038_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0038_NotEqual(fRES(v));
        }
        protected void DoSetQ0038_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_NES, v);
        }
        public void SetQ0038_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_GT, fRES(v));
        }
        public void SetQ0038_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_LT, fRES(v));
        }
        public void SetQ0038_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_GE, fRES(v));
        }
        public void SetQ0038_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_LE, fRES(v));
        }
        public void SetQ0038_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0038(), "Q0038");
        }
        public void SetQ0038_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0038(), "Q0038");
        }
        public void SetQ0038_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0038_LikeSearch(v, cLSOP());
        }
        public void SetQ0038_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0038(), "Q0038", option);
        }
        public void SetQ0038_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0038(), "Q0038", option);
        }
        public void SetQ0038_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0038_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0038(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0038(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0038(), "Q0038");
        }
        protected abstract ConditionValue getCValueQ0038();

        public void SetQ0039_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0039_Equal(fRES(v));
        }
        protected void DoSetQ0039_Equal(String v) { regQ0039(CK_EQ, v); }
        public void SetQ0039_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0039_NotEqual(fRES(v));
        }
        protected void DoSetQ0039_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_NES, v);
        }
        public void SetQ0039_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_GT, fRES(v));
        }
        public void SetQ0039_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_LT, fRES(v));
        }
        public void SetQ0039_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_GE, fRES(v));
        }
        public void SetQ0039_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_LE, fRES(v));
        }
        public void SetQ0039_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0039(), "Q0039");
        }
        public void SetQ0039_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0039(), "Q0039");
        }
        public void SetQ0039_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0039_LikeSearch(v, cLSOP());
        }
        public void SetQ0039_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0039(), "Q0039", option);
        }
        public void SetQ0039_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0039(), "Q0039", option);
        }
        public void SetQ0039_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0039_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0039(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0039(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0039(), "Q0039");
        }
        protected abstract ConditionValue getCValueQ0039();

        public void SetQ0040_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0040_Equal(fRES(v));
        }
        protected void DoSetQ0040_Equal(String v) { regQ0040(CK_EQ, v); }
        public void SetQ0040_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0040_NotEqual(fRES(v));
        }
        protected void DoSetQ0040_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_NES, v);
        }
        public void SetQ0040_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_GT, fRES(v));
        }
        public void SetQ0040_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_LT, fRES(v));
        }
        public void SetQ0040_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_GE, fRES(v));
        }
        public void SetQ0040_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_LE, fRES(v));
        }
        public void SetQ0040_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0040(), "Q0040");
        }
        public void SetQ0040_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0040(), "Q0040");
        }
        public void SetQ0040_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0040_LikeSearch(v, cLSOP());
        }
        public void SetQ0040_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0040(), "Q0040", option);
        }
        public void SetQ0040_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0040(), "Q0040", option);
        }
        public void SetQ0040_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0040_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0040(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0040(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0040(), "Q0040");
        }
        protected abstract ConditionValue getCValueQ0040();

        public void SetQ0041_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0041_Equal(fRES(v));
        }
        protected void DoSetQ0041_Equal(String v) { regQ0041(CK_EQ, v); }
        public void SetQ0041_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0041_NotEqual(fRES(v));
        }
        protected void DoSetQ0041_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_NES, v);
        }
        public void SetQ0041_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_GT, fRES(v));
        }
        public void SetQ0041_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_LT, fRES(v));
        }
        public void SetQ0041_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_GE, fRES(v));
        }
        public void SetQ0041_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_LE, fRES(v));
        }
        public void SetQ0041_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0041(), "Q0041");
        }
        public void SetQ0041_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0041(), "Q0041");
        }
        public void SetQ0041_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0041_LikeSearch(v, cLSOP());
        }
        public void SetQ0041_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0041(), "Q0041", option);
        }
        public void SetQ0041_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0041(), "Q0041", option);
        }
        public void SetQ0041_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0041_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0041(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0041(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0041(), "Q0041");
        }
        protected abstract ConditionValue getCValueQ0041();

        public void SetQ0042_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0042_Equal(fRES(v));
        }
        protected void DoSetQ0042_Equal(String v) { regQ0042(CK_EQ, v); }
        public void SetQ0042_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0042_NotEqual(fRES(v));
        }
        protected void DoSetQ0042_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_NES, v);
        }
        public void SetQ0042_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_GT, fRES(v));
        }
        public void SetQ0042_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_LT, fRES(v));
        }
        public void SetQ0042_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_GE, fRES(v));
        }
        public void SetQ0042_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_LE, fRES(v));
        }
        public void SetQ0042_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0042(), "Q0042");
        }
        public void SetQ0042_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0042(), "Q0042");
        }
        public void SetQ0042_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0042_LikeSearch(v, cLSOP());
        }
        public void SetQ0042_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0042(), "Q0042", option);
        }
        public void SetQ0042_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0042(), "Q0042", option);
        }
        public void SetQ0042_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0042_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0042(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0042(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0042(), "Q0042");
        }
        protected abstract ConditionValue getCValueQ0042();

        public void SetQ0043_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0043_Equal(fRES(v));
        }
        protected void DoSetQ0043_Equal(String v) { regQ0043(CK_EQ, v); }
        public void SetQ0043_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0043_NotEqual(fRES(v));
        }
        protected void DoSetQ0043_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_NES, v);
        }
        public void SetQ0043_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_GT, fRES(v));
        }
        public void SetQ0043_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_LT, fRES(v));
        }
        public void SetQ0043_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_GE, fRES(v));
        }
        public void SetQ0043_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_LE, fRES(v));
        }
        public void SetQ0043_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0043(), "Q0043");
        }
        public void SetQ0043_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0043(), "Q0043");
        }
        public void SetQ0043_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0043_LikeSearch(v, cLSOP());
        }
        public void SetQ0043_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0043(), "Q0043", option);
        }
        public void SetQ0043_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0043(), "Q0043", option);
        }
        public void SetQ0043_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0043_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0043(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0043(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0043(), "Q0043");
        }
        protected abstract ConditionValue getCValueQ0043();

        public void SetQ0044_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0044_Equal(fRES(v));
        }
        protected void DoSetQ0044_Equal(String v) { regQ0044(CK_EQ, v); }
        public void SetQ0044_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0044_NotEqual(fRES(v));
        }
        protected void DoSetQ0044_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_NES, v);
        }
        public void SetQ0044_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_GT, fRES(v));
        }
        public void SetQ0044_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_LT, fRES(v));
        }
        public void SetQ0044_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_GE, fRES(v));
        }
        public void SetQ0044_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_LE, fRES(v));
        }
        public void SetQ0044_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0044(), "Q0044");
        }
        public void SetQ0044_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0044(), "Q0044");
        }
        public void SetQ0044_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0044_LikeSearch(v, cLSOP());
        }
        public void SetQ0044_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0044(), "Q0044", option);
        }
        public void SetQ0044_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0044(), "Q0044", option);
        }
        public void SetQ0044_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0044_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0044(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0044(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0044(), "Q0044");
        }
        protected abstract ConditionValue getCValueQ0044();

        public void SetQ0045_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0045_Equal(fRES(v));
        }
        protected void DoSetQ0045_Equal(String v) { regQ0045(CK_EQ, v); }
        public void SetQ0045_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0045_NotEqual(fRES(v));
        }
        protected void DoSetQ0045_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_NES, v);
        }
        public void SetQ0045_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_GT, fRES(v));
        }
        public void SetQ0045_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_LT, fRES(v));
        }
        public void SetQ0045_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_GE, fRES(v));
        }
        public void SetQ0045_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_LE, fRES(v));
        }
        public void SetQ0045_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0045(), "Q0045");
        }
        public void SetQ0045_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0045(), "Q0045");
        }
        public void SetQ0045_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0045_LikeSearch(v, cLSOP());
        }
        public void SetQ0045_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0045(), "Q0045", option);
        }
        public void SetQ0045_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0045(), "Q0045", option);
        }
        public void SetQ0045_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0045_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0045(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0045(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0045(), "Q0045");
        }
        protected abstract ConditionValue getCValueQ0045();

        public void SetQ0046_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0046_Equal(fRES(v));
        }
        protected void DoSetQ0046_Equal(String v) { regQ0046(CK_EQ, v); }
        public void SetQ0046_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0046_NotEqual(fRES(v));
        }
        protected void DoSetQ0046_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_NES, v);
        }
        public void SetQ0046_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_GT, fRES(v));
        }
        public void SetQ0046_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_LT, fRES(v));
        }
        public void SetQ0046_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_GE, fRES(v));
        }
        public void SetQ0046_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_LE, fRES(v));
        }
        public void SetQ0046_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0046(), "Q0046");
        }
        public void SetQ0046_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0046(), "Q0046");
        }
        public void SetQ0046_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0046_LikeSearch(v, cLSOP());
        }
        public void SetQ0046_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0046(), "Q0046", option);
        }
        public void SetQ0046_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0046(), "Q0046", option);
        }
        public void SetQ0046_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0046_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0046(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0046(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0046(), "Q0046");
        }
        protected abstract ConditionValue getCValueQ0046();

        public void SetQ0047_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0047_Equal(fRES(v));
        }
        protected void DoSetQ0047_Equal(String v) { regQ0047(CK_EQ, v); }
        public void SetQ0047_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0047_NotEqual(fRES(v));
        }
        protected void DoSetQ0047_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_NES, v);
        }
        public void SetQ0047_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_GT, fRES(v));
        }
        public void SetQ0047_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_LT, fRES(v));
        }
        public void SetQ0047_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_GE, fRES(v));
        }
        public void SetQ0047_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_LE, fRES(v));
        }
        public void SetQ0047_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0047(), "Q0047");
        }
        public void SetQ0047_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0047(), "Q0047");
        }
        public void SetQ0047_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0047_LikeSearch(v, cLSOP());
        }
        public void SetQ0047_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0047(), "Q0047", option);
        }
        public void SetQ0047_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0047(), "Q0047", option);
        }
        public void SetQ0047_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0047_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0047(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0047(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0047(), "Q0047");
        }
        protected abstract ConditionValue getCValueQ0047();

        public void SetQ0048_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0048_Equal(fRES(v));
        }
        protected void DoSetQ0048_Equal(String v) { regQ0048(CK_EQ, v); }
        public void SetQ0048_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0048_NotEqual(fRES(v));
        }
        protected void DoSetQ0048_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_NES, v);
        }
        public void SetQ0048_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_GT, fRES(v));
        }
        public void SetQ0048_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_LT, fRES(v));
        }
        public void SetQ0048_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_GE, fRES(v));
        }
        public void SetQ0048_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_LE, fRES(v));
        }
        public void SetQ0048_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0048(), "Q0048");
        }
        public void SetQ0048_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0048(), "Q0048");
        }
        public void SetQ0048_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0048_LikeSearch(v, cLSOP());
        }
        public void SetQ0048_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0048(), "Q0048", option);
        }
        public void SetQ0048_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0048(), "Q0048", option);
        }
        public void SetQ0048_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0048_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0048(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0048(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0048(), "Q0048");
        }
        protected abstract ConditionValue getCValueQ0048();

        public void SetQ0049_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0049_Equal(fRES(v));
        }
        protected void DoSetQ0049_Equal(String v) { regQ0049(CK_EQ, v); }
        public void SetQ0049_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0049_NotEqual(fRES(v));
        }
        protected void DoSetQ0049_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_NES, v);
        }
        public void SetQ0049_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_GT, fRES(v));
        }
        public void SetQ0049_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_LT, fRES(v));
        }
        public void SetQ0049_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_GE, fRES(v));
        }
        public void SetQ0049_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_LE, fRES(v));
        }
        public void SetQ0049_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0049(), "Q0049");
        }
        public void SetQ0049_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0049(), "Q0049");
        }
        public void SetQ0049_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0049_LikeSearch(v, cLSOP());
        }
        public void SetQ0049_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0049(), "Q0049", option);
        }
        public void SetQ0049_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0049(), "Q0049", option);
        }
        public void SetQ0049_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0049_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0049(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0049(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0049(), "Q0049");
        }
        protected abstract ConditionValue getCValueQ0049();

        public void SetQ0050_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0050_Equal(fRES(v));
        }
        protected void DoSetQ0050_Equal(String v) { regQ0050(CK_EQ, v); }
        public void SetQ0050_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0050_NotEqual(fRES(v));
        }
        protected void DoSetQ0050_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_NES, v);
        }
        public void SetQ0050_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_GT, fRES(v));
        }
        public void SetQ0050_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_LT, fRES(v));
        }
        public void SetQ0050_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_GE, fRES(v));
        }
        public void SetQ0050_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_LE, fRES(v));
        }
        public void SetQ0050_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0050(), "Q0050");
        }
        public void SetQ0050_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0050(), "Q0050");
        }
        public void SetQ0050_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0050_LikeSearch(v, cLSOP());
        }
        public void SetQ0050_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0050(), "Q0050", option);
        }
        public void SetQ0050_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0050(), "Q0050", option);
        }
        public void SetQ0050_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0050_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0050(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0050(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0050(), "Q0050");
        }
        protected abstract ConditionValue getCValueQ0050();

        public void SetQ0051_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0051_Equal(fRES(v));
        }
        protected void DoSetQ0051_Equal(String v) { regQ0051(CK_EQ, v); }
        public void SetQ0051_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0051_NotEqual(fRES(v));
        }
        protected void DoSetQ0051_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_NES, v);
        }
        public void SetQ0051_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_GT, fRES(v));
        }
        public void SetQ0051_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_LT, fRES(v));
        }
        public void SetQ0051_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_GE, fRES(v));
        }
        public void SetQ0051_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_LE, fRES(v));
        }
        public void SetQ0051_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0051(), "Q0051");
        }
        public void SetQ0051_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0051(), "Q0051");
        }
        public void SetQ0051_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0051_LikeSearch(v, cLSOP());
        }
        public void SetQ0051_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0051(), "Q0051", option);
        }
        public void SetQ0051_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0051(), "Q0051", option);
        }
        public void SetQ0051_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0051_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0051(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0051(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0051(), "Q0051");
        }
        protected abstract ConditionValue getCValueQ0051();

        public void SetQ0052_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0052_Equal(fRES(v));
        }
        protected void DoSetQ0052_Equal(String v) { regQ0052(CK_EQ, v); }
        public void SetQ0052_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0052_NotEqual(fRES(v));
        }
        protected void DoSetQ0052_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_NES, v);
        }
        public void SetQ0052_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_GT, fRES(v));
        }
        public void SetQ0052_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_LT, fRES(v));
        }
        public void SetQ0052_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_GE, fRES(v));
        }
        public void SetQ0052_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_LE, fRES(v));
        }
        public void SetQ0052_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0052(), "Q0052");
        }
        public void SetQ0052_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0052(), "Q0052");
        }
        public void SetQ0052_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0052_LikeSearch(v, cLSOP());
        }
        public void SetQ0052_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0052(), "Q0052", option);
        }
        public void SetQ0052_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0052(), "Q0052", option);
        }
        public void SetQ0052_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0052_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0052(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0052(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0052(), "Q0052");
        }
        protected abstract ConditionValue getCValueQ0052();

        public void SetQ0053_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0053_Equal(fRES(v));
        }
        protected void DoSetQ0053_Equal(String v) { regQ0053(CK_EQ, v); }
        public void SetQ0053_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0053_NotEqual(fRES(v));
        }
        protected void DoSetQ0053_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_NES, v);
        }
        public void SetQ0053_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_GT, fRES(v));
        }
        public void SetQ0053_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_LT, fRES(v));
        }
        public void SetQ0053_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_GE, fRES(v));
        }
        public void SetQ0053_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_LE, fRES(v));
        }
        public void SetQ0053_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0053(), "Q0053");
        }
        public void SetQ0053_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0053(), "Q0053");
        }
        public void SetQ0053_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0053_LikeSearch(v, cLSOP());
        }
        public void SetQ0053_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0053(), "Q0053", option);
        }
        public void SetQ0053_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0053(), "Q0053", option);
        }
        public void SetQ0053_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0053_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0053(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0053(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0053(), "Q0053");
        }
        protected abstract ConditionValue getCValueQ0053();

        public void SetQ0054_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0054_Equal(fRES(v));
        }
        protected void DoSetQ0054_Equal(String v) { regQ0054(CK_EQ, v); }
        public void SetQ0054_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0054_NotEqual(fRES(v));
        }
        protected void DoSetQ0054_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_NES, v);
        }
        public void SetQ0054_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_GT, fRES(v));
        }
        public void SetQ0054_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_LT, fRES(v));
        }
        public void SetQ0054_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_GE, fRES(v));
        }
        public void SetQ0054_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_LE, fRES(v));
        }
        public void SetQ0054_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0054(), "Q0054");
        }
        public void SetQ0054_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0054(), "Q0054");
        }
        public void SetQ0054_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0054_LikeSearch(v, cLSOP());
        }
        public void SetQ0054_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0054(), "Q0054", option);
        }
        public void SetQ0054_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0054(), "Q0054", option);
        }
        public void SetQ0054_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0054_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0054(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0054(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0054(), "Q0054");
        }
        protected abstract ConditionValue getCValueQ0054();

        public void SetQ0055_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0055_Equal(fRES(v));
        }
        protected void DoSetQ0055_Equal(String v) { regQ0055(CK_EQ, v); }
        public void SetQ0055_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0055_NotEqual(fRES(v));
        }
        protected void DoSetQ0055_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_NES, v);
        }
        public void SetQ0055_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_GT, fRES(v));
        }
        public void SetQ0055_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_LT, fRES(v));
        }
        public void SetQ0055_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_GE, fRES(v));
        }
        public void SetQ0055_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_LE, fRES(v));
        }
        public void SetQ0055_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0055(), "Q0055");
        }
        public void SetQ0055_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0055(), "Q0055");
        }
        public void SetQ0055_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0055_LikeSearch(v, cLSOP());
        }
        public void SetQ0055_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0055(), "Q0055", option);
        }
        public void SetQ0055_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0055(), "Q0055", option);
        }
        public void SetQ0055_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0055_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0055(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0055(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0055(), "Q0055");
        }
        protected abstract ConditionValue getCValueQ0055();

        public void SetQ0056_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0056_Equal(fRES(v));
        }
        protected void DoSetQ0056_Equal(String v) { regQ0056(CK_EQ, v); }
        public void SetQ0056_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0056_NotEqual(fRES(v));
        }
        protected void DoSetQ0056_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_NES, v);
        }
        public void SetQ0056_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_GT, fRES(v));
        }
        public void SetQ0056_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_LT, fRES(v));
        }
        public void SetQ0056_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_GE, fRES(v));
        }
        public void SetQ0056_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_LE, fRES(v));
        }
        public void SetQ0056_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0056(), "Q0056");
        }
        public void SetQ0056_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0056(), "Q0056");
        }
        public void SetQ0056_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0056_LikeSearch(v, cLSOP());
        }
        public void SetQ0056_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0056(), "Q0056", option);
        }
        public void SetQ0056_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0056(), "Q0056", option);
        }
        public void SetQ0056_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0056_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0056(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0056(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0056(), "Q0056");
        }
        protected abstract ConditionValue getCValueQ0056();

        public void SetQ0057_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0057_Equal(fRES(v));
        }
        protected void DoSetQ0057_Equal(String v) { regQ0057(CK_EQ, v); }
        public void SetQ0057_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0057_NotEqual(fRES(v));
        }
        protected void DoSetQ0057_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_NES, v);
        }
        public void SetQ0057_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_GT, fRES(v));
        }
        public void SetQ0057_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_LT, fRES(v));
        }
        public void SetQ0057_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_GE, fRES(v));
        }
        public void SetQ0057_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_LE, fRES(v));
        }
        public void SetQ0057_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0057(), "Q0057");
        }
        public void SetQ0057_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0057(), "Q0057");
        }
        public void SetQ0057_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0057_LikeSearch(v, cLSOP());
        }
        public void SetQ0057_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0057(), "Q0057", option);
        }
        public void SetQ0057_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0057(), "Q0057", option);
        }
        public void SetQ0057_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0057_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0057(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0057(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0057(), "Q0057");
        }
        protected abstract ConditionValue getCValueQ0057();

        public void SetQ0058_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0058_Equal(fRES(v));
        }
        protected void DoSetQ0058_Equal(String v) { regQ0058(CK_EQ, v); }
        public void SetQ0058_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0058_NotEqual(fRES(v));
        }
        protected void DoSetQ0058_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_NES, v);
        }
        public void SetQ0058_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_GT, fRES(v));
        }
        public void SetQ0058_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_LT, fRES(v));
        }
        public void SetQ0058_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_GE, fRES(v));
        }
        public void SetQ0058_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_LE, fRES(v));
        }
        public void SetQ0058_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0058(), "Q0058");
        }
        public void SetQ0058_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0058(), "Q0058");
        }
        public void SetQ0058_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0058_LikeSearch(v, cLSOP());
        }
        public void SetQ0058_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0058(), "Q0058", option);
        }
        public void SetQ0058_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0058(), "Q0058", option);
        }
        public void SetQ0058_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0058_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0058(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0058(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0058(), "Q0058");
        }
        protected abstract ConditionValue getCValueQ0058();

        public void SetQ0059_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0059_Equal(fRES(v));
        }
        protected void DoSetQ0059_Equal(String v) { regQ0059(CK_EQ, v); }
        public void SetQ0059_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0059_NotEqual(fRES(v));
        }
        protected void DoSetQ0059_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_NES, v);
        }
        public void SetQ0059_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_GT, fRES(v));
        }
        public void SetQ0059_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_LT, fRES(v));
        }
        public void SetQ0059_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_GE, fRES(v));
        }
        public void SetQ0059_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_LE, fRES(v));
        }
        public void SetQ0059_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0059(), "Q0059");
        }
        public void SetQ0059_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0059(), "Q0059");
        }
        public void SetQ0059_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0059_LikeSearch(v, cLSOP());
        }
        public void SetQ0059_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0059(), "Q0059", option);
        }
        public void SetQ0059_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0059(), "Q0059", option);
        }
        public void SetQ0059_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0059_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0059(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0059(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0059(), "Q0059");
        }
        protected abstract ConditionValue getCValueQ0059();

        public void SetQ0060_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0060_Equal(fRES(v));
        }
        protected void DoSetQ0060_Equal(String v) { regQ0060(CK_EQ, v); }
        public void SetQ0060_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0060_NotEqual(fRES(v));
        }
        protected void DoSetQ0060_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_NES, v);
        }
        public void SetQ0060_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_GT, fRES(v));
        }
        public void SetQ0060_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_LT, fRES(v));
        }
        public void SetQ0060_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_GE, fRES(v));
        }
        public void SetQ0060_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_LE, fRES(v));
        }
        public void SetQ0060_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0060(), "Q0060");
        }
        public void SetQ0060_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0060(), "Q0060");
        }
        public void SetQ0060_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0060_LikeSearch(v, cLSOP());
        }
        public void SetQ0060_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0060(), "Q0060", option);
        }
        public void SetQ0060_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0060(), "Q0060", option);
        }
        public void SetQ0060_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0060_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0060(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0060(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0060(), "Q0060");
        }
        protected abstract ConditionValue getCValueQ0060();

        public void SetQ0061_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0061_Equal(fRES(v));
        }
        protected void DoSetQ0061_Equal(String v) { regQ0061(CK_EQ, v); }
        public void SetQ0061_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0061_NotEqual(fRES(v));
        }
        protected void DoSetQ0061_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_NES, v);
        }
        public void SetQ0061_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_GT, fRES(v));
        }
        public void SetQ0061_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_LT, fRES(v));
        }
        public void SetQ0061_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_GE, fRES(v));
        }
        public void SetQ0061_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_LE, fRES(v));
        }
        public void SetQ0061_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0061(), "Q0061");
        }
        public void SetQ0061_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0061(), "Q0061");
        }
        public void SetQ0061_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0061_LikeSearch(v, cLSOP());
        }
        public void SetQ0061_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0061(), "Q0061", option);
        }
        public void SetQ0061_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0061(), "Q0061", option);
        }
        public void SetQ0061_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0061_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0061(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0061(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0061(), "Q0061");
        }
        protected abstract ConditionValue getCValueQ0061();

        public void SetQ0062_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0062_Equal(fRES(v));
        }
        protected void DoSetQ0062_Equal(String v) { regQ0062(CK_EQ, v); }
        public void SetQ0062_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0062_NotEqual(fRES(v));
        }
        protected void DoSetQ0062_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_NES, v);
        }
        public void SetQ0062_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_GT, fRES(v));
        }
        public void SetQ0062_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_LT, fRES(v));
        }
        public void SetQ0062_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_GE, fRES(v));
        }
        public void SetQ0062_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_LE, fRES(v));
        }
        public void SetQ0062_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0062(), "Q0062");
        }
        public void SetQ0062_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0062(), "Q0062");
        }
        public void SetQ0062_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0062_LikeSearch(v, cLSOP());
        }
        public void SetQ0062_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0062(), "Q0062", option);
        }
        public void SetQ0062_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0062(), "Q0062", option);
        }
        public void SetQ0062_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0062_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0062(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0062(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0062(), "Q0062");
        }
        protected abstract ConditionValue getCValueQ0062();

        public void SetQ0063_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0063_Equal(fRES(v));
        }
        protected void DoSetQ0063_Equal(String v) { regQ0063(CK_EQ, v); }
        public void SetQ0063_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0063_NotEqual(fRES(v));
        }
        protected void DoSetQ0063_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_NES, v);
        }
        public void SetQ0063_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_GT, fRES(v));
        }
        public void SetQ0063_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_LT, fRES(v));
        }
        public void SetQ0063_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_GE, fRES(v));
        }
        public void SetQ0063_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_LE, fRES(v));
        }
        public void SetQ0063_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0063(), "Q0063");
        }
        public void SetQ0063_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0063(), "Q0063");
        }
        public void SetQ0063_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0063_LikeSearch(v, cLSOP());
        }
        public void SetQ0063_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0063(), "Q0063", option);
        }
        public void SetQ0063_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0063(), "Q0063", option);
        }
        public void SetQ0063_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0063_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0063(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0063(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0063(), "Q0063");
        }
        protected abstract ConditionValue getCValueQ0063();

        public void SetQ0064_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0064_Equal(fRES(v));
        }
        protected void DoSetQ0064_Equal(String v) { regQ0064(CK_EQ, v); }
        public void SetQ0064_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0064_NotEqual(fRES(v));
        }
        protected void DoSetQ0064_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_NES, v);
        }
        public void SetQ0064_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_GT, fRES(v));
        }
        public void SetQ0064_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_LT, fRES(v));
        }
        public void SetQ0064_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_GE, fRES(v));
        }
        public void SetQ0064_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_LE, fRES(v));
        }
        public void SetQ0064_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0064(), "Q0064");
        }
        public void SetQ0064_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0064(), "Q0064");
        }
        public void SetQ0064_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0064_LikeSearch(v, cLSOP());
        }
        public void SetQ0064_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0064(), "Q0064", option);
        }
        public void SetQ0064_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0064(), "Q0064", option);
        }
        public void SetQ0064_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0064_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0064(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0064(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0064(), "Q0064");
        }
        protected abstract ConditionValue getCValueQ0064();

        public void SetQ0065_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0065_Equal(fRES(v));
        }
        protected void DoSetQ0065_Equal(String v) { regQ0065(CK_EQ, v); }
        public void SetQ0065_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0065_NotEqual(fRES(v));
        }
        protected void DoSetQ0065_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_NES, v);
        }
        public void SetQ0065_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_GT, fRES(v));
        }
        public void SetQ0065_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_LT, fRES(v));
        }
        public void SetQ0065_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_GE, fRES(v));
        }
        public void SetQ0065_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_LE, fRES(v));
        }
        public void SetQ0065_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0065(), "Q0065");
        }
        public void SetQ0065_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0065(), "Q0065");
        }
        public void SetQ0065_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0065_LikeSearch(v, cLSOP());
        }
        public void SetQ0065_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0065(), "Q0065", option);
        }
        public void SetQ0065_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0065(), "Q0065", option);
        }
        public void SetQ0065_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0065_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0065(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0065(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0065(), "Q0065");
        }
        protected abstract ConditionValue getCValueQ0065();

        public void SetQ0066_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0066_Equal(fRES(v));
        }
        protected void DoSetQ0066_Equal(String v) { regQ0066(CK_EQ, v); }
        public void SetQ0066_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0066_NotEqual(fRES(v));
        }
        protected void DoSetQ0066_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_NES, v);
        }
        public void SetQ0066_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_GT, fRES(v));
        }
        public void SetQ0066_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_LT, fRES(v));
        }
        public void SetQ0066_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_GE, fRES(v));
        }
        public void SetQ0066_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_LE, fRES(v));
        }
        public void SetQ0066_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0066(), "Q0066");
        }
        public void SetQ0066_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0066(), "Q0066");
        }
        public void SetQ0066_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0066_LikeSearch(v, cLSOP());
        }
        public void SetQ0066_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0066(), "Q0066", option);
        }
        public void SetQ0066_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0066(), "Q0066", option);
        }
        public void SetQ0066_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0066_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0066(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0066(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0066(), "Q0066");
        }
        protected abstract ConditionValue getCValueQ0066();

        public void SetQ0067_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0067_Equal(fRES(v));
        }
        protected void DoSetQ0067_Equal(String v) { regQ0067(CK_EQ, v); }
        public void SetQ0067_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0067_NotEqual(fRES(v));
        }
        protected void DoSetQ0067_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_NES, v);
        }
        public void SetQ0067_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_GT, fRES(v));
        }
        public void SetQ0067_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_LT, fRES(v));
        }
        public void SetQ0067_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_GE, fRES(v));
        }
        public void SetQ0067_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_LE, fRES(v));
        }
        public void SetQ0067_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0067(), "Q0067");
        }
        public void SetQ0067_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0067(), "Q0067");
        }
        public void SetQ0067_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0067_LikeSearch(v, cLSOP());
        }
        public void SetQ0067_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0067(), "Q0067", option);
        }
        public void SetQ0067_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0067(), "Q0067", option);
        }
        public void SetQ0067_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0067_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0067(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0067(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0067(), "Q0067");
        }
        protected abstract ConditionValue getCValueQ0067();

        public void SetQ0068_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0068_Equal(fRES(v));
        }
        protected void DoSetQ0068_Equal(String v) { regQ0068(CK_EQ, v); }
        public void SetQ0068_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0068_NotEqual(fRES(v));
        }
        protected void DoSetQ0068_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_NES, v);
        }
        public void SetQ0068_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_GT, fRES(v));
        }
        public void SetQ0068_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_LT, fRES(v));
        }
        public void SetQ0068_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_GE, fRES(v));
        }
        public void SetQ0068_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_LE, fRES(v));
        }
        public void SetQ0068_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0068(), "Q0068");
        }
        public void SetQ0068_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0068(), "Q0068");
        }
        public void SetQ0068_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0068_LikeSearch(v, cLSOP());
        }
        public void SetQ0068_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0068(), "Q0068", option);
        }
        public void SetQ0068_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0068(), "Q0068", option);
        }
        public void SetQ0068_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0068_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0068(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0068(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0068(), "Q0068");
        }
        protected abstract ConditionValue getCValueQ0068();

        public void SetQ0069_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0069_Equal(fRES(v));
        }
        protected void DoSetQ0069_Equal(String v) { regQ0069(CK_EQ, v); }
        public void SetQ0069_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0069_NotEqual(fRES(v));
        }
        protected void DoSetQ0069_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_NES, v);
        }
        public void SetQ0069_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_GT, fRES(v));
        }
        public void SetQ0069_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_LT, fRES(v));
        }
        public void SetQ0069_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_GE, fRES(v));
        }
        public void SetQ0069_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_LE, fRES(v));
        }
        public void SetQ0069_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0069(), "Q0069");
        }
        public void SetQ0069_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0069(), "Q0069");
        }
        public void SetQ0069_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0069_LikeSearch(v, cLSOP());
        }
        public void SetQ0069_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0069(), "Q0069", option);
        }
        public void SetQ0069_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0069(), "Q0069", option);
        }
        public void SetQ0069_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0069_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0069(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0069(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0069(), "Q0069");
        }
        protected abstract ConditionValue getCValueQ0069();

        public void SetQ0070_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0070_Equal(fRES(v));
        }
        protected void DoSetQ0070_Equal(String v) { regQ0070(CK_EQ, v); }
        public void SetQ0070_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0070_NotEqual(fRES(v));
        }
        protected void DoSetQ0070_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_NES, v);
        }
        public void SetQ0070_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_GT, fRES(v));
        }
        public void SetQ0070_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_LT, fRES(v));
        }
        public void SetQ0070_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_GE, fRES(v));
        }
        public void SetQ0070_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_LE, fRES(v));
        }
        public void SetQ0070_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0070(), "Q0070");
        }
        public void SetQ0070_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0070(), "Q0070");
        }
        public void SetQ0070_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0070_LikeSearch(v, cLSOP());
        }
        public void SetQ0070_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0070(), "Q0070", option);
        }
        public void SetQ0070_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0070(), "Q0070", option);
        }
        public void SetQ0070_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0070_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0070(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0070(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0070(), "Q0070");
        }
        protected abstract ConditionValue getCValueQ0070();

        public void SetQ0071_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0071_Equal(fRES(v));
        }
        protected void DoSetQ0071_Equal(String v) { regQ0071(CK_EQ, v); }
        public void SetQ0071_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0071_NotEqual(fRES(v));
        }
        protected void DoSetQ0071_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_NES, v);
        }
        public void SetQ0071_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_GT, fRES(v));
        }
        public void SetQ0071_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_LT, fRES(v));
        }
        public void SetQ0071_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_GE, fRES(v));
        }
        public void SetQ0071_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_LE, fRES(v));
        }
        public void SetQ0071_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0071(), "Q0071");
        }
        public void SetQ0071_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0071(), "Q0071");
        }
        public void SetQ0071_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0071_LikeSearch(v, cLSOP());
        }
        public void SetQ0071_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0071(), "Q0071", option);
        }
        public void SetQ0071_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0071(), "Q0071", option);
        }
        public void SetQ0071_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0071_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0071(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0071(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0071(), "Q0071");
        }
        protected abstract ConditionValue getCValueQ0071();

        public void SetQ0072_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0072_Equal(fRES(v));
        }
        protected void DoSetQ0072_Equal(String v) { regQ0072(CK_EQ, v); }
        public void SetQ0072_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0072_NotEqual(fRES(v));
        }
        protected void DoSetQ0072_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_NES, v);
        }
        public void SetQ0072_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_GT, fRES(v));
        }
        public void SetQ0072_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_LT, fRES(v));
        }
        public void SetQ0072_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_GE, fRES(v));
        }
        public void SetQ0072_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_LE, fRES(v));
        }
        public void SetQ0072_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0072(), "Q0072");
        }
        public void SetQ0072_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0072(), "Q0072");
        }
        public void SetQ0072_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0072_LikeSearch(v, cLSOP());
        }
        public void SetQ0072_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0072(), "Q0072", option);
        }
        public void SetQ0072_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0072(), "Q0072", option);
        }
        public void SetQ0072_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0072_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0072(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0072(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0072(), "Q0072");
        }
        protected abstract ConditionValue getCValueQ0072();

        public void SetQ0073_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0073_Equal(fRES(v));
        }
        protected void DoSetQ0073_Equal(String v) { regQ0073(CK_EQ, v); }
        public void SetQ0073_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0073_NotEqual(fRES(v));
        }
        protected void DoSetQ0073_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_NES, v);
        }
        public void SetQ0073_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_GT, fRES(v));
        }
        public void SetQ0073_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_LT, fRES(v));
        }
        public void SetQ0073_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_GE, fRES(v));
        }
        public void SetQ0073_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_LE, fRES(v));
        }
        public void SetQ0073_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0073(), "Q0073");
        }
        public void SetQ0073_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0073(), "Q0073");
        }
        public void SetQ0073_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0073_LikeSearch(v, cLSOP());
        }
        public void SetQ0073_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0073(), "Q0073", option);
        }
        public void SetQ0073_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0073(), "Q0073", option);
        }
        public void SetQ0073_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0073_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0073(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0073(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0073(), "Q0073");
        }
        protected abstract ConditionValue getCValueQ0073();

        public void SetQ0074_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0074_Equal(fRES(v));
        }
        protected void DoSetQ0074_Equal(String v) { regQ0074(CK_EQ, v); }
        public void SetQ0074_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0074_NotEqual(fRES(v));
        }
        protected void DoSetQ0074_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_NES, v);
        }
        public void SetQ0074_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_GT, fRES(v));
        }
        public void SetQ0074_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_LT, fRES(v));
        }
        public void SetQ0074_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_GE, fRES(v));
        }
        public void SetQ0074_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_LE, fRES(v));
        }
        public void SetQ0074_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0074(), "Q0074");
        }
        public void SetQ0074_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0074(), "Q0074");
        }
        public void SetQ0074_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0074_LikeSearch(v, cLSOP());
        }
        public void SetQ0074_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0074(), "Q0074", option);
        }
        public void SetQ0074_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0074(), "Q0074", option);
        }
        public void SetQ0074_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0074_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0074(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0074(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0074(), "Q0074");
        }
        protected abstract ConditionValue getCValueQ0074();

        public void SetQ0075_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0075_Equal(fRES(v));
        }
        protected void DoSetQ0075_Equal(String v) { regQ0075(CK_EQ, v); }
        public void SetQ0075_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0075_NotEqual(fRES(v));
        }
        protected void DoSetQ0075_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_NES, v);
        }
        public void SetQ0075_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_GT, fRES(v));
        }
        public void SetQ0075_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_LT, fRES(v));
        }
        public void SetQ0075_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_GE, fRES(v));
        }
        public void SetQ0075_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_LE, fRES(v));
        }
        public void SetQ0075_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0075(), "Q0075");
        }
        public void SetQ0075_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0075(), "Q0075");
        }
        public void SetQ0075_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0075_LikeSearch(v, cLSOP());
        }
        public void SetQ0075_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0075(), "Q0075", option);
        }
        public void SetQ0075_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0075(), "Q0075", option);
        }
        public void SetQ0075_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0075_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0075(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0075(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0075(), "Q0075");
        }
        protected abstract ConditionValue getCValueQ0075();

        public void SetQ0076_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0076_Equal(fRES(v));
        }
        protected void DoSetQ0076_Equal(String v) { regQ0076(CK_EQ, v); }
        public void SetQ0076_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0076_NotEqual(fRES(v));
        }
        protected void DoSetQ0076_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_NES, v);
        }
        public void SetQ0076_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_GT, fRES(v));
        }
        public void SetQ0076_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_LT, fRES(v));
        }
        public void SetQ0076_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_GE, fRES(v));
        }
        public void SetQ0076_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_LE, fRES(v));
        }
        public void SetQ0076_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0076(), "Q0076");
        }
        public void SetQ0076_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0076(), "Q0076");
        }
        public void SetQ0076_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0076_LikeSearch(v, cLSOP());
        }
        public void SetQ0076_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0076(), "Q0076", option);
        }
        public void SetQ0076_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0076(), "Q0076", option);
        }
        public void SetQ0076_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0076_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0076(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0076(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0076(), "Q0076");
        }
        protected abstract ConditionValue getCValueQ0076();

        public void SetQ0077_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0077_Equal(fRES(v));
        }
        protected void DoSetQ0077_Equal(String v) { regQ0077(CK_EQ, v); }
        public void SetQ0077_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0077_NotEqual(fRES(v));
        }
        protected void DoSetQ0077_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_NES, v);
        }
        public void SetQ0077_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_GT, fRES(v));
        }
        public void SetQ0077_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_LT, fRES(v));
        }
        public void SetQ0077_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_GE, fRES(v));
        }
        public void SetQ0077_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_LE, fRES(v));
        }
        public void SetQ0077_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0077(), "Q0077");
        }
        public void SetQ0077_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0077(), "Q0077");
        }
        public void SetQ0077_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0077_LikeSearch(v, cLSOP());
        }
        public void SetQ0077_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0077(), "Q0077", option);
        }
        public void SetQ0077_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0077(), "Q0077", option);
        }
        public void SetQ0077_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0077_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0077(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0077(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0077(), "Q0077");
        }
        protected abstract ConditionValue getCValueQ0077();

        public void SetQ0078_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0078_Equal(fRES(v));
        }
        protected void DoSetQ0078_Equal(String v) { regQ0078(CK_EQ, v); }
        public void SetQ0078_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0078_NotEqual(fRES(v));
        }
        protected void DoSetQ0078_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_NES, v);
        }
        public void SetQ0078_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_GT, fRES(v));
        }
        public void SetQ0078_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_LT, fRES(v));
        }
        public void SetQ0078_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_GE, fRES(v));
        }
        public void SetQ0078_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_LE, fRES(v));
        }
        public void SetQ0078_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0078(), "Q0078");
        }
        public void SetQ0078_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0078(), "Q0078");
        }
        public void SetQ0078_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0078_LikeSearch(v, cLSOP());
        }
        public void SetQ0078_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0078(), "Q0078", option);
        }
        public void SetQ0078_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0078(), "Q0078", option);
        }
        public void SetQ0078_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0078_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0078(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0078(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0078(), "Q0078");
        }
        protected abstract ConditionValue getCValueQ0078();

        public void SetQ0079_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0079_Equal(fRES(v));
        }
        protected void DoSetQ0079_Equal(String v) { regQ0079(CK_EQ, v); }
        public void SetQ0079_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0079_NotEqual(fRES(v));
        }
        protected void DoSetQ0079_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_NES, v);
        }
        public void SetQ0079_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_GT, fRES(v));
        }
        public void SetQ0079_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_LT, fRES(v));
        }
        public void SetQ0079_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_GE, fRES(v));
        }
        public void SetQ0079_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_LE, fRES(v));
        }
        public void SetQ0079_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0079(), "Q0079");
        }
        public void SetQ0079_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0079(), "Q0079");
        }
        public void SetQ0079_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0079_LikeSearch(v, cLSOP());
        }
        public void SetQ0079_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0079(), "Q0079", option);
        }
        public void SetQ0079_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0079(), "Q0079", option);
        }
        public void SetQ0079_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0079_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0079(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0079(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0079(), "Q0079");
        }
        protected abstract ConditionValue getCValueQ0079();

        public void SetQ0080_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0080_Equal(fRES(v));
        }
        protected void DoSetQ0080_Equal(String v) { regQ0080(CK_EQ, v); }
        public void SetQ0080_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0080_NotEqual(fRES(v));
        }
        protected void DoSetQ0080_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_NES, v);
        }
        public void SetQ0080_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_GT, fRES(v));
        }
        public void SetQ0080_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_LT, fRES(v));
        }
        public void SetQ0080_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_GE, fRES(v));
        }
        public void SetQ0080_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_LE, fRES(v));
        }
        public void SetQ0080_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0080(), "Q0080");
        }
        public void SetQ0080_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0080(), "Q0080");
        }
        public void SetQ0080_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0080_LikeSearch(v, cLSOP());
        }
        public void SetQ0080_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0080(), "Q0080", option);
        }
        public void SetQ0080_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0080(), "Q0080", option);
        }
        public void SetQ0080_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0080_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0080(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0080(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0080(), "Q0080");
        }
        protected abstract ConditionValue getCValueQ0080();

        public void SetQ0081_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0081_Equal(fRES(v));
        }
        protected void DoSetQ0081_Equal(String v) { regQ0081(CK_EQ, v); }
        public void SetQ0081_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0081_NotEqual(fRES(v));
        }
        protected void DoSetQ0081_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_NES, v);
        }
        public void SetQ0081_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_GT, fRES(v));
        }
        public void SetQ0081_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_LT, fRES(v));
        }
        public void SetQ0081_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_GE, fRES(v));
        }
        public void SetQ0081_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_LE, fRES(v));
        }
        public void SetQ0081_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0081(), "Q0081");
        }
        public void SetQ0081_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0081(), "Q0081");
        }
        public void SetQ0081_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0081_LikeSearch(v, cLSOP());
        }
        public void SetQ0081_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0081(), "Q0081", option);
        }
        public void SetQ0081_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0081(), "Q0081", option);
        }
        public void SetQ0081_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0081_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0081(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0081(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0081(), "Q0081");
        }
        protected abstract ConditionValue getCValueQ0081();

        public void SetQ0082_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0082_Equal(fRES(v));
        }
        protected void DoSetQ0082_Equal(String v) { regQ0082(CK_EQ, v); }
        public void SetQ0082_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0082_NotEqual(fRES(v));
        }
        protected void DoSetQ0082_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_NES, v);
        }
        public void SetQ0082_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_GT, fRES(v));
        }
        public void SetQ0082_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_LT, fRES(v));
        }
        public void SetQ0082_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_GE, fRES(v));
        }
        public void SetQ0082_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_LE, fRES(v));
        }
        public void SetQ0082_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0082(), "Q0082");
        }
        public void SetQ0082_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0082(), "Q0082");
        }
        public void SetQ0082_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0082_LikeSearch(v, cLSOP());
        }
        public void SetQ0082_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0082(), "Q0082", option);
        }
        public void SetQ0082_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0082(), "Q0082", option);
        }
        public void SetQ0082_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0082_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0082(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0082(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0082(), "Q0082");
        }
        protected abstract ConditionValue getCValueQ0082();

        public void SetQ0083_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0083_Equal(fRES(v));
        }
        protected void DoSetQ0083_Equal(String v) { regQ0083(CK_EQ, v); }
        public void SetQ0083_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0083_NotEqual(fRES(v));
        }
        protected void DoSetQ0083_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_NES, v);
        }
        public void SetQ0083_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_GT, fRES(v));
        }
        public void SetQ0083_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_LT, fRES(v));
        }
        public void SetQ0083_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_GE, fRES(v));
        }
        public void SetQ0083_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_LE, fRES(v));
        }
        public void SetQ0083_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0083(), "Q0083");
        }
        public void SetQ0083_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0083(), "Q0083");
        }
        public void SetQ0083_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0083_LikeSearch(v, cLSOP());
        }
        public void SetQ0083_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0083(), "Q0083", option);
        }
        public void SetQ0083_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0083(), "Q0083", option);
        }
        public void SetQ0083_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0083_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0083(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0083(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0083(), "Q0083");
        }
        protected abstract ConditionValue getCValueQ0083();

        public void SetQ0084_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0084_Equal(fRES(v));
        }
        protected void DoSetQ0084_Equal(String v) { regQ0084(CK_EQ, v); }
        public void SetQ0084_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0084_NotEqual(fRES(v));
        }
        protected void DoSetQ0084_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_NES, v);
        }
        public void SetQ0084_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_GT, fRES(v));
        }
        public void SetQ0084_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_LT, fRES(v));
        }
        public void SetQ0084_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_GE, fRES(v));
        }
        public void SetQ0084_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_LE, fRES(v));
        }
        public void SetQ0084_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0084(), "Q0084");
        }
        public void SetQ0084_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0084(), "Q0084");
        }
        public void SetQ0084_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0084_LikeSearch(v, cLSOP());
        }
        public void SetQ0084_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0084(), "Q0084", option);
        }
        public void SetQ0084_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0084(), "Q0084", option);
        }
        public void SetQ0084_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0084_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0084(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0084(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0084(), "Q0084");
        }
        protected abstract ConditionValue getCValueQ0084();

        public void SetQ0085_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0085_Equal(fRES(v));
        }
        protected void DoSetQ0085_Equal(String v) { regQ0085(CK_EQ, v); }
        public void SetQ0085_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0085_NotEqual(fRES(v));
        }
        protected void DoSetQ0085_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_NES, v);
        }
        public void SetQ0085_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_GT, fRES(v));
        }
        public void SetQ0085_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_LT, fRES(v));
        }
        public void SetQ0085_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_GE, fRES(v));
        }
        public void SetQ0085_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_LE, fRES(v));
        }
        public void SetQ0085_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0085(), "Q0085");
        }
        public void SetQ0085_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0085(), "Q0085");
        }
        public void SetQ0085_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0085_LikeSearch(v, cLSOP());
        }
        public void SetQ0085_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0085(), "Q0085", option);
        }
        public void SetQ0085_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0085(), "Q0085", option);
        }
        public void SetQ0085_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0085_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0085(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0085(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0085(), "Q0085");
        }
        protected abstract ConditionValue getCValueQ0085();

        public void SetQ0086_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0086_Equal(fRES(v));
        }
        protected void DoSetQ0086_Equal(String v) { regQ0086(CK_EQ, v); }
        public void SetQ0086_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0086_NotEqual(fRES(v));
        }
        protected void DoSetQ0086_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_NES, v);
        }
        public void SetQ0086_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_GT, fRES(v));
        }
        public void SetQ0086_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_LT, fRES(v));
        }
        public void SetQ0086_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_GE, fRES(v));
        }
        public void SetQ0086_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_LE, fRES(v));
        }
        public void SetQ0086_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0086(), "Q0086");
        }
        public void SetQ0086_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0086(), "Q0086");
        }
        public void SetQ0086_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0086_LikeSearch(v, cLSOP());
        }
        public void SetQ0086_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0086(), "Q0086", option);
        }
        public void SetQ0086_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0086(), "Q0086", option);
        }
        public void SetQ0086_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0086_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0086(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0086(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0086(), "Q0086");
        }
        protected abstract ConditionValue getCValueQ0086();

        public void SetQ0087_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0087_Equal(fRES(v));
        }
        protected void DoSetQ0087_Equal(String v) { regQ0087(CK_EQ, v); }
        public void SetQ0087_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0087_NotEqual(fRES(v));
        }
        protected void DoSetQ0087_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_NES, v);
        }
        public void SetQ0087_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_GT, fRES(v));
        }
        public void SetQ0087_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_LT, fRES(v));
        }
        public void SetQ0087_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_GE, fRES(v));
        }
        public void SetQ0087_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_LE, fRES(v));
        }
        public void SetQ0087_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0087(), "Q0087");
        }
        public void SetQ0087_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0087(), "Q0087");
        }
        public void SetQ0087_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0087_LikeSearch(v, cLSOP());
        }
        public void SetQ0087_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0087(), "Q0087", option);
        }
        public void SetQ0087_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0087(), "Q0087", option);
        }
        public void SetQ0087_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0087_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0087(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0087(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0087(), "Q0087");
        }
        protected abstract ConditionValue getCValueQ0087();

        public void SetQ0088_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0088_Equal(fRES(v));
        }
        protected void DoSetQ0088_Equal(String v) { regQ0088(CK_EQ, v); }
        public void SetQ0088_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0088_NotEqual(fRES(v));
        }
        protected void DoSetQ0088_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_NES, v);
        }
        public void SetQ0088_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_GT, fRES(v));
        }
        public void SetQ0088_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_LT, fRES(v));
        }
        public void SetQ0088_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_GE, fRES(v));
        }
        public void SetQ0088_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_LE, fRES(v));
        }
        public void SetQ0088_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0088(), "Q0088");
        }
        public void SetQ0088_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0088(), "Q0088");
        }
        public void SetQ0088_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0088_LikeSearch(v, cLSOP());
        }
        public void SetQ0088_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0088(), "Q0088", option);
        }
        public void SetQ0088_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0088(), "Q0088", option);
        }
        public void SetQ0088_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0088_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0088(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0088(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0088(), "Q0088");
        }
        protected abstract ConditionValue getCValueQ0088();

        public void SetQ0089_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0089_Equal(fRES(v));
        }
        protected void DoSetQ0089_Equal(String v) { regQ0089(CK_EQ, v); }
        public void SetQ0089_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0089_NotEqual(fRES(v));
        }
        protected void DoSetQ0089_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_NES, v);
        }
        public void SetQ0089_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_GT, fRES(v));
        }
        public void SetQ0089_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_LT, fRES(v));
        }
        public void SetQ0089_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_GE, fRES(v));
        }
        public void SetQ0089_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_LE, fRES(v));
        }
        public void SetQ0089_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0089(), "Q0089");
        }
        public void SetQ0089_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0089(), "Q0089");
        }
        public void SetQ0089_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0089_LikeSearch(v, cLSOP());
        }
        public void SetQ0089_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0089(), "Q0089", option);
        }
        public void SetQ0089_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0089(), "Q0089", option);
        }
        public void SetQ0089_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0089_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0089(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0089(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0089(), "Q0089");
        }
        protected abstract ConditionValue getCValueQ0089();

        public void SetQ0090_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0090_Equal(fRES(v));
        }
        protected void DoSetQ0090_Equal(String v) { regQ0090(CK_EQ, v); }
        public void SetQ0090_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0090_NotEqual(fRES(v));
        }
        protected void DoSetQ0090_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_NES, v);
        }
        public void SetQ0090_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_GT, fRES(v));
        }
        public void SetQ0090_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_LT, fRES(v));
        }
        public void SetQ0090_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_GE, fRES(v));
        }
        public void SetQ0090_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_LE, fRES(v));
        }
        public void SetQ0090_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0090(), "Q0090");
        }
        public void SetQ0090_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0090(), "Q0090");
        }
        public void SetQ0090_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0090_LikeSearch(v, cLSOP());
        }
        public void SetQ0090_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0090(), "Q0090", option);
        }
        public void SetQ0090_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0090(), "Q0090", option);
        }
        public void SetQ0090_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0090_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0090(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0090(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0090(), "Q0090");
        }
        protected abstract ConditionValue getCValueQ0090();

        public void SetQ0091_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0091_Equal(fRES(v));
        }
        protected void DoSetQ0091_Equal(String v) { regQ0091(CK_EQ, v); }
        public void SetQ0091_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0091_NotEqual(fRES(v));
        }
        protected void DoSetQ0091_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_NES, v);
        }
        public void SetQ0091_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_GT, fRES(v));
        }
        public void SetQ0091_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_LT, fRES(v));
        }
        public void SetQ0091_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_GE, fRES(v));
        }
        public void SetQ0091_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_LE, fRES(v));
        }
        public void SetQ0091_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0091(), "Q0091");
        }
        public void SetQ0091_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0091(), "Q0091");
        }
        public void SetQ0091_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0091_LikeSearch(v, cLSOP());
        }
        public void SetQ0091_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0091(), "Q0091", option);
        }
        public void SetQ0091_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0091(), "Q0091", option);
        }
        public void SetQ0091_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0091_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0091(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0091(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0091(), "Q0091");
        }
        protected abstract ConditionValue getCValueQ0091();

        public void SetQ0092_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0092_Equal(fRES(v));
        }
        protected void DoSetQ0092_Equal(String v) { regQ0092(CK_EQ, v); }
        public void SetQ0092_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0092_NotEqual(fRES(v));
        }
        protected void DoSetQ0092_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_NES, v);
        }
        public void SetQ0092_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_GT, fRES(v));
        }
        public void SetQ0092_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_LT, fRES(v));
        }
        public void SetQ0092_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_GE, fRES(v));
        }
        public void SetQ0092_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_LE, fRES(v));
        }
        public void SetQ0092_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0092(), "Q0092");
        }
        public void SetQ0092_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0092(), "Q0092");
        }
        public void SetQ0092_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0092_LikeSearch(v, cLSOP());
        }
        public void SetQ0092_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0092(), "Q0092", option);
        }
        public void SetQ0092_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0092(), "Q0092", option);
        }
        public void SetQ0092_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0092_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0092(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0092(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0092(), "Q0092");
        }
        protected abstract ConditionValue getCValueQ0092();

        public void SetQ0093_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0093_Equal(fRES(v));
        }
        protected void DoSetQ0093_Equal(String v) { regQ0093(CK_EQ, v); }
        public void SetQ0093_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0093_NotEqual(fRES(v));
        }
        protected void DoSetQ0093_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_NES, v);
        }
        public void SetQ0093_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_GT, fRES(v));
        }
        public void SetQ0093_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_LT, fRES(v));
        }
        public void SetQ0093_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_GE, fRES(v));
        }
        public void SetQ0093_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_LE, fRES(v));
        }
        public void SetQ0093_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0093(), "Q0093");
        }
        public void SetQ0093_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0093(), "Q0093");
        }
        public void SetQ0093_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0093_LikeSearch(v, cLSOP());
        }
        public void SetQ0093_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0093(), "Q0093", option);
        }
        public void SetQ0093_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0093(), "Q0093", option);
        }
        public void SetQ0093_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0093_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0093(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0093(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0093(), "Q0093");
        }
        protected abstract ConditionValue getCValueQ0093();

        public void SetQ0094_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0094_Equal(fRES(v));
        }
        protected void DoSetQ0094_Equal(String v) { regQ0094(CK_EQ, v); }
        public void SetQ0094_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0094_NotEqual(fRES(v));
        }
        protected void DoSetQ0094_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_NES, v);
        }
        public void SetQ0094_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_GT, fRES(v));
        }
        public void SetQ0094_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_LT, fRES(v));
        }
        public void SetQ0094_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_GE, fRES(v));
        }
        public void SetQ0094_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_LE, fRES(v));
        }
        public void SetQ0094_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0094(), "Q0094");
        }
        public void SetQ0094_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0094(), "Q0094");
        }
        public void SetQ0094_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0094_LikeSearch(v, cLSOP());
        }
        public void SetQ0094_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0094(), "Q0094", option);
        }
        public void SetQ0094_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0094(), "Q0094", option);
        }
        public void SetQ0094_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0094_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0094(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0094(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0094(), "Q0094");
        }
        protected abstract ConditionValue getCValueQ0094();

        public void SetQ0095_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0095_Equal(fRES(v));
        }
        protected void DoSetQ0095_Equal(String v) { regQ0095(CK_EQ, v); }
        public void SetQ0095_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0095_NotEqual(fRES(v));
        }
        protected void DoSetQ0095_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_NES, v);
        }
        public void SetQ0095_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_GT, fRES(v));
        }
        public void SetQ0095_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_LT, fRES(v));
        }
        public void SetQ0095_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_GE, fRES(v));
        }
        public void SetQ0095_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_LE, fRES(v));
        }
        public void SetQ0095_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0095(), "Q0095");
        }
        public void SetQ0095_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0095(), "Q0095");
        }
        public void SetQ0095_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0095_LikeSearch(v, cLSOP());
        }
        public void SetQ0095_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0095(), "Q0095", option);
        }
        public void SetQ0095_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0095(), "Q0095", option);
        }
        public void SetQ0095_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0095_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0095(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0095(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0095(), "Q0095");
        }
        protected abstract ConditionValue getCValueQ0095();

        public void SetQ0096_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0096_Equal(fRES(v));
        }
        protected void DoSetQ0096_Equal(String v) { regQ0096(CK_EQ, v); }
        public void SetQ0096_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0096_NotEqual(fRES(v));
        }
        protected void DoSetQ0096_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_NES, v);
        }
        public void SetQ0096_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_GT, fRES(v));
        }
        public void SetQ0096_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_LT, fRES(v));
        }
        public void SetQ0096_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_GE, fRES(v));
        }
        public void SetQ0096_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_LE, fRES(v));
        }
        public void SetQ0096_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0096(), "Q0096");
        }
        public void SetQ0096_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0096(), "Q0096");
        }
        public void SetQ0096_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0096_LikeSearch(v, cLSOP());
        }
        public void SetQ0096_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0096(), "Q0096", option);
        }
        public void SetQ0096_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0096(), "Q0096", option);
        }
        public void SetQ0096_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0096_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0096(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0096(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0096(), "Q0096");
        }
        protected abstract ConditionValue getCValueQ0096();

        public void SetQ0097_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0097_Equal(fRES(v));
        }
        protected void DoSetQ0097_Equal(String v) { regQ0097(CK_EQ, v); }
        public void SetQ0097_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0097_NotEqual(fRES(v));
        }
        protected void DoSetQ0097_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_NES, v);
        }
        public void SetQ0097_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_GT, fRES(v));
        }
        public void SetQ0097_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_LT, fRES(v));
        }
        public void SetQ0097_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_GE, fRES(v));
        }
        public void SetQ0097_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_LE, fRES(v));
        }
        public void SetQ0097_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0097(), "Q0097");
        }
        public void SetQ0097_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0097(), "Q0097");
        }
        public void SetQ0097_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0097_LikeSearch(v, cLSOP());
        }
        public void SetQ0097_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0097(), "Q0097", option);
        }
        public void SetQ0097_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0097(), "Q0097", option);
        }
        public void SetQ0097_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0097_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0097(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0097(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0097(), "Q0097");
        }
        protected abstract ConditionValue getCValueQ0097();

        public void SetQ0098_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0098_Equal(fRES(v));
        }
        protected void DoSetQ0098_Equal(String v) { regQ0098(CK_EQ, v); }
        public void SetQ0098_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0098_NotEqual(fRES(v));
        }
        protected void DoSetQ0098_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_NES, v);
        }
        public void SetQ0098_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_GT, fRES(v));
        }
        public void SetQ0098_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_LT, fRES(v));
        }
        public void SetQ0098_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_GE, fRES(v));
        }
        public void SetQ0098_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_LE, fRES(v));
        }
        public void SetQ0098_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0098(), "Q0098");
        }
        public void SetQ0098_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0098(), "Q0098");
        }
        public void SetQ0098_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0098_LikeSearch(v, cLSOP());
        }
        public void SetQ0098_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0098(), "Q0098", option);
        }
        public void SetQ0098_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0098(), "Q0098", option);
        }
        public void SetQ0098_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0098_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0098(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0098(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0098(), "Q0098");
        }
        protected abstract ConditionValue getCValueQ0098();

        public void SetQ0099_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0099_Equal(fRES(v));
        }
        protected void DoSetQ0099_Equal(String v) { regQ0099(CK_EQ, v); }
        public void SetQ0099_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0099_NotEqual(fRES(v));
        }
        protected void DoSetQ0099_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_NES, v);
        }
        public void SetQ0099_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_GT, fRES(v));
        }
        public void SetQ0099_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_LT, fRES(v));
        }
        public void SetQ0099_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_GE, fRES(v));
        }
        public void SetQ0099_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_LE, fRES(v));
        }
        public void SetQ0099_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0099(), "Q0099");
        }
        public void SetQ0099_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0099(), "Q0099");
        }
        public void SetQ0099_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0099_LikeSearch(v, cLSOP());
        }
        public void SetQ0099_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0099(), "Q0099", option);
        }
        public void SetQ0099_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0099(), "Q0099", option);
        }
        public void SetQ0099_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0099_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0099(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0099(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0099(), "Q0099");
        }
        protected abstract ConditionValue getCValueQ0099();

        public void SetQ0100_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0100_Equal(fRES(v));
        }
        protected void DoSetQ0100_Equal(String v) { regQ0100(CK_EQ, v); }
        public void SetQ0100_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQ0100_NotEqual(fRES(v));
        }
        protected void DoSetQ0100_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_NES, v);
        }
        public void SetQ0100_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_GT, fRES(v));
        }
        public void SetQ0100_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_LT, fRES(v));
        }
        public void SetQ0100_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_GE, fRES(v));
        }
        public void SetQ0100_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_LE, fRES(v));
        }
        public void SetQ0100_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQ0100(), "Q0100");
        }
        public void SetQ0100_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQ0100(), "Q0100");
        }
        public void SetQ0100_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQ0100_LikeSearch(v, cLSOP());
        }
        public void SetQ0100_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQ0100(), "Q0100", option);
        }
        public void SetQ0100_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQ0100(), "Q0100", option);
        }
        public void SetQ0100_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQ0100_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ0100(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQ0100(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQ0100(), "Q0100");
        }
        protected abstract ConditionValue getCValueQ0100();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TSurveyDataCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TSurveyDataCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TSurveyDataCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TSurveyDataCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TSurveyDataCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TSurveyDataCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TSurveyDataCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TSurveyDataCB>(delegate(String function, SubQuery<TSurveyDataCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TSurveyDataCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TSurveyDataCB>", subQuery);
            TSurveyDataCB cb = new TSurveyDataCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TSurveyDataCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TSurveyDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSurveyDataCB>", subQuery);
            TSurveyDataCB cb = new TSurveyDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "SAMPLE_ID", "SAMPLE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TSurveyDataCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
