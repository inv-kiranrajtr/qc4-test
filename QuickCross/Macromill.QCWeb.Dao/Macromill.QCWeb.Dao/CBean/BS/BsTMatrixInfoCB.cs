
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
    public class BsTMatrixInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TMatrixInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_MATRIX_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? matrixInfoId) {
            assertObjectNotNull("matrixInfoId", matrixInfoId);
            BsTMatrixInfoCB cb = this;
            cb.Query().SetMatrixInfoId_Equal(matrixInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_MatrixInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_MatrixInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TMatrixInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TMatrixInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TMatrixInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TMatrixInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TMatrixInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TMatrixInfoCB> unionQuery) {
            TMatrixInfoCB cb = new TMatrixInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TMatrixInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TMatrixInfoCB> unionQuery) {
            TMatrixInfoCB cb = new TMatrixInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TMatrixInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TItemInfoNss _nssTItemInfoByItemInfoId;
        public TItemInfoNss NssTItemInfoByItemInfoId { get {
            if (_nssTItemInfoByItemInfoId == null) { _nssTItemInfoByItemInfoId = new TItemInfoNss(null); }
            return _nssTItemInfoByItemInfoId;
        }}
        public TItemInfoNss SetupSelect_TItemInfoByItemInfoId() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnItemInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTItemInfoByItemInfoId(); });
            if (_nssTItemInfoByItemInfoId == null || !_nssTItemInfoByItemInfoId.HasConditionQuery)
            { _nssTItemInfoByItemInfoId = new TItemInfoNss(Query().QueryTItemInfoByItemInfoId()); }
            return _nssTItemInfoByItemInfoId;
        }
        protected TItemInfoNss _nssTItemInfoByChildItemInfoId;
        public TItemInfoNss NssTItemInfoByChildItemInfoId { get {
            if (_nssTItemInfoByChildItemInfoId == null) { _nssTItemInfoByChildItemInfoId = new TItemInfoNss(null); }
            return _nssTItemInfoByChildItemInfoId;
        }}
        public TItemInfoNss SetupSelect_TItemInfoByChildItemInfoId() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnChildItemInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTItemInfoByChildItemInfoId(); });
            if (_nssTItemInfoByChildItemInfoId == null || !_nssTItemInfoByChildItemInfoId.HasConditionQuery)
            { _nssTItemInfoByChildItemInfoId = new TItemInfoNss(Query().QueryTItemInfoByChildItemInfoId()); }
            return _nssTItemInfoByChildItemInfoId;
        }
        protected TCategoryInfoNss _nssTCategoryInfo;
        public TCategoryInfoNss NssTCategoryInfo { get {
            if (_nssTCategoryInfo == null) { _nssTCategoryInfo = new TCategoryInfoNss(null); }
            return _nssTCategoryInfo;
        }}
        public TCategoryInfoNss SetupSelect_TCategoryInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnAddFaCategoryInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTCategoryInfo(); });
            if (_nssTCategoryInfo == null || !_nssTCategoryInfo.HasConditionQuery)
            { _nssTCategoryInfo = new TCategoryInfoNss(Query().QueryTCategoryInfo()); }
            return _nssTCategoryInfo;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TMatrixInfoCBSpecification _specification;
        public TMatrixInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TMatrixInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TMatrixInfoCQ> {
			protected BsTMatrixInfoCB _myCB;
			public MySpQyCall(BsTMatrixInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TMatrixInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TMatrixInfoCB> ColumnQuery(SpecifyQuery<TMatrixInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TMatrixInfoCB>(delegate(SpecifyQuery<TMatrixInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TMatrixInfoCB xcreateColumnQueryCB() {
            TMatrixInfoCB cb = new TMatrixInfoCB();
            cb.xsetupForColumnQuery((TMatrixInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TMatrixInfoCB> orQuery) {
            xorQ((TMatrixInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TMatrixInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TMatrixInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TMatrixInfoCBColQySpQyCall : HpSpQyCall<TMatrixInfoCQ> {
        protected TMatrixInfoCB _mainCB;
        public TMatrixInfoCBColQySpQyCall(TMatrixInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TMatrixInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TMatrixInfoCBSpecification : AbstractSpecification<TMatrixInfoCQ> {
        protected TItemInfoCBSpecification _tItemInfoByItemInfoId;
        protected TItemInfoCBSpecification _tItemInfoByChildItemInfoId;
        protected TCategoryInfoCBSpecification _tCategoryInfo;
        public TMatrixInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TMatrixInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnMatrixInfoId() { doColumn("MATRIX_INFO_ID"); }
        public void ColumnItemInfoId() { doColumn("ITEM_INFO_ID"); }
        public void ColumnChildItemInfoId() { doColumn("CHILD_ITEM_INFO_ID"); }
        public void ColumnAddFaItemInfoId() { doColumn("ADD_FA_ITEM_INFO_ID"); }
        public void ColumnAddFaCategoryInfoId() { doColumn("ADD_FA_CATEGORY_INFO_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnMatrixInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTItemInfoByItemInfoId()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnItemInfoId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTItemInfoByChildItemInfoId()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnChildItemInfoId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTCategoryInfo()
                    || qyCall().qy().xgetReferrerQuery() is TCategoryInfoCQ) {
                ColumnAddFaCategoryInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_MATRIX_INFO"; }
        public TItemInfoCBSpecification SpecifyTItemInfoByItemInfoId() {
            assertForeign("tItemInfoByItemInfoId");
            if (_tItemInfoByItemInfoId == null) {
                _tItemInfoByItemInfoId = new TItemInfoCBSpecification(_baseCB, new TItemInfoByItemInfoIdSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tItemInfoByItemInfoId.xsetSyncQyCall(new TItemInfoByItemInfoIdSpQyCall(xsyncQyCall())); }
            }
            return _tItemInfoByItemInfoId;
        }
		public class TItemInfoByItemInfoIdSpQyCall : HpSpQyCall<TItemInfoCQ> {
		    protected HpSpQyCall<TMatrixInfoCQ> _qyCall;
		    public TItemInfoByItemInfoIdSpQyCall(HpSpQyCall<TMatrixInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfoByItemInfoId(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfoByItemInfoId(); }
		}
        public TItemInfoCBSpecification SpecifyTItemInfoByChildItemInfoId() {
            assertForeign("tItemInfoByChildItemInfoId");
            if (_tItemInfoByChildItemInfoId == null) {
                _tItemInfoByChildItemInfoId = new TItemInfoCBSpecification(_baseCB, new TItemInfoByChildItemInfoIdSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tItemInfoByChildItemInfoId.xsetSyncQyCall(new TItemInfoByChildItemInfoIdSpQyCall(xsyncQyCall())); }
            }
            return _tItemInfoByChildItemInfoId;
        }
		public class TItemInfoByChildItemInfoIdSpQyCall : HpSpQyCall<TItemInfoCQ> {
		    protected HpSpQyCall<TMatrixInfoCQ> _qyCall;
		    public TItemInfoByChildItemInfoIdSpQyCall(HpSpQyCall<TMatrixInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfoByChildItemInfoId(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfoByChildItemInfoId(); }
		}
        public TCategoryInfoCBSpecification SpecifyTCategoryInfo() {
            assertForeign("tCategoryInfo");
            if (_tCategoryInfo == null) {
                _tCategoryInfo = new TCategoryInfoCBSpecification(_baseCB, new TCategoryInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tCategoryInfo.xsetSyncQyCall(new TCategoryInfoSpQyCall(xsyncQyCall())); }
            }
            return _tCategoryInfo;
        }
		public class TCategoryInfoSpQyCall : HpSpQyCall<TCategoryInfoCQ> {
		    protected HpSpQyCall<TMatrixInfoCQ> _qyCall;
		    public TCategoryInfoSpQyCall(HpSpQyCall<TMatrixInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTCategoryInfo(); }
			public TCategoryInfoCQ qy() { return _qyCall.qy().QueryTCategoryInfo(); }
		}
    }
}
