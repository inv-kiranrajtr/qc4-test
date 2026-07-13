
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
    public class BsTDataProcessNewItemCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataProcessNewItemCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? dataEditId) {
            assertObjectNotNull("dataEditId", dataEditId);
            BsTDataProcessNewItemCB cb = this;
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
        public TDataProcessNewItemCQ Query() {
            return this.ConditionQuery;
        }

        public TDataProcessNewItemCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TDataProcessNewItemCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TDataProcessNewItemCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TDataProcessNewItemCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TDataProcessNewItemCB> unionQuery) {
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDataProcessNewItemCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TDataProcessNewItemCB> unionQuery) {
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TDataProcessNewItemCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TDataEditListNss _nssTDataEditList;
        public TDataEditListNss NssTDataEditList { get {
            if (_nssTDataEditList == null) { _nssTDataEditList = new TDataEditListNss(null); }
            return _nssTDataEditList;
        }}
        public TDataEditListNss SetupSelect_TDataEditList() {
            doSetupSelect(delegate { return Query().QueryTDataEditList(); });
            if (_nssTDataEditList == null || !_nssTDataEditList.HasConditionQuery)
            { _nssTDataEditList = new TDataEditListNss(Query().QueryTDataEditList()); }
            return _nssTDataEditList;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TDataProcessNewItemCBSpecification _specification;
        public TDataProcessNewItemCBSpecification Specify() {
            if (_specification == null) { _specification = new TDataProcessNewItemCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TDataProcessNewItemCQ> {
			protected BsTDataProcessNewItemCB _myCB;
			public MySpQyCall(BsTDataProcessNewItemCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TDataProcessNewItemCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TDataProcessNewItemCB> ColumnQuery(SpecifyQuery<TDataProcessNewItemCB> leftSpecifyQuery) {
            return new HpColQyOperand<TDataProcessNewItemCB>(delegate(SpecifyQuery<TDataProcessNewItemCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TDataProcessNewItemCB xcreateColumnQueryCB() {
            TDataProcessNewItemCB cb = new TDataProcessNewItemCB();
            cb.xsetupForColumnQuery((TDataProcessNewItemCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TDataProcessNewItemCB> orQuery) {
            xorQ((TDataProcessNewItemCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TDataProcessNewItemCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TDataProcessNewItemCBColQySpQyCall(mainCB));
        }
    }

    public class TDataProcessNewItemCBColQySpQyCall : HpSpQyCall<TDataProcessNewItemCQ> {
        protected TDataProcessNewItemCB _mainCB;
        public TDataProcessNewItemCBColQySpQyCall(TDataProcessNewItemCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TDataProcessNewItemCQ qy() { return _mainCB.Query(); } 
    }

    public class TDataProcessNewItemCBSpecification : AbstractSpecification<TDataProcessNewItemCQ> {
        protected TDataEditListCBSpecification _tDataEditList;
        public TDataProcessNewItemCBSpecification(ConditionBean baseCB, HpSpQyCall<TDataProcessNewItemCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        public void ColumnSrcItemId() { doColumn("SRC_ITEM_ID"); }
        public void ColumnNewItemId() { doColumn("NEW_ITEM_ID"); }
        public void ColumnNewItemName() { doColumn("NEW_ITEM_NAME"); }
        public void ColumnNewLv1title() { doColumn("NEW_LV1TITLE"); }
        public void ColumnNewLv2title() { doColumn("NEW_LV2TITLE"); }
        public void ColumnNewAnswerType() { doColumn("NEW_ANSWER_TYPE"); }
        public void ColumnNewCategoryCount() { doColumn("NEW_CATEGORY_COUNT"); }
        public void ColumnUnfitFlag() { doColumn("UNFIT_FLAG"); }
        public void ColumnConditionDiv() { doColumn("CONDITION_DIV"); }
        public void ColumnSeriesFlag() { doColumn("SERIES_FLAG"); }
        public void ColumnUpperFlag() { doColumn("UPPER_FLAG"); }
        public void ColumnBottomFlag() { doColumn("BOTTOM_FLAG"); }
        public void ColumnNoanswerZeroFlag() { doColumn("NOANSWER_ZERO_FLAG"); }
        public void ColumnSelectMethod() { doColumn("SELECT_METHOD"); }
        public void ColumnTargetCategoryCondition() { doColumn("TARGET_CATEGORY_CONDITION"); }
        public void ColumnCalcType() { doColumn("CALC_TYPE"); }
        public void ColumnFormulaString() { doColumn("FORMULA_STRING"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnDataEditId(); // PK
        }
        protected override String getTableDbName() { return "T_DATA_PROCESS_NEW_ITEM"; }
        public TDataEditListCBSpecification SpecifyTDataEditList() {
            assertForeign("tDataEditList");
            if (_tDataEditList == null) {
                _tDataEditList = new TDataEditListCBSpecification(_baseCB, new TDataEditListSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDataEditList.xsetSyncQyCall(new TDataEditListSpQyCall(xsyncQyCall())); }
            }
            return _tDataEditList;
        }
		public class TDataEditListSpQyCall : HpSpQyCall<TDataEditListCQ> {
		    protected HpSpQyCall<TDataProcessNewItemCQ> _qyCall;
		    public TDataEditListSpQyCall(HpSpQyCall<TDataProcessNewItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataEditList(); }
			public TDataEditListCQ qy() { return _qyCall.qy().QueryTDataEditList(); }
		}
        public RAFunction<TDataProcessNewCategoryCB, TDataProcessNewItemCQ> DerivedTDataProcessNewCategoryList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDataProcessNewCategoryCB, TDataProcessNewItemCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDataProcessNewCategoryCB> subQuery, TDataProcessNewItemCQ cq, String aliasName)
                { cq.xsderiveTDataProcessNewCategoryList(function, subQuery, aliasName); });
        }
        public RAFunction<TDataProcessNewItemSrcCB, TDataProcessNewItemCQ> DerivedTDataProcessNewItemSrcList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TDataProcessNewItemSrcCB, TDataProcessNewItemCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TDataProcessNewItemSrcCB> subQuery, TDataProcessNewItemCQ cq, String aliasName)
                { cq.xsderiveTDataProcessNewItemSrcList(function, subQuery, aliasName); });
        }
        public RAFunction<TIntegConditionCB, TDataProcessNewItemCQ> DerivedTIntegConditionList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TIntegConditionCB, TDataProcessNewItemCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TIntegConditionCB> subQuery, TDataProcessNewItemCQ cq, String aliasName)
                { cq.xsderiveTIntegConditionList(function, subQuery, aliasName); });
        }
    }
}
