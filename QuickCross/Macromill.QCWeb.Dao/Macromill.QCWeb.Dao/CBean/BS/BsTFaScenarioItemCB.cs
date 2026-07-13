
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
    public class BsTFaScenarioItemCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TFaScenarioItemCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? faScenarioItemId) {
            assertObjectNotNull("faScenarioItemId", faScenarioItemId);
            BsTFaScenarioItemCB cb = this;
            cb.Query().SetFaScenarioItemId_Equal(faScenarioItemId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_FaScenarioItemId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_FaScenarioItemId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TFaScenarioItemCQ Query() {
            return this.ConditionQuery;
        }

        public TFaScenarioItemCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TFaScenarioItemCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TFaScenarioItemCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TFaScenarioItemCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TFaScenarioItemCB> unionQuery) {
            TFaScenarioItemCB cb = new TFaScenarioItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TFaScenarioItemCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TFaScenarioItemCB> unionQuery) {
            TFaScenarioItemCB cb = new TFaScenarioItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TFaScenarioItemCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TFaScenarioHeaderNss _nssTFaScenarioHeader;
        public TFaScenarioHeaderNss NssTFaScenarioHeader { get {
            if (_nssTFaScenarioHeader == null) { _nssTFaScenarioHeader = new TFaScenarioHeaderNss(null); }
            return _nssTFaScenarioHeader;
        }}
        public TFaScenarioHeaderNss SetupSelect_TFaScenarioHeader() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnFaScenarioHeaderId();
            }
            doSetupSelect(delegate { return Query().QueryTFaScenarioHeader(); });
            if (_nssTFaScenarioHeader == null || !_nssTFaScenarioHeader.HasConditionQuery)
            { _nssTFaScenarioHeader = new TFaScenarioHeaderNss(Query().QueryTFaScenarioHeader()); }
            return _nssTFaScenarioHeader;
        }
        protected TItemInfoNss _nssTItemInfo;
        public TItemInfoNss NssTItemInfo { get {
            if (_nssTItemInfo == null) { _nssTItemInfo = new TItemInfoNss(null); }
            return _nssTItemInfo;
        }}
        public TItemInfoNss SetupSelect_TItemInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnFaTargetItemId();
            }
            doSetupSelect(delegate { return Query().QueryTItemInfo(); });
            if (_nssTItemInfo == null || !_nssTItemInfo.HasConditionQuery)
            { _nssTItemInfo = new TItemInfoNss(Query().QueryTItemInfo()); }
            return _nssTItemInfo;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TFaScenarioItemCBSpecification _specification;
        public TFaScenarioItemCBSpecification Specify() {
            if (_specification == null) { _specification = new TFaScenarioItemCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TFaScenarioItemCQ> {
			protected BsTFaScenarioItemCB _myCB;
			public MySpQyCall(BsTFaScenarioItemCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TFaScenarioItemCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TFaScenarioItemCB> ColumnQuery(SpecifyQuery<TFaScenarioItemCB> leftSpecifyQuery) {
            return new HpColQyOperand<TFaScenarioItemCB>(delegate(SpecifyQuery<TFaScenarioItemCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TFaScenarioItemCB xcreateColumnQueryCB() {
            TFaScenarioItemCB cb = new TFaScenarioItemCB();
            cb.xsetupForColumnQuery((TFaScenarioItemCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TFaScenarioItemCB> orQuery) {
            xorQ((TFaScenarioItemCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TFaScenarioItemCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TFaScenarioItemCBColQySpQyCall(mainCB));
        }
    }

    public class TFaScenarioItemCBColQySpQyCall : HpSpQyCall<TFaScenarioItemCQ> {
        protected TFaScenarioItemCB _mainCB;
        public TFaScenarioItemCBColQySpQyCall(TFaScenarioItemCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TFaScenarioItemCQ qy() { return _mainCB.Query(); } 
    }

    public class TFaScenarioItemCBSpecification : AbstractSpecification<TFaScenarioItemCQ> {
        protected TFaScenarioHeaderCBSpecification _tFaScenarioHeader;
        protected TItemInfoCBSpecification _tItemInfo;
        public TFaScenarioItemCBSpecification(ConditionBean baseCB, HpSpQyCall<TFaScenarioItemCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnFaScenarioItemId() { doColumn("FA_SCENARIO_ITEM_ID"); }
        public void ColumnFaScenarioHeaderId() { doColumn("FA_SCENARIO_HEADER_ID"); }
        public void ColumnFaTargetItemId() { doColumn("FA_TARGET_ITEM_ID"); }
        public void ColumnTitleString() { doColumn("TITLE_STRING"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnFaScenarioItemId(); // PK
            if (qyCall().qy().hasConditionQueryTFaScenarioHeader()
                    || qyCall().qy().xgetReferrerQuery() is TFaScenarioHeaderCQ) {
                ColumnFaScenarioHeaderId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTItemInfo()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnFaTargetItemId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_FA_SCENARIO_ITEM"; }
        public TFaScenarioHeaderCBSpecification SpecifyTFaScenarioHeader() {
            assertForeign("tFaScenarioHeader");
            if (_tFaScenarioHeader == null) {
                _tFaScenarioHeader = new TFaScenarioHeaderCBSpecification(_baseCB, new TFaScenarioHeaderSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tFaScenarioHeader.xsetSyncQyCall(new TFaScenarioHeaderSpQyCall(xsyncQyCall())); }
            }
            return _tFaScenarioHeader;
        }
		public class TFaScenarioHeaderSpQyCall : HpSpQyCall<TFaScenarioHeaderCQ> {
		    protected HpSpQyCall<TFaScenarioItemCQ> _qyCall;
		    public TFaScenarioHeaderSpQyCall(HpSpQyCall<TFaScenarioItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTFaScenarioHeader(); }
			public TFaScenarioHeaderCQ qy() { return _qyCall.qy().QueryTFaScenarioHeader(); }
		}
        public TItemInfoCBSpecification SpecifyTItemInfo() {
            assertForeign("tItemInfo");
            if (_tItemInfo == null) {
                _tItemInfo = new TItemInfoCBSpecification(_baseCB, new TItemInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tItemInfo.xsetSyncQyCall(new TItemInfoSpQyCall(xsyncQyCall())); }
            }
            return _tItemInfo;
        }
		public class TItemInfoSpQyCall : HpSpQyCall<TItemInfoCQ> {
		    protected HpSpQyCall<TFaScenarioItemCQ> _qyCall;
		    public TItemInfoSpQyCall(HpSpQyCall<TFaScenarioItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfo(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfo(); }
		}
    }
}
