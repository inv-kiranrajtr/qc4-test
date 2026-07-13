
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
    public class BsTDataEditListCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataEditListCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_EDIT_LIST"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? dataEditId) {
            assertObjectNotNull("dataEditId", dataEditId);
            BsTDataEditListCB cb = this;
            cb.Query().SetDataEditId_Equal(dataEditId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_DataEditId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_DataEditId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDataEditListCQ Query() {
            return this.ConditionQuery;
        }

        public TDataEditListCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDataEditListCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDataEditListCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDataEditListCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDataEditListCB> unionQuery) {
            TDataEditListCB cb = new TDataEditListCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDataEditListCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDataEditListCB> unionQuery) {
            TDataEditListCB cb = new TDataEditListCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDataEditListCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TEditMenuMasterNss _nssTEditMenuMaster;
        public TEditMenuMasterNss NssTEditMenuMaster { get {
            if (_nssTEditMenuMaster == null) { _nssTEditMenuMaster = new TEditMenuMasterNss(null); }
            return _nssTEditMenuMaster;
        }}
        public TEditMenuMasterNss SetupSelect_TEditMenuMaster() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnEditMenuMasterId();
            }
            doSetupSelect(delegate { return Query().QueryTEditMenuMaster(); });
            if (_nssTEditMenuMaster == null || !_nssTEditMenuMaster.HasConditionQuery)
            { _nssTEditMenuMaster = new TEditMenuMasterNss(Query().QueryTEditMenuMaster()); }
            return _nssTEditMenuMaster;
        }

        protected TDataProcessNewItemNss _nssTDataProcessNewItemAsOne;
        public TDataProcessNewItemNss NssTDataProcessNewItemAsOne { get {
            if (_nssTDataProcessNewItemAsOne == null) { _nssTDataProcessNewItemAsOne = new TDataProcessNewItemNss(null); }
            return _nssTDataProcessNewItemAsOne;
        }}
        public TDataProcessNewItemNss SetupSelect_TDataProcessNewItemAsOne() {
            doSetupSelect(delegate { return Query().QueryTDataProcessNewItemAsOne(); });
            if (_nssTDataProcessNewItemAsOne == null || !_nssTDataProcessNewItemAsOne.HasConditionQuery)
            { _nssTDataProcessNewItemAsOne = new TDataProcessNewItemNss(Query().QueryTDataProcessNewItemAsOne()); }
            return _nssTDataProcessNewItemAsOne;
        }

        protected TDeleteDataNss _nssTDeleteDataAsOne;
        public TDeleteDataNss NssTDeleteDataAsOne { get {
            if (_nssTDeleteDataAsOne == null) { _nssTDeleteDataAsOne = new TDeleteDataNss(null); }
            return _nssTDeleteDataAsOne;
        }}
        public TDeleteDataNss SetupSelect_TDeleteDataAsOne() {
            doSetupSelect(delegate { return Query().QueryTDeleteDataAsOne(); });
            if (_nssTDeleteDataAsOne == null || !_nssTDeleteDataAsOne.HasConditionQuery)
            { _nssTDeleteDataAsOne = new TDeleteDataNss(Query().QueryTDeleteDataAsOne()); }
            return _nssTDeleteDataAsOne;
        }

        protected TEditDataNss _nssTEditDataAsOne;
        public TEditDataNss NssTEditDataAsOne { get {
            if (_nssTEditDataAsOne == null) { _nssTEditDataAsOne = new TEditDataNss(null); }
            return _nssTEditDataAsOne;
        }}
        public TEditDataNss SetupSelect_TEditDataAsOne() {
            doSetupSelect(delegate { return Query().QueryTEditDataAsOne(); });
            if (_nssTEditDataAsOne == null || !_nssTEditDataAsOne.HasConditionQuery)
            { _nssTEditDataAsOne = new TEditDataNss(Query().QueryTEditDataAsOne()); }
            return _nssTEditDataAsOne;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDataEditListCBSpecification _specification;
        public TDataEditListCBSpecification Specify() {
            if (_specification == null) { _specification = new TDataEditListCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDataEditListCQ> {
			protected BsTDataEditListCB _myCB;
			public MySpQyCall(BsTDataEditListCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDataEditListCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDataEditListCB> ColumnQuery(SpecifyQuery<TDataEditListCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDataEditListCB>(delegate(SpecifyQuery<TDataEditListCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDataEditListCB xcreateColumnQueryCB() {
            TDataEditListCB cb = new TDataEditListCB();
            cb.xsetupForColumnQuery((TDataEditListCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDataEditListCB> orQuery) {
            xorQ((TDataEditListCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDataEditListCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDataEditListCBColQySpQyCall(mainCB));
        }
    }

    public class TDataEditListCBColQySpQyCall : HpSpQyCall<TDataEditListCQ> {
        protected TDataEditListCB _mainCB;
        public TDataEditListCBColQySpQyCall(TDataEditListCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDataEditListCQ qy() { return _mainCB.Query(); } 
    }

    public class TDataEditListCBSpecification : AbstractSpecification<TDataEditListCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TEditMenuMasterCBSpecification _tEditMenuMaster;
        protected TDataProcessNewItemCBSpecification _tDataProcessNewItemAsOne;
        protected TDeleteDataCBSpecification _tDeleteDataAsOne;
        protected TEditDataCBSpecification _tEditDataAsOne;
        public TDataEditListCBSpecification(ConditionBean baseCB, HpSpQyCall<TDataEditListCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnExecuteNo() { doColumn("EXECUTE_NO"); }
        public void ColumnExecuteFlag() { doColumn("EXECUTE_FLAG"); }
        public void ColumnEditMenuMasterId() { doColumn("EDIT_MENU_MASTER_ID"); }
        public void ColumnDescription() { doColumn("DESCRIPTION"); }
        public void ColumnConditionItemViewName() { doColumn("CONDITION_ITEM_VIEW_NAME"); }
        public void ColumnTargetItemViewName() { doColumn("TARGET_ITEM_VIEW_NAME"); }
        public void ColumnStatus() { doColumn("STATUS"); }
        public void ColumnLatestFlag() { doColumn("LATEST_FLAG"); }
        public void ColumnDerivedDataEditId() { doColumn("DERIVED_DATA_EDIT_ID"); }
        public void ColumnDeleteReserveFlag() { doColumn("DELETE_RESERVE_FLAG"); }
        public void ColumnLastUpdateUser() { doColumn("LAST_UPDATE_USER"); }
        public void ColumnLastUpdateDatetime() { doColumn("LAST_UPDATE_DATETIME"); }
        public void ColumnEditFlag() { doColumn("EDIT_FLAG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDataEditId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTEditMenuMaster()
                    || qyCall().qy().xgetReferrerQuery() is TEditMenuMasterCQ) {
                ColumnEditMenuMasterId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_DATA_EDIT_LIST"; }
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
		    protected HpSpQyCall<TDataEditListCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TDataEditListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TEditMenuMasterCBSpecification SpecifyTEditMenuMaster() {
            assertForeign("tEditMenuMaster");
            if (_tEditMenuMaster == null) {
                _tEditMenuMaster = new TEditMenuMasterCBSpecification(_baseCB, new TEditMenuMasterSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tEditMenuMaster.xsetSyncQyCall(new TEditMenuMasterSpQyCall(xsyncQyCall())); }
            }
            return _tEditMenuMaster;
        }
		public class TEditMenuMasterSpQyCall : HpSpQyCall<TEditMenuMasterCQ> {
		    protected HpSpQyCall<TDataEditListCQ> _qyCall;
		    public TEditMenuMasterSpQyCall(HpSpQyCall<TDataEditListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTEditMenuMaster(); }
			public TEditMenuMasterCQ qy() { return _qyCall.qy().QueryTEditMenuMaster(); }
		}
        public TDataProcessNewItemCBSpecification SpecifyTDataProcessNewItemAsOne() {
            assertForeign("tDataProcessNewItemAsOne");
            if (_tDataProcessNewItemAsOne == null) {
                _tDataProcessNewItemAsOne = new TDataProcessNewItemCBSpecification(_baseCB, new TDataProcessNewItemAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDataProcessNewItemAsOne.xsetSyncQyCall(new TDataProcessNewItemAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tDataProcessNewItemAsOne;
        }
		public class TDataProcessNewItemAsOneSpQyCall : HpSpQyCall<TDataProcessNewItemCQ> {
		    protected HpSpQyCall<TDataEditListCQ> _qyCall;
		    public TDataProcessNewItemAsOneSpQyCall(HpSpQyCall<TDataEditListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataProcessNewItemAsOne(); }
			public TDataProcessNewItemCQ qy() { return _qyCall.qy().QueryTDataProcessNewItemAsOne(); }
		}
        public TDeleteDataCBSpecification SpecifyTDeleteDataAsOne() {
            assertForeign("tDeleteDataAsOne");
            if (_tDeleteDataAsOne == null) {
                _tDeleteDataAsOne = new TDeleteDataCBSpecification(_baseCB, new TDeleteDataAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDeleteDataAsOne.xsetSyncQyCall(new TDeleteDataAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tDeleteDataAsOne;
        }
		public class TDeleteDataAsOneSpQyCall : HpSpQyCall<TDeleteDataCQ> {
		    protected HpSpQyCall<TDataEditListCQ> _qyCall;
		    public TDeleteDataAsOneSpQyCall(HpSpQyCall<TDataEditListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDeleteDataAsOne(); }
			public TDeleteDataCQ qy() { return _qyCall.qy().QueryTDeleteDataAsOne(); }
		}
        public TEditDataCBSpecification SpecifyTEditDataAsOne() {
            assertForeign("tEditDataAsOne");
            if (_tEditDataAsOne == null) {
                _tEditDataAsOne = new TEditDataCBSpecification(_baseCB, new TEditDataAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tEditDataAsOne.xsetSyncQyCall(new TEditDataAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tEditDataAsOne;
        }
		public class TEditDataAsOneSpQyCall : HpSpQyCall<TEditDataCQ> {
		    protected HpSpQyCall<TDataEditListCQ> _qyCall;
		    public TEditDataAsOneSpQyCall(HpSpQyCall<TDataEditListCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTEditDataAsOne(); }
			public TEditDataCQ qy() { return _qyCall.qy().QueryTEditDataAsOne(); }
		}
        public RAFunction<TItemInfoCB, TDataEditListCQ> DerivedTItemInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TItemInfoCB, TDataEditListCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TItemInfoCB> subQuery, TDataEditListCQ cq, String aliasName)
                { cq.xsderiveTItemInfoList(function, subQuery, aliasName); });
        }
    }
}
