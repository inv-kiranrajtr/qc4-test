
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
    public class BsTMaintenanceCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TMaintenanceCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_MAINTENANCE"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(String maintenanceId) {
            assertObjectNotNull("maintenanceId", maintenanceId);
            BsTMaintenanceCB cb = this;
            cb.Query().SetMaintenanceId_Equal(maintenanceId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_MaintenanceId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_MaintenanceId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TMaintenanceCQ Query() {
            return this.ConditionQuery;
        }

        public TMaintenanceCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TMaintenanceCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TMaintenanceCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TMaintenanceCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TMaintenanceCB> unionQuery) {
            TMaintenanceCB cb = new TMaintenanceCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TMaintenanceCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TMaintenanceCB> unionQuery) {
            TMaintenanceCB cb = new TMaintenanceCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TMaintenanceCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TMaintenanceCBSpecification _specification;
        public TMaintenanceCBSpecification Specify() {
            if (_specification == null) { _specification = new TMaintenanceCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TMaintenanceCQ> {
			protected BsTMaintenanceCB _myCB;
			public MySpQyCall(BsTMaintenanceCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TMaintenanceCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TMaintenanceCB> ColumnQuery(SpecifyQuery<TMaintenanceCB> leftSpecifyQuery) {
            return new HpColQyOperand<TMaintenanceCB>(delegate(SpecifyQuery<TMaintenanceCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TMaintenanceCB xcreateColumnQueryCB() {
            TMaintenanceCB cb = new TMaintenanceCB();
            cb.xsetupForColumnQuery((TMaintenanceCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TMaintenanceCB> orQuery) {
            xorQ((TMaintenanceCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TMaintenanceCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TMaintenanceCBColQySpQyCall(mainCB));
        }
    }

    public class TMaintenanceCBColQySpQyCall : HpSpQyCall<TMaintenanceCQ> {
        protected TMaintenanceCB _mainCB;
        public TMaintenanceCBColQySpQyCall(TMaintenanceCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TMaintenanceCQ qy() { return _mainCB.Query(); } 
    }

    public class TMaintenanceCBSpecification : AbstractSpecification<TMaintenanceCQ> {
        public TMaintenanceCBSpecification(ConditionBean baseCB, HpSpQyCall<TMaintenanceCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnMaintenanceId() { doColumn("MAINTENANCE_ID"); }
        public void ColumnLimitTime() { doColumn("LIMIT_TIME"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnMaintenanceId(); // PK
        }
        protected override String getTableDbName() { return "T_MAINTENANCE"; }
    }
}
