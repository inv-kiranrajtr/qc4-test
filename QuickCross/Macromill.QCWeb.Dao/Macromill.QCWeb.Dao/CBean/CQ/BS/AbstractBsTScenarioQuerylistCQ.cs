
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
    public abstract class AbstractBsTScenarioQuerylistCQ : AbstractConditionQuery {

        public AbstractBsTScenarioQuerylistCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_SCENARIO_QUERYLIST"; }
        public override String getTableSqlName() { return "T_SCENARIO_QUERYLIST"; }

        public void SetScenarioQuerylistId_Equal(decimal? v) { regScenarioQuerylistId(CK_EQ, v); }
        public void SetScenarioQuerylistId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioQuerylistId(CK_NES, v);
        }
        public void SetScenarioQuerylistId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioQuerylistId(CK_GT, v);
        }
        public void SetScenarioQuerylistId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioQuerylistId(CK_LT, v);
        }
        public void SetScenarioQuerylistId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioQuerylistId(CK_GE, v);
        }
        public void SetScenarioQuerylistId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regScenarioQuerylistId(CK_LE, v);
        }
        public void SetScenarioQuerylistId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueScenarioQuerylistId(), "SCENARIO_QUERYLIST_ID");
        }
        public void SetScenarioQuerylistId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueScenarioQuerylistId(), "SCENARIO_QUERYLIST_ID");
        }
        public void SetScenarioQuerylistId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioQuerylistId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetScenarioQuerylistId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioQuerylistId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regScenarioQuerylistId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioQuerylistId(), "SCENARIO_QUERYLIST_ID");
        }
        protected abstract ConditionValue getCValueScenarioQuerylistId();

        public void SetScenarioTotalizationId_Equal(decimal? v) { regScenarioTotalizationId(CK_EQ, v); }
        public void SetScenarioTotalizationId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_NES, v);
        }
        public void SetScenarioTotalizationId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_GT, v);
        }
        public void SetScenarioTotalizationId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_LT, v);
        }
        public void SetScenarioTotalizationId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regScenarioTotalizationId(CK_GE, v);
        }
        public void SetScenarioTotalizationId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regScenarioTotalizationId(CK_LE, v);
        }
        public void SetScenarioTotalizationId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        public void SetScenarioTotalizationId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        public void InScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(cb.Query());
            registerInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_InScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        public void NotInScopeTScenarioTotalization(SubQuery<TScenarioTotalizationCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioTotalizationCB>", subQuery);
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "SCENARIO_TOTALIZATION_ID", "SCENARIO_TOTALIZATION_ID", subQueryPropertyName);
        }
        public abstract String keepScenarioTotalizationId_NotInScopeSubQuery_TScenarioTotalization(TScenarioTotalizationCQ subQuery);
        protected void regScenarioTotalizationId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueScenarioTotalizationId(), "SCENARIO_TOTALIZATION_ID");
        }
        protected abstract ConditionValue getCValueScenarioTotalizationId();

        public void SetSeqNo_Equal(int? v) { regSeqNo(CK_EQ, v); }
        public void SetSeqNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSeqNo(CK_NES, v);
        }
        public void SetSeqNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSeqNo(CK_GT, v);
        }
        public void SetSeqNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSeqNo(CK_LT, v);
        }
        public void SetSeqNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSeqNo(CK_GE, v);
        }
        public void SetSeqNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSeqNo(CK_LE, v);
        }
        public void SetSeqNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSeqNo(), "SEQ_NO");
        }
        public void SetSeqNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSeqNo(), "SEQ_NO");
        }
        protected void regSeqNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSeqNo(), "SEQ_NO");
        }
        protected abstract ConditionValue getCValueSeqNo();

        public void SetItemInfoId_Equal(decimal? v) { regItemInfoId(CK_EQ, v); }
        public void SetItemInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_NES, v);
        }
        public void SetItemInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_GT, v);
        }
        public void SetItemInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_LT, v);
        }
        public void SetItemInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regItemInfoId(CK_GE, v);
        }
        public void SetItemInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regItemInfoId(CK_LE, v);
        }
        public void SetItemInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        public void SetItemInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        public void InScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_InScopeSubQuery_TItemInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TItemInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "Item_Info_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        protected void regItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueItemInfoId();

        public void SetOperationCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of Equal of operationCode as equal. { = }
        /// =: Equal(=)を示す
        /// </summary>
        public void SetOperationCode_Equal_Equal() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(CDef.OperationCode.Equal.Code);
        }
        /// <summary>
        /// Set the value of NotEqual of operationCode as equal. { = }
        /// <>: NotEqual(<>)を示す
        /// </summary>
        public void SetOperationCode_Equal_NotEqual() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(CDef.OperationCode.NotEqual.Code);
        }
        /// <summary>
        /// Set the value of LessThan of operationCode as equal. { = }
        /// <: LessThan(<)を示す
        /// </summary>
        public void SetOperationCode_Equal_LessThan() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(CDef.OperationCode.LessThan.Code);
        }
        /// <summary>
        /// Set the value of GreaterThan of operationCode as equal. { = }
        /// >: GreaterThan(>)を示す
        /// </summary>
        public void SetOperationCode_Equal_GreaterThan() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(CDef.OperationCode.GreaterThan.Code);
        }
        /// <summary>
        /// Set the value of LessEqual of operationCode as equal. { = }
        /// <=: LessEqual(<=)を示す
        /// </summary>
        public void SetOperationCode_Equal_LessEqual() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(CDef.OperationCode.LessEqual.Code);
        }
        /// <summary>
        /// Set the value of GreaterEqual of operationCode as equal. { = }
        /// >=: GreaterEqual(>=)を示す
        /// </summary>
        public void SetOperationCode_Equal_GreaterEqual() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(CDef.OperationCode.GreaterEqual.Code);
        }
        protected void DoSetOperationCode_Equal(String v) { regOperationCode(CK_EQ, v); }
        public void SetOperationCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of Equal of operationCode as notEqual. { &lt;&gt; }
        /// =: Equal(=)を示す
        /// </summary>
        public void SetOperationCode_NotEqual_Equal() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(CDef.OperationCode.Equal.Code);
        }
        /// <summary>
        /// Set the value of NotEqual of operationCode as notEqual. { &lt;&gt; }
        /// <>: NotEqual(<>)を示す
        /// </summary>
        public void SetOperationCode_NotEqual_NotEqual() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(CDef.OperationCode.NotEqual.Code);
        }
        /// <summary>
        /// Set the value of LessThan of operationCode as notEqual. { &lt;&gt; }
        /// <: LessThan(<)を示す
        /// </summary>
        public void SetOperationCode_NotEqual_LessThan() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(CDef.OperationCode.LessThan.Code);
        }
        /// <summary>
        /// Set the value of GreaterThan of operationCode as notEqual. { &lt;&gt; }
        /// >: GreaterThan(>)を示す
        /// </summary>
        public void SetOperationCode_NotEqual_GreaterThan() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(CDef.OperationCode.GreaterThan.Code);
        }
        /// <summary>
        /// Set the value of LessEqual of operationCode as notEqual. { &lt;&gt; }
        /// <=: LessEqual(<=)を示す
        /// </summary>
        public void SetOperationCode_NotEqual_LessEqual() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(CDef.OperationCode.LessEqual.Code);
        }
        /// <summary>
        /// Set the value of GreaterEqual of operationCode as notEqual. { &lt;&gt; }
        /// >=: GreaterEqual(>=)を示す
        /// </summary>
        public void SetOperationCode_NotEqual_GreaterEqual() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(CDef.OperationCode.GreaterEqual.Code);
        }
        protected void DoSetOperationCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_NES, v);
        }
        public void SetOperationCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOperationCode(), "OPERATION_CODE");
        }
        public void SetOperationCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOperationCode(), "OPERATION_CODE");
        }
        protected void regOperationCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOperationCode(), "OPERATION_CODE");
        }
        protected abstract ConditionValue getCValueOperationCode();

        public void SetConditionString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionString_Equal(fRES(v));
        }
        protected void DoSetConditionString_Equal(String v) { regConditionString(CK_EQ, v); }
        public void SetConditionString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionString_NotEqual(fRES(v));
        }
        protected void DoSetConditionString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_NES, v);
        }
        public void SetConditionString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_GT, fRES(v));
        }
        public void SetConditionString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_LT, fRES(v));
        }
        public void SetConditionString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_GE, fRES(v));
        }
        public void SetConditionString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_LE, fRES(v));
        }
        public void SetConditionString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueConditionString(), "CONDITION_STRING");
        }
        public void SetConditionString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueConditionString(), "CONDITION_STRING");
        }
        public void SetConditionString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetConditionString_LikeSearch(v, cLSOP());
        }
        public void SetConditionString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueConditionString(), "CONDITION_STRING", option);
        }
        public void SetConditionString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueConditionString(), "CONDITION_STRING", option);
        }
        protected void regConditionString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionString(), "CONDITION_STRING");
        }
        protected abstract ConditionValue getCValueConditionString();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TScenarioQuerylistCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TScenarioQuerylistCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TScenarioQuerylistCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TScenarioQuerylistCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TScenarioQuerylistCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TScenarioQuerylistCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TScenarioQuerylistCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TScenarioQuerylistCB>(delegate(String function, SubQuery<TScenarioQuerylistCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TScenarioQuerylistCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TScenarioQuerylistCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TScenarioQuerylistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TScenarioQuerylistCB>", subQuery);
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "SCENARIO_QUERYLIST_ID", "SCENARIO_QUERYLIST_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TScenarioQuerylistCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
