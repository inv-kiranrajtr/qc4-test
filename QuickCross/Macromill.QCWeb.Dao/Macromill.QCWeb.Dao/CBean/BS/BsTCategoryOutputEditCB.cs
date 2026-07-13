
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
    public class BsTCategoryOutputEditCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCategoryOutputEditCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_OUTPUT_EDIT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? categoryOutputEditId) {
            assertObjectNotNull("categoryOutputEditId", categoryOutputEditId);
            BsTCategoryOutputEditCB cb = this;
            cb.Query().SetCategoryOutputEditId_Equal(categoryOutputEditId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_CategoryOutputEditId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_CategoryOutputEditId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TCategoryOutputEditCQ Query() {
            return this.ConditionQuery;
        }

        public TCategoryOutputEditCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TCategoryOutputEditCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TCategoryOutputEditCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TCategoryOutputEditCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TCategoryOutputEditCB> unionQuery) {
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCategoryOutputEditCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TCategoryOutputEditCB> unionQuery) {
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCategoryOutputEditCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TCategoryOutputDetailNss _nssTCategoryOutputDetail;
        public TCategoryOutputDetailNss NssTCategoryOutputDetail { get {
            if (_nssTCategoryOutputDetail == null) { _nssTCategoryOutputDetail = new TCategoryOutputDetailNss(null); }
            return _nssTCategoryOutputDetail;
        }}
        public TCategoryOutputDetailNss SetupSelect_TCategoryOutputDetail() {
            doSetupSelect(delegate { return Query().QueryTCategoryOutputDetail(); });
            if (_nssTCategoryOutputDetail == null || !_nssTCategoryOutputDetail.HasConditionQuery)
            { _nssTCategoryOutputDetail = new TCategoryOutputDetailNss(Query().QueryTCategoryOutputDetail()); }
            return _nssTCategoryOutputDetail;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TCategoryOutputEditCBSpecification _specification;
        public TCategoryOutputEditCBSpecification Specify() {
            if (_specification == null) { _specification = new TCategoryOutputEditCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TCategoryOutputEditCQ> {
			protected BsTCategoryOutputEditCB _myCB;
			public MySpQyCall(BsTCategoryOutputEditCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TCategoryOutputEditCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TCategoryOutputEditCB> ColumnQuery(SpecifyQuery<TCategoryOutputEditCB> leftSpecifyQuery) {
            return new HpColQyOperand<TCategoryOutputEditCB>(delegate(SpecifyQuery<TCategoryOutputEditCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TCategoryOutputEditCB xcreateColumnQueryCB() {
            TCategoryOutputEditCB cb = new TCategoryOutputEditCB();
            cb.xsetupForColumnQuery((TCategoryOutputEditCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TCategoryOutputEditCB> orQuery) {
            xorQ((TCategoryOutputEditCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TCategoryOutputEditCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TCategoryOutputEditCBColQySpQyCall(mainCB));
        }
    }

    public class TCategoryOutputEditCBColQySpQyCall : HpSpQyCall<TCategoryOutputEditCQ> {
        protected TCategoryOutputEditCB _mainCB;
        public TCategoryOutputEditCBColQySpQyCall(TCategoryOutputEditCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TCategoryOutputEditCQ qy() { return _mainCB.Query(); } 
    }

    public class TCategoryOutputEditCBSpecification : AbstractSpecification<TCategoryOutputEditCQ> {
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TCategoryOutputDetailCBSpecification _tCategoryOutputDetail;
        public TCategoryOutputEditCBSpecification(ConditionBean baseCB, HpSpQyCall<TCategoryOutputEditCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnCategoryOutputEditId() { doColumn("CATEGORY_OUTPUT_EDIT_ID"); }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnOldItemId() { doColumn("OLD_ITEM_ID"); }
        public void ColumnNewItemId() { doColumn("NEW_ITEM_ID"); }
        public void ColumnTopFlag() { doColumn("TOP_FLAG"); }
        public void ColumnTopCount() { doColumn("TOP_COUNT"); }
        public void ColumnTopName() { doColumn("TOP_NAME"); }
        public void ColumnBottomFlag() { doColumn("BOTTOM_FLAG"); }
        public void ColumnBottomCount() { doColumn("BOTTOM_COUNT"); }
        public void ColumnBottomName() { doColumn("BOTTOM_NAME"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnCategoryOutputEditId(); // PK
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnScenarioTotalizationId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_CATEGORY_OUTPUT_EDIT"; }
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
		    protected HpSpQyCall<TCategoryOutputEditCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TCategoryOutputEditCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
        public TCategoryOutputDetailCBSpecification SpecifyTCategoryOutputDetail() {
            assertForeign("tCategoryOutputDetail");
            if (_tCategoryOutputDetail == null) {
                _tCategoryOutputDetail = new TCategoryOutputDetailCBSpecification(_baseCB, new TCategoryOutputDetailSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tCategoryOutputDetail.xsetSyncQyCall(new TCategoryOutputDetailSpQyCall(xsyncQyCall())); }
            }
            return _tCategoryOutputDetail;
        }
		public class TCategoryOutputDetailSpQyCall : HpSpQyCall<TCategoryOutputDetailCQ> {
		    protected HpSpQyCall<TCategoryOutputEditCQ> _qyCall;
		    public TCategoryOutputDetailSpQyCall(HpSpQyCall<TCategoryOutputEditCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCategoryOutputDetail(); }
			public TCategoryOutputDetailCQ qy() { return _qyCall.qy().QueryTCategoryOutputDetail(); }
		}
        public RAFunction<TCategoryOutputDetailCB, TCategoryOutputEditCQ> DerivedTCategoryOutputDetailList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TCategoryOutputDetailCB, TCategoryOutputEditCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TCategoryOutputDetailCB> subQuery, TCategoryOutputEditCQ cq, String aliasName)
                { cq.xsderiveTCategoryOutputDetailList(function, subQuery, aliasName); });
        }
    }
}
