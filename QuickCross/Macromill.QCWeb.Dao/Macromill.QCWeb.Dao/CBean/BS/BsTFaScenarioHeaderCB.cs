
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
    public class BsTFaScenarioHeaderCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TFaScenarioHeaderCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_SCENARIO_HEADER"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? faScenarioHeaderId) {
            assertObjectNotNull("faScenarioHeaderId", faScenarioHeaderId);
            BsTFaScenarioHeaderCB cb = this;
            cb.Query().SetFaScenarioHeaderId_Equal(faScenarioHeaderId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_FaScenarioHeaderId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_FaScenarioHeaderId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TFaScenarioHeaderCQ Query() {
            return this.ConditionQuery;
        }

        public TFaScenarioHeaderCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TFaScenarioHeaderCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TFaScenarioHeaderCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TFaScenarioHeaderCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TFaScenarioHeaderCB> unionQuery) {
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TFaScenarioHeaderCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TFaScenarioHeaderCB> unionQuery) {
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TFaScenarioHeaderCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TScenarioTotalizationNss _nssTScenarioTotalization;
        public TScenarioTotalizationNss NssTScenarioTotalization { get {
            if (_nssTScenarioTotalization == null) { _nssTScenarioTotalization = new TScenarioTotalizationNss(null); }
            return _nssTScenarioTotalization;
        }}
        public TScenarioTotalizationNss SetupSelect_TScenarioTotalization() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnScenarioTotalizationId();
            }
            doSetupSelect(delegate { return Query().QueryTScenarioTotalization(); });
            if (_nssTScenarioTotalization == null || !_nssTScenarioTotalization.HasConditionQuery)
            { _nssTScenarioTotalization = new TScenarioTotalizationNss(Query().QueryTScenarioTotalization()); }
            return _nssTScenarioTotalization;
        }
        protected TFaScenarioItemNss _nssTFaScenarioItem;
        public TFaScenarioItemNss NssTFaScenarioItem { get {
            if (_nssTFaScenarioItem == null) { _nssTFaScenarioItem = new TFaScenarioItemNss(null); }
            return _nssTFaScenarioItem;
        }}
        public TFaScenarioItemNss SetupSelect_TFaScenarioItem() {
            doSetupSelect(delegate { return Query().QueryTFaScenarioItem(); });
            if (_nssTFaScenarioItem == null || !_nssTFaScenarioItem.HasConditionQuery)
            { _nssTFaScenarioItem = new TFaScenarioItemNss(Query().QueryTFaScenarioItem()); }
            return _nssTFaScenarioItem;
        }
        protected TFaListAddItemNss _nssTFaListAddItem;
        public TFaListAddItemNss NssTFaListAddItem { get {
            if (_nssTFaListAddItem == null) { _nssTFaListAddItem = new TFaListAddItemNss(null); }
            return _nssTFaListAddItem;
        }}
        public TFaListAddItemNss SetupSelect_TFaListAddItem() {
            doSetupSelect(delegate { return Query().QueryTFaListAddItem(); });
            if (_nssTFaListAddItem == null || !_nssTFaListAddItem.HasConditionQuery)
            { _nssTFaListAddItem = new TFaListAddItemNss(Query().QueryTFaListAddItem()); }
            return _nssTFaListAddItem;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TFaScenarioHeaderCBSpecification _specification;
        public TFaScenarioHeaderCBSpecification Specify() {
            if (_specification == null) { _specification = new TFaScenarioHeaderCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TFaScenarioHeaderCQ> {
			protected BsTFaScenarioHeaderCB _myCB;
			public MySpQyCall(BsTFaScenarioHeaderCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TFaScenarioHeaderCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TFaScenarioHeaderCB> ColumnQuery(SpecifyQuery<TFaScenarioHeaderCB> leftSpecifyQuery) {
            return new HpColQyOperand<TFaScenarioHeaderCB>(delegate(SpecifyQuery<TFaScenarioHeaderCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TFaScenarioHeaderCB xcreateColumnQueryCB() {
            TFaScenarioHeaderCB cb = new TFaScenarioHeaderCB();
            cb.xsetupForColumnQuery((TFaScenarioHeaderCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TFaScenarioHeaderCB> orQuery) {
            xorQ((TFaScenarioHeaderCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TFaScenarioHeaderCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TFaScenarioHeaderCBColQySpQyCall(mainCB));
        }
    }

    public class TFaScenarioHeaderCBColQySpQyCall : HpSpQyCall<TFaScenarioHeaderCQ> {
        protected TFaScenarioHeaderCB _mainCB;
        public TFaScenarioHeaderCBColQySpQyCall(TFaScenarioHeaderCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TFaScenarioHeaderCQ qy() { return _mainCB.Query(); } 
    }

    public class TFaScenarioHeaderCBSpecification : AbstractSpecification<TFaScenarioHeaderCQ> {
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TFaScenarioItemCBSpecification _tFaScenarioItem;
        protected TFaListAddItemCBSpecification _tFaListAddItem;
        public TFaScenarioHeaderCBSpecification(ConditionBean baseCB, HpSpQyCall<TFaScenarioHeaderCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnFaScenarioHeaderId() { doColumn("FA_SCENARIO_HEADER_ID"); }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnScenarioComment() { doColumn("SCENARIO_COMMENT"); }
        public void ColumnViewName() { doColumn("VIEW_NAME"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnFaScenarioHeaderId(); // PK
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnScenarioTotalizationId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_FA_SCENARIO_HEADER"; }
        public TScenarioTotalizationCBSpecification SpecifyTScenarioTotalization() {
            assertForeign("tScenarioTotalization");
            if (_tScenarioTotalization == null) {
                _tScenarioTotalization = new TScenarioTotalizationCBSpecification(_baseCB, new TScenarioTotalizationSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tScenarioTotalization.xsetSyncQyCall(new TScenarioTotalizationSpQyCall(xsyncQyCall())); }
            }
            return _tScenarioTotalization;
        }
		public class TScenarioTotalizationSpQyCall : HpSpQyCall<TScenarioTotalizationCQ> {
		    protected HpSpQyCall<TFaScenarioHeaderCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TFaScenarioHeaderCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
        public TFaScenarioItemCBSpecification SpecifyTFaScenarioItem() {
            assertForeign("tFaScenarioItem");
            if (_tFaScenarioItem == null) {
                _tFaScenarioItem = new TFaScenarioItemCBSpecification(_baseCB, new TFaScenarioItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tFaScenarioItem.xsetSyncQyCall(new TFaScenarioItemSpQyCall(xsyncQyCall())); }
            }
            return _tFaScenarioItem;
        }
		public class TFaScenarioItemSpQyCall : HpSpQyCall<TFaScenarioItemCQ> {
		    protected HpSpQyCall<TFaScenarioHeaderCQ> _qyCall;
		    public TFaScenarioItemSpQyCall(HpSpQyCall<TFaScenarioHeaderCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTFaScenarioItem(); }
			public TFaScenarioItemCQ qy() { return _qyCall.qy().QueryTFaScenarioItem(); }
		}
        public TFaListAddItemCBSpecification SpecifyTFaListAddItem() {
            assertForeign("tFaListAddItem");
            if (_tFaListAddItem == null) {
                _tFaListAddItem = new TFaListAddItemCBSpecification(_baseCB, new TFaListAddItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tFaListAddItem.xsetSyncQyCall(new TFaListAddItemSpQyCall(xsyncQyCall())); }
            }
            return _tFaListAddItem;
        }
		public class TFaListAddItemSpQyCall : HpSpQyCall<TFaListAddItemCQ> {
		    protected HpSpQyCall<TFaScenarioHeaderCQ> _qyCall;
		    public TFaListAddItemSpQyCall(HpSpQyCall<TFaScenarioHeaderCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTFaListAddItem(); }
			public TFaListAddItemCQ qy() { return _qyCall.qy().QueryTFaListAddItem(); }
		}
        public RAFunction<TFaListAddItemCB, TFaScenarioHeaderCQ> DerivedTFaListAddItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TFaListAddItemCB, TFaScenarioHeaderCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TFaListAddItemCB> subQuery, TFaScenarioHeaderCQ cq, String aliasName)
                { cq.xsderiveTFaListAddItemList(function, subQuery, aliasName); });
        }
        public RAFunction<TFaScenarioItemCB, TFaScenarioHeaderCQ> DerivedTFaScenarioItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TFaScenarioItemCB, TFaScenarioHeaderCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TFaScenarioItemCB> subQuery, TFaScenarioHeaderCQ cq, String aliasName)
                { cq.xsderiveTFaScenarioItemList(function, subQuery, aliasName); });
        }
    }
}
