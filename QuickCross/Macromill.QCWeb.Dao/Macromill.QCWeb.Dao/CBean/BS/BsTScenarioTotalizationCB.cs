
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
    public class BsTScenarioTotalizationCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TScenarioTotalizationCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SCENARIO_TOTALIZATION"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? scenarioTotalizationId) {
            assertObjectNotNull("scenarioTotalizationId", scenarioTotalizationId);
            BsTScenarioTotalizationCB cb = this;
            cb.Query().SetScenarioTotalizationId_Equal(scenarioTotalizationId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ScenarioTotalizationId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ScenarioTotalizationId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TScenarioTotalizationCQ Query() {
            return this.ConditionQuery;
        }

        public TScenarioTotalizationCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TScenarioTotalizationCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TScenarioTotalizationCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TScenarioTotalizationCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TScenarioTotalizationCB> unionQuery) {
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TScenarioTotalizationCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TScenarioTotalizationCB> unionQuery) {
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TScenarioTotalizationCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TGtScenarioItemNss _nssTGtScenarioItem;
        public TGtScenarioItemNss NssTGtScenarioItem { get {
            if (_nssTGtScenarioItem == null) { _nssTGtScenarioItem = new TGtScenarioItemNss(null); }
            return _nssTGtScenarioItem;
        }}
        public TGtScenarioItemNss SetupSelect_TGtScenarioItem() {
            doSetupSelect(delegate { return Query().QueryTGtScenarioItem(); });
            if (_nssTGtScenarioItem == null || !_nssTGtScenarioItem.HasConditionQuery)
            { _nssTGtScenarioItem = new TGtScenarioItemNss(Query().QueryTGtScenarioItem()); }
            return _nssTGtScenarioItem;
        }
        protected TCrossScenarioTargetNss _nssTCrossScenarioTarget;
        public TCrossScenarioTargetNss NssTCrossScenarioTarget { get {
            if (_nssTCrossScenarioTarget == null) { _nssTCrossScenarioTarget = new TCrossScenarioTargetNss(null); }
            return _nssTCrossScenarioTarget;
        }}
        public TCrossScenarioTargetNss SetupSelect_TCrossScenarioTarget() {
            doSetupSelect(delegate { return Query().QueryTCrossScenarioTarget(); });
            if (_nssTCrossScenarioTarget == null || !_nssTCrossScenarioTarget.HasConditionQuery)
            { _nssTCrossScenarioTarget = new TCrossScenarioTargetNss(Query().QueryTCrossScenarioTarget()); }
            return _nssTCrossScenarioTarget;
        }
        protected TFaScenarioHeaderNss _nssTFaScenarioHeader;
        public TFaScenarioHeaderNss NssTFaScenarioHeader { get {
            if (_nssTFaScenarioHeader == null) { _nssTFaScenarioHeader = new TFaScenarioHeaderNss(null); }
            return _nssTFaScenarioHeader;
        }}
        public TFaScenarioHeaderNss SetupSelect_TFaScenarioHeader() {
            doSetupSelect(delegate { return Query().QueryTFaScenarioHeader(); });
            if (_nssTFaScenarioHeader == null || !_nssTFaScenarioHeader.HasConditionQuery)
            { _nssTFaScenarioHeader = new TFaScenarioHeaderNss(Query().QueryTFaScenarioHeader()); }
            return _nssTFaScenarioHeader;
        }
        protected TScenarioQuerylistNss _nssTScenarioQuerylist;
        public TScenarioQuerylistNss NssTScenarioQuerylist { get {
            if (_nssTScenarioQuerylist == null) { _nssTScenarioQuerylist = new TScenarioQuerylistNss(null); }
            return _nssTScenarioQuerylist;
        }}
        public TScenarioQuerylistNss SetupSelect_TScenarioQuerylist() {
            doSetupSelect(delegate { return Query().QueryTScenarioQuerylist(); });
            if (_nssTScenarioQuerylist == null || !_nssTScenarioQuerylist.HasConditionQuery)
            { _nssTScenarioQuerylist = new TScenarioQuerylistNss(Query().QueryTScenarioQuerylist()); }
            return _nssTScenarioQuerylist;
        }
        protected TCategoryOutputEditNss _nssTCategoryOutputEdit;
        public TCategoryOutputEditNss NssTCategoryOutputEdit { get {
            if (_nssTCategoryOutputEdit == null) { _nssTCategoryOutputEdit = new TCategoryOutputEditNss(null); }
            return _nssTCategoryOutputEdit;
        }}
        public TCategoryOutputEditNss SetupSelect_TCategoryOutputEdit() {
            doSetupSelect(delegate { return Query().QueryTCategoryOutputEdit(); });
            if (_nssTCategoryOutputEdit == null || !_nssTCategoryOutputEdit.HasConditionQuery)
            { _nssTCategoryOutputEdit = new TCategoryOutputEditNss(Query().QueryTCategoryOutputEdit()); }
            return _nssTCategoryOutputEdit;
        }
        protected TGtMatrixInfoNss _nssTGtMatrixInfo;
        public TGtMatrixInfoNss NssTGtMatrixInfo { get {
            if (_nssTGtMatrixInfo == null) { _nssTGtMatrixInfo = new TGtMatrixInfoNss(null); }
            return _nssTGtMatrixInfo;
        }}
        public TGtMatrixInfoNss SetupSelect_TGtMatrixInfo() {
            doSetupSelect(delegate { return Query().QueryTGtMatrixInfo(); });
            if (_nssTGtMatrixInfo == null || !_nssTGtMatrixInfo.HasConditionQuery)
            { _nssTGtMatrixInfo = new TGtMatrixInfoNss(Query().QueryTGtMatrixInfo()); }
            return _nssTGtMatrixInfo;
        }
        protected TDefaultEnvNss _nssTDefaultEnv;
        public TDefaultEnvNss NssTDefaultEnv { get {
            if (_nssTDefaultEnv == null) { _nssTDefaultEnv = new TDefaultEnvNss(null); }
            return _nssTDefaultEnv;
        }}
        public TDefaultEnvNss SetupSelect_TDefaultEnv() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTDefaultEnv(); });
            if (_nssTDefaultEnv == null || !_nssTDefaultEnv.HasConditionQuery)
            { _nssTDefaultEnv = new TDefaultEnvNss(Query().QueryTDefaultEnv()); }
            return _nssTDefaultEnv;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TScenarioTotalizationCBSpecification _specification;
        public TScenarioTotalizationCBSpecification Specify() {
            if (_specification == null) { _specification = new TScenarioTotalizationCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TScenarioTotalizationCQ> {
			protected BsTScenarioTotalizationCB _myCB;
			public MySpQyCall(BsTScenarioTotalizationCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TScenarioTotalizationCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TScenarioTotalizationCB> ColumnQuery(SpecifyQuery<TScenarioTotalizationCB> leftSpecifyQuery) {
            return new HpColQyOperand<TScenarioTotalizationCB>(delegate(SpecifyQuery<TScenarioTotalizationCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TScenarioTotalizationCB xcreateColumnQueryCB() {
            TScenarioTotalizationCB cb = new TScenarioTotalizationCB();
            cb.xsetupForColumnQuery((TScenarioTotalizationCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TScenarioTotalizationCB> orQuery) {
            xorQ((TScenarioTotalizationCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TScenarioTotalizationCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TScenarioTotalizationCBColQySpQyCall(mainCB));
        }
    }

    public class TScenarioTotalizationCBColQySpQyCall : HpSpQyCall<TScenarioTotalizationCQ> {
        protected TScenarioTotalizationCB _mainCB;
        public TScenarioTotalizationCBColQySpQyCall(TScenarioTotalizationCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TScenarioTotalizationCQ qy() { return _mainCB.Query(); } 
    }

    public class TScenarioTotalizationCBSpecification : AbstractSpecification<TScenarioTotalizationCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TGtScenarioItemCBSpecification _tGtScenarioItem;
        protected TCrossScenarioTargetCBSpecification _tCrossScenarioTarget;
        protected TFaScenarioHeaderCBSpecification _tFaScenarioHeader;
        protected TScenarioQuerylistCBSpecification _tScenarioQuerylist;
        protected TCategoryOutputEditCBSpecification _tCategoryOutputEdit;
        protected TGtMatrixInfoCBSpecification _tGtMatrixInfo;
        protected TDefaultEnvCBSpecification _tDefaultEnv;
        public TScenarioTotalizationCBSpecification(ConditionBean baseCB, HpSpQyCall<TScenarioTotalizationCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnScenarioType() { doColumn("SCENARIO_TYPE"); }
        public void ColumnScenarioName() { doColumn("SCENARIO_NAME"); }
        public void ColumnConditionDiv() { doColumn("CONDITION_DIV"); }
        public void ColumnFilterFlag() { doColumn("FILTER_FLAG"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnWeightbackFlag() { doColumn("WEIGHTBACK_FLAG"); }
        public void ColumnWeightbackCode() { doColumn("WEIGHTBACK_CODE"); }
        public void ColumnTotalnumFlag() { doColumn("TOTALNUM_FLAG"); }
        public void ColumnGraphOutputFlag() { doColumn("GRAPH_OUTPUT_FLAG"); }
        public void ColumnPieChartChoiceFlag() { doColumn("PIE_CHART_CHOICE_FLAG"); }
        public void ColumnMinimumRate() { doColumn("MINIMUM_RATE"); }
        public void ColumnAxisNoanswerOnoff() { doColumn("AXIS_NOANSWER_ONOFF"); }
        public void ColumnTargetNoanswerOnoff() { doColumn("TARGET_NOANSWER_ONOFF"); }
        public void ColumnPolylineOnoff() { doColumn("POLYLINE_ONOFF"); }
        public void ColumnMarkingN() { doColumn("MARKING_N"); }
        public void ColumnRankingFlag() { doColumn("RANKING_FLAG"); }
        public void ColumnRateFlag() { doColumn("RATE_FLAG"); }
        public void ColumnRate1Flag() { doColumn("RATE1_FLAG"); }
        public void ColumnRate1Sign() { doColumn("RATE1_SIGN"); }
        public void ColumnRate1Range() { doColumn("RATE1_RANGE"); }
        public void ColumnRate1Backcolor1() { doColumn("RATE1_BACKCOLOR1"); }
        public void ColumnRate1Backcolor2() { doColumn("RATE1_BACKCOLOR2"); }
        public void ColumnRate2Flag() { doColumn("RATE2_FLAG"); }
        public void ColumnRate2Sign() { doColumn("RATE2_SIGN"); }
        public void ColumnRate2Range() { doColumn("RATE2_RANGE"); }
        public void ColumnRate2Backcolor1() { doColumn("RATE2_BACKCOLOR1"); }
        public void ColumnRate2Backcolor2() { doColumn("RATE2_BACKCOLOR2"); }
        public void ColumnLastUpdateUser() { doColumn("LAST_UPDATE_USER"); }
        public void ColumnLastUpdateDatetime() { doColumn("LAST_UPDATE_DATETIME"); }
        public void ColumnTestFlag() { doColumn("TEST_FLAG"); }
        public void ColumnTestType() { doColumn("TEST_TYPE"); }
        public void ColumnTestSignificanceLv() { doColumn("TEST_SIGNIFICANCE_LV"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnScenarioTotalizationId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTDefaultEnv()
                    || qyCall().qy().xgetReferrerQuery() is TDefaultEnvCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_SCENARIO_TOTALIZATION"; }
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
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TGtScenarioItemCBSpecification SpecifyTGtScenarioItem() {
            assertForeign("tGtScenarioItem");
            if (_tGtScenarioItem == null) {
                _tGtScenarioItem = new TGtScenarioItemCBSpecification(_baseCB, new TGtScenarioItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tGtScenarioItem.xsetSyncQyCall(new TGtScenarioItemSpQyCall(xsyncQyCall())); }
            }
            return _tGtScenarioItem;
        }
		public class TGtScenarioItemSpQyCall : HpSpQyCall<TGtScenarioItemCQ> {
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TGtScenarioItemSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTGtScenarioItem(); }
			public TGtScenarioItemCQ qy() { return _qyCall.qy().QueryTGtScenarioItem(); }
		}
        public TCrossScenarioTargetCBSpecification SpecifyTCrossScenarioTarget() {
            assertForeign("tCrossScenarioTarget");
            if (_tCrossScenarioTarget == null) {
                _tCrossScenarioTarget = new TCrossScenarioTargetCBSpecification(_baseCB, new TCrossScenarioTargetSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tCrossScenarioTarget.xsetSyncQyCall(new TCrossScenarioTargetSpQyCall(xsyncQyCall())); }
            }
            return _tCrossScenarioTarget;
        }
		public class TCrossScenarioTargetSpQyCall : HpSpQyCall<TCrossScenarioTargetCQ> {
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TCrossScenarioTargetSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCrossScenarioTarget(); }
			public TCrossScenarioTargetCQ qy() { return _qyCall.qy().QueryTCrossScenarioTarget(); }
		}
        public TFaScenarioHeaderCBSpecification SpecifyTFaScenarioHeader() {
            assertForeign("tFaScenarioHeader");
            if (_tFaScenarioHeader == null) {
                _tFaScenarioHeader = new TFaScenarioHeaderCBSpecification(_baseCB, new TFaScenarioHeaderSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tFaScenarioHeader.xsetSyncQyCall(new TFaScenarioHeaderSpQyCall(xsyncQyCall())); }
            }
            return _tFaScenarioHeader;
        }
		public class TFaScenarioHeaderSpQyCall : HpSpQyCall<TFaScenarioHeaderCQ> {
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TFaScenarioHeaderSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTFaScenarioHeader(); }
			public TFaScenarioHeaderCQ qy() { return _qyCall.qy().QueryTFaScenarioHeader(); }
		}
        public TScenarioQuerylistCBSpecification SpecifyTScenarioQuerylist() {
            assertForeign("tScenarioQuerylist");
            if (_tScenarioQuerylist == null) {
                _tScenarioQuerylist = new TScenarioQuerylistCBSpecification(_baseCB, new TScenarioQuerylistSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tScenarioQuerylist.xsetSyncQyCall(new TScenarioQuerylistSpQyCall(xsyncQyCall())); }
            }
            return _tScenarioQuerylist;
        }
		public class TScenarioQuerylistSpQyCall : HpSpQyCall<TScenarioQuerylistCQ> {
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TScenarioQuerylistSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioQuerylist(); }
			public TScenarioQuerylistCQ qy() { return _qyCall.qy().QueryTScenarioQuerylist(); }
		}
        public TCategoryOutputEditCBSpecification SpecifyTCategoryOutputEdit() {
            assertForeign("tCategoryOutputEdit");
            if (_tCategoryOutputEdit == null) {
                _tCategoryOutputEdit = new TCategoryOutputEditCBSpecification(_baseCB, new TCategoryOutputEditSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tCategoryOutputEdit.xsetSyncQyCall(new TCategoryOutputEditSpQyCall(xsyncQyCall())); }
            }
            return _tCategoryOutputEdit;
        }
		public class TCategoryOutputEditSpQyCall : HpSpQyCall<TCategoryOutputEditCQ> {
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TCategoryOutputEditSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCategoryOutputEdit(); }
			public TCategoryOutputEditCQ qy() { return _qyCall.qy().QueryTCategoryOutputEdit(); }
		}
        public TGtMatrixInfoCBSpecification SpecifyTGtMatrixInfo() {
            assertForeign("tGtMatrixInfo");
            if (_tGtMatrixInfo == null) {
                _tGtMatrixInfo = new TGtMatrixInfoCBSpecification(_baseCB, new TGtMatrixInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tGtMatrixInfo.xsetSyncQyCall(new TGtMatrixInfoSpQyCall(xsyncQyCall())); }
            }
            return _tGtMatrixInfo;
        }
		public class TGtMatrixInfoSpQyCall : HpSpQyCall<TGtMatrixInfoCQ> {
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TGtMatrixInfoSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTGtMatrixInfo(); }
			public TGtMatrixInfoCQ qy() { return _qyCall.qy().QueryTGtMatrixInfo(); }
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
		    protected HpSpQyCall<TScenarioTotalizationCQ> _qyCall;
		    public TDefaultEnvSpQyCall(HpSpQyCall<TScenarioTotalizationCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnv(); }
			public TDefaultEnvCQ qy() { return _qyCall.qy().QueryTDefaultEnv(); }
		}
        public RAFunction<TCategoryOutputEditCB, TScenarioTotalizationCQ> DerivedTCategoryOutputEditList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TCategoryOutputEditCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TCategoryOutputEditCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTCategoryOutputEditList(function, subQuery, aliasName); });
        }
        public RAFunction<TCrossScenarioTargetCB, TScenarioTotalizationCQ> DerivedTCrossScenarioTargetList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TCrossScenarioTargetCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TCrossScenarioTargetCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTCrossScenarioTargetList(function, subQuery, aliasName); });
        }
        public RAFunction<TFaScenarioHeaderCB, TScenarioTotalizationCQ> DerivedTFaScenarioHeaderList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TFaScenarioHeaderCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TFaScenarioHeaderCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTFaScenarioHeaderList(function, subQuery, aliasName); });
        }
        public RAFunction<TGtMatrixInfoCB, TScenarioTotalizationCQ> DerivedTGtMatrixInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TGtMatrixInfoCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TGtMatrixInfoCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTGtMatrixInfoList(function, subQuery, aliasName); });
        }
        public RAFunction<TGtScenarioItemCB, TScenarioTotalizationCQ> DerivedTGtScenarioItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TGtScenarioItemCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TGtScenarioItemCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTGtScenarioItemList(function, subQuery, aliasName); });
        }
        public RAFunction<TScenarioQuerylistCB, TScenarioTotalizationCQ> DerivedTScenarioQuerylistList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TScenarioQuerylistCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TScenarioQuerylistCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTScenarioQuerylistList(function, subQuery, aliasName); });
        }
        public RAFunction<TItemInfoCB, TScenarioTotalizationCQ> DerivedTItemInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TItemInfoCB, TScenarioTotalizationCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TItemInfoCB> subQuery, TScenarioTotalizationCQ cq, String aliasName)
                { cq.xsderiveTItemInfoList(function, subQuery, aliasName); });
        }
    }
}
