
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
    public class BsTOutputSettingGtCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSettingGtCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_GT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? qcwebid) {
            assertObjectNotNull("qcwebid", qcwebid);
            BsTOutputSettingGtCB cb = this;
            cb.Query().SetQcwebid_Equal(qcwebid);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_Qcwebid_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_Qcwebid_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputSettingGtCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSettingGtCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSettingGtCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSettingGtCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSettingGtCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSettingGtCB> unionQuery) {
            TOutputSettingGtCB cb = new TOutputSettingGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSettingGtCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSettingGtCB> unionQuery) {
            TOutputSettingGtCB cb = new TOutputSettingGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSettingGtCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputSettingGtCBSpecification _specification;
        public TOutputSettingGtCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSettingGtCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSettingGtCQ> {
			protected BsTOutputSettingGtCB _myCB;
			public MySpQyCall(BsTOutputSettingGtCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSettingGtCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSettingGtCB> ColumnQuery(SpecifyQuery<TOutputSettingGtCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSettingGtCB>(delegate(SpecifyQuery<TOutputSettingGtCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSettingGtCB xcreateColumnQueryCB() {
            TOutputSettingGtCB cb = new TOutputSettingGtCB();
            cb.xsetupForColumnQuery((TOutputSettingGtCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSettingGtCB> orQuery) {
            xorQ((TOutputSettingGtCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSettingGtCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSettingGtCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSettingGtCBColQySpQyCall : HpSpQyCall<TOutputSettingGtCQ> {
        protected TOutputSettingGtCB _mainCB;
        public TOutputSettingGtCBColQySpQyCall(TOutputSettingGtCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSettingGtCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSettingGtCBSpecification : AbstractSpecification<TOutputSettingGtCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfoAsOne;
        public TOutputSettingGtCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSettingGtCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnGtNpFlag() { doColumn("GT_NP_FLAG"); }
        public void ColumnGtNFlag() { doColumn("GT_N_FLAG"); }
        public void ColumnGtPFlag() { doColumn("GT_P_FLAG"); }
        public void ColumnPageSettingNpFlag() { doColumn("PAGE_SETTING_NP_FLAG"); }
        public void ColumnPageSettingNFlag() { doColumn("PAGE_SETTING_N_FLAG"); }
        public void ColumnPageSettingPFlag() { doColumn("PAGE_SETTING_P_FLAG"); }
        public void ColumnPageSettingPaperSize() { doColumn("PAGE_SETTING_PAPER_SIZE"); }
        public void ColumnPageSettingPaperOrientation() { doColumn("PAGE_SETTING_PAPER_ORIENTATION"); }
        public void ColumnOutputGraphFlag() { doColumn("OUTPUT_GRAPH_FLAG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnQcwebid(); // PK
        }
        protected override String getTableDbName() { return "T_OUTPUT_SETTING_GT"; }
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
		    protected HpSpQyCall<TOutputSettingGtCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TOutputSettingGtCQ> myQyCall) { _qyCall = myQyCall; }
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
		    protected HpSpQyCall<TOutputSettingGtCQ> _qyCall;
		    public TQcwebSurveyInfoAsOneSpQyCall(HpSpQyCall<TOutputSettingGtCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfoAsOne(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfoAsOne(); }
		}
    }
}
