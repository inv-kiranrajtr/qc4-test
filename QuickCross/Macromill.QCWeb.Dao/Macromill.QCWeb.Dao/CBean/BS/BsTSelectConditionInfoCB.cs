
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
    public class BsTSelectConditionInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TSelectConditionInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SELECT_CONDITION_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(long? selectNo, decimal? qcwebid) {
            assertObjectNotNull("selectNo", selectNo);assertObjectNotNull("qcwebid", qcwebid);
            BsTSelectConditionInfoCB cb = this;
            cb.Query().SetSelectNo_Equal(selectNo);cb.Query().SetQcwebid_Equal(qcwebid);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_SelectNo_Asc();
            Query().AddOrderBy_Qcwebid_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_SelectNo_Desc();
            Query().AddOrderBy_Qcwebid_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TSelectConditionInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TSelectConditionInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TSelectConditionInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TSelectConditionInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TSelectConditionInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TSelectConditionInfoCB> unionQuery) {
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TSelectConditionInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TSelectConditionInfoCB> unionQuery) {
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TSelectConditionInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfo(); });
            if (_nssTQcwebSurveyInfo == null || !_nssTQcwebSurveyInfo.HasConditionQuery)
            { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfo()); }
            return _nssTQcwebSurveyInfo;
        }

        protected TQcwebSurveyInfoNss _nssTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoNss NssTQcwebSurveyInfoAsOne { get {
            if (_nssTQcwebSurveyInfoAsOne == null) { _nssTQcwebSurveyInfoAsOne = new TQcwebSurveyInfoNss(null); }
            return _nssTQcwebSurveyInfoAsOne;
        }}
        public TQcwebSurveyInfoNss SetupSelect_TQcwebSurveyInfoAsOne() {
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfoAsOne(); });
            if (_nssTQcwebSurveyInfoAsOne == null || !_nssTQcwebSurveyInfoAsOne.HasConditionQuery)
            { _nssTQcwebSurveyInfoAsOne = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfoAsOne()); }
            return _nssTQcwebSurveyInfoAsOne;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TSelectConditionInfoCBSpecification _specification;
        public TSelectConditionInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TSelectConditionInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TSelectConditionInfoCQ> {
			protected BsTSelectConditionInfoCB _myCB;
			public MySpQyCall(BsTSelectConditionInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TSelectConditionInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TSelectConditionInfoCB> ColumnQuery(SpecifyQuery<TSelectConditionInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TSelectConditionInfoCB>(delegate(SpecifyQuery<TSelectConditionInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TSelectConditionInfoCB xcreateColumnQueryCB() {
            TSelectConditionInfoCB cb = new TSelectConditionInfoCB();
            cb.xsetupForColumnQuery((TSelectConditionInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TSelectConditionInfoCB> orQuery) {
            xorQ((TSelectConditionInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TSelectConditionInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TSelectConditionInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TSelectConditionInfoCBColQySpQyCall : HpSpQyCall<TSelectConditionInfoCQ> {
        protected TSelectConditionInfoCB _mainCB;
        public TSelectConditionInfoCBColQySpQyCall(TSelectConditionInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TSelectConditionInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TSelectConditionInfoCBSpecification : AbstractSpecification<TSelectConditionInfoCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfoAsOne;
        public TSelectConditionInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TSelectConditionInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnSelectNo() { doColumn("SELECT_NO"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnQuestionNo() { doColumn("QUESTION_NO"); }
        public void ColumnChildQuestionNo() { doColumn("CHILD_QUESTION_NO"); }
        public void ColumnSelectCondition() { doColumn("SELECT_CONDITION"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnSelectNo(); // PK
            ColumnQcwebid(); // PK
        }
        protected override String getTableDbName() { return "T_SELECT_CONDITION_INFO"; }
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
		    protected HpSpQyCall<TSelectConditionInfoCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TSelectConditionInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TQcwebSurveyInfoCBSpecification SpecifyTQcwebSurveyInfoAsOne() {
            assertForeign("tQcwebSurveyInfoAsOne");
            if (_tQcwebSurveyInfoAsOne == null) {
                _tQcwebSurveyInfoAsOne = new TQcwebSurveyInfoCBSpecification(_baseCB, new TQcwebSurveyInfoAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tQcwebSurveyInfoAsOne.xsetSyncQyCall(new TQcwebSurveyInfoAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tQcwebSurveyInfoAsOne;
        }
		public class TQcwebSurveyInfoAsOneSpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
		    protected HpSpQyCall<TSelectConditionInfoCQ> _qyCall;
		    public TQcwebSurveyInfoAsOneSpQyCall(HpSpQyCall<TSelectConditionInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfoAsOne(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfoAsOne(); }
		}
    }
}
