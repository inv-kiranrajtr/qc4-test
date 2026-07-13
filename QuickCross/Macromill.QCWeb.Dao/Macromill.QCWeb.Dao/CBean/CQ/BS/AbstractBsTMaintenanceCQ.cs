
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
    public abstract class AbstractBsTMaintenanceCQ : AbstractConditionQuery {

        public AbstractBsTMaintenanceCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_MAINTENANCE"; }
        public override String getTableSqlName() { return "T_MAINTENANCE"; }

        public void SetMaintenanceId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMaintenanceId_Equal(fRES(v));
        }
        protected void DoSetMaintenanceId_Equal(String v) { regMaintenanceId(CK_EQ, v); }
        public void SetMaintenanceId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetMaintenanceId_NotEqual(fRES(v));
        }
        protected void DoSetMaintenanceId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_NES, v);
        }
        public void SetMaintenanceId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_GT, fRES(v));
        }
        public void SetMaintenanceId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_LT, fRES(v));
        }
        public void SetMaintenanceId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_GE, fRES(v));
        }
        public void SetMaintenanceId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_LE, fRES(v));
        }
        public void SetMaintenanceId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueMaintenanceId(), "MAINTENANCE_ID");
        }
        public void SetMaintenanceId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueMaintenanceId(), "MAINTENANCE_ID");
        }
        public void SetMaintenanceId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetMaintenanceId_LikeSearch(v, cLSOP());
        }
        public void SetMaintenanceId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueMaintenanceId(), "MAINTENANCE_ID", option);
        }
        public void SetMaintenanceId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueMaintenanceId(), "MAINTENANCE_ID", option);
        }
        public void SetMaintenanceId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetMaintenanceId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regMaintenanceId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regMaintenanceId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueMaintenanceId(), "MAINTENANCE_ID");
        }
        protected abstract ConditionValue getCValueMaintenanceId();

        public void SetLimitTime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLimitTime(CK_EQ, v);
        }
        public void SetLimitTime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLimitTime(CK_GT, v);
        }
        public void SetLimitTime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLimitTime(CK_LT, v);
        }
        public void SetLimitTime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLimitTime(CK_GE, v);
        }
        public void SetLimitTime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLimitTime(CK_LE, v);
        }
        public void SetLimitTime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueLimitTime(), "LIMIT_TIME", option);
        }
        public void SetLimitTime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLimitTime_FromTo(from, to, new DateFromToOption());
        }
        protected void regLimitTime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLimitTime(), "LIMIT_TIME");
        }
        protected abstract ConditionValue getCValueLimitTime();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TMaintenanceCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TMaintenanceCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TMaintenanceCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TMaintenanceCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TMaintenanceCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TMaintenanceCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TMaintenanceCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TMaintenanceCB>(delegate(String function, SubQuery<TMaintenanceCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TMaintenanceCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TMaintenanceCB>", subQuery);
            TMaintenanceCB cb = new TMaintenanceCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TMaintenanceCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TMaintenanceCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TMaintenanceCB>", subQuery);
            TMaintenanceCB cb = new TMaintenanceCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "MAINTENANCE_ID", "MAINTENANCE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TMaintenanceCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
