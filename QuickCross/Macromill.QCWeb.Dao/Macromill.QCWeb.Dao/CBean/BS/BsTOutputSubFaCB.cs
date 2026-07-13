
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
    public class BsTOutputSubFaCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubFaCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_FA"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputSubFaId) {
            assertObjectNotNull("outputSubFaId", outputSubFaId);
            BsTOutputSubFaCB cb = this;
            cb.Query().SetOutputSubFaId_Equal(outputSubFaId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputSubFaId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputSubFaId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputSubFaCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSubFaCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSubFaCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSubFaCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSubFaCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSubFaCB> unionQuery) {
            TOutputSubFaCB cb = new TOutputSubFaCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubFaCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSubFaCB> unionQuery) {
            TOutputSubFaCB cb = new TOutputSubFaCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubFaCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputSubFaCBSpecification _specification;
        public TOutputSubFaCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSubFaCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSubFaCQ> {
			protected BsTOutputSubFaCB _myCB;
			public MySpQyCall(BsTOutputSubFaCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSubFaCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSubFaCB> ColumnQuery(SpecifyQuery<TOutputSubFaCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSubFaCB>(delegate(SpecifyQuery<TOutputSubFaCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSubFaCB xcreateColumnQueryCB() {
            TOutputSubFaCB cb = new TOutputSubFaCB();
            cb.xsetupForColumnQuery((TOutputSubFaCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSubFaCB> orQuery) {
            xorQ((TOutputSubFaCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSubFaCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSubFaCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSubFaCBColQySpQyCall : HpSpQyCall<TOutputSubFaCQ> {
        protected TOutputSubFaCB _mainCB;
        public TOutputSubFaCBColQySpQyCall(TOutputSubFaCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSubFaCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSubFaCBSpecification : AbstractSpecification<TOutputSubFaCQ> {
        protected TOutputCommonCBSpecification _tOutputCommon;
        public TOutputSubFaCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSubFaCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputSubFaId() { doColumn("OUTPUT_SUB_FA_ID"); }
        public void ColumnOutputCommonId() { doColumn("OUTPUT_COMMON_ID"); }
        public void ColumnPageSettingPaperSize() { doColumn("PAGE_SETTING_PAPER_SIZE"); }
        public void ColumnPageSettingPaperOrientation() { doColumn("PAGE_SETTING_PAPER_ORIENTATION"); }
        public void ColumnFilteringExpression() { doColumn("FILTERING_EXPRESSION"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputSubFaId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputCommon()
                    || qyCall().qy().xgetReferrerQuery() is TOutputCommonCQ) {
                ColumnOutputCommonId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_SUB_FA"; }
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
		    protected HpSpQyCall<TOutputSubFaCQ> _qyCall;
		    public TOutputCommonSpQyCall(HpSpQyCall<TOutputSubFaCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputCommon(); }
			public TOutputCommonCQ qy() { return _qyCall.qy().QueryTOutputCommon(); }
		}
    }
}
