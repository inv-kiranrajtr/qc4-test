
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
    public class BsTOutputSettingReportCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSettingReportCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_REPORT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? qcwebid) {
            assertObjectNotNull("qcwebid", qcwebid);
            BsTOutputSettingReportCB cb = this;
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
        public TOutputSettingReportCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSettingReportCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSettingReportCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSettingReportCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSettingReportCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSettingReportCB> unionQuery) {
            TOutputSettingReportCB cb = new TOutputSettingReportCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSettingReportCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSettingReportCB> unionQuery) {
            TOutputSettingReportCB cb = new TOutputSettingReportCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSettingReportCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputSettingReportCBSpecification _specification;
        public TOutputSettingReportCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSettingReportCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSettingReportCQ> {
			protected BsTOutputSettingReportCB _myCB;
			public MySpQyCall(BsTOutputSettingReportCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSettingReportCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSettingReportCB> ColumnQuery(SpecifyQuery<TOutputSettingReportCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSettingReportCB>(delegate(SpecifyQuery<TOutputSettingReportCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSettingReportCB xcreateColumnQueryCB() {
            TOutputSettingReportCB cb = new TOutputSettingReportCB();
            cb.xsetupForColumnQuery((TOutputSettingReportCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSettingReportCB> orQuery) {
            xorQ((TOutputSettingReportCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSettingReportCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSettingReportCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSettingReportCBColQySpQyCall : HpSpQyCall<TOutputSettingReportCQ> {
        protected TOutputSettingReportCB _mainCB;
        public TOutputSettingReportCBColQySpQyCall(TOutputSettingReportCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSettingReportCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSettingReportCBSpecification : AbstractSpecification<TOutputSettingReportCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfoAsOne;
        public TOutputSettingReportCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSettingReportCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnFileTypeExcelFlag() { doColumn("FILE_TYPE_EXCEL_FLAG"); }
        public void ColumnFileTypePpFlag() { doColumn("FILE_TYPE_PP_FLAG"); }
        public void ColumnFileTypePdfFlag() { doColumn("FILE_TYPE_PDF_FLAG"); }
        public void ColumnReportType() { doColumn("REPORT_TYPE"); }
        public void ColumnGraphOutputFlag() { doColumn("GRAPH_OUTPUT_FLAG"); }
        public void ColumnAscFlag() { doColumn("ASC_FLAG"); }
        public void ColumnCommentVisibleFlag() { doColumn("COMMENT_VISIBLE_FLAG"); }
        public void ColumnSurveyReportFlag() { doColumn("SURVEY_REPORT_FLAG"); }
        public void ColumnOutputTemplateId() { doColumn("OUTPUT_TEMPLATE_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnQcwebid(); // PK
        }
        protected override String getTableDbName() { return "T_OUTPUT_SETTING_REPORT"; }
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
		    protected HpSpQyCall<TOutputSettingReportCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TOutputSettingReportCQ> myQyCall) { _qyCall = myQyCall; }
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
		    protected HpSpQyCall<TOutputSettingReportCQ> _qyCall;
		    public TQcwebSurveyInfoAsOneSpQyCall(HpSpQyCall<TOutputSettingReportCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfoAsOne(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfoAsOne(); }
		}
    }
}
