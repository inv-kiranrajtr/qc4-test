
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
    public abstract class AbstractBsTTableControlCQ : AbstractConditionQuery {

        public AbstractBsTTableControlCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_TABLE_CONTROL"; }
        public override String getTableSqlName() { return "T_TABLE_CONTROL"; }

        public void SetQcwebid_Equal(decimal? v) { regQcwebid(CK_EQ, v); }
        public void SetQcwebid_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_NES, v);
        }
        public void SetQcwebid_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_GT, v);
        }
        public void SetQcwebid_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_LT, v);
        }
        public void SetQcwebid_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_GE, v);
        }
        public void SetQcwebid_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regQcwebid(CK_LE, v);
        }
        public void SetQcwebid_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueQcwebid(), "QCWEBID");
        }
        public void SetQcwebid_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueQcwebid(), "QCWEBID");
        }
        public void ExistsTTableDetailInfoList(SubQuery<TTableDetailInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableDetailInfoCB>", subQuery);
            TTableDetailInfoCB cb = new TTableDetailInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TTableDetailInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery);
        public void ExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void ExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_ExistsSubQuery_TItemInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotExistsTTableDetailInfoList(SubQuery<TTableDetailInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableDetailInfoCB>", subQuery);
            TTableDetailInfoCB cb = new TTableDetailInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TTableDetailInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery);
        public void NotExistsTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotExistsSubQuery_TItemInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void InScopeTTableDetailInfoList(SubQuery<TTableDetailInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableDetailInfoCB>", subQuery);
            TTableDetailInfoCB cb = new TTableDetailInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TTableDetailInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery);
        public void InScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void InScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TItemInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotInScopeTTableDetailInfoList(SubQuery<TTableDetailInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableDetailInfoCB>", subQuery);
            TTableDetailInfoCB cb = new TTableDetailInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TTableDetailInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TTableDetailInfoList(TTableDetailInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfoAsOne(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWebID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfoAsOne(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TItemInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void xsderiveTTableDetailInfoList(String function, SubQuery<TTableDetailInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableDetailInfoCB>", subQuery);
            TTableDetailInfoCB cb = new TTableDetailInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TTableDetailInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TTableDetailInfoList(TTableDetailInfoCQ subQuery);
        public void xsderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, aliasName);
        }
        abstract public String keepQcwebid_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);

        public QDRFunction<TTableDetailInfoCB> DerivedTTableDetailInfoList() {
            return xcreateQDRFunctionTTableDetailInfoList();
        }
        protected QDRFunction<TTableDetailInfoCB> xcreateQDRFunctionTTableDetailInfoList() {
            return new QDRFunction<TTableDetailInfoCB>(delegate(String function, SubQuery<TTableDetailInfoCB> subQuery, String operand, Object value) {
                xqderiveTTableDetailInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTTableDetailInfoList(String function, SubQuery<TTableDetailInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TTableDetailInfoCB>", subQuery);
            TTableDetailInfoCB cb = new TTableDetailInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TTableDetailInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TTableDetailInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TTableDetailInfoList(TTableDetailInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TTableDetailInfoListParameter(Object parameterValue);

        public QDRFunction<TItemInfoCB> DerivedTItemInfoList() {
            return xcreateQDRFunctionTItemInfoList();
        }
        protected QDRFunction<TItemInfoCB> xcreateQDRFunctionTItemInfoList() {
            return new QDRFunction<TItemInfoCB>(delegate(String function, SubQuery<TItemInfoCB> subQuery, String operand, Object value) {
                xqderiveTItemInfoList(function, subQuery, operand, value);
            });
        }
        public void xqderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String operand, Object value) {
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_QueryDerivedReferrer_TItemInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepQcwebid_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);
        public abstract String keepQcwebid_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue);
        public void SetQcwebid_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISN, DUMMY_OBJECT);
        }
        public void SetQcwebid_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQcwebid(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetBaseTableName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetBaseTableName_Equal(fRES(v));
        }
        protected void DoSetBaseTableName_Equal(String v) { regBaseTableName(CK_EQ, v); }
        public void SetBaseTableName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetBaseTableName_NotEqual(fRES(v));
        }
        protected void DoSetBaseTableName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseTableName(CK_NES, v);
        }
        public void SetBaseTableName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseTableName(CK_GT, fRES(v));
        }
        public void SetBaseTableName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseTableName(CK_LT, fRES(v));
        }
        public void SetBaseTableName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseTableName(CK_GE, fRES(v));
        }
        public void SetBaseTableName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regBaseTableName(CK_LE, fRES(v));
        }
        public void SetBaseTableName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueBaseTableName(), "BASE_TABLE_NAME");
        }
        public void SetBaseTableName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueBaseTableName(), "BASE_TABLE_NAME");
        }
        public void SetBaseTableName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetBaseTableName_LikeSearch(v, cLSOP());
        }
        public void SetBaseTableName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueBaseTableName(), "BASE_TABLE_NAME", option);
        }
        public void SetBaseTableName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueBaseTableName(), "BASE_TABLE_NAME", option);
        }
        protected void regBaseTableName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueBaseTableName(), "BASE_TABLE_NAME");
        }
        protected abstract ConditionValue getCValueBaseTableName();

        public void SetActiveTableNo_Equal(int? v) { regActiveTableNo(CK_EQ, v); }
        public void SetActiveTableNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regActiveTableNo(CK_NES, v);
        }
        public void SetActiveTableNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regActiveTableNo(CK_GT, v);
        }
        public void SetActiveTableNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regActiveTableNo(CK_LT, v);
        }
        public void SetActiveTableNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regActiveTableNo(CK_GE, v);
        }
        public void SetActiveTableNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regActiveTableNo(CK_LE, v);
        }
        public void SetActiveTableNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueActiveTableNo(), "ACTIVE_TABLE_NO");
        }
        public void SetActiveTableNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueActiveTableNo(), "ACTIVE_TABLE_NO");
        }
        protected void regActiveTableNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueActiveTableNo(), "ACTIVE_TABLE_NO");
        }
        protected abstract ConditionValue getCValueActiveTableNo();

        public void SetMaxNo_Equal(int? v) { regMaxNo(CK_EQ, v); }
        public void SetMaxNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaxNo(CK_NES, v);
        }
        public void SetMaxNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaxNo(CK_GT, v);
        }
        public void SetMaxNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaxNo(CK_LT, v);
        }
        public void SetMaxNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaxNo(CK_GE, v);
        }
        public void SetMaxNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regMaxNo(CK_LE, v);
        }
        public void SetMaxNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueMaxNo(), "MAX_NO");
        }
        public void SetMaxNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueMaxNo(), "MAX_NO");
        }
        protected void regMaxNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMaxNo(), "MAX_NO");
        }
        protected abstract ConditionValue getCValueMaxNo();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TTableControlCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TTableControlCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TTableControlCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TTableControlCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TTableControlCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TTableControlCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TTableControlCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TTableControlCB>(delegate(String function, SubQuery<TTableControlCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TTableControlCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TTableControlCB>", subQuery);
            TTableControlCB cb = new TTableControlCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TTableControlCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TTableControlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableControlCB>", subQuery);
            TTableControlCB cb = new TTableControlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TTableControlCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
