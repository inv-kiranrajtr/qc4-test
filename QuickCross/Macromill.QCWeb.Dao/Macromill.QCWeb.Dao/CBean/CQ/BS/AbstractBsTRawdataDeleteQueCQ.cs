
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
    public abstract class AbstractBsTRawdataDeleteQueCQ : AbstractConditionQuery {

        public AbstractBsTRawdataDeleteQueCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_RAWDATA_DELETE_QUE"; }
        public override String getTableSqlName() { return "T_RAWDATA_DELETE_QUE"; }

        public void SetRawdataDeleteQueId_Equal(decimal? v) { regRawdataDeleteQueId(CK_EQ, v); }
        public void SetRawdataDeleteQueId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataDeleteQueId(CK_NES, v);
        }
        public void SetRawdataDeleteQueId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataDeleteQueId(CK_GT, v);
        }
        public void SetRawdataDeleteQueId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataDeleteQueId(CK_LT, v);
        }
        public void SetRawdataDeleteQueId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataDeleteQueId(CK_GE, v);
        }
        public void SetRawdataDeleteQueId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regRawdataDeleteQueId(CK_LE, v);
        }
        public void SetRawdataDeleteQueId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueRawdataDeleteQueId(), "RAWDATA_DELETE_QUE_ID");
        }
        public void SetRawdataDeleteQueId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueRawdataDeleteQueId(), "RAWDATA_DELETE_QUE_ID");
        }
        public void SetRawdataDeleteQueId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataDeleteQueId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetRawdataDeleteQueId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regRawdataDeleteQueId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regRawdataDeleteQueId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueRawdataDeleteQueId(), "RAWDATA_DELETE_QUE_ID");
        }
        protected abstract ConditionValue getCValueRawdataDeleteQueId();

        public void SetAddDataNo_Equal(long? v) { regAddDataNo(CK_EQ, v); }
        public void SetAddDataNo_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_NES, v);
        }
        public void SetAddDataNo_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_GT, v);
        }
        public void SetAddDataNo_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_LT, v);
        }
        public void SetAddDataNo_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAddDataNo(CK_GE, v);
        }
        public void SetAddDataNo_LessEqual(long? v) {
            WhereSetterFlag = true;
            regAddDataNo(CK_LE, v);
        }
        public void SetAddDataNo_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueAddDataNo(), "ADD_DATA_NO");
        }
        public void SetAddDataNo_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueAddDataNo(), "ADD_DATA_NO");
        }
        protected void regAddDataNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAddDataNo(), "ADD_DATA_NO");
        }
        protected abstract ConditionValue getCValueAddDataNo();

        public void SetQcwebJobNo_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQcwebJobNo_Equal(fRES(v));
        }
        protected void DoSetQcwebJobNo_Equal(String v) { regQcwebJobNo(CK_EQ, v); }
        public void SetQcwebJobNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetQcwebJobNo_NotEqual(fRES(v));
        }
        protected void DoSetQcwebJobNo_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_NES, v);
        }
        public void SetQcwebJobNo_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_GT, fRES(v));
        }
        public void SetQcwebJobNo_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_LT, fRES(v));
        }
        public void SetQcwebJobNo_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_GE, fRES(v));
        }
        public void SetQcwebJobNo_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebJobNo(CK_LE, fRES(v));
        }
        public void SetQcwebJobNo_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueQcwebJobNo(), "QCWEB_JOB_NO");
        }
        public void SetQcwebJobNo_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueQcwebJobNo(), "QCWEB_JOB_NO");
        }
        public void SetQcwebJobNo_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetQcwebJobNo_LikeSearch(v, cLSOP());
        }
        public void SetQcwebJobNo_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueQcwebJobNo(), "QCWEB_JOB_NO", option);
        }
        public void SetQcwebJobNo_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueQcwebJobNo(), "QCWEB_JOB_NO", option);
        }
        protected void regQcwebJobNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebJobNo(), "QCWEB_JOB_NO");
        }
        protected abstract ConditionValue getCValueQcwebJobNo();

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

        public void SetDeleteOrderDate_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteOrderDate(CK_EQ, v);
        }
        public void SetDeleteOrderDate_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteOrderDate(CK_GT, v);
        }
        public void SetDeleteOrderDate_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteOrderDate(CK_LT, v);
        }
        public void SetDeleteOrderDate_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteOrderDate(CK_GE, v);
        }
        public void SetDeleteOrderDate_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteOrderDate(CK_LE, v);
        }
        public void SetDeleteOrderDate_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueDeleteOrderDate(), "DELETE_ORDER_DATE", option);
        }
        public void SetDeleteOrderDate_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDeleteOrderDate_FromTo(from, to, new DateFromToOption());
        }
        protected void regDeleteOrderDate(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteOrderDate(), "DELETE_ORDER_DATE");
        }
        protected abstract ConditionValue getCValueDeleteOrderDate();

        public void SetDeleteStatus_Equal(int? v) { regDeleteStatus(CK_EQ, v); }
        /// <summary>
        /// Set the value of NONE_DELETE of deleteStatus as equal. { = }
        /// 未削除: 未削除を示す
        /// </summary>
        public void SetDeleteStatus_Equal_NONE_DELETE() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteStatus.NONE_DELETE.Code;
            regDeleteStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of DELETE_EXEC of deleteStatus as equal. { = }
        /// 削除中: 削除中を示す
        /// </summary>
        public void SetDeleteStatus_Equal_DELETE_EXEC() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteStatus.DELETE_EXEC.Code;
            regDeleteStatus(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of DELETE_END of deleteStatus as equal. { = }
        /// 削除完: 削除完を示す
        /// </summary>
        public void SetDeleteStatus_Equal_DELETE_END() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteStatus.DELETE_END.Code;
            regDeleteStatus(CK_EQ, int.Parse(code));
        }
        public void SetDeleteStatus_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteStatus(CK_NES, v);
        }
        /// <summary>
        /// Set the value of NONE_DELETE of deleteStatus as notEqual. { &lt;&gt; }
        /// 未削除: 未削除を示す
        /// </summary>
        public void SetDeleteStatus_NotEqual_NONE_DELETE() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteStatus.NONE_DELETE.Code;
            regDeleteStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of DELETE_EXEC of deleteStatus as notEqual. { &lt;&gt; }
        /// 削除中: 削除中を示す
        /// </summary>
        public void SetDeleteStatus_NotEqual_DELETE_EXEC() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteStatus.DELETE_EXEC.Code;
            regDeleteStatus(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of DELETE_END of deleteStatus as notEqual. { &lt;&gt; }
        /// 削除完: 削除完を示す
        /// </summary>
        public void SetDeleteStatus_NotEqual_DELETE_END() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.DeleteStatus.DELETE_END.Code;
            regDeleteStatus(CK_NES, int.Parse(code));
        }
        public void SetDeleteStatus_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDeleteStatus(), "DELETE_STATUS");
        }
        public void SetDeleteStatus_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDeleteStatus(), "DELETE_STATUS");
        }
        protected void regDeleteStatus(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteStatus(), "DELETE_STATUS");
        }
        protected abstract ConditionValue getCValueDeleteStatus();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TRawdataDeleteQueCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TRawdataDeleteQueCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TRawdataDeleteQueCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TRawdataDeleteQueCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TRawdataDeleteQueCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TRawdataDeleteQueCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TRawdataDeleteQueCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TRawdataDeleteQueCB>(delegate(String function, SubQuery<TRawdataDeleteQueCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TRawdataDeleteQueCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TRawdataDeleteQueCB>", subQuery);
            TRawdataDeleteQueCB cb = new TRawdataDeleteQueCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TRawdataDeleteQueCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TRawdataDeleteQueCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TRawdataDeleteQueCB>", subQuery);
            TRawdataDeleteQueCB cb = new TRawdataDeleteQueCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "RAWDATA_DELETE_QUE_ID", "RAWDATA_DELETE_QUE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TRawdataDeleteQueCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
