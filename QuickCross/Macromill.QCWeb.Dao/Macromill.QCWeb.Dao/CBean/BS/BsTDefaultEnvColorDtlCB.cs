
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
    public class BsTDefaultEnvColorDtlCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorDtlCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? defEnvColorDtlId) {
            assertObjectNotNull("defEnvColorDtlId", defEnvColorDtlId);
            BsTDefaultEnvColorDtlCB cb = this;
            cb.Query().SetDefEnvColorDtlId_Equal(defEnvColorDtlId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DefEnvColorDtlId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DefEnvColorDtlId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDefaultEnvColorDtlCQ Query() {
            return this.ConditionQuery;
        }

        public TDefaultEnvColorDtlCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDefaultEnvColorDtlCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDefaultEnvColorDtlCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDefaultEnvColorDtlCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDefaultEnvColorDtlCB> unionQuery) {
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorDtlCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDefaultEnvColorDtlCB> unionQuery) {
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorDtlCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDefaultEnvColorInfoNss _nssTDefaultEnvColorInfo;
        public TDefaultEnvColorInfoNss NssTDefaultEnvColorInfo { get {
            if (_nssTDefaultEnvColorInfo == null) { _nssTDefaultEnvColorInfo = new TDefaultEnvColorInfoNss(null); }
            return _nssTDefaultEnvColorInfo;
        }}
        public TDefaultEnvColorInfoNss SetupSelect_TDefaultEnvColorInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDefEnvColorInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTDefaultEnvColorInfo(); });
            if (_nssTDefaultEnvColorInfo == null || !_nssTDefaultEnvColorInfo.HasConditionQuery)
            { _nssTDefaultEnvColorInfo = new TDefaultEnvColorInfoNss(Query().QueryTDefaultEnvColorInfo()); }
            return _nssTDefaultEnvColorInfo;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDefaultEnvColorDtlCBSpecification _specification;
        public TDefaultEnvColorDtlCBSpecification Specify() {
            if (_specification == null) { _specification = new TDefaultEnvColorDtlCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDefaultEnvColorDtlCQ> {
			protected BsTDefaultEnvColorDtlCB _myCB;
			public MySpQyCall(BsTDefaultEnvColorDtlCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDefaultEnvColorDtlCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDefaultEnvColorDtlCB> ColumnQuery(SpecifyQuery<TDefaultEnvColorDtlCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDefaultEnvColorDtlCB>(delegate(SpecifyQuery<TDefaultEnvColorDtlCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDefaultEnvColorDtlCB xcreateColumnQueryCB() {
            TDefaultEnvColorDtlCB cb = new TDefaultEnvColorDtlCB();
            cb.xsetupForColumnQuery((TDefaultEnvColorDtlCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDefaultEnvColorDtlCB> orQuery) {
            xorQ((TDefaultEnvColorDtlCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDefaultEnvColorDtlCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDefaultEnvColorDtlCBColQySpQyCall(mainCB));
        }
    }

    public class TDefaultEnvColorDtlCBColQySpQyCall : HpSpQyCall<TDefaultEnvColorDtlCQ> {
        protected TDefaultEnvColorDtlCB _mainCB;
        public TDefaultEnvColorDtlCBColQySpQyCall(TDefaultEnvColorDtlCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDefaultEnvColorDtlCQ qy() { return _mainCB.Query(); } 
    }

    public class TDefaultEnvColorDtlCBSpecification : AbstractSpecification<TDefaultEnvColorDtlCQ> {
        protected TDefaultEnvColorInfoCBSpecification _tDefaultEnvColorInfo;
        public TDefaultEnvColorDtlCBSpecification(ConditionBean baseCB, HpSpQyCall<TDefaultEnvColorDtlCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDefEnvColorDtlId() { doColumn("DEF_ENV_COLOR_DTL_ID"); }
        public void ColumnDefEnvColorInfoId() { doColumn("DEF_ENV_COLOR_INFO_ID"); }
        public void ColumnGraphColorNo() { doColumn("GRAPH_COLOR_NO"); }
        public void ColumnColorCode() { doColumn("COLOR_CODE"); }
        public void ColumnPatternCode() { doColumn("PATTERN_CODE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDefEnvColorDtlId(); // PK
            if (qyCall().qy().hasConditionQueryTDefaultEnvColorInfo()
                    || qyCall().qy().xgetReferrerQuery() is TDefaultEnvColorInfoCQ) {
                ColumnDefEnvColorInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_DTL"; }
        public TDefaultEnvColorInfoCBSpecification SpecifyTDefaultEnvColorInfo() {
            assertForeign("tDefaultEnvColorInfo");
            if (_tDefaultEnvColorInfo == null) {
                _tDefaultEnvColorInfo = new TDefaultEnvColorInfoCBSpecification(_baseCB, new TDefaultEnvColorInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnvColorInfo.xsetSyncQyCall(new TDefaultEnvColorInfoSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnvColorInfo;
        }
		public class TDefaultEnvColorInfoSpQyCall : HpSpQyCall<TDefaultEnvColorInfoCQ> {
		    protected HpSpQyCall<TDefaultEnvColorDtlCQ> _qyCall;
		    public TDefaultEnvColorInfoSpQyCall(HpSpQyCall<TDefaultEnvColorDtlCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnvColorInfo(); }
			public TDefaultEnvColorInfoCQ qy() { return _qyCall.qy().QueryTDefaultEnvColorInfo(); }
		}
    }
}
