
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
    public class BsTDefaultEnvColorDtlCCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorDtlCCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL_C"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(int? defEnvColorDtlCId) {
            assertObjectNotNull("defEnvColorDtlCId", defEnvColorDtlCId);
            BsTDefaultEnvColorDtlCCB cb = this;
            cb.Query().SetDefEnvColorDtlCId_Equal(defEnvColorDtlCId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DefEnvColorDtlCId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DefEnvColorDtlCId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDefaultEnvColorDtlCCQ Query() {
            return this.ConditionQuery;
        }

        public TDefaultEnvColorDtlCCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDefaultEnvColorDtlCCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDefaultEnvColorDtlCCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDefaultEnvColorDtlCCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDefaultEnvColorDtlCCB> unionQuery) {
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorDtlCCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDefaultEnvColorDtlCCB> unionQuery) {
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorDtlCCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDefaultEnvColorInfoCNss _nssTDefaultEnvColorInfoC;
        public TDefaultEnvColorInfoCNss NssTDefaultEnvColorInfoC { get {
            if (_nssTDefaultEnvColorInfoC == null) { _nssTDefaultEnvColorInfoC = new TDefaultEnvColorInfoCNss(null); }
            return _nssTDefaultEnvColorInfoC;
        }}
        public TDefaultEnvColorInfoCNss SetupSelect_TDefaultEnvColorInfoC() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDefEnvColorInfoCId();
            }
            doSetupSelect(delegate { return Query().QueryTDefaultEnvColorInfoC(); });
            if (_nssTDefaultEnvColorInfoC == null || !_nssTDefaultEnvColorInfoC.HasConditionQuery)
            { _nssTDefaultEnvColorInfoC = new TDefaultEnvColorInfoCNss(Query().QueryTDefaultEnvColorInfoC()); }
            return _nssTDefaultEnvColorInfoC;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDefaultEnvColorDtlCCBSpecification _specification;
        public TDefaultEnvColorDtlCCBSpecification Specify() {
            if (_specification == null) { _specification = new TDefaultEnvColorDtlCCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDefaultEnvColorDtlCCQ> {
			protected BsTDefaultEnvColorDtlCCB _myCB;
			public MySpQyCall(BsTDefaultEnvColorDtlCCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDefaultEnvColorDtlCCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDefaultEnvColorDtlCCB> ColumnQuery(SpecifyQuery<TDefaultEnvColorDtlCCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDefaultEnvColorDtlCCB>(delegate(SpecifyQuery<TDefaultEnvColorDtlCCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDefaultEnvColorDtlCCB xcreateColumnQueryCB() {
            TDefaultEnvColorDtlCCB cb = new TDefaultEnvColorDtlCCB();
            cb.xsetupForColumnQuery((TDefaultEnvColorDtlCCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDefaultEnvColorDtlCCB> orQuery) {
            xorQ((TDefaultEnvColorDtlCCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDefaultEnvColorDtlCCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDefaultEnvColorDtlCCBColQySpQyCall(mainCB));
        }
    }

    public class TDefaultEnvColorDtlCCBColQySpQyCall : HpSpQyCall<TDefaultEnvColorDtlCCQ> {
        protected TDefaultEnvColorDtlCCB _mainCB;
        public TDefaultEnvColorDtlCCBColQySpQyCall(TDefaultEnvColorDtlCCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDefaultEnvColorDtlCCQ qy() { return _mainCB.Query(); } 
    }

    public class TDefaultEnvColorDtlCCBSpecification : AbstractSpecification<TDefaultEnvColorDtlCCQ> {
        protected TDefaultEnvColorInfoCCBSpecification _tDefaultEnvColorInfoC;
        public TDefaultEnvColorDtlCCBSpecification(ConditionBean baseCB, HpSpQyCall<TDefaultEnvColorDtlCCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDefEnvColorDtlCId() { doColumn("DEF_ENV_COLOR_DTL_C_ID"); }
        public void ColumnDefEnvColorInfoCId() { doColumn("DEF_ENV_COLOR_INFO_C_ID"); }
        public void ColumnGraphColorNo() { doColumn("GRAPH_COLOR_NO"); }
        public void ColumnColorCode() { doColumn("COLOR_CODE"); }
        public void ColumnPatternCode() { doColumn("PATTERN_CODE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDefEnvColorDtlCId(); // PK
            if (qyCall().qy().hasConditionQueryTDefaultEnvColorInfoC()
                    || qyCall().qy().xgetReferrerQuery() is TDefaultEnvColorInfoCCQ) {
                ColumnDefEnvColorInfoCId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_DTL_C"; }
        public TDefaultEnvColorInfoCCBSpecification SpecifyTDefaultEnvColorInfoC() {
            assertForeign("tDefaultEnvColorInfoC");
            if (_tDefaultEnvColorInfoC == null) {
                _tDefaultEnvColorInfoC = new TDefaultEnvColorInfoCCBSpecification(_baseCB, new TDefaultEnvColorInfoCSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnvColorInfoC.xsetSyncQyCall(new TDefaultEnvColorInfoCSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnvColorInfoC;
        }
		public class TDefaultEnvColorInfoCSpQyCall : HpSpQyCall<TDefaultEnvColorInfoCCQ> {
		    protected HpSpQyCall<TDefaultEnvColorDtlCCQ> _qyCall;
		    public TDefaultEnvColorInfoCSpQyCall(HpSpQyCall<TDefaultEnvColorDtlCCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnvColorInfoC(); }
			public TDefaultEnvColorInfoCCQ qy() { return _qyCall.qy().QueryTDefaultEnvColorInfoC(); }
		}
    }
}
