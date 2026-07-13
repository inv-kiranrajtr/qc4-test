
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
    public abstract class AbstractBsTApplicationCQ : AbstractConditionQuery {

        public AbstractBsTApplicationCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_APPLICATION"; }
        public override String getTableSqlName() { return "T_APPLICATION"; }

        public void SetIdentifier_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetIdentifier_Equal(fRES(v));
        }
        protected void DoSetIdentifier_Equal(String v) { regIdentifier(CK_EQ, v); }
        public void SetIdentifier_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetIdentifier_NotEqual(fRES(v));
        }
        protected void DoSetIdentifier_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_NES, v);
        }
        public void SetIdentifier_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_GT, fRES(v));
        }
        public void SetIdentifier_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_LT, fRES(v));
        }
        public void SetIdentifier_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_GE, fRES(v));
        }
        public void SetIdentifier_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_LE, fRES(v));
        }
        public void SetIdentifier_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueIdentifier(), "IDENTIFIER");
        }
        public void SetIdentifier_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueIdentifier(), "IDENTIFIER");
        }
        public void SetIdentifier_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetIdentifier_LikeSearch(v, cLSOP());
        }
        public void SetIdentifier_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueIdentifier(), "IDENTIFIER", option);
        }
        public void SetIdentifier_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueIdentifier(), "IDENTIFIER", option);
        }
        public void SetIdentifier_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_ISN, DUMMY_OBJECT);
        }
        public void SetIdentifier_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regIdentifier(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regIdentifier(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueIdentifier(), "IDENTIFIER");
        }
        protected abstract ConditionValue getCValueIdentifier();

        public void SetSettingValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSettingValue_Equal(fRES(v));
        }
        protected void DoSetSettingValue_Equal(String v) { regSettingValue(CK_EQ, v); }
        public void SetSettingValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSettingValue_NotEqual(fRES(v));
        }
        protected void DoSetSettingValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSettingValue(CK_NES, v);
        }
        public void SetSettingValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSettingValue(CK_GT, fRES(v));
        }
        public void SetSettingValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSettingValue(CK_LT, fRES(v));
        }
        public void SetSettingValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSettingValue(CK_GE, fRES(v));
        }
        public void SetSettingValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSettingValue(CK_LE, fRES(v));
        }
        public void SetSettingValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSettingValue(), "SETTING_VALUE");
        }
        public void SetSettingValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSettingValue(), "SETTING_VALUE");
        }
        public void SetSettingValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSettingValue_LikeSearch(v, cLSOP());
        }
        public void SetSettingValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSettingValue(), "SETTING_VALUE", option);
        }
        public void SetSettingValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSettingValue(), "SETTING_VALUE", option);
        }
        protected void regSettingValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSettingValue(), "SETTING_VALUE");
        }
        protected abstract ConditionValue getCValueSettingValue();

        public void SetDescription_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDescription_Equal(fRES(v));
        }
        protected void DoSetDescription_Equal(String v) { regDescription(CK_EQ, v); }
        public void SetDescription_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDescription_NotEqual(fRES(v));
        }
        protected void DoSetDescription_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_NES, v);
        }
        public void SetDescription_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_GT, fRES(v));
        }
        public void SetDescription_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_LT, fRES(v));
        }
        public void SetDescription_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_GE, fRES(v));
        }
        public void SetDescription_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_LE, fRES(v));
        }
        public void SetDescription_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueDescription(), "DESCRIPTION");
        }
        public void SetDescription_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueDescription(), "DESCRIPTION");
        }
        public void SetDescription_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDescription_LikeSearch(v, cLSOP());
        }
        public void SetDescription_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueDescription(), "DESCRIPTION", option);
        }
        public void SetDescription_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueDescription(), "DESCRIPTION", option);
        }
        public void SetDescription_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDescription_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDescription(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDescription(), "DESCRIPTION");
        }
        protected abstract ConditionValue getCValueDescription();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TApplicationCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TApplicationCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TApplicationCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TApplicationCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TApplicationCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TApplicationCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TApplicationCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TApplicationCB>(delegate(String function, SubQuery<TApplicationCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TApplicationCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TApplicationCB>", subQuery);
            TApplicationCB cb = new TApplicationCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TApplicationCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TApplicationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TApplicationCB>", subQuery);
            TApplicationCB cb = new TApplicationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "IDENTIFIER", "IDENTIFIER", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TApplicationCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
