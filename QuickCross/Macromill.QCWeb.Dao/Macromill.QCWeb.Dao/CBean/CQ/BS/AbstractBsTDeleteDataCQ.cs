
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
    public abstract class AbstractBsTDeleteDataCQ : AbstractConditionQuery {

        public AbstractBsTDeleteDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DELETE_DATA"; }
        public override String getTableSqlName() { return "T_DELETE_DATA"; }

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
        public void ExistsTDeleteConditionList(SubQuery<TDeleteConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TDeleteConditionList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery);
        public void ExistsTDeleteSampleIdListList(SubQuery<TDeleteSampleIdListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TDeleteSampleIdListList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery);
        public void NotExistsTDeleteConditionList(SubQuery<TDeleteConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TDeleteConditionList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery);
        public void NotExistsTDeleteSampleIdListList(SubQuery<TDeleteSampleIdListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TDeleteSampleIdListList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery);
        public void InScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataEditList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void InScopeTDeleteConditionList(SubQuery<TDeleteConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDeleteConditionList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery);
        public void InScopeTDeleteSampleIdListList(SubQuery<TDeleteSampleIdListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDeleteSampleIdListList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery);
        public void NotInScopeTDataEditList(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataEditList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataEditList(TDataEditListCQ subQuery);
        public void NotInScopeTDeleteConditionList(SubQuery<TDeleteConditionCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDeleteConditionList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDeleteConditionList(TDeleteConditionCQ subQuery);
        public void NotInScopeTDeleteSampleIdListList(SubQuery<TDeleteSampleIdListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDeleteSampleIdListList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery);
        public void xsderiveTDeleteConditionList(String function, SubQuery<TDeleteConditionCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TDeleteConditionList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TDeleteConditionList(TDeleteConditionCQ subQuery);
        public void xsderiveTDeleteSampleIdListList(String function, SubQuery<TDeleteSampleIdListCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery);

        public QDRFunction<TDeleteConditionCB> DerivedTDeleteConditionList() {
            return xcreateQDRFunctionTDeleteConditionList();
        }
        protected QDRFunction<TDeleteConditionCB> xcreateQDRFunctionTDeleteConditionList() {
            return new QDRFunction<TDeleteConditionCB>(delegate(String function, SubQuery<TDeleteConditionCB> subQuery, String operand, Object value) {
                xqderiveTDeleteConditionList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDeleteConditionList(String function, SubQuery<TDeleteConditionCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDeleteConditionCB>", subQuery);
            TDeleteConditionCB cb = new TDeleteConditionCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TDeleteConditionList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TDeleteConditionListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TDeleteConditionList(TDeleteConditionCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TDeleteConditionListParameter(Object parameterValue);

        public QDRFunction<TDeleteSampleIdListCB> DerivedTDeleteSampleIdListList() {
            return xcreateQDRFunctionTDeleteSampleIdListList();
        }
        protected QDRFunction<TDeleteSampleIdListCB> xcreateQDRFunctionTDeleteSampleIdListList() {
            return new QDRFunction<TDeleteSampleIdListCB>(delegate(String function, SubQuery<TDeleteSampleIdListCB> subQuery, String operand, Object value) {
                xqderiveTDeleteSampleIdListList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTDeleteSampleIdListList(String function, SubQuery<TDeleteSampleIdListCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TDeleteSampleIdListList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TDeleteSampleIdListList(TDeleteSampleIdListCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TDeleteSampleIdListListParameter(Object parameterValue);
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

        public void SetDeleteType_Equal(int? v) { regDeleteType(CK_EQ, v); }
        public void SetDeleteType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteType(CK_NES, v);
        }
        public void SetDeleteType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteType(CK_GT, v);
        }
        public void SetDeleteType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteType(CK_LT, v);
        }
        public void SetDeleteType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteType(CK_GE, v);
        }
        public void SetDeleteType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regDeleteType(CK_LE, v);
        }
        public void SetDeleteType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDeleteType(), "DELETE_TYPE");
        }
        public void SetDeleteType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDeleteType(), "DELETE_TYPE");
        }
        protected void regDeleteType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteType(), "DELETE_TYPE");
        }
        protected abstract ConditionValue getCValueDeleteType();

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
        public SSQFunction<TDeleteDataCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDeleteDataCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDeleteDataCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDeleteDataCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDeleteDataCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDeleteDataCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDeleteDataCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDeleteDataCB>(delegate(String function, SubQuery<TDeleteDataCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDeleteDataCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDeleteDataCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDeleteDataCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
