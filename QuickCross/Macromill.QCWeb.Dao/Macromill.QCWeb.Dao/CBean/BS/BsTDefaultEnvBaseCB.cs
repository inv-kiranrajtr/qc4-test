
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
    public class BsTDefaultEnvBaseCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvBaseCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_BASE"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(String language) {
            assertObjectNotNull("language", language);
            BsTDefaultEnvBaseCB cb = this;
            cb.Query().SetLanguage_Equal(language);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_Language_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_Language_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TDefaultEnvBaseCQ Query() {
            return this.ConditionQuery;
        }

        public TDefaultEnvBaseCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDefaultEnvBaseCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDefaultEnvBaseCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDefaultEnvBaseCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDefaultEnvBaseCB> unionQuery) {
            TDefaultEnvBaseCB cb = new TDefaultEnvBaseCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvBaseCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDefaultEnvBaseCB> unionQuery) {
            TDefaultEnvBaseCB cb = new TDefaultEnvBaseCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDefaultEnvBaseCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDefaultEnvBaseCBSpecification _specification;
        public TDefaultEnvBaseCBSpecification Specify() {
            if (_specification == null) { _specification = new TDefaultEnvBaseCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDefaultEnvBaseCQ> {
			protected BsTDefaultEnvBaseCB _myCB;
			public MySpQyCall(BsTDefaultEnvBaseCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDefaultEnvBaseCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDefaultEnvBaseCB> ColumnQuery(SpecifyQuery<TDefaultEnvBaseCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDefaultEnvBaseCB>(delegate(SpecifyQuery<TDefaultEnvBaseCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDefaultEnvBaseCB xcreateColumnQueryCB() {
            TDefaultEnvBaseCB cb = new TDefaultEnvBaseCB();
            cb.xsetupForColumnQuery((TDefaultEnvBaseCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDefaultEnvBaseCB> orQuery) {
            xorQ((TDefaultEnvBaseCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDefaultEnvBaseCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDefaultEnvBaseCBColQySpQyCall(mainCB));
        }
    }

    public class TDefaultEnvBaseCBColQySpQyCall : HpSpQyCall<TDefaultEnvBaseCQ> {
        protected TDefaultEnvBaseCB _mainCB;
        public TDefaultEnvBaseCBColQySpQyCall(TDefaultEnvBaseCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDefaultEnvBaseCQ qy() { return _mainCB.Query(); } 
    }

    public class TDefaultEnvBaseCBSpecification : AbstractSpecification<TDefaultEnvBaseCQ> {
        public TDefaultEnvBaseCBSpecification(ConditionBean baseCB, HpSpQyCall<TDefaultEnvBaseCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnLanguage() { doColumn("LANGUAGE"); }
        public void ColumnNoanswerDenominatorFlag() { doColumn("NOANSWER_DENOMINATOR_FLAG"); }
        public void ColumnVisibleUnfitFlag() { doColumn("VISIBLE_UNFIT_FLAG"); }
        public void ColumnNoanswerUnfitFlag() { doColumn("NOANSWER_UNFIT_FLAG"); }
        public void ColumnWeightbackFlag() { doColumn("WEIGHTBACK_FLAG"); }
        public void ColumnCellJoincellJoinFlag() { doColumn("CELL_JOINCELL_JOIN_FLAG"); }
        public void ColumnChartDirectionGtFlag() { doColumn("CHART_DIRECTION_GT_FLAG"); }
        public void ColumnChartDirectionCrossFlag() { doColumn("CHART_DIRECTION_CROSS_FLAG"); }
        public void ColumnNoanswerTargetFlag() { doColumn("NOANSWER_TARGET_FLAG"); }
        public void ColumnNoanswerAxisFlag() { doColumn("NOANSWER_AXIS_FLAG"); }
        public void ColumnUnfitTargetFlag() { doColumn("UNFIT_TARGET_FLAG"); }
        public void ColumnUnfitAxisFlag() { doColumn("UNFIT_AXIS_FLAG"); }
        public void ColumnTotalnumFlag() { doColumn("TOTALNUM_FLAG"); }
        public void ColumnRateDiffColorMinus5() { doColumn("RATE_DIFF_COLOR_MINUS5"); }
        public void ColumnRateDiffColorMinus10() { doColumn("RATE_DIFF_COLOR_MINUS10"); }
        public void ColumnRateDiffColorPlus5() { doColumn("RATE_DIFF_COLOR_PLUS5"); }
        public void ColumnRateDiffColorPlus10() { doColumn("RATE_DIFF_COLOR_PLUS10"); }
        public void ColumnGraphTypeSa() { doColumn("GRAPH_TYPE_SA"); }
        public void ColumnGraphTypeSaMatrix() { doColumn("GRAPH_TYPE_SA_MATRIX"); }
        public void ColumnGraphTypeMaSimple() { doColumn("GRAPH_TYPE_MA_SIMPLE"); }
        public void ColumnGraphTypeMaCross() { doColumn("GRAPH_TYPE_MA_CROSS"); }
        public void ColumnGraphTypeMaMatrix() { doColumn("GRAPH_TYPE_MA_MATRIX"); }
        public void ColumnGraphTypeNRate() { doColumn("GRAPH_TYPE_N_RATE"); }
        public void ColumnGraphTypeNRanking() { doColumn("GRAPH_TYPE_N_RANKING"); }
        public void ColumnSetExecuteFlag() { doColumn("SET_EXECUTE_FLAG"); }
        public void ColumnTitleAll() { doColumn("TITLE_ALL"); }
        public void ColumnTitleAxisAll() { doColumn("TITLE_AXIS_ALL"); }
        public void ColumnTitleNoanswer() { doColumn("TITLE_NOANSWER"); }
        public void ColumnTitleUnfit() { doColumn("TITLE_UNFIT"); }
        public void ColumnTitleBeforeWb() { doColumn("TITLE_BEFORE_WB"); }
        public void ColumnFlagStatisticsParameter() { doColumn("FLAG_STATISTICS_PARAMETER"); }
        public void ColumnTitleStatisticsParameter() { doColumn("TITLE_STATISTICS_PARAMETER"); }
        public void ColumnFlagTotal() { doColumn("FLAG_TOTAL"); }
        public void ColumnTitleTotal() { doColumn("TITLE_TOTAL"); }
        public void ColumnDpSum() { doColumn("DP_SUM"); }
        public void ColumnFlagAvr() { doColumn("FLAG_AVR"); }
        public void ColumnTitleAvr() { doColumn("TITLE_AVR"); }
        public void ColumnDpAvr() { doColumn("DP_AVR"); }
        public void ColumnFlagSd() { doColumn("FLAG_SD"); }
        public void ColumnTitleSd() { doColumn("TITLE_SD"); }
        public void ColumnDpSd() { doColumn("DP_SD"); }
        public void ColumnFlagMin() { doColumn("FLAG_MIN"); }
        public void ColumnTitleMin() { doColumn("TITLE_MIN"); }
        public void ColumnDpMin() { doColumn("DP_MIN"); }
        public void ColumnFlagMax() { doColumn("FLAG_MAX"); }
        public void ColumnTitleMax() { doColumn("TITLE_MAX"); }
        public void ColumnDpMax() { doColumn("DP_MAX"); }
        public void ColumnFlagMedian() { doColumn("FLAG_MEDIAN"); }
        public void ColumnTitleMedian() { doColumn("TITLE_MEDIAN"); }
        public void ColumnDpMedian() { doColumn("DP_MEDIAN"); }
        public void ColumnDpWeight() { doColumn("DP_WEIGHT"); }
        public void ColumnDpWeightAvr() { doColumn("DP_WEIGHT_AVR"); }
        public void ColumnTestGtFlag() { doColumn("TEST_GT_FLAG"); }
        public void ColumnTestCrossFlag() { doColumn("TEST_CROSS_FLAG"); }
        public void ColumnTestTypeGt() { doColumn("TEST_TYPE_GT"); }
        public void ColumnTestTypeCross() { doColumn("TEST_TYPE_CROSS"); }
        public void ColumnTestSignificanceLvGt() { doColumn("TEST_SIGNIFICANCE_LV_GT"); }
        public void ColumnTestSignificanceLvCross() { doColumn("TEST_SIGNIFICANCE_LV_CROSS"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnLanguage(); // PK
        }
        protected override String getTableDbName() { return "T_DEFAULT_ENV_BASE"; }
        public RAFunction<TDefaultEnvColorInfoCCB, TDefaultEnvBaseCQ> DerivedTDefaultEnvColorInfoCList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDefaultEnvColorInfoCCB, TDefaultEnvBaseCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDefaultEnvColorInfoCCB> subQuery, TDefaultEnvBaseCQ cq, String aliasName)
                { cq.xsderiveTDefaultEnvColorInfoCList(function, subQuery, aliasName); });
        }
    }
}
