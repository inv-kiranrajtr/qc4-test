
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
    public class BsTCrossScenarioItemCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCrossScenarioItemCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CROSS_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? crossScenarioItemId) {
            assertObjectNotNull("crossScenarioItemId", crossScenarioItemId);
            BsTCrossScenarioItemCB cb = this;
            cb.Query().SetCrossScenarioItemId_Equal(crossScenarioItemId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_CrossScenarioItemId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_CrossScenarioItemId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TCrossScenarioItemCQ Query() {
            return this.ConditionQuery;
        }

        public TCrossScenarioItemCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TCrossScenarioItemCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TCrossScenarioItemCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TCrossScenarioItemCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TCrossScenarioItemCB> unionQuery) {
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCrossScenarioItemCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TCrossScenarioItemCB> unionQuery) {
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCrossScenarioItemCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TCrossScenarioTargetNss _nssTCrossScenarioTarget;
        public TCrossScenarioTargetNss NssTCrossScenarioTarget { get {
            if (_nssTCrossScenarioTarget == null) { _nssTCrossScenarioTarget = new TCrossScenarioTargetNss(null); }
            return _nssTCrossScenarioTarget;
        }}
        public TCrossScenarioTargetNss SetupSelect_TCrossScenarioTarget() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnCrossScenarioTargetId();
            }
            doSetupSelect(delegate { return Query().QueryTCrossScenarioTarget(); });
            if (_nssTCrossScenarioTarget == null || !_nssTCrossScenarioTarget.HasConditionQuery)
            { _nssTCrossScenarioTarget = new TCrossScenarioTargetNss(Query().QueryTCrossScenarioTarget()); }
            return _nssTCrossScenarioTarget;
        }
        protected TPolylineCategoryListNss _nssTPolylineCategoryList;
        public TPolylineCategoryListNss NssTPolylineCategoryList { get {
            if (_nssTPolylineCategoryList == null) { _nssTPolylineCategoryList = new TPolylineCategoryListNss(null); }
            return _nssTPolylineCategoryList;
        }}
        public TPolylineCategoryListNss SetupSelect_TPolylineCategoryList() {
            doSetupSelect(delegate { return Query().QueryTPolylineCategoryList(); });
            if (_nssTPolylineCategoryList == null || !_nssTPolylineCategoryList.HasConditionQuery)
            { _nssTPolylineCategoryList = new TPolylineCategoryListNss(Query().QueryTPolylineCategoryList()); }
            return _nssTPolylineCategoryList;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TCrossScenarioItemCBSpecification _specification;
        public TCrossScenarioItemCBSpecification Specify() {
            if (_specification == null) { _specification = new TCrossScenarioItemCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TCrossScenarioItemCQ> {
			protected BsTCrossScenarioItemCB _myCB;
			public MySpQyCall(BsTCrossScenarioItemCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TCrossScenarioItemCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TCrossScenarioItemCB> ColumnQuery(SpecifyQuery<TCrossScenarioItemCB> leftSpecifyQuery) {
            return new HpColQyOperand<TCrossScenarioItemCB>(delegate(SpecifyQuery<TCrossScenarioItemCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TCrossScenarioItemCB xcreateColumnQueryCB() {
            TCrossScenarioItemCB cb = new TCrossScenarioItemCB();
            cb.xsetupForColumnQuery((TCrossScenarioItemCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TCrossScenarioItemCB> orQuery) {
            xorQ((TCrossScenarioItemCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TCrossScenarioItemCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TCrossScenarioItemCBColQySpQyCall(mainCB));
        }
    }

    public class TCrossScenarioItemCBColQySpQyCall : HpSpQyCall<TCrossScenarioItemCQ> {
        protected TCrossScenarioItemCB _mainCB;
        public TCrossScenarioItemCBColQySpQyCall(TCrossScenarioItemCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TCrossScenarioItemCQ qy() { return _mainCB.Query(); } 
    }

    public class TCrossScenarioItemCBSpecification : AbstractSpecification<TCrossScenarioItemCQ> {
        protected TCrossScenarioTargetCBSpecification _tCrossScenarioTarget;
        protected TPolylineCategoryListCBSpecification _tPolylineCategoryList;
        public TCrossScenarioItemCBSpecification(ConditionBean baseCB, HpSpQyCall<TCrossScenarioItemCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnCrossScenarioItemId() { doColumn("CROSS_SCENARIO_ITEM_ID"); }
        public void ColumnCrossScenarioTargetId() { doColumn("CROSS_SCENARIO_TARGET_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnAxis1ItemId() { doColumn("AXIS1_ITEM_ID"); }
        public void ColumnAxis2ItemId() { doColumn("AXIS2_ITEM_ID"); }
        public void ColumnViewItemName() { doColumn("VIEW_ITEM_NAME"); }
        public void ColumnGraphType() { doColumn("GRAPH_TYPE"); }
        public void ColumnReportType() { doColumn("REPORT_TYPE"); }
        public void ColumnTitleString() { doColumn("TITLE_STRING"); }
        public void ColumnScenarioComment() { doColumn("SCENARIO_COMMENT"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnCrossScenarioItemId(); // PK
            if (qyCall().qy().hasConditionQueryTCrossScenarioTarget()
                    || qyCall().qy().xgetReferrerQuery() is TCrossScenarioTargetCQ) {
                ColumnCrossScenarioTargetId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_CROSS_SCENARIO_ITEM"; }
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
		    protected HpSpQyCall<TCrossScenarioItemCQ> _qyCall;
		    public TCrossScenarioTargetSpQyCall(HpSpQyCall<TCrossScenarioItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCrossScenarioTarget(); }
			public TCrossScenarioTargetCQ qy() { return _qyCall.qy().QueryTCrossScenarioTarget(); }
		}
        public TPolylineCategoryListCBSpecification SpecifyTPolylineCategoryList() {
            assertForeign("tPolylineCategoryList");
            if (_tPolylineCategoryList == null) {
                _tPolylineCategoryList = new TPolylineCategoryListCBSpecification(_baseCB, new TPolylineCategoryListSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tPolylineCategoryList.xsetSyncQyCall(new TPolylineCategoryListSpQyCall(xsyncQyCall())); }
            }
            return _tPolylineCategoryList;
        }
		public class TPolylineCategoryListSpQyCall : HpSpQyCall<TPolylineCategoryListCQ> {
		    protected HpSpQyCall<TCrossScenarioItemCQ> _qyCall;
		    public TPolylineCategoryListSpQyCall(HpSpQyCall<TCrossScenarioItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTPolylineCategoryList(); }
			public TPolylineCategoryListCQ qy() { return _qyCall.qy().QueryTPolylineCategoryList(); }
		}
        public RAFunction<TPolylineCategoryListCB, TCrossScenarioItemCQ> DerivedTPolylineCategoryListList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TPolylineCategoryListCB, TCrossScenarioItemCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TPolylineCategoryListCB> subQuery, TCrossScenarioItemCQ cq, String aliasName)
                { cq.xsderiveTPolylineCategoryListList(function, subQuery, aliasName); });
        }
    }
}
