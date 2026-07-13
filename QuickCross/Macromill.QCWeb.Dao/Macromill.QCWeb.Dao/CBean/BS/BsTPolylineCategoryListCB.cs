
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
    public class BsTPolylineCategoryListCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TPolylineCategoryListCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_POLYLINE_CATEGORY_LIST"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? polylineCategoryListId) {
            assertObjectNotNull("polylineCategoryListId", polylineCategoryListId);
            BsTPolylineCategoryListCB cb = this;
            cb.Query().SetPolylineCategoryListId_Equal(polylineCategoryListId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_PolylineCategoryListId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_PolylineCategoryListId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TPolylineCategoryListCQ Query() {
            return this.ConditionQuery;
        }

        public TPolylineCategoryListCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TPolylineCategoryListCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TPolylineCategoryListCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TPolylineCategoryListCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TPolylineCategoryListCB> unionQuery) {
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TPolylineCategoryListCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TPolylineCategoryListCB> unionQuery) {
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TPolylineCategoryListCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TCrossScenarioItemNss _nssTCrossScenarioItem;
        public TCrossScenarioItemNss NssTCrossScenarioItem { get {
            if (_nssTCrossScenarioItem == null) { _nssTCrossScenarioItem = new TCrossScenarioItemNss(null); }
            return _nssTCrossScenarioItem;
        }}
        public TCrossScenarioItemNss SetupSelect_TCrossScenarioItem() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnCrossScenarioItemId();
            }
            doSetupSelect(delegate { return Query().QueryTCrossScenarioItem(); });
            if (_nssTCrossScenarioItem == null || !_nssTCrossScenarioItem.HasConditionQuery)
            { _nssTCrossScenarioItem = new TCrossScenarioItemNss(Query().QueryTCrossScenarioItem()); }
            return _nssTCrossScenarioItem;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TPolylineCategoryListCBSpecification _specification;
        public TPolylineCategoryListCBSpecification Specify() {
            if (_specification == null) { _specification = new TPolylineCategoryListCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TPolylineCategoryListCQ> {
			protected BsTPolylineCategoryListCB _myCB;
			public MySpQyCall(BsTPolylineCategoryListCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TPolylineCategoryListCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TPolylineCategoryListCB> ColumnQuery(SpecifyQuery<TPolylineCategoryListCB> leftSpecifyQuery) {
            return new HpColQyOperand<TPolylineCategoryListCB>(delegate(SpecifyQuery<TPolylineCategoryListCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TPolylineCategoryListCB xcreateColumnQueryCB() {
            TPolylineCategoryListCB cb = new TPolylineCategoryListCB();
            cb.xsetupForColumnQuery((TPolylineCategoryListCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TPolylineCategoryListCB> orQuery) {
            xorQ((TPolylineCategoryListCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TPolylineCategoryListCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TPolylineCategoryListCBColQySpQyCall(mainCB));
        }
    }

    public class TPolylineCategoryListCBColQySpQyCall : HpSpQyCall<TPolylineCategoryListCQ> {
        protected TPolylineCategoryListCB _mainCB;
        public TPolylineCategoryListCBColQySpQyCall(TPolylineCategoryListCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TPolylineCategoryListCQ qy() { return _mainCB.Query(); } 
    }

    public class TPolylineCategoryListCBSpecification : AbstractSpecification<TPolylineCategoryListCQ> {
        protected TCrossScenarioItemCBSpecification _tCrossScenarioItem;
        public TPolylineCategoryListCBSpecification(ConditionBean baseCB, HpSpQyCall<TPolylineCategoryListCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnPolylineCategoryListId() { doColumn("POLYLINE_CATEGORY_LIST_ID"); }
        public void ColumnCrossScenarioItemId() { doColumn("CROSS_SCENARIO_ITEM_ID"); }
        public void ColumnAxisCategoryNo() { doColumn("AXIS_CATEGORY_NO"); }
        public void ColumnAxis2CategoryNo() { doColumn("AXIS2_CATEGORY_NO"); }
        public void ColumnArrayNoSingular() { doColumn("ARRAY_NO_SINGULAR"); }
        public void ColumnArrayNoPlural() { doColumn("ARRAY_NO_PLURAL"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnPolylineCategoryListId(); // PK
            if (qyCall().qy().hasConditionQueryTCrossScenarioItem()
                    || qyCall().qy().xgetReferrerQuery() is TCrossScenarioItemCQ) {
                ColumnCrossScenarioItemId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_POLYLINE_CATEGORY_LIST"; }
        public TCrossScenarioItemCBSpecification SpecifyTCrossScenarioItem() {
            assertForeign("tCrossScenarioItem");
            if (_tCrossScenarioItem == null) {
                _tCrossScenarioItem = new TCrossScenarioItemCBSpecification(_baseCB, new TCrossScenarioItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tCrossScenarioItem.xsetSyncQyCall(new TCrossScenarioItemSpQyCall(xsyncQyCall())); }
            }
            return _tCrossScenarioItem;
        }
		public class TCrossScenarioItemSpQyCall : HpSpQyCall<TCrossScenarioItemCQ> {
		    protected HpSpQyCall<TPolylineCategoryListCQ> _qyCall;
		    public TCrossScenarioItemSpQyCall(HpSpQyCall<TPolylineCategoryListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCrossScenarioItem(); }
			public TCrossScenarioItemCQ qy() { return _qyCall.qy().QueryTCrossScenarioItem(); }
		}
    }
}
