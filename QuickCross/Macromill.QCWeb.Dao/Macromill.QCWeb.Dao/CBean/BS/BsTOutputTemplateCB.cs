
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
    public class BsTOutputTemplateCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputTemplateCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_TEMPLATE"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputTemplateId) {
            assertObjectNotNull("outputTemplateId", outputTemplateId);
            BsTOutputTemplateCB cb = this;
            cb.Query().SetOutputTemplateId_Equal(outputTemplateId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputTemplateId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputTemplateId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputTemplateCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputTemplateCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputTemplateCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputTemplateCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputTemplateCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputTemplateCB> unionQuery) {
            TOutputTemplateCB cb = new TOutputTemplateCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputTemplateCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputTemplateCB> unionQuery) {
            TOutputTemplateCB cb = new TOutputTemplateCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputTemplateCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputTemplateMasterNss _nssTOutputTemplateMaster;
        public TOutputTemplateMasterNss NssTOutputTemplateMaster { get {
            if (_nssTOutputTemplateMaster == null) { _nssTOutputTemplateMaster = new TOutputTemplateMasterNss(null); }
            return _nssTOutputTemplateMaster;
        }}
        public TOutputTemplateMasterNss SetupSelect_TOutputTemplateMaster() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnOutputTemplateMasterId();
            }
            doSetupSelect(delegate { return Query().QueryTOutputTemplateMaster(); });
            if (_nssTOutputTemplateMaster == null || !_nssTOutputTemplateMaster.HasConditionQuery)
            { _nssTOutputTemplateMaster = new TOutputTemplateMasterNss(Query().QueryTOutputTemplateMaster()); }
            return _nssTOutputTemplateMaster;
        }
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputTemplateCBSpecification _specification;
        public TOutputTemplateCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputTemplateCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputTemplateCQ> {
			protected BsTOutputTemplateCB _myCB;
			public MySpQyCall(BsTOutputTemplateCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputTemplateCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputTemplateCB> ColumnQuery(SpecifyQuery<TOutputTemplateCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputTemplateCB>(delegate(SpecifyQuery<TOutputTemplateCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputTemplateCB xcreateColumnQueryCB() {
            TOutputTemplateCB cb = new TOutputTemplateCB();
            cb.xsetupForColumnQuery((TOutputTemplateCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputTemplateCB> orQuery) {
            xorQ((TOutputTemplateCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputTemplateCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputTemplateCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputTemplateCBColQySpQyCall : HpSpQyCall<TOutputTemplateCQ> {
        protected TOutputTemplateCB _mainCB;
        public TOutputTemplateCBColQySpQyCall(TOutputTemplateCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputTemplateCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputTemplateCBSpecification : AbstractSpecification<TOutputTemplateCQ> {
        protected TOutputTemplateMasterCBSpecification _tOutputTemplateMaster;
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        public TOutputTemplateCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputTemplateCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputTemplateId() { doColumn("OUTPUT_TEMPLATE_ID"); }
        public void ColumnOutputTemplateMasterId() { doColumn("OUTPUT_TEMPLATE_MASTER_ID"); }
        public void ColumnUploadPath() { doColumn("UPLOAD_PATH"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnAlias() { doColumn("ALIAS"); }
        public void ColumnCreateDatetime() { doColumn("CREATE_DATETIME"); }
        public void ColumnDeleteFlag() { doColumn("DELETE_FLAG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputTemplateId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputTemplateMaster()
                    || qyCall().qy().xgetReferrerQuery() is TOutputTemplateMasterCQ) {
                ColumnOutputTemplateMasterId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_TEMPLATE"; }
        public TOutputTemplateMasterCBSpecification SpecifyTOutputTemplateMaster() {
            assertForeign("tOutputTemplateMaster");
            if (_tOutputTemplateMaster == null) {
                _tOutputTemplateMaster = new TOutputTemplateMasterCBSpecification(_baseCB, new TOutputTemplateMasterSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputTemplateMaster.xsetSyncQyCall(new TOutputTemplateMasterSpQyCall(xsyncQyCall())); }
            }
            return _tOutputTemplateMaster;
        }
		public class TOutputTemplateMasterSpQyCall : HpSpQyCall<TOutputTemplateMasterCQ> {
		    protected HpSpQyCall<TOutputTemplateCQ> _qyCall;
		    public TOutputTemplateMasterSpQyCall(HpSpQyCall<TOutputTemplateCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputTemplateMaster(); }
			public TOutputTemplateMasterCQ qy() { return _qyCall.qy().QueryTOutputTemplateMaster(); }
		}
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
		    protected HpSpQyCall<TOutputTemplateCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TOutputTemplateCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public RAFunction<TOutputReportsetInfoCB, TOutputTemplateCQ> DerivedTOutputReportsetInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputReportsetInfoCB, TOutputTemplateCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputReportsetInfoCB> subQuery, TOutputTemplateCQ cq, String aliasName)
                { cq.xsderiveTOutputReportsetInfoList(function, subQuery, aliasName); });
        }
    }
}
