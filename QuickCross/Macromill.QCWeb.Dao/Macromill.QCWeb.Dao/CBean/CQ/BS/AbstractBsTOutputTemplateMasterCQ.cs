
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
    public abstract class AbstractBsTOutputTemplateMasterCQ : AbstractConditionQuery {

        public AbstractBsTOutputTemplateMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_TEMPLATE_MASTER"; }
        public override String getTableSqlName() { return "T_OUTPUT_TEMPLATE_MASTER"; }

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
        public void ExistsTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_ExistsSubQuery_TOutputTemplateList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_ExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void NotExistsTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_NotExistsSubQuery_TOutputTemplateList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_NotExistsSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void InScopeTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_InScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void NotInScopeTOutputTemplateList(SubQuery<TOutputTemplateCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_NotInScopeSubQuery_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public void xsderiveTOutputTemplateList(String function, SubQuery<TOutputTemplateCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputTemplateMasterId_SpecifyDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery);

        public QDRFunction<TOutputTemplateCB> DerivedTOutputTemplateList() {
            return xcreateQDRFunctionTOutputTemplateList();
        }
        protected QDRFunction<TOutputTemplateCB> xcreateQDRFunctionTOutputTemplateList() {
            return new QDRFunction<TOutputTemplateCB>(delegate(String function, SubQuery<TOutputTemplateCB> subQuery, String operand, Object value) {
                xqderiveTOutputTemplateList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputTemplateList(String function, SubQuery<TOutputTemplateCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputTemplateCB>", subQuery);
            TOutputTemplateCB cb = new TOutputTemplateCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateList(TOutputTemplateCQ subQuery);
        public abstract String keepOutputTemplateMasterId_QueryDerivedReferrer_TOutputTemplateListParameter(Object parameterValue);
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

        public void SetPath_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPath_Equal(fRES(v));
        }
        protected void DoSetPath_Equal(String v) { regPath(CK_EQ, v); }
        public void SetPath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetPath_NotEqual(fRES(v));
        }
        protected void DoSetPath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPath(CK_NES, v);
        }
        public void SetPath_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPath(CK_GT, fRES(v));
        }
        public void SetPath_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPath(CK_LT, fRES(v));
        }
        public void SetPath_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPath(CK_GE, fRES(v));
        }
        public void SetPath_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPath(CK_LE, fRES(v));
        }
        public void SetPath_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValuePath(), "PATH");
        }
        public void SetPath_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValuePath(), "PATH");
        }
        public void SetPath_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetPath_LikeSearch(v, cLSOP());
        }
        public void SetPath_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValuePath(), "PATH", option);
        }
        public void SetPath_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValuePath(), "PATH", option);
        }
        protected void regPath(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePath(), "PATH");
        }
        protected abstract ConditionValue getCValuePath();

        public void SetMd5Hash_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMd5Hash_Equal(fRES(v));
        }
        protected void DoSetMd5Hash_Equal(String v) { regMd5Hash(CK_EQ, v); }
        public void SetMd5Hash_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMd5Hash_NotEqual(fRES(v));
        }
        protected void DoSetMd5Hash_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMd5Hash(CK_NES, v);
        }
        public void SetMd5Hash_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMd5Hash(CK_GT, fRES(v));
        }
        public void SetMd5Hash_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMd5Hash(CK_LT, fRES(v));
        }
        public void SetMd5Hash_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMd5Hash(CK_GE, fRES(v));
        }
        public void SetMd5Hash_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMd5Hash(CK_LE, fRES(v));
        }
        public void SetMd5Hash_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMd5Hash(), "MD5_HASH");
        }
        public void SetMd5Hash_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMd5Hash(), "MD5_HASH");
        }
        public void SetMd5Hash_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetMd5Hash_LikeSearch(v, cLSOP());
        }
        public void SetMd5Hash_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueMd5Hash(), "MD5_HASH", option);
        }
        public void SetMd5Hash_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueMd5Hash(), "MD5_HASH", option);
        }
        protected void regMd5Hash(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMd5Hash(), "MD5_HASH");
        }
        protected abstract ConditionValue getCValueMd5Hash();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputTemplateMasterCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputTemplateMasterCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputTemplateMasterCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputTemplateMasterCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputTemplateMasterCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputTemplateMasterCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputTemplateMasterCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputTemplateMasterCB>(delegate(String function, SubQuery<TOutputTemplateMasterCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputTemplateMasterCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputTemplateMasterCB>", subQuery);
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputTemplateMasterCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputTemplateMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputTemplateMasterCB>", subQuery);
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_TEMPLATE_MASTER_ID", "OUTPUT_TEMPLATE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputTemplateMasterCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
