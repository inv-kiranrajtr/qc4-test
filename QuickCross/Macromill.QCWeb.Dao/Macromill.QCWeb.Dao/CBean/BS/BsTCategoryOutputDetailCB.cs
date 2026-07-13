
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
    public class BsTCategoryOutputDetailCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCategoryOutputDetailCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_OUTPUT_DETAIL"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? categoryOutputEditDetailId) {
            assertObjectNotNull("categoryOutputEditDetailId", categoryOutputEditDetailId);
            BsTCategoryOutputDetailCB cb = this;
            cb.Query().SetCategoryOutputEditDetailId_Equal(categoryOutputEditDetailId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_CategoryOutputEditDetailId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_CategoryOutputEditDetailId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TCategoryOutputDetailCQ Query() {
            return this.ConditionQuery;
        }

        public TCategoryOutputDetailCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TCategoryOutputDetailCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TCategoryOutputDetailCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TCategoryOutputDetailCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TCategoryOutputDetailCB> unionQuery) {
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCategoryOutputDetailCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TCategoryOutputDetailCB> unionQuery) {
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCategoryOutputDetailCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TCategoryOutputEditNss _nssTCategoryOutputEdit;
        public TCategoryOutputEditNss NssTCategoryOutputEdit { get {
            if (_nssTCategoryOutputEdit == null) { _nssTCategoryOutputEdit = new TCategoryOutputEditNss(null); }
            return _nssTCategoryOutputEdit;
        }}
        public TCategoryOutputEditNss SetupSelect_TCategoryOutputEdit() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnCategoryOutputEditId();
            }
            doSetupSelect(delegate { return Query().QueryTCategoryOutputEdit(); });
            if (_nssTCategoryOutputEdit == null || !_nssTCategoryOutputEdit.HasConditionQuery)
            { _nssTCategoryOutputEdit = new TCategoryOutputEditNss(Query().QueryTCategoryOutputEdit()); }
            return _nssTCategoryOutputEdit;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TCategoryOutputDetailCBSpecification _specification;
        public TCategoryOutputDetailCBSpecification Specify() {
            if (_specification == null) { _specification = new TCategoryOutputDetailCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TCategoryOutputDetailCQ> {
			protected BsTCategoryOutputDetailCB _myCB;
			public MySpQyCall(BsTCategoryOutputDetailCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TCategoryOutputDetailCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TCategoryOutputDetailCB> ColumnQuery(SpecifyQuery<TCategoryOutputDetailCB> leftSpecifyQuery) {
            return new HpColQyOperand<TCategoryOutputDetailCB>(delegate(SpecifyQuery<TCategoryOutputDetailCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TCategoryOutputDetailCB xcreateColumnQueryCB() {
            TCategoryOutputDetailCB cb = new TCategoryOutputDetailCB();
            cb.xsetupForColumnQuery((TCategoryOutputDetailCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TCategoryOutputDetailCB> orQuery) {
            xorQ((TCategoryOutputDetailCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TCategoryOutputDetailCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TCategoryOutputDetailCBColQySpQyCall(mainCB));
        }
    }

    public class TCategoryOutputDetailCBColQySpQyCall : HpSpQyCall<TCategoryOutputDetailCQ> {
        protected TCategoryOutputDetailCB _mainCB;
        public TCategoryOutputDetailCBColQySpQyCall(TCategoryOutputDetailCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TCategoryOutputDetailCQ qy() { return _mainCB.Query(); } 
    }

    public class TCategoryOutputDetailCBSpecification : AbstractSpecification<TCategoryOutputDetailCQ> {
        protected TCategoryOutputEditCBSpecification _tCategoryOutputEdit;
        public TCategoryOutputDetailCBSpecification(ConditionBean baseCB, HpSpQyCall<TCategoryOutputDetailCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnCategoryOutputEditDetailId() { doColumn("CATEGORY_OUTPUT_EDIT_DETAIL_ID"); }
        public void ColumnCategoryOutputEditId() { doColumn("CATEGORY_OUTPUT_EDIT_ID"); }
        public void ColumnOldCategoryNo() { doColumn("OLD_CATEGORY_NO"); }
        public void ColumnNewCategoryNo() { doColumn("NEW_CATEGORY_NO"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnCategoryOutputEditDetailId(); // PK
            if (qyCall().qy().hasConditionQueryTCategoryOutputEdit()
                    || qyCall().qy().xgetReferrerQuery() is TCategoryOutputEditCQ) {
                ColumnCategoryOutputEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_CATEGORY_OUTPUT_DETAIL"; }
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
		    protected HpSpQyCall<TCategoryOutputDetailCQ> _qyCall;
		    public TCategoryOutputEditSpQyCall(HpSpQyCall<TCategoryOutputDetailCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCategoryOutputEdit(); }
			public TCategoryOutputEditCQ qy() { return _qyCall.qy().QueryTCategoryOutputEdit(); }
		}
    }
}
