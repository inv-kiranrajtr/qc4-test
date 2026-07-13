
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
    public class BsTIntegConditionCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TIntegConditionCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_INTEG_CONDITION"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? integConditionId) {
            assertObjectNotNull("integConditionId", integConditionId);
            BsTIntegConditionCB cb = this;
            cb.Query().SetIntegConditionId_Equal(integConditionId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_IntegConditionId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_IntegConditionId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TIntegConditionCQ Query() {
            return this.ConditionQuery;
        }

        public TIntegConditionCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TIntegConditionCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TIntegConditionCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TIntegConditionCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TIntegConditionCB> unionQuery) {
            TIntegConditionCB cb = new TIntegConditionCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TIntegConditionCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TIntegConditionCB> unionQuery) {
            TIntegConditionCB cb = new TIntegConditionCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TIntegConditionCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDataProcessNewItemNss _nssTDataProcessNewItem;
        public TDataProcessNewItemNss NssTDataProcessNewItem { get {
            if (_nssTDataProcessNewItem == null) { _nssTDataProcessNewItem = new TDataProcessNewItemNss(null); }
            return _nssTDataProcessNewItem;
        }}
        public TDataProcessNewItemNss SetupSelect_TDataProcessNewItem() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDataEditId();
            }
            doSetupSelect(delegate { return Query().QueryTDataProcessNewItem(); });
            if (_nssTDataProcessNewItem == null || !_nssTDataProcessNewItem.HasConditionQuery)
            { _nssTDataProcessNewItem = new TDataProcessNewItemNss(Query().QueryTDataProcessNewItem()); }
            return _nssTDataProcessNewItem;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TIntegConditionCBSpecification _specification;
        public TIntegConditionCBSpecification Specify() {
            if (_specification == null) { _specification = new TIntegConditionCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TIntegConditionCQ> {
			protected BsTIntegConditionCB _myCB;
			public MySpQyCall(BsTIntegConditionCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TIntegConditionCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TIntegConditionCB> ColumnQuery(SpecifyQuery<TIntegConditionCB> leftSpecifyQuery) {
            return new HpColQyOperand<TIntegConditionCB>(delegate(SpecifyQuery<TIntegConditionCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TIntegConditionCB xcreateColumnQueryCB() {
            TIntegConditionCB cb = new TIntegConditionCB();
            cb.xsetupForColumnQuery((TIntegConditionCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TIntegConditionCB> orQuery) {
            xorQ((TIntegConditionCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TIntegConditionCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TIntegConditionCBColQySpQyCall(mainCB));
        }
    }

    public class TIntegConditionCBColQySpQyCall : HpSpQyCall<TIntegConditionCQ> {
        protected TIntegConditionCB _mainCB;
        public TIntegConditionCBColQySpQyCall(TIntegConditionCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TIntegConditionCQ qy() { return _mainCB.Query(); } 
    }

    public class TIntegConditionCBSpecification : AbstractSpecification<TIntegConditionCQ> {
        protected TDataProcessNewItemCBSpecification _tDataProcessNewItem;
        public TIntegConditionCBSpecification(ConditionBean baseCB, HpSpQyCall<TIntegConditionCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnIntegConditionId() { doColumn("INTEG_CONDITION_ID"); }
        public void ColumnConditionNo() { doColumn("CONDITION_NO"); }
        public void ColumnSrcItemId() { doColumn("SRC_ITEM_ID"); }
        public void ColumnSourceItemNo() { doColumn("SOURCE_ITEM_NO"); }
        public void ColumnOperationCode() { doColumn("OPERATION_CODE"); }
        public void ColumnConditionString() { doColumn("CONDITION_STRING"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnIntegConditionId(); // PK
            if (qyCall().qy().hasConditionQueryTDataProcessNewItem()
                    || qyCall().qy().xgetReferrerQuery() is TDataProcessNewItemCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_INTEG_CONDITION"; }
        public TDataProcessNewItemCBSpecification SpecifyTDataProcessNewItem() {
            assertForeign("tDataProcessNewItem");
            if (_tDataProcessNewItem == null) {
                _tDataProcessNewItem = new TDataProcessNewItemCBSpecification(_baseCB, new TDataProcessNewItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDataProcessNewItem.xsetSyncQyCall(new TDataProcessNewItemSpQyCall(xsyncQyCall())); }
            }
            return _tDataProcessNewItem;
        }
		public class TDataProcessNewItemSpQyCall : HpSpQyCall<TDataProcessNewItemCQ> {
		    protected HpSpQyCall<TIntegConditionCQ> _qyCall;
		    public TDataProcessNewItemSpQyCall(HpSpQyCall<TIntegConditionCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataProcessNewItem(); }
			public TDataProcessNewItemCQ qy() { return _qyCall.qy().QueryTDataProcessNewItem(); }
		}
    }
}
