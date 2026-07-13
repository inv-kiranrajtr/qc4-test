
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
    public abstract class AbstractBsTCategoryInfoCQ : AbstractConditionQuery {

        public AbstractBsTCategoryInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_CATEGORY_INFO"; }
        public override String getTableSqlName() { return "T_CATEGORY_INFO"; }

        public void SetCategoryInfoId_Equal(decimal? v) { regCategoryInfoId(CK_EQ, v); }
        public void SetCategoryInfoId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryInfoId(CK_NES, v);
        }
        public void SetCategoryInfoId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryInfoId(CK_GT, v);
        }
        public void SetCategoryInfoId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryInfoId(CK_LT, v);
        }
        public void SetCategoryInfoId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryInfoId(CK_GE, v);
        }
        public void SetCategoryInfoId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regCategoryInfoId(CK_LE, v);
        }
        public void SetCategoryInfoId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueCategoryInfoId(), "CATEGORY_INFO_ID");
        }
        public void SetCategoryInfoId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueCategoryInfoId(), "CATEGORY_INFO_ID");
        }
        public void ExistsTMatrixInfoList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryInfoId_ExistsSubQuery_TMatrixInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "CATEGORY_INFO_ID", "Add_Fa_Category_Info_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryInfoId_ExistsSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery);
        public void NotExistsTMatrixInfoList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryInfoId_NotExistsSubQuery_TMatrixInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "CATEGORY_INFO_ID", "Add_Fa_Category_Info_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryInfoId_NotExistsSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery);
        public void InScopeTMatrixInfoList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryInfoId_InScopeSubQuery_TMatrixInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "CATEGORY_INFO_ID", "Add_Fa_Category_Info_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryInfoId_InScopeSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery);
        public void NotInScopeTMatrixInfoList(SubQuery<TMatrixInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryInfoId_NotInScopeSubQuery_TMatrixInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "CATEGORY_INFO_ID", "Add_Fa_Category_Info_ID", subQueryPropertyName);
        }
        public abstract String keepCategoryInfoId_NotInScopeSubQuery_TMatrixInfoList(TMatrixInfoCQ subQuery);
        public void xsderiveTMatrixInfoList(String function, SubQuery<TMatrixInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryInfoId_SpecifyDerivedReferrer_TMatrixInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "CATEGORY_INFO_ID", "Add_Fa_Category_Info_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepCategoryInfoId_SpecifyDerivedReferrer_TMatrixInfoList(TMatrixInfoCQ subQuery);

        public QDRFunction<TMatrixInfoCB> DerivedTMatrixInfoList() {
            return xcreateQDRFunctionTMatrixInfoList();
        }
        protected QDRFunction<TMatrixInfoCB> xcreateQDRFunctionTMatrixInfoList() {
            return new QDRFunction<TMatrixInfoCB>(delegate(String function, SubQuery<TMatrixInfoCB> subQuery, String operand, Object value) {
                xqderiveTMatrixInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTMatrixInfoList(String function, SubQuery<TMatrixInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TMatrixInfoCB>", subQuery);
            TMatrixInfoCB cb = new TMatrixInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepCategoryInfoId_QueryDerivedReferrer_TMatrixInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepCategoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "CATEGORY_INFO_ID", "Add_Fa_Category_Info_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepCategoryInfoId_QueryDerivedReferrer_TMatrixInfoList(TMatrixInfoCQ subQuery);
        public abstract String keepCategoryInfoId_QueryDerivedReferrer_TMatrixInfoListParameter(Object parameterValue);
        public void SetCategoryInfoId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryInfoId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCategoryInfoId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryInfoId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCategoryInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryInfoId(), "CATEGORY_INFO_ID");
        }
        protected abstract ConditionValue getCValueCategoryInfoId();

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
            registerInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_InScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        public void NotInScopeTItemInfo(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepItemInfoId_NotInScopeSubQuery_TItemInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "ITEM_INFO_ID", "ITEM_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepItemInfoId_NotInScopeSubQuery_TItemInfo(TItemInfoCQ subQuery);
        protected void regItemInfoId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueItemInfoId(), "ITEM_INFO_ID");
        }
        protected abstract ConditionValue getCValueItemInfoId();

        public void SetCategoryNo_Equal(int? v) { regCategoryNo(CK_EQ, v); }
        public void SetCategoryNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryNo(CK_NES, v);
        }
        public void SetCategoryNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryNo(CK_GT, v);
        }
        public void SetCategoryNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryNo(CK_LT, v);
        }
        public void SetCategoryNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryNo(CK_GE, v);
        }
        public void SetCategoryNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regCategoryNo(CK_LE, v);
        }
        public void SetCategoryNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueCategoryNo(), "CATEGORY_NO");
        }
        public void SetCategoryNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueCategoryNo(), "CATEGORY_NO");
        }
        protected void regCategoryNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryNo(), "CATEGORY_NO");
        }
        protected abstract ConditionValue getCValueCategoryNo();

        public void SetCategoryName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCategoryName_Equal(fRES(v));
        }
        protected void DoSetCategoryName_Equal(String v) { regCategoryName(CK_EQ, v); }
        public void SetCategoryName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCategoryName_NotEqual(fRES(v));
        }
        protected void DoSetCategoryName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_NES, v);
        }
        public void SetCategoryName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_GT, fRES(v));
        }
        public void SetCategoryName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_LT, fRES(v));
        }
        public void SetCategoryName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_GE, fRES(v));
        }
        public void SetCategoryName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_LE, fRES(v));
        }
        public void SetCategoryName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCategoryName(), "CATEGORY_NAME");
        }
        public void SetCategoryName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCategoryName(), "CATEGORY_NAME");
        }
        public void SetCategoryName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCategoryName_LikeSearch(v, cLSOP());
        }
        public void SetCategoryName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCategoryName(), "CATEGORY_NAME", option);
        }
        public void SetCategoryName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCategoryName(), "CATEGORY_NAME", option);
        }
        public void SetCategoryName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCategoryName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCategoryName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCategoryName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCategoryName(), "CATEGORY_NAME");
        }
        protected abstract ConditionValue getCValueCategoryName();

        public void SetWeightValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetWeightValue_Equal(fRES(v));
        }
        protected void DoSetWeightValue_Equal(String v) { regWeightValue(CK_EQ, v); }
        public void SetWeightValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetWeightValue_NotEqual(fRES(v));
        }
        protected void DoSetWeightValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_NES, v);
        }
        public void SetWeightValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_GT, fRES(v));
        }
        public void SetWeightValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_LT, fRES(v));
        }
        public void SetWeightValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_GE, fRES(v));
        }
        public void SetWeightValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_LE, fRES(v));
        }
        public void SetWeightValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueWeightValue(), "WEIGHT_VALUE");
        }
        public void SetWeightValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueWeightValue(), "WEIGHT_VALUE");
        }
        public void SetWeightValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetWeightValue_LikeSearch(v, cLSOP());
        }
        public void SetWeightValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueWeightValue(), "WEIGHT_VALUE", option);
        }
        public void SetWeightValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueWeightValue(), "WEIGHT_VALUE", option);
        }
        public void SetWeightValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetWeightValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWeightValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regWeightValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWeightValue(), "WEIGHT_VALUE");
        }
        protected abstract ConditionValue getCValueWeightValue();

        public void SetOriginalCategoryName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalCategoryName_Equal(fRES(v));
        }
        protected void DoSetOriginalCategoryName_Equal(String v) { regOriginalCategoryName(CK_EQ, v); }
        public void SetOriginalCategoryName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalCategoryName_NotEqual(fRES(v));
        }
        protected void DoSetOriginalCategoryName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_NES, v);
        }
        public void SetOriginalCategoryName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_GT, fRES(v));
        }
        public void SetOriginalCategoryName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_LT, fRES(v));
        }
        public void SetOriginalCategoryName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_GE, fRES(v));
        }
        public void SetOriginalCategoryName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_LE, fRES(v));
        }
        public void SetOriginalCategoryName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOriginalCategoryName(), "ORIGINAL_CATEGORY_NAME");
        }
        public void SetOriginalCategoryName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOriginalCategoryName(), "ORIGINAL_CATEGORY_NAME");
        }
        public void SetOriginalCategoryName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOriginalCategoryName_LikeSearch(v, cLSOP());
        }
        public void SetOriginalCategoryName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOriginalCategoryName(), "ORIGINAL_CATEGORY_NAME", option);
        }
        public void SetOriginalCategoryName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOriginalCategoryName(), "ORIGINAL_CATEGORY_NAME", option);
        }
        public void SetOriginalCategoryName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOriginalCategoryName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalCategoryName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOriginalCategoryName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOriginalCategoryName(), "ORIGINAL_CATEGORY_NAME");
        }
        protected abstract ConditionValue getCValueOriginalCategoryName();

        public void SetOriginalWeightValue_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalWeightValue_Equal(fRES(v));
        }
        protected void DoSetOriginalWeightValue_Equal(String v) { regOriginalWeightValue(CK_EQ, v); }
        public void SetOriginalWeightValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOriginalWeightValue_NotEqual(fRES(v));
        }
        protected void DoSetOriginalWeightValue_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_NES, v);
        }
        public void SetOriginalWeightValue_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_GT, fRES(v));
        }
        public void SetOriginalWeightValue_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_LT, fRES(v));
        }
        public void SetOriginalWeightValue_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_GE, fRES(v));
        }
        public void SetOriginalWeightValue_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_LE, fRES(v));
        }
        public void SetOriginalWeightValue_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOriginalWeightValue(), "ORIGINAL_WEIGHT_VALUE");
        }
        public void SetOriginalWeightValue_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOriginalWeightValue(), "ORIGINAL_WEIGHT_VALUE");
        }
        public void SetOriginalWeightValue_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetOriginalWeightValue_LikeSearch(v, cLSOP());
        }
        public void SetOriginalWeightValue_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueOriginalWeightValue(), "ORIGINAL_WEIGHT_VALUE", option);
        }
        public void SetOriginalWeightValue_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueOriginalWeightValue(), "ORIGINAL_WEIGHT_VALUE", option);
        }
        public void SetOriginalWeightValue_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOriginalWeightValue_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOriginalWeightValue(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOriginalWeightValue(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOriginalWeightValue(), "ORIGINAL_WEIGHT_VALUE");
        }
        protected abstract ConditionValue getCValueOriginalWeightValue();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TCategoryInfoCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TCategoryInfoCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TCategoryInfoCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TCategoryInfoCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TCategoryInfoCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TCategoryInfoCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TCategoryInfoCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TCategoryInfoCB>(delegate(String function, SubQuery<TCategoryInfoCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TCategoryInfoCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TCategoryInfoCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TCategoryInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TCategoryInfoCB>", subQuery);
            TCategoryInfoCB cb = new TCategoryInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "CATEGORY_INFO_ID", "CATEGORY_INFO_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TCategoryInfoCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
