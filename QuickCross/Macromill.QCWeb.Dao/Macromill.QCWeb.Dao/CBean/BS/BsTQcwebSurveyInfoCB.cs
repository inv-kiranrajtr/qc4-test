
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
    public class BsTQcwebSurveyInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TQcwebSurveyInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_QCWEB_SURVEY_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? qcwebid) {
            assertObjectNotNull("qcwebid", qcwebid);
            BsTQcwebSurveyInfoCB cb = this;
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
        public TQcwebSurveyInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TQcwebSurveyInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TQcwebSurveyInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TQcwebSurveyInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TQcwebSurveyInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TQcwebSurveyInfoCB> unionQuery) {
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TQcwebSurveyInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TQcwebSurveyInfoCB> unionQuery) {
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TQcwebSurveyInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TSurveyInfoNss _nssTSurveyInfo;
        public TSurveyInfoNss NssTSurveyInfo { get {
            if (_nssTSurveyInfo == null) { _nssTSurveyInfo = new TSurveyInfoNss(null); }
            return _nssTSurveyInfo;
        }}
        public TSurveyInfoNss SetupSelect_TSurveyInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnSurveyInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTSurveyInfo(); });
            if (_nssTSurveyInfo == null || !_nssTSurveyInfo.HasConditionQuery)
            { _nssTSurveyInfo = new TSurveyInfoNss(Query().QueryTSurveyInfo()); }
            return _nssTSurveyInfo;
        }
        protected TRawdataImportQueInfoNss _nssTRawdataImportQueInfo;
        public TRawdataImportQueInfoNss NssTRawdataImportQueInfo { get {
            if (_nssTRawdataImportQueInfo == null) { _nssTRawdataImportQueInfo = new TRawdataImportQueInfoNss(null); }
            return _nssTRawdataImportQueInfo;
        }}
        public TRawdataImportQueInfoNss SetupSelect_TRawdataImportQueInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnRawdataImportQueInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTRawdataImportQueInfo(); });
            if (_nssTRawdataImportQueInfo == null || !_nssTRawdataImportQueInfo.HasConditionQuery)
            { _nssTRawdataImportQueInfo = new TRawdataImportQueInfoNss(Query().QueryTRawdataImportQueInfo()); }
            return _nssTRawdataImportQueInfo;
        }
        protected TAllocationCellInfoNss _nssTAllocationCellInfo;
        public TAllocationCellInfoNss NssTAllocationCellInfo { get {
            if (_nssTAllocationCellInfo == null) { _nssTAllocationCellInfo = new TAllocationCellInfoNss(null); }
            return _nssTAllocationCellInfo;
        }}
        public TAllocationCellInfoNss SetupSelect_TAllocationCellInfo() {
            doSetupSelect(delegate { return Query().QueryTAllocationCellInfo(); });
            if (_nssTAllocationCellInfo == null || !_nssTAllocationCellInfo.HasConditionQuery)
            { _nssTAllocationCellInfo = new TAllocationCellInfoNss(Query().QueryTAllocationCellInfo()); }
            return _nssTAllocationCellInfo;
        }
        protected TSelectConditionInfoNss _nssTSelectConditionInfo;
        public TSelectConditionInfoNss NssTSelectConditionInfo { get {
            if (_nssTSelectConditionInfo == null) { _nssTSelectConditionInfo = new TSelectConditionInfoNss(null); }
            return _nssTSelectConditionInfo;
        }}
        public TSelectConditionInfoNss SetupSelect_TSelectConditionInfo() {
            doSetupSelect(delegate { return Query().QueryTSelectConditionInfo(); });
            if (_nssTSelectConditionInfo == null || !_nssTSelectConditionInfo.HasConditionQuery)
            { _nssTSelectConditionInfo = new TSelectConditionInfoNss(Query().QueryTSelectConditionInfo()); }
            return _nssTSelectConditionInfo;
        }
        protected TItemInfoNss _nssTItemInfo;
        public TItemInfoNss NssTItemInfo { get {
            if (_nssTItemInfo == null) { _nssTItemInfo = new TItemInfoNss(null); }
            return _nssTItemInfo;
        }}
        public TItemInfoNss SetupSelect_TItemInfo() {
            doSetupSelect(delegate { return Query().QueryTItemInfo(); });
            if (_nssTItemInfo == null || !_nssTItemInfo.HasConditionQuery)
            { _nssTItemInfo = new TItemInfoNss(Query().QueryTItemInfo()); }
            return _nssTItemInfo;
        }
        protected TTableControlNss _nssTTableControl;
        public TTableControlNss NssTTableControl { get {
            if (_nssTTableControl == null) { _nssTTableControl = new TTableControlNss(null); }
            return _nssTTableControl;
        }}
        public TTableControlNss SetupSelect_TTableControl() {
            doSetupSelect(delegate { return Query().QueryTTableControl(); });
            if (_nssTTableControl == null || !_nssTTableControl.HasConditionQuery)
            { _nssTTableControl = new TTableControlNss(Query().QueryTTableControl()); }
            return _nssTTableControl;
        }
        protected TDefaultEnvNss _nssTDefaultEnv;
        public TDefaultEnvNss NssTDefaultEnv { get {
            if (_nssTDefaultEnv == null) { _nssTDefaultEnv = new TDefaultEnvNss(null); }
            return _nssTDefaultEnv;
        }}
        public TDefaultEnvNss SetupSelect_TDefaultEnv() {
            doSetupSelect(delegate { return Query().QueryTDefaultEnv(); });
            if (_nssTDefaultEnv == null || !_nssTDefaultEnv.HasConditionQuery)
            { _nssTDefaultEnv = new TDefaultEnvNss(Query().QueryTDefaultEnv()); }
            return _nssTDefaultEnv;
        }
        protected TDefaultEnvColorInfoNss _nssTDefaultEnvColorInfo;
        public TDefaultEnvColorInfoNss NssTDefaultEnvColorInfo { get {
            if (_nssTDefaultEnvColorInfo == null) { _nssTDefaultEnvColorInfo = new TDefaultEnvColorInfoNss(null); }
            return _nssTDefaultEnvColorInfo;
        }}
        public TDefaultEnvColorInfoNss SetupSelect_TDefaultEnvColorInfo() {
            doSetupSelect(delegate { return Query().QueryTDefaultEnvColorInfo(); });
            if (_nssTDefaultEnvColorInfo == null || !_nssTDefaultEnvColorInfo.HasConditionQuery)
            { _nssTDefaultEnvColorInfo = new TDefaultEnvColorInfoNss(Query().QueryTDefaultEnvColorInfo()); }
            return _nssTDefaultEnvColorInfo;
        }
        protected TScenarioTotalizationNss _nssTScenarioTotalization;
        public TScenarioTotalizationNss NssTScenarioTotalization { get {
            if (_nssTScenarioTotalization == null) { _nssTScenarioTotalization = new TScenarioTotalizationNss(null); }
            return _nssTScenarioTotalization;
        }}
        public TScenarioTotalizationNss SetupSelect_TScenarioTotalization() {
            doSetupSelect(delegate { return Query().QueryTScenarioTotalization(); });
            if (_nssTScenarioTotalization == null || !_nssTScenarioTotalization.HasConditionQuery)
            { _nssTScenarioTotalization = new TScenarioTotalizationNss(Query().QueryTScenarioTotalization()); }
            return _nssTScenarioTotalization;
        }
        protected TReportsetNss _nssTReportset;
        public TReportsetNss NssTReportset { get {
            if (_nssTReportset == null) { _nssTReportset = new TReportsetNss(null); }
            return _nssTReportset;
        }}
        public TReportsetNss SetupSelect_TReportset() {
            doSetupSelect(delegate { return Query().QueryTReportset(); });
            if (_nssTReportset == null || !_nssTReportset.HasConditionQuery)
            { _nssTReportset = new TReportsetNss(Query().QueryTReportset()); }
            return _nssTReportset;
        }
        protected TDataEditListNss _nssTDataEditList;
        public TDataEditListNss NssTDataEditList { get {
            if (_nssTDataEditList == null) { _nssTDataEditList = new TDataEditListNss(null); }
            return _nssTDataEditList;
        }}
        public TDataEditListNss SetupSelect_TDataEditList() {
            doSetupSelect(delegate { return Query().QueryTDataEditList(); });
            if (_nssTDataEditList == null || !_nssTDataEditList.HasConditionQuery)
            { _nssTDataEditList = new TDataEditListNss(Query().QueryTDataEditList()); }
            return _nssTDataEditList;
        }
        protected TOutputSettingNss _nssTOutputSetting;
        public TOutputSettingNss NssTOutputSetting { get {
            if (_nssTOutputSetting == null) { _nssTOutputSetting = new TOutputSettingNss(null); }
            return _nssTOutputSetting;
        }}
        public TOutputSettingNss SetupSelect_TOutputSetting() {
            doSetupSelect(delegate { return Query().QueryTOutputSetting(); });
            if (_nssTOutputSetting == null || !_nssTOutputSetting.HasConditionQuery)
            { _nssTOutputSetting = new TOutputSettingNss(Query().QueryTOutputSetting()); }
            return _nssTOutputSetting;
        }
        protected TOutputRequestNss _nssTOutputRequest;
        public TOutputRequestNss NssTOutputRequest { get {
            if (_nssTOutputRequest == null) { _nssTOutputRequest = new TOutputRequestNss(null); }
            return _nssTOutputRequest;
        }}
        public TOutputRequestNss SetupSelect_TOutputRequest() {
            doSetupSelect(delegate { return Query().QueryTOutputRequest(); });
            if (_nssTOutputRequest == null || !_nssTOutputRequest.HasConditionQuery)
            { _nssTOutputRequest = new TOutputRequestNss(Query().QueryTOutputRequest()); }
            return _nssTOutputRequest;
        }
        protected TAccessPermissionsInfoNss _nssTAccessPermissionsInfo;
        public TAccessPermissionsInfoNss NssTAccessPermissionsInfo { get {
            if (_nssTAccessPermissionsInfo == null) { _nssTAccessPermissionsInfo = new TAccessPermissionsInfoNss(null); }
            return _nssTAccessPermissionsInfo;
        }}
        public TAccessPermissionsInfoNss SetupSelect_TAccessPermissionsInfo() {
            doSetupSelect(delegate { return Query().QueryTAccessPermissionsInfo(); });
            if (_nssTAccessPermissionsInfo == null || !_nssTAccessPermissionsInfo.HasConditionQuery)
            { _nssTAccessPermissionsInfo = new TAccessPermissionsInfoNss(Query().QueryTAccessPermissionsInfo()); }
            return _nssTAccessPermissionsInfo;
        }
        protected TSessionControlerNss _nssTSessionControler;
        public TSessionControlerNss NssTSessionControler { get {
            if (_nssTSessionControler == null) { _nssTSessionControler = new TSessionControlerNss(null); }
            return _nssTSessionControler;
        }}
        public TSessionControlerNss SetupSelect_TSessionControler() {
            doSetupSelect(delegate { return Query().QueryTSessionControler(); });
            if (_nssTSessionControler == null || !_nssTSessionControler.HasConditionQuery)
            { _nssTSessionControler = new TSessionControlerNss(Query().QueryTSessionControler()); }
            return _nssTSessionControler;
        }
        protected TNoticeNss _nssTNotice;
        public TNoticeNss NssTNotice { get {
            if (_nssTNotice == null) { _nssTNotice = new TNoticeNss(null); }
            return _nssTNotice;
        }}
        public TNoticeNss SetupSelect_TNotice() {
            doSetupSelect(delegate { return Query().QueryTNotice(); });
            if (_nssTNotice == null || !_nssTNotice.HasConditionQuery)
            { _nssTNotice = new TNoticeNss(Query().QueryTNotice()); }
            return _nssTNotice;
        }
        protected TOutputSettingGtNss _nssTOutputSettingGt;
        public TOutputSettingGtNss NssTOutputSettingGt { get {
            if (_nssTOutputSettingGt == null) { _nssTOutputSettingGt = new TOutputSettingGtNss(null); }
            return _nssTOutputSettingGt;
        }}
        public TOutputSettingGtNss SetupSelect_TOutputSettingGt() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingGt(); });
            if (_nssTOutputSettingGt == null || !_nssTOutputSettingGt.HasConditionQuery)
            { _nssTOutputSettingGt = new TOutputSettingGtNss(Query().QueryTOutputSettingGt()); }
            return _nssTOutputSettingGt;
        }
        protected TOutputSettingCrossNss _nssTOutputSettingCross;
        public TOutputSettingCrossNss NssTOutputSettingCross { get {
            if (_nssTOutputSettingCross == null) { _nssTOutputSettingCross = new TOutputSettingCrossNss(null); }
            return _nssTOutputSettingCross;
        }}
        public TOutputSettingCrossNss SetupSelect_TOutputSettingCross() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingCross(); });
            if (_nssTOutputSettingCross == null || !_nssTOutputSettingCross.HasConditionQuery)
            { _nssTOutputSettingCross = new TOutputSettingCrossNss(Query().QueryTOutputSettingCross()); }
            return _nssTOutputSettingCross;
        }
        protected TOutputSettingFaNss _nssTOutputSettingFa;
        public TOutputSettingFaNss NssTOutputSettingFa { get {
            if (_nssTOutputSettingFa == null) { _nssTOutputSettingFa = new TOutputSettingFaNss(null); }
            return _nssTOutputSettingFa;
        }}
        public TOutputSettingFaNss SetupSelect_TOutputSettingFa() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingFa(); });
            if (_nssTOutputSettingFa == null || !_nssTOutputSettingFa.HasConditionQuery)
            { _nssTOutputSettingFa = new TOutputSettingFaNss(Query().QueryTOutputSettingFa()); }
            return _nssTOutputSettingFa;
        }
        protected TOutputSettingReportNss _nssTOutputSettingReport;
        public TOutputSettingReportNss NssTOutputSettingReport { get {
            if (_nssTOutputSettingReport == null) { _nssTOutputSettingReport = new TOutputSettingReportNss(null); }
            return _nssTOutputSettingReport;
        }}
        public TOutputSettingReportNss SetupSelect_TOutputSettingReport() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingReport(); });
            if (_nssTOutputSettingReport == null || !_nssTOutputSettingReport.HasConditionQuery)
            { _nssTOutputSettingReport = new TOutputSettingReportNss(Query().QueryTOutputSettingReport()); }
            return _nssTOutputSettingReport;
        }
        protected TQcwebSurveyDetailNss _nssTQcwebSurveyDetail;
        public TQcwebSurveyDetailNss NssTQcwebSurveyDetail { get {
            if (_nssTQcwebSurveyDetail == null) { _nssTQcwebSurveyDetail = new TQcwebSurveyDetailNss(null); }
            return _nssTQcwebSurveyDetail;
        }}
        public TQcwebSurveyDetailNss SetupSelect_TQcwebSurveyDetail() {
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyDetail(); });
            if (_nssTQcwebSurveyDetail == null || !_nssTQcwebSurveyDetail.HasConditionQuery)
            { _nssTQcwebSurveyDetail = new TQcwebSurveyDetailNss(Query().QueryTQcwebSurveyDetail()); }
            return _nssTQcwebSurveyDetail;
        }

        protected TAccessPermissionsInfoNss _nssTAccessPermissionsInfoAsOne;
        public TAccessPermissionsInfoNss NssTAccessPermissionsInfoAsOne { get {
            if (_nssTAccessPermissionsInfoAsOne == null) { _nssTAccessPermissionsInfoAsOne = new TAccessPermissionsInfoNss(null); }
            return _nssTAccessPermissionsInfoAsOne;
        }}
        public TAccessPermissionsInfoNss SetupSelect_TAccessPermissionsInfoAsOne() {
            doSetupSelect(delegate { return Query().QueryTAccessPermissionsInfoAsOne(); });
            if (_nssTAccessPermissionsInfoAsOne == null || !_nssTAccessPermissionsInfoAsOne.HasConditionQuery)
            { _nssTAccessPermissionsInfoAsOne = new TAccessPermissionsInfoNss(Query().QueryTAccessPermissionsInfoAsOne()); }
            return _nssTAccessPermissionsInfoAsOne;
        }

        protected TOutputSettingNss _nssTOutputSettingAsOne;
        public TOutputSettingNss NssTOutputSettingAsOne { get {
            if (_nssTOutputSettingAsOne == null) { _nssTOutputSettingAsOne = new TOutputSettingNss(null); }
            return _nssTOutputSettingAsOne;
        }}
        public TOutputSettingNss SetupSelect_TOutputSettingAsOne() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingAsOne(); });
            if (_nssTOutputSettingAsOne == null || !_nssTOutputSettingAsOne.HasConditionQuery)
            { _nssTOutputSettingAsOne = new TOutputSettingNss(Query().QueryTOutputSettingAsOne()); }
            return _nssTOutputSettingAsOne;
        }

        protected TOutputSettingCrossNss _nssTOutputSettingCrossAsOne;
        public TOutputSettingCrossNss NssTOutputSettingCrossAsOne { get {
            if (_nssTOutputSettingCrossAsOne == null) { _nssTOutputSettingCrossAsOne = new TOutputSettingCrossNss(null); }
            return _nssTOutputSettingCrossAsOne;
        }}
        public TOutputSettingCrossNss SetupSelect_TOutputSettingCrossAsOne() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingCrossAsOne(); });
            if (_nssTOutputSettingCrossAsOne == null || !_nssTOutputSettingCrossAsOne.HasConditionQuery)
            { _nssTOutputSettingCrossAsOne = new TOutputSettingCrossNss(Query().QueryTOutputSettingCrossAsOne()); }
            return _nssTOutputSettingCrossAsOne;
        }

        protected TOutputSettingFaNss _nssTOutputSettingFaAsOne;
        public TOutputSettingFaNss NssTOutputSettingFaAsOne { get {
            if (_nssTOutputSettingFaAsOne == null) { _nssTOutputSettingFaAsOne = new TOutputSettingFaNss(null); }
            return _nssTOutputSettingFaAsOne;
        }}
        public TOutputSettingFaNss SetupSelect_TOutputSettingFaAsOne() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingFaAsOne(); });
            if (_nssTOutputSettingFaAsOne == null || !_nssTOutputSettingFaAsOne.HasConditionQuery)
            { _nssTOutputSettingFaAsOne = new TOutputSettingFaNss(Query().QueryTOutputSettingFaAsOne()); }
            return _nssTOutputSettingFaAsOne;
        }

        protected TOutputSettingGtNss _nssTOutputSettingGtAsOne;
        public TOutputSettingGtNss NssTOutputSettingGtAsOne { get {
            if (_nssTOutputSettingGtAsOne == null) { _nssTOutputSettingGtAsOne = new TOutputSettingGtNss(null); }
            return _nssTOutputSettingGtAsOne;
        }}
        public TOutputSettingGtNss SetupSelect_TOutputSettingGtAsOne() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingGtAsOne(); });
            if (_nssTOutputSettingGtAsOne == null || !_nssTOutputSettingGtAsOne.HasConditionQuery)
            { _nssTOutputSettingGtAsOne = new TOutputSettingGtNss(Query().QueryTOutputSettingGtAsOne()); }
            return _nssTOutputSettingGtAsOne;
        }

        protected TOutputSettingReportNss _nssTOutputSettingReportAsOne;
        public TOutputSettingReportNss NssTOutputSettingReportAsOne { get {
            if (_nssTOutputSettingReportAsOne == null) { _nssTOutputSettingReportAsOne = new TOutputSettingReportNss(null); }
            return _nssTOutputSettingReportAsOne;
        }}
        public TOutputSettingReportNss SetupSelect_TOutputSettingReportAsOne() {
            doSetupSelect(delegate { return Query().QueryTOutputSettingReportAsOne(); });
            if (_nssTOutputSettingReportAsOne == null || !_nssTOutputSettingReportAsOne.HasConditionQuery)
            { _nssTOutputSettingReportAsOne = new TOutputSettingReportNss(Query().QueryTOutputSettingReportAsOne()); }
            return _nssTOutputSettingReportAsOne;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TQcwebSurveyInfoCBSpecification _specification;
        public TQcwebSurveyInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TQcwebSurveyInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
			protected BsTQcwebSurveyInfoCB _myCB;
			public MySpQyCall(BsTQcwebSurveyInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TQcwebSurveyInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TQcwebSurveyInfoCB> ColumnQuery(SpecifyQuery<TQcwebSurveyInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TQcwebSurveyInfoCB>(delegate(SpecifyQuery<TQcwebSurveyInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TQcwebSurveyInfoCB xcreateColumnQueryCB() {
            TQcwebSurveyInfoCB cb = new TQcwebSurveyInfoCB();
            cb.xsetupForColumnQuery((TQcwebSurveyInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TQcwebSurveyInfoCB> orQuery) {
            xorQ((TQcwebSurveyInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TQcwebSurveyInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TQcwebSurveyInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TQcwebSurveyInfoCBColQySpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
        protected TQcwebSurveyInfoCB _mainCB;
        public TQcwebSurveyInfoCBColQySpQyCall(TQcwebSurveyInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TQcwebSurveyInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TQcwebSurveyInfoCBSpecification : AbstractSpecification<TQcwebSurveyInfoCQ> {
        protected TSurveyInfoCBSpecification _tSurveyInfo;
        protected TRawdataImportQueInfoCBSpecification _tRawdataImportQueInfo;
        protected TAllocationCellInfoCBSpecification _tAllocationCellInfo;
        protected TSelectConditionInfoCBSpecification _tSelectConditionInfo;
        protected TItemInfoCBSpecification _tItemInfo;
        protected TTableControlCBSpecification _tTableControl;
        protected TDefaultEnvCBSpecification _tDefaultEnv;
        protected TDefaultEnvColorInfoCBSpecification _tDefaultEnvColorInfo;
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TReportsetCBSpecification _tReportset;
        protected TDataEditListCBSpecification _tDataEditList;
        protected TOutputSettingCBSpecification _tOutputSetting;
        protected TOutputRequestCBSpecification _tOutputRequest;
        protected TAccessPermissionsInfoCBSpecification _tAccessPermissionsInfo;
        protected TSessionControlerCBSpecification _tSessionControler;
        protected TNoticeCBSpecification _tNotice;
        protected TOutputSettingGtCBSpecification _tOutputSettingGt;
        protected TOutputSettingCrossCBSpecification _tOutputSettingCross;
        protected TOutputSettingFaCBSpecification _tOutputSettingFa;
        protected TOutputSettingReportCBSpecification _tOutputSettingReport;
        protected TQcwebSurveyDetailCBSpecification _tQcwebSurveyDetail;
        protected TAccessPermissionsInfoCBSpecification _tAccessPermissionsInfoAsOne;
        protected TOutputSettingCBSpecification _tOutputSettingAsOne;
        protected TOutputSettingCrossCBSpecification _tOutputSettingCrossAsOne;
        protected TOutputSettingFaCBSpecification _tOutputSettingFaAsOne;
        protected TOutputSettingGtCBSpecification _tOutputSettingGtAsOne;
        protected TOutputSettingReportCBSpecification _tOutputSettingReportAsOne;
        public TQcwebSurveyInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TQcwebSurveyInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnAddDataNo() { doColumn("ADD_DATA_NO"); }
        public void ColumnSurveyNameOrg() { doColumn("SURVEY_NAME_ORG"); }
        public void ColumnImportDatetime() { doColumn("IMPORT_DATETIME"); }
        public void ColumnImportFileName() { doColumn("IMPORT_FILE_NAME"); }
        public void ColumnDeleteFlag() { doColumn("DELETE_FLAG"); }
        public void ColumnViewSurveyName() { doColumn("VIEW_SURVEY_NAME"); }
        public void ColumnGtCount() { doColumn("GT_COUNT"); }
        public void ColumnCrossCount() { doColumn("CROSS_COUNT"); }
        public void ColumnFaCount() { doColumn("FA_COUNT"); }
        public void ColumnVersionNo() { doColumn("VERSION_NO"); }
        public void ColumnLastUpdateUser() { doColumn("LAST_UPDATE_USER"); }
        public void ColumnLastUpdateDatetime() { doColumn("LAST_UPDATE_DATETIME"); }
        public void ColumnSurveyInfoId() { doColumn("SURVEY_INFO_ID"); }
        public void ColumnRawdataImportQueInfoId() { doColumn("RAWDATA_IMPORT_QUE_INFO_ID"); }
        public void ColumnUtf8Flag() { doColumn("UTF8_FLAG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnQcwebid(); // PK
            if (qyCall().qy().hasConditionQueryTSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TSurveyInfoCQ) {
                ColumnSurveyInfoId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTRawdataImportQueInfo()
                    || qyCall().qy().xgetReferrerQuery() is TRawdataImportQueInfoCQ) {
                ColumnRawdataImportQueInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_QCWEB_SURVEY_INFO"; }
        public TSurveyInfoCBSpecification SpecifyTSurveyInfo() {
            assertForeign("tSurveyInfo");
            if (_tSurveyInfo == null) {
                _tSurveyInfo = new TSurveyInfoCBSpecification(_baseCB, new TSurveyInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tSurveyInfo.xsetSyncQyCall(new TSurveyInfoSpQyCall(xsyncQyCall())); }
            }
            return _tSurveyInfo;
        }
		public class TSurveyInfoSpQyCall : HpSpQyCall<TSurveyInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TSurveyInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTSurveyInfo(); }
			public TSurveyInfoCQ qy() { return _qyCall.qy().QueryTSurveyInfo(); }
		}
        public TRawdataImportQueInfoCBSpecification SpecifyTRawdataImportQueInfo() {
            assertForeign("tRawdataImportQueInfo");
            if (_tRawdataImportQueInfo == null) {
                _tRawdataImportQueInfo = new TRawdataImportQueInfoCBSpecification(_baseCB, new TRawdataImportQueInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tRawdataImportQueInfo.xsetSyncQyCall(new TRawdataImportQueInfoSpQyCall(xsyncQyCall())); }
            }
            return _tRawdataImportQueInfo;
        }
		public class TRawdataImportQueInfoSpQyCall : HpSpQyCall<TRawdataImportQueInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TRawdataImportQueInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTRawdataImportQueInfo(); }
			public TRawdataImportQueInfoCQ qy() { return _qyCall.qy().QueryTRawdataImportQueInfo(); }
		}
        public TAllocationCellInfoCBSpecification SpecifyTAllocationCellInfo() {
            assertForeign("tAllocationCellInfo");
            if (_tAllocationCellInfo == null) {
                _tAllocationCellInfo = new TAllocationCellInfoCBSpecification(_baseCB, new TAllocationCellInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tAllocationCellInfo.xsetSyncQyCall(new TAllocationCellInfoSpQyCall(xsyncQyCall())); }
            }
            return _tAllocationCellInfo;
        }
		public class TAllocationCellInfoSpQyCall : HpSpQyCall<TAllocationCellInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TAllocationCellInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTAllocationCellInfo(); }
			public TAllocationCellInfoCQ qy() { return _qyCall.qy().QueryTAllocationCellInfo(); }
		}
        public TSelectConditionInfoCBSpecification SpecifyTSelectConditionInfo() {
            assertForeign("tSelectConditionInfo");
            if (_tSelectConditionInfo == null) {
                _tSelectConditionInfo = new TSelectConditionInfoCBSpecification(_baseCB, new TSelectConditionInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tSelectConditionInfo.xsetSyncQyCall(new TSelectConditionInfoSpQyCall(xsyncQyCall())); }
            }
            return _tSelectConditionInfo;
        }
		public class TSelectConditionInfoSpQyCall : HpSpQyCall<TSelectConditionInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TSelectConditionInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTSelectConditionInfo(); }
			public TSelectConditionInfoCQ qy() { return _qyCall.qy().QueryTSelectConditionInfo(); }
		}
        public TItemInfoCBSpecification SpecifyTItemInfo() {
            assertForeign("tItemInfo");
            if (_tItemInfo == null) {
                _tItemInfo = new TItemInfoCBSpecification(_baseCB, new TItemInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tItemInfo.xsetSyncQyCall(new TItemInfoSpQyCall(xsyncQyCall())); }
            }
            return _tItemInfo;
        }
		public class TItemInfoSpQyCall : HpSpQyCall<TItemInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TItemInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfo(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfo(); }
		}
        public TTableControlCBSpecification SpecifyTTableControl() {
            assertForeign("tTableControl");
            if (_tTableControl == null) {
                _tTableControl = new TTableControlCBSpecification(_baseCB, new TTableControlSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tTableControl.xsetSyncQyCall(new TTableControlSpQyCall(xsyncQyCall())); }
            }
            return _tTableControl;
        }
		public class TTableControlSpQyCall : HpSpQyCall<TTableControlCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TTableControlSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTTableControl(); }
			public TTableControlCQ qy() { return _qyCall.qy().QueryTTableControl(); }
		}
        public TDefaultEnvCBSpecification SpecifyTDefaultEnv() {
            assertForeign("tDefaultEnv");
            if (_tDefaultEnv == null) {
                _tDefaultEnv = new TDefaultEnvCBSpecification(_baseCB, new TDefaultEnvSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnv.xsetSyncQyCall(new TDefaultEnvSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnv;
        }
		public class TDefaultEnvSpQyCall : HpSpQyCall<TDefaultEnvCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TDefaultEnvSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnv(); }
			public TDefaultEnvCQ qy() { return _qyCall.qy().QueryTDefaultEnv(); }
		}
        public TDefaultEnvColorInfoCBSpecification SpecifyTDefaultEnvColorInfo() {
            assertForeign("tDefaultEnvColorInfo");
            if (_tDefaultEnvColorInfo == null) {
                _tDefaultEnvColorInfo = new TDefaultEnvColorInfoCBSpecification(_baseCB, new TDefaultEnvColorInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnvColorInfo.xsetSyncQyCall(new TDefaultEnvColorInfoSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnvColorInfo;
        }
		public class TDefaultEnvColorInfoSpQyCall : HpSpQyCall<TDefaultEnvColorInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TDefaultEnvColorInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnvColorInfo(); }
			public TDefaultEnvColorInfoCQ qy() { return _qyCall.qy().QueryTDefaultEnvColorInfo(); }
		}
        public TScenarioTotalizationCBSpecification SpecifyTScenarioTotalization() {
            assertForeign("tScenarioTotalization");
            if (_tScenarioTotalization == null) {
                _tScenarioTotalization = new TScenarioTotalizationCBSpecification(_baseCB, new TScenarioTotalizationSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tScenarioTotalization.xsetSyncQyCall(new TScenarioTotalizationSpQyCall(xsyncQyCall())); }
            }
            return _tScenarioTotalization;
        }
		public class TScenarioTotalizationSpQyCall : HpSpQyCall<TScenarioTotalizationCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
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
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TReportsetSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTReportset(); }
			public TReportsetCQ qy() { return _qyCall.qy().QueryTReportset(); }
		}
        public TDataEditListCBSpecification SpecifyTDataEditList() {
            assertForeign("tDataEditList");
            if (_tDataEditList == null) {
                _tDataEditList = new TDataEditListCBSpecification(_baseCB, new TDataEditListSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDataEditList.xsetSyncQyCall(new TDataEditListSpQyCall(xsyncQyCall())); }
            }
            return _tDataEditList;
        }
		public class TDataEditListSpQyCall : HpSpQyCall<TDataEditListCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TDataEditListSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataEditList(); }
			public TDataEditListCQ qy() { return _qyCall.qy().QueryTDataEditList(); }
		}
        public TOutputSettingCBSpecification SpecifyTOutputSetting() {
            assertForeign("tOutputSetting");
            if (_tOutputSetting == null) {
                _tOutputSetting = new TOutputSettingCBSpecification(_baseCB, new TOutputSettingSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSetting.xsetSyncQyCall(new TOutputSettingSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSetting;
        }
		public class TOutputSettingSpQyCall : HpSpQyCall<TOutputSettingCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSetting(); }
			public TOutputSettingCQ qy() { return _qyCall.qy().QueryTOutputSetting(); }
		}
        public TOutputRequestCBSpecification SpecifyTOutputRequest() {
            assertForeign("tOutputRequest");
            if (_tOutputRequest == null) {
                _tOutputRequest = new TOutputRequestCBSpecification(_baseCB, new TOutputRequestSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputRequest.xsetSyncQyCall(new TOutputRequestSpQyCall(xsyncQyCall())); }
            }
            return _tOutputRequest;
        }
		public class TOutputRequestSpQyCall : HpSpQyCall<TOutputRequestCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputRequestSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputRequest(); }
			public TOutputRequestCQ qy() { return _qyCall.qy().QueryTOutputRequest(); }
		}
        public TAccessPermissionsInfoCBSpecification SpecifyTAccessPermissionsInfo() {
            assertForeign("tAccessPermissionsInfo");
            if (_tAccessPermissionsInfo == null) {
                _tAccessPermissionsInfo = new TAccessPermissionsInfoCBSpecification(_baseCB, new TAccessPermissionsInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tAccessPermissionsInfo.xsetSyncQyCall(new TAccessPermissionsInfoSpQyCall(xsyncQyCall())); }
            }
            return _tAccessPermissionsInfo;
        }
		public class TAccessPermissionsInfoSpQyCall : HpSpQyCall<TAccessPermissionsInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TAccessPermissionsInfoSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTAccessPermissionsInfo(); }
			public TAccessPermissionsInfoCQ qy() { return _qyCall.qy().QueryTAccessPermissionsInfo(); }
		}
        public TSessionControlerCBSpecification SpecifyTSessionControler() {
            assertForeign("tSessionControler");
            if (_tSessionControler == null) {
                _tSessionControler = new TSessionControlerCBSpecification(_baseCB, new TSessionControlerSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tSessionControler.xsetSyncQyCall(new TSessionControlerSpQyCall(xsyncQyCall())); }
            }
            return _tSessionControler;
        }
		public class TSessionControlerSpQyCall : HpSpQyCall<TSessionControlerCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TSessionControlerSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTSessionControler(); }
			public TSessionControlerCQ qy() { return _qyCall.qy().QueryTSessionControler(); }
		}
        public TNoticeCBSpecification SpecifyTNotice() {
            assertForeign("tNotice");
            if (_tNotice == null) {
                _tNotice = new TNoticeCBSpecification(_baseCB, new TNoticeSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tNotice.xsetSyncQyCall(new TNoticeSpQyCall(xsyncQyCall())); }
            }
            return _tNotice;
        }
		public class TNoticeSpQyCall : HpSpQyCall<TNoticeCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TNoticeSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTNotice(); }
			public TNoticeCQ qy() { return _qyCall.qy().QueryTNotice(); }
		}
        public TOutputSettingGtCBSpecification SpecifyTOutputSettingGt() {
            assertForeign("tOutputSettingGt");
            if (_tOutputSettingGt == null) {
                _tOutputSettingGt = new TOutputSettingGtCBSpecification(_baseCB, new TOutputSettingGtSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingGt.xsetSyncQyCall(new TOutputSettingGtSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingGt;
        }
		public class TOutputSettingGtSpQyCall : HpSpQyCall<TOutputSettingGtCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingGtSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingGt(); }
			public TOutputSettingGtCQ qy() { return _qyCall.qy().QueryTOutputSettingGt(); }
		}
        public TOutputSettingCrossCBSpecification SpecifyTOutputSettingCross() {
            assertForeign("tOutputSettingCross");
            if (_tOutputSettingCross == null) {
                _tOutputSettingCross = new TOutputSettingCrossCBSpecification(_baseCB, new TOutputSettingCrossSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingCross.xsetSyncQyCall(new TOutputSettingCrossSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingCross;
        }
		public class TOutputSettingCrossSpQyCall : HpSpQyCall<TOutputSettingCrossCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingCrossSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingCross(); }
			public TOutputSettingCrossCQ qy() { return _qyCall.qy().QueryTOutputSettingCross(); }
		}
        public TOutputSettingFaCBSpecification SpecifyTOutputSettingFa() {
            assertForeign("tOutputSettingFa");
            if (_tOutputSettingFa == null) {
                _tOutputSettingFa = new TOutputSettingFaCBSpecification(_baseCB, new TOutputSettingFaSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingFa.xsetSyncQyCall(new TOutputSettingFaSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingFa;
        }
		public class TOutputSettingFaSpQyCall : HpSpQyCall<TOutputSettingFaCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingFaSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingFa(); }
			public TOutputSettingFaCQ qy() { return _qyCall.qy().QueryTOutputSettingFa(); }
		}
        public TOutputSettingReportCBSpecification SpecifyTOutputSettingReport() {
            assertForeign("tOutputSettingReport");
            if (_tOutputSettingReport == null) {
                _tOutputSettingReport = new TOutputSettingReportCBSpecification(_baseCB, new TOutputSettingReportSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingReport.xsetSyncQyCall(new TOutputSettingReportSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingReport;
        }
		public class TOutputSettingReportSpQyCall : HpSpQyCall<TOutputSettingReportCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingReportSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingReport(); }
			public TOutputSettingReportCQ qy() { return _qyCall.qy().QueryTOutputSettingReport(); }
		}
        public TQcwebSurveyDetailCBSpecification SpecifyTQcwebSurveyDetail() {
            assertForeign("tQcwebSurveyDetail");
            if (_tQcwebSurveyDetail == null) {
                _tQcwebSurveyDetail = new TQcwebSurveyDetailCBSpecification(_baseCB, new TQcwebSurveyDetailSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tQcwebSurveyDetail.xsetSyncQyCall(new TQcwebSurveyDetailSpQyCall(xsyncQyCall())); }
            }
            return _tQcwebSurveyDetail;
        }
		public class TQcwebSurveyDetailSpQyCall : HpSpQyCall<TQcwebSurveyDetailCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TQcwebSurveyDetailSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyDetail(); }
			public TQcwebSurveyDetailCQ qy() { return _qyCall.qy().QueryTQcwebSurveyDetail(); }
		}
        public TAccessPermissionsInfoCBSpecification SpecifyTAccessPermissionsInfoAsOne() {
            assertForeign("tAccessPermissionsInfoAsOne");
            if (_tAccessPermissionsInfoAsOne == null) {
                _tAccessPermissionsInfoAsOne = new TAccessPermissionsInfoCBSpecification(_baseCB, new TAccessPermissionsInfoAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tAccessPermissionsInfoAsOne.xsetSyncQyCall(new TAccessPermissionsInfoAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tAccessPermissionsInfoAsOne;
        }
		public class TAccessPermissionsInfoAsOneSpQyCall : HpSpQyCall<TAccessPermissionsInfoCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TAccessPermissionsInfoAsOneSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTAccessPermissionsInfoAsOne(); }
			public TAccessPermissionsInfoCQ qy() { return _qyCall.qy().QueryTAccessPermissionsInfoAsOne(); }
		}
        public TOutputSettingCBSpecification SpecifyTOutputSettingAsOne() {
            assertForeign("tOutputSettingAsOne");
            if (_tOutputSettingAsOne == null) {
                _tOutputSettingAsOne = new TOutputSettingCBSpecification(_baseCB, new TOutputSettingAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingAsOne.xsetSyncQyCall(new TOutputSettingAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingAsOne;
        }
		public class TOutputSettingAsOneSpQyCall : HpSpQyCall<TOutputSettingCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingAsOneSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingAsOne(); }
			public TOutputSettingCQ qy() { return _qyCall.qy().QueryTOutputSettingAsOne(); }
		}
        public TOutputSettingCrossCBSpecification SpecifyTOutputSettingCrossAsOne() {
            assertForeign("tOutputSettingCrossAsOne");
            if (_tOutputSettingCrossAsOne == null) {
                _tOutputSettingCrossAsOne = new TOutputSettingCrossCBSpecification(_baseCB, new TOutputSettingCrossAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingCrossAsOne.xsetSyncQyCall(new TOutputSettingCrossAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingCrossAsOne;
        }
		public class TOutputSettingCrossAsOneSpQyCall : HpSpQyCall<TOutputSettingCrossCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingCrossAsOneSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingCrossAsOne(); }
			public TOutputSettingCrossCQ qy() { return _qyCall.qy().QueryTOutputSettingCrossAsOne(); }
		}
        public TOutputSettingFaCBSpecification SpecifyTOutputSettingFaAsOne() {
            assertForeign("tOutputSettingFaAsOne");
            if (_tOutputSettingFaAsOne == null) {
                _tOutputSettingFaAsOne = new TOutputSettingFaCBSpecification(_baseCB, new TOutputSettingFaAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingFaAsOne.xsetSyncQyCall(new TOutputSettingFaAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingFaAsOne;
        }
		public class TOutputSettingFaAsOneSpQyCall : HpSpQyCall<TOutputSettingFaCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingFaAsOneSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingFaAsOne(); }
			public TOutputSettingFaCQ qy() { return _qyCall.qy().QueryTOutputSettingFaAsOne(); }
		}
        public TOutputSettingGtCBSpecification SpecifyTOutputSettingGtAsOne() {
            assertForeign("tOutputSettingGtAsOne");
            if (_tOutputSettingGtAsOne == null) {
                _tOutputSettingGtAsOne = new TOutputSettingGtCBSpecification(_baseCB, new TOutputSettingGtAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingGtAsOne.xsetSyncQyCall(new TOutputSettingGtAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingGtAsOne;
        }
		public class TOutputSettingGtAsOneSpQyCall : HpSpQyCall<TOutputSettingGtCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingGtAsOneSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingGtAsOne(); }
			public TOutputSettingGtCQ qy() { return _qyCall.qy().QueryTOutputSettingGtAsOne(); }
		}
        public TOutputSettingReportCBSpecification SpecifyTOutputSettingReportAsOne() {
            assertForeign("tOutputSettingReportAsOne");
            if (_tOutputSettingReportAsOne == null) {
                _tOutputSettingReportAsOne = new TOutputSettingReportCBSpecification(_baseCB, new TOutputSettingReportAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSettingReportAsOne.xsetSyncQyCall(new TOutputSettingReportAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSettingReportAsOne;
        }
		public class TOutputSettingReportAsOneSpQyCall : HpSpQyCall<TOutputSettingReportCQ> {
		    protected HpSpQyCall<TQcwebSurveyInfoCQ> _qyCall;
		    public TOutputSettingReportAsOneSpQyCall(HpSpQyCall<TQcwebSurveyInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSettingReportAsOne(); }
			public TOutputSettingReportCQ qy() { return _qyCall.qy().QueryTOutputSettingReportAsOne(); }
		}
        public RAFunction<TAllocationCellInfoCB, TQcwebSurveyInfoCQ> DerivedTAllocationCellInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TAllocationCellInfoCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TAllocationCellInfoCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTAllocationCellInfoList(function, subQuery, aliasName); });
        }
        public RAFunction<TDataEditListCB, TQcwebSurveyInfoCQ> DerivedTDataEditListList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDataEditListCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDataEditListCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTDataEditListList(function, subQuery, aliasName); });
        }
        public RAFunction<TItemInfoCB, TQcwebSurveyInfoCQ> DerivedTItemInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TItemInfoCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TItemInfoCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTItemInfoList(function, subQuery, aliasName); });
        }
        public RAFunction<TNoticeCB, TQcwebSurveyInfoCQ> DerivedTNoticeList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TNoticeCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TNoticeCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTNoticeList(function, subQuery, aliasName); });
        }
        public RAFunction<TOutputRequestCB, TQcwebSurveyInfoCQ> DerivedTOutputRequestList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputRequestCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputRequestCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTOutputRequestList(function, subQuery, aliasName); });
        }
        public RAFunction<TOutputTemplateCB, TQcwebSurveyInfoCQ> DerivedTOutputTemplateList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputTemplateCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputTemplateCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTOutputTemplateList(function, subQuery, aliasName); });
        }
        public RAFunction<TQcwebSurveyDetailCB, TQcwebSurveyInfoCQ> DerivedTQcwebSurveyDetailList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TQcwebSurveyDetailCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TQcwebSurveyDetailCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTQcwebSurveyDetailList(function, subQuery, aliasName); });
        }
        public RAFunction<TRawdataImportQueInfoCB, TQcwebSurveyInfoCQ> DerivedTRawdataImportQueInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TRawdataImportQueInfoCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TRawdataImportQueInfoCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTRawdataImportQueInfoList(function, subQuery, aliasName); });
        }
        public RAFunction<TReportsetCB, TQcwebSurveyInfoCQ> DerivedTReportsetList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TReportsetCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TReportsetCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTReportsetList(function, subQuery, aliasName); });
        }
        public RAFunction<TScenarioTotalizationCB, TQcwebSurveyInfoCQ> DerivedTScenarioTotalizationList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TScenarioTotalizationCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TScenarioTotalizationCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTScenarioTotalizationList(function, subQuery, aliasName); });
        }
        public RAFunction<TSelectConditionInfoCB, TQcwebSurveyInfoCQ> DerivedTSelectConditionInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TSelectConditionInfoCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TSelectConditionInfoCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTSelectConditionInfoList(function, subQuery, aliasName); });
        }
        public RAFunction<TSessionControlerCB, TQcwebSurveyInfoCQ> DerivedTSessionControlerList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TSessionControlerCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TSessionControlerCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTSessionControlerList(function, subQuery, aliasName); });
        }
        public RAFunction<TWeightbackCB, TQcwebSurveyInfoCQ> DerivedTWeightbackList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TWeightbackCB, TQcwebSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TWeightbackCB> subQuery, TQcwebSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTWeightbackList(function, subQuery, aliasName); });
        }
    }
}
