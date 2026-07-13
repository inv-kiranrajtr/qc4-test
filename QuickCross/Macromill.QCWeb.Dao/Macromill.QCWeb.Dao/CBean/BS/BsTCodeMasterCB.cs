
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
    public class BsTCodeMasterCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCodeMasterCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CODE_MASTER"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(String codeMasterId) {
            assertObjectNotNull("codeMasterId", codeMasterId);
            BsTCodeMasterCB cb = this;
            cb.Query().SetCodeMasterId_Equal(codeMasterId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_CodeMasterId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_CodeMasterId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TCodeMasterCQ Query() {
            return this.ConditionQuery;
        }

        public TCodeMasterCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TCodeMasterCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TCodeMasterCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TCodeMasterCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TCodeMasterCB> unionQuery) {
            TCodeMasterCB cb = new TCodeMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCodeMasterCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TCodeMasterCB> unionQuery) {
            TCodeMasterCB cb = new TCodeMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCodeMasterCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TCodeMasterCBSpecification _specification;
        public TCodeMasterCBSpecification Specify() {
            if (_specification == null) { _specification = new TCodeMasterCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TCodeMasterCQ> {
			protected BsTCodeMasterCB _myCB;
			public MySpQyCall(BsTCodeMasterCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TCodeMasterCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TCodeMasterCB> ColumnQuery(SpecifyQuery<TCodeMasterCB> leftSpecifyQuery) {
            return new HpColQyOperand<TCodeMasterCB>(delegate(SpecifyQuery<TCodeMasterCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TCodeMasterCB xcreateColumnQueryCB() {
            TCodeMasterCB cb = new TCodeMasterCB();
            cb.xsetupForColumnQuery((TCodeMasterCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TCodeMasterCB> orQuery) {
            xorQ((TCodeMasterCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TCodeMasterCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TCodeMasterCBColQySpQyCall(mainCB));
        }
    }

    public class TCodeMasterCBColQySpQyCall : HpSpQyCall<TCodeMasterCQ> {
        protected TCodeMasterCB _mainCB;
        public TCodeMasterCBColQySpQyCall(TCodeMasterCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TCodeMasterCQ qy() { return _mainCB.Query(); } 
    }

    public class TCodeMasterCBSpecification : AbstractSpecification<TCodeMasterCQ> {
        public TCodeMasterCBSpecification(ConditionBean baseCB, HpSpQyCall<TCodeMasterCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnCodeMasterId() { doColumn("CODE_MASTER_ID"); }
        public void ColumnGroupKey() { doColumn("GROUP_KEY"); }
        public void ColumnCodeValue() { doColumn("CODE_VALUE"); }
        public void ColumnMessageId() { doColumn("MESSAGE_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnCodeMasterId(); // PK
        }
        protected override String getTableDbName() { return "T_CODE_MASTER"; }
    }
}
