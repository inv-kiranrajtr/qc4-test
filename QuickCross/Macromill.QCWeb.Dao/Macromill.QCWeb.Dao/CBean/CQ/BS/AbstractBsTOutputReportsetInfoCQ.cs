
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
    public abstract class AbstractBsTOutputReportsetInfoCQ : AbstractConditionQuery {

        public AbstractBsTOutputReportsetInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_REPORTSET_INFO"; }
        public override String getTableSqlName() { return "T_OUTPUT_REPORTSET_INFO"; }

        public void SetOutputReportsetInfoId_Equal(decimal? v) { regOutputReportsetInfoId(CK_EQ, v); }
        public void SetOutputReportsetInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_NES, v);
        }
        public void SetOutputReportsetInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_GT, v);
        }
        public void SetOutputReportsetInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_LT, v);
        }
        public void SetOutputReportsetInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_GE, v);
        }
        public void SetOutputReportsetInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputReportsetInfoId(CK_LE, v);
        }
        public void SetOutputReportsetInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputReportsetInfoId(), "OUTPUT_REPORTSET_INFO_ID");
        }
        public void SetOutputReportsetInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputReportsetInfoId(), "OUTPUT_REPORTSET_INFO_ID");
        }
        public void ExistsTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_ExistsSubQuery_TOutputRequestList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_ExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void NotExistsTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_NotExistsSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void InScopeTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_InScopeSubQuery_TOutputRequestList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_InScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void NotInScopeTOutputRequestList(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_NotInScopeSubQuery_TOutputRequestList(TOutputRequestCQ subQuery);
        public void xsderiveTOutputRequestList(String function, SubQuery<TOutputRequestCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputReportsetInfoId_SpecifyDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery);

        public QDRFunction<TOutputRequestCB> DerivedTOutputRequestList() {
            return xcreateQDRFunctionTOutputRequestList();
        }
        protected QDRFunction<TOutputRequestCB> xcreateQDRFunctionTOutputRequestList() {
            return new QDRFunction<TOutputRequestCB>(delegate(String function, SubQuery<TOutputRequestCB> subQuery, String operand, Object value) {
                xqderiveTOutputRequestList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputRequestList(String function, SubQuery<TOutputRequestCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestList(TOutputRequestCQ subQuery);
        public abstract String keepOutputReportsetInfoId_QueryDerivedReferrer_TOutputRequestListParameter(Object parameterValue);
        public void SetOutputReportsetInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputReportsetInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputReportsetInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputReportsetInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputReportsetInfoId(), "OUTPUT_REPORTSET_INFO_ID");
        }
        protected abstract ConditionValue getCValueOutputReportsetInfoId();

        public void SetOutputFileTypeCode_Equal(int? v) { regOutputFileTypeCode(CK_EQ, v); }
        public void SetOutputFileTypeCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileTypeCode(CK_NES, v);
        }
        public void SetOutputFileTypeCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileTypeCode(CK_GT, v);
        }
        public void SetOutputFileTypeCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileTypeCode(CK_LT, v);
        }
        public void SetOutputFileTypeCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputFileTypeCode(CK_GE, v);
        }
        public void SetOutputFileTypeCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regOutputFileTypeCode(CK_LE, v);
        }
        public void SetOutputFileTypeCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueOutputFileTypeCode(), "OUTPUT_FILE_TYPE_CODE");
        }
        public void SetOutputFileTypeCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueOutputFileTypeCode(), "OUTPUT_FILE_TYPE_CODE");
        }
        protected void regOutputFileTypeCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputFileTypeCode(), "OUTPUT_FILE_TYPE_CODE");
        }
        protected abstract ConditionValue getCValueOutputFileTypeCode();

        public void SetReportFilenNamePrefix_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetReportFilenNamePrefix_Equal(fRES(v));
        }
        protected void DoSetReportFilenNamePrefix_Equal(String v) { regReportFilenNamePrefix(CK_EQ, v); }
        public void SetReportFilenNamePrefix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetReportFilenNamePrefix_NotEqual(fRES(v));
        }
        protected void DoSetReportFilenNamePrefix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_NES, v);
        }
        public void SetReportFilenNamePrefix_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_GT, fRES(v));
        }
        public void SetReportFilenNamePrefix_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_LT, fRES(v));
        }
        public void SetReportFilenNamePrefix_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_GE, fRES(v));
        }
        public void SetReportFilenNamePrefix_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_LE, fRES(v));
        }
        public void SetReportFilenNamePrefix_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueReportFilenNamePrefix(), "REPORT_FILEN_NAME_PREFIX");
        }
        public void SetReportFilenNamePrefix_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueReportFilenNamePrefix(), "REPORT_FILEN_NAME_PREFIX");
        }
        public void SetReportFilenNamePrefix_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetReportFilenNamePrefix_LikeSearch(v, cLSOP());
        }
        public void SetReportFilenNamePrefix_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueReportFilenNamePrefix(), "REPORT_FILEN_NAME_PREFIX", option);
        }
        public void SetReportFilenNamePrefix_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueReportFilenNamePrefix(), "REPORT_FILEN_NAME_PREFIX", option);
        }
        public void SetReportFilenNamePrefix_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_ISN, DUMMY_OBJECT);
        }
        public void SetReportFilenNamePrefix_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regReportFilenNamePrefix(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regReportFilenNamePrefix(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueReportFilenNamePrefix(), "REPORT_FILEN_NAME_PREFIX");
        }
        protected abstract ConditionValue getCValueReportFilenNamePrefix();

        public void SetCommentOutputFlag_Equal(int? v) { regCommentOutputFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of commentOutputFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetCommentOutputFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCommentOutputFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of commentOutputFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCommentOutputFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCommentOutputFlag(CK_EQ, int.Parse(code));
        }
        public void SetCommentOutputFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCommentOutputFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of commentOutputFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetCommentOutputFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regCommentOutputFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of commentOutputFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetCommentOutputFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regCommentOutputFlag(CK_NES, int.Parse(code));
        }
        public void SetCommentOutputFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCommentOutputFlag(), "COMMENT_OUTPUT_FLAG");
        }
        public void SetCommentOutputFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCommentOutputFlag(), "COMMENT_OUTPUT_FLAG");
        }
        protected void regCommentOutputFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCommentOutputFlag(), "COMMENT_OUTPUT_FLAG");
        }
        protected abstract ConditionValue getCValueCommentOutputFlag();

        public void SetPowerpointType_Equal(int? v) { regPowerpointType(CK_EQ, v); }
        public void SetPowerpointType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPowerpointType(CK_NES, v);
        }
        public void SetPowerpointType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPowerpointType(CK_GT, v);
        }
        public void SetPowerpointType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPowerpointType(CK_LT, v);
        }
        public void SetPowerpointType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPowerpointType(CK_GE, v);
        }
        public void SetPowerpointType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPowerpointType(CK_LE, v);
        }
        public void SetPowerpointType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePowerpointType(), "POWERPOINT_TYPE");
        }
        public void SetPowerpointType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePowerpointType(), "POWERPOINT_TYPE");
        }
        public void SetPowerpointType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPowerpointType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetPowerpointType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPowerpointType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regPowerpointType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePowerpointType(), "POWERPOINT_TYPE");
        }
        protected abstract ConditionValue getCValuePowerpointType();

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
        public void InScopeTOutputTemplate(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_InScopeSubQuery_TOutputTemplate(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateId_InScopeSubQuery_TOutputTemplate(TOutputTemplateCQ subQuery);
        public void NotInScopeTOutputTemplate(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateId_NotInScopeSubQuery_TOutputTemplate(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_ID", "Output_Template_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateId_NotInScopeSubQuery_TOutputTemplate(TOutputTemplateCQ subQuery);
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
        public SSQFunction<TOutputReportsetInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputReportsetInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputReportsetInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputReportsetInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputReportsetInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputReportsetInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputReportsetInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputReportsetInfoCB>(delegate(String function, SubQuery<TOutputReportsetInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputReportsetInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputReportsetInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputReportsetInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputReportsetInfoCB>", subQuery);
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REPORTSET_INFO_ID", "OUTPUT_REPORTSET_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputReportsetInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
