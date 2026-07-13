
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
    public class BsTDefaultEnvColorInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? defEnvColorInfoId) {
            assertObjectNotNull("defEnvColorInfoId", defEnvColorInfoId);
            BsTDefaultEnvColorInfoCB cb = this;
            cb.Query().SetDefEnvColorInfoId_Equal(defEnvColorInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DefEnvColorInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DefEnvColorInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDefaultEnvColorInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TDefaultEnvColorInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDefaultEnvColorInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDefaultEnvColorInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDefaultEnvColorInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDefaultEnvColorInfoCB> unionQuery) {
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDefaultEnvColorInfoCB> unionQuery) {
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDefaultEnvNss _nssTDefaultEnv;
        public TDefaultEnvNss NssTDefaultEnv { get {
            if (_nssTDefaultEnv == null) { _nssTDefaultEnv = new TDefaultEnvNss(null); }
            return _nssTDefaultEnv;
        }}
        public TDefaultEnvNss SetupSelect_TDefaultEnv() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTDefaultEnv(); });
            if (_nssTDefaultEnv == null || !_nssTDefaultEnv.HasConditionQuery)
            { _nssTDefaultEnv = new TDefaultEnvNss(Query().QueryTDefaultEnv()); }
            return _nssTDefaultEnv;
        }
        protected TDefaultEnvColorDtlNss _nssTDefaultEnvColorDtl;
        public TDefaultEnvColorDtlNss NssTDefaultEnvColorDtl { get {
            if (_nssTDefaultEnvColorDtl == null) { _nssTDefaultEnvColorDtl = new TDefaultEnvColorDtlNss(null); }
            return _nssTDefaultEnvColorDtl;
        }}
        public TDefaultEnvColorDtlNss SetupSelect_TDefaultEnvColorDtl() {
            doSetupSelect(delegate { return Query().QueryTDefaultEnvColorDtl(); });
            if (_nssTDefaultEnvColorDtl == null || !_nssTDefaultEnvColorDtl.HasConditionQuery)
            { _nssTDefaultEnvColorDtl = new TDefaultEnvColorDtlNss(Query().QueryTDefaultEnvColorDtl()); }
            return _nssTDefaultEnvColorDtl;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDefaultEnvColorInfoCBSpecification _specification;
        public TDefaultEnvColorInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TDefaultEnvColorInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDefaultEnvColorInfoCQ> {
			protected BsTDefaultEnvColorInfoCB _myCB;
			public MySpQyCall(BsTDefaultEnvColorInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDefaultEnvColorInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDefaultEnvColorInfoCB> ColumnQuery(SpecifyQuery<TDefaultEnvColorInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDefaultEnvColorInfoCB>(delegate(SpecifyQuery<TDefaultEnvColorInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDefaultEnvColorInfoCB xcreateColumnQueryCB() {
            TDefaultEnvColorInfoCB cb = new TDefaultEnvColorInfoCB();
            cb.xsetupForColumnQuery((TDefaultEnvColorInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDefaultEnvColorInfoCB> orQuery) {
            xorQ((TDefaultEnvColorInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDefaultEnvColorInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDefaultEnvColorInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TDefaultEnvColorInfoCBColQySpQyCall : HpSpQyCall<TDefaultEnvColorInfoCQ> {
        protected TDefaultEnvColorInfoCB _mainCB;
        public TDefaultEnvColorInfoCBColQySpQyCall(TDefaultEnvColorInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDefaultEnvColorInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TDefaultEnvColorInfoCBSpecification : AbstractSpecification<TDefaultEnvColorInfoCQ> {
        protected TDefaultEnvCBSpecification _tDefaultEnv;
        protected TDefaultEnvColorDtlCBSpecification _tDefaultEnvColorDtl;
        public TDefaultEnvColorInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TDefaultEnvColorInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDefEnvColorInfoId() { doColumn("DEF_ENV_COLOR_INFO_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnTypeCode() { doColumn("TYPE_CODE"); }
        public void ColumnGradationType() { doColumn("GRADATION_TYPE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDefEnvColorInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTDefaultEnv()
                    || qyCall().qy().xgetReferrerQuery() is TDefaultEnvCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_INFO"; }
        public TDefaultEnvCBSpecification SpecifyTDefaultEnv() {
            assertForeign("tDefaultEnv");
            if (_tDefaultEnv == null) {
                _tDefaultEnv = new TDefaultEnvCBSpecification(_baseCB, new TDefaultEnvSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnv.xsetSyncQyCall(new TDefaultEnvSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnv;
        }
		public class TDefaultEnvSpQyCall : HpSpQyCall<TDefaultEnvCQ> {
		    protected HpSpQyCall<TDefaultEnvColorInfoCQ> _qyCall;
		    public TDefaultEnvSpQyCall(HpSpQyCall<TDefaultEnvColorInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnv(); }
			public TDefaultEnvCQ qy() { return _qyCall.qy().QueryTDefaultEnv(); }
		}
        public TDefaultEnvColorDtlCBSpecification SpecifyTDefaultEnvColorDtl() {
            assertForeign("tDefaultEnvColorDtl");
            if (_tDefaultEnvColorDtl == null) {
                _tDefaultEnvColorDtl = new TDefaultEnvColorDtlCBSpecification(_baseCB, new TDefaultEnvColorDtlSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnvColorDtl.xsetSyncQyCall(new TDefaultEnvColorDtlSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnvColorDtl;
        }
		public class TDefaultEnvColorDtlSpQyCall : HpSpQyCall<TDefaultEnvColorDtlCQ> {
		    protected HpSpQyCall<TDefaultEnvColorInfoCQ> _qyCall;
		    public TDefaultEnvColorDtlSpQyCall(HpSpQyCall<TDefaultEnvColorInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnvColorDtl(); }
			public TDefaultEnvColorDtlCQ qy() { return _qyCall.qy().QueryTDefaultEnvColorDtl(); }
		}
        public RAFunction<TDefaultEnvColorDtlCB, TDefaultEnvColorInfoCQ> DerivedTDefaultEnvColorDtlList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDefaultEnvColorDtlCB, TDefaultEnvColorInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDefaultEnvColorDtlCB> subQuery, TDefaultEnvColorInfoCQ cq, String aliasName)
                { cq.xsderiveTDefaultEnvColorDtlList(function, subQuery, aliasName); });
        }
    }
}
