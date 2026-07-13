
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
    public class BsTRawdataImportQueInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TRawdataImportQueInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_RAWDATA_IMPORT_QUE_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? rawdataImportQueInfoId) {
            assertObjectNotNull("rawdataImportQueInfoId", rawdataImportQueInfoId);
            BsTRawdataImportQueInfoCB cb = this;
            cb.Query().SetRawdataImportQueInfoId_Equal(rawdataImportQueInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_RawdataImportQueInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_RawdataImportQueInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TRawdataImportQueInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TRawdataImportQueInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TRawdataImportQueInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TRawdataImportQueInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TRawdataImportQueInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TRawdataImportQueInfoCB> unionQuery) {
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TRawdataImportQueInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TRawdataImportQueInfoCB> unionQuery) {
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TRawdataImportQueInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TRawdataImportQueInfoCBSpecification _specification;
        public TRawdataImportQueInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TRawdataImportQueInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TRawdataImportQueInfoCQ> {
			protected BsTRawdataImportQueInfoCB _myCB;
			public MySpQyCall(BsTRawdataImportQueInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TRawdataImportQueInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TRawdataImportQueInfoCB> ColumnQuery(SpecifyQuery<TRawdataImportQueInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TRawdataImportQueInfoCB>(delegate(SpecifyQuery<TRawdataImportQueInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TRawdataImportQueInfoCB xcreateColumnQueryCB() {
            TRawdataImportQueInfoCB cb = new TRawdataImportQueInfoCB();
            cb.xsetupForColumnQuery((TRawdataImportQueInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TRawdataImportQueInfoCB> orQuery) {
            xorQ((TRawdataImportQueInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TRawdataImportQueInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TRawdataImportQueInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TRawdataImportQueInfoCBColQySpQyCall : HpSpQyCall<TRawdataImportQueInfoCQ> {
        protected TRawdataImportQueInfoCB _mainCB;
        public TRawdataImportQueInfoCBColQySpQyCall(TRawdataImportQueInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TRawdataImportQueInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TRawdataImportQueInfoCBSpecification : AbstractSpecification<TRawdataImportQueInfoCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        public TRawdataImportQueInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TRawdataImportQueInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnRawdataImportQueInfoId() { doColumn("RAWDATA_IMPORT_QUE_INFO_ID"); }
        public void ColumnQcwebJobNo() { doColumn("QCWEB_JOB_NO"); }
        public void ColumnMainSurveyId() { doColumn("MAIN_SURVEY_ID"); }
        public void ColumnSurveyDataType() { doColumn("SURVEY_DATA_TYPE"); }
        public void ColumnFilepath() { doColumn("FILEPATH"); }
        public void ColumnFileName() { doColumn("FILE_NAME"); }
        public void ColumnImportStatus() { doColumn("IMPORT_STATUS"); }
        public void ColumnMessage() { doColumn("MESSAGE"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnAddDataNo() { doColumn("ADD_DATA_NO"); }
        public void ColumnRequestDatetime() { doColumn("REQUEST_DATETIME"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnRawdataImportQueInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_RAWDATA_IMPORT_QUE_INFO"; }
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
		    protected HpSpQyCall<TRawdataImportQueInfoCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TRawdataImportQueInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public RAFunction<TQcwebSurveyInfoCB, TRawdataImportQueInfoCQ> DerivedTQcwebSurveyInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TQcwebSurveyInfoCB, TRawdataImportQueInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, TRawdataImportQueInfoCQ cq, String aliasName)
                { cq.xsderiveTQcwebSurveyInfoList(function, subQuery, aliasName); });
        }
    }
}
