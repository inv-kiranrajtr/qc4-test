
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
    public class BsTOutputRequestCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputRequestCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_REQUEST"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputRequestId) {
            assertObjectNotNull("outputRequestId", outputRequestId);
            BsTOutputRequestCB cb = this;
            cb.Query().SetOutputRequestId_Equal(outputRequestId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputRequestId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputRequestId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputRequestCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputRequestCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputRequestCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputRequestCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputRequestCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputRequestCB> unionQuery) {
            TOutputRequestCB cb = new TOutputRequestCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputRequestCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputRequestCB> unionQuery) {
            TOutputRequestCB cb = new TOutputRequestCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputRequestCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputReportsetInfoNss _nssTOutputReportsetInfo;
        public TOutputReportsetInfoNss NssTOutputReportsetInfo { get {
            if (_nssTOutputReportsetInfo == null) { _nssTOutputReportsetInfo = new TOutputReportsetInfoNss(null); }
            return _nssTOutputReportsetInfo;
        }}
        public TOutputReportsetInfoNss SetupSelect_TOutputReportsetInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnOutputReportsetInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTOutputReportsetInfo(); });
            if (_nssTOutputReportsetInfo == null || !_nssTOutputReportsetInfo.HasConditionQuery)
            { _nssTOutputReportsetInfo = new TOutputReportsetInfoNss(Query().QueryTOutputReportsetInfo()); }
            return _nssTOutputReportsetInfo;
        }
        protected TOutputCommonNss _nssTOutputCommon;
        public TOutputCommonNss NssTOutputCommon { get {
            if (_nssTOutputCommon == null) { _nssTOutputCommon = new TOutputCommonNss(null); }
            return _nssTOutputCommon;
        }}
        public TOutputCommonNss SetupSelect_TOutputCommon() {
            doSetupSelect(delegate { return Query().QueryTOutputCommon(); });
            if (_nssTOutputCommon == null || !_nssTOutputCommon.HasConditionQuery)
            { _nssTOutputCommon = new TOutputCommonNss(Query().QueryTOutputCommon()); }
            return _nssTOutputCommon;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputRequestCBSpecification _specification;
        public TOutputRequestCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputRequestCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputRequestCQ> {
			protected BsTOutputRequestCB _myCB;
			public MySpQyCall(BsTOutputRequestCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputRequestCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputRequestCB> ColumnQuery(SpecifyQuery<TOutputRequestCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputRequestCB>(delegate(SpecifyQuery<TOutputRequestCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputRequestCB xcreateColumnQueryCB() {
            TOutputRequestCB cb = new TOutputRequestCB();
            cb.xsetupForColumnQuery((TOutputRequestCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputRequestCB> orQuery) {
            xorQ((TOutputRequestCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputRequestCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputRequestCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputRequestCBColQySpQyCall : HpSpQyCall<TOutputRequestCQ> {
        protected TOutputRequestCB _mainCB;
        public TOutputRequestCBColQySpQyCall(TOutputRequestCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputRequestCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputRequestCBSpecification : AbstractSpecification<TOutputRequestCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TOutputReportsetInfoCBSpecification _tOutputReportsetInfo;
        protected TOutputCommonCBSpecification _tOutputCommon;
        public TOutputRequestCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputRequestCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputRequestId() { doColumn("OUTPUT_REQUEST_ID"); }
        public void ColumnRequestServerCode() { doColumn("REQUEST_SERVER_CODE"); }
        public void ColumnRequestUserId() { doColumn("REQUEST_USER_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnLastDownloadUserid() { doColumn("LAST_DOWNLOAD_USERID"); }
        public void ColumnRequestDatetime() { doColumn("REQUEST_DATETIME"); }
        public void ColumnDownloadPath() { doColumn("DOWNLOAD_PATH"); }
        public void ColumnProcServerCode() { doColumn("PROC_SERVER_CODE"); }
        public void ColumnStatusCode() { doColumn("STATUS_CODE"); }
        public void ColumnDescription() { doColumn("DESCRIPTION"); }
        public void ColumnEndDatetime() { doColumn("END_DATETIME"); }
        public void ColumnLastDownloadDatetime() { doColumn("LAST_DOWNLOAD_DATETIME"); }
        public void ColumnExcelbookType() { doColumn("EXCELBOOK_TYPE"); }
        public void ColumnNumericAnswerViewCode() { doColumn("NUMERIC_ANSWER_VIEW_CODE"); }
        public void ColumnDpTotal() { doColumn("DP_TOTAL"); }
        public void ColumnDpAverage() { doColumn("DP_AVERAGE"); }
        public void ColumnDpStandardDiv() { doColumn("DP_STANDARD_DIV"); }
        public void ColumnDpMin() { doColumn("DP_MIN"); }
        public void ColumnDpMax() { doColumn("DP_MAX"); }
        public void ColumnDpMedian() { doColumn("DP_MEDIAN"); }
        public void ColumnDpWeight() { doColumn("DP_WEIGHT"); }
        public void ColumnDpWeightavr() { doColumn("DP_WEIGHTAVR"); }
        public void ColumnProcWeight() { doColumn("PROC_WEIGHT"); }
        public void ColumnOutputReportsetInfoId() { doColumn("OUTPUT_REPORTSET_INFO_ID"); }
        public void ColumnDeleteFlag() { doColumn("DELETE_FLAG"); }
        public void ColumnViewSurveyName() { doColumn("VIEW_SURVEY_NAME"); }
        public void ColumnLanguage() { doColumn("LANGUAGE"); }
        public void ColumnShowZeroNaIvCode() { doColumn("SHOW_ZERO_NA_IV_CODE"); }
        public void ColumnMergeAxisCellsFlag() { doColumn("MERGE_AXIS_CELLS_FLAG"); }
        public void ColumnScenarioName() { doColumn("SCENARIO_NAME"); }
        public void ColumnStartDatetime() { doColumn("START_DATETIME"); }
        public void ColumnTestLogFlag() { doColumn("TEST_LOG_FLAG"); }
        public void ColumnTsvFileSizeGt() { doColumn("TSV_FILE_SIZE_GT"); }
        public void ColumnTsvFileSizeCross() { doColumn("TSV_FILE_SIZE_CROSS"); }
        public void ColumnTsvFileSizeFa() { doColumn("TSV_FILE_SIZE_FA"); }
        public void ColumnTsvFileSizeDataOutput() { doColumn("TSV_FILE_SIZE_DATA_OUTPUT"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputRequestId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTOutputReportsetInfo()
                    || qyCall().qy().xgetReferrerQuery() is TOutputReportsetInfoCQ) {
                ColumnOutputReportsetInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_REQUEST"; }
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
		    protected HpSpQyCall<TOutputRequestCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TOutputRequestCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TOutputReportsetInfoCBSpecification SpecifyTOutputReportsetInfo() {
            assertForeign("tOutputReportsetInfo");
            if (_tOutputReportsetInfo == null) {
                _tOutputReportsetInfo = new TOutputReportsetInfoCBSpecification(_baseCB, new TOutputReportsetInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputReportsetInfo.xsetSyncQyCall(new TOutputReportsetInfoSpQyCall(xsyncQyCall())); }
            }
            return _tOutputReportsetInfo;
        }
		public class TOutputReportsetInfoSpQyCall : HpSpQyCall<TOutputReportsetInfoCQ> {
		    protected HpSpQyCall<TOutputRequestCQ> _qyCall;
		    public TOutputReportsetInfoSpQyCall(HpSpQyCall<TOutputRequestCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputReportsetInfo(); }
			public TOutputReportsetInfoCQ qy() { return _qyCall.qy().QueryTOutputReportsetInfo(); }
		}
        public TOutputCommonCBSpecification SpecifyTOutputCommon() {
            assertForeign("tOutputCommon");
            if (_tOutputCommon == null) {
                _tOutputCommon = new TOutputCommonCBSpecification(_baseCB, new TOutputCommonSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputCommon.xsetSyncQyCall(new TOutputCommonSpQyCall(xsyncQyCall())); }
            }
            return _tOutputCommon;
        }
		public class TOutputCommonSpQyCall : HpSpQyCall<TOutputCommonCQ> {
		    protected HpSpQyCall<TOutputRequestCQ> _qyCall;
		    public TOutputCommonSpQyCall(HpSpQyCall<TOutputRequestCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputCommon(); }
			public TOutputCommonCQ qy() { return _qyCall.qy().QueryTOutputCommon(); }
		}
        public RAFunction<TOutputCommonCB, TOutputRequestCQ> DerivedTOutputCommonList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputCommonCB, TOutputRequestCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputCommonCB> subQuery, TOutputRequestCQ cq, String aliasName)
                { cq.xsderiveTOutputCommonList(function, subQuery, aliasName); });
        }
    }
}
