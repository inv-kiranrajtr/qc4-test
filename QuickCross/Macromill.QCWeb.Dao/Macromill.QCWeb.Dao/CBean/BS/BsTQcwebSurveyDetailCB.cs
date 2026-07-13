
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
    public class BsTQcwebSurveyDetailCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TQcwebSurveyDetailCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_QCWEB_SURVEY_DETAIL"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? qcwebDetailId) {
            assertObjectNotNull("qcwebDetailId", qcwebDetailId);
            BsTQcwebSurveyDetailCB cb = this;
            cb.Query().SetQcwebDetailId_Equal(qcwebDetailId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_QcwebDetailId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_QcwebDetailId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TQcwebSurveyDetailCQ Query() {
            return this.ConditionQuery;
        }

        public TQcwebSurveyDetailCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TQcwebSurveyDetailCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TQcwebSurveyDetailCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TQcwebSurveyDetailCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TQcwebSurveyDetailCB> unionQuery) {
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TQcwebSurveyDetailCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TQcwebSurveyDetailCB> unionQuery) {
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TQcwebSurveyDetailCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TQcwebSurveyDetailCBSpecification _specification;
        public TQcwebSurveyDetailCBSpecification Specify() {
            if (_specification == null) { _specification = new TQcwebSurveyDetailCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TQcwebSurveyDetailCQ> {
			protected BsTQcwebSurveyDetailCB _myCB;
			public MySpQyCall(BsTQcwebSurveyDetailCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TQcwebSurveyDetailCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TQcwebSurveyDetailCB> ColumnQuery(SpecifyQuery<TQcwebSurveyDetailCB> leftSpecifyQuery) {
            return new HpColQyOperand<TQcwebSurveyDetailCB>(delegate(SpecifyQuery<TQcwebSurveyDetailCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TQcwebSurveyDetailCB xcreateColumnQueryCB() {
            TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB();
            cb.xsetupForColumnQuery((TQcwebSurveyDetailCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TQcwebSurveyDetailCB> orQuery) {
            xorQ((TQcwebSurveyDetailCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TQcwebSurveyDetailCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TQcwebSurveyDetailCBColQySpQyCall(mainCB));
        }
    }

    public class TQcwebSurveyDetailCBColQySpQyCall : HpSpQyCall<TQcwebSurveyDetailCQ> {
        protected TQcwebSurveyDetailCB _mainCB;
        public TQcwebSurveyDetailCBColQySpQyCall(TQcwebSurveyDetailCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TQcwebSurveyDetailCQ qy() { return _mainCB.Query(); } 
    }

    public class TQcwebSurveyDetailCBSpecification : AbstractSpecification<TQcwebSurveyDetailCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        public TQcwebSurveyDetailCBSpecification(ConditionBean baseCB, HpSpQyCall<TQcwebSurveyDetailCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnQcwebDetailId() { doColumn("QCWEB_DETAIL_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnSurveyNo() { doColumn("SURVEY_NO"); }
        public void ColumnSurveyName() { doColumn("SURVEY_NAME"); }
        public void ColumnQc3uniqueId() { doColumn("QC3UNIQUE_ID"); }
        public void ColumnSurveyMethod() { doColumn("SURVEY_METHOD"); }
        public void ColumnServiceType() { doColumn("SERVICE_TYPE"); }
        public void ColumnSurveyDate() { doColumn("SURVEY_DATE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnQcwebDetailId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_QCWEB_SURVEY_DETAIL"; }
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
		    protected HpSpQyCall<TQcwebSurveyDetailCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TQcwebSurveyDetailCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
    }
}
