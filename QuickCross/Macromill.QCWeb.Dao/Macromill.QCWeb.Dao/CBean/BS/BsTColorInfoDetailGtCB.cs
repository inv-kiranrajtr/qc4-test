
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
    public class BsTColorInfoDetailGtCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorInfoDetailGtCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_INFO_DETAIL_GT"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? colorInfoDetailGtId) {
            assertObjectNotNull("colorInfoDetailGtId", colorInfoDetailGtId);
            BsTColorInfoDetailGtCB cb = this;
            cb.Query().SetColorInfoDetailGtId_Equal(colorInfoDetailGtId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ColorInfoDetailGtId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ColorInfoDetailGtId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TColorInfoDetailGtCQ Query() {
            return this.ConditionQuery;
        }

        public TColorInfoDetailGtCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TColorInfoDetailGtCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TColorInfoDetailGtCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TColorInfoDetailGtCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TColorInfoDetailGtCB> unionQuery) {
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorInfoDetailGtCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TColorInfoDetailGtCB> unionQuery) {
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorInfoDetailGtCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TColorSetInfoGtNss _nssTColorSetInfoGt;
        public TColorSetInfoGtNss NssTColorSetInfoGt { get {
            if (_nssTColorSetInfoGt == null) { _nssTColorSetInfoGt = new TColorSetInfoGtNss(null); }
            return _nssTColorSetInfoGt;
        }}
        public TColorSetInfoGtNss SetupSelect_TColorSetInfoGt() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnColorSetInfoGtId();
            }
            doSetupSelect(delegate { return Query().QueryTColorSetInfoGt(); });
            if (_nssTColorSetInfoGt == null || !_nssTColorSetInfoGt.HasConditionQuery)
            { _nssTColorSetInfoGt = new TColorSetInfoGtNss(Query().QueryTColorSetInfoGt()); }
            return _nssTColorSetInfoGt;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TColorInfoDetailGtCBSpecification _specification;
        public TColorInfoDetailGtCBSpecification Specify() {
            if (_specification == null) { _specification = new TColorInfoDetailGtCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TColorInfoDetailGtCQ> {
			protected BsTColorInfoDetailGtCB _myCB;
			public MySpQyCall(BsTColorInfoDetailGtCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TColorInfoDetailGtCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TColorInfoDetailGtCB> ColumnQuery(SpecifyQuery<TColorInfoDetailGtCB> leftSpecifyQuery) {
            return new HpColQyOperand<TColorInfoDetailGtCB>(delegate(SpecifyQuery<TColorInfoDetailGtCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TColorInfoDetailGtCB xcreateColumnQueryCB() {
            TColorInfoDetailGtCB cb = new TColorInfoDetailGtCB();
            cb.xsetupForColumnQuery((TColorInfoDetailGtCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TColorInfoDetailGtCB> orQuery) {
            xorQ((TColorInfoDetailGtCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TColorInfoDetailGtCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TColorInfoDetailGtCBColQySpQyCall(mainCB));
        }
    }

    public class TColorInfoDetailGtCBColQySpQyCall : HpSpQyCall<TColorInfoDetailGtCQ> {
        protected TColorInfoDetailGtCB _mainCB;
        public TColorInfoDetailGtCBColQySpQyCall(TColorInfoDetailGtCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TColorInfoDetailGtCQ qy() { return _mainCB.Query(); } 
    }

    public class TColorInfoDetailGtCBSpecification : AbstractSpecification<TColorInfoDetailGtCQ> {
        protected TColorSetInfoGtCBSpecification _tColorSetInfoGt;
        public TColorInfoDetailGtCBSpecification(ConditionBean baseCB, HpSpQyCall<TColorInfoDetailGtCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnColorInfoDetailGtId() { doColumn("COLOR_INFO_DETAIL_GT_ID"); }
        public void ColumnGraphColorNo() { doColumn("GRAPH_COLOR_NO"); }
        public void ColumnColorCode() { doColumn("COLOR_CODE"); }
        public void ColumnPatternCode() { doColumn("PATTERN_CODE"); }
        public void ColumnColorSetInfoGtId() { doColumn("COLOR_SET_INFO_GT_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnColorInfoDetailGtId(); // PK
            if (qyCall().qy().hasConditionQueryTColorSetInfoGt()
                    || qyCall().qy().xgetReferrerQuery() is TColorSetInfoGtCQ) {
                ColumnColorSetInfoGtId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_COLOR_INFO_DETAIL_GT"; }
        public TColorSetInfoGtCBSpecification SpecifyTColorSetInfoGt() {
            assertForeign("tColorSetInfoGt");
            if (_tColorSetInfoGt == null) {
                _tColorSetInfoGt = new TColorSetInfoGtCBSpecification(_baseCB, new TColorSetInfoGtSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tColorSetInfoGt.xsetSyncQyCall(new TColorSetInfoGtSpQyCall(xsyncQyCall())); }
            }
            return _tColorSetInfoGt;
        }
		public class TColorSetInfoGtSpQyCall : HpSpQyCall<TColorSetInfoGtCQ> {
		    protected HpSpQyCall<TColorInfoDetailGtCQ> _qyCall;
		    public TColorSetInfoGtSpQyCall(HpSpQyCall<TColorInfoDetailGtCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTColorSetInfoGt(); }
			public TColorSetInfoGtCQ qy() { return _qyCall.qy().QueryTColorSetInfoGt(); }
		}
    }
}
