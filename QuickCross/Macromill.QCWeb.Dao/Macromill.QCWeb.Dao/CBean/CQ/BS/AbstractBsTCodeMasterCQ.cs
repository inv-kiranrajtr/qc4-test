
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
    public abstract class AbstractBsTCodeMasterCQ : AbstractConditionQuery {

        public AbstractBsTCodeMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_CODE_MASTER"; }
        public override String getTableSqlName() { return "T_CODE_MASTER"; }

        public void SetCodeMasterId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCodeMasterId_Equal(fRES(v));
        }
        protected void DoSetCodeMasterId_Equal(String v) { regCodeMasterId(CK_EQ, v); }
        public void SetCodeMasterId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCodeMasterId_NotEqual(fRES(v));
        }
        protected void DoSetCodeMasterId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_NES, v);
        }
        public void SetCodeMasterId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_GT, fRES(v));
        }
        public void SetCodeMasterId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_LT, fRES(v));
        }
        public void SetCodeMasterId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_GE, fRES(v));
        }
        public void SetCodeMasterId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_LE, fRES(v));
        }
        public void SetCodeMasterId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCodeMasterId(), "CODE_MASTER_ID");
        }
        public void SetCodeMasterId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCodeMasterId(), "CODE_MASTER_ID");
        }
        public void SetCodeMasterId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCodeMasterId_LikeSearch(v, cLSOP());
        }
        public void SetCodeMasterId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCodeMasterId(), "CODE_MASTER_ID", option);
        }
        public void SetCodeMasterId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCodeMasterId(), "CODE_MASTER_ID", option);
        }
        public void SetCodeMasterId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCodeMasterId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeMasterId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCodeMasterId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCodeMasterId(), "CODE_MASTER_ID");
        }
        protected abstract ConditionValue getCValueCodeMasterId();

        public void SetGroupKey_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGroupKey_Equal(fRES(v));
        }
        protected void DoSetGroupKey_Equal(String v) { regGroupKey(CK_EQ, v); }
        public void SetGroupKey_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetGroupKey_NotEqual(fRES(v));
        }
        protected void DoSetGroupKey_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGroupKey(CK_NES, v);
        }
        public void SetGroupKey_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGroupKey(CK_GT, fRES(v));
        }
        public void SetGroupKey_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGroupKey(CK_LT, fRES(v));
        }
        public void SetGroupKey_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGroupKey(CK_GE, fRES(v));
        }
        public void SetGroupKey_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regGroupKey(CK_LE, fRES(v));
        }
        public void SetGroupKey_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueGroupKey(), "GROUP_KEY");
        }
        public void SetGroupKey_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueGroupKey(), "GROUP_KEY");
        }
        public void SetGroupKey_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetGroupKey_LikeSearch(v, cLSOP());
        }
        public void SetGroupKey_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueGroupKey(), "GROUP_KEY", option);
        }
        public void SetGroupKey_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueGroupKey(), "GROUP_KEY", option);
        }
        protected void regGroupKey(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueGroupKey(), "GROUP_KEY");
        }
        protected abstract ConditionValue getCValueGroupKey();

        public void SetCodeValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCodeValue_Equal(fRES(v));
        }
        protected void DoSetCodeValue_Equal(String v) { regCodeValue(CK_EQ, v); }
        public void SetCodeValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCodeValue_NotEqual(fRES(v));
        }
        protected void DoSetCodeValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeValue(CK_NES, v);
        }
        public void SetCodeValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeValue(CK_GT, fRES(v));
        }
        public void SetCodeValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeValue(CK_LT, fRES(v));
        }
        public void SetCodeValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeValue(CK_GE, fRES(v));
        }
        public void SetCodeValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCodeValue(CK_LE, fRES(v));
        }
        public void SetCodeValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCodeValue(), "CODE_VALUE");
        }
        public void SetCodeValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCodeValue(), "CODE_VALUE");
        }
        public void SetCodeValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCodeValue_LikeSearch(v, cLSOP());
        }
        public void SetCodeValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCodeValue(), "CODE_VALUE", option);
        }
        public void SetCodeValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCodeValue(), "CODE_VALUE", option);
        }
        protected void regCodeValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCodeValue(), "CODE_VALUE");
        }
        protected abstract ConditionValue getCValueCodeValue();

        public void SetMessageId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMessageId_Equal(fRES(v));
        }
        protected void DoSetMessageId_Equal(String v) { regMessageId(CK_EQ, v); }
        public void SetMessageId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMessageId_NotEqual(fRES(v));
        }
        protected void DoSetMessageId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessageId(CK_NES, v);
        }
        public void SetMessageId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessageId(CK_GT, fRES(v));
        }
        public void SetMessageId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessageId(CK_LT, fRES(v));
        }
        public void SetMessageId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessageId(CK_GE, fRES(v));
        }
        public void SetMessageId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMessageId(CK_LE, fRES(v));
        }
        public void SetMessageId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMessageId(), "MESSAGE_ID");
        }
        public void SetMessageId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMessageId(), "MESSAGE_ID");
        }
        public void SetMessageId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetMessageId_LikeSearch(v, cLSOP());
        }
        public void SetMessageId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueMessageId(), "MESSAGE_ID", option);
        }
        public void SetMessageId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueMessageId(), "MESSAGE_ID", option);
        }
        protected void regMessageId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMessageId(), "MESSAGE_ID");
        }
        protected abstract ConditionValue getCValueMessageId();

        public void SetSortNo_Equal(int? v) { regSortNo(CK_EQ, v); }
        public void SetSortNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_NES, v);
        }
        public void SetSortNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GT, v);
        }
        public void SetSortNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_LT, v);
        }
        public void SetSortNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSortNo(CK_GE, v);
        }
        public void SetSortNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSortNo(CK_LE, v);
        }
        public void SetSortNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        public void SetSortNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSortNo(), "SORT_NO");
        }
        protected void regSortNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSortNo(), "SORT_NO");
        }
        protected abstract ConditionValue getCValueSortNo();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TCodeMasterCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TCodeMasterCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TCodeMasterCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TCodeMasterCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TCodeMasterCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TCodeMasterCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TCodeMasterCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TCodeMasterCB>(delegate(String function, SubQuery<TCodeMasterCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TCodeMasterCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TCodeMasterCB>", subQuery);
            TCodeMasterCB cb = new TCodeMasterCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TCodeMasterCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TCodeMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCodeMasterCB>", subQuery);
            TCodeMasterCB cb = new TCodeMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "CODE_MASTER_ID", "CODE_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TCodeMasterCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
