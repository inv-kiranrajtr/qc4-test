
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
    public class BsTOutputSettingCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSettingCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? qcwebid) {
            assertObjectNotNull("qcwebid", qcwebid);
            BsTOutputSettingCB cb = this;
            cb.Query().SetQcwebid_Equal(qcwebid);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_Qcwebid_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_Qcwebid_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TOutputSettingCQ Query() {
            return this.ConditionQuery;
        }

        public TOutputSettingCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TOutputSettingCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TOutputSettingCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TOutputSettingCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TOutputSettingCB> unionQuery) {
            TOutputSettingCB cb = new TOutputSettingCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSettingCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TOutputSettingCB> unionQuery) {
            TOutputSettingCB cb = new TOutputSettingCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TOutputSettingCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfo(); });
            if (_nssTQcwebSurveyInfo == null || !_nssTQcwebSurveyInfo.HasConditionQuery)
            { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfo()); }
            return _nssTQcwebSurveyInfo;
        }
        protected TOutputHistoryItemNss _nssTOutputHistoryItem;
        public TOutputHistoryItemNss NssTOutputHistoryItem { get {
            if (_nssTOutputHistoryItem == null) { _nssTOutputHistoryItem = new TOutputHistoryItemNss(null); }
            return _nssTOutputHistoryItem;
        }}
        public TOutputHistoryItemNss SetupSelect_TOutputHistoryItem() {
            doSetupSelect(delegate { return Query().QueryTOutputHistoryItem(); });
            if (_nssTOutputHistoryItem == null || !_nssTOutputHistoryItem.HasConditionQuery)
            { _nssTOutputHistoryItem = new TOutputHistoryItemNss(Query().QueryTOutputHistoryItem()); }
            return _nssTOutputHistoryItem;
        }

        protected TQcwebSurveyInfoNss _nssTQcwebSurveyInfoAsOne;
        public TQcwebSurveyInfoNss NssTQcwebSurveyInfoAsOne { get {
            if (_nssTQcwebSurveyInfoAsOne == null) { _nssTQcwebSurveyInfoAsOne = new TQcwebSurveyInfoNss(null); }
            return _nssTQcwebSurveyInfoAsOne;
        }}
        public TQcwebSurveyInfoNss SetupSelect_TQcwebSurveyInfoAsOne() {
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfoAsOne(); });
            if (_nssTQcwebSurveyInfoAsOne == null || !_nssTQcwebSurveyInfoAsOne.HasConditionQuery)
            { _nssTQcwebSurveyInfoAsOne = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfoAsOne()); }
            return _nssTQcwebSurveyInfoAsOne;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TOutputSettingCBSpecification _specification;
        public TOutputSettingCBSpecification Specify() {
            if (_specification == null) { _specification = new TOutputSettingCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TOutputSettingCQ> {
			protected BsTOutputSettingCB _myCB;
			public MySpQyCall(BsTOutputSettingCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TOutputSettingCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TOutputSettingCB> ColumnQuery(SpecifyQuery<TOutputSettingCB> leftSpecifyQuery) {
            return new HpColQyOperand<TOutputSettingCB>(delegate(SpecifyQuery<TOutputSettingCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TOutputSettingCB xcreateColumnQueryCB() {
            TOutputSettingCB cb = new TOutputSettingCB();
            cb.xsetupForColumnQuery((TOutputSettingCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TOutputSettingCB> orQuery) {
            xorQ((TOutputSettingCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TOutputSettingCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TOutputSettingCBColQySpQyCall(mainCB));
        }
    }

    public class TOutputSettingCBColQySpQyCall : HpSpQyCall<TOutputSettingCQ> {
        protected TOutputSettingCB _mainCB;
        public TOutputSettingCBColQySpQyCall(TOutputSettingCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TOutputSettingCQ qy() { return _mainCB.Query(); } 
    }

    public class TOutputSettingCBSpecification : AbstractSpecification<TOutputSettingCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TOutputHistoryItemCBSpecification _tOutputHistoryItem;
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfoAsOne;
        public TOutputSettingCBSpecification(ConditionBean baseCB, HpSpQyCall<TOutputSettingCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnOutputFileType() { doColumn("OUTPUT_FILE_TYPE"); }
        public void ColumnPartitionFlag() { doColumn("PARTITION_FLAG"); }
        public void ColumnLayoutFlag() { doColumn("LAYOUT_FLAG"); }
        public void ColumnOutputType() { doColumn("OUTPUT_TYPE"); }
        public void ColumnNoAnswerChar() { doColumn("NO_ANSWER_CHAR"); }
        public void ColumnUnmacthChar() { doColumn("UNMACTH_CHAR"); }
        public void ColumnMultiItemType() { doColumn("MULTI_ITEM_TYPE"); }
        public void ColumnNumberType() { doColumn("NUMBER_TYPE"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnQcwebid(); // PK
        }
        protected override String getTableDbName() { return "T_OUTPUT_SETTING"; }
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
		    protected HpSpQyCall<TOutputSettingCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TOutputSettingCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TOutputHistoryItemCBSpecification SpecifyTOutputHistoryItem() {
            assertForeign("tOutputHistoryItem");
            if (_tOutputHistoryItem == null) {
                _tOutputHistoryItem = new TOutputHistoryItemCBSpecification(_baseCB, new TOutputHistoryItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tOutputHistoryItem.xsetSyncQyCall(new TOutputHistoryItemSpQyCall(xsyncQyCall())); }
            }
            return _tOutputHistoryItem;
        }
		public class TOutputHistoryItemSpQyCall : HpSpQyCall<TOutputHistoryItemCQ> {
		    protected HpSpQyCall<TOutputSettingCQ> _qyCall;
		    public TOutputHistoryItemSpQyCall(HpSpQyCall<TOutputSettingCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTOutputHistoryItem(); }
			public TOutputHistoryItemCQ qy() { return _qyCall.qy().QueryTOutputHistoryItem(); }
		}
        public TQcwebSurveyInfoCBSpecification SpecifyTQcwebSurveyInfoAsOne() {
            assertForeign("tQcwebSurveyInfoAsOne");
            if (_tQcwebSurveyInfoAsOne == null) {
                _tQcwebSurveyInfoAsOne = new TQcwebSurveyInfoCBSpecification(_baseCB, new TQcwebSurveyInfoAsOneSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tQcwebSurveyInfoAsOne.xsetSyncQyCall(new TQcwebSurveyInfoAsOneSpQyCall(xsyncQyCall())); }
            }
            return _tQcwebSurveyInfoAsOne;
        }
		public class TQcwebSurveyInfoAsOneSpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
		    protected HpSpQyCall<TOutputSettingCQ> _qyCall;
		    public TQcwebSurveyInfoAsOneSpQyCall(HpSpQyCall<TOutputSettingCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfoAsOne(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfoAsOne(); }
		}
        public RAFunction<TOutputHistoryItemCB, TOutputSettingCQ> DerivedTOutputHistoryItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TOutputHistoryItemCB, TOutputSettingCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TOutputHistoryItemCB> subQuery, TOutputSettingCQ cq, String aliasName)
                { cq.xsderiveTOutputHistoryItemList(function, subQuery, aliasName); });
        }
    }
}
