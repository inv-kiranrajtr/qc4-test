
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
    public class BsTGtMatrixInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TGtMatrixInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_MATRIX_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? gtMatrixInfoId) {
            assertObjectNotNull("gtMatrixInfoId", gtMatrixInfoId);
            BsTGtMatrixInfoCB cb = this;
            cb.Query().SetGtMatrixInfoId_Equal(gtMatrixInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_GtMatrixInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_GtMatrixInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TGtMatrixInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TGtMatrixInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TGtMatrixInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TGtMatrixInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TGtMatrixInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TGtMatrixInfoCB> unionQuery) {
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TGtMatrixInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TGtMatrixInfoCB> unionQuery) {
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TGtMatrixInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TGtMatrixChildNss _nssTGtMatrixChild;
        public TGtMatrixChildNss NssTGtMatrixChild { get {
            if (_nssTGtMatrixChild == null) { _nssTGtMatrixChild = new TGtMatrixChildNss(null); }
            return _nssTGtMatrixChild;
        }}
        public TGtMatrixChildNss SetupSelect_TGtMatrixChild() {
            doSetupSelect(delegate { return Query().QueryTGtMatrixChild(); });
            if (_nssTGtMatrixChild == null || !_nssTGtMatrixChild.HasConditionQuery)
            { _nssTGtMatrixChild = new TGtMatrixChildNss(Query().QueryTGtMatrixChild()); }
            return _nssTGtMatrixChild;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TGtMatrixInfoCBSpecification _specification;
        public TGtMatrixInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TGtMatrixInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TGtMatrixInfoCQ> {
			protected BsTGtMatrixInfoCB _myCB;
			public MySpQyCall(BsTGtMatrixInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TGtMatrixInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TGtMatrixInfoCB> ColumnQuery(SpecifyQuery<TGtMatrixInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TGtMatrixInfoCB>(delegate(SpecifyQuery<TGtMatrixInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TGtMatrixInfoCB xcreateColumnQueryCB() {
            TGtMatrixInfoCB cb = new TGtMatrixInfoCB();
            cb.xsetupForColumnQuery((TGtMatrixInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TGtMatrixInfoCB> orQuery) {
            xorQ((TGtMatrixInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TGtMatrixInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TGtMatrixInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TGtMatrixInfoCBColQySpQyCall : HpSpQyCall<TGtMatrixInfoCQ> {
        protected TGtMatrixInfoCB _mainCB;
        public TGtMatrixInfoCBColQySpQyCall(TGtMatrixInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TGtMatrixInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TGtMatrixInfoCBSpecification : AbstractSpecification<TGtMatrixInfoCQ> {
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TGtMatrixChildCBSpecification _tGtMatrixChild;
        public TGtMatrixInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TGtMatrixInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnGtMatrixInfoId() { doColumn("GT_MATRIX_INFO_ID"); }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnBaseItemId() { doColumn("BASE_ITEM_ID"); }
        public void ColumnNewItemId() { doColumn("NEW_ITEM_ID"); }
        public void ColumnTotalizationType() { doColumn("TOTALIZATION_TYPE"); }
        public void ColumnLv1title() { doColumn("LV1TITLE"); }
        public void ColumnItemName() { doColumn("ITEM_NAME"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnGtMatrixInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnScenarioTotalizationId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_GT_MATRIX_INFO"; }
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
		    protected HpSpQyCall<TGtMatrixInfoCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TGtMatrixInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
        public TGtMatrixChildCBSpecification SpecifyTGtMatrixChild() {
            assertForeign("tGtMatrixChild");
            if (_tGtMatrixChild == null) {
                _tGtMatrixChild = new TGtMatrixChildCBSpecification(_baseCB, new TGtMatrixChildSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tGtMatrixChild.xsetSyncQyCall(new TGtMatrixChildSpQyCall(xsyncQyCall())); }
            }
            return _tGtMatrixChild;
        }
		public class TGtMatrixChildSpQyCall : HpSpQyCall<TGtMatrixChildCQ> {
		    protected HpSpQyCall<TGtMatrixInfoCQ> _qyCall;
		    public TGtMatrixChildSpQyCall(HpSpQyCall<TGtMatrixInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTGtMatrixChild(); }
			public TGtMatrixChildCQ qy() { return _qyCall.qy().QueryTGtMatrixChild(); }
		}
        public RAFunction<TGtMatrixChildCB, TGtMatrixInfoCQ> DerivedTGtMatrixChildList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TGtMatrixChildCB, TGtMatrixInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TGtMatrixChildCB> subQuery, TGtMatrixInfoCQ cq, String aliasName)
                { cq.xsderiveTGtMatrixChildList(function, subQuery, aliasName); });
        }
    }
}
