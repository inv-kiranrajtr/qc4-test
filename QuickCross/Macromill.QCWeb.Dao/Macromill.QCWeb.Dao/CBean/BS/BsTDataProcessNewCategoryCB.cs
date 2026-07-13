
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
    public class BsTDataProcessNewCategoryCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataProcessNewCategoryCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_CATEGORY"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? dataProcessNewCategoryId) {
            assertObjectNotNull("dataProcessNewCategoryId", dataProcessNewCategoryId);
            BsTDataProcessNewCategoryCB cb = this;
            cb.Query().SetDataProcessNewCategoryId_Equal(dataProcessNewCategoryId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DataProcessNewCategoryId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DataProcessNewCategoryId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDataProcessNewCategoryCQ Query() {
            return this.ConditionQuery;
        }

        public TDataProcessNewCategoryCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDataProcessNewCategoryCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDataProcessNewCategoryCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDataProcessNewCategoryCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDataProcessNewCategoryCB> unionQuery) {
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDataProcessNewCategoryCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDataProcessNewCategoryCB> unionQuery) {
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDataProcessNewCategoryCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDataProcessNewCategoryCBSpecification _specification;
        public TDataProcessNewCategoryCBSpecification Specify() {
            if (_specification == null) { _specification = new TDataProcessNewCategoryCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDataProcessNewCategoryCQ> {
			protected BsTDataProcessNewCategoryCB _myCB;
			public MySpQyCall(BsTDataProcessNewCategoryCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDataProcessNewCategoryCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDataProcessNewCategoryCB> ColumnQuery(SpecifyQuery<TDataProcessNewCategoryCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDataProcessNewCategoryCB>(delegate(SpecifyQuery<TDataProcessNewCategoryCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDataProcessNewCategoryCB xcreateColumnQueryCB() {
            TDataProcessNewCategoryCB cb = new TDataProcessNewCategoryCB();
            cb.xsetupForColumnQuery((TDataProcessNewCategoryCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDataProcessNewCategoryCB> orQuery) {
            xorQ((TDataProcessNewCategoryCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDataProcessNewCategoryCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDataProcessNewCategoryCBColQySpQyCall(mainCB));
        }
    }

    public class TDataProcessNewCategoryCBColQySpQyCall : HpSpQyCall<TDataProcessNewCategoryCQ> {
        protected TDataProcessNewCategoryCB _mainCB;
        public TDataProcessNewCategoryCBColQySpQyCall(TDataProcessNewCategoryCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDataProcessNewCategoryCQ qy() { return _mainCB.Query(); } 
    }

    public class TDataProcessNewCategoryCBSpecification : AbstractSpecification<TDataProcessNewCategoryCQ> {
        protected TDataProcessNewItemCBSpecification _tDataProcessNewItem;
        public TDataProcessNewCategoryCBSpecification(ConditionBean baseCB, HpSpQyCall<TDataProcessNewCategoryCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDataProcessNewCategoryId() { doColumn("DATA_PROCESS_NEW_CATEGORY_ID"); }
        public void ColumnNewCategoryNo() { doColumn("NEW_CATEGORY_NO"); }
        public void ColumnNewCategoryName() { doColumn("NEW_CATEGORY_NAME"); }
        public void ColumnSrcItemId() { doColumn("SRC_ITEM_ID"); }
        public void ColumnOperationCode() { doColumn("OPERATION_CODE"); }
        public void ColumnConditionString() { doColumn("CONDITION_STRING"); }
        public void ColumnBottomValue() { doColumn("BOTTOM_VALUE"); }
        public void ColumnUpperValue() { doColumn("UPPER_VALUE"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDataProcessNewCategoryId(); // PK
            if (qyCall().qy().hasConditionQueryTDataProcessNewItem()
                    || qyCall().qy().xgetReferrerQuery() is TDataProcessNewItemCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DATA_PROCESS_NEW_CATEGORY"; }
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
		    protected HpSpQyCall<TDataProcessNewCategoryCQ> _qyCall;
		    public TDataProcessNewItemSpQyCall(HpSpQyCall<TDataProcessNewCategoryCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataProcessNewItem(); }
			public TDataProcessNewItemCQ qy() { return _qyCall.qy().QueryTDataProcessNewItem(); }
		}
    }
}
