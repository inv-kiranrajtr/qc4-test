
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
    public class BsTDeleteSampleIdListCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDeleteSampleIdListCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_SAMPLE_ID_LIST"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? deleteSampleId) {
            assertObjectNotNull("deleteSampleId", deleteSampleId);
            BsTDeleteSampleIdListCB cb = this;
            cb.Query().SetDeleteSampleId_Equal(deleteSampleId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DeleteSampleId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DeleteSampleId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDeleteSampleIdListCQ Query() {
            return this.ConditionQuery;
        }

        public TDeleteSampleIdListCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDeleteSampleIdListCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDeleteSampleIdListCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDeleteSampleIdListCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDeleteSampleIdListCB> unionQuery) {
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDeleteSampleIdListCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDeleteSampleIdListCB> unionQuery) {
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDeleteSampleIdListCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDeleteDataNss _nssTDeleteData;
        public TDeleteDataNss NssTDeleteData { get {
            if (_nssTDeleteData == null) { _nssTDeleteData = new TDeleteDataNss(null); }
            return _nssTDeleteData;
        }}
        public TDeleteDataNss SetupSelect_TDeleteData() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDataEditId();
            }
            doSetupSelect(delegate { return Query().QueryTDeleteData(); });
            if (_nssTDeleteData == null || !_nssTDeleteData.HasConditionQuery)
            { _nssTDeleteData = new TDeleteDataNss(Query().QueryTDeleteData()); }
            return _nssTDeleteData;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDeleteSampleIdListCBSpecification _specification;
        public TDeleteSampleIdListCBSpecification Specify() {
            if (_specification == null) { _specification = new TDeleteSampleIdListCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDeleteSampleIdListCQ> {
			protected BsTDeleteSampleIdListCB _myCB;
			public MySpQyCall(BsTDeleteSampleIdListCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDeleteSampleIdListCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDeleteSampleIdListCB> ColumnQuery(SpecifyQuery<TDeleteSampleIdListCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDeleteSampleIdListCB>(delegate(SpecifyQuery<TDeleteSampleIdListCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDeleteSampleIdListCB xcreateColumnQueryCB() {
            TDeleteSampleIdListCB cb = new TDeleteSampleIdListCB();
            cb.xsetupForColumnQuery((TDeleteSampleIdListCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDeleteSampleIdListCB> orQuery) {
            xorQ((TDeleteSampleIdListCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDeleteSampleIdListCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDeleteSampleIdListCBColQySpQyCall(mainCB));
        }
    }

    public class TDeleteSampleIdListCBColQySpQyCall : HpSpQyCall<TDeleteSampleIdListCQ> {
        protected TDeleteSampleIdListCB _mainCB;
        public TDeleteSampleIdListCBColQySpQyCall(TDeleteSampleIdListCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDeleteSampleIdListCQ qy() { return _mainCB.Query(); } 
    }

    public class TDeleteSampleIdListCBSpecification : AbstractSpecification<TDeleteSampleIdListCQ> {
        protected TDeleteDataCBSpecification _tDeleteData;
        public TDeleteSampleIdListCBSpecification(ConditionBean baseCB, HpSpQyCall<TDeleteSampleIdListCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDeleteSampleId() { doColumn("DELETE_SAMPLE_ID"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        public void ColumnDeleteSampleIdText() { doColumn("DELETE_SAMPLE_ID_TEXT"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDeleteSampleId(); // PK
            if (qyCall().qy().hasConditionQueryTDeleteData()
                    || qyCall().qy().xgetReferrerQuery() is TDeleteDataCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DELETE_SAMPLE_ID_LIST"; }
        public TDeleteDataCBSpecification SpecifyTDeleteData() {
            assertForeign("tDeleteData");
            if (_tDeleteData == null) {
                _tDeleteData = new TDeleteDataCBSpecification(_baseCB, new TDeleteDataSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDeleteData.xsetSyncQyCall(new TDeleteDataSpQyCall(xsyncQyCall())); }
            }
            return _tDeleteData;
        }
		public class TDeleteDataSpQyCall : HpSpQyCall<TDeleteDataCQ> {
		    protected HpSpQyCall<TDeleteSampleIdListCQ> _qyCall;
		    public TDeleteDataSpQyCall(HpSpQyCall<TDeleteSampleIdListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDeleteData(); }
			public TDeleteDataCQ qy() { return _qyCall.qy().QueryTDeleteData(); }
		}
    }
}
