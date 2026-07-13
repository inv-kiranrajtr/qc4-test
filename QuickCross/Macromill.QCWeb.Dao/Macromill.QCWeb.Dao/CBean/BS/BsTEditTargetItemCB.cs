
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
    public class BsTEditTargetItemCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TEditTargetItemCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_TARGET_ITEM"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? editTargetItemId) {
            assertObjectNotNull("editTargetItemId", editTargetItemId);
            BsTEditTargetItemCB cb = this;
            cb.Query().SetEditTargetItemId_Equal(editTargetItemId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_EditTargetItemId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_EditTargetItemId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TEditTargetItemCQ Query() {
            return this.ConditionQuery;
        }

        public TEditTargetItemCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TEditTargetItemCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TEditTargetItemCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TEditTargetItemCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TEditTargetItemCB> unionQuery) {
            TEditTargetItemCB cb = new TEditTargetItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditTargetItemCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TEditTargetItemCB> unionQuery) {
            TEditTargetItemCB cb = new TEditTargetItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditTargetItemCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TEditDataNss _nssTEditData;
        public TEditDataNss NssTEditData { get {
            if (_nssTEditData == null) { _nssTEditData = new TEditDataNss(null); }
            return _nssTEditData;
        }}
        public TEditDataNss SetupSelect_TEditData() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDataEditId();
            }
            doSetupSelect(delegate { return Query().QueryTEditData(); });
            if (_nssTEditData == null || !_nssTEditData.HasConditionQuery)
            { _nssTEditData = new TEditDataNss(Query().QueryTEditData()); }
            return _nssTEditData;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TEditTargetItemCBSpecification _specification;
        public TEditTargetItemCBSpecification Specify() {
            if (_specification == null) { _specification = new TEditTargetItemCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TEditTargetItemCQ> {
			protected BsTEditTargetItemCB _myCB;
			public MySpQyCall(BsTEditTargetItemCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TEditTargetItemCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TEditTargetItemCB> ColumnQuery(SpecifyQuery<TEditTargetItemCB> leftSpecifyQuery) {
            return new HpColQyOperand<TEditTargetItemCB>(delegate(SpecifyQuery<TEditTargetItemCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TEditTargetItemCB xcreateColumnQueryCB() {
            TEditTargetItemCB cb = new TEditTargetItemCB();
            cb.xsetupForColumnQuery((TEditTargetItemCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TEditTargetItemCB> orQuery) {
            xorQ((TEditTargetItemCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TEditTargetItemCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TEditTargetItemCBColQySpQyCall(mainCB));
        }
    }

    public class TEditTargetItemCBColQySpQyCall : HpSpQyCall<TEditTargetItemCQ> {
        protected TEditTargetItemCB _mainCB;
        public TEditTargetItemCBColQySpQyCall(TEditTargetItemCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TEditTargetItemCQ qy() { return _mainCB.Query(); } 
    }

    public class TEditTargetItemCBSpecification : AbstractSpecification<TEditTargetItemCQ> {
        protected TEditDataCBSpecification _tEditData;
        public TEditTargetItemCBSpecification(ConditionBean baseCB, HpSpQyCall<TEditTargetItemCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnEditTargetItemId() { doColumn("EDIT_TARGET_ITEM_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnTargetItemId() { doColumn("TARGET_ITEM_ID"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnEditTargetItemId(); // PK
            if (qyCall().qy().hasConditionQueryTEditData()
                    || qyCall().qy().xgetReferrerQuery() is TEditDataCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_EDIT_TARGET_ITEM"; }
        public TEditDataCBSpecification SpecifyTEditData() {
            assertForeign("tEditData");
            if (_tEditData == null) {
                _tEditData = new TEditDataCBSpecification(_baseCB, new TEditDataSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tEditData.xsetSyncQyCall(new TEditDataSpQyCall(xsyncQyCall())); }
            }
            return _tEditData;
        }
		public class TEditDataSpQyCall : HpSpQyCall<TEditDataCQ> {
		    protected HpSpQyCall<TEditTargetItemCQ> _qyCall;
		    public TEditDataSpQyCall(HpSpQyCall<TEditTargetItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTEditData(); }
			public TEditDataCQ qy() { return _qyCall.qy().QueryTEditData(); }
		}
    }
}
