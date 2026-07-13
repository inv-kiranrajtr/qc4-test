
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
    public class BsTReportsetCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TReportsetCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORTSET"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? reportsetId) {
            assertObjectNotNull("reportsetId", reportsetId);
            BsTReportsetCB cb = this;
            cb.Query().SetReportsetId_Equal(reportsetId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ReportsetId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ReportsetId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TReportsetCQ Query() {
            return this.ConditionQuery;
        }

        public TReportsetCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TReportsetCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TReportsetCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TReportsetCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TReportsetCB> unionQuery) {
            TReportsetCB cb = new TReportsetCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TReportsetCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TReportsetCB> unionQuery) {
            TReportsetCB cb = new TReportsetCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TReportsetCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TQcwebSurveyInfoNss _nssTQcwebSurveyInfo;
        public TQcwebSurveyInfoNss NssTQcwebSurveyInfo { get {
            if (_nssTQcwebSurveyInfo == null) { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(null); }
            return _nssTQcwebSurveyInfo;
        }}
        public TQcwebSurveyInfoNss SetupSelect_TQcwebSurveyInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfo(); });
            if (_nssTQcwebSurveyInfo == null || !_nssTQcwebSurveyInfo.HasConditionQuery)
            { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfo()); }
            return _nssTQcwebSurveyInfo;
        }
        protected TReportNss _nssTReport;
        public TReportNss NssTReport { get {
            if (_nssTReport == null) { _nssTReport = new TReportNss(null); }
            return _nssTReport;
        }}
        public TReportNss SetupSelect_TReport() {
            doSetupSelect(delegate { return Query().QueryTReport(); });
            if (_nssTReport == null || !_nssTReport.HasConditionQuery)
            { _nssTReport = new TReportNss(Query().QueryTReport()); }
            return _nssTReport;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TReportsetCBSpecification _specification;
        public TReportsetCBSpecification Specify() {
            if (_specification == null) { _specification = new TReportsetCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TReportsetCQ> {
			protected BsTReportsetCB _myCB;
			public MySpQyCall(BsTReportsetCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TReportsetCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TReportsetCB> ColumnQuery(SpecifyQuery<TReportsetCB> leftSpecifyQuery) {
            return new HpColQyOperand<TReportsetCB>(delegate(SpecifyQuery<TReportsetCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TReportsetCB xcreateColumnQueryCB() {
            TReportsetCB cb = new TReportsetCB();
            cb.xsetupForColumnQuery((TReportsetCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TReportsetCB> orQuery) {
            xorQ((TReportsetCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TReportsetCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TReportsetCBColQySpQyCall(mainCB));
        }
    }

    public class TReportsetCBColQySpQyCall : HpSpQyCall<TReportsetCQ> {
        protected TReportsetCB _mainCB;
        public TReportsetCBColQySpQyCall(TReportsetCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TReportsetCQ qy() { return _mainCB.Query(); } 
    }

    public class TReportsetCBSpecification : AbstractSpecification<TReportsetCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TReportCBSpecification _tReport;
        public TReportsetCBSpecification(ConditionBean baseCB, HpSpQyCall<TReportsetCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnReportsetId() { doColumn("REPORTSET_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnReportsetName() { doColumn("REPORTSET_NAME"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnReportsetId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_REPORTSET"; }
        public TQcwebSurveyInfoCBSpecification SpecifyTQcwebSurveyInfo() {
            assertForeign("tQcwebSurveyInfo");
            if (_tQcwebSurveyInfo == null) {
                _tQcwebSurveyInfo = new TQcwebSurveyInfoCBSpecification(_baseCB, new TQcwebSurveyInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tQcwebSurveyInfo.xsetSyncQyCall(new TQcwebSurveyInfoSpQyCall(xsyncQyCall())); }
            }
            return _tQcwebSurveyInfo;
        }
		public class TQcwebSurveyInfoSpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
		    protected HpSpQyCall<TReportsetCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TReportsetCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
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
		    protected HpSpQyCall<TReportsetCQ> _qyCall;
		    public TReportSpQyCall(HpSpQyCall<TReportsetCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTReport(); }
			public TReportCQ qy() { return _qyCall.qy().QueryTReport(); }
		}
        public RAFunction<TReportCB, TReportsetCQ> DerivedTReportList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TReportCB, TReportsetCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TReportCB> subQuery, TReportsetCQ cq, String aliasName)
                { cq.xsderiveTReportList(function, subQuery, aliasName); });
        }
    }
}
