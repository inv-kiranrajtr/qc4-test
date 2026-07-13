
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
    public class BsTOutputReportsetInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputReportsetInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_REPORTSET_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputReportsetInfoId) {
            assertObjectNotNull("outputReportsetInfoId", outputReportsetInfoId);
            BsTOutputReportsetInfoCB cb = this;
            cb.Query().SetOutputReportsetInfoId_Equal(outputReportsetInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputReportsetInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputReportsetInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputReportsetInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputReportsetInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputReportsetInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputReportsetInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputReportsetInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputReportsetInfoCB> unionQuery) {
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputReportsetInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputReportsetInfoCB> unionQuery) {
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputReportsetInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputTemplateNss _nssTOutputTemplate;
        public TOutputTemplateNss NssTOutputTemplate { get {
            if (_nssTOutputTemplate == null) { _nssTOutputTemplate = new TOutputTemplateNss(null); }
            return _nssTOutputTemplate;
        }}
        public TOutputTemplateNss SetupSelect_TOutputTemplate() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnOutputTemplateId();
            }
            doSetupSelect(delegate { return Query().QueryTOutputTemplate(); });
            if (_nssTOutputTemplate == null || !_nssTOutputTemplate.HasConditionQuery)
            { _nssTOutputTemplate = new TOutputTemplateNss(Query().QueryTOutputTemplate()); }
            return _nssTOutputTemplate;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputReportsetInfoCBSpecification _specification;
        public TOutputReportsetInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputReportsetInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputReportsetInfoCQ> {
			protected BsTOutputReportsetInfoCB _myCB;
			public MySpQyCall(BsTOutputReportsetInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputReportsetInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputReportsetInfoCB> ColumnQuery(SpecifyQuery<TOutputReportsetInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputReportsetInfoCB>(delegate(SpecifyQuery<TOutputReportsetInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputReportsetInfoCB xcreateColumnQueryCB() {
            TOutputReportsetInfoCB cb = new TOutputReportsetInfoCB();
            cb.xsetupForColumnQuery((TOutputReportsetInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputReportsetInfoCB> orQuery) {
            xorQ((TOutputReportsetInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputReportsetInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputReportsetInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputReportsetInfoCBColQySpQyCall : HpSpQyCall<TOutputReportsetInfoCQ> {
        protected TOutputReportsetInfoCB _mainCB;
        public TOutputReportsetInfoCBColQySpQyCall(TOutputReportsetInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputReportsetInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputReportsetInfoCBSpecification : AbstractSpecification<TOutputReportsetInfoCQ> {
        protected TOutputTemplateCBSpecification _tOutputTemplate;
        public TOutputReportsetInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputReportsetInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputReportsetInfoId() { doColumn("OUTPUT_REPORTSET_INFO_ID"); }
        public void ColumnOutputFileTypeCode() { doColumn("OUTPUT_FILE_TYPE_CODE"); }
        public void ColumnReportFilenNamePrefix() { doColumn("REPORT_FILEN_NAME_PREFIX"); }
        public void ColumnCommentOutputFlag() { doColumn("COMMENT_OUTPUT_FLAG"); }
        public void ColumnPowerpointType() { doColumn("POWERPOINT_TYPE"); }
        public void ColumnOutputTemplateId() { doColumn("OUTPUT_TEMPLATE_ID"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputReportsetInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputTemplate()
                    || qyCall().qy().xgetReferrerQuery() is TOutputTemplateCQ) {
                ColumnOutputTemplateId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_REPORTSET_INFO"; }
        public TOutputTemplateCBSpecification SpecifyTOutputTemplate() {
            assertForeign("tOutputTemplate");
            if (_tOutputTemplate == null) {
                _tOutputTemplate = new TOutputTemplateCBSpecification(_baseCB, new TOutputTemplateSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputTemplate.xsetSyncQyCall(new TOutputTemplateSpQyCall(xsyncQyCall())); }
            }
            return _tOutputTemplate;
        }
		public class TOutputTemplateSpQyCall : HpSpQyCall<TOutputTemplateCQ> {
		    protected HpSpQyCall<TOutputReportsetInfoCQ> _qyCall;
		    public TOutputTemplateSpQyCall(HpSpQyCall<TOutputReportsetInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputTemplate(); }
			public TOutputTemplateCQ qy() { return _qyCall.qy().QueryTOutputTemplate(); }
		}
        public RAFunction<TOutputRequestCB, TOutputReportsetInfoCQ> DerivedTOutputRequestList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputRequestCB, TOutputReportsetInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputRequestCB> subQuery, TOutputReportsetInfoCQ cq, String aliasName)
                { cq.xsderiveTOutputRequestList(function, subQuery, aliasName); });
        }
    }
}
