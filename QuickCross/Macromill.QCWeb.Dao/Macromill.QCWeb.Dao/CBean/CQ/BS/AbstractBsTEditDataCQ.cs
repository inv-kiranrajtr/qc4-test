
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
    public abstract class AbstractBsTEditDataCQ : AbstractConditionQuery {

        public AbstractBsTEditDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_EDIT_DATA"; }
        public override String getTableSqlName() { return "T_EDIT_DATA"; }

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
        public void ExistsTEditConditionList(SubQuery<TEditConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditConditionCB>", subQuery);
            TEditConditionCB cb = new TEditConditionCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TEditConditionList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TEditConditionList(TEditConditionCQ subQuery);
        public void ExistsTEditTargetItemList(SubQuery<TEditTargetItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditTargetItemCB>", subQuery);
            TEditTargetItemCB cb = new TEditTargetItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TEditTargetItemList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery);
        public void NotExistsTEditConditionList(SubQuery<TEditConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditConditionCB>", subQuery);
            TEditConditionCB cb = new TEditConditionCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TEditConditionList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TEditConditionList(TEditConditionCQ subQuery);
        public void NotExistsTEditTargetItemList(SubQuery<TEditTargetItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditTargetItemCB>", subQuery);
            TEditTargetItemCB cb = new TEditTargetItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TEditTargetItemList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery);
        public void InScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataEditList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void InScopeTEditConditionList(SubQuery<TEditConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditConditionCB>", subQuery);
            TEditConditionCB cb = new TEditConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TEditConditionList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TEditConditionList(TEditConditionCQ subQuery);
        public void InScopeTEditTargetItemList(SubQuery<TEditTargetItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditTargetItemCB>", subQuery);
            TEditTargetItemCB cb = new TEditTargetItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TEditTargetItemList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery);
        public void NotInScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataEditList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void NotInScopeTEditConditionList(SubQuery<TEditConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditConditionCB>", subQuery);
            TEditConditionCB cb = new TEditConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TEditConditionList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TEditConditionList(TEditConditionCQ subQuery);
        public void NotInScopeTEditTargetItemList(SubQuery<TEditTargetItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditTargetItemCB>", subQuery);
            TEditTargetItemCB cb = new TEditTargetItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TEditTargetItemList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TEditTargetItemList(TEditTargetItemCQ subQuery);
        public void xsderiveTEditConditionList(String function, SubQuery<TEditConditionCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditConditionCB>", subQuery);
            TEditConditionCB cb = new TEditConditionCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TEditConditionList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TEditConditionList(TEditConditionCQ subQuery);
        public void xsderiveTEditTargetItemList(String function, SubQuery<TEditTargetItemCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditTargetItemCB>", subQuery);
            TEditTargetItemCB cb = new TEditTargetItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TEditTargetItemList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TEditTargetItemList(TEditTargetItemCQ subQuery);

        public QDRFunction<TEditConditionCB> DerivedTEditConditionList() {
            return xcreateQDRFunctionTEditConditionList();
        }
        protected QDRFunction<TEditConditionCB> xcreateQDRFunctionTEditConditionList() {
            return new QDRFunction<TEditConditionCB>(delegate(String function, SubQuery<TEditConditionCB> subQuery, String operand, Object value) {
                xqderiveTEditConditionList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTEditConditionList(String function, SubQuery<TEditConditionCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TEditConditionCB>", subQuery);
            TEditConditionCB cb = new TEditConditionCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TEditConditionList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TEditConditionListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TEditConditionList(TEditConditionCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TEditConditionListParameter(Object parameterValue);

        public QDRFunction<TEditTargetItemCB> DerivedTEditTargetItemList() {
            return xcreateQDRFunctionTEditTargetItemList();
        }
        protected QDRFunction<TEditTargetItemCB> xcreateQDRFunctionTEditTargetItemList() {
            return new QDRFunction<TEditTargetItemCB>(delegate(String function, SubQuery<TEditTargetItemCB> subQuery, String operand, Object value) {
                xqderiveTEditTargetItemList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTEditTargetItemList(String function, SubQuery<TEditTargetItemCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TEditTargetItemCB>", subQuery);
            TEditTargetItemCB cb = new TEditTargetItemCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TEditTargetItemList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TEditTargetItemListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TEditTargetItemList(TEditTargetItemCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TEditTargetItemListParameter(Object parameterValue);
        public void SetDataEditId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDataEditId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDataEditId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataEditId(), "DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDataEditId();

        public void SetConditionFlag_Equal(int? v) { regConditionFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of conditionFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetConditionFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regConditionFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of conditionFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetConditionFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regConditionFlag(CK_EQ, int.Parse(code));
        }
        public void SetConditionFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of conditionFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetConditionFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regConditionFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of conditionFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetConditionFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regConditionFlag(CK_NES, int.Parse(code));
        }
        public void SetConditionFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueConditionFlag(), "CONDITION_FLAG");
        }
        public void SetConditionFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueConditionFlag(), "CONDITION_FLAG");
        }
        protected void regConditionFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionFlag(), "CONDITION_FLAG");
        }
        protected abstract ConditionValue getCValueConditionFlag();

        public void SetEditMethod_Equal(int? v) { regEditMethod(CK_EQ, v); }
        public void SetEditMethod_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMethod(CK_NES, v);
        }
        public void SetEditMethod_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMethod(CK_GT, v);
        }
        public void SetEditMethod_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMethod(CK_LT, v);
        }
        public void SetEditMethod_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMethod(CK_GE, v);
        }
        public void SetEditMethod_LessEqual(int? v) {
            WhereSetterFlag = true;
            regEditMethod(CK_LE, v);
        }
        public void SetEditMethod_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueEditMethod(), "EDIT_METHOD");
        }
        public void SetEditMethod_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueEditMethod(), "EDIT_METHOD");
        }
        public void SetEditMethod_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMethod(CK_ISN, DUMMY_OBJECT);
        }
        public void SetEditMethod_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMethod(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regEditMethod(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditMethod(), "EDIT_METHOD");
        }
        protected abstract ConditionValue getCValueEditMethod();

        public void SetEditValueType_Equal(int? v) { regEditValueType(CK_EQ, v); }
        public void SetEditValueType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValueType(CK_NES, v);
        }
        public void SetEditValueType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValueType(CK_GT, v);
        }
        public void SetEditValueType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValueType(CK_LT, v);
        }
        public void SetEditValueType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValueType(CK_GE, v);
        }
        public void SetEditValueType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regEditValueType(CK_LE, v);
        }
        public void SetEditValueType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueEditValueType(), "EDIT_VALUE_TYPE");
        }
        public void SetEditValueType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueEditValueType(), "EDIT_VALUE_TYPE");
        }
        public void SetEditValueType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValueType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetEditValueType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValueType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regEditValueType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditValueType(), "EDIT_VALUE_TYPE");
        }
        protected abstract ConditionValue getCValueEditValueType();

        public void SetEditValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetEditValue_Equal(fRES(v));
        }
        protected void DoSetEditValue_Equal(String v) { regEditValue(CK_EQ, v); }
        public void SetEditValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetEditValue_NotEqual(fRES(v));
        }
        protected void DoSetEditValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_NES, v);
        }
        public void SetEditValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_GT, fRES(v));
        }
        public void SetEditValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_LT, fRES(v));
        }
        public void SetEditValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_GE, fRES(v));
        }
        public void SetEditValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_LE, fRES(v));
        }
        public void SetEditValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueEditValue(), "EDIT_VALUE");
        }
        public void SetEditValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueEditValue(), "EDIT_VALUE");
        }
        public void SetEditValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetEditValue_LikeSearch(v, cLSOP());
        }
        public void SetEditValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueEditValue(), "EDIT_VALUE", option);
        }
        public void SetEditValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueEditValue(), "EDIT_VALUE", option);
        }
        public void SetEditValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetEditValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regEditValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditValue(), "EDIT_VALUE");
        }
        protected abstract ConditionValue getCValueEditValue();

        public void SetConditionDiv_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_Equal(fRES(v));
        }
        protected void DoSetConditionDiv_Equal(String v) { regConditionDiv(CK_EQ, v); }
        public void SetConditionDiv_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionDiv_NotEqual(fRES(v));
        }
        protected void DoSetConditionDiv_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionDiv(CK_NES, v);
        }
        public void SetConditionDiv_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionDiv(CK_GT, fRES(v));
        }
        public void SetConditionDiv_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionDiv(CK_LT, fRES(v));
        }
        public void SetConditionDiv_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionDiv(CK_GE, fRES(v));
        }
        public void SetConditionDiv_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionDiv(CK_LE, fRES(v));
        }
        public void SetConditionDiv_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueConditionDiv(), "CONDITION_DIV");
        }
        public void SetConditionDiv_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueConditionDiv(), "CONDITION_DIV");
        }
        public void SetConditionDiv_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetConditionDiv_LikeSearch(v, cLSOP());
        }
        public void SetConditionDiv_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueConditionDiv(), "CONDITION_DIV", option);
        }
        public void SetConditionDiv_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueConditionDiv(), "CONDITION_DIV", option);
        }
        protected void regConditionDiv(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionDiv(), "CONDITION_DIV");
        }
        protected abstract ConditionValue getCValueConditionDiv();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TEditDataCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TEditDataCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TEditDataCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TEditDataCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TEditDataCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TEditDataCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TEditDataCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TEditDataCB>(delegate(String function, SubQuery<TEditDataCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TEditDataCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TEditDataCB>", subQuery);
            TEditDataCB cb = new TEditDataCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TEditDataCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TEditDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditDataCB>", subQuery);
            TEditDataCB cb = new TEditDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TEditDataCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
