
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
    public class BsTGtScenarioItemCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TGtScenarioItemCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? gtScenarioItemId) {
            assertObjectNotNull("gtScenarioItemId", gtScenarioItemId);
            BsTGtScenarioItemCB cb = this;
            cb.Query().SetGtScenarioItemId_Equal(gtScenarioItemId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_GtScenarioItemId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_GtScenarioItemId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TGtScenarioItemCQ Query() {
            return this.ConditionQuery;
        }

        public TGtScenarioItemCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TGtScenarioItemCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TGtScenarioItemCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TGtScenarioItemCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TGtScenarioItemCB> unionQuery) {
            TGtScenarioItemCB cb = new TGtScenarioItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TGtScenarioItemCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TGtScenarioItemCB> unionQuery) {
            TGtScenarioItemCB cb = new TGtScenarioItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TGtScenarioItemCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TScenarioTotalizationNss _nssTScenarioTotalization;
        public TScenarioTotalizationNss NssTScenarioTotalization { get {
            if (_nssTScenarioTotalization == null) { _nssTScenarioTotalization = new TScenarioTotalizationNss(null); }
            return _nssTScenarioTotalization;
        }}
        public TScenarioTotalizationNss SetupSelect_TScenarioTotalization() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnScenarioTotalizationId();
            }
            doSetupSelect(delegate { return Query().QueryTScenarioTotalization(); });
            if (_nssTScenarioTotalization == null || !_nssTScenarioTotalization.HasConditionQuery)
            { _nssTScenarioTotalization = new TScenarioTotalizationNss(Query().QueryTScenarioTotalization()); }
            return _nssTScenarioTotalization;
        }
        protected TItemInfoNss _nssTItemInfo;
        public TItemInfoNss NssTItemInfo { get {
            if (_nssTItemInfo == null) { _nssTItemInfo = new TItemInfoNss(null); }
            return _nssTItemInfo;
        }}
        public TItemInfoNss SetupSelect_TItemInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnItemInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTItemInfo(); });
            if (_nssTItemInfo == null || !_nssTItemInfo.HasConditionQuery)
            { _nssTItemInfo = new TItemInfoNss(Query().QueryTItemInfo()); }
            return _nssTItemInfo;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TGtScenarioItemCBSpecification _specification;
        public TGtScenarioItemCBSpecification Specify() {
            if (_specification == null) { _specification = new TGtScenarioItemCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TGtScenarioItemCQ> {
			protected BsTGtScenarioItemCB _myCB;
			public MySpQyCall(BsTGtScenarioItemCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TGtScenarioItemCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TGtScenarioItemCB> ColumnQuery(SpecifyQuery<TGtScenarioItemCB> leftSpecifyQuery) {
            return new HpColQyOperand<TGtScenarioItemCB>(delegate(SpecifyQuery<TGtScenarioItemCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TGtScenarioItemCB xcreateColumnQueryCB() {
            TGtScenarioItemCB cb = new TGtScenarioItemCB();
            cb.xsetupForColumnQuery((TGtScenarioItemCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TGtScenarioItemCB> orQuery) {
            xorQ((TGtScenarioItemCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TGtScenarioItemCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TGtScenarioItemCBColQySpQyCall(mainCB));
        }
    }

    public class TGtScenarioItemCBColQySpQyCall : HpSpQyCall<TGtScenarioItemCQ> {
        protected TGtScenarioItemCB _mainCB;
        public TGtScenarioItemCBColQySpQyCall(TGtScenarioItemCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TGtScenarioItemCQ qy() { return _mainCB.Query(); } 
    }

    public class TGtScenarioItemCBSpecification : AbstractSpecification<TGtScenarioItemCQ> {
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TItemInfoCBSpecification _tItemInfo;
        public TGtScenarioItemCBSpecification(ConditionBean baseCB, HpSpQyCall<TGtScenarioItemCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnGtScenarioItemId() { doColumn("GT_SCENARIO_ITEM_ID"); }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnItemInfoId() { doColumn("ITEM_INFO_ID"); }
        public void ColumnScenarioName() { doColumn("SCENARIO_NAME"); }
        public void ColumnGraphType() { doColumn("GRAPH_TYPE"); }
        public void ColumnReportType() { doColumn("REPORT_TYPE"); }
        public void ColumnViewItemString() { doColumn("VIEW_ITEM_STRING"); }
        public void ColumnScenarioComment() { doColumn("SCENARIO_COMMENT"); }
        public void ColumnSurveyType() { doColumn("SURVEY_TYPE"); }
        public void ColumnGraphTypeReport() { doColumn("GRAPH_TYPE_REPORT"); }
        public void ColumnTestTargetType() { doColumn("TEST_TARGET_TYPE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnGtScenarioItemId(); // PK
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnScenarioTotalizationId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTItemInfo()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnItemInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_GT_SCENARIO_ITEM"; }
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
		    protected HpSpQyCall<TGtScenarioItemCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TGtScenarioItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
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
		    protected HpSpQyCall<TGtScenarioItemCQ> _qyCall;
		    public TItemInfoSpQyCall(HpSpQyCall<TGtScenarioItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfo(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfo(); }
		}
        public RAFunction<TColorSetInfoGtCB, TGtScenarioItemCQ> DerivedTColorSetInfoGtList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TColorSetInfoGtCB, TGtScenarioItemCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TColorSetInfoGtCB> subQuery, TGtScenarioItemCQ cq, String aliasName)
                { cq.xsderiveTColorSetInfoGtList(function, subQuery, aliasName); });
        }
    }
}
