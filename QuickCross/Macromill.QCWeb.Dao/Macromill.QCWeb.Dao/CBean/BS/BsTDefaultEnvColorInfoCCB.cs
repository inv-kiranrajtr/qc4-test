
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
    public class BsTDefaultEnvColorInfoCCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorInfoCCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO_C"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(int? defEnvColorInfoCId) {
            assertObjectNotNull("defEnvColorInfoCId", defEnvColorInfoCId);
            BsTDefaultEnvColorInfoCCB cb = this;
            cb.Query().SetDefEnvColorInfoCId_Equal(defEnvColorInfoCId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DefEnvColorInfoCId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DefEnvColorInfoCId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDefaultEnvColorInfoCCQ Query() {
            return this.ConditionQuery;
        }

        public TDefaultEnvColorInfoCCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDefaultEnvColorInfoCCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDefaultEnvColorInfoCCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDefaultEnvColorInfoCCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDefaultEnvColorInfoCCB> unionQuery) {
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorInfoCCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDefaultEnvColorInfoCCB> unionQuery) {
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvColorInfoCCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDefaultEnvBaseNss _nssTDefaultEnvBase;
        public TDefaultEnvBaseNss NssTDefaultEnvBase { get {
            if (_nssTDefaultEnvBase == null) { _nssTDefaultEnvBase = new TDefaultEnvBaseNss(null); }
            return _nssTDefaultEnvBase;
        }}
        public TDefaultEnvBaseNss SetupSelect_TDefaultEnvBase() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnLanguage();
            }
            doSetupSelect(delegate { return Query().QueryTDefaultEnvBase(); });
            if (_nssTDefaultEnvBase == null || !_nssTDefaultEnvBase.HasConditionQuery)
            { _nssTDefaultEnvBase = new TDefaultEnvBaseNss(Query().QueryTDefaultEnvBase()); }
            return _nssTDefaultEnvBase;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDefaultEnvColorInfoCCBSpecification _specification;
        public TDefaultEnvColorInfoCCBSpecification Specify() {
            if (_specification == null) { _specification = new TDefaultEnvColorInfoCCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDefaultEnvColorInfoCCQ> {
			protected BsTDefaultEnvColorInfoCCB _myCB;
			public MySpQyCall(BsTDefaultEnvColorInfoCCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDefaultEnvColorInfoCCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDefaultEnvColorInfoCCB> ColumnQuery(SpecifyQuery<TDefaultEnvColorInfoCCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDefaultEnvColorInfoCCB>(delegate(SpecifyQuery<TDefaultEnvColorInfoCCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDefaultEnvColorInfoCCB xcreateColumnQueryCB() {
            TDefaultEnvColorInfoCCB cb = new TDefaultEnvColorInfoCCB();
            cb.xsetupForColumnQuery((TDefaultEnvColorInfoCCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDefaultEnvColorInfoCCB> orQuery) {
            xorQ((TDefaultEnvColorInfoCCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDefaultEnvColorInfoCCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDefaultEnvColorInfoCCBColQySpQyCall(mainCB));
        }
    }

    public class TDefaultEnvColorInfoCCBColQySpQyCall : HpSpQyCall<TDefaultEnvColorInfoCCQ> {
        protected TDefaultEnvColorInfoCCB _mainCB;
        public TDefaultEnvColorInfoCCBColQySpQyCall(TDefaultEnvColorInfoCCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDefaultEnvColorInfoCCQ qy() { return _mainCB.Query(); } 
    }

    public class TDefaultEnvColorInfoCCBSpecification : AbstractSpecification<TDefaultEnvColorInfoCCQ> {
        protected TDefaultEnvBaseCBSpecification _tDefaultEnvBase;
        public TDefaultEnvColorInfoCCBSpecification(ConditionBean baseCB, HpSpQyCall<TDefaultEnvColorInfoCCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDefEnvColorInfoCId() { doColumn("DEF_ENV_COLOR_INFO_C_ID"); }
        public void ColumnLanguage() { doColumn("LANGUAGE"); }
        public void ColumnTypeCode() { doColumn("TYPE_CODE"); }
        public void ColumnGradationType() { doColumn("GRADATION_TYPE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDefEnvColorInfoCId(); // PK
            if (qyCall().qy().hasConditionQueryTDefaultEnvBase()
                    || qyCall().qy().xgetReferrerQuery() is TDefaultEnvBaseCQ) {
                ColumnLanguage(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DEFAULT_ENV_COLOR_INFO_C"; }
        public TDefaultEnvBaseCBSpecification SpecifyTDefaultEnvBase() {
            assertForeign("tDefaultEnvBase");
            if (_tDefaultEnvBase == null) {
                _tDefaultEnvBase = new TDefaultEnvBaseCBSpecification(_baseCB, new TDefaultEnvBaseSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDefaultEnvBase.xsetSyncQyCall(new TDefaultEnvBaseSpQyCall(xsyncQyCall())); }
            }
            return _tDefaultEnvBase;
        }
		public class TDefaultEnvBaseSpQyCall : HpSpQyCall<TDefaultEnvBaseCQ> {
		    protected HpSpQyCall<TDefaultEnvColorInfoCCQ> _qyCall;
		    public TDefaultEnvBaseSpQyCall(HpSpQyCall<TDefaultEnvColorInfoCCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDefaultEnvBase(); }
			public TDefaultEnvBaseCQ qy() { return _qyCall.qy().QueryTDefaultEnvBase(); }
		}
        public RAFunction<TDefaultEnvColorDtlCCB, TDefaultEnvColorInfoCCQ> DerivedTDefaultEnvColorDtlCList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDefaultEnvColorDtlCCB, TDefaultEnvColorInfoCCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDefaultEnvColorDtlCCB> subQuery, TDefaultEnvColorInfoCCQ cq, String aliasName)
                { cq.xsderiveTDefaultEnvColorDtlCList(function, subQuery, aliasName); });
        }
    }
}
