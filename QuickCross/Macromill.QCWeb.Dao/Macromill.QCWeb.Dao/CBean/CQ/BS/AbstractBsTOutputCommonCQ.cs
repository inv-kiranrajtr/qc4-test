
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
    public abstract class AbstractBsTOutputCommonCQ : AbstractConditionQuery {

        public AbstractBsTOutputCommonCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_COMMON"; }
        public override String getTableSqlName() { return "T_OUTPUT_COMMON"; }

        public void SetOutputCommonId_Equal(decimal? v) { regOutputCommonId(CK_EQ, v); }
        public void SetOutputCommonId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_NES, v);
        }
        public void SetOutputCommonId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GT, v);
        }
        public void SetOutputCommonId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_LT, v);
        }
        public void SetOutputCommonId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_GE, v);
        }
        public void SetOutputCommonId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputCommonId(CK_LE, v);
        }
        public void SetOutputCommonId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void SetOutputCommonId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        public void ExistsTOutputSubCklistList(SubQuery<TOutputSubCklistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_ExistsSubQuery_TOutputSubCklistList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_ExistsSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery);
        public void ExistsTOutputSubCrossList(SubQuery<TOutputSubCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_ExistsSubQuery_TOutputSubCrossList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_ExistsSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery);
        public void ExistsTOutputSubFaList(SubQuery<TOutputSubFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_ExistsSubQuery_TOutputSubFaList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_ExistsSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery);
        public void ExistsTOutputSubGtList(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_ExistsSubQuery_TOutputSubGtList(cb.Query());
            registerExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_ExistsSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery);
        public void NotExistsTOutputSubCklistList(SubQuery<TOutputSubCklistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotExistsSubQuery_TOutputSubCklistList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotExistsSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery);
        public void NotExistsTOutputSubCrossList(SubQuery<TOutputSubCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotExistsSubQuery_TOutputSubCrossList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotExistsSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery);
        public void NotExistsTOutputSubFaList(SubQuery<TOutputSubFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotExistsSubQuery_TOutputSubFaList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotExistsSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery);
        public void NotExistsTOutputSubGtList(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotExistsSubQuery_TOutputSubGtList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotExistsSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery);
        public void InScopeTOutputSubGt(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputSubGt(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "Output_Common_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputSubGt(TOutputSubGtCQ subQuery);
        public void InScopeTOutputSubCklistList(SubQuery<TOutputSubCklistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputSubCklistList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery);
        public void InScopeTOutputSubCrossList(SubQuery<TOutputSubCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputSubCrossList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery);
        public void InScopeTOutputSubFaList(SubQuery<TOutputSubFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputSubFaList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery);
        public void InScopeTOutputSubGtList(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_InScopeSubQuery_TOutputSubGtList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_InScopeSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery);
        public void NotInScopeTOutputSubGt(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputSubGt(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "Output_Common_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputSubGt(TOutputSubGtCQ subQuery);
        public void NotInScopeTOutputSubCklistList(SubQuery<TOutputSubCklistCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputSubCklistList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputSubCklistList(TOutputSubCklistCQ subQuery);
        public void NotInScopeTOutputSubCrossList(SubQuery<TOutputSubCrossCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputSubCrossList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputSubCrossList(TOutputSubCrossCQ subQuery);
        public void NotInScopeTOutputSubFaList(SubQuery<TOutputSubFaCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputSubFaList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputSubFaList(TOutputSubFaCQ subQuery);
        public void NotInScopeTOutputSubGtList(SubQuery<TOutputSubGtCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_NotInScopeSubQuery_TOutputSubGtList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepOutputCommonId_NotInScopeSubQuery_TOutputSubGtList(TOutputSubGtCQ subQuery);
        public void xsderiveTOutputSubCklistList(String function, SubQuery<TOutputSubCklistCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCklistList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCklistList(TOutputSubCklistCQ subQuery);
        public void xsderiveTOutputSubCrossList(String function, SubQuery<TOutputSubCrossCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCrossList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubCrossList(TOutputSubCrossCQ subQuery);
        public void xsderiveTOutputSubFaList(String function, SubQuery<TOutputSubFaCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubFaList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubFaList(TOutputSubFaCQ subQuery);
        public void xsderiveTOutputSubGtList(String function, SubQuery<TOutputSubGtCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubGtList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepOutputCommonId_SpecifyDerivedReferrer_TOutputSubGtList(TOutputSubGtCQ subQuery);

        public QDRFunction<TOutputSubCklistCB> DerivedTOutputSubCklistList() {
            return xcreateQDRFunctionTOutputSubCklistList();
        }
        protected QDRFunction<TOutputSubCklistCB> xcreateQDRFunctionTOutputSubCklistList() {
            return new QDRFunction<TOutputSubCklistCB>(delegate(String function, SubQuery<TOutputSubCklistCB> subQuery, String operand, Object value) {
                xqderiveTOutputSubCklistList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputSubCklistList(String function, SubQuery<TOutputSubCklistCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputSubCklistCB>", subQuery);
            TOutputSubCklistCB cb = new TOutputSubCklistCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistList(TOutputSubCklistCQ subQuery);
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCklistListParameter(Object parameterValue);

        public QDRFunction<TOutputSubCrossCB> DerivedTOutputSubCrossList() {
            return xcreateQDRFunctionTOutputSubCrossList();
        }
        protected QDRFunction<TOutputSubCrossCB> xcreateQDRFunctionTOutputSubCrossList() {
            return new QDRFunction<TOutputSubCrossCB>(delegate(String function, SubQuery<TOutputSubCrossCB> subQuery, String operand, Object value) {
                xqderiveTOutputSubCrossList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputSubCrossList(String function, SubQuery<TOutputSubCrossCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputSubCrossCB>", subQuery);
            TOutputSubCrossCB cb = new TOutputSubCrossCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossList(TOutputSubCrossCQ subQuery);
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubCrossListParameter(Object parameterValue);

        public QDRFunction<TOutputSubFaCB> DerivedTOutputSubFaList() {
            return xcreateQDRFunctionTOutputSubFaList();
        }
        protected QDRFunction<TOutputSubFaCB> xcreateQDRFunctionTOutputSubFaList() {
            return new QDRFunction<TOutputSubFaCB>(delegate(String function, SubQuery<TOutputSubFaCB> subQuery, String operand, Object value) {
                xqderiveTOutputSubFaList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputSubFaList(String function, SubQuery<TOutputSubFaCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputSubFaCB>", subQuery);
            TOutputSubFaCB cb = new TOutputSubFaCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaList(TOutputSubFaCQ subQuery);
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubFaListParameter(Object parameterValue);

        public QDRFunction<TOutputSubGtCB> DerivedTOutputSubGtList() {
            return xcreateQDRFunctionTOutputSubGtList();
        }
        protected QDRFunction<TOutputSubGtCB> xcreateQDRFunctionTOutputSubGtList() {
            return new QDRFunction<TOutputSubGtCB>(delegate(String function, SubQuery<TOutputSubGtCB> subQuery, String operand, Object value) {
                xqderiveTOutputSubGtList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTOutputSubGtList(String function, SubQuery<TOutputSubGtCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TOutputSubGtCB>", subQuery);
            TOutputSubGtCB cb = new TOutputSubGtCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtList(TOutputSubGtCQ subQuery);
        public abstract String keepOutputCommonId_QueryDerivedReferrer_TOutputSubGtListParameter(Object parameterValue);
        public void SetOutputCommonId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputCommonId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputCommonId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputCommonId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputCommonId(), "OUTPUT_COMMON_ID");
        }
        protected abstract ConditionValue getCValueOutputCommonId();

        public void SetOrderCount_Equal(long? v) { regOrderCount(CK_EQ, v); }
        public void SetOrderCount_NotEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOrderCount(CK_NES, v);
        }
        public void SetOrderCount_GreaterThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOrderCount(CK_GT, v);
        }
        public void SetOrderCount_LessThan(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOrderCount(CK_LT, v);
        }
        public void SetOrderCount_GreaterEqual(long? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOrderCount(CK_GE, v);
        }
        public void SetOrderCount_LessEqual(long? v) {
            WhereSetterFlag = true;
            regOrderCount(CK_LE, v);
        }
        public void SetOrderCount_InScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_INS, cTL<long?>(ls), getCValueOrderCount(), "ORDER_COUNT");
        }
        public void SetOrderCount_NotInScope(IList<long?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<long?>(CK_NINS, cTL<long?>(ls), getCValueOrderCount(), "ORDER_COUNT");
        }
        protected void regOrderCount(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOrderCount(), "ORDER_COUNT");
        }
        protected abstract ConditionValue getCValueOrderCount();

        public void SetTsvFilePath_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTsvFilePath_Equal(fRES(v));
        }
        protected void DoSetTsvFilePath_Equal(String v) { regTsvFilePath(CK_EQ, v); }
        public void SetTsvFilePath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTsvFilePath_NotEqual(fRES(v));
        }
        protected void DoSetTsvFilePath_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_NES, v);
        }
        public void SetTsvFilePath_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_GT, fRES(v));
        }
        public void SetTsvFilePath_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_LT, fRES(v));
        }
        public void SetTsvFilePath_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_GE, fRES(v));
        }
        public void SetTsvFilePath_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_LE, fRES(v));
        }
        public void SetTsvFilePath_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTsvFilePath(), "TSV_FILE_PATH");
        }
        public void SetTsvFilePath_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTsvFilePath(), "TSV_FILE_PATH");
        }
        public void SetTsvFilePath_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTsvFilePath_LikeSearch(v, cLSOP());
        }
        public void SetTsvFilePath_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTsvFilePath(), "TSV_FILE_PATH", option);
        }
        public void SetTsvFilePath_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTsvFilePath(), "TSV_FILE_PATH", option);
        }
        public void SetTsvFilePath_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTsvFilePath_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTsvFilePath(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTsvFilePath(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTsvFilePath(), "TSV_FILE_PATH");
        }
        protected abstract ConditionValue getCValueTsvFilePath();

        public void SetExcelbookNamePrefix_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetExcelbookNamePrefix_Equal(fRES(v));
        }
        protected void DoSetExcelbookNamePrefix_Equal(String v) { regExcelbookNamePrefix(CK_EQ, v); }
        public void SetExcelbookNamePrefix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetExcelbookNamePrefix_NotEqual(fRES(v));
        }
        protected void DoSetExcelbookNamePrefix_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_NES, v);
        }
        public void SetExcelbookNamePrefix_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_GT, fRES(v));
        }
        public void SetExcelbookNamePrefix_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_LT, fRES(v));
        }
        public void SetExcelbookNamePrefix_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_GE, fRES(v));
        }
        public void SetExcelbookNamePrefix_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_LE, fRES(v));
        }
        public void SetExcelbookNamePrefix_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueExcelbookNamePrefix(), "EXCELBOOK_NAME_PREFIX");
        }
        public void SetExcelbookNamePrefix_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueExcelbookNamePrefix(), "EXCELBOOK_NAME_PREFIX");
        }
        public void SetExcelbookNamePrefix_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetExcelbookNamePrefix_LikeSearch(v, cLSOP());
        }
        public void SetExcelbookNamePrefix_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueExcelbookNamePrefix(), "EXCELBOOK_NAME_PREFIX", option);
        }
        public void SetExcelbookNamePrefix_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueExcelbookNamePrefix(), "EXCELBOOK_NAME_PREFIX", option);
        }
        public void SetExcelbookNamePrefix_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_ISN, DUMMY_OBJECT);
        }
        public void SetExcelbookNamePrefix_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExcelbookNamePrefix(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regExcelbookNamePrefix(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExcelbookNamePrefix(), "EXCELBOOK_NAME_PREFIX");
        }
        protected abstract ConditionValue getCValueExcelbookNamePrefix();

        public void SetProcessStartDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_EQ, v);
        }
        public void SetProcessStartDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_GT, v);
        }
        public void SetProcessStartDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_LT, v);
        }
        public void SetProcessStartDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_GE, v);
        }
        public void SetProcessStartDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_LE, v);
        }
        public void SetProcessStartDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueProcessStartDatetime(), "PROCESS_START_DATETIME", option);
        }
        public void SetProcessStartDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetProcessStartDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetProcessStartDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetProcessStartDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessStartDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regProcessStartDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcessStartDatetime(), "PROCESS_START_DATETIME");
        }
        protected abstract ConditionValue getCValueProcessStartDatetime();

        public void SetProcessForecastEndDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_EQ, v);
        }
        public void SetProcessForecastEndDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_GT, v);
        }
        public void SetProcessForecastEndDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_LT, v);
        }
        public void SetProcessForecastEndDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_GE, v);
        }
        public void SetProcessForecastEndDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_LE, v);
        }
        public void SetProcessForecastEndDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueProcessForecastEndDatetime(), "PROCESS_FORECAST_END_DATETIME", option);
        }
        public void SetProcessForecastEndDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetProcessForecastEndDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetProcessForecastEndDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetProcessForecastEndDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessForecastEndDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regProcessForecastEndDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcessForecastEndDatetime(), "PROCESS_FORECAST_END_DATETIME");
        }
        protected abstract ConditionValue getCValueProcessForecastEndDatetime();

        public void SetProcessEndDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_EQ, v);
        }
        public void SetProcessEndDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_GT, v);
        }
        public void SetProcessEndDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_LT, v);
        }
        public void SetProcessEndDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_GE, v);
        }
        public void SetProcessEndDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_LE, v);
        }
        public void SetProcessEndDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueProcessEndDatetime(), "PROCESS_END_DATETIME", option);
        }
        public void SetProcessEndDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetProcessEndDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetProcessEndDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetProcessEndDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regProcessEndDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regProcessEndDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueProcessEndDatetime(), "PROCESS_END_DATETIME");
        }
        protected abstract ConditionValue getCValueProcessEndDatetime();

        public void SetStatusCode_Equal(int? v) { regStatusCode(CK_EQ, v); }
        public void SetStatusCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_NES, v);
        }
        public void SetStatusCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_GT, v);
        }
        public void SetStatusCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_LT, v);
        }
        public void SetStatusCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatusCode(CK_GE, v);
        }
        public void SetStatusCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regStatusCode(CK_LE, v);
        }
        public void SetStatusCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueStatusCode(), "STATUS_CODE");
        }
        public void SetStatusCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueStatusCode(), "STATUS_CODE");
        }
        protected void regStatusCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueStatusCode(), "STATUS_CODE");
        }
        protected abstract ConditionValue getCValueStatusCode();

        public void SetDescription_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDescription_Equal(fRES(v));
        }
        protected void DoSetDescription_Equal(String v) { regDescription(CK_EQ, v); }
        public void SetDescription_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetDescription_NotEqual(fRES(v));
        }
        protected void DoSetDescription_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_NES, v);
        }
        public void SetDescription_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_GT, fRES(v));
        }
        public void SetDescription_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_LT, fRES(v));
        }
        public void SetDescription_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_GE, fRES(v));
        }
        public void SetDescription_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_LE, fRES(v));
        }
        public void SetDescription_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueDescription(), "DESCRIPTION");
        }
        public void SetDescription_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueDescription(), "DESCRIPTION");
        }
        public void SetDescription_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetDescription_LikeSearch(v, cLSOP());
        }
        public void SetDescription_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueDescription(), "DESCRIPTION", option);
        }
        public void SetDescription_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueDescription(), "DESCRIPTION", option);
        }
        public void SetDescription_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDescription_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDescription(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDescription(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDescription(), "DESCRIPTION");
        }
        protected abstract ConditionValue getCValueDescription();

        public void SetOutputType_Equal(int? v) { regOutputType(CK_EQ, v); }
        public void SetOutputType_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_NES, v);
        }
        public void SetOutputType_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_GT, v);
        }
        public void SetOutputType_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_LT, v);
        }
        public void SetOutputType_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputType(CK_GE, v);
        }
        public void SetOutputType_LessEqual(int? v) {
            WhereSetterFlag = true;
            regOutputType(CK_LE, v);
        }
        public void SetOutputType_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueOutputType(), "OUTPUT_TYPE");
        }
        public void SetOutputType_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueOutputType(), "OUTPUT_TYPE");
        }
        protected void regOutputType(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputType(), "OUTPUT_TYPE");
        }
        protected abstract ConditionValue getCValueOutputType();

        public void SetOutputRequestId_Equal(decimal? v) { regOutputRequestId(CK_EQ, v); }
        public void SetOutputRequestId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_NES, v);
        }
        public void SetOutputRequestId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_GT, v);
        }
        public void SetOutputRequestId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_LT, v);
        }
        public void SetOutputRequestId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputRequestId(CK_GE, v);
        }
        public void SetOutputRequestId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regOutputRequestId(CK_LE, v);
        }
        public void SetOutputRequestId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueOutputRequestId(), "OUTPUT_REQUEST_ID");
        }
        public void SetOutputRequestId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueOutputRequestId(), "OUTPUT_REQUEST_ID");
        }
        public void InScopeTOutputRequest(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_InScopeSubQuery_TOutputRequest(cb.Query());
            registerInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_InScopeSubQuery_TOutputRequest(TOutputRequestCQ subQuery);
        public void NotInScopeTOutputRequest(SubQuery<TOutputRequestCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputRequestCB>", subQuery);
            TOutputRequestCB cb = new TOutputRequestCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepOutputRequestId_NotInScopeSubQuery_TOutputRequest(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "OUTPUT_REQUEST_ID", "OUTPUT_REQUEST_ID", subQueryPropertyName);
        }
        public abstract String keepOutputRequestId_NotInScopeSubQuery_TOutputRequest(TOutputRequestCQ subQuery);
        protected void regOutputRequestId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputRequestId(), "OUTPUT_REQUEST_ID");
        }
        protected abstract ConditionValue getCValueOutputRequestId();

        public void SetWbSettingCode_Equal(int? v) { regWbSettingCode(CK_EQ, v); }
        public void SetWbSettingCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWbSettingCode(CK_NES, v);
        }
        public void SetWbSettingCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWbSettingCode(CK_GT, v);
        }
        public void SetWbSettingCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWbSettingCode(CK_LT, v);
        }
        public void SetWbSettingCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regWbSettingCode(CK_GE, v);
        }
        public void SetWbSettingCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regWbSettingCode(CK_LE, v);
        }
        public void SetWbSettingCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueWbSettingCode(), "WB_SETTING_CODE");
        }
        public void SetWbSettingCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueWbSettingCode(), "WB_SETTING_CODE");
        }
        protected void regWbSettingCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueWbSettingCode(), "WB_SETTING_CODE");
        }
        protected abstract ConditionValue getCValueWbSettingCode();

        public void SetNoanswerVisibleCode_Equal(int? v) { regNoanswerVisibleCode(CK_EQ, v); }
        public void SetNoanswerVisibleCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerVisibleCode(CK_NES, v);
        }
        public void SetNoanswerVisibleCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerVisibleCode(CK_GT, v);
        }
        public void SetNoanswerVisibleCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerVisibleCode(CK_LT, v);
        }
        public void SetNoanswerVisibleCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerVisibleCode(CK_GE, v);
        }
        public void SetNoanswerVisibleCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regNoanswerVisibleCode(CK_LE, v);
        }
        public void SetNoanswerVisibleCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueNoanswerVisibleCode(), "NOANSWER_VISIBLE_CODE");
        }
        public void SetNoanswerVisibleCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueNoanswerVisibleCode(), "NOANSWER_VISIBLE_CODE");
        }
        public void SetNoanswerVisibleCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerVisibleCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetNoanswerVisibleCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regNoanswerVisibleCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regNoanswerVisibleCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueNoanswerVisibleCode(), "NOANSWER_VISIBLE_CODE");
        }
        protected abstract ConditionValue getCValueNoanswerVisibleCode();

        public void SetUnmatchVisibleCode_Equal(int? v) { regUnmatchVisibleCode(CK_EQ, v); }
        public void SetUnmatchVisibleCode_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmatchVisibleCode(CK_NES, v);
        }
        public void SetUnmatchVisibleCode_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmatchVisibleCode(CK_GT, v);
        }
        public void SetUnmatchVisibleCode_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmatchVisibleCode(CK_LT, v);
        }
        public void SetUnmatchVisibleCode_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmatchVisibleCode(CK_GE, v);
        }
        public void SetUnmatchVisibleCode_LessEqual(int? v) {
            WhereSetterFlag = true;
            regUnmatchVisibleCode(CK_LE, v);
        }
        public void SetUnmatchVisibleCode_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUnmatchVisibleCode(), "UNMATCH_VISIBLE_CODE");
        }
        public void SetUnmatchVisibleCode_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUnmatchVisibleCode(), "UNMATCH_VISIBLE_CODE");
        }
        public void SetUnmatchVisibleCode_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmatchVisibleCode(CK_ISN, DUMMY_OBJECT);
        }
        public void SetUnmatchVisibleCode_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUnmatchVisibleCode(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regUnmatchVisibleCode(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUnmatchVisibleCode(), "UNMATCH_VISIBLE_CODE");
        }
        protected abstract ConditionValue getCValueUnmatchVisibleCode();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputCommonCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputCommonCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputCommonCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputCommonCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputCommonCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputCommonCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputCommonCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputCommonCB>(delegate(String function, SubQuery<TOutputCommonCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputCommonCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputCommonCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputCommonCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputCommonCB>", subQuery);
            TOutputCommonCB cb = new TOutputCommonCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_COMMON_ID", "OUTPUT_COMMON_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputCommonCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
