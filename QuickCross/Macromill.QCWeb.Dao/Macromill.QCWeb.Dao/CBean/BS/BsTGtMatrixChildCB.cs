
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
    public class BsTGtMatrixChildCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TGtMatrixChildCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_MATRIX_CHILD"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? gtMatrixChildid) {
            assertObjectNotNull("gtMatrixChildid", gtMatrixChildid);
            BsTGtMatrixChildCB cb = this;
            cb.Query().SetGtMatrixChildid_Equal(gtMatrixChildid);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_GtMatrixChildid_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_GtMatrixChildid_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TGtMatrixChildCQ Query() {
            return this.ConditionQuery;
        }

        public TGtMatrixChildCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TGtMatrixChildCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TGtMatrixChildCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TGtMatrixChildCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TGtMatrixChildCB> unionQuery) {
            TGtMatrixChildCB cb = new TGtMatrixChildCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TGtMatrixChildCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TGtMatrixChildCB> unionQuery) {
            TGtMatrixChildCB cb = new TGtMatrixChildCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TGtMatrixChildCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TGtMatrixInfoNss _nssTGtMatrixInfo;
        public TGtMatrixInfoNss NssTGtMatrixInfo { get {
            if (_nssTGtMatrixInfo == null) { _nssTGtMatrixInfo = new TGtMatrixInfoNss(null); }
            return _nssTGtMatrixInfo;
        }}
        public TGtMatrixInfoNss SetupSelect_TGtMatrixInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnGtMatrixInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTGtMatrixInfo(); });
            if (_nssTGtMatrixInfo == null || !_nssTGtMatrixInfo.HasConditionQuery)
            { _nssTGtMatrixInfo = new TGtMatrixInfoNss(Query().QueryTGtMatrixInfo()); }
            return _nssTGtMatrixInfo;
        }
        protected TItemInfoNss _nssTItemInfo;
        public TItemInfoNss NssTItemInfo { get {
            if (_nssTItemInfo == null) { _nssTItemInfo = new TItemInfoNss(null); }
            return _nssTItemInfo;
        }}
        public TItemInfoNss SetupSelect_TItemInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnChildItemId();
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
        protected TGtMatrixChildCBSpecification _specification;
        public TGtMatrixChildCBSpecification Specify() {
            if (_specification == null) { _specification = new TGtMatrixChildCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TGtMatrixChildCQ> {
			protected BsTGtMatrixChildCB _myCB;
			public MySpQyCall(BsTGtMatrixChildCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TGtMatrixChildCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TGtMatrixChildCB> ColumnQuery(SpecifyQuery<TGtMatrixChildCB> leftSpecifyQuery) {
            return new HpColQyOperand<TGtMatrixChildCB>(delegate(SpecifyQuery<TGtMatrixChildCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TGtMatrixChildCB xcreateColumnQueryCB() {
            TGtMatrixChildCB cb = new TGtMatrixChildCB();
            cb.xsetupForColumnQuery((TGtMatrixChildCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TGtMatrixChildCB> orQuery) {
            xorQ((TGtMatrixChildCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TGtMatrixChildCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TGtMatrixChildCBColQySpQyCall(mainCB));
        }
    }

    public class TGtMatrixChildCBColQySpQyCall : HpSpQyCall<TGtMatrixChildCQ> {
        protected TGtMatrixChildCB _mainCB;
        public TGtMatrixChildCBColQySpQyCall(TGtMatrixChildCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TGtMatrixChildCQ qy() { return _mainCB.Query(); } 
    }

    public class TGtMatrixChildCBSpecification : AbstractSpecification<TGtMatrixChildCQ> {
        protected TGtMatrixInfoCBSpecification _tGtMatrixInfo;
        protected TItemInfoCBSpecification _tItemInfo;
        public TGtMatrixChildCBSpecification(ConditionBean baseCB, HpSpQyCall<TGtMatrixChildCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnGtMatrixChildid() { doColumn("GT_MATRIX_CHILDID"); }
        public void ColumnGtMatrixInfoId() { doColumn("GT_MATRIX_INFO_ID"); }
        public void ColumnChildItemId() { doColumn("CHILD_ITEM_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnGtMatrixChildid(); // PK
            if (qyCall().qy().hasConditionQueryTGtMatrixInfo()
                    || qyCall().qy().xgetReferrerQuery() is TGtMatrixInfoCQ) {
                ColumnGtMatrixInfoId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTItemInfo()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnChildItemId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_GT_MATRIX_CHILD"; }
        public TGtMatrixInfoCBSpecification SpecifyTGtMatrixInfo() {
            assertForeign("tGtMatrixInfo");
            if (_tGtMatrixInfo == null) {
                _tGtMatrixInfo = new TGtMatrixInfoCBSpecification(_baseCB, new TGtMatrixInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tGtMatrixInfo.xsetSyncQyCall(new TGtMatrixInfoSpQyCall(xsyncQyCall())); }
            }
            return _tGtMatrixInfo;
        }
		public class TGtMatrixInfoSpQyCall : HpSpQyCall<TGtMatrixInfoCQ> {
		    protected HpSpQyCall<TGtMatrixChildCQ> _qyCall;
		    public TGtMatrixInfoSpQyCall(HpSpQyCall<TGtMatrixChildCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTGtMatrixInfo(); }
			public TGtMatrixInfoCQ qy() { return _qyCall.qy().QueryTGtMatrixInfo(); }
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
		    protected HpSpQyCall<TGtMatrixChildCQ> _qyCall;
		    public TItemInfoSpQyCall(HpSpQyCall<TGtMatrixChildCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfo(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfo(); }
		}
    }
}
