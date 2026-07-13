
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
    public class BsTWeightbackCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TWeightbackCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_WEIGHTBACK"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? weightbackId) {
            assertObjectNotNull("weightbackId", weightbackId);
            BsTWeightbackCB cb = this;
            cb.Query().SetWeightbackId_Equal(weightbackId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_WeightbackId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_WeightbackId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TWeightbackCQ Query() {
            return this.ConditionQuery;
        }

        public TWeightbackCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TWeightbackCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TWeightbackCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TWeightbackCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TWeightbackCB> unionQuery) {
            TWeightbackCB cb = new TWeightbackCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TWeightbackCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TWeightbackCB> unionQuery) {
            TWeightbackCB cb = new TWeightbackCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TWeightbackCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TQcwebSurveyInfoNss _nssTQcwebSurveyInfo;
        public TQcwebSurveyInfoNss NssTQcwebSurveyInfo { get {
            if (_nssTQcwebSurveyInfo == null) { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(null); }
            return _nssTQcwebSurveyInfo;
        }}
        public TQcwebSurveyInfoNss SetupSelect_TQcwebSurveyInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfo(); });
            if (_nssTQcwebSurveyInfo == null || !_nssTQcwebSurveyInfo.HasConditionQuery)
            { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfo()); }
            return _nssTQcwebSurveyInfo;
        }
        protected TWeightbackValueNss _nssTWeightbackValue;
        public TWeightbackValueNss NssTWeightbackValue { get {
            if (_nssTWeightbackValue == null) { _nssTWeightbackValue = new TWeightbackValueNss(null); }
            return _nssTWeightbackValue;
        }}
        public TWeightbackValueNss SetupSelect_TWeightbackValue() {
            doSetupSelect(delegate { return Query().QueryTWeightbackValue(); });
            if (_nssTWeightbackValue == null || !_nssTWeightbackValue.HasConditionQuery)
            { _nssTWeightbackValue = new TWeightbackValueNss(Query().QueryTWeightbackValue()); }
            return _nssTWeightbackValue;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TWeightbackCBSpecification _specification;
        public TWeightbackCBSpecification Specify() {
            if (_specification == null) { _specification = new TWeightbackCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TWeightbackCQ> {
			protected BsTWeightbackCB _myCB;
			public MySpQyCall(BsTWeightbackCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TWeightbackCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TWeightbackCB> ColumnQuery(SpecifyQuery<TWeightbackCB> leftSpecifyQuery) {
            return new HpColQyOperand<TWeightbackCB>(delegate(SpecifyQuery<TWeightbackCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TWeightbackCB xcreateColumnQueryCB() {
            TWeightbackCB cb = new TWeightbackCB();
            cb.xsetupForColumnQuery((TWeightbackCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TWeightbackCB> orQuery) {
            xorQ((TWeightbackCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TWeightbackCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TWeightbackCBColQySpQyCall(mainCB));
        }
    }

    public class TWeightbackCBColQySpQyCall : HpSpQyCall<TWeightbackCQ> {
        protected TWeightbackCB _mainCB;
        public TWeightbackCBColQySpQyCall(TWeightbackCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TWeightbackCQ qy() { return _mainCB.Query(); } 
    }

    public class TWeightbackCBSpecification : AbstractSpecification<TWeightbackCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TWeightbackValueCBSpecification _tWeightbackValue;
        public TWeightbackCBSpecification(ConditionBean baseCB, HpSpQyCall<TWeightbackCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnWeightbackId() { doColumn("WEIGHTBACK_ID"); }
        public void ColumnWeightbackItemId() { doColumn("WEIGHTBACK_ITEM_ID"); }
        public void ColumnAssistCalcFlag() { doColumn("ASSIST_CALC_FLAG"); }
        public void ColumnAssistCalcType() { doColumn("ASSIST_CALC_TYPE"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnLastUpdateUser() { doColumn("LAST_UPDATE_USER"); }
        public void ColumnLastUpdateDatetime() { doColumn("LAST_UPDATE_DATETIME"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnWeightbackId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_WEIGHTBACK"; }
        public TQcwebSurveyInfoCBSpecification SpecifyTQcwebSurveyInfo() {
            assertForeign("tQcwebSurveyInfo");
            if (_tQcwebSurveyInfo == null) {
                _tQcwebSurveyInfo = new TQcwebSurveyInfoCBSpecification(_baseCB, new TQcwebSurveyInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tQcwebSurveyInfo.xsetSyncQyCall(new TQcwebSurveyInfoSpQyCall(xsyncQyCall())); }
            }
            return _tQcwebSurveyInfo;
        }
		public class TQcwebSurveyInfoSpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
		    protected HpSpQyCall<TWeightbackCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TWeightbackCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TWeightbackValueCBSpecification SpecifyTWeightbackValue() {
            assertForeign("tWeightbackValue");
            if (_tWeightbackValue == null) {
                _tWeightbackValue = new TWeightbackValueCBSpecification(_baseCB, new TWeightbackValueSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tWeightbackValue.xsetSyncQyCall(new TWeightbackValueSpQyCall(xsyncQyCall())); }
            }
            return _tWeightbackValue;
        }
		public class TWeightbackValueSpQyCall : HpSpQyCall<TWeightbackValueCQ> {
		    protected HpSpQyCall<TWeightbackCQ> _qyCall;
		    public TWeightbackValueSpQyCall(HpSpQyCall<TWeightbackCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTWeightbackValue(); }
			public TWeightbackValueCQ qy() { return _qyCall.qy().QueryTWeightbackValue(); }
		}
        public RAFunction<TWeightbackValueCB, TWeightbackCQ> DerivedTWeightbackValueList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TWeightbackValueCB, TWeightbackCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TWeightbackValueCB> subQuery, TWeightbackCQ cq, String aliasName)
                { cq.xsderiveTWeightbackValueList(function, subQuery, aliasName); });
        }
    }
}
