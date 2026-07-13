
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
    public class BsTScenarioQuerylistCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TScenarioQuerylistCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SCENARIO_QUERYLIST"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? scenarioQuerylistId) {
            assertObjectNotNull("scenarioQuerylistId", scenarioQuerylistId);
            BsTScenarioQuerylistCB cb = this;
            cb.Query().SetScenarioQuerylistId_Equal(scenarioQuerylistId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ScenarioQuerylistId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ScenarioQuerylistId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TScenarioQuerylistCQ Query() {
            return this.ConditionQuery;
        }

        public TScenarioQuerylistCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TScenarioQuerylistCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TScenarioQuerylistCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TScenarioQuerylistCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TScenarioQuerylistCB> unionQuery) {
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TScenarioQuerylistCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TScenarioQuerylistCB> unionQuery) {
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TScenarioQuerylistCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TScenarioTotalizationNss _nssTScenarioTotalization;
        public TScenarioTotalizationNss NssTScenarioTotalization { get {
            if (_nssTScenarioTotalization == null) { _nssTScenarioTotalization = new TScenarioTotalizationNss(null); }
            return _nssTScenarioTotalization;
        }}
        public TScenarioTotalizationNss SetupSelect_TScenarioTotalization() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnScenarioTotalizationId();
            }
            doSetupSelect(delegate { return Query().QueryTScenarioTotalization(); });
            if (_nssTScenarioTotalization == null || !_nssTScenarioTotalization.HasConditionQuery)
            { _nssTScenarioTotalization = new TScenarioTotalizationNss(Query().QueryTScenarioTotalization()); }
            return _nssTScenarioTotalization;
        }
        protected TItemInfoNss _nssTItemInfo;
        public TItemInfoNss NssTItemInfo { get {
            if (_nssTItemInfo == null) { _nssTItemInfo = new TItemInfoNss(null); }
            return _nssTItemInfo;
        }}
        public TItemInfoNss SetupSelect_TItemInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnItemInfoId();
            }
            doSetupSelect(delegate { return Query().QueryTItemInfo(); });
            if (_nssTItemInfo == null || !_nssTItemInfo.HasConditionQuery)
            { _nssTItemInfo = new TItemInfoNss(Query().QueryTItemInfo()); }
            return _nssTItemInfo;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TScenarioQuerylistCBSpecification _specification;
        public TScenarioQuerylistCBSpecification Specify() {
            if (_specification == null) { _specification = new TScenarioQuerylistCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TScenarioQuerylistCQ> {
			protected BsTScenarioQuerylistCB _myCB;
			public MySpQyCall(BsTScenarioQuerylistCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TScenarioQuerylistCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TScenarioQuerylistCB> ColumnQuery(SpecifyQuery<TScenarioQuerylistCB> leftSpecifyQuery) {
            return new HpColQyOperand<TScenarioQuerylistCB>(delegate(SpecifyQuery<TScenarioQuerylistCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TScenarioQuerylistCB xcreateColumnQueryCB() {
            TScenarioQuerylistCB cb = new TScenarioQuerylistCB();
            cb.xsetupForColumnQuery((TScenarioQuerylistCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TScenarioQuerylistCB> orQuery) {
            xorQ((TScenarioQuerylistCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TScenarioQuerylistCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TScenarioQuerylistCBColQySpQyCall(mainCB));
        }
    }

    public class TScenarioQuerylistCBColQySpQyCall : HpSpQyCall<TScenarioQuerylistCQ> {
        protected TScenarioQuerylistCB _mainCB;
        public TScenarioQuerylistCBColQySpQyCall(TScenarioQuerylistCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TScenarioQuerylistCQ qy() { return _mainCB.Query(); } 
    }

    public class TScenarioQuerylistCBSpecification : AbstractSpecification<TScenarioQuerylistCQ> {
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TItemInfoCBSpecification _tItemInfo;
        public TScenarioQuerylistCBSpecification(ConditionBean baseCB, HpSpQyCall<TScenarioQuerylistCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnScenarioQuerylistId() { doColumn("SCENARIO_QUERYLIST_ID"); }
        public void ColumnScenarioTotalizationId() { doColumn("SCENARIO_TOTALIZATION_ID"); }
        public void ColumnSeqNo() { doColumn("SEQ_NO"); }
        public void ColumnItemInfoId() { doColumn("ITEM_INFO_ID"); }
        public void ColumnOperationCode() { doColumn("OPERATION_CODE"); }
        public void ColumnConditionString() { doColumn("CONDITION_STRING"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnScenarioQuerylistId(); // PK
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnScenarioTotalizationId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTItemInfo()
                    || qyCall().qy().xgetReferrerQuery() is TItemInfoCQ) {
                ColumnItemInfoId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_SCENARIO_QUERYLIST"; }
        public TScenarioTotalizationCBSpecification SpecifyTScenarioTotalization() {
            assertForeign("tScenarioTotalization");
            if (_tScenarioTotalization == null) {
                _tScenarioTotalization = new TScenarioTotalizationCBSpecification(_baseCB, new TScenarioTotalizationSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tScenarioTotalization.xsetSyncQyCall(new TScenarioTotalizationSpQyCall(xsyncQyCall())); }
            }
            return _tScenarioTotalization;
        }
		public class TScenarioTotalizationSpQyCall : HpSpQyCall<TScenarioTotalizationCQ> {
		    protected HpSpQyCall<TScenarioQuerylistCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TScenarioQuerylistCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
        public TItemInfoCBSpecification SpecifyTItemInfo() {
            assertForeign("tItemInfo");
            if (_tItemInfo == null) {
                _tItemInfo = new TItemInfoCBSpecification(_baseCB, new TItemInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tItemInfo.xsetSyncQyCall(new TItemInfoSpQyCall(xsyncQyCall())); }
            }
            return _tItemInfo;
        }
		public class TItemInfoSpQyCall : HpSpQyCall<TItemInfoCQ> {
		    protected HpSpQyCall<TScenarioQuerylistCQ> _qyCall;
		    public TItemInfoSpQyCall(HpSpQyCall<TScenarioQuerylistCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTItemInfo(); }
			public TItemInfoCQ qy() { return _qyCall.qy().QueryTItemInfo(); }
		}
    }
}
