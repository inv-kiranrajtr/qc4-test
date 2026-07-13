
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
    public abstract class AbstractBsTDataEditListCQ : AbstractConditionQuery {

        public AbstractBsTDataEditListCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_DATA_EDIT_LIST"; }
        public override String getTableSqlName() { return "T_DATA_EDIT_LIST"; }

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
        public void ExistsTDataProcessNewItemAsOne(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TDataProcessNewItemAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery);
        public void ExistsTDeleteDataAsOne(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TDeleteDataAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery);
        public void ExistsTEditDataAsOne(SubQuery<TEditDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditDataCB>", subQuery);
            TEditDataCB cb = new TEditDataCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TEditDataAsOne(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TEditDataAsOne(TEditDataCQ subQuery);
        public void ExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_ExistsSubQuery_TItemInfoList(cb.Query());
            registerExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_ExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotExistsTDataProcessNewItemAsOne(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery);
        public void NotExistsTDeleteDataAsOne(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TDeleteDataAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery);
        public void NotExistsTEditDataAsOne(SubQuery<TEditDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditDataCB>", subQuery);
            TEditDataCB cb = new TEditDataCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TEditDataAsOne(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TEditDataAsOne(TEditDataCQ subQuery);
        public void NotExistsTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForExistsReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotExistsSubQuery_TItemInfoList(cb.Query());
            registerNotExistsSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotExistsSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void InScopeTDataProcessNewItemAsOne(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDataProcessNewItemAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery);
        public void InScopeTDeleteDataAsOne(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TDeleteDataAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery);
        public void InScopeTEditDataAsOne(SubQuery<TEditDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditDataCB>", subQuery);
            TEditDataCB cb = new TEditDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TEditDataAsOne(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TEditDataAsOne(TEditDataCQ subQuery);
        public void InScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_InScopeSubQuery_TItemInfoList(cb.Query());
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_InScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void NotInScopeTDataProcessNewItemAsOne(SubQuery<TDataProcessNewItemCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataProcessNewItemCB>", subQuery);
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDataProcessNewItemAsOne(TDataProcessNewItemCQ subQuery);
        public void NotInScopeTDeleteDataAsOne(SubQuery<TDeleteDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDeleteDataCB>", subQuery);
            TDeleteDataCB cb = new TDeleteDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TDeleteDataAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TDeleteDataAsOne(TDeleteDataCQ subQuery);
        public void NotInScopeTEditDataAsOne(SubQuery<TEditDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditDataCB>", subQuery);
            TEditDataCB cb = new TEditDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TEditDataAsOne(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TEditDataAsOne(TEditDataCQ subQuery);
        public void NotInScopeTItemInfoList(SubQuery<TItemInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_NotInScopeSubQuery_TItemInfoList(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepDataEditId_NotInScopeSubQuery_TItemInfoList(TItemInfoCQ subQuery);
        public void xsderiveTItemInfoList(String function, SubQuery<TItemInfoCB> subQuery, String aliasName) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TItemInfoCB>", subQuery);
            TItemInfoCB cb = new TItemInfoCB(); cb.xsetupForDerivedReferrer(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepDataEditId_SpecifyDerivedReferrer_TItemInfoList(cb.Query());
            registerSpecifyDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, aliasName);
        }
        abstract public String keepDataEditId_SpecifyDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);

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
            String subQueryPropertyName = keepDataEditId_QueryDerivedReferrer_TItemInfoList(cb.Query()); // for saving query-value.
            String parameterPropertyName = keepDataEditId_QueryDerivedReferrer_TItemInfoListParameter(value);
            registerQueryDerivedReferrer(function, cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName, operand, value, parameterPropertyName);
        }
        public abstract String keepDataEditId_QueryDerivedReferrer_TItemInfoList(TItemInfoCQ subQuery);
        public abstract String keepDataEditId_QueryDerivedReferrer_TItemInfoListParameter(Object parameterValue);
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
        public void InScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_InScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        public void NotInScopeTQcwebSurveyInfo(SubQuery<TQcwebSurveyInfoCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TQcwebSurveyInfoCB>", subQuery);
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "QCWEBID", "QCWEBID", subQueryPropertyName);
        }
        public abstract String keepQcwebid_NotInScopeSubQuery_TQcwebSurveyInfo(TQcwebSurveyInfoCQ subQuery);
        protected void regQcwebid(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueQcwebid(), "QCWEBID");
        }
        protected abstract ConditionValue getCValueQcwebid();

        public void SetExecuteNo_Equal(int? v) { regExecuteNo(CK_EQ, v); }
        public void SetExecuteNo_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExecuteNo(CK_NES, v);
        }
        public void SetExecuteNo_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExecuteNo(CK_GT, v);
        }
        public void SetExecuteNo_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExecuteNo(CK_LT, v);
        }
        public void SetExecuteNo_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExecuteNo(CK_GE, v);
        }
        public void SetExecuteNo_LessEqual(int? v) {
            WhereSetterFlag = true;
            regExecuteNo(CK_LE, v);
        }
        public void SetExecuteNo_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueExecuteNo(), "EXECUTE_NO");
        }
        public void SetExecuteNo_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueExecuteNo(), "EXECUTE_NO");
        }
        protected void regExecuteNo(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExecuteNo(), "EXECUTE_NO");
        }
        protected abstract ConditionValue getCValueExecuteNo();

        public void SetExecuteFlag_Equal(int? v) { regExecuteFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of executeFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetExecuteFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regExecuteFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of executeFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetExecuteFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regExecuteFlag(CK_EQ, int.Parse(code));
        }
        public void SetExecuteFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regExecuteFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of executeFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetExecuteFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regExecuteFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of executeFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetExecuteFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regExecuteFlag(CK_NES, int.Parse(code));
        }
        public void SetExecuteFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueExecuteFlag(), "EXECUTE_FLAG");
        }
        public void SetExecuteFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueExecuteFlag(), "EXECUTE_FLAG");
        }
        protected void regExecuteFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueExecuteFlag(), "EXECUTE_FLAG");
        }
        protected abstract ConditionValue getCValueExecuteFlag();

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
        public void InScopeTEditMenuMaster(SubQuery<TEditMenuMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditMenuMasterCB>", subQuery);
            TEditMenuMasterCB cb = new TEditMenuMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_InScopeSubQuery_TEditMenuMaster(cb.Query());
            registerInScopeSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepEditMenuMasterId_InScopeSubQuery_TEditMenuMaster(TEditMenuMasterCQ subQuery);
        public void NotInScopeTEditMenuMaster(SubQuery<TEditMenuMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TEditMenuMasterCB>", subQuery);
            TEditMenuMasterCB cb = new TEditMenuMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepEditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster(cb.Query());
            registerNotInScopeSubQuery(cb.Query(), "EDIT_MENU_MASTER_ID", "EDIT_MENU_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepEditMenuMasterId_NotInScopeSubQuery_TEditMenuMaster(TEditMenuMasterCQ subQuery);
        protected void regEditMenuMasterId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditMenuMasterId(), "EDIT_MENU_MASTER_ID");
        }
        protected abstract ConditionValue getCValueEditMenuMasterId();

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

        public void SetConditionItemViewName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionItemViewName_Equal(fRES(v));
        }
        protected void DoSetConditionItemViewName_Equal(String v) { regConditionItemViewName(CK_EQ, v); }
        public void SetConditionItemViewName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetConditionItemViewName_NotEqual(fRES(v));
        }
        protected void DoSetConditionItemViewName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_NES, v);
        }
        public void SetConditionItemViewName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_GT, fRES(v));
        }
        public void SetConditionItemViewName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_LT, fRES(v));
        }
        public void SetConditionItemViewName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_GE, fRES(v));
        }
        public void SetConditionItemViewName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_LE, fRES(v));
        }
        public void SetConditionItemViewName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueConditionItemViewName(), "CONDITION_ITEM_VIEW_NAME");
        }
        public void SetConditionItemViewName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueConditionItemViewName(), "CONDITION_ITEM_VIEW_NAME");
        }
        public void SetConditionItemViewName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetConditionItemViewName_LikeSearch(v, cLSOP());
        }
        public void SetConditionItemViewName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueConditionItemViewName(), "CONDITION_ITEM_VIEW_NAME", option);
        }
        public void SetConditionItemViewName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueConditionItemViewName(), "CONDITION_ITEM_VIEW_NAME", option);
        }
        public void SetConditionItemViewName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetConditionItemViewName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regConditionItemViewName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regConditionItemViewName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueConditionItemViewName(), "CONDITION_ITEM_VIEW_NAME");
        }
        protected abstract ConditionValue getCValueConditionItemViewName();

        public void SetTargetItemViewName_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTargetItemViewName_Equal(fRES(v));
        }
        protected void DoSetTargetItemViewName_Equal(String v) { regTargetItemViewName(CK_EQ, v); }
        public void SetTargetItemViewName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetTargetItemViewName_NotEqual(fRES(v));
        }
        protected void DoSetTargetItemViewName_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_NES, v);
        }
        public void SetTargetItemViewName_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_GT, fRES(v));
        }
        public void SetTargetItemViewName_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_LT, fRES(v));
        }
        public void SetTargetItemViewName_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_GE, fRES(v));
        }
        public void SetTargetItemViewName_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_LE, fRES(v));
        }
        public void SetTargetItemViewName_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueTargetItemViewName(), "TARGET_ITEM_VIEW_NAME");
        }
        public void SetTargetItemViewName_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueTargetItemViewName(), "TARGET_ITEM_VIEW_NAME");
        }
        public void SetTargetItemViewName_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetTargetItemViewName_LikeSearch(v, cLSOP());
        }
        public void SetTargetItemViewName_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueTargetItemViewName(), "TARGET_ITEM_VIEW_NAME", option);
        }
        public void SetTargetItemViewName_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueTargetItemViewName(), "TARGET_ITEM_VIEW_NAME", option);
        }
        public void SetTargetItemViewName_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_ISN, DUMMY_OBJECT);
        }
        public void SetTargetItemViewName_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regTargetItemViewName(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regTargetItemViewName(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueTargetItemViewName(), "TARGET_ITEM_VIEW_NAME");
        }
        protected abstract ConditionValue getCValueTargetItemViewName();

        public void SetStatus_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetStatus_Equal(fRES(v));
        }
        protected void DoSetStatus_Equal(String v) { regStatus(CK_EQ, v); }
        public void SetStatus_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetStatus_NotEqual(fRES(v));
        }
        protected void DoSetStatus_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatus(CK_NES, v);
        }
        public void SetStatus_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatus(CK_GT, fRES(v));
        }
        public void SetStatus_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatus(CK_LT, fRES(v));
        }
        public void SetStatus_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatus(CK_GE, fRES(v));
        }
        public void SetStatus_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regStatus(CK_LE, fRES(v));
        }
        public void SetStatus_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueStatus(), "STATUS");
        }
        public void SetStatus_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueStatus(), "STATUS");
        }
        public void SetStatus_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetStatus_LikeSearch(v, cLSOP());
        }
        public void SetStatus_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueStatus(), "STATUS", option);
        }
        public void SetStatus_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueStatus(), "STATUS", option);
        }
        protected void regStatus(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueStatus(), "STATUS");
        }
        protected abstract ConditionValue getCValueStatus();

        public void SetLatestFlag_Equal(int? v) { regLatestFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of latestFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetLatestFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regLatestFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of latestFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetLatestFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regLatestFlag(CK_EQ, int.Parse(code));
        }
        public void SetLatestFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLatestFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of latestFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetLatestFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regLatestFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of latestFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetLatestFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regLatestFlag(CK_NES, int.Parse(code));
        }
        public void SetLatestFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueLatestFlag(), "LATEST_FLAG");
        }
        public void SetLatestFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueLatestFlag(), "LATEST_FLAG");
        }
        protected void regLatestFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLatestFlag(), "LATEST_FLAG");
        }
        protected abstract ConditionValue getCValueLatestFlag();

        public void SetDerivedDataEditId_Equal(decimal? v) { regDerivedDataEditId(CK_EQ, v); }
        public void SetDerivedDataEditId_NotEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDerivedDataEditId(CK_NES, v);
        }
        public void SetDerivedDataEditId_GreaterThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDerivedDataEditId(CK_GT, v);
        }
        public void SetDerivedDataEditId_LessThan(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDerivedDataEditId(CK_LT, v);
        }
        public void SetDerivedDataEditId_GreaterEqual(decimal? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDerivedDataEditId(CK_GE, v);
        }
        public void SetDerivedDataEditId_LessEqual(decimal? v) {
            WhereSetterFlag = true;
            regDerivedDataEditId(CK_LE, v);
        }
        public void SetDerivedDataEditId_InScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_INS, cTL<decimal?>(ls), getCValueDerivedDataEditId(), "DERIVED_DATA_EDIT_ID");
        }
        public void SetDerivedDataEditId_NotInScope(IList<decimal?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<decimal?>(CK_NINS, cTL<decimal?>(ls), getCValueDerivedDataEditId(), "DERIVED_DATA_EDIT_ID");
        }
        public void SetDerivedDataEditId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDerivedDataEditId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetDerivedDataEditId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDerivedDataEditId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regDerivedDataEditId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDerivedDataEditId(), "DERIVED_DATA_EDIT_ID");
        }
        protected abstract ConditionValue getCValueDerivedDataEditId();

        public void SetDeleteReserveFlag_Equal(int? v) { regDeleteReserveFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of deleteReserveFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetDeleteReserveFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regDeleteReserveFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteReserveFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetDeleteReserveFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regDeleteReserveFlag(CK_EQ, int.Parse(code));
        }
        public void SetDeleteReserveFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regDeleteReserveFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of deleteReserveFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetDeleteReserveFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regDeleteReserveFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of deleteReserveFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetDeleteReserveFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regDeleteReserveFlag(CK_NES, int.Parse(code));
        }
        public void SetDeleteReserveFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueDeleteReserveFlag(), "DELETE_RESERVE_FLAG");
        }
        public void SetDeleteReserveFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueDeleteReserveFlag(), "DELETE_RESERVE_FLAG");
        }
        protected void regDeleteReserveFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueDeleteReserveFlag(), "DELETE_RESERVE_FLAG");
        }
        protected abstract ConditionValue getCValueDeleteReserveFlag();

        public void SetLastUpdateUser_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastUpdateUser_Equal(fRES(v));
        }
        protected void DoSetLastUpdateUser_Equal(String v) { regLastUpdateUser(CK_EQ, v); }
        public void SetLastUpdateUser_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetLastUpdateUser_NotEqual(fRES(v));
        }
        protected void DoSetLastUpdateUser_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_NES, v);
        }
        public void SetLastUpdateUser_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_GT, fRES(v));
        }
        public void SetLastUpdateUser_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_LT, fRES(v));
        }
        public void SetLastUpdateUser_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_GE, fRES(v));
        }
        public void SetLastUpdateUser_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_LE, fRES(v));
        }
        public void SetLastUpdateUser_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        public void SetLastUpdateUser_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        public void SetLastUpdateUser_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastUpdateUser_LikeSearch(v, cLSOP());
        }
        public void SetLastUpdateUser_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueLastUpdateUser(), "LAST_UPDATE_USER", option);
        }
        public void SetLastUpdateUser_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueLastUpdateUser(), "LAST_UPDATE_USER", option);
        }
        public void SetLastUpdateUser_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastUpdateUser_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateUser(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastUpdateUser(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastUpdateUser(), "LAST_UPDATE_USER");
        }
        protected abstract ConditionValue getCValueLastUpdateUser();

        public void SetLastUpdateDatetime_Equal(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_EQ, v);
        }
        public void SetLastUpdateDatetime_GreaterThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_GT, v);
        }
        public void SetLastUpdateDatetime_LessThan(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_LT, v);
        }
        public void SetLastUpdateDatetime_GreaterEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_GE, v);
        }
        public void SetLastUpdateDatetime_LessEqual(DateTime? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_LE, v);
        }
        public void SetLastUpdateDatetime_FromTo(DateTime? from, DateTime? to, FromToOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFTQ(from, to, getCValueLastUpdateDatetime(), "LAST_UPDATE_DATETIME", option);
        }
        public void SetLastUpdateDatetime_DateFromTo(DateTime? from, DateTime? to) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetLastUpdateDatetime_FromTo(from, to, new DateFromToOption());
        }
        public void SetLastUpdateDatetime_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_ISN, DUMMY_OBJECT);
        }
        public void SetLastUpdateDatetime_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLastUpdateDatetime(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regLastUpdateDatetime(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueLastUpdateDatetime(), "LAST_UPDATE_DATETIME");
        }
        protected abstract ConditionValue getCValueLastUpdateDatetime();

        public void SetEditFlag_Equal(int? v) { regEditFlag(CK_EQ, v); }
        /// <summary>
        /// Set the value of True of editFlag as equal. { = }
        /// はい: 有効を示す
        /// </summary>
        public void SetEditFlag_Equal_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regEditFlag(CK_EQ, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of editFlag as equal. { = }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetEditFlag_Equal_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regEditFlag(CK_EQ, int.Parse(code));
        }
        public void SetEditFlag_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regEditFlag(CK_NES, v);
        }
        /// <summary>
        /// Set the value of True of editFlag as notEqual. { &lt;&gt; }
        /// はい: 有効を示す
        /// </summary>
        public void SetEditFlag_NotEqual_True() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.True.Code;
            regEditFlag(CK_NES, int.Parse(code));
        }
        /// <summary>
        /// Set the value of False of editFlag as notEqual. { &lt;&gt; }
        /// いいえ: 無効を示す
        /// </summary>
        public void SetEditFlag_NotEqual_False() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            String code = CDef.Flag.False.Code;
            regEditFlag(CK_NES, int.Parse(code));
        }
        public void SetEditFlag_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValueEditFlag(), "EDIT_FLAG");
        }
        public void SetEditFlag_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValueEditFlag(), "EDIT_FLAG");
        }
        protected void regEditFlag(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueEditFlag(), "EDIT_FLAG");
        }
        protected abstract ConditionValue getCValueEditFlag();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TDataEditListCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TDataEditListCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TDataEditListCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TDataEditListCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TDataEditListCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TDataEditListCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TDataEditListCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TDataEditListCB>(delegate(String function, SubQuery<TDataEditListCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TDataEditListCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TDataEditListCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TDataEditListCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TDataEditListCB>", subQuery);
            TDataEditListCB cb = new TDataEditListCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "DATA_EDIT_ID", "DATA_EDIT_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TDataEditListCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
