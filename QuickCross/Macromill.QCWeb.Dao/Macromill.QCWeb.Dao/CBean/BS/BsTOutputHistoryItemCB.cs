
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
    public class BsTOutputHistoryItemCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputHistoryItemCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_HISTORY_ITEM"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? outputHistoryItemId) {
            assertObjectNotNull("outputHistoryItemId", outputHistoryItemId);
            BsTOutputHistoryItemCB cb = this;
            cb.Query().SetOutputHistoryItemId_Equal(outputHistoryItemId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_OutputHistoryItemId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_OutputHistoryItemId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputHistoryItemCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputHistoryItemCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputHistoryItemCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputHistoryItemCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputHistoryItemCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputHistoryItemCB> unionQuery) {
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputHistoryItemCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputHistoryItemCB> unionQuery) {
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputHistoryItemCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
        protected TOutputSettingNss _nssTOutputSetting;
        public TOutputSettingNss NssTOutputSetting { get {
            if (_nssTOutputSetting == null) { _nssTOutputSetting = new TOutputSettingNss(null); }
            return _nssTOutputSetting;
        }}
        public TOutputSettingNss SetupSelect_TOutputSetting() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTOutputSetting(); });
            if (_nssTOutputSetting == null || !_nssTOutputSetting.HasConditionQuery)
            { _nssTOutputSetting = new TOutputSettingNss(Query().QueryTOutputSetting()); }
            return _nssTOutputSetting;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputHistoryItemCBSpecification _specification;
        public TOutputHistoryItemCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputHistoryItemCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputHistoryItemCQ> {
			protected BsTOutputHistoryItemCB _myCB;
			public MySpQyCall(BsTOutputHistoryItemCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputHistoryItemCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputHistoryItemCB> ColumnQuery(SpecifyQuery<TOutputHistoryItemCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputHistoryItemCB>(delegate(SpecifyQuery<TOutputHistoryItemCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputHistoryItemCB xcreateColumnQueryCB() {
            TOutputHistoryItemCB cb = new TOutputHistoryItemCB();
            cb.xsetupForColumnQuery((TOutputHistoryItemCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputHistoryItemCB> orQuery) {
            xorQ((TOutputHistoryItemCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputHistoryItemCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputHistoryItemCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputHistoryItemCBColQySpQyCall : HpSpQyCall<TOutputHistoryItemCQ> {
        protected TOutputHistoryItemCB _mainCB;
        public TOutputHistoryItemCBColQySpQyCall(TOutputHistoryItemCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputHistoryItemCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputHistoryItemCBSpecification : AbstractSpecification<TOutputHistoryItemCQ> {
        protected TOutputSettingCBSpecification _tOutputSetting;
        public TOutputHistoryItemCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputHistoryItemCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnOutputHistoryItemId() { doColumn("OUTPUT_HISTORY_ITEM_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnItemInfoId() { doColumn("ITEM_INFO_ID"); }
        public void ColumnSortNo() { doColumn("SORT_NO"); }
        public void ColumnOutputFlag() { doColumn("OUTPUT_FLAG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnOutputHistoryItemId(); // PK
            if (qyCall().qy().hasConditionQueryTOutputSetting()
                    || qyCall().qy().xgetReferrerQuery() is TOutputSettingCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_OUTPUT_HISTORY_ITEM"; }
        public TOutputSettingCBSpecification SpecifyTOutputSetting() {
            assertForeign("tOutputSetting");
            if (_tOutputSetting == null) {
                _tOutputSetting = new TOutputSettingCBSpecification(_baseCB, new TOutputSettingSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputSetting.xsetSyncQyCall(new TOutputSettingSpQyCall(xsyncQyCall())); }
            }
            return _tOutputSetting;
        }
		public class TOutputSettingSpQyCall : HpSpQyCall<TOutputSettingCQ> {
		    protected HpSpQyCall<TOutputHistoryItemCQ> _qyCall;
		    public TOutputSettingSpQyCall(HpSpQyCall<TOutputHistoryItemCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputSetting(); }
			public TOutputSettingCQ qy() { return _qyCall.qy().QueryTOutputSetting(); }
		}
    }
}
