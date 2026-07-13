
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
    public abstract class AbstractBsTDeleteSampleIdListCQ : AbstractConditionQuery {

        public AbstractBsTDeleteSampleIdListCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DELETE_SAMPLE_ID_LIST"; }
        public override String getTableSqlName() { return "T_DELETE_SAMPLE_ID_LIST"; }

        public void SetDeleteSampleId_Equal(decimal? v) { regDeleteSampleId(CK_EQ, v); }
        public void SetDeleteSampleId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleId(CK_NES, v);
        }
        public void SetDeleteSampleId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleId(CK_GT, v);
        }
        public void SetDeleteSampleId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleId(CK_LT, v);
        }
        public void SetDeleteSampleId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleId(CK_GE, v);
        }
        public void SetDeleteSampleId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDeleteSampleId(CK_LE, v);
        }
        public void SetDeleteSampleId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDeleteSampleId(), "DELETE_SAMPLE_ID");
        }
        public void SetDeleteSampleId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDeleteSampleId(), "DELETE_SAMPLE_ID");
        }
        public void SetDeleteSampleId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDeleteSampleId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDeleteSampleId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteSampleId(), "DELETE_SAMPLE_ID");
        }
        protected abstract ConditionValue getCValueDeleteSampleId();

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
        public void InScopeTDeleteData(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDeleteData(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDeleteData(TDeleteDataCQ subQuery);
        public void NotInScopeTDeleteData(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDeleteData(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDeleteData(TDeleteDataCQ subQuery);
        protected void regDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDataEditId(), "DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDataEditId();

        public void SetDeleteSampleIdText_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDeleteSampleIdText_Equal(fRES(v));
        }
        protected void DoSetDeleteSampleIdText_Equal(String v) { regDeleteSampleIdText(CK_EQ, v); }
        public void SetDeleteSampleIdText_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDeleteSampleIdText_NotEqual(fRES(v));
        }
        protected void DoSetDeleteSampleIdText_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_NES, v);
        }
        public void SetDeleteSampleIdText_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_GT, fRES(v));
        }
        public void SetDeleteSampleIdText_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_LT, fRES(v));
        }
        public void SetDeleteSampleIdText_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_GE, fRES(v));
        }
        public void SetDeleteSampleIdText_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_LE, fRES(v));
        }
        public void SetDeleteSampleIdText_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueDeleteSampleIdText(), "DELETE_SAMPLE_ID_TEXT");
        }
        public void SetDeleteSampleIdText_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueDeleteSampleIdText(), "DELETE_SAMPLE_ID_TEXT");
        }
        public void SetDeleteSampleIdText_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDeleteSampleIdText_LikeSearch(v, cLSOP());
        }
        public void SetDeleteSampleIdText_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueDeleteSampleIdText(), "DELETE_SAMPLE_ID_TEXT", option);
        }
        public void SetDeleteSampleIdText_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueDeleteSampleIdText(), "DELETE_SAMPLE_ID_TEXT", option);
        }
        public void SetDeleteSampleIdText_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDeleteSampleIdText_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteSampleIdText(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDeleteSampleIdText(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteSampleIdText(), "DELETE_SAMPLE_ID_TEXT");
        }
        protected abstract ConditionValue getCValueDeleteSampleIdText();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDeleteSampleIdListCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDeleteSampleIdListCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDeleteSampleIdListCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDeleteSampleIdListCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDeleteSampleIdListCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDeleteSampleIdListCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDeleteSampleIdListCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDeleteSampleIdListCB>(delegate(String function, SubQuery<TDeleteSampleIdListCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDeleteSampleIdListCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDeleteSampleIdListCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDeleteSampleIdListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteSampleIdListCB>", subQuery);
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DELETE_SAMPLE_ID", "DELETE_SAMPLE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDeleteSampleIdListCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
