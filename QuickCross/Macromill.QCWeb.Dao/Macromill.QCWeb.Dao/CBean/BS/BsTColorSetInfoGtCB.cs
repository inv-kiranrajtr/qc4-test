
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
    public class BsTColorSetInfoGtCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorSetInfoGtCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_SET_INFO_GT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? colorSetInfoGtId) {
            assertObjectNotNull("colorSetInfoGtId", colorSetInfoGtId);
            BsTColorSetInfoGtCB cb = this;
            cb.Query().SetColorSetInfoGtId_Equal(colorSetInfoGtId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ColorSetInfoGtId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ColorSetInfoGtId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TColorSetInfoGtCQ Query() {
            return this.ConditionQuery;
        }

        public TColorSetInfoGtCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TColorSetInfoGtCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TColorSetInfoGtCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TColorSetInfoGtCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TColorSetInfoGtCB> unionQuery) {
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorSetInfoGtCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TColorSetInfoGtCB> unionQuery) {
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorSetInfoGtCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TGtScenarioItemNss _nssTGtScenarioItem;
        public TGtScenarioItemNss NssTGtScenarioItem { get {
            if (_nssTGtScenarioItem == null) { _nssTGtScenarioItem = new TGtScenarioItemNss(null); }
            return _nssTGtScenarioItem;
        }}
        public TGtScenarioItemNss SetupSelect_TGtScenarioItem() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnGtScenarioItemId();
            }
            doSetupSelect(delegate { return Query().QueryTGtScenarioItem(); });
            if (_nssTGtScenarioItem == null || !_nssTGtScenarioItem.HasConditionQuery)
            { _nssTGtScenarioItem = new TGtScenarioItemNss(Query().QueryTGtScenarioItem()); }
            return _nssTGtScenarioItem;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TColorSetInfoGtCBSpecification _specification;
        public TColorSetInfoGtCBSpecification Specify() {
            if (_specification == null) { _specification = new TColorSetInfoGtCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TColorSetInfoGtCQ> {
			protected BsTColorSetInfoGtCB _myCB;
			public MySpQyCall(BsTColorSetInfoGtCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TColorSetInfoGtCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TColorSetInfoGtCB> ColumnQuery(SpecifyQuery<TColorSetInfoGtCB> leftSpecifyQuery) {
            return new HpColQyOperand<TColorSetInfoGtCB>(delegate(SpecifyQuery<TColorSetInfoGtCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TColorSetInfoGtCB xcreateColumnQueryCB() {
            TColorSetInfoGtCB cb = new TColorSetInfoGtCB();
            cb.xsetupForColumnQuery((TColorSetInfoGtCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TColorSetInfoGtCB> orQuery) {
            xorQ((TColorSetInfoGtCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TColorSetInfoGtCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TColorSetInfoGtCBColQySpQyCall(mainCB));
        }
    }

    public class TColorSetInfoGtCBColQySpQyCall : HpSpQyCall<TColorSetInfoGtCQ> {
        protected TColorSetInfoGtCB _mainCB;
        public TColorSetInfoGtCBColQySpQyCall(TColorSetInfoGtCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TColorSetInfoGtCQ qy() { return _mainCB.Query(); } 
    }

    public class TColorSetInfoGtCBSpecification : AbstractSpecification<TColorSetInfoGtCQ> {
        protected TGtScenarioItemCBSpecification _tGtScenarioItem;
        public TColorSetInfoGtCBSpecification(ConditionBean baseCB, HpSpQyCall<TColorSetInfoGtCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnColorSetInfoGtId() { doColumn("COLOR_SET_INFO_GT_ID"); }
        public void ColumnTypeCode() { doColumn("TYPE_CODE"); }
        public void ColumnGradationType() { doColumn("GRADATION_TYPE"); }
        public void ColumnGtScenarioItemId() { doColumn("GT_SCENARIO_ITEM_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnColorSetInfoGtId(); // PK
            if (qyCall().qy().hasConditionQueryTGtScenarioItem()
                    || qyCall().qy().xgetReferrerQuery() is TGtScenarioItemCQ) {
                ColumnGtScenarioItemId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_COLOR_SET_INFO_GT"; }
        public TGtScenarioItemCBSpecification SpecifyTGtScenarioItem() {
            assertForeign("tGtScenarioItem");
            if (_tGtScenarioItem == null) {
                _tGtScenarioItem = new TGtScenarioItemCBSpecification(_baseCB, new TGtScenarioItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tGtScenarioItem.xsetSyncQyCall(new TGtScenarioItemSpQyCall(xsyncQyCall())); }
            }
            return _tGtScenarioItem;
        }
		public class TGtScenarioItemSpQyCall : HpSpQyCall<TGtScenarioItemCQ> {
		    protected HpSpQyCall<TColorSetInfoGtCQ> _qyCall;
		    public TGtScenarioItemSpQyCall(HpSpQyCall<TColorSetInfoGtCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTGtScenarioItem(); }
			public TGtScenarioItemCQ qy() { return _qyCall.qy().QueryTGtScenarioItem(); }
		}
        public RAFunction<TColorInfoDetailGtCB, TColorSetInfoGtCQ> DerivedTColorInfoDetailGtList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TColorInfoDetailGtCB, TColorSetInfoGtCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TColorInfoDetailGtCB> subQuery, TColorSetInfoGtCQ cq, String aliasName)
                { cq.xsderiveTColorInfoDetailGtList(function, subQuery, aliasName); });
        }
    }
}
