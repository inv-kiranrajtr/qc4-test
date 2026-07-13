
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
    public class BsTSurveyInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TSurveyInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SURVEY_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? surveyInfoId) {
            assertObjectNotNull("surveyInfoId", surveyInfoId);
            BsTSurveyInfoCB cb = this;
            cb.Query().SetSurveyInfoId_Equal(surveyInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_SurveyInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_SurveyInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TSurveyInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TSurveyInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TSurveyInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TSurveyInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TSurveyInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TSurveyInfoCB> unionQuery) {
            TSurveyInfoCB cb = new TSurveyInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TSurveyInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TSurveyInfoCB> unionQuery) {
            TSurveyInfoCB cb = new TSurveyInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TSurveyInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TSurveyInfoCBSpecification _specification;
        public TSurveyInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TSurveyInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TSurveyInfoCQ> {
			protected BsTSurveyInfoCB _myCB;
			public MySpQyCall(BsTSurveyInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TSurveyInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TSurveyInfoCB> ColumnQuery(SpecifyQuery<TSurveyInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TSurveyInfoCB>(delegate(SpecifyQuery<TSurveyInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TSurveyInfoCB xcreateColumnQueryCB() {
            TSurveyInfoCB cb = new TSurveyInfoCB();
            cb.xsetupForColumnQuery((TSurveyInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TSurveyInfoCB> orQuery) {
            xorQ((TSurveyInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TSurveyInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TSurveyInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TSurveyInfoCBColQySpQyCall : HpSpQyCall<TSurveyInfoCQ> {
        protected TSurveyInfoCB _mainCB;
        public TSurveyInfoCBColQySpQyCall(TSurveyInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TSurveyInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TSurveyInfoCBSpecification : AbstractSpecification<TSurveyInfoCQ> {
        public TSurveyInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TSurveyInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnSurveyInfoId() { doColumn("SURVEY_INFO_ID"); }
        public void ColumnMainSurveyId() { doColumn("MAIN_SURVEY_ID"); }
        public void ColumnScheduleDeleteDate() { doColumn("SCHEDULE_DELETE_DATE"); }
        public void ColumnDeleteFlag() { doColumn("DELETE_FLAG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnSurveyInfoId(); // PK
        }
        protected override String getTableDbName() { return "T_SURVEY_INFO"; }
        public RAFunction<TQcwebSurveyInfoCB, TSurveyInfoCQ> DerivedTQcwebSurveyInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TQcwebSurveyInfoCB, TSurveyInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TQcwebSurveyInfoCB> subQuery, TSurveyInfoCQ cq, String aliasName)
                { cq.xsderiveTQcwebSurveyInfoList(function, subQuery, aliasName); });
        }
    }
}
