
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
    public abstract class AbstractBsTSurveyInfoCQ : AbstractConditionQuery {

        public AbstractBsTSurveyInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_SURVEY_INFO"; }
        public override String getTableSqlName() { return "T_SURVEY_INFO"; }

        public void SetSurveyInfoId_Equal(decimal? v) { regSurveyInfoId(CK_EQ, v); }
        public void SetSurveyInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_NES, v);
        }
        public void SetSurveyInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_GT, v);
        }
        public void SetSurveyInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_LT, v);
        }
        public void SetSurveyInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_GE, v);
        }
        public void SetSurveyInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regSurveyInfoId(CK_LE, v);
        }
        public void SetSurveyInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueSurveyInfoId(), "SURVEY_INFO_ID");
        }
        public void SetSurveyInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueSurveyInfoId(), "SURVEY_INFO_ID");
        }
        public void ExistsTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_ExistsSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepSurveyInfoId_ExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepSurveyInfoId_NotExistsSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_InScopeSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepSurveyInfoId_InScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoList(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepSurveyInfoId_NotInScopeSubQuery_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public void xsderiveTQcwebSurveyInfoList(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepSurveyInfoId_SpecifyDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);

        public QDRFunction<TQcwebSurveyInfoCB> DerivedTQcwebSurveyInfoList() {
            return xcreateQDRFunctionTQcwebSurveyInfoList();
        }
        protected QDRFunction<TQcwebSurveyInfoCB> xcreateQDRFunctionTQcwebSurveyInfoList() {
            return new QDRFunction<TQcwebSurveyInfoCB>(delegate(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String operand, Object value) {
                xqderiveTQcwebSurveyInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTQcwebSurveyInfoList(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepSurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepSurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepSurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoList(TQcwebSurveyInfoCQ subQuery);
        public abstract String keepSurveyInfoId_QueryDerivedReferrer_TQcwebSurveyInfoListParameter(Object parameterValue);
        public void SetSurveyInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSurveyInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSurveyInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSurveyInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSurveyInfoId(), "SURVEY_INFO_ID");
        }
        protected abstract ConditionValue getCValueSurveyInfoId();

        public void SetMainSurveyId_Equal(decimal? v) { regMainSurveyId(CK_EQ, v); }
        public void SetMainSurveyId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_NES, v);
        }
        public void SetMainSurveyId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_GT, v);
        }
        public void SetMainSurveyId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_LT, v);
        }
        public void SetMainSurveyId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMainSurveyId(CK_GE, v);
        }
        public void SetMainSurveyId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regMainSurveyId(CK_LE, v);
        }
        public void SetMainSurveyId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueMainSurveyId(), "MAIN_SURVEY_ID");
        }
        public void SetMainSurveyId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueMainSurveyId(), "MAIN_SURVEY_ID");
        }
        protected void regMainSurveyId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMainSurveyId(), "MAIN_SURVEY_ID");
        }
        protected abstract ConditionValue getCValueMainSurveyId();

        public void SetScheduleDeleteDate_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScheduleDeleteDate(CK_EQ, v);
        }
        public void SetScheduleDeleteDate_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScheduleDeleteDate(CK_GT, v);
        }
        public void SetScheduleDeleteDate_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScheduleDeleteDate(CK_LT, v);
        }
        public void SetScheduleDeleteDate_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScheduleDeleteDate(CK_GE, v);
        }
        public void SetScheduleDeleteDate_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScheduleDeleteDate(CK_LE, v);
        }
        public void SetScheduleDeleteDate_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueScheduleDeleteDate(), "SCHEDULE_DELETE_DATE", option);
        }
        public void SetScheduleDeleteDate_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetScheduleDeleteDate_FromTo(from, to, new DateFromToOption());
        }
        protected void regScheduleDeleteDate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScheduleDeleteDate(), "SCHEDULE_DELETE_DATE");
        }
        protected abstract ConditionValue getCValueScheduleDeleteDate();

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
        protected void regDeleteFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteFlag(), "DELETE_FLAG");
        }
        protected abstract ConditionValue getCValueDeleteFlag();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TSurveyInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TSurveyInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TSurveyInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TSurveyInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TSurveyInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TSurveyInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TSurveyInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TSurveyInfoCB>(delegate(String function, SubQuery<TSurveyInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TSurveyInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TSurveyInfoCB>", subQuery);
            TSurveyInfoCB cb = new TSurveyInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TSurveyInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TSurveyInfoCB>", subQuery);
            TSurveyInfoCB cb = new TSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "SURVEY_INFO_ID", "SURVEY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TSurveyInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
