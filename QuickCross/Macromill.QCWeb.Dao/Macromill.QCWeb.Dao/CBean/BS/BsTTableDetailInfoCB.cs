
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
    public class BsTTableDetailInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TTableDetailInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_TABLE_DETAIL_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? qcwebid, int? tableNo) {
            assertObjectNotNull("qcwebid", qcwebid);assertObjectNotNull("tableNo", tableNo);
            BsTTableDetailInfoCB cb = this;
            cb.Query().SetQcwebid_Equal(qcwebid);cb.Query().SetTableNo_Equal(tableNo);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_Qcwebid_Asc();
            Query().AddOrderBy_TableNo_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_Qcwebid_Desc();
            Query().AddOrderBy_TableNo_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TTableDetailInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TTableDetailInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TTableDetailInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TTableDetailInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TTableDetailInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TTableDetailInfoCB> unionQuery) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TTableDetailInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TTableDetailInfoCB> unionQuery) {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TTableDetailInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TTableControlNss _nssTTableControl;
        public TTableControlNss NssTTableControl { get {
            if (_nssTTableControl == null) { _nssTTableControl = new TTableControlNss(null); }
            return _nssTTableControl;
        }}
        public TTableControlNss SetupSelect_TTableControl() {
            doSetupSelect(delegate { return Query().QueryTTableControl(); });
            if (_nssTTableControl == null || !_nssTTableControl.HasConditionQuery)
            { _nssTTableControl = new TTableControlNss(Query().QueryTTableControl()); }
            return _nssTTableControl;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TTableDetailInfoCBSpecification _specification;
        public TTableDetailInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TTableDetailInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TTableDetailInfoCQ> {
			protected BsTTableDetailInfoCB _myCB;
			public MySpQyCall(BsTTableDetailInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TTableDetailInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TTableDetailInfoCB> ColumnQuery(SpecifyQuery<TTableDetailInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TTableDetailInfoCB>(delegate(SpecifyQuery<TTableDetailInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TTableDetailInfoCB xcreateColumnQueryCB() {
            TTableDetailInfoCB cb = new TTableDetailInfoCB();
            cb.xsetupForColumnQuery((TTableDetailInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TTableDetailInfoCB> orQuery) {
            xorQ((TTableDetailInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TTableDetailInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TTableDetailInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TTableDetailInfoCBColQySpQyCall : HpSpQyCall<TTableDetailInfoCQ> {
        protected TTableDetailInfoCB _mainCB;
        public TTableDetailInfoCBColQySpQyCall(TTableDetailInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TTableDetailInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TTableDetailInfoCBSpecification : AbstractSpecification<TTableDetailInfoCQ> {
        protected TTableControlCBSpecification _tTableControl;
        public TTableDetailInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TTableDetailInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnTableNo() { doColumn("TABLE_NO"); }
        public void ColumnUsedNo() { doColumn("USED_NO"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnQcwebid(); // PK
            ColumnTableNo(); // PK
        }
        protected override String getTableDbName() { return "T_TABLE_DETAIL_INFO"; }
        public TTableControlCBSpecification SpecifyTTableControl() {
            assertForeign("tTableControl");
            if (_tTableControl == null) {
                _tTableControl = new TTableControlCBSpecification(_baseCB, new TTableControlSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tTableControl.xsetSyncQyCall(new TTableControlSpQyCall(xsyncQyCall())); }
            }
            return _tTableControl;
        }
		public class TTableControlSpQyCall : HpSpQyCall<TTableControlCQ> {
		    protected HpSpQyCall<TTableDetailInfoCQ> _qyCall;
		    public TTableControlSpQyCall(HpSpQyCall<TTableDetailInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTTableControl(); }
			public TTableControlCQ qy() { return _qyCall.qy().QueryTTableControl(); }
		}
    }
}
