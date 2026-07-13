
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
    public class BsTRawdataDeleteQueCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TRawdataDeleteQueCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_RAWDATA_DELETE_QUE"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? rawdataDeleteQueId) {
            assertObjectNotNull("rawdataDeleteQueId", rawdataDeleteQueId);
            BsTRawdataDeleteQueCB cb = this;
            cb.Query().SetRawdataDeleteQueId_Equal(rawdataDeleteQueId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_RawdataDeleteQueId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_RawdataDeleteQueId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TRawdataDeleteQueCQ Query() {
            return this.ConditionQuery;
        }

        public TRawdataDeleteQueCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TRawdataDeleteQueCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TRawdataDeleteQueCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TRawdataDeleteQueCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TRawdataDeleteQueCB> unionQuery) {
            TRawdataDeleteQueCB cb = new TRawdataDeleteQueCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TRawdataDeleteQueCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TRawdataDeleteQueCB> unionQuery) {
            TRawdataDeleteQueCB cb = new TRawdataDeleteQueCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TRawdataDeleteQueCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TRawdataDeleteQueCBSpecification _specification;
        public TRawdataDeleteQueCBSpecification Specify() {
            if (_specification == null) { _specification = new TRawdataDeleteQueCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TRawdataDeleteQueCQ> {
			protected BsTRawdataDeleteQueCB _myCB;
			public MySpQyCall(BsTRawdataDeleteQueCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TRawdataDeleteQueCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TRawdataDeleteQueCB> ColumnQuery(SpecifyQuery<TRawdataDeleteQueCB> leftSpecifyQuery) {
            return new HpColQyOperand<TRawdataDeleteQueCB>(delegate(SpecifyQuery<TRawdataDeleteQueCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TRawdataDeleteQueCB xcreateColumnQueryCB() {
            TRawdataDeleteQueCB cb = new TRawdataDeleteQueCB();
            cb.xsetupForColumnQuery((TRawdataDeleteQueCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TRawdataDeleteQueCB> orQuery) {
            xorQ((TRawdataDeleteQueCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TRawdataDeleteQueCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TRawdataDeleteQueCBColQySpQyCall(mainCB));
        }
    }

    public class TRawdataDeleteQueCBColQySpQyCall : HpSpQyCall<TRawdataDeleteQueCQ> {
        protected TRawdataDeleteQueCB _mainCB;
        public TRawdataDeleteQueCBColQySpQyCall(TRawdataDeleteQueCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TRawdataDeleteQueCQ qy() { return _mainCB.Query(); } 
    }

    public class TRawdataDeleteQueCBSpecification : AbstractSpecification<TRawdataDeleteQueCQ> {
        public TRawdataDeleteQueCBSpecification(ConditionBean baseCB, HpSpQyCall<TRawdataDeleteQueCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnRawdataDeleteQueId() { doColumn("RAWDATA_DELETE_QUE_ID"); }
        public void ColumnAddDataNo() { doColumn("ADD_DATA_NO"); }
        public void ColumnQcwebJobNo() { doColumn("QCWEB_JOB_NO"); }
        public void ColumnMainSurveyId() { doColumn("MAIN_SURVEY_ID"); }
        public void ColumnDeleteOrderDate() { doColumn("DELETE_ORDER_DATE"); }
        public void ColumnDeleteStatus() { doColumn("DELETE_STATUS"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnRawdataDeleteQueId(); // PK
        }
        protected override String getTableDbName() { return "T_RAWDATA_DELETE_QUE"; }
    }
}
