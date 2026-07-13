
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
    public class BsTReportCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TReportCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? reportId) {
            assertObjectNotNull("reportId", reportId);
            BsTReportCB cb = this;
            cb.Query().SetReportId_Equal(reportId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ReportId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ReportId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TReportCQ Query() {
            return this.ConditionQuery;
        }

        public TReportCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TReportCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TReportCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TReportCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TReportCB> unionQuery) {
            TReportCB cb = new TReportCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TReportCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TReportCB> unionQuery) {
            TReportCB cb = new TReportCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TReportCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TReportsetNss _nssTReportset;
        public TReportsetNss NssTReportset { get {
            if (_nssTReportset == null) { _nssTReportset = new TReportsetNss(null); }
            return _nssTReportset;
        }}
        public TReportsetNss SetupSelect_TReportset() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnReportsetId();
            }
            doSetupSelect(delegate { return Query().QueryTReportset(); });
            if (_nssTReportset == null || !_nssTReportset.HasConditionQuery)
            { _nssTReportset = new TReportsetNss(Query().QueryTReportset()); }
            return _nssTReportset;
        }
        protected TReportChildNss _nssTReportChild;
        public TReportChildNss NssTReportChild { get {
            if (_nssTReportChild == null) { _nssTReportChild = new TReportChildNss(null); }
            return _nssTReportChild;
        }}
        public TReportChildNss SetupSelect_TReportChild() {
            doSetupSelect(delegate { return Query().QueryTReportChild(); });
            if (_nssTReportChild == null || !_nssTReportChild.HasConditionQuery)
            { _nssTReportChild = new TReportChildNss(Query().QueryTReportChild()); }
            return _nssTReportChild;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TReportCBSpecification _specification;
        public TReportCBSpecification Specify() {
            if (_specification == null) { _specification = new TReportCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TReportCQ> {
			protected BsTReportCB _myCB;
			public MySpQyCall(BsTReportCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TReportCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TReportCB> ColumnQuery(SpecifyQuery<TReportCB> leftSpecifyQuery) {
            return new HpColQyOperand<TReportCB>(delegate(SpecifyQuery<TReportCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TReportCB xcreateColumnQueryCB() {
            TReportCB cb = new TReportCB();
            cb.xsetupForColumnQuery((TReportCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TReportCB> orQuery) {
            xorQ((TReportCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TReportCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TReportCBColQySpQyCall(mainCB));
        }
    }

    public class TReportCBColQySpQyCall : HpSpQyCall<TReportCQ> {
        protected TReportCB _mainCB;
        public TReportCBColQySpQyCall(TReportCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TReportCQ qy() { return _mainCB.Query(); } 
    }

    public class TReportCBSpecification : AbstractSpecification<TReportCQ> {
        protected TReportsetCBSpecification _tReportset;
        protected TReportChildCBSpecification _tReportChild;
        public TReportCBSpecification(ConditionBean baseCB, HpSpQyCall<TReportCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnReportId() { doColumn("REPORT_ID"); }
        public void ColumnReportsetId() { doColumn("REPORTSET_ID"); }
        public void ColumnTargetScenarioItemId() { doColumn("TARGET_SCENARIO_ITEM_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnChildDiv() { doColumn("CHILD_DIV"); }
        public void ColumnScenarioType() { doColumn("SCENARIO_TYPE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnReportId(); // PK
            if (qyCall().qy().hasConditionQueryTReportset()
                    || qyCall().qy().xgetReferrerQuery() is TReportsetCQ) {
                ColumnReportsetId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_REPORT"; }
        public TReportsetCBSpecification SpecifyTReportset() {
            assertForeign("tReportset");
            if (_tReportset == null) {
                _tReportset = new TReportsetCBSpecification(_baseCB, new TReportsetSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tReportset.xsetSyncQyCall(new TReportsetSpQyCall(xsyncQyCall())); }
            }
            return _tReportset;
        }
		public class TReportsetSpQyCall : HpSpQyCall<TReportsetCQ> {
		    protected HpSpQyCall<TReportCQ> _qyCall;
		    public TReportsetSpQyCall(HpSpQyCall<TReportCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTReportset(); }
			public TReportsetCQ qy() { return _qyCall.qy().QueryTReportset(); }
		}
        public TReportChildCBSpecification SpecifyTReportChild() {
            assertForeign("tReportChild");
            if (_tReportChild == null) {
                _tReportChild = new TReportChildCBSpecification(_baseCB, new TReportChildSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tReportChild.xsetSyncQyCall(new TReportChildSpQyCall(xsyncQyCall())); }
            }
            return _tReportChild;
        }
		public class TReportChildSpQyCall : HpSpQyCall<TReportChildCQ> {
		    protected HpSpQyCall<TReportCQ> _qyCall;
		    public TReportChildSpQyCall(HpSpQyCall<TReportCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTReportChild(); }
			public TReportChildCQ qy() { return _qyCall.qy().QueryTReportChild(); }
		}
        public RAFunction<TReportChildCB, TReportCQ> DerivedTReportChildList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TReportChildCB, TReportCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TReportChildCB> subQuery, TReportCQ cq, String aliasName)
                { cq.xsderiveTReportChildList(function, subQuery, aliasName); });
        }
    }
}
