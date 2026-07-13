
using System;
using System.Collections;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.Helper;

using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.Nss;

namespace Macromill.QCWeb.Dao.CBean.BS {

    [System.Serializable]
    public class BsTSurveyDataCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TSurveyDataCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SURVEY_DATA"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(String sampleId) {
            assertObjectNotNull("sampleId", sampleId);
            BsTSurveyDataCB cb = this;
            cb.Query().SetSampleId_Equal(sampleId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_SampleId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_SampleId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TSurveyDataCQ Query() {
            return this.ConditionQuery;
        }

        public TSurveyDataCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TSurveyDataCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TSurveyDataCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TSurveyDataCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TSurveyDataCB> unionQuery) {
            TSurveyDataCB cb = new TSurveyDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TSurveyDataCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TSurveyDataCB> unionQuery) {
            TSurveyDataCB cb = new TSurveyDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TSurveyDataCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
	    }

        public override bool HasUnionQueryOrUnionAllQuery() {
            return Query().hasUnionQueryOrUnionAllQuery();
        }

        // ===============================================================================
        //                                                                       Lock Wait
        //                                                                       =========
        public virtual ConditionBean LockForUpdateNoWait()
        { if (xhelpIsSqlClauseOracle()) { xhelpGettingSqlClauseOracle().lockForUpdateNoWait(); } return this; }
        public virtual ConditionBean LockForUpdateWait(int waitSec)
        { if (xhelpIsSqlClauseOracle()) { xhelpGettingSqlClauseOracle().lockForUpdateWait(waitSec); } return this; }

        protected virtual bool xhelpIsSqlClauseOracle() {
            return this.SqlClause is Macromill.QCWeb.Dao.AllCommon.CBean.SClause.SqlClauseOracle;
        }

        protected virtual Macromill.QCWeb.Dao.AllCommon.CBean.SClause.SqlClauseOracle xhelpGettingSqlClauseOracle() {
            return (Macromill.QCWeb.Dao.AllCommon.CBean.SClause.SqlClauseOracle)this.SqlClause;
        }

        // ===============================================================================
        //                                                                    Setup Select
        //                                                                    ============

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TSurveyDataCBSpecification _specification;
        public TSurveyDataCBSpecification Specify() {
            if (_specification == null) { _specification = new TSurveyDataCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TSurveyDataCQ> {
			protected BsTSurveyDataCB _myCB;
			public MySpQyCall(BsTSurveyDataCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TSurveyDataCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TSurveyDataCB> ColumnQuery(SpecifyQuery<TSurveyDataCB> leftSpecifyQuery) {
            return new HpColQyOperand<TSurveyDataCB>(delegate(SpecifyQuery<TSurveyDataCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TSurveyDataCB xcreateColumnQueryCB() {
            TSurveyDataCB cb = new TSurveyDataCB();
            cb.xsetupForColumnQuery((TSurveyDataCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TSurveyDataCB> orQuery) {
            xorQ((TSurveyDataCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TSurveyDataCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TSurveyDataCBColQySpQyCall(mainCB));
        }
    }

    public class TSurveyDataCBColQySpQyCall : HpSpQyCall<TSurveyDataCQ> {
        protected TSurveyDataCB _mainCB;
        public TSurveyDataCBColQySpQyCall(TSurveyDataCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TSurveyDataCQ qy() { return _mainCB.Query(); } 
    }

    public class TSurveyDataCBSpecification : AbstractSpecification<TSurveyDataCQ> {
        public TSurveyDataCBSpecification(ConditionBean baseCB, HpSpQyCall<TSurveyDataCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnSampleId() { doColumn("SAMPLE_ID"); }
        public void ColumnMergeCode() { doColumn("MERGE_CODE"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnDeleteFlag() { doColumn("DELETE_FLAG"); }
        public void ColumnAnswerDate() { doColumn("ANSWER_DATE"); }
        public void ColumnSex() { doColumn("SEX"); }
        public void ColumnAge() { doColumn("AGE"); }
        public void ColumnAgeId() { doColumn("AGE_ID"); }
        public void ColumnPrefecture() { doColumn("PREFECTURE"); }
        public void ColumnArea() { doColumn("AREA"); }
        public void ColumnMarried() { doColumn("MARRIED"); }
        public void ColumnChild() { doColumn("CHILD"); }
        public void ColumnHincome() { doColumn("HINCOME"); }
        public void ColumnPincome() { doColumn("PINCOME"); }
        public void ColumnJob() { doColumn("JOB"); }
        public void ColumnStudent() { doColumn("STUDENT"); }
        public void ColumnCell() { doColumn("CELL"); }
        public void ColumnCellName() { doColumn("CELL_NAME"); }
        public void ColumnQ0001() { doColumn("Q0001"); }
        public void ColumnQ0002() { doColumn("Q0002"); }
        public void ColumnQ0003() { doColumn("Q0003"); }
        public void ColumnQ0004() { doColumn("Q0004"); }
        public void ColumnQ0005() { doColumn("Q0005"); }
        public void ColumnQ0006() { doColumn("Q0006"); }
        public void ColumnQ0007() { doColumn("Q0007"); }
        public void ColumnQ0008() { doColumn("Q0008"); }
        public void ColumnQ0009() { doColumn("Q0009"); }
        public void ColumnQ0010() { doColumn("Q0010"); }
        public void ColumnQ0011() { doColumn("Q0011"); }
        public void ColumnQ0012() { doColumn("Q0012"); }
        public void ColumnQ0013() { doColumn("Q0013"); }
        public void ColumnQ0014() { doColumn("Q0014"); }
        public void ColumnQ0015() { doColumn("Q0015"); }
        public void ColumnQ0016() { doColumn("Q0016"); }
        public void ColumnQ0017() { doColumn("Q0017"); }
        public void ColumnQ0018() { doColumn("Q0018"); }
        public void ColumnQ0019() { doColumn("Q0019"); }
        public void ColumnQ0020() { doColumn("Q0020"); }
        public void ColumnQ0021() { doColumn("Q0021"); }
        public void ColumnQ0022() { doColumn("Q0022"); }
        public void ColumnQ0023() { doColumn("Q0023"); }
        public void ColumnQ0024() { doColumn("Q0024"); }
        public void ColumnQ0025() { doColumn("Q0025"); }
        public void ColumnQ0026() { doColumn("Q0026"); }
        public void ColumnQ0027() { doColumn("Q0027"); }
        public void ColumnQ0028() { doColumn("Q0028"); }
        public void ColumnQ0029() { doColumn("Q0029"); }
        public void ColumnQ0030() { doColumn("Q0030"); }
        public void ColumnQ0031() { doColumn("Q0031"); }
        public void ColumnQ0032() { doColumn("Q0032"); }
        public void ColumnQ0033() { doColumn("Q0033"); }
        public void ColumnQ0034() { doColumn("Q0034"); }
        public void ColumnQ0035() { doColumn("Q0035"); }
        public void ColumnQ0036() { doColumn("Q0036"); }
        public void ColumnQ0037() { doColumn("Q0037"); }
        public void ColumnQ0038() { doColumn("Q0038"); }
        public void ColumnQ0039() { doColumn("Q0039"); }
        public void ColumnQ0040() { doColumn("Q0040"); }
        public void ColumnQ0041() { doColumn("Q0041"); }
        public void ColumnQ0042() { doColumn("Q0042"); }
        public void ColumnQ0043() { doColumn("Q0043"); }
        public void ColumnQ0044() { doColumn("Q0044"); }
        public void ColumnQ0045() { doColumn("Q0045"); }
        public void ColumnQ0046() { doColumn("Q0046"); }
        public void ColumnQ0047() { doColumn("Q0047"); }
        public void ColumnQ0048() { doColumn("Q0048"); }
        public void ColumnQ0049() { doColumn("Q0049"); }
        public void ColumnQ0050() { doColumn("Q0050"); }
        public void ColumnQ0051() { doColumn("Q0051"); }
        public void ColumnQ0052() { doColumn("Q0052"); }
        public void ColumnQ0053() { doColumn("Q0053"); }
        public void ColumnQ0054() { doColumn("Q0054"); }
        public void ColumnQ0055() { doColumn("Q0055"); }
        public void ColumnQ0056() { doColumn("Q0056"); }
        public void ColumnQ0057() { doColumn("Q0057"); }
        public void ColumnQ0058() { doColumn("Q0058"); }
        public void ColumnQ0059() { doColumn("Q0059"); }
        public void ColumnQ0060() { doColumn("Q0060"); }
        public void ColumnQ0061() { doColumn("Q0061"); }
        public void ColumnQ0062() { doColumn("Q0062"); }
        public void ColumnQ0063() { doColumn("Q0063"); }
        public void ColumnQ0064() { doColumn("Q0064"); }
        public void ColumnQ0065() { doColumn("Q0065"); }
        public void ColumnQ0066() { doColumn("Q0066"); }
        public void ColumnQ0067() { doColumn("Q0067"); }
        public void ColumnQ0068() { doColumn("Q0068"); }
        public void ColumnQ0069() { doColumn("Q0069"); }
        public void ColumnQ0070() { doColumn("Q0070"); }
        public void ColumnQ0071() { doColumn("Q0071"); }
        public void ColumnQ0072() { doColumn("Q0072"); }
        public void ColumnQ0073() { doColumn("Q0073"); }
        public void ColumnQ0074() { doColumn("Q0074"); }
        public void ColumnQ0075() { doColumn("Q0075"); }
        public void ColumnQ0076() { doColumn("Q0076"); }
        public void ColumnQ0077() { doColumn("Q0077"); }
        public void ColumnQ0078() { doColumn("Q0078"); }
        public void ColumnQ0079() { doColumn("Q0079"); }
        public void ColumnQ0080() { doColumn("Q0080"); }
        public void ColumnQ0081() { doColumn("Q0081"); }
        public void ColumnQ0082() { doColumn("Q0082"); }
        public void ColumnQ0083() { doColumn("Q0083"); }
        public void ColumnQ0084() { doColumn("Q0084"); }
        public void ColumnQ0085() { doColumn("Q0085"); }
        public void ColumnQ0086() { doColumn("Q0086"); }
        public void ColumnQ0087() { doColumn("Q0087"); }
        public void ColumnQ0088() { doColumn("Q0088"); }
        public void ColumnQ0089() { doColumn("Q0089"); }
        public void ColumnQ0090() { doColumn("Q0090"); }
        public void ColumnQ0091() { doColumn("Q0091"); }
        public void ColumnQ0092() { doColumn("Q0092"); }
        public void ColumnQ0093() { doColumn("Q0093"); }
        public void ColumnQ0094() { doColumn("Q0094"); }
        public void ColumnQ0095() { doColumn("Q0095"); }
        public void ColumnQ0096() { doColumn("Q0096"); }
        public void ColumnQ0097() { doColumn("Q0097"); }
        public void ColumnQ0098() { doColumn("Q0098"); }
        public void ColumnQ0099() { doColumn("Q0099"); }
        public void ColumnQ0100() { doColumn("Q0100"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnSampleId(); // PK
        }
        protected override String getTableDbName() { return "T_SURVEY_DATA"; }
    }
}
