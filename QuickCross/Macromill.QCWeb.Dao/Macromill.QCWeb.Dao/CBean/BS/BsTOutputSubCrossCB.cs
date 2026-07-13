
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
    public class BsTOutputSubCrossCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubCrossCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_CROSS"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputSubCrossId) {
            assertObjectNotNull("outputSubCrossId", outputSubCrossId);
            BsTOutputSubCrossCB cb = this;
            cb.Query().SetOutputSubCrossId_Equal(outputSubCrossId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputSubCrossId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputSubCrossId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputSubCrossCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSubCrossCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSubCrossCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSubCrossCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSubCrossCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSubCrossCB> unionQuery) {
            TOutputSubCrossCB cb = new TOutputSubCrossCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubCrossCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSubCrossCB> unionQuery) {
            TOutputSubCrossCB cb = new TOutputSubCrossCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSubCrossCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputCommonNss _nssTOutputCommon;
        public TOutputCommonNss NssTOutputCommon { get {
            if (_nssTOutputCommon == null) { _nssTOutputCommon = new TOutputCommonNss(null); }
            return _nssTOutputCommon;
        }}
        public TOutputCommonNss SetupSelect_TOutputCommon() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnOutputCommonId();
            }
            doSetupSelect(delegate { return Query().QueryTOutputCommon(); });
            if (_nssTOutputCommon == null || !_nssTOutputCommon.HasConditionQuery)
            { _nssTOutputCommon = new TOutputCommonNss(Query().QueryTOutputCommon()); }
            return _nssTOutputCommon;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputSubCrossCBSpecification _specification;
        public TOutputSubCrossCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSubCrossCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSubCrossCQ> {
			protected BsTOutputSubCrossCB _myCB;
			public MySpQyCall(BsTOutputSubCrossCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSubCrossCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSubCrossCB> ColumnQuery(SpecifyQuery<TOutputSubCrossCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSubCrossCB>(delegate(SpecifyQuery<TOutputSubCrossCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSubCrossCB xcreateColumnQueryCB() {
            TOutputSubCrossCB cb = new TOutputSubCrossCB();
            cb.xsetupForColumnQuery((TOutputSubCrossCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSubCrossCB> orQuery) {
            xorQ((TOutputSubCrossCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSubCrossCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSubCrossCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSubCrossCBColQySpQyCall : HpSpQyCall<TOutputSubCrossCQ> {
        protected TOutputSubCrossCB _mainCB;
        public TOutputSubCrossCBColQySpQyCall(TOutputSubCrossCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSubCrossCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSubCrossCBSpecification : AbstractSpecification<TOutputSubCrossCQ> {
        protected TOutputCommonCBSpecification _tOutputCommon;
        public TOutputSubCrossCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSubCrossCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputSubCrossId() { doColumn("OUTPUT_SUB_CROSS_ID"); }
        public void ColumnOutputCommonId() { doColumn("OUTPUT_COMMON_ID"); }
        public void ColumnOutputType() { doColumn("OUTPUT_TYPE"); }
        public void ColumnOutputTableType() { doColumn("OUTPUT_TABLE_TYPE"); }
        public void ColumnOutputTableOrientation() { doColumn("OUTPUT_TABLE_ORIENTATION"); }
        public void ColumnPageSettingTableType() { doColumn("PAGE_SETTING_TABLE_TYPE"); }
        public void ColumnPageSettingPaperSize() { doColumn("PAGE_SETTING_PAPER_SIZE"); }
        public void ColumnPageSettingPaperOrientation() { doColumn("PAGE_SETTING_PAPER_ORIENTATION"); }
        public void ColumnMarkingMinParameter() { doColumn("MARKING_MIN_PARAMETER"); }
        public void ColumnMarkingCode() { doColumn("MARKING_CODE"); }
        public void ColumnMarkingLevel() { doColumn("MARKING_LEVEL"); }
        public void ColumnLevel2pluscolor() { doColumn("LEVEL2PLUSCOLOR"); }
        public void ColumnLevel1pluscolor() { doColumn("LEVEL1PLUSCOLOR"); }
        public void ColumnLevel1minuscolor() { doColumn("LEVEL1MINUSCOLOR"); }
        public void ColumnLevel2minuscolor() { doColumn("LEVEL2MINUSCOLOR"); }
        public void ColumnLevel2percent() { doColumn("LEVEL2PERCENT"); }
        public void ColumnLevel1percent() { doColumn("LEVEL1PERCENT"); }
        public void ColumnFilteringExpression() { doColumn("FILTERING_EXPRESSION"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputSubCrossId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputCommon()
                    || qyCall().qy().xgetReferrerQuery() is TOutputCommonCQ) {
                ColumnOutputCommonId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_SUB_CROSS"; }
        public TOutputCommonCBSpecification SpecifyTOutputCommon() {
            assertForeign("tOutputCommon");
            if (_tOutputCommon == null) {
                _tOutputCommon = new TOutputCommonCBSpecification(_baseCB, new TOutputCommonSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputCommon.xsetSyncQyCall(new TOutputCommonSpQyCall(xsyncQyCall())); }
            }
            return _tOutputCommon;
        }
		public class TOutputCommonSpQyCall : HpSpQyCall<TOutputCommonCQ> {
		    protected HpSpQyCall<TOutputSubCrossCQ> _qyCall;
		    public TOutputCommonSpQyCall(HpSpQyCall<TOutputSubCrossCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputCommon(); }
			public TOutputCommonCQ qy() { return _qyCall.qy().QueryTOutputCommon(); }
		}
    }
}
