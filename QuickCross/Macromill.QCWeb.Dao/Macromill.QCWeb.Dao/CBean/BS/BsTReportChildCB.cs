
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
    public class BsTReportChildCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TReportChildCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORT_CHILD"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? reportChildId) {
            assertObjectNotNull("reportChildId", reportChildId);
            BsTReportChildCB cb = this;
            cb.Query().SetReportChildId_Equal(reportChildId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ReportChildId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ReportChildId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TReportChildCQ Query() {
            return this.ConditionQuery;
        }

        public TReportChildCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TReportChildCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TReportChildCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TReportChildCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TReportChildCB> unionQuery) {
            TReportChildCB cb = new TReportChildCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TReportChildCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TReportChildCB> unionQuery) {
            TReportChildCB cb = new TReportChildCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TReportChildCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TReportNss _nssTReport;
        public TReportNss NssTReport { get {
            if (_nssTReport == null) { _nssTReport = new TReportNss(null); }
            return _nssTReport;
        }}
        public TReportNss SetupSelect_TReport() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnParentReportId();
            }
            doSetupSelect(delegate { return Query().QueryTReport(); });
            if (_nssTReport == null || !_nssTReport.HasConditionQuery)
            { _nssTReport = new TReportNss(Query().QueryTReport()); }
            return _nssTReport;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TReportChildCBSpecification _specification;
        public TReportChildCBSpecification Specify() {
            if (_specification == null) { _specification = new TReportChildCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TReportChildCQ> {
			protected BsTReportChildCB _myCB;
			public MySpQyCall(BsTReportChildCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TReportChildCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TReportChildCB> ColumnQuery(SpecifyQuery<TReportChildCB> leftSpecifyQuery) {
            return new HpColQyOperand<TReportChildCB>(delegate(SpecifyQuery<TReportChildCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TReportChildCB xcreateColumnQueryCB() {
            TReportChildCB cb = new TReportChildCB();
            cb.xsetupForColumnQuery((TReportChildCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TReportChildCB> orQuery) {
            xorQ((TReportChildCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TReportChildCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TReportChildCBColQySpQyCall(mainCB));
        }
    }

    public class TReportChildCBColQySpQyCall : HpSpQyCall<TReportChildCQ> {
        protected TReportChildCB _mainCB;
        public TReportChildCBColQySpQyCall(TReportChildCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TReportChildCQ qy() { return _mainCB.Query(); } 
    }

    public class TReportChildCBSpecification : AbstractSpecification<TReportChildCQ> {
        protected TReportCBSpecification _tReport;
        public TReportChildCBSpecification(ConditionBean baseCB, HpSpQyCall<TReportChildCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnReportChildId() { doColumn("REPORT_CHILD_ID"); }
        public void ColumnParentReportId() { doColumn("PARENT_REPORT_ID"); }
        public void ColumnTargetScenarioItemId() { doColumn("TARGET_SCENARIO_ITEM_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnReportChildId(); // PK
            if (qyCall().qy().hasConditionQueryTReport()
                    || qyCall().qy().xgetReferrerQuery() is TReportCQ) {
                ColumnParentReportId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_REPORT_CHILD"; }
        public TReportCBSpecification SpecifyTReport() {
            assertForeign("tReport");
            if (_tReport == null) {
                _tReport = new TReportCBSpecification(_baseCB, new TReportSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tReport.xsetSyncQyCall(new TReportSpQyCall(xsyncQyCall())); }
            }
            return _tReport;
        }
		public class TReportSpQyCall : HpSpQyCall<TReportCQ> {
		    protected HpSpQyCall<TReportChildCQ> _qyCall;
		    public TReportSpQyCall(HpSpQyCall<TReportChildCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTReport(); }
			public TReportCQ qy() { return _qyCall.qy().QueryTReport(); }
		}
    }
}
