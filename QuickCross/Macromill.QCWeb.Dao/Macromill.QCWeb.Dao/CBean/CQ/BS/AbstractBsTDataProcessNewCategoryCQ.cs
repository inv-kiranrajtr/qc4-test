
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
    public abstract class AbstractBsTDataProcessNewCategoryCQ : AbstractConditionQuery {

        public AbstractBsTDataProcessNewCategoryCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DATA_PROCESS_NEW_CATEGORY"; }
        public override String getTableSqlName() { return "T_DATA_PROCESS_NEW_CATEGORY"; }

        public void SetDataProcessNewCategoryId_Equal(decimal? v) { regDataProcessNewCategoryId(CK_EQ, v); }
        public void SetDataProcessNewCategoryId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewCategoryId(CK_NES, v);
        }
        public void SetDataProcessNewCategoryId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewCategoryId(CK_GT, v);
        }
        public void SetDataProcessNewCategoryId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewCategoryId(CK_LT, v);
        }
        public void SetDataProcessNewCategoryId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewCategoryId(CK_GE, v);
        }
        public void SetDataProcessNewCategoryId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDataProcessNewCategoryId(CK_LE, v);
        }
        public void SetDataProcessNewCategoryId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDataProcessNewCategoryId(), "DATA_PROCESS_NEW_CATEGORY_ID");
        }
        public void SetDataProcessNewCategoryId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDataProcessNewCategoryId(), "DATA_PROCESS_NEW_CATEGORY_ID");
        }
        public void SetDataProcessNewCategoryId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewCategoryId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDataProcessNewCategoryId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataProcessNewCategoryId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDataProcessNewCategoryId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataProcessNewCategoryId(), "DATA_PROCESS_NEW_CATEGORY_ID");
        }
        protected abstract ConditionValue getCValueDataProcessNewCategoryId();

        public void SetNewCategoryNo_Equal(int? v) { regNewCategoryNo(CK_EQ, v); }
        public void SetNewCategoryNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_NES, v);
        }
        public void SetNewCategoryNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_GT, v);
        }
        public void SetNewCategoryNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_LT, v);
        }
        public void SetNewCategoryNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryNo(CK_GE, v);
        }
        public void SetNewCategoryNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regNewCategoryNo(CK_LE, v);
        }
        public void SetNewCategoryNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNewCategoryNo(), "NEW_CATEGORY_NO");
        }
        public void SetNewCategoryNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNewCategoryNo(), "NEW_CATEGORY_NO");
        }
        protected void regNewCategoryNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewCategoryNo(), "NEW_CATEGORY_NO");
        }
        protected abstract ConditionValue getCValueNewCategoryNo();

        public void SetNewCategoryName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewCategoryName_Equal(fRES(v));
        }
        protected void DoSetNewCategoryName_Equal(String v) { regNewCategoryName(CK_EQ, v); }
        public void SetNewCategoryName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewCategoryName_NotEqual(fRES(v));
        }
        protected void DoSetNewCategoryName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryName(CK_NES, v);
        }
        public void SetNewCategoryName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryName(CK_GT, fRES(v));
        }
        public void SetNewCategoryName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryName(CK_LT, fRES(v));
        }
        public void SetNewCategoryName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryName(CK_GE, fRES(v));
        }
        public void SetNewCategoryName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryName(CK_LE, fRES(v));
        }
        public void SetNewCategoryName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNewCategoryName(), "NEW_CATEGORY_NAME");
        }
        public void SetNewCategoryName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNewCategoryName(), "NEW_CATEGORY_NAME");
        }
        public void SetNewCategoryName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNewCategoryName_LikeSearch(v, cLSOP());
        }
        public void SetNewCategoryName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNewCategoryName(), "NEW_CATEGORY_NAME", option);
        }
        public void SetNewCategoryName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNewCategoryName(), "NEW_CATEGORY_NAME", option);
        }
        protected void regNewCategoryName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewCategoryName(), "NEW_CATEGORY_NAME");
        }
        protected abstract ConditionValue getCValueNewCategoryName();

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

        public void SetOperationCode_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_Equal(fRES(v));
        }
        protected void DoSetOperationCode_Equal(String v) { regOperationCode(CK_EQ, v); }
        public void SetOperationCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOperationCode_NotEqual(fRES(v));
        }
        protected void DoSetOperationCode_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_NES, v);
        }
        public void SetOperationCode_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_GT, fRES(v));
        }
        public void SetOperationCode_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_LT, fRES(v));
        }
        public void SetOperationCode_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_GE, fRES(v));
        }
        public void SetOperationCode_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_LE, fRES(v));
        }
        public void SetOperationCode_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOperationCode(), "OPERATION_CODE");
        }
        public void SetOperationCode_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOperationCode(), "OPERATION_CODE");
        }
        public void SetOperationCode_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOperationCode_LikeSearch(v, cLSOP());
        }
        public void SetOperationCode_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOperationCode(), "OPERATION_CODE", option);
        }
        public void SetOperationCode_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOperationCode(), "OPERATION_CODE", option);
        }
        public void SetOperationCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOperationCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOperationCode(CK_ISNN, DUMMY_OBJECT);
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
        public void SetConditionString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetConditionString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regConditionString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionString(), "CONDITION_STRING");
        }
        protected abstract ConditionValue getCValueConditionString();

        public void SetBottomValue_Equal(decimal? v) { regBottomValue(CK_EQ, v); }
        public void SetBottomValue_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomValue(CK_NES, v);
        }
        public void SetBottomValue_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomValue(CK_GT, v);
        }
        public void SetBottomValue_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomValue(CK_LT, v);
        }
        public void SetBottomValue_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomValue(CK_GE, v);
        }
        public void SetBottomValue_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regBottomValue(CK_LE, v);
        }
        public void SetBottomValue_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueBottomValue(), "BOTTOM_VALUE");
        }
        public void SetBottomValue_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueBottomValue(), "BOTTOM_VALUE");
        }
        public void SetBottomValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetBottomValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regBottomValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBottomValue(), "BOTTOM_VALUE");
        }
        protected abstract ConditionValue getCValueBottomValue();

        public void SetUpperValue_Equal(decimal? v) { regUpperValue(CK_EQ, v); }
        public void SetUpperValue_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperValue(CK_NES, v);
        }
        public void SetUpperValue_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperValue(CK_GT, v);
        }
        public void SetUpperValue_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperValue(CK_LT, v);
        }
        public void SetUpperValue_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperValue(CK_GE, v);
        }
        public void SetUpperValue_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regUpperValue(CK_LE, v);
        }
        public void SetUpperValue_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueUpperValue(), "UPPER_VALUE");
        }
        public void SetUpperValue_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueUpperValue(), "UPPER_VALUE");
        }
        public void SetUpperValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetUpperValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regUpperValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUpperValue(), "UPPER_VALUE");
        }
        protected abstract ConditionValue getCValueUpperValue();

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
        public SSQFunction<TDataProcessNewCategoryCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDataProcessNewCategoryCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDataProcessNewCategoryCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDataProcessNewCategoryCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDataProcessNewCategoryCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDataProcessNewCategoryCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDataProcessNewCategoryCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDataProcessNewCategoryCB>(delegate(String function, SubQuery<TDataProcessNewCategoryCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDataProcessNewCategoryCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDataProcessNewCategoryCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDataProcessNewCategoryCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DATA_PROCESS_NEW_CATEGORY_ID", "DATA_PROCESS_NEW_CATEGORY_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDataProcessNewCategoryCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
