
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
    public class BsTColorInfoDetailCrossCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorInfoDetailCrossCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_INFO_DETAIL_CROSS"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? colorInfoDetailCrossId) {
            assertObjectNotNull("colorInfoDetailCrossId", colorInfoDetailCrossId);
            BsTColorInfoDetailCrossCB cb = this;
            cb.Query().SetColorInfoDetailCrossId_Equal(colorInfoDetailCrossId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ColorInfoDetailCrossId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ColorInfoDetailCrossId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TColorInfoDetailCrossCQ Query() {
            return this.ConditionQuery;
        }

        public TColorInfoDetailCrossCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TColorInfoDetailCrossCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TColorInfoDetailCrossCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TColorInfoDetailCrossCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TColorInfoDetailCrossCB> unionQuery) {
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorInfoDetailCrossCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TColorInfoDetailCrossCB> unionQuery) {
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TColorInfoDetailCrossCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TColorSetInfoCrossNss _nssTColorSetInfoCross;
        public TColorSetInfoCrossNss NssTColorSetInfoCross { get {
            if (_nssTColorSetInfoCross == null) { _nssTColorSetInfoCross = new TColorSetInfoCrossNss(null); }
            return _nssTColorSetInfoCross;
        }}
        public TColorSetInfoCrossNss SetupSelect_TColorSetInfoCross() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnColorSetInfoCrossId();
            }
            doSetupSelect(delegate { return Query().QueryTColorSetInfoCross(); });
            if (_nssTColorSetInfoCross == null || !_nssTColorSetInfoCross.HasConditionQuery)
            { _nssTColorSetInfoCross = new TColorSetInfoCrossNss(Query().QueryTColorSetInfoCross()); }
            return _nssTColorSetInfoCross;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TColorInfoDetailCrossCBSpecification _specification;
        public TColorInfoDetailCrossCBSpecification Specify() {
            if (_specification == null) { _specification = new TColorInfoDetailCrossCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TColorInfoDetailCrossCQ> {
			protected BsTColorInfoDetailCrossCB _myCB;
			public MySpQyCall(BsTColorInfoDetailCrossCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TColorInfoDetailCrossCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TColorInfoDetailCrossCB> ColumnQuery(SpecifyQuery<TColorInfoDetailCrossCB> leftSpecifyQuery) {
            return new HpColQyOperand<TColorInfoDetailCrossCB>(delegate(SpecifyQuery<TColorInfoDetailCrossCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TColorInfoDetailCrossCB xcreateColumnQueryCB() {
            TColorInfoDetailCrossCB cb = new TColorInfoDetailCrossCB();
            cb.xsetupForColumnQuery((TColorInfoDetailCrossCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TColorInfoDetailCrossCB> orQuery) {
            xorQ((TColorInfoDetailCrossCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TColorInfoDetailCrossCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TColorInfoDetailCrossCBColQySpQyCall(mainCB));
        }
    }

    public class TColorInfoDetailCrossCBColQySpQyCall : HpSpQyCall<TColorInfoDetailCrossCQ> {
        protected TColorInfoDetailCrossCB _mainCB;
        public TColorInfoDetailCrossCBColQySpQyCall(TColorInfoDetailCrossCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TColorInfoDetailCrossCQ qy() { return _mainCB.Query(); } 
    }

    public class TColorInfoDetailCrossCBSpecification : AbstractSpecification<TColorInfoDetailCrossCQ> {
        protected TColorSetInfoCrossCBSpecification _tColorSetInfoCross;
        public TColorInfoDetailCrossCBSpecification(ConditionBean baseCB, HpSpQyCall<TColorInfoDetailCrossCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnColorInfoDetailCrossId() { doColumn("COLOR_INFO_DETAIL_CROSS_ID"); }
        public void ColumnGraphColorNo() { doColumn("GRAPH_COLOR_NO"); }
        public void ColumnColorCode() { doColumn("COLOR_CODE"); }
        public void ColumnPatternCode() { doColumn("PATTERN_CODE"); }
        public void ColumnColorSetInfoCrossId() { doColumn("COLOR_SET_INFO_CROSS_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnColorInfoDetailCrossId(); // PK
            if (qyCall().qy().hasConditionQueryTColorSetInfoCross()
                    || qyCall().qy().xgetReferrerQuery() is TColorSetInfoCrossCQ) {
                ColumnColorSetInfoCrossId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_COLOR_INFO_DETAIL_CROSS"; }
        public TColorSetInfoCrossCBSpecification SpecifyTColorSetInfoCross() {
            assertForeign("tColorSetInfoCross");
            if (_tColorSetInfoCross == null) {
                _tColorSetInfoCross = new TColorSetInfoCrossCBSpecification(_baseCB, new TColorSetInfoCrossSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tColorSetInfoCross.xsetSyncQyCall(new TColorSetInfoCrossSpQyCall(xsyncQyCall())); }
            }
            return _tColorSetInfoCross;
        }
		public class TColorSetInfoCrossSpQyCall : HpSpQyCall<TColorSetInfoCrossCQ> {
		    protected HpSpQyCall<TColorInfoDetailCrossCQ> _qyCall;
		    public TColorSetInfoCrossSpQyCall(HpSpQyCall<TColorInfoDetailCrossCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTColorSetInfoCross(); }
			public TColorSetInfoCrossCQ qy() { return _qyCall.qy().QueryTColorSetInfoCross(); }
		}
    }
}
