
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
    public class BsTEditConditionCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TEditConditionCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_CONDITION"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? editConditionId) {
            assertObjectNotNull("editConditionId", editConditionId);
            BsTEditConditionCB cb = this;
            cb.Query().SetEditConditionId_Equal(editConditionId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_EditConditionId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_EditConditionId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TEditConditionCQ Query() {
            return this.ConditionQuery;
        }

        public TEditConditionCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TEditConditionCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TEditConditionCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TEditConditionCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TEditConditionCB> unionQuery) {
            TEditConditionCB cb = new TEditConditionCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditConditionCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TEditConditionCB> unionQuery) {
            TEditConditionCB cb = new TEditConditionCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditConditionCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TEditConditionCBSpecification _specification;
        public TEditConditionCBSpecification Specify() {
            if (_specification == null) { _specification = new TEditConditionCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TEditConditionCQ> {
			protected BsTEditConditionCB _myCB;
			public MySpQyCall(BsTEditConditionCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TEditConditionCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TEditConditionCB> ColumnQuery(SpecifyQuery<TEditConditionCB> leftSpecifyQuery) {
            return new HpColQyOperand<TEditConditionCB>(delegate(SpecifyQuery<TEditConditionCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TEditConditionCB xcreateColumnQueryCB() {
            TEditConditionCB cb = new TEditConditionCB();
            cb.xsetupForColumnQuery((TEditConditionCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TEditConditionCB> orQuery) {
            xorQ((TEditConditionCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TEditConditionCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TEditConditionCBColQySpQyCall(mainCB));
        }
    }

    public class TEditConditionCBColQySpQyCall : HpSpQyCall<TEditConditionCQ> {
        protected TEditConditionCB _mainCB;
        public TEditConditionCBColQySpQyCall(TEditConditionCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TEditConditionCQ qy() { return _mainCB.Query(); } 
    }

    public class TEditConditionCBSpecification : AbstractSpecification<TEditConditionCQ> {
        protected TEditDataCBSpecification _tEditData;
        public TEditConditionCBSpecification(ConditionBean baseCB, HpSpQyCall<TEditConditionCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnEditConditionId() { doColumn("EDIT_CONDITION_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnItemId() { doColumn("ITEM_ID"); }
        public void ColumnOperationCode() { doColumn("OPERATION_CODE"); }
        public void ColumnOperationValue() { doColumn("OPERATION_VALUE"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnEditConditionId(); // PK
            if (qyCall().qy().hasConditionQueryTEditData()
                    || qyCall().qy().xgetReferrerQuery() is TEditDataCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_EDIT_CONDITION"; }
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
		    protected HpSpQyCall<TEditConditionCQ> _qyCall;
		    public TEditDataSpQyCall(HpSpQyCall<TEditConditionCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTEditData(); }
			public TEditDataCQ qy() { return _qyCall.qy().QueryTEditData(); }
		}
    }
}
