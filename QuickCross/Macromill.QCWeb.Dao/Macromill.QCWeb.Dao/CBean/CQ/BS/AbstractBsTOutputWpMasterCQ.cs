
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
    public abstract class AbstractBsTOutputWpMasterCQ : AbstractConditionQuery {

        public AbstractBsTOutputWpMasterCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_OUTPUT_WP_MASTER"; }
        public override String getTableSqlName() { return "T_OUTPUT_WP_MASTER"; }

        public void SetOutputWpMasterId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_Equal(fRES(v));
        }
        /// <summary>
        /// Set the value of GT of outputWpMasterId as equal. { = }
        /// GT: GTを示す
        /// </summary>
        public void SetOutputWpMasterId_Equal_GT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_Equal(CDef.OutputWPMasterID.GT.Code);
        }
        /// <summary>
        /// Set the value of CROSS of outputWpMasterId as equal. { = }
        /// CROSS: CROSSを示す
        /// </summary>
        public void SetOutputWpMasterId_Equal_CROSS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_Equal(CDef.OutputWPMasterID.CROSS.Code);
        }
        /// <summary>
        /// Set the value of FA of outputWpMasterId as equal. { = }
        /// FA: FAを示す
        /// </summary>
        public void SetOutputWpMasterId_Equal_FA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_Equal(CDef.OutputWPMasterID.FA.Code);
        }
        /// <summary>
        /// Set the value of DataOutput of outputWpMasterId as equal. { = }
        /// データ出力: データ出力を示す
        /// </summary>
        public void SetOutputWpMasterId_Equal_DataOutput() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_Equal(CDef.OutputWPMasterID.DataOutput.Code);
        }
        protected void DoSetOutputWpMasterId_Equal(String v) { regOutputWpMasterId(CK_EQ, v); }
        public void SetOutputWpMasterId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_NotEqual(fRES(v));
        }
        /// <summary>
        /// Set the value of GT of outputWpMasterId as notEqual. { &lt;&gt; }
        /// GT: GTを示す
        /// </summary>
        public void SetOutputWpMasterId_NotEqual_GT() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_NotEqual(CDef.OutputWPMasterID.GT.Code);
        }
        /// <summary>
        /// Set the value of CROSS of outputWpMasterId as notEqual. { &lt;&gt; }
        /// CROSS: CROSSを示す
        /// </summary>
        public void SetOutputWpMasterId_NotEqual_CROSS() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_NotEqual(CDef.OutputWPMasterID.CROSS.Code);
        }
        /// <summary>
        /// Set the value of FA of outputWpMasterId as notEqual. { &lt;&gt; }
        /// FA: FAを示す
        /// </summary>
        public void SetOutputWpMasterId_NotEqual_FA() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_NotEqual(CDef.OutputWPMasterID.FA.Code);
        }
        /// <summary>
        /// Set the value of DataOutput of outputWpMasterId as notEqual. { &lt;&gt; }
        /// データ出力: データ出力を示す
        /// </summary>
        public void SetOutputWpMasterId_NotEqual_DataOutput() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetOutputWpMasterId_NotEqual(CDef.OutputWPMasterID.DataOutput.Code);
        }
        protected void DoSetOutputWpMasterId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputWpMasterId(CK_NES, v);
        }
        public void SetOutputWpMasterId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueOutputWpMasterId(), "OUTPUT_WP_MASTER_ID");
        }
        public void SetOutputWpMasterId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueOutputWpMasterId(), "OUTPUT_WP_MASTER_ID");
        }
        public void SetOutputWpMasterId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputWpMasterId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetOutputWpMasterId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regOutputWpMasterId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regOutputWpMasterId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueOutputWpMasterId(), "OUTPUT_WP_MASTER_ID");
        }
        protected abstract ConditionValue getCValueOutputWpMasterId();

        public void SetPoint_Equal(int? v) { regPoint(CK_EQ, v); }
        public void SetPoint_NotEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPoint(CK_NES, v);
        }
        public void SetPoint_GreaterThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPoint(CK_GT, v);
        }
        public void SetPoint_LessThan(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPoint(CK_LT, v);
        }
        public void SetPoint_GreaterEqual(int? v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regPoint(CK_GE, v);
        }
        public void SetPoint_LessEqual(int? v) {
            WhereSetterFlag = true;
            regPoint(CK_LE, v);
        }
        public void SetPoint_InScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_INS, cTL<int?>(ls), getCValuePoint(), "POINT");
        }
        public void SetPoint_NotInScope(IList<int?> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<int?>(CK_NINS, cTL<int?>(ls), getCValuePoint(), "POINT");
        }
        protected void regPoint(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValuePoint(), "POINT");
        }
        protected abstract ConditionValue getCValuePoint();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TOutputWpMasterCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TOutputWpMasterCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TOutputWpMasterCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TOutputWpMasterCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TOutputWpMasterCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TOutputWpMasterCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TOutputWpMasterCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TOutputWpMasterCB>(delegate(String function, SubQuery<TOutputWpMasterCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TOutputWpMasterCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TOutputWpMasterCB>", subQuery);
            TOutputWpMasterCB cb = new TOutputWpMasterCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TOutputWpMasterCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TOutputWpMasterCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TOutputWpMasterCB>", subQuery);
            TOutputWpMasterCB cb = new TOutputWpMasterCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "OUTPUT_WP_MASTER_ID", "OUTPUT_WP_MASTER_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TOutputWpMasterCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
