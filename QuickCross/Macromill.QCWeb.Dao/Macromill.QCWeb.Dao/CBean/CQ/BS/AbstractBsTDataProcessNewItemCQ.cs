
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
    public abstract class AbstractBsTDataProcessNewItemCQ : AbstractConditionQuery {

        public AbstractBsTDataProcessNewItemCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DATA_PROCESS_NEW_ITEM"; }
        public override String getTableSqlName() { return "T_DATA_PROCESS_NEW_ITEM"; }

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
        public void ExistsTDataProcessNewCategoryList(SubQuery<TDataProcessNewCategoryCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TDataProcessNewCategoryList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery);
        public void ExistsTDataProcessNewItemSrcList(SubQuery<TDataProcessNewItemSrcCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TDataProcessNewItemSrcList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery);
        public void ExistsTIntegConditionList(SubQuery<TIntegConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TIntegConditionList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TIntegConditionList(TIntegConditionCQ subQuery);
        public void NotExistsTDataProcessNewCategoryList(SubQuery<TDataProcessNewCategoryCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TDataProcessNewCategoryList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery);
        public void NotExistsTDataProcessNewItemSrcList(SubQuery<TDataProcessNewItemSrcCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery);
        public void NotExistsTIntegConditionList(SubQuery<TIntegConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TIntegConditionList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TIntegConditionList(TIntegConditionCQ subQuery);
        public void InScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataEditList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void InScopeTDataProcessNewCategoryList(SubQuery<TDataProcessNewCategoryCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataProcessNewCategoryList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery);
        public void InScopeTDataProcessNewItemSrcList(SubQuery<TDataProcessNewItemSrcCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataProcessNewItemSrcList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery);
        public void InScopeTIntegConditionList(SubQuery<TIntegConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TIntegConditionList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TIntegConditionList(TIntegConditionCQ subQuery);
        public void NotInScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataEditList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void NotInScopeTDataProcessNewCategoryList(SubQuery<TDataProcessNewCategoryCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery);
        public void NotInScopeTDataProcessNewItemSrcList(SubQuery<TDataProcessNewItemSrcCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery);
        public void NotInScopeTIntegConditionList(SubQuery<TIntegConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TIntegConditionList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TIntegConditionList(TIntegConditionCQ subQuery);
        public void xsderiveTDataProcessNewCategoryList(String function, SubQuery<TDataProcessNewCategoryCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery);
        public void xsderiveTDataProcessNewItemSrcList(String function, SubQuery<TDataProcessNewItemSrcCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery);
        public void xsderiveTIntegConditionList(String function, SubQuery<TIntegConditionCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TIntegConditionList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TIntegConditionList(TIntegConditionCQ subQuery);

        public QDRFunction<TDataProcessNewCategoryCB> DerivedTDataProcessNewCategoryList() {
            return xcreateQDRFunctionTDataProcessNewCategoryList();
        }
        protected QDRFunction<TDataProcessNewCategoryCB> xcreateQDRFunctionTDataProcessNewCategoryList() {
            return new QDRFunction<TDataProcessNewCategoryCB>(delegate(String function, SubQuery<TDataProcessNewCategoryCB> subQuery, String operand, Object value) {
                xqderiveTDataProcessNewCategoryList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDataProcessNewCategoryList(String function, SubQuery<TDataProcessNewCategoryCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDataProcessNewCategoryCB>", subQuery);
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryList(TDataProcessNewCategoryCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TDataProcessNewCategoryListParameter(Object parameterValue);

        public QDRFunction<TDataProcessNewItemSrcCB> DerivedTDataProcessNewItemSrcList() {
            return xcreateQDRFunctionTDataProcessNewItemSrcList();
        }
        protected QDRFunction<TDataProcessNewItemSrcCB> xcreateQDRFunctionTDataProcessNewItemSrcList() {
            return new QDRFunction<TDataProcessNewItemSrcCB>(delegate(String function, SubQuery<TDataProcessNewItemSrcCB> subQuery, String operand, Object value) {
                xqderiveTDataProcessNewItemSrcList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDataProcessNewItemSrcList(String function, SubQuery<TDataProcessNewItemSrcCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDataProcessNewItemSrcCB>", subQuery);
            TDataProcessNewItemSrcCB cb = new TDataProcessNewItemSrcCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcList(TDataProcessNewItemSrcCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TDataProcessNewItemSrcListParameter(Object parameterValue);

        public QDRFunction<TIntegConditionCB> DerivedTIntegConditionList() {
            return xcreateQDRFunctionTIntegConditionList();
        }
        protected QDRFunction<TIntegConditionCB> xcreateQDRFunctionTIntegConditionList() {
            return new QDRFunction<TIntegConditionCB>(delegate(String function, SubQuery<TIntegConditionCB> subQuery, String operand, Object value) {
                xqderiveTIntegConditionList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTIntegConditionList(String function, SubQuery<TIntegConditionCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TIntegConditionCB>", subQuery);
            TIntegConditionCB cb = new TIntegConditionCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TIntegConditionList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TIntegConditionListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TIntegConditionList(TIntegConditionCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TIntegConditionListParameter(Object parameterValue);
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
        protected void regNewItemId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewItemId(), "NEW_ITEM_ID");
        }
        protected abstract ConditionValue getCValueNewItemId();

        public void SetNewItemName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewItemName_Equal(fRES(v));
        }
        protected void DoSetNewItemName_Equal(String v) { regNewItemName(CK_EQ, v); }
        public void SetNewItemName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewItemName_NotEqual(fRES(v));
        }
        protected void DoSetNewItemName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemName(CK_NES, v);
        }
        public void SetNewItemName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemName(CK_GT, fRES(v));
        }
        public void SetNewItemName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemName(CK_LT, fRES(v));
        }
        public void SetNewItemName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemName(CK_GE, fRES(v));
        }
        public void SetNewItemName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewItemName(CK_LE, fRES(v));
        }
        public void SetNewItemName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNewItemName(), "NEW_ITEM_NAME");
        }
        public void SetNewItemName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNewItemName(), "NEW_ITEM_NAME");
        }
        public void SetNewItemName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNewItemName_LikeSearch(v, cLSOP());
        }
        public void SetNewItemName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNewItemName(), "NEW_ITEM_NAME", option);
        }
        public void SetNewItemName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNewItemName(), "NEW_ITEM_NAME", option);
        }
        protected void regNewItemName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewItemName(), "NEW_ITEM_NAME");
        }
        protected abstract ConditionValue getCValueNewItemName();

        public void SetNewLv1title_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewLv1title_Equal(fRES(v));
        }
        protected void DoSetNewLv1title_Equal(String v) { regNewLv1title(CK_EQ, v); }
        public void SetNewLv1title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewLv1title_NotEqual(fRES(v));
        }
        protected void DoSetNewLv1title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_NES, v);
        }
        public void SetNewLv1title_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_GT, fRES(v));
        }
        public void SetNewLv1title_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_LT, fRES(v));
        }
        public void SetNewLv1title_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_GE, fRES(v));
        }
        public void SetNewLv1title_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_LE, fRES(v));
        }
        public void SetNewLv1title_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNewLv1title(), "NEW_LV1TITLE");
        }
        public void SetNewLv1title_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNewLv1title(), "NEW_LV1TITLE");
        }
        public void SetNewLv1title_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNewLv1title_LikeSearch(v, cLSOP());
        }
        public void SetNewLv1title_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNewLv1title(), "NEW_LV1TITLE", option);
        }
        public void SetNewLv1title_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNewLv1title(), "NEW_LV1TITLE", option);
        }
        public void SetNewLv1title_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNewLv1title_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv1title(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNewLv1title(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewLv1title(), "NEW_LV1TITLE");
        }
        protected abstract ConditionValue getCValueNewLv1title();

        public void SetNewLv2title_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewLv2title_Equal(fRES(v));
        }
        protected void DoSetNewLv2title_Equal(String v) { regNewLv2title(CK_EQ, v); }
        public void SetNewLv2title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewLv2title_NotEqual(fRES(v));
        }
        protected void DoSetNewLv2title_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_NES, v);
        }
        public void SetNewLv2title_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_GT, fRES(v));
        }
        public void SetNewLv2title_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_LT, fRES(v));
        }
        public void SetNewLv2title_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_GE, fRES(v));
        }
        public void SetNewLv2title_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_LE, fRES(v));
        }
        public void SetNewLv2title_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNewLv2title(), "NEW_LV2TITLE");
        }
        public void SetNewLv2title_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNewLv2title(), "NEW_LV2TITLE");
        }
        public void SetNewLv2title_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNewLv2title_LikeSearch(v, cLSOP());
        }
        public void SetNewLv2title_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNewLv2title(), "NEW_LV2TITLE", option);
        }
        public void SetNewLv2title_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNewLv2title(), "NEW_LV2TITLE", option);
        }
        public void SetNewLv2title_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNewLv2title_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewLv2title(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNewLv2title(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewLv2title(), "NEW_LV2TITLE");
        }
        protected abstract ConditionValue getCValueNewLv2title();

        public void SetNewAnswerType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewAnswerType_Equal(fRES(v));
        }
        protected void DoSetNewAnswerType_Equal(String v) { regNewAnswerType(CK_EQ, v); }
        public void SetNewAnswerType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetNewAnswerType_NotEqual(fRES(v));
        }
        protected void DoSetNewAnswerType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewAnswerType(CK_NES, v);
        }
        public void SetNewAnswerType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewAnswerType(CK_GT, fRES(v));
        }
        public void SetNewAnswerType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewAnswerType(CK_LT, fRES(v));
        }
        public void SetNewAnswerType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewAnswerType(CK_GE, fRES(v));
        }
        public void SetNewAnswerType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewAnswerType(CK_LE, fRES(v));
        }
        public void SetNewAnswerType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueNewAnswerType(), "NEW_ANSWER_TYPE");
        }
        public void SetNewAnswerType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueNewAnswerType(), "NEW_ANSWER_TYPE");
        }
        public void SetNewAnswerType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetNewAnswerType_LikeSearch(v, cLSOP());
        }
        public void SetNewAnswerType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueNewAnswerType(), "NEW_ANSWER_TYPE", option);
        }
        public void SetNewAnswerType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueNewAnswerType(), "NEW_ANSWER_TYPE", option);
        }
        protected void regNewAnswerType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewAnswerType(), "NEW_ANSWER_TYPE");
        }
        protected abstract ConditionValue getCValueNewAnswerType();

        public void SetNewCategoryCount_Equal(int? v) { regNewCategoryCount(CK_EQ, v); }
        public void SetNewCategoryCount_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryCount(CK_NES, v);
        }
        public void SetNewCategoryCount_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryCount(CK_GT, v);
        }
        public void SetNewCategoryCount_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryCount(CK_LT, v);
        }
        public void SetNewCategoryCount_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNewCategoryCount(CK_GE, v);
        }
        public void SetNewCategoryCount_LessEqual(int? v) {
            WhereSetterFlag = true;
            regNewCategoryCount(CK_LE, v);
        }
        public void SetNewCategoryCount_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNewCategoryCount(), "NEW_CATEGORY_COUNT");
        }
        public void SetNewCategoryCount_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNewCategoryCount(), "NEW_CATEGORY_COUNT");
        }
        protected void regNewCategoryCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNewCategoryCount(), "NEW_CATEGORY_COUNT");
        }
        protected abstract ConditionValue getCValueNewCategoryCount();

        public void SetUnfitFlag_Equal(int? v) { regUnfitFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of unfitFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetUnfitFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUnfitFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of unfitFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUnfitFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUnfitFlag(CK_EQ, int.Parse(code));
        }
        public void SetUnfitFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnfitFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of unfitFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetUnfitFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUnfitFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of unfitFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUnfitFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUnfitFlag(CK_NES, int.Parse(code));
        }
        public void SetUnfitFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUnfitFlag(), "UNFIT_FLAG");
        }
        public void SetUnfitFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUnfitFlag(), "UNFIT_FLAG");
        }
        protected void regUnfitFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUnfitFlag(), "UNFIT_FLAG");
        }
        protected abstract ConditionValue getCValueUnfitFlag();

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

        public void SetSeriesFlag_Equal(int? v) { regSeriesFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of seriesFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetSeriesFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSeriesFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of seriesFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSeriesFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSeriesFlag(CK_EQ, int.Parse(code));
        }
        public void SetSeriesFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSeriesFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of seriesFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetSeriesFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regSeriesFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of seriesFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetSeriesFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regSeriesFlag(CK_NES, int.Parse(code));
        }
        public void SetSeriesFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSeriesFlag(), "SERIES_FLAG");
        }
        public void SetSeriesFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSeriesFlag(), "SERIES_FLAG");
        }
        protected void regSeriesFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSeriesFlag(), "SERIES_FLAG");
        }
        protected abstract ConditionValue getCValueSeriesFlag();

        public void SetUpperFlag_Equal(int? v) { regUpperFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of upperFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetUpperFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUpperFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of upperFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUpperFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUpperFlag(CK_EQ, int.Parse(code));
        }
        public void SetUpperFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUpperFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of upperFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetUpperFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regUpperFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of upperFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetUpperFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regUpperFlag(CK_NES, int.Parse(code));
        }
        public void SetUpperFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUpperFlag(), "UPPER_FLAG");
        }
        public void SetUpperFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUpperFlag(), "UPPER_FLAG");
        }
        protected void regUpperFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUpperFlag(), "UPPER_FLAG");
        }
        protected abstract ConditionValue getCValueUpperFlag();

        public void SetBottomFlag_Equal(int? v) { regBottomFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of bottomFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetBottomFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regBottomFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of bottomFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetBottomFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regBottomFlag(CK_EQ, int.Parse(code));
        }
        public void SetBottomFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBottomFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of bottomFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetBottomFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regBottomFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of bottomFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetBottomFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regBottomFlag(CK_NES, int.Parse(code));
        }
        public void SetBottomFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueBottomFlag(), "BOTTOM_FLAG");
        }
        public void SetBottomFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueBottomFlag(), "BOTTOM_FLAG");
        }
        protected void regBottomFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBottomFlag(), "BOTTOM_FLAG");
        }
        protected abstract ConditionValue getCValueBottomFlag();

        public void SetNoanswerZeroFlag_Equal(int? v) { regNoanswerZeroFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of noanswerZeroFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerZeroFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerZeroFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerZeroFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerZeroFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerZeroFlag(CK_EQ, int.Parse(code));
        }
        public void SetNoanswerZeroFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerZeroFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of noanswerZeroFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetNoanswerZeroFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regNoanswerZeroFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of noanswerZeroFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetNoanswerZeroFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regNoanswerZeroFlag(CK_NES, int.Parse(code));
        }
        public void SetNoanswerZeroFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNoanswerZeroFlag(), "NOANSWER_ZERO_FLAG");
        }
        public void SetNoanswerZeroFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNoanswerZeroFlag(), "NOANSWER_ZERO_FLAG");
        }
        protected void regNoanswerZeroFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoanswerZeroFlag(), "NOANSWER_ZERO_FLAG");
        }
        protected abstract ConditionValue getCValueNoanswerZeroFlag();

        public void SetSelectMethod_Equal(int? v) { regSelectMethod(CK_EQ, v); }
        public void SetSelectMethod_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectMethod(CK_NES, v);
        }
        public void SetSelectMethod_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectMethod(CK_GT, v);
        }
        public void SetSelectMethod_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectMethod(CK_LT, v);
        }
        public void SetSelectMethod_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSelectMethod(CK_GE, v);
        }
        public void SetSelectMethod_LessEqual(int? v) {
            WhereSetterFlag = true;
            regSelectMethod(CK_LE, v);
        }
        public void SetSelectMethod_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueSelectMethod(), "SELECT_METHOD");
        }
        public void SetSelectMethod_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueSelectMethod(), "SELECT_METHOD");
        }
        protected void regSelectMethod(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSelectMethod(), "SELECT_METHOD");
        }
        protected abstract ConditionValue getCValueSelectMethod();

        public void SetTargetCategoryCondition_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTargetCategoryCondition_Equal(fRES(v));
        }
        protected void DoSetTargetCategoryCondition_Equal(String v) { regTargetCategoryCondition(CK_EQ, v); }
        public void SetTargetCategoryCondition_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTargetCategoryCondition_NotEqual(fRES(v));
        }
        protected void DoSetTargetCategoryCondition_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_NES, v);
        }
        public void SetTargetCategoryCondition_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_GT, fRES(v));
        }
        public void SetTargetCategoryCondition_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_LT, fRES(v));
        }
        public void SetTargetCategoryCondition_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_GE, fRES(v));
        }
        public void SetTargetCategoryCondition_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_LE, fRES(v));
        }
        public void SetTargetCategoryCondition_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTargetCategoryCondition(), "TARGET_CATEGORY_CONDITION");
        }
        public void SetTargetCategoryCondition_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTargetCategoryCondition(), "TARGET_CATEGORY_CONDITION");
        }
        public void SetTargetCategoryCondition_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTargetCategoryCondition_LikeSearch(v, cLSOP());
        }
        public void SetTargetCategoryCondition_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTargetCategoryCondition(), "TARGET_CATEGORY_CONDITION", option);
        }
        public void SetTargetCategoryCondition_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTargetCategoryCondition(), "TARGET_CATEGORY_CONDITION", option);
        }
        public void SetTargetCategoryCondition_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTargetCategoryCondition_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetCategoryCondition(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTargetCategoryCondition(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTargetCategoryCondition(), "TARGET_CATEGORY_CONDITION");
        }
        protected abstract ConditionValue getCValueTargetCategoryCondition();

        public void SetCalcType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCalcType_Equal(fRES(v));
        }
        protected void DoSetCalcType_Equal(String v) { regCalcType(CK_EQ, v); }
        public void SetCalcType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetCalcType_NotEqual(fRES(v));
        }
        protected void DoSetCalcType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_NES, v);
        }
        public void SetCalcType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_GT, fRES(v));
        }
        public void SetCalcType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_LT, fRES(v));
        }
        public void SetCalcType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_GE, fRES(v));
        }
        public void SetCalcType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_LE, fRES(v));
        }
        public void SetCalcType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueCalcType(), "CALC_TYPE");
        }
        public void SetCalcType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueCalcType(), "CALC_TYPE");
        }
        public void SetCalcType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetCalcType_LikeSearch(v, cLSOP());
        }
        public void SetCalcType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueCalcType(), "CALC_TYPE", option);
        }
        public void SetCalcType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueCalcType(), "CALC_TYPE", option);
        }
        public void SetCalcType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetCalcType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regCalcType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regCalcType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueCalcType(), "CALC_TYPE");
        }
        protected abstract ConditionValue getCValueCalcType();

        public void SetFormulaString_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFormulaString_Equal(fRES(v));
        }
        protected void DoSetFormulaString_Equal(String v) { regFormulaString(CK_EQ, v); }
        public void SetFormulaString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFormulaString_NotEqual(fRES(v));
        }
        protected void DoSetFormulaString_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_NES, v);
        }
        public void SetFormulaString_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_GT, fRES(v));
        }
        public void SetFormulaString_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_LT, fRES(v));
        }
        public void SetFormulaString_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_GE, fRES(v));
        }
        public void SetFormulaString_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_LE, fRES(v));
        }
        public void SetFormulaString_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFormulaString(), "FORMULA_STRING");
        }
        public void SetFormulaString_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFormulaString(), "FORMULA_STRING");
        }
        public void SetFormulaString_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFormulaString_LikeSearch(v, cLSOP());
        }
        public void SetFormulaString_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFormulaString(), "FORMULA_STRING", option);
        }
        public void SetFormulaString_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFormulaString(), "FORMULA_STRING", option);
        }
        public void SetFormulaString_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFormulaString_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFormulaString(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFormulaString(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFormulaString(), "FORMULA_STRING");
        }
        protected abstract ConditionValue getCValueFormulaString();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDataProcessNewItemCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDataProcessNewItemCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDataProcessNewItemCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDataProcessNewItemCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDataProcessNewItemCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDataProcessNewItemCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDataProcessNewItemCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDataProcessNewItemCB>(delegate(String function, SubQuery<TDataProcessNewItemCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDataProcessNewItemCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDataProcessNewItemCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDataProcessNewItemCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
