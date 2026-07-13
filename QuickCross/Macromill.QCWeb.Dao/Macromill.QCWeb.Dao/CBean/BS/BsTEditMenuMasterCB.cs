
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
    public class BsTEditMenuMasterCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TEditMenuMasterCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_MENU_MASTER"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(int? editMenuMasterId) {
            assertObjectNotNull("editMenuMasterId", editMenuMasterId);
            BsTEditMenuMasterCB cb = this;
            cb.Query().SetEditMenuMasterId_Equal(editMenuMasterId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_EditMenuMasterId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_EditMenuMasterId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TEditMenuMasterCQ Query() {
            return this.ConditionQuery;
        }

        public TEditMenuMasterCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TEditMenuMasterCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TEditMenuMasterCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TEditMenuMasterCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TEditMenuMasterCB> unionQuery) {
            TEditMenuMasterCB cb = new TEditMenuMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditMenuMasterCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TEditMenuMasterCB> unionQuery) {
            TEditMenuMasterCB cb = new TEditMenuMasterCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditMenuMasterCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TEditMenuMasterCBSpecification _specification;
        public TEditMenuMasterCBSpecification Specify() {
            if (_specification == null) { _specification = new TEditMenuMasterCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TEditMenuMasterCQ> {
			protected BsTEditMenuMasterCB _myCB;
			public MySpQyCall(BsTEditMenuMasterCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TEditMenuMasterCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TEditMenuMasterCB> ColumnQuery(SpecifyQuery<TEditMenuMasterCB> leftSpecifyQuery) {
            return new HpColQyOperand<TEditMenuMasterCB>(delegate(SpecifyQuery<TEditMenuMasterCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TEditMenuMasterCB xcreateColumnQueryCB() {
            TEditMenuMasterCB cb = new TEditMenuMasterCB();
            cb.xsetupForColumnQuery((TEditMenuMasterCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TEditMenuMasterCB> orQuery) {
            xorQ((TEditMenuMasterCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TEditMenuMasterCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TEditMenuMasterCBColQySpQyCall(mainCB));
        }
    }

    public class TEditMenuMasterCBColQySpQyCall : HpSpQyCall<TEditMenuMasterCQ> {
        protected TEditMenuMasterCB _mainCB;
        public TEditMenuMasterCBColQySpQyCall(TEditMenuMasterCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TEditMenuMasterCQ qy() { return _mainCB.Query(); } 
    }

    public class TEditMenuMasterCBSpecification : AbstractSpecification<TEditMenuMasterCQ> {
        public TEditMenuMasterCBSpecification(ConditionBean baseCB, HpSpQyCall<TEditMenuMasterCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnEditMenuMasterId() { doColumn("EDIT_MENU_MASTER_ID"); }
        public void ColumnEditClassification() { doColumn("EDIT_CLASSIFICATION"); }
        public void ColumnProcessType() { doColumn("PROCESS_TYPE"); }
        public void ColumnExplanation() { doColumn("EXPLANATION"); }
        public void ColumnExample() { doColumn("EXAMPLE"); }
        public void ColumnDetailedexplanation() { doColumn("DETAILEDEXPLANATION"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnTypeBitUnion() { doColumn("TYPE_BIT_UNION"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnEditMenuMasterId(); // PK
        }
        protected override String getTableDbName() { return "T_EDIT_MENU_MASTER"; }
        public RAFunction<TDataEditListCB, TEditMenuMasterCQ> DerivedTDataEditListList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDataEditListCB, TEditMenuMasterCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDataEditListCB> subQuery, TEditMenuMasterCQ cq, String aliasName)
                { cq.xsderiveTDataEditListList(function, subQuery, aliasName); });
        }
    }
}
