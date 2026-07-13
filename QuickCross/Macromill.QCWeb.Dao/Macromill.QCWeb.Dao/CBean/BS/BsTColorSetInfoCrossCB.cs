
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
    public class BsTColorSetInfoCrossCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorSetInfoCrossCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_SET_INFO_CROSS"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? colorSetInfoCrossId) {
            assertObjectNotNull("colorSetInfoCrossId", colorSetInfoCrossId);
            BsTColorSetInfoCrossCB cb = this;
            cb.Query().SetColorSetInfoCrossId_Equal(colorSetInfoCrossId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ColorSetInfoCrossId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ColorSetInfoCrossId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TColorSetInfoCrossCQ Query() {
            return this.ConditionQuery;
        }

        public TColorSetInfoCrossCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TColorSetInfoCrossCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TColorSetInfoCrossCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TColorSetInfoCrossCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TColorSetInfoCrossCB> unionQuery) {
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorSetInfoCrossCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TColorSetInfoCrossCB> unionQuery) {
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorSetInfoCrossCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TColorSetInfoCrossCBSpecification _specification;
        public TColorSetInfoCrossCBSpecification Specify() {
            if (_specification == null) { _specification = new TColorSetInfoCrossCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TColorSetInfoCrossCQ> {
			protected BsTColorSetInfoCrossCB _myCB;
			public MySpQyCall(BsTColorSetInfoCrossCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TColorSetInfoCrossCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TColorSetInfoCrossCB> ColumnQuery(SpecifyQuery<TColorSetInfoCrossCB> leftSpecifyQuery) {
            return new HpColQyOperand<TColorSetInfoCrossCB>(delegate(SpecifyQuery<TColorSetInfoCrossCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TColorSetInfoCrossCB xcreateColumnQueryCB() {
            TColorSetInfoCrossCB cb = new TColorSetInfoCrossCB();
            cb.xsetupForColumnQuery((TColorSetInfoCrossCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TColorSetInfoCrossCB> orQuery) {
            xorQ((TColorSetInfoCrossCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TColorSetInfoCrossCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TColorSetInfoCrossCBColQySpQyCall(mainCB));
        }
    }

    public class TColorSetInfoCrossCBColQySpQyCall : HpSpQyCall<TColorSetInfoCrossCQ> {
        protected TColorSetInfoCrossCB _mainCB;
        public TColorSetInfoCrossCBColQySpQyCall(TColorSetInfoCrossCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TColorSetInfoCrossCQ qy() { return _mainCB.Query(); } 
    }

    public class TColorSetInfoCrossCBSpecification : AbstractSpecification<TColorSetInfoCrossCQ> {
        protected TCrossScenarioTargetCBSpecification _tCrossScenarioTarget;
        public TColorSetInfoCrossCBSpecification(ConditionBean baseCB, HpSpQyCall<TColorSetInfoCrossCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnColorSetInfoCrossId() { doColumn("COLOR_SET_INFO_CROSS_ID"); }
        public void ColumnTypeCode() { doColumn("TYPE_CODE"); }
        public void ColumnGradationType() { doColumn("GRADATION_TYPE"); }
        public void ColumnCrossScenarioTargetId() { doColumn("CROSS_SCENARIO_TARGET_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnColorSetInfoCrossId(); // PK
            if (qyCall().qy().hasConditionQueryTCrossScenarioTarget()
                    || qyCall().qy().xgetReferrerQuery() is TCrossScenarioTargetCQ) {
                ColumnCrossScenarioTargetId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_COLOR_SET_INFO_CROSS"; }
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
		    protected HpSpQyCall<TColorSetInfoCrossCQ> _qyCall;
		    public TCrossScenarioTargetSpQyCall(HpSpQyCall<TColorSetInfoCrossCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCrossScenarioTarget(); }
			public TCrossScenarioTargetCQ qy() { return _qyCall.qy().QueryTCrossScenarioTarget(); }
		}
        public RAFunction<TColorInfoDetailCrossCB, TColorSetInfoCrossCQ> DerivedTColorInfoDetailCrossList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TColorInfoDetailCrossCB, TColorSetInfoCrossCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TColorInfoDetailCrossCB> subQuery, TColorSetInfoCrossCQ cq, String aliasName)
                { cq.xsderiveTColorInfoDetailCrossList(function, subQuery, aliasName); });
        }
    }
}
