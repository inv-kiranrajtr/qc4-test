
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
    public abstract class AbstractBsTOutputTemplateCQ : AbstractConditionQuery {

        public AbstractBsTOutputTemplateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_TEMPLATE"; }
        public override String getTableSqlName() { return "T_OUTPUT_TEMPLATE"; }

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
        public void ExistsTOutputReportsetInfoList(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateId_ExistsSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery);
        public void NotExistsTOutputReportsetInfoList(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateId_NotExistsSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery);
        public void InScopeTOutputReportsetInfoList(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateId_InScopeSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery);
        public void NotInScopeTOutputReportsetInfoList(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateId_NotInScopeSubQuery_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery);
        public void xsderiveTOutputReportsetInfoList(String function, SubQuery<TOutputReportsetInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputTemplateId_SpecifyDerivedReferrer_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery);

        public QDRFunction<TOutputReportsetInfoCB> DerivedTOutputReportsetInfoList() {
            return xcreateQDRFunctionTOutputReportsetInfoList();
        }
        protected QDRFunction<TOutputReportsetInfoCB> xcreateQDRFunctionTOutputReportsetInfoList() {
            return new QDRFunction<TOutputReportsetInfoCB>(delegate(String function, SubQuery<TOutputReportsetInfoCB> subQuery, String operand, Object value) {
                xqderiveTOutputReportsetInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputReportsetInfoList(String function, SubQuery<TOutputReportsetInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoList(TOutputReportsetInfoCQ subQuery);
        public abstract String keepOutputTemplateId_QueryDerivedReferrer_TOutputReportsetInfoListParameter(Object parameterValue);
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

        public void SetOutputTemplateMasterId_Equal(decimal? v) { regOutputTemplateMasterId(CK_EQ, v); }
        public void SetOutputTemplateMasterId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateMasterId(CK_NES, v);
        }
        public void SetOutputTemplateMasterId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateMasterId(CK_GT, v);
        }
        public void SetOutputTemplateMasterId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateMasterId(CK_LT, v);
        }
        public void SetOutputTemplateMasterId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateMasterId(CK_GE, v);
        }
        public void SetOutputTemplateMasterId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputTemplateMasterId(CK_LE, v);
        }
        public void SetOutputTemplateMasterId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputTemplateMasterId(), "OUTPUT_TEMPLATE_MASTER_ID");
        }
        public void SetOutputTemplateMasterId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputTemplateMasterId(), "OUTPUT_TEMPLATE_MASTER_ID");
        }
        public void InScopeTOutputTemplateMaster(SubQuery<TOutputTemplateMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateMasterCB>", subQuery);
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateMaster(TOutputTemplateMasterCQ subQuery);
        public void NotInScopeTOutputTemplateMaster(SubQuery<TOutputTemplateMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateMasterCB>", subQuery);
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateMaster(TOutputTemplateMasterCQ subQuery);
        public void SetOutputTemplateMasterId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateMasterId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputTemplateMasterId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputTemplateMasterId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputTemplateMasterId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputTemplateMasterId(), "OUTPUT_TEMPLATE_MASTER_ID");
        }
        protected abstract ConditionValue getCValueOutputTemplateMasterId();

        public void SetUploadPath_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetUploadPath_Equal(fRES(v));
        }
        protected void DoSetUploadPath_Equal(String v) { regUploadPath(CK_EQ, v); }
        public void SetUploadPath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetUploadPath_NotEqual(fRES(v));
        }
        protected void DoSetUploadPath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUploadPath(CK_NES, v);
        }
        public void SetUploadPath_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUploadPath(CK_GT, fRES(v));
        }
        public void SetUploadPath_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUploadPath(CK_LT, fRES(v));
        }
        public void SetUploadPath_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUploadPath(CK_GE, fRES(v));
        }
        public void SetUploadPath_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUploadPath(CK_LE, fRES(v));
        }
        public void SetUploadPath_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueUploadPath(), "UPLOAD_PATH");
        }
        public void SetUploadPath_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueUploadPath(), "UPLOAD_PATH");
        }
        public void SetUploadPath_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetUploadPath_LikeSearch(v, cLSOP());
        }
        public void SetUploadPath_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueUploadPath(), "UPLOAD_PATH", option);
        }
        public void SetUploadPath_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueUploadPath(), "UPLOAD_PATH", option);
        }
        protected void regUploadPath(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUploadPath(), "UPLOAD_PATH");
        }
        protected abstract ConditionValue getCValueUploadPath();

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
        public void InScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetAlias_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAlias_Equal(fRES(v));
        }
        protected void DoSetAlias_Equal(String v) { regAlias(CK_EQ, v); }
        public void SetAlias_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetAlias_NotEqual(fRES(v));
        }
        protected void DoSetAlias_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAlias(CK_NES, v);
        }
        public void SetAlias_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAlias(CK_GT, fRES(v));
        }
        public void SetAlias_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAlias(CK_LT, fRES(v));
        }
        public void SetAlias_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAlias(CK_GE, fRES(v));
        }
        public void SetAlias_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regAlias(CK_LE, fRES(v));
        }
        public void SetAlias_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueAlias(), "ALIAS");
        }
        public void SetAlias_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueAlias(), "ALIAS");
        }
        public void SetAlias_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetAlias_LikeSearch(v, cLSOP());
        }
        public void SetAlias_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueAlias(), "ALIAS", option);
        }
        public void SetAlias_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueAlias(), "ALIAS", option);
        }
        protected void regAlias(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueAlias(), "ALIAS");
        }
        protected abstract ConditionValue getCValueAlias();

        public void SetCreateDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCreateDatetime(CK_EQ, v);
        }
        public void SetCreateDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCreateDatetime(CK_GT, v);
        }
        public void SetCreateDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCreateDatetime(CK_LT, v);
        }
        public void SetCreateDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCreateDatetime(CK_GE, v);
        }
        public void SetCreateDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCreateDatetime(CK_LE, v);
        }
        public void SetCreateDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueCreateDatetime(), "CREATE_DATETIME", option);
        }
        public void SetCreateDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCreateDatetime_FromTo(from, to, new DateFromToOption());
        }
        protected void regCreateDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCreateDatetime(), "CREATE_DATETIME");
        }
        protected abstract ConditionValue getCValueCreateDatetime();

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
        public SSQFunction<TOutputTemplateCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputTemplateCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputTemplateCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputTemplateCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputTemplateCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputTemplateCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputTemplateCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputTemplateCB>(delegate(String function, SubQuery<TOutputTemplateCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputTemplateCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputTemplateCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "OUTPUT_TEMPLATE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputTemplateCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
