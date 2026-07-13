
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
    public class BsTDeleteDataCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDeleteDataCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_DATA"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? dataEditId) {
            assertObjectNotNull("dataEditId", dataEditId);
            BsTDeleteDataCB cb = this;
            cb.Query().SetDataEditId_Equal(dataEditId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DataEditId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DataEditId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDeleteDataCQ Query() {
            return this.ConditionQuery;
        }

        public TDeleteDataCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDeleteDataCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDeleteDataCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDeleteDataCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDeleteDataCB> unionQuery) {
            TDeleteDataCB cb = new TDeleteDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDeleteDataCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDeleteDataCB> unionQuery) {
            TDeleteDataCB cb = new TDeleteDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDeleteDataCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDataEditListNss _nssTDataEditList;
        public TDataEditListNss NssTDataEditList { get {
            if (_nssTDataEditList == null) { _nssTDataEditList = new TDataEditListNss(null); }
            return _nssTDataEditList;
        }}
        public TDataEditListNss SetupSelect_TDataEditList() {
            doSetupSelect(delegate { return Query().QueryTDataEditList(); });
            if (_nssTDataEditList == null || !_nssTDataEditList.HasConditionQuery)
            { _nssTDataEditList = new TDataEditListNss(Query().QueryTDataEditList()); }
            return _nssTDataEditList;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDeleteDataCBSpecification _specification;
        public TDeleteDataCBSpecification Specify() {
            if (_specification == null) { _specification = new TDeleteDataCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDeleteDataCQ> {
			protected BsTDeleteDataCB _myCB;
			public MySpQyCall(BsTDeleteDataCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDeleteDataCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDeleteDataCB> ColumnQuery(SpecifyQuery<TDeleteDataCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDeleteDataCB>(delegate(SpecifyQuery<TDeleteDataCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDeleteDataCB xcreateColumnQueryCB() {
            TDeleteDataCB cb = new TDeleteDataCB();
            cb.xsetupForColumnQuery((TDeleteDataCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDeleteDataCB> orQuery) {
            xorQ((TDeleteDataCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDeleteDataCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDeleteDataCBColQySpQyCall(mainCB));
        }
    }

    public class TDeleteDataCBColQySpQyCall : HpSpQyCall<TDeleteDataCQ> {
        protected TDeleteDataCB _mainCB;
        public TDeleteDataCBColQySpQyCall(TDeleteDataCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDeleteDataCQ qy() { return _mainCB.Query(); } 
    }

    public class TDeleteDataCBSpecification : AbstractSpecification<TDeleteDataCQ> {
        protected TDataEditListCBSpecification _tDataEditList;
        public TDeleteDataCBSpecification(ConditionBean baseCB, HpSpQyCall<TDeleteDataCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        public void ColumnDeleteType() { doColumn("DELETE_TYPE"); }
        public void ColumnConditionDiv() { doColumn("CONDITION_DIV"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDataEditId(); // PK
        }
        protected override String getTableDbName() { return "T_DELETE_DATA"; }
        public TDataEditListCBSpecification SpecifyTDataEditList() {
            assertForeign("tDataEditList");
            if (_tDataEditList == null) {
                _tDataEditList = new TDataEditListCBSpecification(_baseCB, new TDataEditListSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDataEditList.xsetSyncQyCall(new TDataEditListSpQyCall(xsyncQyCall())); }
            }
            return _tDataEditList;
        }
		public class TDataEditListSpQyCall : HpSpQyCall<TDataEditListCQ> {
		    protected HpSpQyCall<TDeleteDataCQ> _qyCall;
		    public TDataEditListSpQyCall(HpSpQyCall<TDeleteDataCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataEditList(); }
			public TDataEditListCQ qy() { return _qyCall.qy().QueryTDataEditList(); }
		}
        public RAFunction<TDeleteConditionCB, TDeleteDataCQ> DerivedTDeleteConditionList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDeleteConditionCB, TDeleteDataCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDeleteConditionCB> subQuery, TDeleteDataCQ cq, String aliasName)
                { cq.xsderiveTDeleteConditionList(function, subQuery, aliasName); });
        }
        public RAFunction<TDeleteSampleIdListCB, TDeleteDataCQ> DerivedTDeleteSampleIdListList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDeleteSampleIdListCB, TDeleteDataCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDeleteSampleIdListCB> subQuery, TDeleteDataCQ cq, String aliasName)
                { cq.xsderiveTDeleteSampleIdListList(function, subQuery, aliasName); });
        }
    }
}
