
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
    public class BsTOutputSubCklistCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubCklistCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_CKLIST"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputSubCklistId) {
            assertObjectNotNull("outputSubCklistId", outputSubCklistId);
            BsTOutputSubCklistCB cb = this;
            cb.Query().SetOutputSubCklistId_Equal(outputSubCklistId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputSubCklistId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputSubCklistId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputSubCklistCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSubCklistCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSubCklistCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSubCklistCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSubCklistCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSubCklistCB> unionQuery) {
            TOutputSubCklistCB cb = new TOutputSubCklistCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubCklistCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSubCklistCB> unionQuery) {
            TOutputSubCklistCB cb = new TOutputSubCklistCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubCklistCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputCommonNss _nssTOutputCommon;
        public TOutputCommonNss NssTOutputCommon { get {
            if (_nssTOutputCommon == null) { _nssTOutputCommon = new TOutputCommonNss(null); }
            return _nssTOutputCommon;
        }}
        public TOutputCommonNss SetupSelect_TOutputCommon() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnOutputCommonId();
            }
            doSetupSelect(delegate { return Query().QueryTOutputCommon(); });
            if (_nssTOutputCommon == null || !_nssTOutputCommon.HasConditionQuery)
            { _nssTOutputCommon = new TOutputCommonNss(Query().QueryTOutputCommon()); }
            return _nssTOutputCommon;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputSubCklistCBSpecification _specification;
        public TOutputSubCklistCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSubCklistCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSubCklistCQ> {
			protected BsTOutputSubCklistCB _myCB;
			public MySpQyCall(BsTOutputSubCklistCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSubCklistCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSubCklistCB> ColumnQuery(SpecifyQuery<TOutputSubCklistCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSubCklistCB>(delegate(SpecifyQuery<TOutputSubCklistCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSubCklistCB xcreateColumnQueryCB() {
            TOutputSubCklistCB cb = new TOutputSubCklistCB();
            cb.xsetupForColumnQuery((TOutputSubCklistCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSubCklistCB> orQuery) {
            xorQ((TOutputSubCklistCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSubCklistCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSubCklistCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSubCklistCBColQySpQyCall : HpSpQyCall<TOutputSubCklistCQ> {
        protected TOutputSubCklistCB _mainCB;
        public TOutputSubCklistCBColQySpQyCall(TOutputSubCklistCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSubCklistCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSubCklistCBSpecification : AbstractSpecification<TOutputSubCklistCQ> {
        protected TOutputCommonCBSpecification _tOutputCommon;
        public TOutputSubCklistCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSubCklistCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputSubCklistId() { doColumn("OUTPUT_SUB_CKLIST_ID"); }
        public void ColumnOutputCommonId() { doColumn("OUTPUT_COMMON_ID"); }
        public void ColumnTotalCount() { doColumn("TOTAL_COUNT"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputSubCklistId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputCommon()
                    || qyCall().qy().xgetReferrerQuery() is TOutputCommonCQ) {
                ColumnOutputCommonId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_SUB_CKLIST"; }
        public TOutputCommonCBSpecification SpecifyTOutputCommon() {
            assertForeign("tOutputCommon");
            if (_tOutputCommon == null) {
                _tOutputCommon = new TOutputCommonCBSpecification(_baseCB, new TOutputCommonSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputCommon.xsetSyncQyCall(new TOutputCommonSpQyCall(xsyncQyCall())); }
            }
            return _tOutputCommon;
        }
		public class TOutputCommonSpQyCall : HpSpQyCall<TOutputCommonCQ> {
		    protected HpSpQyCall<TOutputSubCklistCQ> _qyCall;
		    public TOutputCommonSpQyCall(HpSpQyCall<TOutputSubCklistCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputCommon(); }
			public TOutputCommonCQ qy() { return _qyCall.qy().QueryTOutputCommon(); }
		}
    }
}
