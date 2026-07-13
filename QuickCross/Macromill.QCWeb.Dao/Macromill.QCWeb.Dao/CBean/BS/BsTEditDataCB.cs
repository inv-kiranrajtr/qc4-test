
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
    public class BsTEditDataCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TEditDataCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_DATA"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? dataEditId) {
            assertObjectNotNull("dataEditId", dataEditId);
            BsTEditDataCB cb = this;
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
        public TEditDataCQ Query() {
            return this.ConditionQuery;
        }

        public TEditDataCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TEditDataCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TEditDataCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TEditDataCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TEditDataCB> unionQuery) {
            TEditDataCB cb = new TEditDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditDataCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TEditDataCB> unionQuery) {
            TEditDataCB cb = new TEditDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TEditDataCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TEditDataCBSpecification _specification;
        public TEditDataCBSpecification Specify() {
            if (_specification == null) { _specification = new TEditDataCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TEditDataCQ> {
			protected BsTEditDataCB _myCB;
			public MySpQyCall(BsTEditDataCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TEditDataCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TEditDataCB> ColumnQuery(SpecifyQuery<TEditDataCB> leftSpecifyQuery) {
            return new HpColQyOperand<TEditDataCB>(delegate(SpecifyQuery<TEditDataCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TEditDataCB xcreateColumnQueryCB() {
            TEditDataCB cb = new TEditDataCB();
            cb.xsetupForColumnQuery((TEditDataCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TEditDataCB> orQuery) {
            xorQ((TEditDataCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TEditDataCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TEditDataCBColQySpQyCall(mainCB));
        }
    }

    public class TEditDataCBColQySpQyCall : HpSpQyCall<TEditDataCQ> {
        protected TEditDataCB _mainCB;
        public TEditDataCBColQySpQyCall(TEditDataCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TEditDataCQ qy() { return _mainCB.Query(); } 
    }

    public class TEditDataCBSpecification : AbstractSpecification<TEditDataCQ> {
        protected TDataEditListCBSpecification _tDataEditList;
        public TEditDataCBSpecification(ConditionBean baseCB, HpSpQyCall<TEditDataCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        public void ColumnConditionFlag() { doColumn("CONDITION_FLAG"); }
        public void ColumnEditMethod() { doColumn("EDIT_METHOD"); }
        public void ColumnEditValueType() { doColumn("EDIT_VALUE_TYPE"); }
        public void ColumnEditValue() { doColumn("EDIT_VALUE"); }
        public void ColumnConditionDiv() { doColumn("CONDITION_DIV"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDataEditId(); // PK
        }
        protected override String getTableDbName() { return "T_EDIT_DATA"; }
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
		    protected HpSpQyCall<TEditDataCQ> _qyCall;
		    public TDataEditListSpQyCall(HpSpQyCall<TEditDataCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataEditList(); }
			public TDataEditListCQ qy() { return _qyCall.qy().QueryTDataEditList(); }
		}
        public RAFunction<TEditConditionCB, TEditDataCQ> DerivedTEditConditionList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TEditConditionCB, TEditDataCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TEditConditionCB> subQuery, TEditDataCQ cq, String aliasName)
                { cq.xsderiveTEditConditionList(function, subQuery, aliasName); });
        }
        public RAFunction<TEditTargetItemCB, TEditDataCQ> DerivedTEditTargetItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TEditTargetItemCB, TEditDataCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TEditTargetItemCB> subQuery, TEditDataCQ cq, String aliasName)
                { cq.xsderiveTEditTargetItemList(function, subQuery, aliasName); });
        }
    }
}
