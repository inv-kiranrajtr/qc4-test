
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
    public class BsTCrossScenarioTargetCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCrossScenarioTargetCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CROSS_SCENARIO_TARGET"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? crossScenarioTargetId) {
            assertObjectNotNull("crossScenarioTargetId", crossScenarioTargetId);
            BsTCrossScenarioTargetCB cb = this;
            cb.Query().SetCrossScenarioTargetId_Equal(crossScenarioTargetId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_CrossScenarioTargetId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_CrossScenarioTargetId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TCrossScenarioTargetCQ Query() {
            return this.ConditionQuery;
        }

        public TCrossScenarioTargetCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TCrossScenarioTargetCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TCrossScenarioTargetCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TCrossScenarioTargetCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TCrossScenarioTargetCB> unionQuery) {
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCrossScenarioTargetCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TCrossScenarioTargetCB> unionQuery) {
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCrossScenarioTargetCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TCrossScenarioTargetCBSpecification _specification;
        public TCrossScenarioTargetCBSpecification Specify() {
            if (_specification == null) { _specification = new TCrossScenarioTargetCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TCrossScenarioTargetCQ> {
			protected BsTCrossScenarioTargetCB _myCB;
			public MySpQyCall(BsTCrossScenarioTargetCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TCrossScenarioTargetCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TCrossScenarioTargetCB> ColumnQuery(SpecifyQuery<TCrossScenarioTargetCB> leftSpecifyQuery) {
            return new HpColQyOperand<TCrossScenarioTargetCB>(delegate(SpecifyQuery<TCrossScenarioTargetCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TCrossScenarioTargetCB xcreateColumnQueryCB() {
            TCrossScenarioTargetCB cb = new TCrossScenarioTargetCB();
            cb.xsetupForColumnQuery((TCrossScenarioTargetCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TCrossScenarioTargetCB> orQuery) {
            xorQ((TCrossScenarioTargetCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TCrossScenarioTargetCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TCrossScenarioTargetCBColQySpQyCall(mainCB));
        }
    }

    public class TCrossScenarioTargetCBColQySpQyCall : HpSpQyCall<TCrossScenarioTargetCQ> {
        protected TCrossScenarioTargetCB _mainCB;
        public TCrossScenarioTargetCBColQySpQyCall(TCrossScenarioTargetCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TCrossScenarioTargetCQ qy() { return _mainCB.Query(); } 
    }

    public class TCrossScenarioTargetCBSpecification : AbstractSpecification<TCrossScenarioTargetCQ> {
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        public TCrossScenarioTargetCBSpecification(ConditionBean baseCB, HpSpQyCall<TCrossScenarioTargetCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnCrossScenarioTargetId() { doColumn("CROSS_SCENARIO_TARGET_ID"); }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnScenariosetNo() { doColumn("SCENARIOSET_NO"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnScItemId() { doColumn("SC_ITEM_ID"); }
        public void ColumnViewName() { doColumn("VIEW_NAME"); }
        public void ColumnGraphType() { doColumn("GRAPH_TYPE"); }
        public void ColumnReportType() { doColumn("REPORT_TYPE"); }
        public void ColumnViewItemString() { doColumn("VIEW_ITEM_STRING"); }
        public void ColumnScenarioComment() { doColumn("SCENARIO_COMMENT"); }
        public void ColumnPolylineFlag() { doColumn("POLYLINE_FLAG"); }
        public void ColumnGraphTypeReport() { doColumn("GRAPH_TYPE_REPORT"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnCrossScenarioTargetId(); // PK
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnScenarioTotalizationId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_CROSS_SCENARIO_TARGET"; }
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
		    protected HpSpQyCall<TCrossScenarioTargetCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TCrossScenarioTargetCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
        public RAFunction<TColorSetInfoCrossCB, TCrossScenarioTargetCQ> DerivedTColorSetInfoCrossList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TColorSetInfoCrossCB, TCrossScenarioTargetCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TColorSetInfoCrossCB> subQuery, TCrossScenarioTargetCQ cq, String aliasName)
                { cq.xsderiveTColorSetInfoCrossList(function, subQuery, aliasName); });
        }
        public RAFunction<TCrossScenarioItemCB, TCrossScenarioTargetCQ> DerivedTCrossScenarioItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TCrossScenarioItemCB, TCrossScenarioTargetCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TCrossScenarioItemCB> subQuery, TCrossScenarioTargetCQ cq, String aliasName)
                { cq.xsderiveTCrossScenarioItemList(function, subQuery, aliasName); });
        }
    }
}
