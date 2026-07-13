
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
    public class BsTOutputSubGtCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubGtCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_GT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputSubGtId) {
            assertObjectNotNull("outputSubGtId", outputSubGtId);
            BsTOutputSubGtCB cb = this;
            cb.Query().SetOutputSubGtId_Equal(outputSubGtId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputSubGtId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputSubGtId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputSubGtCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSubGtCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSubGtCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSubGtCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSubGtCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSubGtCB> unionQuery) {
            TOutputSubGtCB cb = new TOutputSubGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubGtCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSubGtCB> unionQuery) {
            TOutputSubGtCB cb = new TOutputSubGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubGtCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputSubGtCBSpecification _specification;
        public TOutputSubGtCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSubGtCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSubGtCQ> {
			protected BsTOutputSubGtCB _myCB;
			public MySpQyCall(BsTOutputSubGtCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSubGtCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSubGtCB> ColumnQuery(SpecifyQuery<TOutputSubGtCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSubGtCB>(delegate(SpecifyQuery<TOutputSubGtCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSubGtCB xcreateColumnQueryCB() {
            TOutputSubGtCB cb = new TOutputSubGtCB();
            cb.xsetupForColumnQuery((TOutputSubGtCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSubGtCB> orQuery) {
            xorQ((TOutputSubGtCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSubGtCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSubGtCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSubGtCBColQySpQyCall : HpSpQyCall<TOutputSubGtCQ> {
        protected TOutputSubGtCB _mainCB;
        public TOutputSubGtCBColQySpQyCall(TOutputSubGtCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSubGtCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSubGtCBSpecification : AbstractSpecification<TOutputSubGtCQ> {
        protected TOutputCommonCBSpecification _tOutputCommon;
        public TOutputSubGtCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSubGtCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputSubGtId() { doColumn("OUTPUT_SUB_GT_ID"); }
        public void ColumnOutputCommonId() { doColumn("OUTPUT_COMMON_ID"); }
        public void ColumnOutputTableType() { doColumn("OUTPUT_TABLE_TYPE"); }
        public void ColumnOutputTableOrientation() { doColumn("OUTPUT_TABLE_ORIENTATION"); }
        public void ColumnPageSettingTableType() { doColumn("PAGE_SETTING_TABLE_TYPE"); }
        public void ColumnPageSettingPaperSize() { doColumn("PAGE_SETTING_PAPER_SIZE"); }
        public void ColumnPageSettingPaperOrientation() { doColumn("PAGE_SETTING_PAPER_ORIENTATION"); }
        public void ColumnMarkingLevel() { doColumn("MARKING_LEVEL"); }
        public void ColumnMarkingMinParameter() { doColumn("MARKING_MIN_PARAMETER"); }
        public void ColumnMarkingCode() { doColumn("MARKING_CODE"); }
        public void ColumnFilteringExpression() { doColumn("FILTERING_EXPRESSION"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputSubGtId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputCommon()
                    || qyCall().qy().xgetReferrerQuery() is TOutputCommonCQ) {
                ColumnOutputCommonId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_SUB_GT"; }
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
		    protected HpSpQyCall<TOutputSubGtCQ> _qyCall;
		    public TOutputCommonSpQyCall(HpSpQyCall<TOutputSubGtCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputCommon(); }
			public TOutputCommonCQ qy() { return _qyCall.qy().QueryTOutputCommon(); }
		}
    }
}
