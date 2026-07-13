
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
    public class BsTDeleteConditionCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDeleteConditionCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_CONDITION"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? deleteConditionId) {
            assertObjectNotNull("deleteConditionId", deleteConditionId);
            BsTDeleteConditionCB cb = this;
            cb.Query().SetDeleteConditionId_Equal(deleteConditionId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DeleteConditionId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DeleteConditionId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDeleteConditionCQ Query() {
            return this.ConditionQuery;
        }

        public TDeleteConditionCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDeleteConditionCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDeleteConditionCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDeleteConditionCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDeleteConditionCB> unionQuery) {
            TDeleteConditionCB cb = new TDeleteConditionCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDeleteConditionCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDeleteConditionCB> unionQuery) {
            TDeleteConditionCB cb = new TDeleteConditionCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDeleteConditionCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDeleteDataNss _nssTDeleteData;
        public TDeleteDataNss NssTDeleteData { get {
            if (_nssTDeleteData == null) { _nssTDeleteData = new TDeleteDataNss(null); }
            return _nssTDeleteData;
        }}
        public TDeleteDataNss SetupSelect_TDeleteData() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDataEditId();
            }
            doSetupSelect(delegate { return Query().QueryTDeleteData(); });
            if (_nssTDeleteData == null || !_nssTDeleteData.HasConditionQuery)
            { _nssTDeleteData = new TDeleteDataNss(Query().QueryTDeleteData()); }
            return _nssTDeleteData;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDeleteConditionCBSpecification _specification;
        public TDeleteConditionCBSpecification Specify() {
            if (_specification == null) { _specification = new TDeleteConditionCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDeleteConditionCQ> {
			protected BsTDeleteConditionCB _myCB;
			public MySpQyCall(BsTDeleteConditionCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDeleteConditionCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDeleteConditionCB> ColumnQuery(SpecifyQuery<TDeleteConditionCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDeleteConditionCB>(delegate(SpecifyQuery<TDeleteConditionCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDeleteConditionCB xcreateColumnQueryCB() {
            TDeleteConditionCB cb = new TDeleteConditionCB();
            cb.xsetupForColumnQuery((TDeleteConditionCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDeleteConditionCB> orQuery) {
            xorQ((TDeleteConditionCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDeleteConditionCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDeleteConditionCBColQySpQyCall(mainCB));
        }
    }

    public class TDeleteConditionCBColQySpQyCall : HpSpQyCall<TDeleteConditionCQ> {
        protected TDeleteConditionCB _mainCB;
        public TDeleteConditionCBColQySpQyCall(TDeleteConditionCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDeleteConditionCQ qy() { return _mainCB.Query(); } 
    }

    public class TDeleteConditionCBSpecification : AbstractSpecification<TDeleteConditionCQ> {
        protected TDeleteDataCBSpecification _tDeleteData;
        public TDeleteConditionCBSpecification(ConditionBean baseCB, HpSpQyCall<TDeleteConditionCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDeleteConditionId() { doColumn("DELETE_CONDITION_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnItemId() { doColumn("ITEM_ID"); }
        public void ColumnOperationCode() { doColumn("OPERATION_CODE"); }
        public void ColumnOperationValue() { doColumn("OPERATION_VALUE"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDeleteConditionId(); // PK
            if (qyCall().qy().hasConditionQueryTDeleteData()
                    || qyCall().qy().xgetReferrerQuery() is TDeleteDataCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DELETE_CONDITION"; }
        public TDeleteDataCBSpecification SpecifyTDeleteData() {
            assertForeign("tDeleteData");
            if (_tDeleteData == null) {
                _tDeleteData = new TDeleteDataCBSpecification(_baseCB, new TDeleteDataSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDeleteData.xsetSyncQyCall(new TDeleteDataSpQyCall(xsyncQyCall())); }
            }
            return _tDeleteData;
        }
		public class TDeleteDataSpQyCall : HpSpQyCall<TDeleteDataCQ> {
		    protected HpSpQyCall<TDeleteConditionCQ> _qyCall;
		    public TDeleteDataSpQyCall(HpSpQyCall<TDeleteConditionCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDeleteData(); }
			public TDeleteDataCQ qy() { return _qyCall.qy().QueryTDeleteData(); }
		}
    }
}
