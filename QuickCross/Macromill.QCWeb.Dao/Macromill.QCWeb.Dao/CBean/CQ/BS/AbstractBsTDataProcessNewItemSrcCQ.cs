
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
    public abstract class AbstractBsTDataProcessNewItemSrcCQ : AbstractConditionQuery {

        public AbstractBsTDataProcessNewItemSrcCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DATA_PROCESS_NEW_ITEM_SRC"; }
        public override String getTableSqlName() { return "T_DATA_PROCESS_NEW_ITEM_SRC"; }

        public void SetDataProcessNewItemSrcId_Equal(decimal? v) { regDataProcessNewItemSrcId(CK_EQ, v); }
        public void SetDataProcessNewItemSrcId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewItemSrcId(CK_NES, v);
        }
        public void SetDataProcessNewItemSrcId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewItemSrcId(CK_GT, v);
        }
        public void SetDataProcessNewItemSrcId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewItemSrcId(CK_LT, v);
        }
        public void SetDataProcessNewItemSrcId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewItemSrcId(CK_GE, v);
        }
        public void SetDataProcessNewItemSrcId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDataProcessNewItemSrcId(CK_LE, v);
        }
        public void SetDataProcessNewItemSrcId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDataProcessNewItemSrcId(), "DATA_PROCESS_NEW_ITEM_SRC_ID");
        }
        public void SetDataProcessNewItemSrcId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDataProcessNewItemSrcId(), "DATA_PROCESS_NEW_ITEM_SRC_ID");
        }
        public void SetDataProcessNewItemSrcId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewItemSrcId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDataProcessNewItemSrcId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewItemSrcId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDataProcessNewItemSrcId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataProcessNewItemSrcId(), "DATA_PROCESS_NEW_ITEM_SRC_ID");
        }
        protected abstract ConditionValue getCValueDataProcessNewItemSrcId();

        public void SetSrcItemId_Equal(decimal? v) { regSrcItemId(CK_EQ, v); }
        public void SetSrcItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_NES, v);
        }
        public void SetSrcItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_GT, v);
        }
        public void SetSrcItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_LT, v);
        }
        public void SetSrcItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_GE, v);
        }
        public void SetSrcItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regSrcItemId(CK_LE, v);
        }
        public void SetSrcItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueSrcItemId(), "SRC_ITEM_ID");
        }
        public void SetSrcItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueSrcItemId(), "SRC_ITEM_ID");
        }
        public void SetSrcItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSrcItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSrcItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSrcItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSrcItemId(), "SRC_ITEM_ID");
        }
        protected abstract ConditionValue getCValueSrcItemId();

        public void SetNewItemId_Equal(decimal? v) { regNewItemId(CK_EQ, v); }
        public void SetNewItemId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_NES, v);
        }
        public void SetNewItemId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_GT, v);
        }
        public void SetNewItemId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_LT, v);
        }
        public void SetNewItemId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_GE, v);
        }
        public void SetNewItemId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regNewItemId(CK_LE, v);
        }
        public void SetNewItemId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueNewItemId(), "NEW_ITEM_ID");
        }
        public void SetNewItemId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueNewItemId(), "NEW_ITEM_ID");
        }
        public void SetNewItemId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNewItemId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNewItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewItemId(), "NEW_ITEM_ID");
        }
        protected abstract ConditionValue getCValueNewItemId();

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

        public void SetTargetFlag_Equal(int? v) { regTargetFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of targetFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetTargetFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTargetFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of targetFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTargetFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTargetFlag(CK_EQ, int.Parse(code));
        }
        public void SetTargetFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of targetFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetTargetFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regTargetFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of targetFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetTargetFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regTargetFlag(CK_NES, int.Parse(code));
        }
        public void SetTargetFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTargetFlag(), "TARGET_FLAG");
        }
        public void SetTargetFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTargetFlag(), "TARGET_FLAG");
        }
        protected void regTargetFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTargetFlag(), "TARGET_FLAG");
        }
        protected abstract ConditionValue getCValueTargetFlag();

        public void SetDataEditId_Equal(decimal? v) { regDataEditId(CK_EQ, v); }
        public void SetDataEditId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_NES, v);
        }
        public void SetDataEditId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_GT, v);
        }
        public void SetDataEditId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_LT, v);
        }
        public void SetDataEditId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_GE, v);
        }
        public void SetDataEditId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDataEditId(CK_LE, v);
        }
        public void SetDataEditId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDataEditId(), "DATA_EDIT_ID");
        }
        public void SetDataEditId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDataEditId(), "DATA_EDIT_ID");
        }
        public void InScopeTDataProcessNewItem(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataProcessNewItem(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery);
        public void NotInScopeTDataProcessNewItem(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItem(TDataProcessNewItemCQ subQuery);
        protected void regDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataEditId(), "DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDataEditId();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDataProcessNewItemSrcCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDataProcessNewItemSrcCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDataProcessNewItemSrcCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDataProcessNewItemSrcCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDataProcessNewItemSrcCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDataProcessNewItemSrcCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDataProcessNewItemSrcCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDataProcessNewItemSrcCB>(delegate(String function, SubQuery<TDataProcessNewItemSrcCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDataProcessNewItemSrcCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDataProcessNewItemSrcCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDataProcessNewItemSrcCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DATA_PROCESS_NEW_ITEM_SRC_ID", "DATA_PROCESS_NEW_ITEM_SRC_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDataProcessNewItemSrcCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
