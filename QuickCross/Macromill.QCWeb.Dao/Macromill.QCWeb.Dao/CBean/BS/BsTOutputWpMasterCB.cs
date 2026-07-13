
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
    public class BsTOutputWpMasterCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputWpMasterCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_WP_MASTER"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(String outputWpMasterId) {
            assertObjectNotNull("outputWpMasterId", outputWpMasterId);
            BsTOutputWpMasterCB cb = this;
            cb.Query().SetOutputWpMasterId_Equal(outputWpMasterId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputWpMasterId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputWpMasterId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputWpMasterCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputWpMasterCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputWpMasterCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputWpMasterCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputWpMasterCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputWpMasterCB> unionQuery) {
            TOutputWpMasterCB cb = new TOutputWpMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputWpMasterCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputWpMasterCB> unionQuery) {
            TOutputWpMasterCB cb = new TOutputWpMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputWpMasterCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputWpMasterCBSpecification _specification;
        public TOutputWpMasterCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputWpMasterCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputWpMasterCQ> {
			protected BsTOutputWpMasterCB _myCB;
			public MySpQyCall(BsTOutputWpMasterCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputWpMasterCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputWpMasterCB> ColumnQuery(SpecifyQuery<TOutputWpMasterCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputWpMasterCB>(delegate(SpecifyQuery<TOutputWpMasterCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputWpMasterCB xcreateColumnQueryCB() {
            TOutputWpMasterCB cb = new TOutputWpMasterCB();
            cb.xsetupForColumnQuery((TOutputWpMasterCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputWpMasterCB> orQuery) {
            xorQ((TOutputWpMasterCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputWpMasterCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputWpMasterCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputWpMasterCBColQySpQyCall : HpSpQyCall<TOutputWpMasterCQ> {
        protected TOutputWpMasterCB _mainCB;
        public TOutputWpMasterCBColQySpQyCall(TOutputWpMasterCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputWpMasterCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputWpMasterCBSpecification : AbstractSpecification<TOutputWpMasterCQ> {
        public TOutputWpMasterCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputWpMasterCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputWpMasterId() { doColumn("OUTPUT_WP_MASTER_ID"); }
        public void ColumnPoint() { doColumn("POINT"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputWpMasterId(); // PK
        }
        protected override String getTableDbName() { return "T_OUTPUT_WP_MASTER"; }
    }
}
