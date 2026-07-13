
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
    public class BsTWeightbackValueCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TWeightbackValueCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_WEIGHTBACK_VALUE"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? weightbackValueId) {
            assertObjectNotNull("weightbackValueId", weightbackValueId);
            BsTWeightbackValueCB cb = this;
            cb.Query().SetWeightbackValueId_Equal(weightbackValueId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_WeightbackValueId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_WeightbackValueId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TWeightbackValueCQ Query() {
            return this.ConditionQuery;
        }

        public TWeightbackValueCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TWeightbackValueCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TWeightbackValueCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TWeightbackValueCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TWeightbackValueCB> unionQuery) {
            TWeightbackValueCB cb = new TWeightbackValueCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TWeightbackValueCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TWeightbackValueCB> unionQuery) {
            TWeightbackValueCB cb = new TWeightbackValueCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TWeightbackValueCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TWeightbackNss _nssTWeightback;
        public TWeightbackNss NssTWeightback { get {
            if (_nssTWeightback == null) { _nssTWeightback = new TWeightbackNss(null); }
            return _nssTWeightback;
        }}
        public TWeightbackNss SetupSelect_TWeightback() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnWeightbackId();
            }
            doSetupSelect(delegate { return Query().QueryTWeightback(); });
            if (_nssTWeightback == null || !_nssTWeightback.HasConditionQuery)
            { _nssTWeightback = new TWeightbackNss(Query().QueryTWeightback()); }
            return _nssTWeightback;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TWeightbackValueCBSpecification _specification;
        public TWeightbackValueCBSpecification Specify() {
            if (_specification == null) { _specification = new TWeightbackValueCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TWeightbackValueCQ> {
			protected BsTWeightbackValueCB _myCB;
			public MySpQyCall(BsTWeightbackValueCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TWeightbackValueCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TWeightbackValueCB> ColumnQuery(SpecifyQuery<TWeightbackValueCB> leftSpecifyQuery) {
            return new HpColQyOperand<TWeightbackValueCB>(delegate(SpecifyQuery<TWeightbackValueCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TWeightbackValueCB xcreateColumnQueryCB() {
            TWeightbackValueCB cb = new TWeightbackValueCB();
            cb.xsetupForColumnQuery((TWeightbackValueCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TWeightbackValueCB> orQuery) {
            xorQ((TWeightbackValueCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TWeightbackValueCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TWeightbackValueCBColQySpQyCall(mainCB));
        }
    }

    public class TWeightbackValueCBColQySpQyCall : HpSpQyCall<TWeightbackValueCQ> {
        protected TWeightbackValueCB _mainCB;
        public TWeightbackValueCBColQySpQyCall(TWeightbackValueCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TWeightbackValueCQ qy() { return _mainCB.Query(); } 
    }

    public class TWeightbackValueCBSpecification : AbstractSpecification<TWeightbackValueCQ> {
        protected TWeightbackCBSpecification _tWeightback;
        public TWeightbackValueCBSpecification(ConditionBean baseCB, HpSpQyCall<TWeightbackValueCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnWeightbackValueId() { doColumn("WEIGHTBACK_VALUE_ID"); }
        public void ColumnWeightbackItemNo() { doColumn("WEIGHTBACK_ITEM_NO"); }
        public void ColumnPercentValue() { doColumn("PERCENT_VALUE"); }
        public void ColumnParameterNValue() { doColumn("PARAMETER_N_VALUE"); }
        public void ColumnWeightbackValue() { doColumn("WEIGHTBACK_VALUE"); }
        public void ColumnWeightbackId() { doColumn("WEIGHTBACK_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnWeightbackValueId(); // PK
            if (qyCall().qy().hasConditionQueryTWeightback()
                    || qyCall().qy().xgetReferrerQuery() is TWeightbackCQ) {
                ColumnWeightbackId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_WEIGHTBACK_VALUE"; }
        public TWeightbackCBSpecification SpecifyTWeightback() {
            assertForeign("tWeightback");
            if (_tWeightback == null) {
                _tWeightback = new TWeightbackCBSpecification(_baseCB, new TWeightbackSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tWeightback.xsetSyncQyCall(new TWeightbackSpQyCall(xsyncQyCall())); }
            }
            return _tWeightback;
        }
		public class TWeightbackSpQyCall : HpSpQyCall<TWeightbackCQ> {
		    protected HpSpQyCall<TWeightbackValueCQ> _qyCall;
		    public TWeightbackSpQyCall(HpSpQyCall<TWeightbackValueCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTWeightback(); }
			public TWeightbackCQ qy() { return _qyCall.qy().QueryTWeightback(); }
		}
    }
}
