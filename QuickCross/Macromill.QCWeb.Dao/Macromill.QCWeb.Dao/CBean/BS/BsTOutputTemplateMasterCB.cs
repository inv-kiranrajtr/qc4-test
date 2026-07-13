
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
    public class BsTOutputTemplateMasterCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputTemplateMasterCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_TEMPLATE_MASTER"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputTemplateMasterId) {
            assertObjectNotNull("outputTemplateMasterId", outputTemplateMasterId);
            BsTOutputTemplateMasterCB cb = this;
            cb.Query().SetOutputTemplateMasterId_Equal(outputTemplateMasterId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputTemplateMasterId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputTemplateMasterId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputTemplateMasterCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputTemplateMasterCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputTemplateMasterCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputTemplateMasterCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputTemplateMasterCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputTemplateMasterCB> unionQuery) {
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputTemplateMasterCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputTemplateMasterCB> unionQuery) {
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputTemplateMasterCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputTemplateMasterCBSpecification _specification;
        public TOutputTemplateMasterCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputTemplateMasterCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputTemplateMasterCQ> {
			protected BsTOutputTemplateMasterCB _myCB;
			public MySpQyCall(BsTOutputTemplateMasterCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputTemplateMasterCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputTemplateMasterCB> ColumnQuery(SpecifyQuery<TOutputTemplateMasterCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputTemplateMasterCB>(delegate(SpecifyQuery<TOutputTemplateMasterCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputTemplateMasterCB xcreateColumnQueryCB() {
            TOutputTemplateMasterCB cb = new TOutputTemplateMasterCB();
            cb.xsetupForColumnQuery((TOutputTemplateMasterCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputTemplateMasterCB> orQuery) {
            xorQ((TOutputTemplateMasterCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputTemplateMasterCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputTemplateMasterCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputTemplateMasterCBColQySpQyCall : HpSpQyCall<TOutputTemplateMasterCQ> {
        protected TOutputTemplateMasterCB _mainCB;
        public TOutputTemplateMasterCBColQySpQyCall(TOutputTemplateMasterCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputTemplateMasterCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputTemplateMasterCBSpecification : AbstractSpecification<TOutputTemplateMasterCQ> {
        public TOutputTemplateMasterCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputTemplateMasterCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputTemplateMasterId() { doColumn("OUTPUT_TEMPLATE_MASTER_ID"); }
        public void ColumnPath() { doColumn("PATH"); }
        public void ColumnMd5Hash() { doColumn("MD5_HASH"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputTemplateMasterId(); // PK
        }
        protected override String getTableDbName() { return "T_OUTPUT_TEMPLATE_MASTER"; }
        public RAFunction<TOutputTemplateCB, TOutputTemplateMasterCQ> DerivedTOutputTemplateList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputTemplateCB, TOutputTemplateMasterCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputTemplateCB> subQuery, TOutputTemplateMasterCQ cq, String aliasName)
                { cq.xsderiveTOutputTemplateList(function, subQuery, aliasName); });
        }
    }
}
