
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
    public abstract class AbstractBsTEditMenuMasterCQ : AbstractConditionQuery {

        public AbstractBsTEditMenuMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_EDIT_MENU_MASTER"; }
        public override String getTableSqlName() { return "T_EDIT_MENU_MASTER"; }

        public void SetEditMenuMasterId_Equal(int? v) { regEditMenuMasterId(CK_EQ, v); }
        public void SetEditMenuMasterId_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMenuMasterId(CK_NES, v);
        }
        public void SetEditMenuMasterId_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMenuMasterId(CK_GT, v);
        }
        public void SetEditMenuMasterId_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMenuMasterId(CK_LT, v);
        }
        public void SetEditMenuMasterId_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMenuMasterId(CK_GE, v);
        }
        public void SetEditMenuMasterId_LessEqual(int? v) {
            WhereSetterFlag = true;
            regEditMenuMasterId(CK_LE, v);
        }
        public void SetEditMenuMasterId_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueEditMenuMasterId(), "EDIT_MENU_MASTER_ID");
        }
        public void SetEditMenuMasterId_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueEditMenuMasterId(), "EDIT_MENU_MASTER_ID");
        }
        public void ExistsTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_ExistsSubQuery_TDataEditListList(cb.Query());
            registerExistsSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepEditMenuMasterId_ExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void NotExistsTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_NotExistsSubQuery_TDataEditListList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepEditMenuMasterId_NotExistsSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void InScopeTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_InScopeSubQuery_TDataEditListList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepEditMenuMasterId_InScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void NotInScopeTDataEditListList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_NotInScopeSubQuery_TDataEditListList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepEditMenuMasterId_NotInScopeSubQuery_TDataEditListList(TDataEditListCQ subQuery);
        public void xsderiveTDataEditListList(String function, SubQuery<TDataEditListCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_SpecifyDerivedReferrer_TDataEditListList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepEditMenuMasterId_SpecifyDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery);

        public QDRFunction<TDataEditListCB> DerivedTDataEditListList() {
            return xcreateQDRFunctionTDataEditListList();
        }
        protected QDRFunction<TDataEditListCB> xcreateQDRFunctionTDataEditListList() {
            return new QDRFunction<TDataEditListCB>(delegate(String function, SubQuery<TDataEditListCB> subQuery, String operand, Object value) {
                xqderiveTDataEditListList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDataEditListList(String function, SubQuery<TDataEditListCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_QueryDerivedReferrer_TDataEditListList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepEditMenuMasterId_QueryDerivedReferrer_TDataEditListListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepEditMenuMasterId_QueryDerivedReferrer_TDataEditListList(TDataEditListCQ subQuery);
        public abstract String keepEditMenuMasterId_QueryDerivedReferrer_TDataEditListListParameter(Object parameterValue);
        public void SetEditMenuMasterId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMenuMasterId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetEditMenuMasterId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditMenuMasterId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regEditMenuMasterId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditMenuMasterId(), "EDIT_MENU_MASTER_ID");
        }
        protected abstract ConditionValue getCValueEditMenuMasterId();

        public void SetEditClassification_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetEditClassification_Equal(fRES(v));
        }
        protected void DoSetEditClassification_Equal(String v) { regEditClassification(CK_EQ, v); }
        public void SetEditClassification_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetEditClassification_NotEqual(fRES(v));
        }
        protected void DoSetEditClassification_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_NES, v);
        }
        public void SetEditClassification_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_GT, fRES(v));
        }
        public void SetEditClassification_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_LT, fRES(v));
        }
        public void SetEditClassification_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_GE, fRES(v));
        }
        public void SetEditClassification_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_LE, fRES(v));
        }
        public void SetEditClassification_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueEditClassification(), "EDIT_CLASSIFICATION");
        }
        public void SetEditClassification_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueEditClassification(), "EDIT_CLASSIFICATION");
        }
        public void SetEditClassification_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetEditClassification_LikeSearch(v, cLSOP());
        }
        public void SetEditClassification_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueEditClassification(), "EDIT_CLASSIFICATION", option);
        }
        public void SetEditClassification_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueEditClassification(), "EDIT_CLASSIFICATION", option);
        }
        public void SetEditClassification_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_ISN, DUMMY_OBJECT);
        }
        public void SetEditClassification_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditClassification(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regEditClassification(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditClassification(), "EDIT_CLASSIFICATION");
        }
        protected abstract ConditionValue getCValueEditClassification();

        public void SetProcessType_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetProcessType_Equal(fRES(v));
        }
        protected void DoSetProcessType_Equal(String v) { regProcessType(CK_EQ, v); }
        public void SetProcessType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetProcessType_NotEqual(fRES(v));
        }
        protected void DoSetProcessType_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_NES, v);
        }
        public void SetProcessType_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_GT, fRES(v));
        }
        public void SetProcessType_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_LT, fRES(v));
        }
        public void SetProcessType_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_GE, fRES(v));
        }
        public void SetProcessType_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_LE, fRES(v));
        }
        public void SetProcessType_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueProcessType(), "PROCESS_TYPE");
        }
        public void SetProcessType_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueProcessType(), "PROCESS_TYPE");
        }
        public void SetProcessType_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetProcessType_LikeSearch(v, cLSOP());
        }
        public void SetProcessType_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueProcessType(), "PROCESS_TYPE", option);
        }
        public void SetProcessType_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueProcessType(), "PROCESS_TYPE", option);
        }
        public void SetProcessType_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_ISN, DUMMY_OBJECT);
        }
        public void SetProcessType_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessType(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regProcessType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcessType(), "PROCESS_TYPE");
        }
        protected abstract ConditionValue getCValueProcessType();

        public void SetExplanation_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetExplanation_Equal(fRES(v));
        }
        protected void DoSetExplanation_Equal(String v) { regExplanation(CK_EQ, v); }
        public void SetExplanation_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetExplanation_NotEqual(fRES(v));
        }
        protected void DoSetExplanation_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_NES, v);
        }
        public void SetExplanation_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_GT, fRES(v));
        }
        public void SetExplanation_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_LT, fRES(v));
        }
        public void SetExplanation_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_GE, fRES(v));
        }
        public void SetExplanation_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_LE, fRES(v));
        }
        public void SetExplanation_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueExplanation(), "EXPLANATION");
        }
        public void SetExplanation_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueExplanation(), "EXPLANATION");
        }
        public void SetExplanation_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetExplanation_LikeSearch(v, cLSOP());
        }
        public void SetExplanation_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueExplanation(), "EXPLANATION", option);
        }
        public void SetExplanation_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueExplanation(), "EXPLANATION", option);
        }
        public void SetExplanation_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExplanation_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExplanation(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExplanation(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExplanation(), "EXPLANATION");
        }
        protected abstract ConditionValue getCValueExplanation();

        public void SetExample_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetExample_Equal(fRES(v));
        }
        protected void DoSetExample_Equal(String v) { regExample(CK_EQ, v); }
        public void SetExample_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetExample_NotEqual(fRES(v));
        }
        protected void DoSetExample_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_NES, v);
        }
        public void SetExample_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_GT, fRES(v));
        }
        public void SetExample_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_LT, fRES(v));
        }
        public void SetExample_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_GE, fRES(v));
        }
        public void SetExample_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_LE, fRES(v));
        }
        public void SetExample_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueExample(), "EXAMPLE");
        }
        public void SetExample_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueExample(), "EXAMPLE");
        }
        public void SetExample_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetExample_LikeSearch(v, cLSOP());
        }
        public void SetExample_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueExample(), "EXAMPLE", option);
        }
        public void SetExample_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueExample(), "EXAMPLE", option);
        }
        public void SetExample_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExample_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExample(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExample(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExample(), "EXAMPLE");
        }
        protected abstract ConditionValue getCValueExample();

        public void SetDetailedexplanation_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDetailedexplanation_Equal(fRES(v));
        }
        protected void DoSetDetailedexplanation_Equal(String v) { regDetailedexplanation(CK_EQ, v); }
        public void SetDetailedexplanation_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDetailedexplanation_NotEqual(fRES(v));
        }
        protected void DoSetDetailedexplanation_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_NES, v);
        }
        public void SetDetailedexplanation_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_GT, fRES(v));
        }
        public void SetDetailedexplanation_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_LT, fRES(v));
        }
        public void SetDetailedexplanation_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_GE, fRES(v));
        }
        public void SetDetailedexplanation_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_LE, fRES(v));
        }
        public void SetDetailedexplanation_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueDetailedexplanation(), "DETAILEDEXPLANATION");
        }
        public void SetDetailedexplanation_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueDetailedexplanation(), "DETAILEDEXPLANATION");
        }
        public void SetDetailedexplanation_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDetailedexplanation_LikeSearch(v, cLSOP());
        }
        public void SetDetailedexplanation_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueDetailedexplanation(), "DETAILEDEXPLANATION", option);
        }
        public void SetDetailedexplanation_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueDetailedexplanation(), "DETAILEDEXPLANATION", option);
        }
        public void SetDetailedexplanation_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDetailedexplanation_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDetailedexplanation(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDetailedexplanation(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDetailedexplanation(), "DETAILEDEXPLANATION");
        }
        protected abstract ConditionValue getCValueDetailedexplanation();

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

        public void SetTypeBitUnion_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTypeBitUnion_Equal(fRES(v));
        }
        protected void DoSetTypeBitUnion_Equal(String v) { regTypeBitUnion(CK_EQ, v); }
        public void SetTypeBitUnion_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTypeBitUnion_NotEqual(fRES(v));
        }
        protected void DoSetTypeBitUnion_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_NES, v);
        }
        public void SetTypeBitUnion_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_GT, fRES(v));
        }
        public void SetTypeBitUnion_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_LT, fRES(v));
        }
        public void SetTypeBitUnion_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_GE, fRES(v));
        }
        public void SetTypeBitUnion_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_LE, fRES(v));
        }
        public void SetTypeBitUnion_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTypeBitUnion(), "TYPE_BIT_UNION");
        }
        public void SetTypeBitUnion_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTypeBitUnion(), "TYPE_BIT_UNION");
        }
        public void SetTypeBitUnion_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTypeBitUnion_LikeSearch(v, cLSOP());
        }
        public void SetTypeBitUnion_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTypeBitUnion(), "TYPE_BIT_UNION", option);
        }
        public void SetTypeBitUnion_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTypeBitUnion(), "TYPE_BIT_UNION", option);
        }
        public void SetTypeBitUnion_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTypeBitUnion_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTypeBitUnion(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTypeBitUnion(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTypeBitUnion(), "TYPE_BIT_UNION");
        }
        protected abstract ConditionValue getCValueTypeBitUnion();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TEditMenuMasterCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TEditMenuMasterCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TEditMenuMasterCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TEditMenuMasterCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TEditMenuMasterCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TEditMenuMasterCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TEditMenuMasterCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TEditMenuMasterCB>(delegate(String function, SubQuery<TEditMenuMasterCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TEditMenuMasterCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TEditMenuMasterCB>", subQuery);
            TEditMenuMasterCB cb = new TEditMenuMasterCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TEditMenuMasterCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TEditMenuMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditMenuMasterCB>", subQuery);
            TEditMenuMasterCB cb = new TEditMenuMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TEditMenuMasterCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
