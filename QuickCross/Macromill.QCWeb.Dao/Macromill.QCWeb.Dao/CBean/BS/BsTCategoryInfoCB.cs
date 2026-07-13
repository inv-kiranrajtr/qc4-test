
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
    public class BsTCategoryInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCategoryInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? categoryInfoId) {
            assertObjectNotNull("categoryInfoId", categoryInfoId);
            BsTCategoryInfoCB cb = this;
            cb.Query().SetCategoryInfoId_Equal(categoryInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_CategoryInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_CategoryInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TCategoryInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TCategoryInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TCategoryInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TCategoryInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TCategoryInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TCategoryInfoCB> unionQuery) {
            TCategoryInfoCB cb = new TCategoryInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCategoryInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TCategoryInfoCB> unionQuery) {
            TCategoryInfoCB cb = new TCategoryInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TCategoryInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TItemInfoNss _nssTItemInfo;
        public TItemInfoNss NssTItemInfo { get {
            if (_nssTItemInfo == null) { _nssTItemInfo = new TItemInfoNss(null); }
            return _nssTItemInfo;
        }}
        public TItemInfoNss SetupSelect_TItemInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnItemInfoId();
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
        protected TCategoryInfoCBSpecification _specification;
        public TCategoryInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TCategoryInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TCategoryInfoCQ> {
			protected BsTCategoryInfoCB _myCB;
			public MySpQyCall(BsTCategoryInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TCategoryInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TCategoryInfoCB> ColumnQuery(SpecifyQuery<TCategoryInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TCategoryInfoCB>(delegate(SpecifyQuery<TCategoryInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TCategoryInfoCB xcreateColumnQueryCB() {
            TCategoryInfoCB cb = new TCategoryInfoCB();
            cb.xsetupForColumnQuery((TCategoryInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TCategoryInfoCB> orQuery) {
            xorQ((TCategoryInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TCategoryInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TCategoryInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TCategoryInfoCBColQySpQyCall : HpSpQyCall<TCategoryInfoCQ> {
        protected TCategoryInfoCB _mainCB;
        public TCategoryInfoCBColQySpQyCall(TCategoryInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TCategoryInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TCategoryInfoCBSpecification : AbstractSpecification<TCategoryInfoCQ> {
        protected TItemInfoCBSpecification _tItemInfo;
        public TCategoryInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TCategoryInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnCategoryInfoId() { doColumn("CATEGORY_INFO_ID"); }
        public void ColumnItemInfoId() { doColumn("ITEM_INFO_ID"); }
        public void ColumnCategoryNo() { doColumn("CATEGORY_NO"); }
        public void ColumnCategoryName() { doColumn("CATEGORY_NAME"); }
        public void ColumnWeightValue() { doColumn("WEIGHT_VALUE"); }
        public void ColumnOriginalCategoryName() { doColumn("ORIGINAL_CATEGORY_NAME"); }
        public void ColumnOriginalWeightValue() { doColumn("ORIGINAL_WEIGHT_VALUE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnCategoryInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTItemInfo()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnItemInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_CATEGORY_INFO"; }
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
		    protected HpSpQyCall<TCategoryInfoCQ> _qyCall;
		    public TItemInfoSpQyCall(HpSpQyCall<TCategoryInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfo(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfo(); }
		}
        public RAFunction<TMatrixInfoCB, TCategoryInfoCQ> DerivedTMatrixInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TMatrixInfoCB, TCategoryInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TMatrixInfoCB> subQuery, TCategoryInfoCQ cq, String aliasName)
                { cq.xsderiveTMatrixInfoList(function, subQuery, aliasName); });
        }
    }
}
