
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
    public class BsTOutputCommonCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputCommonCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_COMMON"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputCommonId) {
            assertObjectNotNull("outputCommonId", outputCommonId);
            BsTOutputCommonCB cb = this;
            cb.Query().SetOutputCommonId_Equal(outputCommonId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputCommonId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputCommonId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputCommonCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputCommonCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputCommonCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputCommonCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputCommonCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputCommonCB> unionQuery) {
            TOutputCommonCB cb = new TOutputCommonCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputCommonCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputCommonCB> unionQuery) {
            TOutputCommonCB cb = new TOutputCommonCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputCommonCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputRequestNss _nssTOutputRequest;
        public TOutputRequestNss NssTOutputRequest { get {
            if (_nssTOutputRequest == null) { _nssTOutputRequest = new TOutputRequestNss(null); }
            return _nssTOutputRequest;
        }}
        public TOutputRequestNss SetupSelect_TOutputRequest() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnOutputRequestId();
            }
            doSetupSelect(delegate { return Query().QueryTOutputRequest(); });
            if (_nssTOutputRequest == null || !_nssTOutputRequest.HasConditionQuery)
            { _nssTOutputRequest = new TOutputRequestNss(Query().QueryTOutputRequest()); }
            return _nssTOutputRequest;
        }
        protected TOutputSubGtNss _nssTOutputSubGt;
        public TOutputSubGtNss NssTOutputSubGt { get {
            if (_nssTOutputSubGt == null) { _nssTOutputSubGt = new TOutputSubGtNss(null); }
            return _nssTOutputSubGt;
        }}
        public TOutputSubGtNss SetupSelect_TOutputSubGt() {
            doSetupSelect(delegate { return Query().QueryTOutputSubGt(); });
            if (_nssTOutputSubGt == null || !_nssTOutputSubGt.HasConditionQuery)
            { _nssTOutputSubGt = new TOutputSubGtNss(Query().QueryTOutputSubGt()); }
            return _nssTOutputSubGt;
        }
        protected TOutputSubCrossNss _nssTOutputSubCross;
        public TOutputSubCrossNss NssTOutputSubCross { get {
            if (_nssTOutputSubCross == null) { _nssTOutputSubCross = new TOutputSubCrossNss(null); }
            return _nssTOutputSubCross;
        }}
        public TOutputSubCrossNss SetupSelect_TOutputSubCross() {
            doSetupSelect(delegate { return Query().QueryTOutputSubCross(); });
            if (_nssTOutputSubCross == null || !_nssTOutputSubCross.HasConditionQuery)
            { _nssTOutputSubCross = new TOutputSubCrossNss(Query().QueryTOutputSubCross()); }
            return _nssTOutputSubCross;
        }
        protected TOutputSubFaNss _nssTOutputSubFa;
        public TOutputSubFaNss NssTOutputSubFa { get {
            if (_nssTOutputSubFa == null) { _nssTOutputSubFa = new TOutputSubFaNss(null); }
            return _nssTOutputSubFa;
        }}
        public TOutputSubFaNss SetupSelect_TOutputSubFa() {
            doSetupSelect(delegate { return Query().QueryTOutputSubFa(); });
            if (_nssTOutputSubFa == null || !_nssTOutputSubFa.HasConditionQuery)
            { _nssTOutputSubFa = new TOutputSubFaNss(Query().QueryTOutputSubFa()); }
            return _nssTOutputSubFa;
        }
        protected TOutputSubCklistNss _nssTOutputSubCklist;
        public TOutputSubCklistNss NssTOutputSubCklist { get {
            if (_nssTOutputSubCklist == null) { _nssTOutputSubCklist = new TOutputSubCklistNss(null); }
            return _nssTOutputSubCklist;
        }}
        public TOutputSubCklistNss SetupSelect_TOutputSubCklist() {
            doSetupSelect(delegate { return Query().QueryTOutputSubCklist(); });
            if (_nssTOutputSubCklist == null || !_nssTOutputSubCklist.HasConditionQuery)
            { _nssTOutputSubCklist = new TOutputSubCklistNss(Query().QueryTOutputSubCklist()); }
            return _nssTOutputSubCklist;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputCommonCBSpecification _specification;
        public TOutputCommonCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputCommonCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputCommonCQ> {
			protected BsTOutputCommonCB _myCB;
			public MySpQyCall(BsTOutputCommonCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputCommonCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputCommonCB> ColumnQuery(SpecifyQuery<TOutputCommonCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputCommonCB>(delegate(SpecifyQuery<TOutputCommonCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputCommonCB xcreateColumnQueryCB() {
            TOutputCommonCB cb = new TOutputCommonCB();
            cb.xsetupForColumnQuery((TOutputCommonCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputCommonCB> orQuery) {
            xorQ((TOutputCommonCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputCommonCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputCommonCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputCommonCBColQySpQyCall : HpSpQyCall<TOutputCommonCQ> {
        protected TOutputCommonCB _mainCB;
        public TOutputCommonCBColQySpQyCall(TOutputCommonCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputCommonCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputCommonCBSpecification : AbstractSpecification<TOutputCommonCQ> {
        protected TOutputRequestCBSpecification _tOutputRequest;
        protected TOutputSubGtCBSpecification _tOutputSubGt;
        protected TOutputSubCrossCBSpecification _tOutputSubCross;
        protected TOutputSubFaCBSpecification _tOutputSubFa;
        protected TOutputSubCklistCBSpecification _tOutputSubCklist;
        public TOutputCommonCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputCommonCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputCommonId() { doColumn("OUTPUT_COMMON_ID"); }
        public void ColumnOrderCount() { doColumn("ORDER_COUNT"); }
        public void ColumnTsvFilePath() { doColumn("TSV_FILE_PATH"); }
        public void ColumnExcelbookNamePrefix() { doColumn("EXCELBOOK_NAME_PREFIX"); }
        public void ColumnProcessStartDatetime() { doColumn("PROCESS_START_DATETIME"); }
        public void ColumnProcessForecastEndDatetime() { doColumn("PROCESS_FORECAST_END_DATETIME"); }
        public void ColumnProcessEndDatetime() { doColumn("PROCESS_END_DATETIME"); }
        public void ColumnStatusCode() { doColumn("STATUS_CODE"); }
        public void ColumnDescription() { doColumn("DESCRIPTION"); }
        public void ColumnOutputType() { doColumn("OUTPUT_TYPE"); }
        public void ColumnOutputRequestId() { doColumn("OUTPUT_REQUEST_ID"); }
        public void ColumnWbSettingCode() { doColumn("WB_SETTING_CODE"); }
        public void ColumnNoanswerVisibleCode() { doColumn("NOANSWER_VISIBLE_CODE"); }
        public void ColumnUnmatchVisibleCode() { doColumn("UNMATCH_VISIBLE_CODE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputCommonId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputRequest()
                    || qyCall().qy().xgetReferrerQuery() is TOutputRequestCQ) {
                ColumnOutputRequestId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_COMMON"; }
        public TOutputRequestCBSpecification SpecifyTOutputRequest() {
            assertForeign("tOutputRequest");
            if (_tOutputRequest == null) {
                _tOutputRequest = new TOutputRequestCBSpecification(_baseCB, new TOutputRequestSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputRequest.xsetSyncQyCall(new TOutputRequestSpQyCall(xsyncQyCall())); }
            }
            return _tOutputRequest;
        }
		public class TOutputRequestSpQyCall : HpSpQyCall<TOutputRequestCQ> {
		    protected HpSpQyCall<TOutputCommonCQ> _qyCall;
		    public TOutputRequestSpQyCall(HpSpQyCall<TOutputCommonCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputRequest(); }
			public TOutputRequestCQ qy() { return _qyCall.qy().QueryTOutputRequest(); }
		}
        public TOutputSubGtCBSpecification SpecifyTOutputSubGt() {
            assertForeign("tOutputSubGt");
            if (_tOutputSubGt == null) {
                _tOutputSubGt = new TOutputSubGtCBSpecification(_baseCB, new TOutputSubGtSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSubGt.xsetSyncQyCall(new TOutputSubGtSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSubGt;
        }
		public class TOutputSubGtSpQyCall : HpSpQyCall<TOutputSubGtCQ> {
		    protected HpSpQyCall<TOutputCommonCQ> _qyCall;
		    public TOutputSubGtSpQyCall(HpSpQyCall<TOutputCommonCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSubGt(); }
			public TOutputSubGtCQ qy() { return _qyCall.qy().QueryTOutputSubGt(); }
		}
        public TOutputSubCrossCBSpecification SpecifyTOutputSubCross() {
            assertForeign("tOutputSubCross");
            if (_tOutputSubCross == null) {
                _tOutputSubCross = new TOutputSubCrossCBSpecification(_baseCB, new TOutputSubCrossSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSubCross.xsetSyncQyCall(new TOutputSubCrossSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSubCross;
        }
		public class TOutputSubCrossSpQyCall : HpSpQyCall<TOutputSubCrossCQ> {
		    protected HpSpQyCall<TOutputCommonCQ> _qyCall;
		    public TOutputSubCrossSpQyCall(HpSpQyCall<TOutputCommonCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSubCross(); }
			public TOutputSubCrossCQ qy() { return _qyCall.qy().QueryTOutputSubCross(); }
		}
        public TOutputSubFaCBSpecification SpecifyTOutputSubFa() {
            assertForeign("tOutputSubFa");
            if (_tOutputSubFa == null) {
                _tOutputSubFa = new TOutputSubFaCBSpecification(_baseCB, new TOutputSubFaSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSubFa.xsetSyncQyCall(new TOutputSubFaSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSubFa;
        }
		public class TOutputSubFaSpQyCall : HpSpQyCall<TOutputSubFaCQ> {
		    protected HpSpQyCall<TOutputCommonCQ> _qyCall;
		    public TOutputSubFaSpQyCall(HpSpQyCall<TOutputCommonCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSubFa(); }
			public TOutputSubFaCQ qy() { return _qyCall.qy().QueryTOutputSubFa(); }
		}
        public TOutputSubCklistCBSpecification SpecifyTOutputSubCklist() {
            assertForeign("tOutputSubCklist");
            if (_tOutputSubCklist == null) {
                _tOutputSubCklist = new TOutputSubCklistCBSpecification(_baseCB, new TOutputSubCklistSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSubCklist.xsetSyncQyCall(new TOutputSubCklistSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSubCklist;
        }
		public class TOutputSubCklistSpQyCall : HpSpQyCall<TOutputSubCklistCQ> {
		    protected HpSpQyCall<TOutputCommonCQ> _qyCall;
		    public TOutputSubCklistSpQyCall(HpSpQyCall<TOutputCommonCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSubCklist(); }
			public TOutputSubCklistCQ qy() { return _qyCall.qy().QueryTOutputSubCklist(); }
		}
        public RAFunction<TOutputSubCklistCB, TOutputCommonCQ> DerivedTOutputSubCklistList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputSubCklistCB, TOutputCommonCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputSubCklistCB> subQuery, TOutputCommonCQ cq, String aliasName)
                { cq.xsderiveTOutputSubCklistList(function, subQuery, aliasName); });
        }
        public RAFunction<TOutputSubCrossCB, TOutputCommonCQ> DerivedTOutputSubCrossList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputSubCrossCB, TOutputCommonCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputSubCrossCB> subQuery, TOutputCommonCQ cq, String aliasName)
                { cq.xsderiveTOutputSubCrossList(function, subQuery, aliasName); });
        }
        public RAFunction<TOutputSubFaCB, TOutputCommonCQ> DerivedTOutputSubFaList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputSubFaCB, TOutputCommonCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputSubFaCB> subQuery, TOutputCommonCQ cq, String aliasName)
                { cq.xsderiveTOutputSubFaList(function, subQuery, aliasName); });
        }
        public RAFunction<TOutputSubGtCB, TOutputCommonCQ> DerivedTOutputSubGtList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputSubGtCB, TOutputCommonCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputSubGtCB> subQuery, TOutputCommonCQ cq, String aliasName)
                { cq.xsderiveTOutputSubGtList(function, subQuery, aliasName); });
        }
    }
}
