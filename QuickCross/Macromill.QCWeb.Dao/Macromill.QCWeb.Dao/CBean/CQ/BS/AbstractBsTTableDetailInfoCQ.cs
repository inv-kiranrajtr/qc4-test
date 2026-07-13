
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
    public abstract class AbstractBsTTableDetailInfoCQ : AbstractConditionQuery {

        public AbstractBsTTableDetailInfoCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_TABLE_DETAIL_INFO"; }
        public override String getTableSqlName() { return "T_TABLE_DETAIL_INFO"; }

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
        public void InScopeTTableControl(SubQuery<TTableControlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableControlCB>", subQuery);
            TTableControlCB cb = new TTableControlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TTableControl(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TTableControl(TTableControlCQ subQuery);
        public void NotInScopeTTableControl(SubQuery<TTableControlCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TTableControlCB>", subQuery);
            TTableControlCB cb = new TTableControlCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TTableControl(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TTableControl(TTableControlCQ subQuery);
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

        public void SetTableNo_Equal(int? v) { regTableNo(CK_EQ, v); }
        public void SetTableNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_NES, v);
        }
        public void SetTableNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_GT, v);
        }
        public void SetTableNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_LT, v);
        }
        public void SetTableNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_GE, v);
        }
        public void SetTableNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regTableNo(CK_LE, v);
        }
        public void SetTableNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueTableNo(), "TABLE_NO");
        }
        public void SetTableNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueTableNo(), "TABLE_NO");
        }
        public void SetTableNo_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTableNo_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTableNo(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTableNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTableNo(), "TABLE_NO");
        }
        protected abstract ConditionValue getCValueTableNo();

        public void SetUsedNo_Equal(int? v) { regUsedNo(CK_EQ, v); }
        public void SetUsedNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUsedNo(CK_NES, v);
        }
        public void SetUsedNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUsedNo(CK_GT, v);
        }
        public void SetUsedNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUsedNo(CK_LT, v);
        }
        public void SetUsedNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regUsedNo(CK_GE, v);
        }
        public void SetUsedNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regUsedNo(CK_LE, v);
        }
        public void SetUsedNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueUsedNo(), "USED_NO");
        }
        public void SetUsedNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueUsedNo(), "USED_NO");
        }
        protected void regUsedNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueUsedNo(), "USED_NO");
        }
        protected abstract ConditionValue getCValueUsedNo();

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
